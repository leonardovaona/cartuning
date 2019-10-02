using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DetalleIdiomaConversor : IConversor<DetalleIdiomaBE>
    {
        public DetalleIdiomaBE Convertir(DataRow row)
        {
            DetalleIdiomaBE detalleIdioma = new DetalleIdiomaBE();

            detalleIdioma.Id = Convert.ToInt32(row["id"]);
            detalleIdioma.Palabra = Convert.ToString(row["palabra"]);
            detalleIdioma.Control = Convert.ToString(row["control"]);
            
            return detalleIdioma;
        }

        public DetalleIdiomaBE Convertir(IDataReader reader)
        {
            DetalleIdiomaBE detalleIdioma = new DetalleIdiomaBE();

            detalleIdioma.Id = Convert.ToInt32(reader["id"]);
            detalleIdioma.Palabra = Convert.ToString(reader["palabra"]);
            detalleIdioma.Control = Convert.ToString(reader["control"]);

            return detalleIdioma;
        }
    }
}
