using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class TurnoBLL
    {
        private TurnoDAL _dal = new TurnoDAL();

        public bool Alta(TurnoBE value)
        {
            return this._dal.Alta(value);
         }

        public TurnoBE Consulta(int id)
        {
            return this._dal.Consulta(id);
        }        
    }
}
