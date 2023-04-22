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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICachingService _cachingService;
    public UpdateStatusCommandHandler(
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

    public async Task<ErrorOr<StatusResult>> Handle(
        UpdateStatusCommand request,
        CancellationToken cancellationToken)
    {
        var status = await _statusRepository.GetById(
            StatusId.Create(request.Id),
            cancellationToken);

        if (status is null)
            return Errors.Status.NotRegistered;

        status.Update(
            request.Name,
            request.Description,
            request.Quantity,
            _dateTimeProvider.UtcNow
        );

        return await _unitOfWork.Execute(async _ =>
        {
            await _statusRepository.Update(status);
            var result = status.Adapt<StatusResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        status.DomainEvents,
        cancellationToken);
    }
}
