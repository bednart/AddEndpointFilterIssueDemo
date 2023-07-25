var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app
    .MapGet("/", () => "Hello World!")
    .AddEndpointFilter<OperationCancelledFilter>();

app.Run();

class OperationCancelledFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (OperationCanceledException)
        {

            return Results.StatusCode(499);
        }
    }
}


//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app
//    .MapGet("/", () => "Hello World!")
//    .AddEndpointFilter(async (context, next) =>
//    {
//        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("program");

//        try
//        {
//            return await next(context);
//        }
//        catch (OperationCanceledException ex)
//        {
//            logger.LogDebug(ex, "Request was cancelled");

//            return Results.StatusCode(499);
//        }
//    });

//app.Run();
