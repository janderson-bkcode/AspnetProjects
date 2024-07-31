using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elastic.Configuration
{
   public class ElasticConfiguration
{
    public string Uri { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string DefaultIndex { get; set; }
}

}