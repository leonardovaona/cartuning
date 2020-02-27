using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class DetallePedidoBE
    {
        public int Id { get; set; }
        public ProductoBE Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }        
    }
}
