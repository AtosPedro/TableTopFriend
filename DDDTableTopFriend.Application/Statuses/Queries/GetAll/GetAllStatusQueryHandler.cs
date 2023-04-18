using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Statuses.Common;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using ErrorOr;
using Mapster;
using MediatR;

namespace DDDTableTopFriend.Application.Statuses.Queries.GetAll;

public class GetAllStatusQueryHandler : IRequestHandler<GetAllStatusQuery, ErrorOr<IReadOnlyList<StatusResult>>>
{
    private readonly IStatusRepository _statusRepository;
    public GetAllStatusQueryHandler(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<StatusResult>>> Handle(
        GetAllStatusQuery request,
        CancellationToken cancellationToken)
    {
        var statuses = await _statusRepository.GetAll(
            UserId.Create(request.UserId),
            cancellationToken);

        var campaignsResult = new List<StatusResult>();
        foreach (var campaign in statuses)
            campaignsResult.Add(campaign.Adapt<StatusResult>());

        return campaignsResult;
    }
}
