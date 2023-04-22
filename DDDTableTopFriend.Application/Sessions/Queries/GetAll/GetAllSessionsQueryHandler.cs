using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Sessions.Common;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.GetAll;

public class GetAllSessionsQueryHandler : IRequestHandler<GetAllSessionsQuery, ErrorOr<IReadOnlyList<SessionResult>>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICachingService _cachingService;

    public GetAllSessionsQueryHandler(
        ISessionRepository sessionRepository,
        ICachingService cachingService)
    {
        _sessionRepository = sessionRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<IReadOnlyList<SessionResult>>> Handle(
        GetAllSessionsQuery request,
        CancellationToken cancellationToken)
    {
        var cachedSessions = await _cachingService.GetManyCacheValueAsync<SessionResult>(
            c => c.UserId == request.UserId &&
            c.CampaignId == request.CampaignId);

        if (cachedSessions is not null && cachedSessions.Count() > 0)
            return cachedSessions.AsReadOnly();

        var sessions = await _sessionRepository.GetAll(
            UserId.Create(request.UserId),
            CampaignId.Create(request.CampaignId),
            cancellationToken);

        var sessionsResult = new List<SessionResult>();
        foreach (var campaign in sessions)
        {
            var result = campaign.Adapt<SessionResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            sessionsResult.Add(result);
        }

        return sessionsResult;
    }
}
