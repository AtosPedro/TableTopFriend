using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Sessions.Common;
using DDDTableTopFriend.Domain.AggregateSession.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Sessions.Commands.Update;

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, ErrorOr<SessionResult>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    public UpdateSessionCommandHandler(
        ISessionRepository sessionRepository,
        ICachingService cachingService,
        IDateTimeProvider dateTimeProvider)
    {
        _sessionRepository = sessionRepository;
        _cachingService = cachingService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<SessionResult>> Handle(
        UpdateSessionCommand request,
        CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetById(
            SessionId.Create(request.Id),
            cancellationToken);

        if (session is null)
            return Errors.Session.NotScheduled;

        session.Update(
            request.Name,
            request.DateTime,
            request.Duration,
            _dateTimeProvider.UtcNow
        );

        await _sessionRepository.Update(session);
        var result = session.Adapt<SessionResult>();
        await _cachingService.SetCacheValueAsync<SessionResult>(
            request.Id.ToString(),
            result);
        return result;
    }
}
