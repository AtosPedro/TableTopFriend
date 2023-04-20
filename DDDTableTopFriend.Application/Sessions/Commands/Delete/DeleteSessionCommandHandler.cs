using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Delete;

public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, ErrorOr<bool>>
{
    public DeleteSessionCommandHandler()
    {
    }

    public async Task<ErrorOr<bool>> Handle(
        DeleteSessionCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
