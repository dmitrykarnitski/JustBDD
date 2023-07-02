using Microsoft.AspNetCore.Builder;
using Sample.Api.Api.IoC;
using Sample.Api.Api.RequestPipeline.ErrorHandling;
using Sample.Api.Api.RequestPipeline.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

var app = builder.Build();

app.UseUnhandledErrorHandling(app.Environment);

app.UseSwaggerDocs();

app.UseHttpLogging();

app.UseApplicationErrorHandling();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
