using BE;
using System.Web.Http;
using System.Web;

public class AutenticacionVista
{

    private Autenticador _autenticador = new Autenticador();
    public UsuarioBE UsuarioActual
    {
        get
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session["UsuarioActual"] != null)
                    // existe sesion de usuario, usuario logueado y dentro de una
                    // sesion valida
                    return (UsuarioBE)HttpContext.Current.Session["UsuarioActual"];
                else
                    // no existe ninguna sesion de usuario valida en el contexto actual
                    // usuario no logueado o sesion expirada
                    return null;
            }
            else
                return null;
        }
    }

    public int IntentosFallidos
    {
        get
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Session["IntentosFallidos"] != null)
                    // existe sesion de intentos fallidos
                    return System.Convert.ToInt32(HttpContext.Current.Session["IntentosFallidos"]);
                else
                    return 0;
            }
            else
                return 0;
        }
        set
        {
            HttpContext.Current.Session["IntentosFallidos"] = value;
        }
    }

    public bool IniciarSesion(UsuarioBE value)
    {

        // --- PASO 6: Se inicia sesión, invocando al autenticador  
        UsuarioBE usuarioIntentoActual = this._autenticador.IniciarSesion(value, this.IntentosFallidos);

        // Se guardar lon intentos fallidos
        this.IntentosFallidos = this._autenticador.IntentosFallidos;
        var loginOk = false;
        if (usuarioIntentoActual != null)
        {
            // es un usuario valido, se crea la variable de Sesion de usuario
            if (HttpContext.Current != null)
                HttpContext.Current.Session["UsuarioActual"] = usuarioIntentoActual;
            loginOk = true;
        }

        // --- PASO 9: Se guarda en la bitacora el resultado del loguin ----
        if (loginOk)
        {
            BitacoraBLL mBitacora = new BitacoraBLL();
            mBitacora.Loguear(System.Convert .ToInt32(BitacoraBLL.TIPOLOG.LOGINOK), usuarioIntentoActual.Username );
        }
        else
        {
            BitacoraBLL mBitacora = new BitacoraBLL();            
            mBitacora.Loguear(System.Convert.ToInt32(BitacoraBLL.TIPOLOG.LOGINFAIL), value.Username);
        }
        //leitho
        return (usuarioIntentoActual != null);
    }


    /// <summary>
    ///     ''' Elimina toda la informacion de la sesion del Usuario actual.
    ///     ''' </summary>
    ///     ''' <remarks></remarks>
    public void CerrarSesion()
    {
        // limpiar al sesion
        HttpContext.Current.Session.Clear();
    }

    /// <summary>
    ///     ''' Retorna una instancia de un usuario valido de acuerdo a los parametros requeridos
    ///     ''' de inicio de sesion.
    ///     ''' </summary>
    public UsuarioBE CrearUsuarioParaIniciarSesion(string username, string clave)
    {

        // --- PASO 2 : Se instancia un usuario nuevo y se encripta la clave ingresada en el loguin
        UsuarioBE usuarioLogin = new UsuarioBE();
        usuarioLogin.Username = username;        
        usuarioLogin.Clave = Encrypter.EncriptarSHA512(clave);
        usuarioLogin.Eliminado = false;
        usuarioLogin.Bloqueado = 0;
        return usuarioLogin;
    }

    /// <summary>
    ///     ''' Valida que un usuairo posee un permiso dentro de su perfil.
    ///     ''' </summary>
    public bool UsuarioPoseePermiso(UsuarioBE usuarioActual, int id)
    {
        /*bool contiene = false;
        if (usuarioActual != null)
            contiene = this._autenticador.ValidarPermiso(id, usuarioActual.Perfil);
        return contiene;*/
        if (id ==5)
            return true;
        else
            return false;
    }

    public void BloquearUsuario(UsuarioBE UsuarioActual)
    {
        if (UsuarioActual != null)
        {
            UsuarioBLL mUsuarioBLL = new UsuarioBLL();
            mUsuarioBLL.BloquearUsuario(ref UsuarioActual);
        }
    }

    public int UsuarioBloqueado(UsuarioBE usuarioLogin)
    {
        // --- PASO 4: Se consulta el usuario por nombre, y se devuelve el atributo Bloqueado, 
        // ---- que puede estar en verdadero o falso.
        if (usuarioLogin != null)
        {
            UsuarioBLL mUsuarioBLL = new UsuarioBLL();
            var mDTO = mUsuarioBLL.Consulta(ref usuarioLogin);
            if (mDTO == null)
                return 0;
            return mDTO.Bloqueado;
        }
        return 0;
    }
}
