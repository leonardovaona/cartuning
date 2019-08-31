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
using System.Data.SqlClient;

public class DB : IComando
{
    private string connectionString;
    private SqlConnection connection;
    private SqlTransaction transaction;

    public DB(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public System.Data.IDataParameter AgregarParametro(System.Data.IDbCommand command, string parameterName, object value)
    {
        SqlParameter param = new SqlParameter(parameterName, value);
        command.Parameters.Add(param);
        return param;
    }

    public System.Data.IDataParameter AgregarParametro(System.Data.IDbCommand command, string parameterName, object value, System.Data.DbType type, System.Data.ParameterDirection direction)
    {
        SqlParameter param = new SqlParameter(parameterName, value);
        param.DbType = type;
        param.Direction = direction;
        command.Parameters.Add(param);
        return param;
    }

    public System.Data.IDbTransaction IniciarTransaccion()
    {
        return this.ValidarConnection().BeginTransaction();
    }

    public void CerrarConexion(System.Data.IDbCommand command)
    {
        if (command.Connection != null)
        {
            if (command.Connection.State != ConnectionState.Closed)
                command.Connection.Close();
        }
    }

    public void FinalizarTransaccion(IDbCommand command)
    {
        if (command.Transaction != null)
            this.transaction = (SqlTransaction)command.Transaction;

        if (this.transaction != null)
        {
            this.transaction.Commit();
            this.transaction = null;
        }
    }

    public System.Data.IDbCommand CrearComando(string commandText, System.Data.CommandType commandType)
    {
        SqlCommand command = new SqlCommand(commandText);
        command.CommandType = commandType;
        return command;
    }

    public int EjecutarConsulta(System.Data.IDbCommand command)
    {
        command.Connection = this.ValidarConnection();
        command.Transaction = this.transaction;
        return command.ExecuteNonQuery();
    }

    public int EjecutarConsultaEscalar(System.Data.IDbCommand command)
    {
        command.Connection = this.ValidarConnection();
        command.Transaction = this.transaction;
        return System.Convert.ToInt32(command.ExecuteScalar());
    }


    public System.Data.IDataReader ConsultarReader(System.Data.IDbCommand command)
    {
        command.Connection = this.ValidarConnection();
        return command.ExecuteReader();
    }

    public System.Data.DataTable ConsultarDataTable(System.Data.IDbCommand command)
    {
        DataTable table = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter((SqlCommand)command);
        command.Connection = this.ValidarConnection();
        adapter.Fill(table);
        return table;
    }

    public System.Data.IDbConnection ObtenerConexion()
    {
        if (this.connection == null)
            this.connection = new SqlConnection(connectionString);

        return this.connection;
    }

    public void CancelarTransaccion(IDbCommand command)
    {
        if (command.Transaction != null)
            this.transaction = (SqlTransaction)command.Transaction;

        if (this.transaction != null)
        {
            this.transaction.Rollback();
            this.transaction = null;
        }
    }

    private SqlConnection ValidarConnection()
    {
        if (this.connection == null)
            this.connection = (SqlConnection)this.ObtenerConexion();

        if (this.connection.State != ConnectionState.Open)
            this.connection.Open();

        return this.connection;
    }
}
