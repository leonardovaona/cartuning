using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    class FamiliaConversor : IConversor <FamiliaBE>
    {
        private FamiliaDAL _familiaDAL = new FamiliaDAL();

        public FamiliaBE Convertir(System.Data.DataRow row)
        {
            Int32 cantHijos = 0;

            FamiliaBE familia;
            if (cantHijos > 0)
                familia = new FamiliaBE();
            else
                familia = new FamiliaBE();
            familia.Id = Convert.ToInt32(row["id"]);
            

            // leitho ver esta parte
            /*if (cantHijos > 0)
            {
                this._familiaDAL.PermisoPadre = familia;
                //TO DO VER QUE HACE permiso = this._famPemisosDAL.ConsultaRango(null, null);
            }*/

            return familia;
        }

        public FamiliaBE Convertir(System.Data.IDataReader reader)
        {
            Int32 cantHijos = 0;

            FamiliaBE familia;
            if (cantHijos > 0)
                familia = new FamiliaBE();
            else
                familia = new FamiliaBE();
            familia.Id = Convert.ToInt32(reader["id"]);
            familia.Nombre = Convert.ToString(reader["Nombre"]);

            // leitho ver que pasa con esto
        /*if (cantHijos > 0)
            {
                this._familiaDA = permiso;
                permiso.Permisos = this._famPemisosDAL.ConsultaRango(null, null);
            }*/

            return familia;
        }
    }
}
