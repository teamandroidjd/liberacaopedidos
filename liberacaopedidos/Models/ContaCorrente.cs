using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liberacaopedidos.Models
{
    public class ContaCorrente
    {
        public String NumDocumento { get; set; }
        public String NumPedido { get; set; }
        public String VlDuplic { get; set; }
        public String DiasAtraso { get; set; }
        public String DiasVenc { get; set; }
        public String DatVenc { get; set; }
        public String SitAtual { get; set; }
        public String TotalReceber { get; set; }
        public String TotalRecebido { get; set; }
        public String TotalGeral { get; set; }
        public String MediaDias { get; set; }
        public String CodCliente { get; set; }
        public String NomeCliente{ get; set; }

    }
}