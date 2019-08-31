using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class UsuarioBE
    {
        public UsuarioBE () {}

        public int Id { get; set; }
        public string Username { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }        
        public int DVH { get; set; }
        public List<PermisoBE> Perfil { get; set; }
        public bool Eliminado { get; set; }
        public int Bloqueado { get; set; }

        public override string ToString()
        {
            return this.Username;
        }
    }
}

    