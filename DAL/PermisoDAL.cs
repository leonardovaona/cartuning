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




public class PermisoDAL
{
        
    private IComando _wrapper = null;
    
    private IConversor<PermisoBE> _conversor = null;

    public bool Alta(ref PermisoBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO permiso values (@nombre, @descripcion, 0,0,0 ) SET @identity =@@Identity", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre);
            this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);
                       
             IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);
            
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
        IDbCommand comando = this.Wrapper.CrearComando("update permiso set elimnado = @eliminado where id = @id", CommandType.Text);
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
    public BE.PermisoBE Consulta(int id)
    {
        PermisoBE permisoFiltro = new PermisoBE();
        permisoFiltro.Id = id;
        permisoFiltro.EsPadre = 0;
        List<PermisoBE> lista = this.ConsultaRango(permisoFiltro, permisoFiltro);
        if (lista != null && lista.Count > 0)
            //List<PermisoBE> permisoList = ConsultarHijos(lista[0].Id);
            return lista[0];
        else
            return null;
    }
    public BE.PermisoBE Consulta(ref BE.PermisoBE filtro)
    {
        List<PermisoBE> lista = this.ConsultaRango(filtro, null);
        if (lista != null && lista.Count > 0)
            //List<PermisoBE> permisoList = ConsultarHijos(lista[0].Id);
            return lista[0];
        else
            return null;
    }

    public List<PermisoBE> ConsultaRango(BE.PermisoBE filtroDesde, BE.PermisoBE filtroHasta)
    {
        List<BE.PermisoBE> lista = new List<BE.PermisoBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM permiso WHERE (nombre=@nombre OR @nombre IS NULL) AND (id=@id OR @id IS NULL) AND (espadre = @espadre OR @espadre IS NULL) ORDER BY Nombre ", CommandType.Text);
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
            if (filtroDesde != null && filtroDesde.EsPadre > 0)
                this.Wrapper.AgregarParametro(comando, "@espadre", filtroDesde.EsPadre);
            else
                this.Wrapper.AgregarParametro(comando, "@espadre", DBNull.Value);

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

    public List<PermisoBE> ConsultaRango(ref PermisoBE filtroDesde, ref PermisoBE filtroHasta)
    {
        List<BE.PermisoBE> lista = new List<PermisoBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM permiso WHERE(nombre = @nombre OR @nombre IS NULL) AND(id = @id OR @id IS NULL) AND espadre = 1 ORDER BY Nombre ", CommandType.Text);
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

    public List<PermisoBE> ConsultarHijos(PermisoBE filtro)
    {
        List<PermisoBE> permisosHijos = new List<PermisoBE>();
        IDbCommand comando = this.Wrapper.CrearComando("SELECT p.* FROM familia f, permiso p WHERE f.id_permiso = p.id AND id_familia = @id", CommandType.Text);
        try
        {
            if (filtro != null && filtro.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtro.Id);
            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                while (reader.Read())
                {   
                    permisosHijos.Add(this.Conversor.Convertir(reader));
                }
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
        return permisosHijos;
    }

    public bool AltaFamilia(int idFamilia, int idPermiso)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO familia values (@idfamilia, @idpermiso) ", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@idfamilia", idFamilia);
            this.Wrapper.AgregarParametro(comando, "@idpermiso", idPermiso);
                        
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

    public bool BajaFamilia(int idFamilia)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("DELETE familia WHERE id_familia = @idfamilia" , CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@idfamilia", idFamilia);            

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

    public bool AgregarPermisos(FamiliaBE familia)
    {
        foreach (PermisoBE permisoBE in familia.Permisos)
        {            
            if (!AltaFamilia(familia.Id, permisoBE.Id))
                return false;
         }
        return true;
    }

    public bool QuitarPermisos(FamiliaBE familia)
    {
        return BajaFamilia(familia.Id);
    }

    public bool Modificacion(ref BE.PermisoBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("UPDATE permiso SET nombre =@nombre, descripcion =@descripcion WHERE id =@id", CommandType.StoredProcedure);
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
