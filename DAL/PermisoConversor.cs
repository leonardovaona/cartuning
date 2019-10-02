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
    private FamiliaDAL _familiaDAL = new FamiliaDAL();

    public BE.PermisoBE Convertir(System.Data.DataRow row)
    {
        Int32 espadre = Convert.ToInt32(row["espadre"]);

        PermisoBE permiso = new PermisoBE();
       
        permiso.Id = Convert.ToInt32(row["id"]);
        permiso.Nombre = Convert.ToString(row["Nombre"]);
        permiso.Descripcion = Convert.ToString(row["Descripcion"]);
        permiso.Eliminado = Convert.ToBoolean(row["Eliminado"]);
        /*
        if (espadre != 0)
        {
            FamiliaBE familiaFiltro = new FamiliaBE();
            familiaFiltro.Id = permiso.Id;
            List <FamiliaBE> familias= _familiaDAL.Consulta(ref familiaFiltro);

            foreach (FamiliaBE familia in familias)
            {                                
                PermisoDAL permisoDAL = new PermisoDAL();
                PermisoBE permisoAsignado = permisoDAL.Consulta(familia.IdPermiso);
                permiso.Permisos.Add(permisoAsignado);
            }
        }*/
        return permiso;
    }

    public BE.PermisoBE Convertir(System.Data.IDataReader reader)
    {
        Int32 esPadre = Convert.ToInt32(reader["espadre"]);

        PermisoBE permiso = new PermisoBE();
        
        permiso.Id = Convert.ToInt32(reader["id"]);
        permiso.Nombre = Convert.ToString(reader["Nombre"]);
        permiso.Descripcion = Convert.ToString(reader["Descripcion"]);
        permiso.Eliminado = Convert.ToBoolean(reader["Eliminado"]);
        /*
        if (esPadre != 0)
        {
            FamiliaBE familiaFiltro = new FamiliaBE();
            familiaFiltro.Id = permiso.Id;
            List<FamiliaBE> familias = _familiaDAL.Consulta(ref familiaFiltro);

            foreach (FamiliaBE familia in familias)
            {
                PermisoDAL permisoDAL = new PermisoDAL();
                PermisoBE permisoAsignado = new PermisoBE();
                permisoAsignado = permisoDAL.Consulta(familia.IdPermiso);
                permiso.Permisos.Add(permisoAsignado);
            }
        }*/

        return permiso;
    }
}
