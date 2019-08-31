using System;
using System.Collections.Generic;
using System.Data;
using BE;
using DAL;

public class FamiliaDAL
{

    private IComando _wrapper = null;

    private IConversor<FamiliaBE> _conversor = null;

        public bool Alta(ref FamiliaBE value)
        {
            int resultado = 0;
            IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO familia VALUES(@id, @id_patente,0)", CommandType.Text);
        try
        {
            foreach (var permisos in value.Permisos)
                {
                this.Wrapper.AgregarParametro(comando, "@id", value.Id);
                this.Wrapper.AgregarParametro(comando, "@id_permiso", permisos.Id);
                IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "RETURN_VALUE", 0, DbType.Int32, ParameterDirection.ReturnValue);

                resultado = this._wrapper.EjecutarConsulta(comando);

                if ((resultado > 0))
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

        public bool Baja(ref BE.FamiliaBE value)
        {
            int resultado = 0;
            IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_ELIMINAR", CommandType.StoredProcedure);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", value.Id);
                this.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado);

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

        public FamiliaBE Consulta(ref FamiliaBE filtro)
        {
            List<FamiliaBE> lista = this.ConsultaRango(filtro, null);
            if (lista != null && lista.Count > 0)
                return lista[0];
            else
                return null/* TODO Change to default(_) if this is not a reference type */;
        }

        public List<FamiliaBE> ConsultaRango(FamiliaBE filtroDesde, FamiliaBE filtroHasta)
        {
            List<FamiliaBE> lista = new List<FamiliaBE>();

            IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_LISTAR", CommandType.StoredProcedure);
            try
            {
                if (filtroDesde != null && filtroDesde.Id > 0)
                    this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
                else
                    this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
                if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Nombre))
                    this.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Nombre);
                else
                    this.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value);

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

        public List<FamiliaBE> ConsultaRango(ref FamiliaBE filtroDesde, ref FamiliaBE filtroHasta)
        {
            List<FamiliaBE> lista = new List<FamiliaBE>();

            IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_LISTAR", CommandType.StoredProcedure);
            try
            {
                if (filtroDesde != null && filtroDesde.Id > 0)
                    this.Wrapper.AgregarParametro(comando, "@id", filtroDesde.Id);
                else
                    this.Wrapper.AgregarParametro(comando, "@id", DBNull.Value);
                if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Nombre))
                    this.Wrapper.AgregarParametro(comando, "@nombre", filtroDesde.Nombre);
                else
                    this.Wrapper.AgregarParametro(comando, "@nombre", DBNull.Value);
            
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

        public bool Modificacion(ref FamiliaBE value)
        {
            int resultado = 0;
            IDbCommand comando = this.Wrapper.CrearComando("HS_PERMISO_MODIFICAR", CommandType.StoredProcedure);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@nombre", value.Nombre);
                this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);
                this.Wrapper.AgregarParametro(comando, "@eliminado", value.Eliminado);
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

        public IConversor<FamiliaBE> Conversor
        {
            get
            {
                if (this._conversor == null)
                    this._conversor = new FamiliaConversor();
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
