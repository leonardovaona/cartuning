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
using System.Data;
using BE;


public interface IPermisoDAL : IMapeador<PermisoBE>
{
}


public class PermisoDAL : IPermisoDAL
{

    /// <summary>
    ///     ''' objeto que encapsula la funcionalidad de acceso, persistencia y lectura
    ///     ''' de datos en el origen de datos.
    ///     ''' </summary>
    private IComando _wrapper = null/* TODO Change to default(_) if this is not a reference type */;
    /// <summary>
    ///     ''' conversor a BE de los datos devueltos por la consulta SQL.
    ///     ''' </summary>
    private IConversor<BE.PermisoBE> _conversor = null/* TODO Change to default(_) if this is not a reference type */;

    public bool Alta(ref BE.PermisoBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_AGREGAR", CommandType.StoredProcedure);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre);
            this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);
            IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);

            resultado = this._wrapper.EjecutarConsulta(comando);

            if ((resultado > 0))
                value.Id = System.Convert.ToInt32(paramRet.Value);
        }
        catch
        {
            throw;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }
        return (resultado > 0);
    }

    public bool Baja(ref BE.PermisoBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_ELIMINAR", CommandType.StoredProcedure);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);
            this.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado);

            resultado = this._wrapper.EjecutarConsulta(comando);
        }
        catch
        {
            throw;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }
        return (resultado > 0);
    }

    public BE.PermisoBE Consulta(ref BE.PermisoBE filtro)
    {
        List<BE.PermisoBE> lista = this.ConsultaRango(filtro, null/* TODO Change to default(_) if this is not a reference type */);
        if (lista != null && lista.Count > 0)
            return lista[0];
        else
            return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public System.Collections.Generic.List<BE.PermisoBE> ConsultaRango(BE.PermisoBE filtroDesde, BE.PermisoBE filtroHasta)
    {
        List<BE.PermisoBE> lista = new List<BE.PermisoBE>();

        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_LISTAR", CommandType.StoredProcedure);
        try
        {
            if (filtroDesde != null && filtroDesde.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
            else
                this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
            if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Nombre))
                this.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Nombre);
            else
                this.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value);
            if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Descripcion))
                this.Wrapper.AgregarParametro(comando, "@descripcion", filtroDesde.Descripcion);
            else
                this.Wrapper.AgregarParametro(comando, "@descripcion", DBNull.Value);

            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                while (reader.Read())
                    lista.Add(this.Conversor.Convertir(reader));
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }

        return lista;
    }

    public System.Collections.Generic.List<BE.PermisoBE> ConsultaRango(ref BE.PermisoBE filtroDesde, ref BE.PermisoBE filtroHasta)
    {
        List<BE.PermisoBE> lista = new List<BE.PermisoBE>();

        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_LISTAR", CommandType.StoredProcedure);
        try
        {
            if (filtroDesde != null && filtroDesde.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
            else
                this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
            if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Nombre))
                this.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Nombre);
            else
                this.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value);
            if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Descripcion))
                this.Wrapper.AgregarParametro(comando, "@descripcion", filtroDesde.Descripcion);
            else
                this.Wrapper.AgregarParametro(comando, "@descripcion", DBNull.Value);

            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                while (reader.Read())
                    lista.Add(this.Conversor.Convertir(reader));
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }

        return lista;
    }

    public bool Modificacion(ref BE.PermisoBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_MODIFICAR", CommandType.StoredProcedure);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre);
            this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);
            this.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado);
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);

            resultado = this._wrapper.EjecutarConsulta(comando);
        }
        catch
        {
            throw;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }
        return (resultado > 0);
    }

    public IConversor<BE.PermisoBE> Conversor
    {
        get
        {
            if (this._conversor == null)
                this._conversor = new PermisoConversor();
            return this._conversor;
        }
        set
        {
            this._conversor = value;
        }
    }

    public IComando Wrapper
    {
        get
        {
            if (this._wrapper == null)
                this._wrapper = ComandoFactory.CrearComando("Default");
            return this._wrapper;
        }
        set
        {
            this._wrapper = value;
        }
    }
}
