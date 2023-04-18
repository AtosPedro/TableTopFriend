using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Statuses.Common;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Update;

public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, ErrorOr<StatusResult>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    public UpdateStatusCommandHandler(
        IStatusRepository statusRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _statusRepository = statusRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<StatusResult>> Handle(
        UpdateStatusCommand request,
        CancellationToken cancellationToken)
    {
        var status = await _statusRepository.GetById(
            StatusId.Create(request.StatusId),
            cancellationToken);

        if (status is null)
            return Errors.Status.NotRegistered;

        status.Update(
            request.Name,
            request.Description,
            request.Quantity,
            _dateTimeProvider.UtcNow
        );

        await _statusRepository.Update(status);
        return status.Adapt<StatusResult>();
    }
}
