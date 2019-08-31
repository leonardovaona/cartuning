using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace BE
{
    public class VerificadorBE : IVerificable
    {
        public VerificadorBE() { }

        public VerificadorBE(string tablaNombre, long digitoHorizontal, long digitoVertical)
        {
            TablaNombre = tablaNombre;
            DigitoHorizontal = digitoHorizontal;
            DigitoVertical = digitoVertical;            
        }

        public VerificadorBE(VerificadorBE obj)
        {
            this.TablaNombre = obj.TablaNombre;
            this.DigitoHorizontal = obj.DigitoHorizontal;
            this.DigitoVertical = obj.DigitoVertical;
        }


        public int Id { get; set; }        
        public string TablaNombre { get; set; }
        public long DigitoHorizontal { get; set; }
        public long DigitoVertical { get; set; }
        
    }
}
