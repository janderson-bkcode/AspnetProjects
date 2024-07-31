using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mapaAstral.Models
{
    public class Signo
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string? Elemento { get; set; }
        public string? Regente { get; set; }
    }
}