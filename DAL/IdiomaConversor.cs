using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IdiomaConversor : IConversor<IdiomaBE>
    {
        public IdiomaBE Convertir(DataRow row)
        {
            IdiomaBE idioma = new IdiomaBE();
            IdiomaDAL idiomaDAL = new IdiomaDAL();

            idioma.Id = Convert.ToInt32(row["id"]);
            idioma.Codigo = Convert.ToString(row["codigo"]);
            idioma.Descripcion = Convert.ToString(row["descripcion"]);
            idioma.Detalle = idiomaDAL.BuscarDetalleByIdIdioma(idioma.Id);

            return idioma;
        }

        public IdiomaBE Convertir(IDataReader reader)
        {
            IdiomaBE idioma = new IdiomaBE();
            IdiomaDAL idiomaDAL = new IdiomaDAL();

            idioma.Id = Convert.ToInt32(reader["id"]);
            idioma.Codigo = Convert.ToString(reader["codigo"]);
            idioma.Descripcion = Convert.ToString(reader["descripcion"]);
            idioma.Detalle = idiomaDAL.BuscarDetalleByIdIdioma(idioma.Id);

            return idioma;
        }
    }
}
