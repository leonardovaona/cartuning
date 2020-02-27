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
    public class PagoConversor : IConversor<PagoBE>
    {
        public PagoBE Convertir(DataRow fila)
        {
            PagoBE pago = new PagoBE();
            PedidoDAL pedidoDAL = new PedidoDAL();
            UsuarioDAL usuarioDAL = new UsuarioDAL();

            pago.Id = Convert.ToInt32(fila["id"]);
            pago.Fecha = Convert.ToDateTime(fila["fecha"]);
            pago.Monto = Convert.ToDecimal(fila["monto"]);
            pago.TipoTarjeta = Convert.ToString(fila["tipotarjeta"]);
            pago.NumeroTarjeta = Convert.ToString(fila["numerotarjeta"]);
            pago.Vencimiento = Convert.ToString(fila["fechavencimiento"]);
            pago.Titular = Convert.ToString(fila["titular"]);
            pago.CodigoSeguridad = Convert.ToInt32(fila["codigoseguridad"]);
            pago.Pedido = pedidoDAL.Consulta(Convert.ToInt32(fila["id_pedido"]));
            pago.Usuario = usuarioDAL.Consulta(Convert.ToInt32(fila["ïd_usuario"]));

            return pago;
        }

        public PagoBE Convertir(IDataReader fila)
        {
            PagoBE pago = new PagoBE();
            PedidoDAL pedidoDAL = new PedidoDAL();
            UsuarioDAL usuarioDAL = new UsuarioDAL();

            pago.Id = Convert.ToInt32(fila["id"]);
            pago.Fecha = Convert.ToDateTime(fila["fecha"]);
            pago.Monto = Convert.ToDecimal(fila["monto"]);
            pago.TipoTarjeta = Convert.ToString(fila["tipotarjeta"]);
            pago.NumeroTarjeta = Convert.ToString(fila["numerotarjeta"]);
            pago.Vencimiento = Convert.ToString(fila["fechavencimiento"]);
            pago.Titular = Convert.ToString(fila["titular"]);
            pago.CodigoSeguridad = Convert.ToInt32(fila["codigoseguridad"]);
            pago.Pedido = pedidoDAL.Consulta(Convert.ToInt32(fila["id_pedido"]));
            pago.Usuario = usuarioDAL.Consulta(Convert.ToInt32(fila["ïd_usuario"]));

            return pago;
        }
    }
}
