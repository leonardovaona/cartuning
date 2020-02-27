using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductoBE
    {
        public int Id { set; get; }
        public string Descripcion { set; get; }
        public List<ParteProductoBE> Partes { set; get; }
        public double Precio { set; get; }
        public TipoProductoBE Tipo { set; get; }
        public UsuarioBE Usuario { set; get; }
        public object Imagen { set; get; }

        public List<Byte[]> Imagenes { set; get; }
        public ProductoBE()
        {
            this.Partes = new List<ParteProductoBE>();
            this.Tipo = new TipoProductoBE();
            this.Usuario = new UsuarioBE();
            this.Imagenes = new List<Byte[]>();
        }
    }  
}
