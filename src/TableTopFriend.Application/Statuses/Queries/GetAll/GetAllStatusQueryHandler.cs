﻿using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Statuses.Common;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Statuses.Queries.GetAll;

public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, ErrorOr<IReadOnlyList<StatusResult>>>
{
    private readonly IStatusRepository _statusRepository;
    private readonly ICachingService _cachingService;
    public GetAllStatusQueryHandler(
        IStatusRepository statusRepository,
        ICachingService cachingService)
    {
        _statusRepository = statusRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<IReadOnlyList<StatusResult>>> Handle(
        GetAllStatusQuery request,
        CancellationToken cancellationToken)
    {
        var cachedSessions = await _cachingService.GetManyCacheValueAsync<StatusResult>(w => w.UserId == request.UserId);
        if (cachedSessions.Any())
            return cachedSessions;

        var statuses = await _statusRepository.GetAll(
            UserId.Create(request.UserId),
            cancellationToken);

        var statusResult = new List<StatusResult>();
        foreach (var status in statuses)
        {
            var result = status.Adapt<StatusResult>();
            statusResult.Add(result);
            await _cachingService.SetCacheValueAsync<StatusResult>(result.Id.ToString(), result);
        }

        return statusResult;
    }
}
