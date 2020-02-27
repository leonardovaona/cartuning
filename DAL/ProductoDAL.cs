using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ProductoDAL
    {
        private IComando _wrapper = null;

        private IConversor<ProductoBE> _conversor = null;
        private IConversor<ParteProductoBE> _parteConversor = null;
        private IConversor<TipoProductoBE> _tipoConversor = null;
        private IConversor<MaterialBE> _materialConversor = null;
        
        public bool Alta(ProductoBE value)
        {
            int resultado = 0;
            System.Data.IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO producto values (@descripcion, @precio, @idtipo, @idusuario) SET @identity =@@Identity", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);
                this.Wrapper.AgregarParametro(comando, "@precio", value.Precio);
                this.Wrapper.AgregarParametro(comando, "@idtipo", value.Tipo.Id);
                this.Wrapper.AgregarParametro(comando, "@idusuario", value.Usuario.Id);

                IDataParameter paramRet = this.Wrapper.AgregarParametro(comando, "@identity", 0, DbType.Int32, ParameterDirection.Output);

                resultado = this._wrapper.EjecutarConsulta(comando);


                if ((resultado > 0))
                {
                    value.Id = System.Convert.ToInt32(paramRet.Value);
                    AltaPartes(value);
                    AltaImagenes(value);
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

        public int Alta(ParteProductoBE value)
        {
            int resultado = 0;
            System.Data.IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO parteproducto values (@descripcion,@idmaterial, @color ) SET @identity =@@Identity", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@descripcion", value.Descripcion);                
                this.Wrapper.AgregarParametro(comando, "@idmaterial", value.Material.Id);
                this.Wrapper.AgregarParametro(comando, "@color", value.Color);


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
            return (value.Id);
        }

        public void AltaPartes(ProductoBE value)
        {
            int resultado = 0;
            foreach (var parte in value.Partes)
            {
                    System.Data.IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO productoparte values (@idproducto, @idparte) ", CommandType.Text);
                    try
                    {
                        this.Wrapper.AgregarParametro(comando, "@idproducto", value.Id);
                        this.Wrapper.AgregarParametro(comando, "@idparte", parte.Id);

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
                
            }
        }

        public void AltaImagenes(ProductoBE value)
        {
            int resultado = 0;
            foreach (var imagen in value.Imagenes)
            {
                System.Data.IDbCommand comando = this.Wrapper.CrearComando("INSERT INTO productoimagen values (@idproducto, @imagen) ", CommandType.Text);
                try
                {
                    this.Wrapper.AgregarParametro(comando, "@idproducto", value.Id);
                    this.Wrapper.AgregarParametro(comando, "@imagen", imagen);

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

            }
        }

        public List<ParteProductoBE> getPartes(int id)
        {
            List<ParteProductoBE> partes = new List<ParteProductoBE>();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT par.*  FROM parteproducto par, productoparte prod WHERE par.id = prod.id_parteproducto and prod.id_producto = @id ", CommandType.Text);
            try
            {

                this.Wrapper.AgregarParametro(comando, "@id", id);


                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        partes.Add(this.ParteConversor.Convertir(reader));
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
            return partes;
        }

        public ParteProductoBE getParte(int id)
        {

            ParteProductoBE parte = new ParteProductoBE();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT *  FROM parteproducto  WHERE id = @id ", CommandType.Text);
            try
            {

                this.Wrapper.AgregarParametro(comando, "@id", id);


                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        parte = this.ParteConversor.Convertir(reader);
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
            return parte;
        }

        public TipoProductoBE getTipoProducto(int id)
        {
            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM tipoproducto WHERE id= @id ", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", id);

                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        return this.TipoConversor.Convertir(reader);
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

        public MaterialBE getMaterial(int id)
        {

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM material where id = @id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", id);

                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        return this.MaterialConversor.Convertir(reader);
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

        public List<MaterialBE> getMateriales()
        {
            List<MaterialBE> materiales = new List<MaterialBE>();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM material ", CommandType.Text);
            try
            {
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        materiales.Add(this.MaterialConversor.Convertir(reader));
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
            return materiales;
        }

        public List<TipoProductoBE> getTipoProducto()
        {
            List<TipoProductoBE> lista = new List<TipoProductoBE>();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM tipoproducto ", CommandType.Text);
            try
            {
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        lista.Add(this.TipoConversor.Convertir(reader));
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

        public List<Byte[]> getImagenes(int id)
        {
            List<Byte[]> imagenes = new List<Byte[]>();

            IDbCommand comando = this.Wrapper.CrearComando("SELECT * FROM productoimagen where id_producto = @id", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", id);

                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                    { 
                        imagenes.Add((Byte[])reader["imagen"]);
                    }
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

            return imagenes;
        }

        public List<ProductoBE> ConsultaRango(ProductoBE filtroDesde, ProductoBE filtroHasta)
        {
            List<ProductoBE> productos = new List<ProductoBE>();            
            IDbCommand comando = this.Wrapper.CrearComando("SELECT pro.* FROM producto pro, tipoproducto ti where pro.id_tipo = ti.id AND (ti.descripcion=@tipo OR @tipo IS NULL) ORDER BY pro.descripcion ", CommandType.Text);  
            try
            {                
                if (filtroDesde != null && !string.IsNullOrEmpty(filtroDesde.Tipo.Descripcion))
                    this.Wrapper.AgregarParametro(comando, "@tipo", filtroDesde.Tipo.Descripcion);
                else
                    this.Wrapper.AgregarParametro(comando, "@tipo", DBNull.Value);
                
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        productos.Add(this.Conversor.Convertir(reader));
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
            return productos;
        }

        public ProductoBE Consulta(ProductoBE filtro)
        {
            ProductoBE producto = new ProductoBE();
            IDbCommand comando = this.Wrapper.CrearComando("SELECT pro.* FROM producto pro, tipoproducto ti where pro.id_tipo = ti.id AND pro.id =@id ", CommandType.Text);
            try
            {
                this.Wrapper.AgregarParametro(comando, "@id", filtro.Id);
                using (IDataReader reader = this.Wrapper.ConsultarReader(comando))
                {
                    while (reader.Read())
                        producto = this.Conversor.Convertir(reader);
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
            return producto;
        }

        public IConversor<ProductoBE> Conversor
        {
            get
            {
                if (this._conversor == null)
                    this._conversor = new ProductoConversor();
                return this._conversor;
            }
            set
            {
                this._conversor = value;
            }
        }

        public IConversor<ParteProductoBE> ParteConversor
        {
            get
            {
                if (this._parteConversor == null)
                    this._parteConversor = new ParteProductoConversor();
                return this._parteConversor;
            }
            set
            {
                this._parteConversor = value;
            }
        }

        public IConversor<TipoProductoBE> TipoConversor
        {
            get
            {
                if (this._tipoConversor == null)
                    this._tipoConversor = new TipoProductoConversor();
                return this._tipoConversor;
            }
            set
            {
                this._tipoConversor = value;
            }
        }

        public IConversor<MaterialBE> MaterialConversor
        {
            get
            {
                if (this._materialConversor == null)
                    this._materialConversor = new MaterialConversor();
                return this._materialConversor;
            }
            set
            {
                this._materialConversor = value;
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
