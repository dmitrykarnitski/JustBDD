using Microsoft.AspNetCore.Builder;
using Sample.Api.Api.IoC;
using Sample.Api.Api.RequestPipeline.ExceptionHandling;
using Sample.Api.Api.RequestPipeline.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

var app = builder.Build();

app.UseExceptionHandling(app.Environment);

app.UseSwaggerDocs();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
