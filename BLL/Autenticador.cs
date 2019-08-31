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


/*public interface IAutenticador
{
    /// <summary>
    ///     ''' Retorna el numero de veces que se intento un inicio de sesion (sin exito)
    ///     ''' </summary>
    int IntentosFallidos { get; }

    /// <summary>
    ///     ''' Retorna el usuario con todas sus propiedades establecidas si
    ///     ''' el inicio de sesion fue exitoso, sino Nothing.
    ///     ''' </summary>
    UsuarioBE IniciarSesion(UsuarioBE value, int intentosFallidos);

    /// <summary>
    ///     ''' Valida si existe el nombre de un pemiso dentro de una lista de permisos.
    ///     ''' </summary>
    bool ValidarPermiso(string nombrePermiso, List<PermisoBE> lista);
}*/

/// <summary>

/// ''' Gestiona la Autenticacion de usuarios en el sistema.

/// ''' </summary>

public class Autenticador
{
    public Autenticador() : this(null)
    {
    }

    public Autenticador(IUsuarioBLL UsuarioBLL)
    {
        this._UsuarioBLL = UsuarioBLL;
        if (this._UsuarioBLL == null)
            this._UsuarioBLL = new UsuarioBLL();
    }

    private IUsuarioBLL _UsuarioBLL = null;

    private int _intentosFallidos = 0;

    // Retorna el numero de veces que se intento un inicio de sesion (sin exito)
    public int IntentosFallidos
    {
        get
        {
            return this._intentosFallidos;
        }
    }

    public UsuarioBE IniciarSesion(UsuarioBE value, int intentosFallidos)
    {
        UsuarioBE usuarioIntentoActual = null;
        this._intentosFallidos = intentosFallidos;
        if (value != null && !string.IsNullOrWhiteSpace(value.Username) && !string.IsNullOrWhiteSpace(value.Clave))
        {
            usuarioIntentoActual = this._UsuarioBLL.Consulta(ref value);
            if (usuarioIntentoActual != null)
            {
                if (usuarioIntentoActual.Username.ToUpper().Equals(value.Username.ToUpper()) && usuarioIntentoActual.Clave.Equals(value.Clave) && usuarioIntentoActual.Bloqueado == 0 && usuarioIntentoActual.Eliminado == false)
                    this._intentosFallidos = 0;
                else
                {
                    this._intentosFallidos += 1;
                    
                    if (this.IntentosFallidos.Equals(3))
                    {
                        usuarioIntentoActual.Bloqueado = 1;
                        this._UsuarioBLL.Modificacion(ref usuarioIntentoActual);
                    }

                    usuarioIntentoActual = null;
                }
            }
        }        
        return usuarioIntentoActual;
    }

    public bool ValidarPermiso(int id, List<PermisoBE> lista)
    {
        bool contiene = false;
        if (lista != null)
        {
            foreach (var perm in lista)
            {
                if (perm.Id.Equals(id))
                {
                    contiene = true;
                    break;
                }
             }
        }
        return contiene;
    }
}
