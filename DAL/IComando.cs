using System.Data;

/// <summary>

/// ''' Interfaz que define los metodos que deben implementar todos los proveedores

/// ''' de acceso a datos.

/// ''' </summary>

/// ''' <remarks></remarks>
public interface IComando
{
    IDbCommand CrearComando(string commandText, CommandType commandType);
    IDataParameter AgregarParametro(IDbCommand command, string parameterName, object value);
    IDataParameter AgregarParametro(IDbCommand command, string parameterName, object value, DbType type, ParameterDirection direction);

    int EjecutarConsulta(IDbCommand command);

    int EjecutarConsultaEscalar(System.Data.IDbCommand command);
    IDataReader ConsultarReader(IDbCommand command);
    DataTable ConsultarDataTable(IDbCommand command);

    IDbConnection ObtenerConexion();
    void CerrarConexion(IDbCommand command);

    IDbTransaction IniciarTransaccion();
    void FinalizarTransaccion(IDbCommand command);
    void CancelarTransaccion(IDbCommand command);
}
