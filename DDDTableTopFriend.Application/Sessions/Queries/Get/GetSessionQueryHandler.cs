using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Sessions.Common;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Queries.Get;

public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, ErrorOr<SessionResult>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICachingService _cachingService;

    public GetSessionQueryHandler(
        ISessionRepository sessionRepository,
        ICachingService cachingService)
    {
        _sessionRepository = sessionRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<SessionResult>> Handle(
        GetSessionQuery request,
        CancellationToken cancellationToken)
    {
        var cachedSession = await _cachingService.GetCacheValueAsync<SessionResult>(request.Id.ToString());
        if (cachedSession is not null)
            return cachedSession;

        var session = await _sessionRepository.GetById(
            SessionId.Create(request.Id),
            cancellationToken);

        if (session is null)
            return Errors.Session.NotScheduled;

        var result = session.Adapt<SessionResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(), request);
        return result;
    }
}
