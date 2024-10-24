using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            $"[START] Handling Request={typeof(TRequest).Name} - Response={typeof(TResponse).Name} - RequestData={request}"
        );

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning(
                $"[PERFORMANCE] Handling Request={typeof(TRequest).Name} - Response={typeof(TResponse).Name} - TimeTaken={timeTaken.Milliseconds}ms"
            );
        }

        logger.LogInformation(
            $"[END] Handling Request={typeof(TRequest).Name} - Response={typeof(TResponse).Name} - TimeTaken={timeTaken.Milliseconds}ms"
        );

        return response;
    }
}
