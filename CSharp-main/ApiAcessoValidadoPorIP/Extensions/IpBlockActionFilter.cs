using ApiAcessoValidadoPorIP.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace ApiAcessoValidadoPorIP.Extensions
{
    public class IpBlockActionFilter : ActionFilterAttribute
    {
        private readonly IIpBlockingService _blockingService;

        public IpBlockActionFilter(IIpBlockingService blockingService)
        {
            _blockingService = blockingService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;

            var isBlocked = _blockingService.IsBlocked(remoteIp!);

            if (isBlocked)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
