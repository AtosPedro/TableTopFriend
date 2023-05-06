using DDDTableTopFriend.Domain.Common.Enums;
using DDDTableTopFriend.Domain.Common.Models;

namespace DDDTableTopFriend.Domain.AggregateUser.ValueObjects;

public class Validation : ValueObject
{
    public StatusValidation Value { get; set; }
    public DateTime? ValidationDate { get; set; }

    private Validation(
        StatusValidation value,
        DateTime? validationDate = null)
    {
        Value = value;
        ValidationDate = validationDate;
    }

    public static Validation Create() => new Validation(StatusValidation.NotValidated, null);

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
}
