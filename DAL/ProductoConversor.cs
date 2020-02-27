using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;

namespace DAL
{
    public class ProductoConversor : IConversor<ProductoBE>
    {
        private ProductoDAL _dal = new ProductoDAL();
        public ProductoBE Convertir(DataRow row)
        {
            
            ProductoBE producto = new ProductoBE();

            producto.Id = Convert.ToInt32(row["id"]);
            producto.Descripcion = Convert.ToString(row["descripcion"]);
            producto.Partes = _dal.getPartes(producto.Id);
            producto.Precio = Convert.ToDouble(row["precio"]);
            producto.Tipo = _dal.getTipoProducto(Convert.ToInt32(row["id_tipo"]));

            return producto;
        }

        public ProductoBE Convertir(IDataReader reader)
        {

            ProductoBE producto = new ProductoBE();

            producto.Id = Convert.ToInt32(reader["id"]);
            producto.Descripcion = Convert.ToString(reader["descripcion"]);
            producto.Partes = _dal.getPartes(producto.Id);
            producto.Precio = Convert.ToDouble(reader["precio"]);
            producto.Tipo = _dal.getTipoProducto(Convert.ToInt32(reader["id_tipo"]));
            producto.Imagenes = _dal.getImagenes(producto.Id);
            return producto;
        }
    }
}
