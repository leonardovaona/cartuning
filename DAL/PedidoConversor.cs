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
    public class PedidoConversor : IConversor<PedidoBE>
    {
        public PedidoBE Convertir(DataRow row)
        {
            PedidoBE pedido = new PedidoBE();
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            TurnoDAL turnoDAL = new TurnoDAL();
            PedidoDAL pedidoDAL = new PedidoDAL();

            pedido.Id = Convert.ToInt32(row["id"]);
            pedido.Estado = Convert.ToString(row["estado"]);
            pedido.Fecha = Convert.ToDateTime(row["fecha"]);
            pedido.Usuario = usuarioDAL.Consulta( Convert.ToInt32(row["id_usuario"]));

            if (Convert.ToInt32(row["id_turno"]) == 0)
            {
                pedido.Turno = null;
            }
            else {
                pedido.Turno = turnoDAL.Consulta(Convert.ToInt32(row["id_turno"]));
            }
            pedido.Detalles = pedidoDAL.ConsultaDetalle(pedido.Id);
            
            return pedido;
        }

        public PedidoBE Convertir(IDataReader reader)
        {
            PedidoBE pedido = new PedidoBE();
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            TurnoDAL turnoDAL = new TurnoDAL();
            PedidoDAL pedidoDAL = new PedidoDAL();

            pedido.Id = Convert.ToInt32(reader["id"]);
            pedido.Estado = Convert.ToString(reader["estado"]);
            pedido.Fecha = Convert.ToDateTime(reader["fecha"]);
            pedido.Usuario = usuarioDAL.Consulta(Convert.ToInt32(reader["id_usuario"]));
            
            if(Convert.ToInt32(reader["id_turno"]) == 0)
            {
                pedido.Turno = null;
            }
            else
            {
                pedido.Turno = turnoDAL.Consulta(Convert.ToInt32(reader["id_turno"]));
            }
            pedido.Detalles = pedidoDAL.ConsultaDetalle(pedido.Id);

            return pedido;
        }
    }
}
