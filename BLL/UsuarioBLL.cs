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
using BE;
using Microsoft.VisualBasic;

public interface IUsuarioBLL : ICRUD<UsuarioBE>
{

    /// <summary>
    ///     ''' Agrega un permiso al perfil del usuario.
    ///     ''' </summary>
    bool AgregarPermiso(BE.UsuarioBE usuario, BE.PermisoBE permiso);
    /// <summary>
    ///     ''' Quita un permiso del perfil del usuario.
    ///     ''' </summary>
    bool QuitarPermiso(BE.UsuarioBE usuario, BE.PermisoBE permiso);
}

/// <summary>

/// ''' Gestiona los usuarios del sistema.

/// ''' </summary>
public class UsuarioBLL : IUsuarioBLL
{

    /// <summary>
    ///     ''' objeto que se conectara al origen de datos para actualizarlo y consultarlo
    ///     ''' </summary>
    private IUsuarioDAL _dal = null;
    private IUsuarioPermisoDAL _dalPerfil = null;

    public UsuarioBLL(IUsuarioDAL DAO, IUsuarioPermisoDAL perfilDAL)
    {
        this._dal = DAO;
        this._dalPerfil = perfilDAL;
    }

    public UsuarioBLL()
    {
        this._dal = new UsuarioDAL();
        this._dalPerfil = new UsuarioPermisoDAL();
    }

    /// <summary>
    ///     ''' Agrega un nuevo usuario al sistema.
    ///     ''' </summary>
    public bool Alta(ref BE.UsuarioBE value)
    {
        try
        {
            return _dal.Alta(ref value);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede agregar.", ex);
        }
    }

    /// <summary>
    ///     ''' Elimina un usuario existente del sistema.
    ///     ''' </summary>
    public bool Baja(ref BE.UsuarioBE value)
    {
        try
        {
            return this._dal.Baja(ref value);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede eliminar.", ex);
        }
    }

    /// <summary>
    ///     ''' Retorna el primer usuario que coincida con el filtro especificado.
    ///     ''' </summary>
    public BE.UsuarioBE Consulta(ref BE.UsuarioBE filtro)
    {
        try
        {
            return this._dal.Consulta(ref filtro);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede consultar.", ex);
        }
    }

    public bool BloquearUsuario(ref BE.UsuarioBE filtro)
    {
        try
        {
            return this._dal.BloquearCuenta(ref filtro);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede consultar.", ex);
        }
    }

    /// <summary>
    ///     ''' Retorna todos los usuarios que coincidan con el filtro especificado.
    ///     ''' </summary>
    public System.Collections.Generic.List<BE.UsuarioBE> ConsultaRango(ref BE.UsuarioBE filtroDesde, ref BE.UsuarioBE filtroHasta)
    {
        try
        {
            return this._dal.ConsultaRango(ref filtroDesde, ref filtroHasta);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede consultar por rango.", ex);
        }
    }

    public System.Collections.Generic.List<BE.UsuarioBE> ConsultaRango(BE.UsuarioBE filtroDesde,BE.UsuarioBE filtroHasta)
    {
        try
        {
            return this._dal.ConsultaRango(filtroDesde, filtroHasta);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede consultar por rango.", ex);
        }
    }


    /// <summary>
    ///     ''' Modifica un usuario existente del sistema.
    ///     ''' </summary>
    public bool Modificacion(ref BE.UsuarioBE value)
    {
        try
        {
            return _dal.Modificacion(ref value);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede modificar.", ex);
        }
    }

    /// <summary>
    ///     ''' Agrega un permiso al perfil del usuario.
    ///     ''' </summary>
    public bool AgregarPermiso(BE.UsuarioBE usuario, BE.PermisoBE permiso)
    {
        try
        {
            this._dalPerfil.UsuarioActual = usuario;
            return this._dalPerfil.Alta(ref permiso);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede agregar el permiso al perfil del usuario.", ex);
        }
    }

    /// <summary>
    ///     ''' Quita un permiso del perfil del usuario.
    ///     ''' </summary>
    public bool QuitarPermiso(BE.UsuarioBE usuario, BE.PermisoBE permiso)
    {
        try
        {
            this._dalPerfil.UsuarioActual = usuario;
            return this._dalPerfil.Baja(ref permiso);
        }
        catch (Exception ex)
        {
            throw new Exception("No se puede quitar el permiso del perfil del usuario.", ex);
        }
    }

    public List<PermisoBE> ObtenerPermisoPorUsuario(UsuarioBE value)
    {
        return null;
    }




}

//tritonyl 300 mg