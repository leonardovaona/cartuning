using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class PagoBLL
    {
        private PagoDAL _dal = new PagoDAL();

        public bool Alta(PagoBE value)
        {
            return this._dal.Alta(value);
        }

        public PagoBE Consulta (PagoBE value)
        {
            return this._dal.Consulta(value);
        }

        public PagoBE BuscarPagoPorIdPedido(PagoBE value)
        {
            return this._dal.BuscarPagoPorIdPedido(value);

        }
    }
}
