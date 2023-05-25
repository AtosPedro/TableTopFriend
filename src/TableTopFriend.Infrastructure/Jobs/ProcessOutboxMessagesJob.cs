using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Domain.Common.Models;
using TableTopFriend.Infrastructure.Persistence.Context;
using TableTopFriend.Infrastructure.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace TableTopFriend.Infrastructure.Jobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ProcessOutboxMessagesJob(
        ApplicationDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessOutboxMessagesJob> logger,
        IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var message in messages)
        {
            try
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    message.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ContractResolver = new PrivateResolver()
                    });

                if (domainEvent is null)
                {
                    _logger.LogWarning(
                        "The domain event {@type} was not published",
                        message.Type);
                    continue;
                }

                await _publisher.Publish(domainEvent, context.CancellationToken);
                message.ProcessedOnUtc = _dateTimeProvider.UtcNow;
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation(
                        "The domain event {@type} with data {@data} was published at {@dateUtc}",
                        message.Type,
                        domainEvent,
                        message.ProcessedOnUtc);
            }
            catch (Exception ex)
            {
                message.Error = ex.Message;
                _logger.LogCritical(
                   "A critical error has occurred while publishing the domain events {@Error}",
                   ex.Message);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
