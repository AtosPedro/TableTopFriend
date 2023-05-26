using TableTopFriend.Application.Common.Interfaces.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TableTopFriend.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            _dateTimeProvider.UtcNow);

        var result = await next();

        if (result.IsError)
        {
            var errorList = result.Errors ?? new List<Error>();
            var errors = string.Join(',', errorList);
            _logger.LogError(
                "Request Failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                errors,
                _dateTimeProvider.UtcNow);
        }

        _logger.LogInformation(
            "Completed request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            _dateTimeProvider.UtcNow);

        return result!;
    }
}
