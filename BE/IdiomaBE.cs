using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class IdiomaBE
    {
        public int Id { set; get; }
        public String Codigo { set; get; }
        public String Descripcion { set; get; }
        public List<DetalleIdiomaBE> Detalle { set; get; }

        public IdiomaBE()
        {
            Detalle = new List<DetalleIdiomaBE>(); 
        }
    }
}
