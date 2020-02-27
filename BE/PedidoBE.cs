using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PedidoBE
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public UsuarioBE Usuario { get; set; }
        public string Estado { get; set; }
        public TurnoBE Turno { get; set; }
        public List<DetallePedidoBE> Detalles { get; set; } 

        public PedidoBE()
        {
            this.Detalles = new List<DetallePedidoBE>();
        }
    }
}
