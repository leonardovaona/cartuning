using System;
using System.Data;
using BE;

namespace DAL
{


    public class BackupConversor : IConversor<BackupRestoreBE>
    {
        public BackupRestoreBE Convertir(DataRow row)
        {
            BackupRestoreBE objDTO = new BackupRestoreBE();
            objDTO.Id = Convert.ToInt32(row["media_set_id"]);
            objDTO.Nombre = Convert.ToString(row["backupset_name"]);
            objDTO.Fecha = Convert.ToDateTime(row["backup_finish_date"]);
            objDTO.Size = Convert.ToDouble(row["backup_size_mb"]);
            objDTO.Path = Convert.ToString(row["physical_device_name"]);
            objDTO.Descripcion = Convert.ToString(row["description"]);
            return objDTO;
        }

        public BackupRestoreBE Convertir(IDataReader reader)
        {
            BackupRestoreBE objDTO = new BackupRestoreBE();
            objDTO.Id = Convert.ToInt32(reader["media_set_id"]);
            objDTO.Nombre = Convert.ToString(reader["backupset_name"]);
            objDTO.Fecha = Convert.ToDateTime(reader["backup_finish_date"]);
            objDTO.Size = Convert.ToDouble(reader["backup_size_mb"]);
            objDTO.Path = Convert.ToString(reader["physical_device_name"]);
            objDTO.Descripcion = Convert.ToString(reader["description"]);
            return objDTO;
        }
    }
}
