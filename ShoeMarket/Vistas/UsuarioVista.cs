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

    public class UsuarioVista
    {
        private UsuarioBE _usuario;
        public UsuarioBE Usuario
        {
            get
            {
                if (_usuario == null)
                    _usuario = new UsuarioBE();
                return _usuario;
            }
            set
            {
                _usuario = value;
            }
        }

        private IUsuarioBLL _usuarioBLL;
        public IUsuarioBLL usuarioBLL
        {
            get
            {
                if (_usuarioBLL == null)
                    _usuarioBLL = new UsuarioBLL();
                return _usuarioBLL;
            }
            set
            {
                _usuarioBLL = value;
            }
        }

        public UsuarioBE ObtenerPorId(int id)
        {
            UsuarioBE filtro = new UsuarioBE();
            filtro.Id = id;
            this.Usuario = this.usuarioBLL.Consulta(ref filtro);
            return this.Usuario;
        }

        public UsuarioBE ObtenerPorId(string id)
        {
            int n;
            if (int.TryParse(id, out n))
                return this.ObtenerPorId(Convert.ToInt32(n));
            else
                this.Usuario = null;
            return this.Usuario;
        }

        public bool EliminarPorId(string id)
        {
            UsuarioBE filtro = new UsuarioBE();
            filtro.Id = Convert.ToInt32(id);
            UsuarioBE _usuario = this.usuarioBLL.Consulta(ref filtro);
            if (_usuario != null)
            {
                _usuario.Eliminado = 1;
                return this.usuarioBLL.Baja(ref _usuario);
            }
            else
                return false;
        }

        public bool RestaurarPorId(string id)
        {
            UsuarioBE filtro = new UsuarioBE();
            filtro.Id = Convert.ToInt32(id);
            UsuarioBE _usuario = this.usuarioBLL.Consulta(ref filtro);
            if (_usuario != null)
            {
                _usuario.Eliminado = 0;
                return this.usuarioBLL.Baja(ref _usuario);
            }
            else
                return false;
        }

        public void LlenarGrilla(ref System.Web.UI.WebControls.GridView dataGrid)
        {

            UsuarioBE prueba = new UsuarioBE();
            dataGrid.DataSource = this.usuarioBLL.ConsultaRango(ref prueba, ref prueba);
            dataGrid.DataBind();
        }

        public void LlenarLista(ref System.Web.UI.WebControls.ListControl listBox)
        {
            UsuarioBE prueba = new UsuarioBE();
            List<UsuarioBE> lista = this.usuarioBLL.ConsultaRango(ref prueba, ref prueba);
            this.LlenarLista(ref listBox, lista);
        }

        public void LlenarLista(ref System.Web.UI.WebControls.ListControl listBox, List<UsuarioBE> lista)
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

