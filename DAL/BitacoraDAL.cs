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
using DAL;

public interface IBitacoraDAL : IMapeador<BitacoraBE>, IVerificador<BitacoraBE>
{
}

public class BitacoraDAL : IBitacoraDAL
{

    private IComando _wrapper = null;

    private IConversor<BE.BitacoraBE> _conversor = null;
    
    public bool Alta(ref BitacoraBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO BITACORA (fecha, descripcion,dvh) VALUES(@fecha, @descripcion,0)  SET @identity=@@Identity ", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@fecha", System.Convert.ToDateTime (value.Fecha));
            string descripcion = value.Username  + "|" + value.Descripcion + "|" + value.Criticidad;
           
            this.Wrapper.AgregarParametro(comando, "@descripcion", descripcion);

            IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);

            resultado = this._wrapper.EjecutarConsulta(comando);

            // asignar el Id devuelto por la consulta al objeto
            if ((resultado > 0))
            {
                value.Id = System.Convert.ToInt32(paramRet.Value);

                // Calculo el nuevo digito horizontal
                //value.DVH  = CalcularDVH(ref value);
                //Modificacion(ref value);
                //VerificadorDAL.ActualizarDVV("BITACORA", "id");
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
        // este metodo retornará true si hubo registros afectados en el origen de datos
        return (resultado > 0);
    }

    public bool Baja(ref BitacoraBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("UPDATE BITACORA SET eliminado=@eliminado WHERE id=@id", CommandType.Text);
        try
        {
            // agregar los parametros necesarios para poder ejecutar la consulta
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);
            this.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado);

            // ejecutar el comando/consulta SQL en el origen de datos
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
        // este metodo retornará true si hubo registros afectados en el origen de datos
        return (resultado > 0);
    }

    public BitacoraBE Consulta(ref BitacoraBE filtro)
    {
        List<BE.BitacoraBE> lista = this.ConsultaRango(filtro, null/* TODO Change to default(_) if this is not a reference type */);
        if (lista != null && lista.Count > 0)
            // retornar solo el primer objeto que cumpla con el filtro
            return lista[0];
        else
            return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public List<BitacoraBE> ConsultaRango(BitacoraBE filtroDesde, BitacoraBE filtroHasta)
    {
        List<BE.BitacoraBE> lista = new List<BE.BitacoraBE>();

        // crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM BITACORA WHERE (id=@id OR @id IS NULL)", CommandType.Text);
        try
        {
            // agregar los parametros necesarios para poder ejecutar la consulta
            // solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
            if (filtroDesde != null && filtroDesde.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
            else
                this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                while (reader.Read())
                    lista.Add(this.Conversor.Convertir(reader));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            this.Wrapper.CerrarConexion(comando);
        }
        // este metodo retornará la lista con todas las BE convertidas que
        // se obtuvieron del origen de datos
        return lista;
    }

    public List<BitacoraBE> ConsultaRango(ref BitacoraBE filtroDesde, ref BitacoraBE filtroHasta)
    {
        List<BE.BitacoraBE> lista = new List<BE.BitacoraBE>();

        // crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM BITACORA WHERE (id=@id OR @id IS NULL)", CommandType.Text);
        try
        {
            // agregar los parametros necesarios para poder ejecutar la consulta
            // solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
            if (filtroDesde != null && filtroDesde.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
            else
                this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
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
        // este metodo retornará la lista con todas las BE convertidas que
        // se obtuvieron del origen de datos
        return lista;
    }


    public List<BitacoraBE> ConsultaRango()
    {
        List<BE.BitacoraBE> lista = new List<BE.BitacoraBE>();

        
        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM BITACORA ", CommandType.Text);
        try
        {
           
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
        // este metodo retornará la lista con todas las BE convertidas que
        // se obtuvieron del origen de datos
        return lista;
    }

    public bool Modificacion(ref BitacoraBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("UPDATE BITACORA SET fecha=@fecha, descripcion=@descripcion, dvh=@dvh, WHERE id=@id", CommandType.Text);
        try
        {

            this.Wrapper.AgregarParametro(comando, "@fecha", System.Convert.ToDateTime(value.Fecha));
            string descripcion = value.Username + "|" + value.Descripcion + "|" + value.Criticidad;
            
            this.Wrapper.AgregarParametro(comando, "@descripcion", descripcion);
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);

            value.DVH = CalcularDVH(ref value);
            this.Wrapper.AgregarParametro(comando, "@dvh", value.DVH );

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
        // este metodo retornará true si hubo registros afectados en el origen de datos
        return (resultado > 0);
    }

    public IConversor<BitacoraBE> Conversor
    {
        get
        {
            if (this._conversor == null)
                this._conversor = new BitacoraConversor();
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


    private int CalcularDVH(ref BitacoraBE value)
    {
        int DVH = 0;

        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Id), 0);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Fecha), 1);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Descripcion), 2);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Username), 3);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Criticidad), 4);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Eliminado), 5);
        return DVH;
    }

    private void ActualizarDVH(BitacoraBE value)
    {
        value.DVH = CalcularDVH(ref value);
        Modificacion(ref value);
    }

    public void ActualizarDVHTabla()
    {       
        
        List<BitacoraBE> listaDTO = ConsultaRango();
        foreach (BitacoraBE objDTO in listaDTO)
            ActualizarDVH(objDTO);
    }

    private bool VerificarDVH(BitacoraBE value)
    {
        if ((value.DVH != CalcularDVH(ref value)))
            return false;
        return true;
    }

    public bool VerificarDVHTabla()
    {
        List<BitacoraBE> listaDTO = ConsultaRango(null/* TODO Change to default(_) if this is not a reference type */, null/* TODO Change to default(_) if this is not a reference type */);
        foreach (BitacoraBE objDTO in listaDTO)
        {
            if ((!VerificarDVH(objDTO)))
                throw new Exception("Verificacion Digito Horizontal en tabla BITACORA, id:" + System.Convert.ToString(objDTO.Id) + " Fallido");
        }
        return true;
    }

    int IVerificador<BitacoraBE>.CalcularDVH(ref BitacoraBE value)
    {
        throw new NotImplementedException();
    }

    bool IVerificador<BitacoraBE>.VerificarDVH(BitacoraBE value)
    {
        throw new NotImplementedException();
    }

    void IVerificador<BitacoraBE>.ActualizarDVH(BitacoraBE value)
    {
        throw new NotImplementedException();
    }
}
