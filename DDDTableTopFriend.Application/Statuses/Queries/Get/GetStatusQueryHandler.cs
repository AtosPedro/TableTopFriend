using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Application.Statuses.Common;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Queries.Get;

public class GetStatusQueryHandler : IRequestHandler<GetStatusQuery, ErrorOr<StatusResult>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly ICachingService _cachingService;
    public GetStatusQueryHandler(
        IStatusRepository statusRepository,
        ICachingService cachingService)
    {
        _statusRepository = statusRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<StatusResult>> Handle(
        GetStatusQuery request,
        CancellationToken cancellationToken)
    {
        var statusCached = await _cachingService.GetCacheValueAsync<StatusResult>(request.Id.ToString());
        if (statusCached is not null)
            return statusCached;

        var status = await _statusRepository.GetById(
            StatusId.Create(request.Id),
            cancellationToken);

        if (status is null)
            return Errors.Status.NotRegistered;

        var result = status.Adapt<StatusResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(),result);
        return result;
    }
}
