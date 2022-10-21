using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalSignalRServer.models
{
    public class Mensagem
    {
        public Guid Id { get; set; }
        public DateTime DataHoraDeEnvio { get; set; }
        public string Texto { get; set; } = "";
        public string Usuario { get; set; } = "";
        public Mensagem(string texto)
        {
            Id = Guid.NewGuid();
            DataHoraDeEnvio = DateTime.Now;
            Texto = texto;
        }
    }
}