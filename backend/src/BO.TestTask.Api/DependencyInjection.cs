using BO.TestTask.Api.ExceptionHandlers;

namespace BO.TestTask.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddProblemDetails();
        services.AddExceptionHandlers();

        return services;
    }

    private static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<NotFoundExceptionHandler>();

        // Default exception handler. Must be added to pipeline at the end
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
}
