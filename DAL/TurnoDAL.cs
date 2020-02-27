using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class TurnoDAL
    {
        private IComando _wrapper = null;
        private IConversor<TurnoBE> _conversor = null;        

        public bool Alta(TurnoBE value)
        {
            int resultado = 0;
            System.Data.IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO turno values (@fecha, @dia, @idusuario) SET @identity =@@Identity", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@fecha", value.Fecha);
                this.Wrapper.AgregarParametro(comando, "@dia", value.Hora);                
                this.Wrapper.AgregarParametro(comando, "@idusuario", value.Usuario.Id);

                IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);

                resultado = this._wrapper.EjecutarConsulta(comando);


                if ((resultado > 0))
                {
                    value.Id = System.Convert.ToInt32(paramRet.Value);                    
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
            return (resultado > 0);
        }


        public TurnoBE Consulta(int id)
        {

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM turno where id = @id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", id);

                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        return this.Conversor.Convertir(reader);
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
            return null;
        }
        public IConversor<TurnoBE> Conversor
        {
            get
            {
                if (this._conversor == null)
                    this._conversor = new TurnoConversor();
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
