using FluentValidation;

namespace DDDTableTopFriend.Application.Sessions.Commands.Schedule;

public class ScheduleSessionCommandValidator : AbstractValidator<ScheduleSessionCommand>
{
    public ScheduleSessionCommandValidator()
    {
        RuleFor(c => c.CampaignId)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.DateTime)
            .NotEmpty()
            .NotNull();
    }
}
