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

public class PermisoConversor : IConversor<BE.PermisoBE>
{
    private FamiliaPermisoDAL _famPemisosDAL = new FamiliaPermisoDAL();

    public BE.PermisoBE Convertir(System.Data.DataRow row)
    {
        Int32 cantHijos = 0; // Convert.ToInt32(row["Cant_Hijos"]);

        PermisoBE permiso = new PermisoBE();
       
        permiso.Id = Convert.ToInt32(row["id"]);
        permiso.Nombre = Convert.ToString(row["Nombre"]);
        permiso.Descripcion = Convert.ToString(row["Descripcion"]);
        permiso.Eliminado = Convert.ToBoolean(row["Eliminado"]);


        return permiso;
    }

    public BE.PermisoBE Convertir(System.Data.IDataReader reader)
    {
        Int32 cantHijos = 0;//Convert.ToInt32(reader["Cant_Hijos"]);

        PermisoBE permiso;
        if (cantHijos > 0)
            permiso = new PermisoCompuestoDTO();
        else
            permiso = new PermisoSimpleDTO();
        permiso.Id = Convert.ToInt32(reader["id"]);
        permiso.Nombre = Convert.ToString(reader["Nombre"]);
        permiso.Descripcion = Convert.ToString(reader["Descripcion"]);
        permiso.Eliminado = Convert.ToBoolean(reader["Eliminado"]);

        return permiso;
    }
}
