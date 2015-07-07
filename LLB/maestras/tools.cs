using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.IO;
using Brainsbits.LLB.programasRseGlobal;
using Brainsbits.LLB.GestionHumana;

namespace Brainsbits.LLB.maestras
{
    public class tools
    {
        #region variables
        String _mensaje_error = null;
        #endregion variables

        #region propiedades
        public String MensajError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        #endregion propiedades

        #region constructores
        public tools()
        {
        }
        #endregion constructores

        #region metodos
        public string RemplazarCaracteresEnString(string InputTxt)
        {
            string Result = InputTxt;

            Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"[\000\010\012\015\']", ".");

            return Result;
        }

        public String obtenerStringConFormatoFechaSQLServer(DateTime fecha)
        {
            return fecha.ToString("yyyyMMdd");
        }

        public String obtenerStringConFormatoHoraSQLServer(int hora, int minutos)
        {
            String resultado;

            String hora_string = hora.ToString();
            String minutos_string = minutos.ToString();

            if (hora_string.Length == 1)
            {
                hora_string = "0" + hora_string;
            }
            if (minutos_string.Length == 1)
            {
                minutos_string = "0" + minutos_string;
            }

            resultado = hora_string + minutos_string;

            return resultado;
        }

        public String convierteComaEnPuntoParaDecimalesEnSQL(Decimal numero)
        {
            String resultado = numero.ToString();

            resultado = resultado.Replace(',', '.');

            return resultado;
        }

        public String conviertePuntoEnComa(String numero)
        {
            String resultado = numero.ToString();

            resultado = resultado.Replace('.', ',');

            return resultado;
        }

        public int obtenerIndexOfArreglo(int indexParaComenzar, String valor, String[] arreglo)
        {
            int resultado = -1;

            for (int i = indexParaComenzar; i < arreglo.Length; i++)
            {
                if (valor == arreglo[i])
                {
                    resultado = i;
                    return resultado;
                }
            }

            return resultado;
        }

        public int obtenerIndexOfArregloSubString(int indexParaComenzar, int indexParaTerminar, String valorSubString, String[] arreglo)
        {
            int resultado = -1;

            for (int i = indexParaComenzar; i < indexParaTerminar; i++)
            {
                if (arreglo[i].Contains(valorSubString) == true)
                {
                    resultado = i;
                    return resultado;
                }
            }

            return resultado;
        }

