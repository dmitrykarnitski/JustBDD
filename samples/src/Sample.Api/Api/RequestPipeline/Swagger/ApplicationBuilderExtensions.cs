using Microsoft.AspNetCore.Builder;

namespace Sample.Api.Api.RequestPipeline.Swagger;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
