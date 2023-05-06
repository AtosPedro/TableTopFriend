namespace DDDTableTopFriend.Domain.Common.Exceptions;

public class PasswordLengthException : Exception
{
    public PasswordLengthException()
    {
    }

    public PasswordLengthException(string? message) : base(message)
    {
    }
}

