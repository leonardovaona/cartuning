using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class TurnoBE
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Hora { get; set; }
        public UsuarioBE Usuario { get; set; }
    }
}
