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

public interface IFamiliaPermisoDAL : IMapeador<PermisoBE>
{

    /// <summary>
    ///     ''' Permiso padre de todos los permisos que se van a agregar, borrar o consultar.
    ///     ''' </summary>
    ///     ''' <value></value>
    PermisoBE PermisoPadre { get; set; }
}

public class FamiliaPermisoDAL : IFamiliaPermisoDAL
{

    /// <summary>
    ///     ''' objeto que encapsula la funcionalidad de acceso, persistencia y lectura
    ///     ''' de datos en el origen de datos.
    ///     ''' </summary>
    ///     ''' <remarks></remarks>
    private IComando _wrapper = null/* TODO Change to default(_) if this is not a reference type */;
    /// <summary>
    ///     ''' conversor a entidades de los datos devueltos por la consulta SQL.
    ///     ''' </summary>
    ///     ''' <remarks></remarks>
    private IConversor<BE.PermisoBE> _conversor = null/* TODO Change to default(_) if this is not a reference type */;
    /// <summary>
    ///     ''' Permiso padre de todos los pemisos que se van a agregar, borrar o consultar
    ///     ''' </summary>
    ///     ''' <remarks></remarks>
    private PermisoBE _permisoPadre = null/* TODO Change to default(_) if this is not a reference type */;

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
                // obtener el wrapper por defecto
                this._wrapper = ComandoFactory.CrearComando("Default");
            return this._wrapper;
        }
        set
        {
            this._wrapper = value;
        }
    }

    /// <summary>
    ///     ''' Permiso padre de todos los permisos que se van a agregar, borrar o consultar.
    ///     ''' </summary>
    ///     ''' <value></value>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    public PermisoBE PermisoPadre
    {
        get
        {
            if (this._permisoPadre == null)
                throw new ArgumentNullException("No se especificó el permiso padre.");
            return this._permisoPadre;
        }
        set
        {
            this._permisoPadre = value;
        }
    }

    public bool Alta(ref BE.PermisoBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_FAMILIA_AGREGAR", CommandType.StoredProcedure);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@padreId", this.PermisoPadre.Id);
            this.Wrapper.AgregarParametro(comando, "@hijoId", value.Id);
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

    public bool Baja(ref BE.PermisoBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_FAMILIA_ELIMINAR", CommandType.StoredProcedure);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@padreId", this.PermisoPadre.Id);
            this.Wrapper.AgregarParametro(comando, "@hijoId", value.Id);

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
        //TO DO VER
        //PermisoDTO permi = new PermisoDTO();
        //List<PermisoDTO> lista = this.ConsultaRango(ref permi, ref permi);
        List<PermisoBE> lista = new List<PermisoBE>();
        if (lista.Count > 0)
            return lista[0];
        else
            return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public System.Collections.Generic.List<BE.PermisoBE> ConsultaRango(ref BE.PermisoBE filtroDesde, ref BE.PermisoBE filtroHasta)
    {
        List<BE.PermisoBE> lista = new List<BE.PermisoBE>();

        IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_FAMILIA_LISTAR", CommandType.StoredProcedure);
        try
        {
            if (filtroHasta != null && filtroHasta.Id > 0)
            {
                this.Wrapper.AgregarParametro(comando, "@padreid", DBNull.Value);
                this.Wrapper.AgregarParametro(comando, "@hijoId", filtroHasta.Id);
            }
            else
            {
                // siempre buscar por el Id del padre
                this.Wrapper.AgregarParametro(comando, "@padreid", this.PermisoPadre.Id);
                // solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
                if (filtroDesde != null && filtroDesde.Id > 0)
                    this.Wrapper.AgregarParametro(comando, "@hijoId", filtroDesde.Id);
                else
                    this.Wrapper.AgregarParametro(comando, "@hijoId", DBNull.Value);
            }

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

    public List<PermisoBE> ConsultaRango(PermisoBE filtroDesde, PermisoBE filtroHasta)
    {
        throw new NotImplementedException();
    }

    public bool Modificacion(ref BE.PermisoBE value)
    {
        throw new NotImplementedException("No se puede realizar una modificacion para el permiso.");
    }
}
