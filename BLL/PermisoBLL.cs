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
    public interface IPermisoBLL : ICRUD<PermisoBE>
    {

        /// <summary>
        ///     ''' Agrega nuevo un permiso hijo a otro permiso.
        ///     ''' </summary>
        bool AgregarHijo(BE.PermisoBE padre, BE.PermisoBE hijo);

        /// <summary>
        ///     ''' Elimina un permiso hijo de su permiso padre.
        ///     ''' </summary>
        bool QuitarHijo(BE.PermisoBE padre, BE.PermisoBE hijo);
    }

    /// <summary>

    /// ''' Gestiona los permisos del sistema.

    /// ''' </summary>

    public class PermisoBLL : IPermisoBLL
    {

        /// <summary>
        ///     ''' objeto que se conectara al origen de datos para actualizarlo y consultarlo
        ///     ''' </summary>
        private IPermisoDAL _dao = null/* TODO Change to default(_) if this is not a reference type */;
        private IFamiliaPermisoDAL _daoFamilia = null/* TODO Change to default(_) if this is not a reference type */;

        public PermisoBLL(IPermisoDAL pDAO, IFamiliaPermisoDAL pFamiliaDAO)
        {
            this._dao = pDAO;
            this._daoFamilia = pFamiliaDAO;
        }

        public PermisoBLL()
        {
            this._dao = new PermisoDAL();
            this._daoFamilia = new FamiliaPermisoDAL();
        }

        /// <summary>
        ///     ''' Agrega un nuevo permiso al sistema.
        ///     ''' </summary>
        public bool Alta(ref BE.PermisoBE value)
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
        public bool Baja(ref BE.PermisoBE value)
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

        /// <summary>
        ///     ''' Retorna el primer permiso que coincida con el filtro especificado.
        ///     ''' </summary>
        public BE.PermisoBE Consulta(ref BE.PermisoBE filtro)
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
        public System.Collections.Generic.List<BE.PermisoBE> ConsultaRango(ref BE.PermisoBE filtroDesde, ref BE.PermisoBE filtroHasta)
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

        public System.Collections.Generic.List<BE.PermisoBE> ConsultaRango( BE.PermisoBE filtroDesde, BE.PermisoBE filtroHasta)
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

        /// <summary>
        ///     ''' Modifica un permiso existente del sistema.
        ///     ''' </summary>
        public bool Modificacion(ref BE.PermisoBE value)
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

        /// <summary>
        ///     ''' Agrega nuevo un permiso hijo a otro permiso.
        ///     ''' </summary>
        public bool AgregarHijo(BE.PermisoBE padre, BE.PermisoBE hijo)
        {
            
            if (!ValidarPermisoHijo(padre, hijo, padre))
                throw new Exception("No se puede agregar el hijo, porque violaria la regla de recursividad.");

            try
            {
            
                this._daoFamilia.PermisoPadre = padre;
                return this._daoFamilia.Alta(ref hijo);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede agregar el hijo.", ex);
            }
        }

        /// <summary>
        ///     ''' Elimina un permiso hijo de su permiso padre.
        ///     ''' </summary>
        public bool QuitarHijo(BE.PermisoBE padre, BE.PermisoBE hijo)
        {
            try
            {
                // quitar
                this._daoFamilia.PermisoPadre = padre;
                return this._daoFamilia.Baja(ref hijo);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede quitar el hijo.", ex);
            }
        }

        /// <summary>
        ///     ''' Valida recursivamente que el nuevo permiso hijo no este dentro de los padres o dentro de los padres de los padres.
        ///     ''' </summary>
        private bool ValidarPermisoHijo(BE.PermisoBE padre, BE.PermisoBE nuevoHijo, BE.PermisoBE padreOriginal)
        {
         
            bool esValido = true;
         
            if (nuevoHijo != null)
            {
         
                if (!padre.Equals(nuevoHijo))
                {
         
                    PermisoBE filtroPadre = new PermisoSimpleDTO();
                    filtroPadre.Id = padre.Id;
                    Int32 i = 0;
                    this._daoFamilia.PermisoPadre = null;
                    List<PermisoBE> listaPadres = this._daoFamilia.ConsultaRango(null, filtroPadre);
                    PermisoBE padreDePadre;
                    while (i < listaPadres.Count & esValido)
                    {
                        padreDePadre = listaPadres[i];
                        if (!padreDePadre.Equals(padreOriginal))
         
                            esValido = this.ValidarPermisoHijo(padreDePadre, nuevoHijo, padreOriginal);
                        i += 1;
                    }
                }
                else
                    esValido = false;
            }
            else
                esValido = false;
            return esValido;
        }
        private List<PermisoBE> ObtenerPermisoPorUsuario(int id) {
            return null;
        }
    }
}