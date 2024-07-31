using ApiAcessoValidadoPorIP.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ApiAcessoValidadoPorIP.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [ServiceFilter(typeof(IpBlockActionFilter))]
        [HttpGet("Unblocked")]
        public string Unblocked()
        {
            return "Unblocked access";
        }
        [ServiceFilter(typeof(IpBlockActionFilter))]
        [HttpGet("blocked")]
        public string blocked()
        {
            return "blocked access";
        }

    }
}
