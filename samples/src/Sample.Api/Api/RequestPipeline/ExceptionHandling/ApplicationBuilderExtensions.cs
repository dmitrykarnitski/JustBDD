using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Sample.Api.Api.RequestPipeline.ExceptionHandling;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.UseMiddleware<ExceptionHandlingMiddleware>();
            });
        }

        return app;
    }
}
