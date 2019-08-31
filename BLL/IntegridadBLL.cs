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
using DAL;


namespace BLL
{
    public class IntegridadBLL
    {
        private static string ObtenerMensajeDeError(Exception ex)
        {
            string mensaje = string.Format("<br /><small>{0}</small>", ex.Message);
            return mensaje;
        }

        public static string VerificarIntegridadBD()
        {
            UsuarioDAL mUsuario = new UsuarioDAL();
            VerificadorDAL mVerificador = new VerificadorDAL();
            BitacoraDAL mBitacora = new BitacoraDAL();
            

            string sMsg = null;

            try
            {
                mVerificador.VerificarDVV("USUARIO", "idusuario");
            }
            catch (Exception ex)
            {
                sMsg += ObtenerMensajeDeError(ex);
            }

            try
            {
                mVerificador.VerificarDVV("BITACORA", "id");
            }
            catch (Exception ex)
            {
                sMsg += ObtenerMensajeDeError(ex);
            }

            try
            {
                mVerificador.VerificarDVV("DIGITOVERIFICADOR", "IDDIGITOVERIFICADOR");
            }
            catch (Exception ex)
            {
                sMsg += ObtenerMensajeDeError(ex);
            }
            try
            {
                mUsuario.VerificarDVHTabla();
            }
            catch (Exception ex)
            {
                sMsg += ObtenerMensajeDeError(ex);
            }

            try
            {
                mBitacora.VerificarDVHTabla();
            }
            catch (Exception ex)
            {
                sMsg += ObtenerMensajeDeError(ex);
            }

            try
            {
                mVerificador.VerificarDVHTabla();
            }
            catch (Exception ex)
            {
                sMsg += ObtenerMensajeDeError(ex);
            }

            

            return sMsg;
        }

        public static void RegenerarDigitosVerificadores()
        {
            UsuarioDAL mUsuario = new UsuarioDAL();
            BitacoraDAL mBitacora = new BitacoraDAL();
            VerificadorDAL mVerificador = new VerificadorDAL();
            
            try
            {
                mUsuario.ActualizarDVHTabla();
                mBitacora.ActualizarDVHTabla();
                mVerificador.ActualizarDVHTabla();
                

                VerificadorDAL.ActualizarDVV("USUARIO", "id");
                VerificadorDAL.ActualizarDVV("BITACORA", "id");
                VerificadorDAL.ActualizarDVV("DIGITOVERIFICADOR", "IDDIGITOVERIFICADOR");
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

