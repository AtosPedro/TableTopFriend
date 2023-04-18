using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Statuses.Common;
using DDDTableTopFriend.Domain.AggregateStatus;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Create;

public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, ErrorOr<StatusResult>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateStatusCommandHandler(
        IStatusRepository statusRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _statusRepository = statusRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<StatusResult>> Handle(
        CreateStatusCommand request,
        CancellationToken cancellationToken)
    {
        var status = Status.Create(
            request.Name,
            request.Description,
            request.Quantity,
            _dateTimeProvider.UtcNow
        );

        await _statusRepository.Add(status, cancellationToken);
        return status.Adapt<StatusResult>();
    }
}
