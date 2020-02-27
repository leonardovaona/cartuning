using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ParteProductoBE
    {
        public int Id { set; get; }
        public string Descripcion { set; get; }
        public MaterialBE Material { set; get; }
        public string Color { set; get; }

        public ParteProductoBE()
        {
            this.Material = new MaterialBE();
        }
    }    
}
