using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
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
    public GetStatusQueryHandler(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<ErrorOr<StatusResult>> Handle(
        GetStatusQuery request,
        CancellationToken cancellationToken)
    {
        var status = await _statusRepository.GetById(
            StatusId.Create(request.Id),
            cancellationToken);

        if (status is null)
            return Errors.Status.NotRegistered;

        return status.Adapt<StatusResult>();
    }
}
