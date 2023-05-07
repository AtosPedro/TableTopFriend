using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

public sealed class Validation : ValueObject
{
    public StatusValidation Value { get; private set; }
    public DateTime? ValidationDate { get; private set; }

    private Validation(
        StatusValidation value,
        DateTime? validationDate = null)
    {
        Value = value;
        ValidationDate = validationDate;
    }

    public static Validation Create() => new(StatusValidation.NotValidated, null);

    public void Validate(DateTime validationDate)
    {
        if (Value == StatusValidation.Validated)
            return;

        Value = StatusValidation.Validated;
        ValidationDate = validationDate;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return ValidationDate;
    }

#pragma warning disable CS8618
    private Validation()
    {
    }
#pragma warning restore CS8618
}
