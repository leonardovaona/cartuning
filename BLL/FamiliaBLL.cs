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
using DAL;

namespace BLL
{
 
    public class FamiliaBLL
    {

        /// <summary>
        ///     ''' objeto que se conectara al origen de datos para actualizarlo y consultarlo
        ///     ''' </summary>
        private FamiliaDAL _dao = new FamiliaDAL();
        
        public FamiliaBLL()
        {
            this._dao = new FamiliaDAL();
         }

        /// <summary>
        ///     ''' Agrega un nuevo permiso al sistema.
        ///     ''' </summary>
        public bool Alta(ref FamiliaBE value)
        {
            try
            {
                return this._dao.Alta(ref value);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede agregar.", ex);
            }
        }

        /// <summary>
        ///     ''' Elimina un permiso existente del sistema.
        ///     ''' </summary>
        public bool Baja(ref FamiliaBE value)
        {
            try
            {
                return this._dao.Baja(ref value);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede eliminar.", ex);
            }
        }

        public List<FamiliaBE> Consulta(ref FamiliaBE filtro)
        {
            try
            {
                return this._dao.Consulta(ref filtro);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede consultar.", ex);
            }
        }

        /// <summary>
        ///     ''' Retorna todos los permisos que coincidan con el fitrol especificado.
        ///     ''' </summary>
        public System.Collections.Generic.List<FamiliaBE> ConsultaRango(ref FamiliaBE filtroDesde, ref FamiliaBE filtroHasta)
        {
            try
            {
                return this._dao.ConsultaRango(filtroDesde, filtroHasta);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede consultar por rango.", ex);
            }
        }

        public System.Collections.Generic.List<FamiliaBE> ConsultaRango( FamiliaBE filtroDesde, FamiliaBE filtroHasta)
        {
            try
            {
                return this._dao.ConsultaRango(filtroDesde, filtroHasta);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede consultar por rango.", ex);
            }
        }

        public List<PermisoBE> ConsultaPermisos(ref FamiliaBE filtro)
        {
            try
            {
                return this._dao.ConsultaPermisos(ref filtro);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede consultar.", ex);
            }
        }

        public bool Modificacion(ref FamiliaBE value)
        {
            try
            {
                return this._dao.Modificacion(ref value);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar.", ex);
            }
        }

    }
}