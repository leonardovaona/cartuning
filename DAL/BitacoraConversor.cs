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
using BE;
using DAL;
using System.Data;

namespace DAL
{

public class BitacoraConversor : IConversor<BE.BitacoraBE>
{
    public BitacoraBE Convertir(DataRow row)
    {
        BitacoraBE bitacoraDTO = new BitacoraBE();

        bitacoraDTO.Id = Convert.ToInt32(row["id"]);
        string log = Convert.ToString(row["descripcion"]);
        string[] logSplited = log.Split(new string[] { "|" }, StringSplitOptions.None);
        bitacoraDTO.Fecha = Convert.ToDateTime(row["fecha"]);        
        bitacoraDTO.Descripcion = System.Convert.ToString(logSplited.GetValue(2));
        bitacoraDTO.Criticidad = System.Convert.ToChar(logSplited.GetValue(3));
        bitacoraDTO.Username = Convert.ToString(logSplited.GetValue(4));
            bitacoraDTO.DVH = Convert.ToInt32(row["dvh"]);
        return bitacoraDTO;
    }

    public BitacoraBE Convertir(IDataReader reader)
    {
        BitacoraBE bitacoraDTO = new BitacoraBE();

        bitacoraDTO.Id = Convert.ToInt32(reader["id"]);
        string log = Convert.ToString(reader["descripcion"]);
        string[] logSplited = log.Split(new string[] { "|" }, StringSplitOptions.None);
        bitacoraDTO.Fecha = Convert.ToDateTime (reader["fecha"]);
        bitacoraDTO.Username = Convert.ToString (logSplited.GetValue(0));
        bitacoraDTO.Descripcion = System.Convert.ToString(logSplited.GetValue(1));
        bitacoraDTO.Criticidad = System.Convert.ToInt32(logSplited.GetValue(2));
        bitacoraDTO.DVH = Convert.ToInt32(reader["dvh"]);

        return bitacoraDTO;
    }
}
}