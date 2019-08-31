using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using BE;
using BLL;

namespace ShoeMarket.Vistas
{
    public class BitacoraVista
    {
        public BE.UsuarioBE _usuario;
        private BitacoraBE _bitacoraDTO;

        public BitacoraBE Bitacora
        {
            get
            {
                if (_usuario == null)
                    _bitacoraDTO = new BitacoraBE();
                return _bitacoraDTO;
            }
            set
            {
                _bitacoraDTO = value;
            }
        }

        private IBitacoraBLL _BitacoraBLL;
        public IBitacoraBLL BitacoraBLL
        {
            get
            {
                if (_BitacoraBLL == null)
                    _BitacoraBLL = new BitacoraBLL();
                return _BitacoraBLL;
            }
            set
            {
                _BitacoraBLL = value;
            }
        }

        private List<BitacoraBE> listaBitacoraDTo = new List<BitacoraBE>();

        public void LlenarGrilla(ref System.Web.UI.WebControls.GridView dataGrid)
        {
            // traer todos los logs de bitacoras para enlazarlo al DataSource del control
            BitacoraBE prueba = new BitacoraBE();            

            List<BitacoraBE> Lista = this.BitacoraBLL.ConsultaRango(null, null);
            var listaBitacoraDTo = new List<BitacoraBE>();
            foreach (BitacoraBE obj in Lista)
            {
                if (!obj.Eliminado)
                    listaBitacoraDTo.Add(obj);
            }
            listaBitacoraDTo.Sort((x, y) => y.Fecha.CompareTo(x.Fecha));
            dataGrid.DataSource = listaBitacoraDTo;
            dataGrid.DataBind();
        }
    }
}