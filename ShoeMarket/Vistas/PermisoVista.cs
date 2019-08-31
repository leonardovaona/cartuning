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
   
    public class PermisoVista
    {
        private PermisoBE _permiso;
        public PermisoBE Permiso
        {
            get
            {
                if (_permiso == null)
                    _permiso = new PermisoSimpleDTO();
                return _permiso;
            }
            set
            {
                _permiso = value;
            }
        }

        private IPermisoBLL _PermisoBLL;
        public IPermisoBLL PermisoBLL
        {
            get
            {
                if (_PermisoBLL == null)
                    _PermisoBLL = new PermisoBLL();
                return _PermisoBLL;
            }
            set
            {
                _PermisoBLL = value;
            }
        }

        public PermisoBE ObtenerPorId(int id)
        {
            PermisoBE filtro = new PermisoSimpleDTO();
            filtro.Id = id;
            this.Permiso = this.PermisoBLL.Consulta(ref filtro);
            return this.Permiso;
        }

        public PermisoBE ObtenerPorId(string id)
        {
            int n;
            if (int.TryParse(id, out n))
                return this.ObtenerPorId(Convert.ToInt32(n));
            else
                this.Permiso = null/* TODO Change to default(_) if this is not a reference type */;
            return this.Permiso;
        }

        public bool EliminarPorId(string id)
        {
            PermisoBE filtro = new PermisoSimpleDTO();
            filtro.Id = Convert.ToInt32(id);
            PermisoBE _permiso = this.PermisoBLL.Consulta(ref filtro);
            if (_permiso != null)
            {
                _permiso.Eliminado = true;
                return this.PermisoBLL.Baja(ref _permiso);
            }
            else
                return false;
        }

        public bool RestaurarPorId(string id)
        {
            PermisoBE filtro = new PermisoSimpleDTO();
            filtro.Id = Convert.ToInt32(id);
            PermisoBE _permiso = this.PermisoBLL.Consulta(ref filtro);
            if (_permiso != null)
            {
                _permiso.Eliminado = false;
                return this.PermisoBLL.Baja(ref _permiso);
            }
            else
                return false;
        }

        public void LlenarGrilla(ref System.Web.UI.WebControls.GridView dataGrid)
        {
            PermisoBE prueba = new PermisoSimpleDTO();
            dataGrid.DataSource = this.PermisoBLL.ConsultaRango(ref prueba, ref prueba);
            dataGrid.DataBind();
        }

        public void LlenarLista(ref System.Web.UI.WebControls.ListControl listBox)
        {
            PermisoBE prueba = new PermisoSimpleDTO();
            List<PermisoBE> lista = this.PermisoBLL.ConsultaRango(ref prueba, ref prueba);
            this.LlenarLista(ref listBox, lista);
        }

        public void LlenarLista(ref System.Web.UI.WebControls.ListControl listBox, List<PermisoBE> lista)
        {
            listBox.Items.Clear();
            listBox.DataTextField = "Nombre";
            listBox.DataValueField = "Id";
            listBox.DataSource = lista;
            listBox.DataBind();
        }

        public int ObtenerIdEnLista(System.Web.UI.WebControls.ListControl listBox)
        {
            if (listBox != null && listBox.SelectedItem != null)
                return Convert.ToInt32(listBox.SelectedItem.Value);
            else
                return 0;
        }
    }

}