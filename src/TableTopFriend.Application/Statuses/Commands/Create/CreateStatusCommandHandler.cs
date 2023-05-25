using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Statuses.Common;
using TableTopFriend.Domain.AggregateStatus;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using TableTopFriend.Domain.Common.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Statuses.Commands.Create;

public class CreateStatusCommandHandler : IRequestHandler<CreateStatusCommand, ErrorOr<StatusResult>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICachingService _cachingService;

    public CreateStatusCommandHandler(
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
        CreateStatusCommand request,
        CancellationToken cancellationToken)
    {
        var status = (await _statusRepository.SearchAsNoTracking(
            c => c.Name.Value == request.Name &&
            c.UserId == UserId.Create(request.UserId),
            cancellationToken)).FirstOrDefault();

        if (status is not null)
            return Errors.Status.AlreadyRegistered;

        var statusOrError = Status.Create(
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.Quantity,
            _dateTimeProvider.UtcNow
        );

        if (statusOrError.IsError)
            return statusOrError.Errors;

        return await _unitOfWork.Execute(async cancellationToken =>
        {
            await _statusRepository.Add(statusOrError.Value, cancellationToken);
            var result = statusOrError.Value.Adapt<StatusResult>();
            await _cachingService.SetCacheValueAsync(result.Id.ToString(), result);
            return result;
        },
        cancellationToken);
    }
}
