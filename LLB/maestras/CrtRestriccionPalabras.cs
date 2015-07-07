using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class CrtRestriccionPalabra
    {
        #region varialbes

        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        Dictionary<String, String> _listaPalabrasEntrada = new Dictionary<string, string>();
        Dictionary<String, String> _listaPalabrasSalida = new Dictionary<string, string>();
        #endregion varialbes

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Dictionary<String, String> listaPalabrasEntrada
        {
            get { return _listaPalabrasEntrada; }
            set { _listaPalabrasEntrada = value; }
        }

        public Dictionary<String, String> listaPalabrasSalida
        {
            get { return _listaPalabrasSalida; }
            set { _listaPalabrasSalida = value; }
        }
        #endregion propiedades

        #region constructores
        public CrtRestriccionPalabra(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public Dictionary<String, String> ComprobarListaPalabras()
        {
            foreach (KeyValuePair<String, String> palabra in listaPalabrasEntrada)
            {
                if (IsCorrecto(palabra.Value) == false)
                {
                    if (MensajeError != null)
                    {
                        listaPalabrasSalida.Add(palabra.Key, "ERROR: La consulta de restricciones en el campo " + palabra.Key + " no se pudo llevar a cabo. DESCRIPCIÓN: " + MensajeError);
                    }
                    else
                    {
                        listaPalabrasSalida.Add(palabra.Key, "El campo " + palabra.Key + ", no puede contener palabras restringidas (Ejemplo: XXX, CCCC, NA, NINGUNO, ETC.).");
                    }
                }
            }

            return listaPalabrasSalida;
        }

        private Boolean IsCorrecto(String palabra)
        {
            MensajeError = null;

            Int32 numRestricciones = ObtenerNumeroRestriccionesPalabra(palabra);

            if (MensajeError != null)
            {
                return false;
            }
            else
            {
                if (numRestricciones <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private Int32 ObtenerNumeroRestriccionesPalabra(String palabra)
        {
            String sql = null;
            Int32 resultado = 0;
            Boolean ejecutar = true;

            sql = "usp_crt_restriccion_palabras_obtenerRestriccionDeFrase '" + palabra + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    resultado = Convert.ToInt32(conexion.ExecuteScalar(sql));
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return resultado;
        }
        #endregion metodos
    }
}