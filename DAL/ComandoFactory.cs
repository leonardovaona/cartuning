using System;
using System.Configuration;


public class ComandoFactory
{

    /// <summary>
    ///     ''' Retorna una instancia del objeto que se va a conectar a la base de datos.
    ///     ''' </summary>
    public static IComando CrearComando(string connectionKey)
    {

        ConnectionStringSettings connSettings = new ConnectionStringSettings();
        connSettings.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConString"].ConnectionString.ToString();

        if (connSettings != null)
        {
            switch (connSettings.ProviderName.ToUpper())
            {
                case "SYSTEM.DATA.SQLCLIENT":
                    {
                        return new DB(connSettings.ConnectionString);
                    }

                default:
                    {
                        return new DB(connSettings.ConnectionString);
                    }
            }
        }
        else
            throw new ConfigurationErrorsException(string.Format("No se configuró la cadena de conexion para la entrada '{0}'.", connectionKey));
    }
}
