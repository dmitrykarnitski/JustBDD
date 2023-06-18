using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Sample.Api.Api.RequestPipeline.ErrorHandling;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseUnhandledErrorHandling(this IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.UseMiddleware<UnhandledErrorHandlingMiddleware>();
            });
        }

        return app;
    }

    public static IApplicationBuilder UseApplicationErrorHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<ApplicationErrorHandlingMiddleware>();

        return app;
    }
}
