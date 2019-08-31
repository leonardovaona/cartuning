using System;
using System.Collections.Generic;
using BE;
using DAL;

namespace BLL
{
 

    public class BackupRestoreBLL
    {
        private BackupRestoreDAL _dao = null;

        public BackupRestoreBLL(BackupRestoreDAL pDAO)
        {
            this._dao = pDAO;
        }

        public BackupRestoreBLL()
        {
            this._dao = new BackupRestoreDAL();
        }

        public bool RealizarBackup(string nombreBackup, string filename, int fragmentMB = -1)
        {
            try
            {
                List<string> filenameList = new List<string>();
                double backupSizeInMb = _dao.BackupSize();
                if ((fragmentMB > 0))
                {
                    int fileParts = System.Convert.ToInt32(Math.Truncate(backupSizeInMb / fragmentMB)); // 1.21 / 1 = 1.21 ==> 1 || 2/1 = 2
                    if ((fileParts > 0))
                    {
                        if ((backupSizeInMb % fragmentMB > 0))
                            fileParts += 1;
                        for (var index = 1; index <= fileParts; index++)
                            filenameList.Add(filename + "_part" + System.Convert.ToString(index));
                    }
                    else
                        filenameList.Add(filename);
                }
                else
                    filenameList.Add(filename);


                _dao.RealizarBackup(nombreBackup, filenameList);
            }

            // Using zip As ZipFile = New ZipFile()
            // zip.AddFile(filename)
            // zip.Comment = "Backup creado a las " & System.DateTime.Now.ToString("G")
            // If (fragmentMB > 0) Then
            // zip.MaxOutputSegmentSize = fragmentMB * 1024 * 1024   '' 2mb
            // End If
            // zip.Save(filename + ".zip")
            // End Using
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }



        public bool RealizarRestore(List<string> filenameList)
        {
            try
            {
                _dao.RealizarRestore(filenameList);
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        public List<BackupRestoreBE> BackupLista()
        {
            return _dao.BackupExistentes();
        }
    }
}
