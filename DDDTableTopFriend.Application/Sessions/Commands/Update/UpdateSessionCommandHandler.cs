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
    private readonly IUnitOfWork _unitOfWork;
    public UpdateSessionCommandHandler(
        ISessionRepository sessionRepository,
        ICachingService cachingService,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _cachingService = cachingService;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
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

        var sessionOrError = session.Update(
            request.Name,
            request.Description,
            request.DateTime,
            request.Duration,
            _dateTimeProvider.UtcNow
        );

        if (sessionOrError.IsError)
            return sessionOrError.Errors;

        return await _unitOfWork.Execute(async _ =>
        {
            await _sessionRepository.Update(sessionOrError.Value);
            var result = sessionOrError.Value.Adapt<SessionResult>();
            await _cachingService.SetCacheValueAsync(
                request.Id.ToString(),
                result);
            return result;
        },
        cancellationToken);
    }
}
