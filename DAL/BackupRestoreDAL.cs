using System;
using System.Data;
using System.Collections.Generic;
using BE;

namespace DAL
{


    public class BackupRestoreDAL
    {

        private IComando _wrapper = null;

        private IConversor<BackupRestoreBE> _conversor = null;

        const string DBNAME = "CarTuningDB";

        public bool RealizarBackup(string nombreBackup, List<string> filenames)
        {
            int resultado = 0;
            string cmdStr = "BACKUP DATABASE " + DBNAME + " TO ";
            int paramcount = 1;
            foreach (string filename in filenames)
            {
                cmdStr += "DISK = @filename" + System.Convert.ToString(paramcount) + ",";
                paramcount += 1;
            }
            cmdStr = cmdStr.Trim().Remove(cmdStr.Length - 1);
            cmdStr += " WITH FORMAT,NAME=@name";
            IDbCommand comando = this.Wrapper.CrearComando(cmdStr, CommandType.Text);

            try
            {
                this.Wrapper.AgregarParametro(comando, "@name", nombreBackup);

                paramcount = 1;
                foreach (string filename in filenames)
                {
                    this.Wrapper.AgregarParametro(comando, "@filename" + System.Convert.ToString(paramcount), filename);
                    paramcount += 1;
                }

                resultado = this._wrapper.EjecutarConsulta(comando);
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            return (resultado > 0);
        }

        public bool RealizarRestore(List<string> filenames)
        {
            int resultado = 0;
            string cmdStr = "USE [MASTER] ; ALTER DATABASE " + DBNAME + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE ; RESTORE DATABASE " + DBNAME + " FROM ";
            int paramcount = 1;
            foreach (string filename in filenames)
            {
                cmdStr += "DISK = @filename" + System.Convert.ToString(paramcount) + ",";
                paramcount += 1;
            }
            cmdStr = cmdStr.Trim().Remove(cmdStr.Length - 1);
            cmdStr += " ;ALTER DATABASE " + DBNAME + " SET MULTI_USER";

            IDbCommand comando = this.Wrapper.CrearComando(cmdStr, CommandType.Text);

            try
            {
                paramcount = 1;
                foreach (string filename in filenames)
                {
                    this.Wrapper.AgregarParametro(comando, "@filename" + System.Convert.ToString(paramcount), filename);
                    paramcount += 1;
                }
                resultado = this._wrapper.EjecutarConsulta(comando);
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            return (resultado > 0);
        }

        public List<BackupRestoreBE> BackupExistentes()
        {
             List<BackupRestoreBE> lista = new List<BackupRestoreBE>();
            string sCmd = "  SELECT DISTINCT  msdb.dbo.backupset.media_set_id,msdb.dbo.backupset.name AS backupset_name,msdb.dbo.backupset.backup_finish_date, ";
            sCmd += " cast(round(msdb.dbo.backupset.backup_size/1048576,2) as decimal(18,2)) as backup_size_mb,  ";
            sCmd += " msdb.dbo.backupmediafamily.physical_device_name, ";
            sCmd += " msdb.dbo.backupset.description";
            sCmd += " FROM msdb.dbo.backupmediafamily";
            sCmd += " INNER JOIN msdb.dbo.backupset ON msdb.dbo.backupmediafamily.media_set_id = msdb.dbo.backupset.media_set_id  ";
            sCmd += " WHERE  msdb..backupset.type = 'D' ";
            sCmd += " and msdb..backupset.database_name = '" + DBNAME + "' ";
            sCmd += " order by backup_finish_date";
            IDbCommand comando = this.Wrapper.CrearComando(sCmd, CommandType.Text);
            try
            {
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        lista.Add(this.Conversor.Convertir(reader));
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            return lista;
        }

        public double BackupSize()
        {
            double fileSize = -1;
            IDbCommand comando = this.Wrapper.CrearComando("select backupSize = convert(numeric(10,2), round(fileproperty( a.name,'SpaceUsed')/128.,2)) from sysfiles a where a.name = '" + DBNAME + "'", CommandType.Text);
            try
            {
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        fileSize = (Convert.ToDouble(reader["backupSize"]));
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            return fileSize;
        }

        public IConversor<BackupRestoreBE> Conversor
        {
            get
            {
                if (this._conversor == null)
                    // obtener el conversor por defecto para esta entidad
                    this._conversor = new BackupConversor();
                return this._conversor;
            }
            set
            {
                this._conversor = value;
            }
        }


        public IComando Wrapper
        {
            get
            {
                if (this._wrapper == null)
                    this._wrapper = ComandoFactory.CrearComando("Default");
                return this._wrapper;
            }
            set
            {
                this._wrapper = value;
            }
        }
    }

}
