using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;

namespace DAL
{
    public class ParteProductoConversor : IConversor<ParteProductoBE>
    {
        private ProductoDAL _dal = new ProductoDAL();
        public ParteProductoBE Convertir(IDataReader reader)
        {
            ParteProductoBE parteProducto = new ParteProductoBE();

            parteProducto.Id = Convert.ToInt32(reader["ID"]);
            parteProducto.Descripcion = Convert.ToString(reader["descripcion"]);
            parteProducto.Color = Convert.ToString(reader["color"]);
            parteProducto.Material = _dal.getMaterial(Convert.ToInt32(reader["id_material"]));          

            return parteProducto;
        }

        public ParteProductoBE Convertir(DataRow row)
        {
            ParteProductoBE parteProducto = new ParteProductoBE();

            parteProducto.Id = Convert.ToInt32(row["id"]);
            parteProducto.Descripcion = Convert.ToString(row["descripcion"]);
            parteProducto.Color = Convert.ToString(row["color"]);
            parteProducto.Material = _dal.getMaterial(Convert.ToInt32(row["id_material"]));

            return parteProducto;
        }
    }
}
