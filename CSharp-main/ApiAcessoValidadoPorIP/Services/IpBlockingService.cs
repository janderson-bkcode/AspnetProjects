using ApiAcessoValidadoPorIP.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ApiAcessoValidadoPorIP.Services
{
    public class IpBlockingService : IIpBlockingService
    {
        private readonly List<string> _blockedIps;

        public IpBlockingService(IConfiguration configuration)
        {
            var blockedIps = configuration.GetValue<string>("BlockedIPs");
            _blockedIps = blockedIps.Split(',').ToList();
        }

        public bool IsBlocked(IPAddress ipAddress) => _blockedIps.Contains(ipAddress.ToString());
    }
}
