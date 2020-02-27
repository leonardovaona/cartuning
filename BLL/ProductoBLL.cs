using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    public class ProductoBLL
    {
        private ProductoDAL _dal = new ProductoDAL();
        public bool Alta(ProductoBE value)
        {
            return this._dal.Alta(value);
        }

        public int Alta(ParteProductoBE value)
        {
            return this._dal.Alta(value);
        }

        public List<MaterialBE> getMateriales()
        {
            return this._dal.getMateriales();
        }

        public MaterialBE getMaterial(int id)
        {
            return this._dal.getMaterial(id);
        }

        public List<TipoProductoBE> getTipoProducto()
        {
            return this._dal.getTipoProducto();
        }

        public TipoProductoBE getTipoProducto(int id)
        {
            return this._dal.getTipoProducto(id);
        }

        public ParteProductoBE getParte(int id)
        {
            return _dal.getParte(id);
        }

        public ProductoBE Consulta(ProductoBE filtro)
        {
            return _dal.Consulta(filtro);
        }
        public List<ProductoBE> ConsultaRango (ProductoBE filtroDesde, ProductoBE filtroHasta)
        {
            return _dal.ConsultaRango(filtroDesde, filtroHasta);
        }
    }
}
