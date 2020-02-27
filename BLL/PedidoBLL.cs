using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    public class PedidoBLL
    {
        private PedidoDAL _dal = new PedidoDAL();

        public bool Alta(PedidoBE value)
        {
            return _dal.Alta(value);
        }

        public void AltaDetalle(PedidoBE value)
        {
            _dal.AltaDetalle(value);
        }

        public PedidoBE Consulta(PedidoBE value)
        {
            return _dal.Consulta(value);
         }

        public void BajaDetalle(int idDetalle, int idPedido)
        {
            _dal.BajaDetalle(idDetalle, idPedido);
        }
        public PedidoBE Consulta(int id)
        {
            PedidoBE pedido = new PedidoBE();
            pedido.Id = id;
            return _dal.Consulta(pedido);
        }

        public PedidoBE getPedidoPorUsuario (int id, string estado)
        {
            return _dal.getPedidoPorUsuario(id,estado);
        }
        public bool Modificacion(PedidoBE value)
        {
            return _dal.Modificacion(value);
        }

        public void Modificacion(int id, int turno)
        {
            _dal.Modificacion(id, turno);
        }

        public decimal ObtenerTotal(int id)
        {
            return this._dal.ObtenerTotal(id);
        }
     }
}
