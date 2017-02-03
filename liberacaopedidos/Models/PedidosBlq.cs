using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liberacaopedidos.Models
{
    public class PedidosBlq
    {
        public String NumPedido { get; set; }
        public String DataEmissao { get; set; }
        public String ValorTotal { get; set; }
        public String NomRazao { get; set; }
        public int CodCLie { get; set; }
        public String NomeVend { get; set; }
        public String Observacao { get; set; }
    }
}