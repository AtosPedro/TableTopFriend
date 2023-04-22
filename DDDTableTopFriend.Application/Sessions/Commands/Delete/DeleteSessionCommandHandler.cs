using DDDTableTopFriend.Application.Campaigns.Common;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Sessions.Common;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using DDDTableTopFriend.Domain.Common.Models;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Delete;

public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, ErrorOr<bool>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICampaignRepository _campaignRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteSessionCommandHandler(
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

    public async Task<ErrorOr<bool>> Handle(
        DeleteSessionCommand request,
        CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetById(
            SessionId.Create(request.Id),
            cancellationToken);

        if (session is null)
            return Errors.Session.NotScheduled;

        var campaign = await _campaignRepository.GetById(
            session.CampaignId,
            cancellationToken);

        if (campaign is null)
            return Errors.Campaign.NotRegistered;

        session.MarkToDelete(_dateTimeProvider.UtcNow);
        campaign.RemoveSessionId(
            SessionId.Create(session.GetId().Value),
            _dateTimeProvider.UtcNow);

        List<IDomainEvent> domainEvents = new(session.DomainEvents.Concat(campaign.DomainEvents));
        return await _unitOfWork.Execute(async _ =>
        {
            await _campaignRepository.Update(campaign);
            await _sessionRepository.Remove(session);
            await _cachingService.RemoveCacheValueAsync<SessionResult>(request.Id.ToString());
            await _cachingService.RemoveCacheValueAsync<CampaignResult>(session.CampaignId.Value.ToString());
            return session is not null;
        },
        domainEvents,
        cancellationToken);
    }
}
