using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public interface IVerificador <T>
    {
        int CalcularDVH(ref T value);
        bool VerificarDVH(T value);
        bool VerificarDVHTabla();
        void ActualizarDVH(T value);
        void ActualizarDVHTabla();
    }
