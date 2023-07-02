using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Api.Attributes;

public sealed class LogActionExecutionAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<LogActionExecutionAttribute>>();

        var controller = context.ActionDescriptor.RouteValues["controller"]!;
        var action = context.ActionDescriptor.RouteValues["action"]!;
        var httpMethod = context.HttpContext.Request.Method;
        var pathTemplate = context.ActionDescriptor.AttributeRouteInfo?.Template;
        var path = pathTemplate is not null
            ? "/" + pathTemplate
            : string.Empty;

        var scope = new Dictionary<string, object>
        {
            [LoggerExtensions.ControllerNamePlaceholder] = controller,
            [LoggerExtensions.ActionNamePlaceholder] = action,
            [LoggerExtensions.HttpMethodPlaceholder] = httpMethod,
            [LoggerExtensions.PathTemplatePlaceholder] = path,
        };

        using (logger.BeginScope(scope))
        {
            logger.LogActionExecution(controller, action, httpMethod, path);

            await next();
        }
    }
}
