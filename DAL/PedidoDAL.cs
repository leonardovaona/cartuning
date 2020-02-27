using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class PedidoDAL
    {
        private IComando _wrapper = null;
        private IConversor<PedidoBE> _conversor = null;
        private IConversor<DetallePedidoBE> _detalleConversor = null;

        public bool Alta(PedidoBE value)
        {
            int resultado = 0;
            IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO pedido (fecha,id_usuario,estado,id_turno) VALUES(@fecha, @usuario, @estado,0)  SET @identity=@@Identity ", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@fecha", value.Fecha);
                this.Wrapper.AgregarParametro(comando, "@usuario", value.Usuario.Id);
                this.Wrapper.AgregarParametro(comando, "@estado", value.Estado);

                IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);

                resultado = this.Wrapper.EjecutarConsulta(comando);

                // asignar el Id devuelto por la consulta al objeto
                if ((resultado > 0))
                {
                    value.Id = System.Convert.ToInt32(paramRet.Value);

                    // Calculo el nuevo digito horizontal
                    //value.DVH  = CalcularDVH(ref value);
                    //Modificacion(ref value);
                    //VerificadorDAL.ActualizarDVV("BITACORA", "id");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            // este metodo retornará true si hubo registros afectados en el origen de datos
            return (resultado > 0);
        }

        public void AltaDetalle(PedidoBE pedido)
        {
            int resultado = 0;

            foreach (var value in pedido.Detalles)
            {
                IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO detallepedido (id_producto,cantidad,subtotal, id_pedido) VALUES(@producto, @cantidad, @subtotal, @pedido)  SET @identity=@@Identity ", CommandType.Text);
                try
                {                    
                    this.Wrapper.AgregarParametro(comando, "@producto", value.Producto.Id);
                    this.Wrapper.AgregarParametro(comando, "@cantidad", value.Cantidad);
                    this.Wrapper.AgregarParametro(comando, "@subtotal", value.Subtotal);
                    this.Wrapper.AgregarParametro(comando, "@pedido", pedido.Id);

                    IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);
                    
                    resultado = this.Wrapper.EjecutarConsulta(comando);
                    value.Id = System.Convert.ToInt32(paramRet.Value);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.Wrapper.CerrarConexion(comando);
                }
            }
        }

        public void BajaDetalle(int idDetalle, int idPedido)
        {
            int resultado = 0;

            IDbCommand comando = this.Wrapper.CrearComando("DELETE detallepedido WHERE id=@iddetalle AND id_pedido=@idpedido ", CommandType.Text);
                try
                {
                    this.Wrapper.AgregarParametro(comando, "@iddetalle", idDetalle);
                    this.Wrapper.AgregarParametro(comando, "@idpedido", idPedido);                    
                    
                    resultado = this.Wrapper.EjecutarConsulta(comando);                    
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.Wrapper.CerrarConexion(comando);
                }
            
        }

        public PedidoBE Consulta(int id)
        {
            PedidoBE pedido = new PedidoBE();
            pedido.Id = id;
            return Consulta(pedido);
        }

        public PedidoBE Consulta(PedidoBE filtro)
        {
            List<PedidoBE> lista = this.ConsultaRango(filtro, null);
            if (lista != null && lista.Count > 0)
                // retornar solo el primer objeto que cumpla con el filtro
                return lista[0];
            else
                return null;
        }

        public List<PedidoBE> ConsultaRango(PedidoBE filtroDesde, PedidoBE filtroHasta)
        {
            List<PedidoBE> lista = new List<PedidoBE>();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM pedido WHERE (id=@id OR @id IS NULL)", CommandType.Text);
            try
            {
                // agregar los parametros necesarios para poder ejecutar la consulta
                // solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
                if (filtroDesde != null && filtroDesde.Id > 0)
                    this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
                else
                    this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                    {
                        lista.Add(this.Conversor.Convertir(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            // este metodo retornará la lista con todas las BE convertidas que
            // se obtuvieron del origen de datos
            return lista;
        }

        public PedidoBE getPedidoPorUsuario(int id, string estado)
        {
            PedidoBE pedido = new PedidoBE();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM pedido WHERE id_usuario=@id AND estado=@estado", CommandType.Text);
            try
            {
                // agregar los parametros necesarios para poder ejecutar la consulta
                // solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
           
                this.Wrapper.AgregarParametro(comando, "@id", id);
                this.Wrapper.AgregarParametro(comando, "@estado", estado);

                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                    {
                         pedido = this.Conversor.Convertir(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            
            return pedido;
        }

        public decimal ObtenerTotal(int id)
        {
            decimal resultado = 0;
            IDbCommand comando = this.Wrapper.CrearComando("SELECT round(sum (det.cantidad * prod.precio),2) as total FROM pedido pe, detallepedido det, producto prod WHERE pe.id = det.id_pedido and prod.id = det.id_producto  and pe.id =@id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", id);
                
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                    {
                        resultado = Convert.ToDecimal(reader["total"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            // este metodo retornará la lista con todas las BE convertidas que
            // se obtuvieron del origen de datos
            return resultado;
        }
        public List<DetallePedidoBE> ConsultaDetalle(int id)
        {
            List<DetallePedidoBE> lista = new List<DetallePedidoBE>();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM detallepedido WHERE (id_pedido=@id OR @id IS NULL)", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", id);

                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                    {
                        lista.Add(this.ConversorDetalle.Convertir(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Wrapper.CerrarConexion(comando);
            }
            // este metodo retornará la lista con todas las BE convertidas que
            // se obtuvieron del origen de datos
            return lista;
        }
            public bool Modificacion(PedidoBE value)
        {
            int resultado = 0;
            IDbCommand comando = this.Wrapper.CrearComando("UPDATE pedido SET estado=@estado, WHERE id=@id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", value.Id);
                this.Wrapper.AgregarParametro(comando, "@estado", value.Estado);

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
        public bool Modificacion(int id, int turno)
        {
            int resultado = 0;
            IDbCommand comando = this.Wrapper.CrearComando("UPDATE pedido SET id_turno=@turno WHERE id=@id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", id);
                this.Wrapper.AgregarParametro(comando, "@turno", turno);

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
        public IConversor<PedidoBE> Conversor
        {
            get
            {
                if (this._conversor == null)
                    this._conversor = new PedidoConversor();
                return this._conversor;
            }
            set
            {
                this._conversor = value;
            }
        }

        public IConversor<DetallePedidoBE> ConversorDetalle
        {
            get
            {
                if (this._detalleConversor == null)
                    this._detalleConversor = new DetallePedidoConversor();
                return this._detalleConversor;
            }
            set
            {
                this._detalleConversor = value;
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
