using System;
using System.Collections.Generic;
using BE;
using DAL;

namespace BLL
{
    public class IdiomaBLL
    {
        private IdiomaDAL _dao = new IdiomaDAL();

        public IdiomaBLL()
        {
            this._dao = new IdiomaDAL();
        }

        /// <summary>
        ///     ''' Agrega un nuevo permiso al sistema.
        ///     ''' </summary>
        public bool Alta(IdiomaBE value)
        {
            try
            {
                return this._dao.Alta(value);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede agregar.", ex);
            }
        }

        /// <summary>
        ///     ''' Elimina un permiso existente del sistema.
        ///     ''' </summary>
     
        public IdiomaBE Consulta(IdiomaBE filtro)
        {
            try
            {
                return this._dao.Consulta(filtro);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede consultar.", ex);
            }
        }

        /// <summary>
        ///     ''' Retorna todos los permisos que coincidan con el fitrol especificado.
        ///     ''' </summary>
        public List<IdiomaBE> ConsultaRango(IdiomaBE filtroDesde,IdiomaBE filtroHasta)
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

        public bool Modificacion(IdiomaBE value)
        {
            try
            {
                return this._dao.Modificacion(value);
            }
            catch (Exception ex)
            {
                throw new Exception("No se puede modificar.", ex);
            }
        }
    }
}
