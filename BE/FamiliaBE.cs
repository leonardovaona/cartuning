using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class FamiliaBE : PermisoBE
    {
        public List<PermisoBE> Permisos { get; set; }

        public FamiliaBE()
        {
            this.Permisos = new List<PermisoBE>();
        }
        
        public FamiliaBE(PermisoBE permiso)
        {
            this.Id = permiso.Id;
            this.Nombre = permiso.Nombre;
            this.Descripcion = permiso.Descripcion;
            this.Permisos = new List<PermisoBE>();
        }    
    }
}
