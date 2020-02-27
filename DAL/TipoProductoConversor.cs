using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    class TipoProductoConversor : IConversor<TipoProductoBE>
    {

            public TipoProductoBE Convertir(IDataReader reader)
            {
                TipoProductoBE tipoProducto = new TipoProductoBE();

                tipoProducto.Id = Convert.ToInt32(reader["id"]);
                tipoProducto.Descripcion = Convert.ToString(reader["descripcion"]);
                return tipoProducto;
            }

            public TipoProductoBE Convertir(DataRow row)
            {
                TipoProductoBE tipoProducto = new TipoProductoBE();

                tipoProducto.Id = Convert.ToInt32(row["id"]);
                tipoProducto.Descripcion = Convert.ToString(row["descripcion"]);
                return tipoProducto;
            }
     }
}
