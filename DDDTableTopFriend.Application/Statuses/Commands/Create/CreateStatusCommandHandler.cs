using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Statuses.Common;
using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
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
        var status = (await _statusRepository.Search(
            c => c.Name == request.Name &&
            c.UserId == UserId.Create(request.UserId),
            cancellationToken)).FirstOrDefault();

        if (status is not null)
            return Errors.Status.AlreadyRegistered;

        status = Status.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.Quantity,
            _dateTimeProvider.UtcNow
        );

        await _statusRepository.Add(status, cancellationToken);
        return status.Adapt<StatusResult>();
    }
}
