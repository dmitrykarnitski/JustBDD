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

        using (logger.ExecutingControllerScope(
                   controller,
                   action,
                   context.HttpContext.Request.Method,
                   context.ActionDescriptor.AttributeRouteInfo?.Template))
        {
            await next();
        }
    }
}
