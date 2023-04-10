using DDDTableTopFriend.Application.Campaigns.Common;
using ErrorOr;
using MediatR;

namespace DDDTableTopFriend.Application.Campaigns.Commands.Join;

public class JoinCampaignCommandHandler : IRequestHandler<JoinCampaignCommand, ErrorOr<CampaignJoinedResult>>
{
    public JoinCampaignCommandHandler()
    {
    }

    public Task<ErrorOr<CampaignJoinedResult>> Handle(JoinCampaignCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
