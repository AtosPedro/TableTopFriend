using TableTopFriend.Application.Campaigns.Common;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Sessions.Common;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateSession;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Sessions.Commands.Schedule;

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
        var session = (await _sessionRepository.SearchAsNoTracking(
            c => c.DateTime == request.DateTime &&
            c.CampaignId == CampaignId.Create(request.CampaignId),
            cancellationToken)).FirstOrDefault();

        if (session is not null)
            return Errors.Session.AlreadyScheduled;

        var campaign = await _campaignRepository.GetById(
            CampaignId.Create(request.CampaignId),
            cancellationToken);

        if (campaign is null)
            return Errors.Campaign.NotRegistered;

        var sessionOrError = Session.Create(
            UserId.Create(request.UserId),
            CampaignId.Create(request.CampaignId),
            request.Name,
            request.Description,
            request.DateTime,
            _dateTimeProvider.UtcNow
        );

        if (sessionOrError.IsError)
            return sessionOrError.Errors;

        campaign.AddSessionId(
            SessionId.Create(sessionOrError.Value.Id.Value),
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _sessionRepository.Add(sessionOrError.Value, cancellationToken);
            await _campaignRepository.Update(campaign);
            var result = sessionOrError.Value.Adapt<SessionResult>();
            var campaignResult = campaign.Adapt<CampaignResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            await _cachingService.SetCacheValueAsync(result.CampaignId.ToString(), campaignResult);
            return result;
        },
        cancellationToken);
    }
}
