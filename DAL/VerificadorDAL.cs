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

public interface IVerificadorDAL : IMapeador<VerificadorBE>, IVerificador<VerificadorBE>
{
}

public class VerificadorDAL //: IVerificadorDAL
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
    private IConversor<BE.VerificadorBE> _conversor = null/* TODO Change to default(_) if this is not a reference type */;
    public bool Alta(ref VerificadorBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO DIGITOVERIFICADOR VALUES(@digitovertical,@tablanombre, 0) SET @identity=@@Identity", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@tablanombre", value.TablaNombre);
            // Calculo el nuevo digito horizontal
            this.Wrapper.AgregarParametro(comando, "@digitovertical", value.DigitoVertical);
            IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);

            resultado = this._wrapper.EjecutarConsulta(comando);

            // asignar el Id devuelto por la consulta al objeto
            if ((resultado > 0))
            {
                value.Id = System.Convert.ToInt32(paramRet.Value);

                // Calculo el nuevo digito horizontal
                value.DigitoHorizontal = CalcularDVH(ref value);
                Modificacion(ref value);

                // CASO PARTICULAR PARA TDIGITOVERIFICAR. Una vez que inserto el nuevo registro, tengo que modificar el campo DIGITOVERTICAL del registro propio en la tabla 
                ActualizarDVVPropio();
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

    public bool Baja(ref VerificadorBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("DELETE FROM DIGITOVERIFICADOR WHERE iddigitoverificador=@id", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);

            resultado = this._wrapper.EjecutarConsulta(comando);

            if ((resultado > 0))

                // CASO PARTICULAR PARA TDIGITOVERIFICAR. Una vez que borro un registro, tengo que modificar el campo DIGITOVERTICAL del registro propio en la tabla 
                ActualizarDVVPropio();
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

    public VerificadorBE Consulta(ref VerificadorBE filtro)
    {
        List<BE.VerificadorBE> lista = this.ConsultaRango(ref filtro, ref filtro);
        if (lista != null && lista.Count > 0)
            // retornar solo el primer objeto que cumpla con el filtro
            return lista[0];
        else
            return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public List<VerificadorBE> Consulta()
    {
        List<BE.VerificadorBE> lista = new List<BE.VerificadorBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM DIGITOVERIFICADOR", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@tablanombre", DBNull.Value);
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
    public List<VerificadorBE> ConsultaRango(ref VerificadorBE filtroDesde, ref VerificadorBE filtroHasta)
    {
        List<BE.VerificadorBE> lista = new List<BE.VerificadorBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM DIGITOVERIFICADOR WHERE (iddigitoverificador=@id OR @id IS NULL) AND (tablanombre=@tablanombre OR @tablanombre IS NULL)", CommandType.Text);
        try
        {
            if (filtroDesde != null && filtroDesde.Id > 0)
                this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
            else
                this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);

            if (filtroDesde != null && !string.IsNullOrWhiteSpace(filtroDesde.TablaNombre))
                this.Wrapper.AgregarParametro(comando, "@tablanombre", filtroDesde.TablaNombre);
            else
                this.Wrapper.AgregarParametro(comando, "@tablanombre", DBNull.Value);
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

    public List<VerificadorBE> ConsultaRango()
    {
        List<BE.VerificadorBE> lista = new List<BE.VerificadorBE>();

        IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM DIGITOVERIFICADOR", CommandType.Text);
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
        return lista;
    }

    public bool Modificacion(ref VerificadorBE value)
    {
        int resultado = 0;
        IDbCommand comando = this.Wrapper.CrearComando("UPDATE DIGITOVERIFICADOR SET dvv=@digitovertical, dvh=@digitohorizontal, TABLANOMBRE=@nombretabla WHERE iddigitoverificador=@id", CommandType.Text);
        try
        {
            this.Wrapper.AgregarParametro(comando, "@nombretabla", value.TablaNombre);
            this.Wrapper.AgregarParametro(comando, "@digitovertical", value.DigitoVertical);
            this.Wrapper.AgregarParametro(comando, "@id", value.Id);

            // Calculo el nuevo digito horizontal
            value.DigitoHorizontal = CalcularDVH(ref value);
            this.Wrapper.AgregarParametro(comando, "@digitohorizontal", value.DigitoHorizontal);

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
        return (resultado > 0);
    }

    public IConversor<VerificadorBE> Conversor
    {
        get
        {
            if (this._conversor == null)
                this._conversor = new VerificadorConversor();
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

    private void ActualizarDVH(ref VerificadorBE value)
    {
        value.DigitoHorizontal = CalcularDVH(ref value);
        Modificacion(ref value);
    }

    public void ActualizarDVHTabla()
    {
        List<VerificadorBE> listaDTO = Consulta();
        foreach (VerificadorBE objDTO in listaDTO)
        {
            VerificadorBE verificadorDTO = new VerificadorBE(objDTO);
            ActualizarDVH(ref verificadorDTO);
        }
    }

    private int CalcularDVH(ref VerificadorBE value)
    {
        int DVH = 0;        
        
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.Id), 0);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.DigitoVertical), 1);
        DVH += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(value.TablaNombre), 2);
        return DVH;
    }

    private bool VerificarDVH(VerificadorBE value)
    {
        if ((value.DigitoHorizontal != CalcularDVH(ref value)))
            return false;
        return true;
    }

    public bool VerificarDVHTabla()
    {
        
        List<VerificadorBE> listaDTO = ConsultaRango();
        foreach (VerificadorBE objDTO in listaDTO)
        {        
            if ((!VerificarDVH(objDTO)))
                throw new Exception("Verificacion Digito Horizontal en tabla DIGITOVERIFICADOR, id:" + System.Convert.ToString(objDTO.Id) + " Fallido");
        }
        return true;
    }

    private void ActualizarDVVPropio()
    {
        ActualizarDVV("DIGITOVERIFICADOR", "IDDIGITOVERIFICADOR");
    }
    public static void ActualizarDVV(string sNombreTabla, string nombreCampoId)
    {
        VerificadorBE propioObj = new VerificadorBE();
        propioObj.TablaNombre = sNombreTabla;
        VerificadorDAL objDAO = new VerificadorDAL();
        propioObj = objDAO.Consulta(ref propioObj);
        if ((propioObj == null))
        {
            propioObj = new VerificadorBE();
            propioObj.TablaNombre = sNombreTabla;
            propioObj.DigitoVertical = objDAO.CalcularDVV(sNombreTabla, nombreCampoId);
            objDAO.Alta(ref propioObj);
        }
        else
        {
            propioObj.DigitoVertical = objDAO.CalcularDVV(sNombreTabla, nombreCampoId);
            objDAO.Modificacion(ref propioObj);
        }
    }

    private int CalcularDVV(string nombreTabla, string nombreCampoId)
    {
        List<int> lista = new List<int>();
        IDbCommand comando = this.Wrapper.CrearComando("select " + nombreCampoId + " FROM " + nombreTabla, CommandType.Text);
        int DVV = 0;

        try
        {
            using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
            {
                int i = 0;
                while (reader.Read())
                {
                    int id = (Convert.ToInt32(reader[nombreCampoId]));
                   
                    DVV += DBUtils.CalcularDigitoVerificador(System.Convert.ToString(id), i);
                    i += 1;
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
        return DVV;
    }

    public bool VerificarDVV(string sNombreTabla, string nombreCampoId)
    {
        VerificadorBE propioObj = new VerificadorBE();
        propioObj.TablaNombre = sNombreTabla;
        propioObj = Consulta(ref propioObj);

        if ((propioObj == null))
            return false;
        else if (propioObj.DigitoVertical != CalcularDVV(sNombreTabla, nombreCampoId))
            throw new Exception("Verificacion Digito VERTICAL en tabla " + sNombreTabla + " Fallido");

        return true;
    }
}
