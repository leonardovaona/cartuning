using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PagoBE
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public string Titular { get; set; }
        public string Vencimiento { get; set; }
        public int CodigoSeguridad { get; set; }
        public PedidoBE Pedido { get; set; }
        public UsuarioBE Usuario { get; set; }
    }
}
