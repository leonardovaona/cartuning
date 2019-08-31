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

public class UsuarioConversor : IConversor<BE.UsuarioBE>
{
    private UsuarioPermisoDAL _usuPermisoDAL = new UsuarioPermisoDAL();

    public BE.UsuarioBE Convertir(DataRow row)
    {
        UsuarioBE usuario = new UsuarioBE();
        usuario.Id = Convert.ToInt32(row["Id"]);
        usuario.Username = Convert.ToString(row["Username"]);
        usuario.Nombre = Convert.ToString(row["Nombre"]);
        usuario.Apellido = Convert.ToString(row["Apellido"]);
        usuario.Clave = Convert.ToString(row["Clave"]);
        usuario.DNI = Convert.ToString(row["DNI"]);
        usuario.Email = Convert.ToString(row["Email"]);
        usuario.Bloqueado = Convert.ToInt16(row["Bloqueado"]);
        usuario.Eliminado = Convert.ToBoolean(row["Eliminado"]);
        usuario.DVH = Convert.ToInt32(row["DVH"]);
        // obtener el perfil del usuario
        //this._usuPermisoDAL.UsuarioActual = usuario;
        //usuario.Perfil = this._usuPermisoDAL.ConsultaRango(null/* TODO Change to default(_) if this is not a reference type */, null/* TODO Change to default(_) if this is not a reference type */);

        return usuario;
    }

    public BE.UsuarioBE Convertir(System.Data.IDataReader reader)
    {
        UsuarioBE usuario = new UsuarioBE();
        usuario.Id = Convert.ToInt32(reader["id"]);
        usuario.Username = Convert.ToString(reader["Username"]);
        usuario.Clave  = Convert.ToString(reader["Clave"]);
        usuario.Nombre = Convert.ToString(reader["Nombre"]);
        usuario.Apellido  = Convert.ToString(reader["Apellido"]);
        usuario.DNI = Convert.ToString(reader["DNI"]);
        usuario.Email = Convert.ToString(reader["Email"]);
        usuario.Bloqueado = Convert.ToInt16(reader["Bloqueado"]);
        usuario.DVH = Convert.ToInt32(reader["DVH"]);
        // obtener el perfil del usuario
        //this._usuPermisoDAL.UsuarioActual = usuario;
        //usuario.Perfil = this._usuPermisoDAL.ConsultaRango(null, null);

        return usuario;
    }
}
