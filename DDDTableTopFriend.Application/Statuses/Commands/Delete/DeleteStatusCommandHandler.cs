using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Delete;

public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, ErrorOr<bool>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public DeleteStatusCommandHandler(
        IStatusRepository statusRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _statusRepository = statusRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteStatusCommand request,
        CancellationToken cancellationToken)
    {
        var status = await _statusRepository.GetById(StatusId.Create(request.StatusId), cancellationToken);

        if(status is null)
            return Errors.Status.NotRegistered;

        status.MarkToDelete(_dateTimeProvider.UtcNow);
        await _statusRepository.Remove(status);

        return status is not null;
    }
}
