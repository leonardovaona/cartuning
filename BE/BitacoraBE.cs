using System;

namespace BE
{
    public class BitacoraBE
    {
        public BitacoraBE() { }

        public BitacoraBE (DateTime fecha, string descripcion, string username)
        {
            Fecha = fecha;
            Descripcion = descripcion;            
            Username = username;
            Eliminado = false;
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }        
        public int Criticidad { get; set; }
        public string Username { get; set; }
        public bool Eliminado { get; set; }
        public int DVH { get; set; }
    }
}