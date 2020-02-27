using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DetallePedidoConversor : IConversor<DetallePedidoBE>
    {
        public DetallePedidoBE Convertir(DataRow row)
        {
            DetallePedidoBE detalle = new DetallePedidoBE();
            ProductoDAL productoDAL = new ProductoDAL();

            detalle.Id = Convert.ToInt32(row["id"]);
            detalle.Cantidad = Convert.ToInt32(row["cantidad"]);
            detalle.Subtotal = Convert.ToDecimal (row["subtotal"]);
            ProductoBE producto = new ProductoBE();
            producto.Id = Convert.ToInt32(row["id_producto"]);
            detalle.Producto = productoDAL.Consulta(producto);

            return detalle;
        }

        public DetallePedidoBE Convertir(IDataReader reader)
        {
            DetallePedidoBE detalle = new DetallePedidoBE();
            ProductoDAL productoDAL = new ProductoDAL();

            detalle.Id = Convert.ToInt32(reader["id"]);
            detalle.Cantidad = Convert.ToInt32(reader["cantidad"]);
            detalle.Subtotal = Convert.ToDecimal(reader["subtotal"]);
            ProductoBE producto = new ProductoBE();
            producto.Id = Convert.ToInt32(reader["id_producto"]);
            detalle.Producto = productoDAL.Consulta(producto);

            return detalle;
        }
    }    
}
