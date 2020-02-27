using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BE;

namespace DAL
{
    class MaterialConversor : IConversor<MaterialBE>
    {

        public MaterialBE Convertir(IDataReader reader)
        {
            MaterialBE material = new MaterialBE();

            material.Id = Convert.ToInt32(reader["id"]);
            material.Descripcion = Convert.ToString(reader["descripcion"]);
            return material;
        }

        public MaterialBE Convertir(DataRow row)
        {
            MaterialBE material = new MaterialBE();

            material.Id = Convert.ToInt32(row["id"]);
            material.Descripcion = Convert.ToString(row["descripcion"]);
            return material;
        }
    }
}
