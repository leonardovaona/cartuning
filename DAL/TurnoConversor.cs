using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace DAL
{
    public class TurnoConversor : IConversor<TurnoBE>
    {
        public TurnoBE Convertir(DataRow row)
        {
            TurnoBE turno = new TurnoBE();            
            UsuarioDAL usuarioDAL = new UsuarioDAL();

            turno.Id = Convert.ToInt32(row["id"]);
            turno.Fecha = Convert.ToDateTime(row["fecha"]);
            turno.Hora = Convert.ToInt16(row["hora"]);
            turno.Usuario = usuarioDAL.Consulta(Convert.ToInt32(row["id_usuario"]));

            return turno;
        }

        public TurnoBE Convertir(IDataReader reader)
        {
            TurnoBE turno = new TurnoBE();            
            UsuarioDAL usuarioDAL = new UsuarioDAL();

            turno.Id = Convert.ToInt32(reader["id"]);
            turno.Fecha = Convert.ToDateTime(reader["fecha"]);
            turno.Hora = Convert.ToInt16(reader["hora"]);
            turno.Usuario = usuarioDAL.Consulta(Convert.ToInt32(reader["id_usuario"]));

            return turno;

        }
    }
}