        public void enviarCorreo(String to, String subject, string message)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            SmtpClient smpt = new SmtpClient();
            smpt.Send(mailMessage);
        }

        public Boolean enviarCorreoConCuerpoHtmlyArchivoAdjunto(String to, String subject, String message, Stream streamArchivo, String nombreArchivo)
        {
            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message;

                mailMessage.Attachments.Add(new Attachment(streamArchivo, nombreArchivo));

                SmtpClient smpt = new SmtpClient();
                smpt.UseDefaultCredentials = false;
                smpt.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                MensajError = ex.ToString();
                return false;
            }
        }

        public Boolean enviarCorreoConCuerpoHtmlyArchivoAdjuntoConFromYCopias(String from, List<String> listaCopias, String[] to, String subject, String message, Stream streamArchivo, String nombreArchivo)
        {
            MailAddress direccionFrom = new MailAddress(from);

            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                mailMessage.From = direccionFrom;

                foreach (String para in to)
                {
                    mailMessage.To.Add(new MailAddress(para));
                }

                foreach (String mailCopia in listaCopias)
                {
                    mailMessage.Bcc.Add(new MailAddress(mailCopia));
                }

                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message;
                mailMessage.Attachments.Add(new Attachment(streamArchivo, nombreArchivo));

                SmtpClient smpt = new SmtpClient();
                smpt.UseDefaultCredentials = false;
                smpt.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                MensajError = ex.ToString();
                return false;
            }
        }

        public Boolean enviarCorreoConCuerpoHtml(String to, String subject, String message)
        {
            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                String[] toArray = to.Split(';');
                foreach (String correo in toArray)
                {
                    mailMessage.To.Add(correo);
                }

                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message;
                SmtpClient smpt = new SmtpClient();
                smpt.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                MensajError = ex.ToString();
                return false;
            }
        }

        public Byte[] byteParaQueryStringSeguro()
        {
            Byte[] resultado = new Byte[16];

            resultado[0] = 0;
            resultado[1] = 1;
            resultado[2] = 2;
            resultado[3] = 3;
            resultado[4] = 4;
            resultado[5] = 5;
            resultado[6] = 6;
            resultado[7] = 7;
            resultado[8] = 8;
            resultado[9] = 9;
            resultado[10] = 1;
            resultado[11] = 2;
            resultado[12] = 3;
            resultado[13] = 4;
            resultado[14] = 5;
            resultado[15] = 8;

            return resultado;
        }

        public String convierteEnMeses(string anno, string mes)
        {
            String meses = null;

            int annos = (Convert.ToInt32(anno) / 12) + Convert.ToInt32(mes);
            meses = annos.ToString();

            return meses;
        }

        public String convierteEnAnnoMes(String valor)
        {
            String annoMes = null;
            int anno = 0;
            int meses = 0;
            Decimal x = Convert.ToDecimal(valor);
            if (x > 12)
            {
                annoMes = "0." + x;
            }
            else
            {
                annoMes = anno + "." + meses;
            }

            return annoMes;
        }


        public Image Bytes2Image(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bm = null;

            try
            {
                bm = new Bitmap(ms);
            }
            catch
            {
                return null;
            }

            return bm;
        }

        public String obtenerExtensionArchivo(String nombreArchivo)
        {
            Int32 lastIndex = nombreArchivo.LastIndexOf('.');
            Int32 caracteresRestantes = nombreArchivo.Length - lastIndex;
            String resultado = nombreArchivo.Substring(lastIndex, caracteresRestantes);
            return resultado;
        }

        public String obtenerNombreMes(Int32 numeroMes)
        {
            switch (numeroMes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return "Desconocido";
            }
        }
        public Boolean myDateValidated(String MyDateVal)
        {
            Boolean validado = true;
            try
            {
                Convert.ToDateTime(MyDateVal);
            }
            catch (Exception)
            {
                validado = false;
            }
            return validado;
        }

        public String obtenerRutaVerdaderaScript(String rutaPorVerificar)
        {
            if (rutaPorVerificar.StartsWith("/investigacion", StringComparison.OrdinalIgnoreCase) == true)
            {
                String[] rutaArray = rutaPorVerificar.Split('/');
                String resultado = "";

                for (int i = 2; i < rutaArray.Length; i++)
                {
                    resultado += "/" + rutaArray[i];
                }

                return resultado;
            }
            else
            {
                return rutaPorVerificar;
            }
        }

        public string CalcularDigitoVerificacion(string nit)
        {
            string temp;
            int contador;
            int residuo;
            int acumulador;
            int[] vector = new int[15];

            vector[0] = 3;
            vector[1] = 7;
            vector[2] = 13;
            vector[3] = 17;
            vector[4] = 19;
            vector[5] = 23;
            vector[6] = 29;
            vector[7] = 37;
            vector[8] = 41;
            vector[9] = 43;
            vector[10] = 47;
            vector[11] = 53;
            vector[12] = 59;
            vector[13] = 67;
            vector[14] = 71;

            acumulador = 0;

            residuo = 0;

            for (contador = 0; contador < nit.Length; contador++)
            {
                temp = nit[(nit.Length - 1) - contador].ToString();
                acumulador = acumulador + (Convert.ToInt32(temp) * vector[contador]);
            }

            residuo = acumulador % 11;

            if (residuo > 1)
                return Convert.ToString(11 - residuo);

            return residuo.ToString();
        }

        public int ObtenerEdadDesdeFechaNacimiento(DateTime nacimiento)
        {
            DateTime ahora = DateTime.Today;
            int edad = ahora.Year - nacimiento.Year;
            if (nacimiento > ahora.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }

        public String DiferenciaFechas(DateTime newdt, DateTime olddt)
        {
            Int32 anios;
            Int32 meses;
            Int32 dias;
            String str = "";

            anios = (newdt.Year - olddt.Year);
            meses = (newdt.Month - olddt.Month);
            dias = (newdt.Day - olddt.Day);

            if (meses < 0)
            {
                anios -= 1;
                meses += 12;
            }
            if (dias < 0)
            {
                meses -= 1;
                dias += DateTime.DaysInMonth(newdt.Year, newdt.Month);
            }

            if (anios < 0)
            {
                return "Error en fechas.";
            }
            if (anios > 0)
                str = str + anios.ToString() + " años ";
            if (meses > 0)
                str = str + meses.ToString() + " meses ";
            if (dias > 0)
                str = str + dias.ToString() + " dias ";

            return str;
        }

        public Boolean IsNumeric(String dato)
        {
            Boolean Numerico = false;

            try
            {
                if (dato.Length > 0)
                {
                    Decimal DatoNumerico = Convert.ToDecimal(dato.ToString());
                    if (DatoNumerico > 0) Numerico = true;
                }
            }
            catch
            {
                Numerico = false;
            }
            return Numerico;
        }


        public String ObtenerIdAreaProceso(int proceso)
        {
            if (proceso == ((int)tabla.proceso.ContactoRse))
            {
                return Programa.Areas.RSE.ToString();
            }
            else
            {
                if (proceso == ((int)tabla.proceso.ContactoGlobalSalud))
                {
                    return Programa.Areas.GLOBALSALUD.ToString();
                }
                else
                {
                    if (proceso == ((int)tabla.proceso.ContactoBienestarSocial))
                    {
                        return Programa.Areas.BS.ToString();
                    }
                    else
                    {
                        if (proceso == ((int)tabla.proceso.ContactoOperaciones))
                        {
                            return Programa.Areas.OPERACIONES.ToString();
                        }
                        else
                        {
                            if (proceso == ((int)tabla.proceso.ContactoGestionHumana))
                            {
                                return Programa.Areas.GESTIONHUMANA.ToString();
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
        }







        public Programa.Areas ObtenerEnumIdAreaProceso(int proceso)
        {
            if (proceso == ((int)tabla.proceso.ContactoRse))
            {
                return Programa.Areas.RSE;
            }
            else
            {
                if (proceso == ((int)tabla.proceso.ContactoGlobalSalud))
                {
                    return Programa.Areas.GLOBALSALUD;
                }
                else
                {
                    if (proceso == ((int)tabla.proceso.ContactoBienestarSocial))
                    {
                        return Programa.Areas.BS;
                    }
                    else
                    {
                        if (proceso == ((int)tabla.proceso.ContactoOperaciones))
                        {
                            return Programa.Areas.OPERACIONES;
                        }
                        else
                        {
                            return Programa.Areas.GESTIONHUMANA;
                        }
                    }
                }
            }
        }




        public String ObtenerTablaNombreArea(int proceso)
        {
            String NOMBRE_AREA = "FALTA_CONFIGURAR AREA EN METODO ObtenerTablaNombreArea in tools";

            switch (proceso)
            {
                case ((int)tabla.proceso.ContactoRse):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_RESPONSABILIDAD_SOCIAL_EMPRESARIAL;
                    break;
                case ((int)tabla.proceso.ContactoGlobalSalud):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_SALUD_INTEGRAL;
                    break;
                case ((int)tabla.proceso.ContactoBienestarSocial):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_BIENESTAR_SOCIAL;
                    break;
                case ((int)tabla.proceso.ContactoOperaciones):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_OPERACIONES;
                    break;
                case ((int)tabla.proceso.Contabilidad):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_CONTABILIDAD;
                    break;
                case ((int)tabla.proceso.ContactoComercial):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;
                    break;
                case ((int)tabla.proceso.ContactoComprasEInventario):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_COMPRAS_INVENTARIO;
                    break;
                case ((int)tabla.proceso.ContactoContabilidad):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_CONTABILIDAD;
                    break;
                case ((int)tabla.proceso.ContactoContratacion):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_CONTRATACION;
                    break;
                case ((int)tabla.proceso.ContactoFinanciera):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_FINANCIERA;
                    break;
                case ((int)tabla.proceso.ContactoJuridica):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_JURIDICA;
                    break;
                case ((int)tabla.proceso.ContactoLiquidacionPrestaciones):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_LIQUIDACION_NOMINA;
                    break;
                case ((int)tabla.proceso.ContactoNominaFacturacion):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_LIQUIDACION_NOMINA;
                    break;
                case ((int)tabla.proceso.ContactoSaludIntegral):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_SALUD_INTEGRAL;
                    break;
                case ((int)tabla.proceso.ContactoSeleccion):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_SELECCION;
                    break;
                case ((int)tabla.proceso.Financiera):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_FINANCIERA;
                    break;
                case ((int)tabla.proceso.Nomina):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_LIQUIDACION_NOMINA;
                    break;
                case ((int)tabla.proceso.ContactoGestionHumana):
                    NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_HUMANA;
                    break;
            }

            return NOMBRE_AREA;
        }

        public Decimal ObtenerIdEmpleadorPorSession(Int32 idEmpresa)
        {
            if (idEmpresa == 1)
            {
                return 205;
            }
            else
            {
                return 34;
            }
        }



        public EvaluacionPlanta.TiposEvaluacion ObtenerEnumTipoEvaluacion(String tipoEvaluacion)
        {
            if (tipoEvaluacion == EvaluacionPlanta.TiposEvaluacion.ACTITUDINAL.ToString())
            {
                return EvaluacionPlanta.TiposEvaluacion.ACTITUDINAL;
            }
            else
            {
                if (tipoEvaluacion == EvaluacionPlanta.TiposEvaluacion.DESEMPENO.ToString())
                {
                    return EvaluacionPlanta.TiposEvaluacion.DESEMPENO;
                }
                else
                {
                    return EvaluacionPlanta.TiposEvaluacion.PRUEBA;
                }
            }
        }


        public String ObtenerNombreModuloEvaluacion(String tipoEvaluacion)
        {
            if (tipoEvaluacion == EvaluacionPlanta.TiposEvaluacion.ACTITUDINAL.ToString())
            {
                return "EVALUACIÓN ACTITUDINAL";
            }
            else
            {
                if (tipoEvaluacion == EvaluacionPlanta.TiposEvaluacion.DESEMPENO.ToString())
                {
                    return "EVALUACIÓN DE DESEMPEÑO";
                }
                else
                {
                    return "EVALUACIÓN PERIODO DE PRUEBA";
                }
            }
        }

        public Decimal ConvertirDecimal(String valor)
        {
            String separadorDecimal = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            if (separadorDecimal == ",")
            {
                return Convert.ToDecimal(valor.Replace(".", separadorDecimal));
            }
            else
            {
                return Convert.ToDecimal(valor.Replace(",", separadorDecimal));
            }
        }

        public Boolean DeterminarMultiploNumero(decimal numValidar, decimal multiploDe)
        {
            if ((numValidar > 0) && (numValidar >= multiploDe))
            {
                if ((numValidar % multiploDe) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (numValidar == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
