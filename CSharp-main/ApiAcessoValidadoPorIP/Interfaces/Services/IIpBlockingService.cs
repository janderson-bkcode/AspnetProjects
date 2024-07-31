using System.Net;

namespace ApiAcessoValidadoPorIP.Interfaces.Services
{
    public interface IIpBlockingService
    {
        bool IsBlocked(IPAddress ipAddress);
    }
}
