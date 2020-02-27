using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class PagoDAL
    {
        private IComando _wrapper = null;
        private IConversor<PagoBE> _conversor = null;

        public bool Alta(PagoBE value)
        {
            int resultado = 0;
            System.Data.IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO pago values (@monto, @fecha, @tipotarjeta, @numerotarjeta, @fechavencimiento, @titular, @codigoseguridad,@idpedido, @idusuario) SET @identity =@@Identity", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@monto", value.Monto);
                this.Wrapper.AgregarParametro(comando, "@fecha", value.Fecha);
                this.Wrapper.AgregarParametro(comando, "@tipotarjeta", value.TipoTarjeta);
                this.Wrapper.AgregarParametro(comando, "@numerotarjeta", value.NumeroTarjeta);
                this.Wrapper.AgregarParametro(comando, "@fechavencimiento", value.Vencimiento);
                this.Wrapper.AgregarParametro(comando, "@titular", value.Titular);
                this.Wrapper.AgregarParametro(comando, "@codigoseguridad", value.CodigoSeguridad);
                this.Wrapper.AgregarParametro(comando, "@idpedido", value.Pedido.Id);
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

        public PagoBE Consulta(int id)
        {
            PagoBE pago = new PagoBE();
            pago.Id = id;
            return Consulta(pago);
        }

        public PagoBE Consulta(PagoBE filtro)
        {

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM pago where id = @id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", filtro.Id);

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

        public PagoBE BuscarPagoPorIdPedido(PagoBE filtro)
        {

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM pago WHERE id_pedido = @id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", filtro.Pedido.Id);

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

        public IConversor<PagoBE> Conversor
        {
            get
            {
                if (this._conversor == null)
                    this._conversor = new PagoConversor();
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
