using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Statuses.Common;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Commands.Delete;

public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, ErrorOr<bool>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICachingService _cachingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    public DeleteStatusCommandHandler(
        IStatusRepository statusRepository,
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork,
        ICachingService cachingService)
    {
        _statusRepository = statusRepository;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteStatusCommand request,
        CancellationToken cancellationToken)
    {
        var status = await _statusRepository.GetById(StatusId.Create(request.StatusId), cancellationToken);

        if (status is null)
            return Errors.Status.NotRegistered;

        status.MarkToDelete(_dateTimeProvider.UtcNow);

        return await _unitOfWork.Execute(async _ =>
        {
            await _statusRepository.Remove(status);
            await _cachingService.RemoveCacheValueAsync<StatusResult>(status.GetId().Value.ToString());
            return status is not null;
        },
        cancellationToken);
    }
}
