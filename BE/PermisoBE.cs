using System.Collections.Generic;

namespace BE
{ 
public class PermisoBE
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int EsPadre { get; set; }
    public bool Eliminado { get; set; }
        

}
}