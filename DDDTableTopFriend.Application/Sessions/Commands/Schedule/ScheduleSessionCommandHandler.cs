using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Sessions.Common;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using DDDTableTopFriend.Domain.Common.Models;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Schedule;

public class ScheduleSessionCommandHandler : IRequestHandler<ScheduleSessionCommand, ErrorOr<SessionResult>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    public ScheduleSessionCommandHandler(
        ISessionRepository sessionRepository,
        ICachingService cachingService,
        IDateTimeProvider dateTimeProvider,
        ICampaignRepository campaignRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _cachingService = cachingService;
        _dateTimeProvider = dateTimeProvider;
        _campaignRepository = campaignRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<SessionResult>> Handle(
        ScheduleSessionCommand request,
        CancellationToken cancellationToken)
    {
        var session = (await _sessionRepository.Search(
            c => c.DateTime == request.DateTime && c.CampaignId == CampaignId.Create(request.CampaignId),
            cancellationToken)).FirstOrDefault();

        if (session is not null)
            return Errors.Session.AlreadyScheduled;

        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.CampaignId),
            cancellationToken);

        if (campaign is null)
            return Errors.Campaign.NotRegistered;

        session = Session.Create(
            UserId.Create(request.UserId),
            CampaignId.Create(request.CampaignId),
            request.Name,
            request.DateTime,
            _dateTimeProvider.UtcNow
        );

        campaign.AddSessionId(
            SessionId.Create(session.Id.Value),
            _dateTimeProvider.UtcNow);

        List<IDomainEvent> domainEvents = new(session.DomainEvents.Concat(campaign.DomainEvents));
        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _sessionRepository.Add(session, cancellationToken);
            await _campaignRepository.Update(campaign);
            var result = session.Adapt<SessionResult>();
            var campaignResult = campaign.Adapt<CampaignResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            await _cachingService.SetCacheValueAsync(result.CampaignId.ToString(), campaignResult);
            return result;
        },
        domainEvents,
        cancellationToken);
    }
}
