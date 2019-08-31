using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class BackupRestoreBE
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public double Size { get; set; }

        public DateTime Fecha { get; set; }
    }
}
