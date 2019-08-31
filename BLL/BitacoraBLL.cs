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

public interface IBitacoraBLL : ICRUD<BitacoraBE>
{
}


public class BitacoraBLL : IBitacoraBLL
{

    /// <summary>
    ///     ''' objeto que se conectara al origen de datos para actualizarlo y consultarlo
    ///     ''' </summary>
    private IBitacoraDAL _dao = null;

    public BitacoraBLL(IBitacoraDAL pDAO)
    {
        this._dao = pDAO;
    }

    public BitacoraBLL()
    {
        this._dao = new BitacoraDAL();
    }

    public bool Alta(ref BitacoraBE value)
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

    enum CRITICIDAD
    {
        ALTA = 1,
        MEDIA = 2,
        BAJA = 3
    }
    public enum TIPOLOG
    {
        ALTAUSUARIO = 1,
        BAJAUSUARIO = 2,
        MODIFICACIONUSUARIO = 3,
        ALTAFAMILIA = 4,
        BAJAFAMILIA = 5,
        MODIFICACIONFAMILIA = 6,
        LOGINOK = 10,
        LOGINFAIL = 11,
        CUENTABLOQUEADA = 12,
        CHECKINTEGRIDADOK = 20,
        CHECKINTEGRIDADFAIL = 21
    }


    public void Loguear(int TLog, string  username )
    {
        BitacoraBE mBitacora = new BitacoraBE();
        mBitacora.Fecha = DateTime.Now;        
        mBitacora.Username = username;        
        mBitacora.Eliminado = false;

        switch (TLog)
        {
            case (int)TIPOLOG.ALTAUSUARIO:                    
                    mBitacora.Descripcion = "Creacion de usuario";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.MEDIA));
                    break;
            case (int)TIPOLOG.MODIFICACIONUSUARIO:                    
                    mBitacora.Descripcion = "Modificacion de usuario";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.MEDIA));
                    break;
            case (int)TIPOLOG.BAJAUSUARIO:                    
                    mBitacora.Descripcion = "Baja de usuario";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.MEDIA));
                    break;
            case (int)TIPOLOG.ALTAFAMILIA:                    
                    mBitacora.Descripcion = "Creacion de familia";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.MEDIA));
                    break;
            case (int)TIPOLOG.MODIFICACIONFAMILIA:                
                    mBitacora.Descripcion = "Modificacion de familia";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.MEDIA));
                    break;
            case (int)TIPOLOG.BAJAFAMILIA:                
                    mBitacora.Descripcion = "Baja de familia";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.MEDIA));
                    break;
            case (int)TIPOLOG.CHECKINTEGRIDADFAIL:                
                    mBitacora.Descripcion = "ERROR. Chequeo de integridad de BD FALLIDO";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.ALTA));
                    break;
            case (int)TIPOLOG.CHECKINTEGRIDADOK:                    
                    mBitacora.Descripcion = "Chequeo de integridad OK";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.BAJA));
                    break;
            case (int)TIPOLOG.CUENTABLOQUEADA:                   
                    mBitacora.Descripcion = "Cuenta de usuario bloqueada por intentos";
                    mBitacora.Criticidad = System.Convert.ToChar(System.Convert.ToString(CRITICIDAD.MEDIA));
                    break;
            case (int)TIPOLOG.LOGINOK:                    
                    mBitacora.Descripcion = "Login con Exito";
                    mBitacora.Criticidad = Convert.ToInt32(CRITICIDAD.BAJA);
                    break;
            case (int)TIPOLOG.LOGINFAIL:                    
                    mBitacora.Descripcion = "Login Fallido";
                    mBitacora.Criticidad = Convert.ToInt32(CRITICIDAD.BAJA);
                    break;
            default:                   
                    mBitacora.Descripcion = "Evento no contemplado";
                    mBitacora.Criticidad = Convert.ToInt32(CRITICIDAD.ALTA);
                    break;
        }
        try
        {
            Alta(ref mBitacora);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede insertar.", ex);
        }
    }

    public bool Baja(ref BitacoraBE value)
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

    public BitacoraBE Consulta(ref BitacoraBE filtro)
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

    public List<BitacoraBE> ConsultaRango(ref BitacoraBE filtroDesde, ref BitacoraBE filtroHasta)
    {
        try
        {
            return this._dao.ConsultaRango(ref filtroDesde, ref filtroHasta);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede consultar por rango.", ex);
        }
    }

    public List<BitacoraBE> ConsultaRango(BitacoraBE filtroDesde, BitacoraBE filtroHasta)
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

    public List<BitacoraBE> ConsultaTodo()
    {
        try
        {
            return this._dao.ConsultaRango(null, null);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede consultar por rango.", ex);
        }
    }

    public bool Modificacion(ref BitacoraBE value)
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
