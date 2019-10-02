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
using System.Data;
using BE;

public class VerificadorConversor : IConversor<BE.VerificadorBE>
{
    public VerificadorBE Convertir(DataRow row)
    {        
        VerificadorBE objDTO = new VerificadorBE();
        objDTO.Id = Convert.ToInt32(row["id"]);
        objDTO.DigitoHorizontal = Convert.ToInt32(row["dvh"]);
        objDTO.DigitoVertical = Convert.ToInt32(row["digito"]);
        objDTO.TablaNombre = Convert.ToString(row["tabla"]);
        return objDTO;
    }

    public VerificadorBE Convertir(IDataReader reader)
    {
        VerificadorBE objDTO = new VerificadorBE();
        objDTO.Id = Convert.ToInt32(reader["id"]);
        objDTO.DigitoHorizontal = Convert.ToInt32(reader["dvh"]);
        objDTO.DigitoVertical = Convert.ToInt32(reader["digito"]);
        objDTO.TablaNombre = Convert.ToString(reader["tabla"]);
        return objDTO;
    }
}
