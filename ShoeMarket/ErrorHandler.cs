using System;

public class ErrorHandler
{
    public static string ObtenerMensajeDeError(Exception ex)
    {
        string mensaje = ex.Message;
        if (ex.InnerException != null)
            mensaje += string.Format("<br /><small>{0}</small>", ex.InnerException.Message);
        return mensaje;
    }
}
