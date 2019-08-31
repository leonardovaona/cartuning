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


public interface IUsuarioDAL : IMapeador<UsuarioBE>, IVerificador<UsuarioBE>
{
    bool BloquearCuenta(ref UsuarioBE value);
    
}

public class UsuarioDAL : IUsuarioDAL
{
    private IComando _wrapper = null;
    private IConversor<UsuarioBE> _conversor = null;

    public bool Alta(ref UsuarioBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO USUARIO VALUES(@nombre, @apellido, @username, @clave,@email, @dni, @bloqueado, 0,0 )   SET @identity=@@Identity", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre);
            this.Wrapper.AgregarParametro(comando, "@apellido", value.Apellido);
            this.Wrapper.AgregarParametro(comando, "@username", value.Username);            
            this.Wrapper.AgregarParametro(comando, "@clave", value.Clave);
            this.Wrapper.AgregarParametro(comando, "@dni", value.DNI);
            this.Wrapper.AgregarParametro(comando, "@email", value.Email);
            this.Wrapper.AgregarParametro(comando, "@bloqueado", value.Bloqueado);
            

            IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);


            resultado = this._wrapper.EjecutarConsulta(comando);

            // asignar el Id devuelto por la consulta al objeto
            if ((resultado > 0))
            {
                value.Id = System.Convert.ToInt32(1);

                // Calculo el nuevo digito horizontal
                //value.DVH  = CalcularDVH(ref value);
                //Modificacion(ref value);
                //VerificadorDAL.ActualizarDVV("USUARIO", "idusuario");
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
        return (resultado > 0);
    }

    public bool Baja(ref BE.UsuarioBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("UPDATE USUARIO SET Usu_Eliminado=@eliminado WHERE idusuario =@id", CommandType.Text);
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

    public BE.UsuarioBE Consulta(ref BE.UsuarioBE filtro)
    {
        List<BE.UsuarioBE> lista = this.ConsultaRango(ref filtro, ref filtro);
        if (lista != null && lista.Count > 0)
            return lista[0];
        else
            return null;
    }

    public bool BloquearCuenta(ref BE.UsuarioBE value)
    {
        value.Bloqueado = 0;
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("UPDATE USUARIO SET Bloqueado=@bloqueado WHERE id=@id", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);
            this.Wrapper.AgregarParametro(comando, "@bloqueado", value.Bloqueado);
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

    public List<BE.UsuarioBE> ConsultaRango(ref BE.UsuarioBE filtroDesde, ref BE.UsuarioBE filtroHasta)
    {
        List<BE.UsuarioBE> lista = new List<BE.UsuarioBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM USUARIO WHERE (username=@nombre OR @nombre IS NULL) AND (id=@id OR @id IS NULL) ORDER BY Nombre", CommandType.Text);
        
        try
        {
            if (filtroDesde != null && filtroDesde.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
            else
                this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
            if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Username ))
                this.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Username);
            else
                this.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value);
            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                // recorrer el IDataReader obtenido de la base de datos y convertirlo a un objeto entidad
                while (reader.Read())
                    lista.Add(this.Conversor.Convertir(reader));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }
        return lista;
    }

    public List<BE.UsuarioBE> Consulta()
    {
        List<BE.UsuarioBE> lista = new List<BE.UsuarioBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM USUARIO ORDER BY Nombre", CommandType.Text);
        try
        {


            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                // recorrer el IDataReader obtenido de la base de datos y convertirlo a un objeto entidad
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

    public BE.UsuarioBE Consulta(int id)
    {
        List<BE.UsuarioBE> lista = new List<BE.UsuarioBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM USUARIO WHERE id = @idusuario", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@idusuario", id);

            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                
                while (reader.Read())
                    return this.Conversor.Convertir(reader);
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
        return null;
    }

    public bool Modificacion(ref BE.UsuarioBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("UPDATE USUARIO SET nombre=@nombre, dvh=@digitohorizontal, clave=@clave, email=@email, Bloqueado=@bloqueado WHERE id=@id", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre);
            this.Wrapper.AgregarParametro(comando, "@clave", value.Clave);
            this.Wrapper.AgregarParametro(comando, "@email", value.Email);
            this.Wrapper.AgregarParametro(comando, "@bloqueado", value.Bloqueado);
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);

            value.DVH  = CalcularDVH(ref value);
            this.Wrapper.AgregarParametro(comando, "@digitohorizontal", value.DVH);

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

    public IConversor<BE.UsuarioBE> Conversor
    {
        get
        {
            if (this._conversor == null)
                this._conversor = new UsuarioConversor();
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

    private void ActualizarDVH(UsuarioBE value)
    {
        value.DVH = CalcularDVH(ref value);
        Modificacion(ref value);
    }

    public void ActualizarDVHTabla()
    {
        List<UsuarioBE> listaDTO = Consulta();
        foreach (UsuarioBE objDTO in listaDTO)
            ActualizarDVH(objDTO);
    }
    

    private int CalcularDVH(ref UsuarioBE value)
    {
        int DVH = 0;       

        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString (value.Id), 0);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Nombre), 1);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Apellido ), 2);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Clave ), 3);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.DNI), 4);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Email), 5);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Bloqueado), 6);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Eliminado), 7);
        return DVH;
    }

    private bool VerificarDVH(UsuarioBE value)
    {
        if ((value.DVH != CalcularDVH(ref value)))
            return false;
        return true;
    }

    public bool VerificarDVHTabla()
    {
        List<UsuarioBE> listaDTO = Consulta();
        foreach (UsuarioBE objDTO in listaDTO)
        {
            if ((!VerificarDVH(objDTO)))
                throw new Exception("Verificacion Digito Horizontal en tabla USUARIO, id:" + System.Convert.ToString(objDTO.Id) + " Fallido");
        }
        return true;
    }

    public List<UsuarioBE> ConsultaRango(UsuarioBE filtroDesde, UsuarioBE filtroHasta)
    {
        List<BE.UsuarioBE> lista = new List<BE.UsuarioBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM USUARIO WHERE (username=@nombre OR @nombre IS NULL) AND (id=@id OR @id IS NULL) ORDER BY Nombre", CommandType.Text);

        try
        {
            if (filtroDesde != null && filtroDesde.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
            else
                this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
            if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Username))
                this.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Username);
            else
                this.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value);
            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                // recorrer el IDataReader obtenido de la base de datos y convertirlo a un objeto entidad
                while (reader.Read())
                    lista.Add(this.Conversor.Convertir(reader));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }
        return lista;
    }

    int IVerificador<UsuarioBE>.CalcularDVH(ref UsuarioBE value)
    {
        throw new NotImplementedException();
    }

    bool IVerificador<UsuarioBE>.VerificarDVH(UsuarioBE value)
    {
        throw new NotImplementedException();
    }

    void IVerificador<UsuarioBE>.ActualizarDVH(UsuarioBE value)
    {
        throw new NotImplementedException();
    }
}
