using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
        public class IdiomaDAL
        {
            private IComando _wrapper = null;

            private IConversor<IdiomaBE> _conversor = null;

            private IConversor<DetalleIdiomaBE> _conversorDetalle = null;

        public bool Alta(IdiomaBE value)
            {
                int resultado = 0;
                IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO idioma (codigo,descripcion) VALUES(@codigo, @descripcion)  SET @identity=@@Identity ", CommandType.Text);
                try
                {
                    this.Wrapper.AgregarParametro(comando, "@codigo", value.Codigo);
                    this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);

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
        
            public IdiomaBE Consulta(IdiomaBE filtro)
            {
                List<IdiomaBE> lista = this.ConsultaRango(filtro, null);
                if (lista != null && lista.Count > 0)
                    // retornar solo el primer objeto que cumpla con el filtro
                    return lista[0];
                else
                    return null;
            }

            public List<IdiomaBE> ConsultaRango(IdiomaBE filtroDesde, IdiomaBE filtroHasta)
            {
                List<IdiomaBE> lista = new List<IdiomaBE>();

                // crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
                IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM idioma WHERE (id=@id OR @id IS NULL)", CommandType.Text);
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
            public bool Modificacion(IdiomaBE value)
            {
                int resultado = 0;
                IDbCommand comando = this.Wrapper.CrearComando("UPDATE idioma SET descripcion=@descripcion, codigo=@codigo WHERE id=@id", CommandType.Text);
                try
                {
                    this.Wrapper.AgregarParametro(comando, "@codigo", value.Codigo);
                    this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);
                    this.Wrapper.AgregarParametro(comando, "@id", value.Id);

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

        public List<DetalleIdiomaBE> BuscarDetalleByIdIdioma(int id)
        {
            List<DetalleIdiomaBE> detalleIdiomas = new List<DetalleIdiomaBE>();
            // crear el objeto comando que vamos a usar para realizar la accion en el origen de datos (NOTA: se recomienda usar PROCEDIMIENTOS ALMACENADOS)
            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM detalleIdioma WHERE (id_idioma=@id)", CommandType.Text);
            try
            {
                // agregar los parametros necesarios para poder ejecutar la consulta
                // solo buscar por Id, si se especifico filtrodesde y el Id en el filtroDesde es mayor que cero
                if (id > 0)
                    this.Wrapper.AgregarParametro(comando, "@id", id);
                else
                    this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                    {
                        DetalleIdiomaBE detalle = new DetalleIdiomaBE();
                        detalle = this.ConversorDetalle.Convertir(reader);
                        detalleIdiomas.Add(detalle);
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

            return detalleIdiomas;
        }
            public IConversor<IdiomaBE> Conversor
            {
                get
                {
                    if (this._conversor == null)
                        this._conversor = new IdiomaConversor();
                    return this._conversor;
                }
                set
                {
                    this._conversor = value;
                }
            }

        public IConversor<DetalleIdiomaBE> ConversorDetalle
        {
            get
            {
                if (this._conversorDetalle == null)
                    this._conversorDetalle = new DetalleIdiomaConversor();
                return this._conversorDetalle;
            }
            set
            {
                this._conversorDetalle = value;
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
