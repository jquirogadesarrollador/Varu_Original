using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;

namespace Brainsbits.LLB.seleccion
{
    public class radicacionHojasDeVida
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        #endregion propiedades

        #region constructores
        public radicacionHojasDeVida(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #region solicitudesIngreso

        #region FUNCION ADICIONAR APUNTANDO A ID_OCUPACION
        #endregion



        public Decimal AdicionarRegistroRegSolicitudIngreso(DateTime FECHA_R,
            String APELLIDOS,
            String NOMBRES,
            String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD,
            String CIU_CEDULA,
            String LIB_MILITAR,
            String CAT_LIC_COND,
            String DIR_ASPIRANTE,
            String SECTOR,
            String CIU_ASPIRANTE,
            String TEL_ASPIRANTE,
            String SEXO,
            DateTime FCH_NACIMIENTO,
            String ID_GRUPOS_PRIMARIOS,
            int ID_FUENTE,
            String CONDUCTO,
            int NIV_EDUCACION,
            String E_MAIL,
            int ID_AREASINTERES,
            Decimal ASPIRACION_SALARIAL,
            String EXPERIENCIA,
            Decimal ID_OCUPACION,
            String NUCLEO_FORMACION,
            String TALLA_CAMISA,
            String TALLA_PANTALON,
            String TALLA_ZAPATOS,
            int ESTRATO,
            int NRO_HIJOS,
            Boolean C_FMLIA,
            String CEL_ASPIRANTE,
            String ESTADO_CIVIL,
            Int32 ID_PAIS,
            String TIPO_VIVIENDA,
            String FUENTE_CONOCIMIENTO,
            String RH,
            Conexion conexion)
        {
            String sql = null;

            Decimal ID_SOLICITUD = 0;

            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_solicitudes_ingreso_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "APELLIDOS = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NOMBRES = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TIP_DOC_IDENTIDAD)))
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIP_DOC_IDENTIDAD = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUM_DOC_IDENTIDAD = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CIU_CEDULA)))
            {
                sql += "'" + CIU_CEDULA + "', ";
                informacion += "CIU_CEDULA = '" + CIU_CEDULA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CIU_CEDULA = 'NULL', ";
            }

            if (SEXO.Equals("M"))
            {
                if (!(String.IsNullOrEmpty(LIB_MILITAR)))
                {
                    sql += "'" + LIB_MILITAR + "', ";
                    informacion += "LIB_MILITAR = '" + LIB_MILITAR + "', ";
                }
                else
                {
                    sql += "null, ";
                    informacion += "LIB_MILITAR = 'null', ";
                }
            }
            else
            {
                sql += "null, ";
                informacion += "LIB_MILITAR = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CAT_LIC_COND)))
            {
                sql += "'" + CAT_LIC_COND + "', ";
                informacion += "CAT_LIC_COND = '" + CAT_LIC_COND + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CAT_LIC_COND = 'null', ";
            }

            if (!(String.IsNullOrEmpty(DIR_ASPIRANTE)))
            {
                sql += "'" + DIR_ASPIRANTE + "', ";
                informacion += "DIR_ASPIRANTE = '" + DIR_ASPIRANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DIR_ASPIRANTE = 'null', ";
            }

            if (!(String.IsNullOrEmpty(SECTOR)))
            {
                sql += "'" + SECTOR + "', ";
                informacion += "SECTOR = '" + SECTOR + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "SECTOR = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CIU_ASPIRANTE)))
            {
                sql += "'" + CIU_ASPIRANTE + "', ";
                informacion += "CIU_ASPIRANTE = '" + CIU_ASPIRANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CIU_ASPIRANTE = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TEL_ASPIRANTE)))
            {
                sql += "'" + TEL_ASPIRANTE + "', ";
                informacion += "TEL_ASPIRANTE = '" + TEL_ASPIRANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TEL_ASPIRANTE = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "', ";
                informacion += "SEXO = '" + SEXO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "SEXO = 'null', ";
            }

            if (FCH_NACIMIENTO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_NACIMIENTO) + "', ";
                informacion += "FCH_NACIMIENTO = '" + FCH_NACIMIENTO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_NACIMIENTO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(ID_GRUPOS_PRIMARIOS)))
            {
                sql += "'" + ID_GRUPOS_PRIMARIOS + "', ";
                informacion += "ID_GRUPOS_PRIMARIOS= '" + ID_GRUPOS_PRIMARIOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_GRUPOS_PRIMARIOS = 'null', ";
            }

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_FUENTE = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CONDUCTO)))
            {
                sql += "'" + CONDUCTO + "', ";
                informacion += "CONDUCTO = '" + CONDUCTO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CONDUCTO = 'null', ";
            }

            if (NIV_EDUCACION != 0)
            {
                sql += NIV_EDUCACION + ", ";
                informacion += "NIV_EDUCACION = '" + NIV_EDUCACION.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NIV_EDUCACION = 'null', ";
            }

            if (!(String.IsNullOrEmpty(E_MAIL)))
            {
                sql += "'" + E_MAIL + "', ";
                informacion += "E_MAIL= '" + E_MAIL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "E_MAIL = 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_AREASINTERES != 0)
            {
                sql += ID_AREASINTERES.ToString() + ", ";
                informacion += "ID_AREASINTERES = '" + ID_AREASINTERES.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_AREASINTERES = 'null', ";
            }

            if (ASPIRACION_SALARIAL != 0)
            {
                sql += ASPIRACION_SALARIAL.ToString().Replace(",", ".") + ", ";
                informacion += "ASPIRACION_SALARIAL= '" + ASPIRACION_SALARIAL.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ASPIRACION_SALARIAL= 'null', ";
            }

            if (!(String.IsNullOrEmpty(EXPERIENCIA)))
            {
                sql += "'" + EXPERIENCIA + "', ";
                informacion += "EXPERIENCIA= '" + EXPERIENCIA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "EXPERIENCIA = 'null', ";
            }

            if (ID_OCUPACION != 0)
            {
                sql += ID_OCUPACION + ", ";
                informacion += "ID_OCUPACION = '" + ID_OCUPACION.ToString() + "',";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_OCUPACION = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NUCLEO_FORMACION)))
            {
                sql += "'" + NUCLEO_FORMACION + "', ";
                informacion += "NUCLEO_FORMACION = '" + NUCLEO_FORMACION + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUCLEO_FORMACION = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TALLA_CAMISA)))
            {
                sql += "'" + TALLA_CAMISA + "', ";
                informacion += "TALLA_CAMISA = '" + TALLA_CAMISA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA_CAMISA = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TALLA_PANTALON)))
            {
                sql += "'" + TALLA_PANTALON + "', ";
                informacion += "TALLA_PANTALON = '" + TALLA_PANTALON + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA_PANTALON = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TALLA_ZAPATOS)))
            {
                sql += "'" + TALLA_ZAPATOS + "', ";
                informacion += "TALLA_ZAPATOS = '" + TALLA_ZAPATOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA_ZAPATOS = 'null', ";
            }

            if (ESTRATO != 0)
            {
                sql += ESTRATO + ", ";
                informacion += "ESTRATO = '" + ESTRATO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ESTRATO = 'null', ";
            }

            sql += NRO_HIJOS + ", ";
            informacion += "NRO_HIJOS = '" + NRO_HIJOS + "', ";

            if (C_FMLIA == false)
            {
                sql += "'N', ";
                informacion += "C_FMLIA = 'N', ";
            }
            else
            {
                sql += "'S', ";
                informacion += "C_FMLIA = 'S', ";
            }

            if (String.IsNullOrEmpty(CEL_ASPIRANTE) == false)
            {
                sql += "'" + CEL_ASPIRANTE + "', ";
                informacion += "CEL_ASPIRANTE = '" + CEL_ASPIRANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CEL_ASPIRANTE = 'NULL', ";
            }

            if (String.IsNullOrEmpty(ESTADO_CIVIL) == false)
            {
                sql += "'" + ESTADO_CIVIL + "', ";
                informacion += "ESTADO_CIVIL = '" + ESTADO_CIVIL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ESTADO_CIVIL = 'null', ";
            }

            if (ID_PAIS != 0)
            {
                sql += ID_PAIS + ", ";
                informacion += "ID_PAIS = '" + ID_PAIS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_PAIS = 'null', ";
            }

            if (String.IsNullOrEmpty(TIPO_VIVIENDA) == false)
            {
                sql += "'" + TIPO_VIVIENDA + "', ";
                informacion += "TIPO_VIVIENDA = '" + TIPO_VIVIENDA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_VIVIENDA = 'null', ";
            }

            if (String.IsNullOrEmpty(FUENTE_CONOCIMIENTO) == false)
            {
                sql += "'" + FUENTE_CONOCIMIENTO + "', ";
                informacion += "FUENTE_CONOCIMIENTO = '" + FUENTE_CONOCIMIENTO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FUENTE_CONOCIMIENTO = 'null', ";
            }

            if (String.IsNullOrEmpty(RH) == false)
            {
                sql += "'" + RH + "'";
                informacion += "RH = '" + RH + "'";
            }
            else
            {
                sql += "null";
                informacion += "RH = 'null'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_SOLICITUD = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_SOLICITUD = 0;
                }
            }
            else
            {
                ID_SOLICITUD = 0;
            }

            return ID_SOLICITUD;
        }




        public Decimal AdicionarRegistroRegSolicitudIngresoMasivo(String APELLIDOS,
                String NOMBRES,
                String TIP_DOC_IDENTIDAD,
                String NUM_DOC_IDENTIDAD)
        {
            String sql = null;

            Decimal ID_SOLICITUD = 0;

            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_solicitudes_ingreso_adicionarMasivo ";

            #region validaciones

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIP_DOC_IDENTIDAD) == false)
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NUMERO DOCUMENTO IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    ID_SOLICITUD = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_SOLICITUD = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                ID_SOLICITUD = 0;
            }

            return ID_SOLICITUD;
        }



        public Int32 ObtenerNumRegSolicitudesPorTipDocAndNumDoc(String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD)
        {
            String sql = null;

            Int32 resultado = 0;

            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_porTipDocAndnumDoc ";

            #region validaciones

            if (String.IsNullOrEmpty(TIP_DOC_IDENTIDAD) == false)
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_IDENTIDAD no puede ser nulo.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo.";
                ejecutar = false;
            }

            #endregion validaciones

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
                    resultado = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                resultado = 0;
            }

            return resultado;
        }

        public Decimal AdicionarRegSolicitudesingreso(DateTime FECHA_R,
            String APELLIDOS,
            String NOMBRES,
            String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD,
            String CIU_CEDULA,
            String LIB_MILITAR,
            String CAT_LIC_COND,
            String DIR_ASPIRANTE,
            String SECTOR,
            String CIU_ASPIRANTE,
            String TEL_ASPIRANTE,
            String SEXO,
            DateTime FCH_NACIMIENTO,
            String ID_GRUPOS_PRIMARIOS,
            int ID_FUENTE,
            String CONDUCTO,
            int NIV_EDUCACION,
            String E_MAIL,
            int ID_AREASINTERES,
            Decimal ASPIRACION_SALARIAL,
            String EXPERIENCIA,
            Decimal ID_OCUPACION,
            String NUCLEO_FORMACION,
            String TALLA_CAMISA,
            String TALLA_PANTALON,
            String TALLA_ZAPATOS,
            int ESTRATO,
            int NRO_HIJOS,
            Boolean C_FMLIA,
            String CEL_ASPIRANTE,
            String ESTADO_CIVIL,
            Int32 ID_PAIS,
            String TIPO_VIVIENDA,
            String FUENTE_CONOCIMIENTO,
            List<FormacionAcademica> listaFormacionAcademica,
            List<ExperienciaLaboral> listaExperiencia,
            List<ComposicionFamiliar> listaComposicionFamiliar,
            String RH)
        {

            Boolean correcto = true;

            Decimal ID_SOLICITUD = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_SOLICITUD = AdicionarRegistroRegSolicitudIngreso(FECHA_R, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, ID_GRUPOS_PRIMARIOS, ID_FUENTE, CONDUCTO, NIV_EDUCACION, E_MAIL, ID_AREASINTERES, ASPIRACION_SALARIAL, EXPERIENCIA, ID_OCUPACION, NUCLEO_FORMACION, TALLA_CAMISA, TALLA_PANTALON, TALLA_ZAPATOS, ESTRATO, NRO_HIJOS, C_FMLIA, CEL_ASPIRANTE, ESTADO_CIVIL, ID_PAIS, TIPO_VIVIENDA, FUENTE_CONOCIMIENTO, RH, conexion);

                if (ID_SOLICITUD <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    ID_SOLICITUD = 0;
                }
                else
                {
                    hojasVida _hoja = new hojasVida(Empresa, Usuario);

                    DataTable tablaEntrevista = _hoja.ObtenerSelRegEntrevistasPorIdSolicitud(ID_SOLICITUD, conexion);

                    if (_hoja.MensajeError != null)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        ID_SOLICITUD = 0;
                    }
                    else
                    {
                        Decimal ID_ENTREVISTA;

                        if (tablaEntrevista.Rows.Count <= 0)
                        {
                            ID_ENTREVISTA = _hoja.AdicionarSelRegEntrevistas(ID_SOLICITUD, FECHA_R, "Ninguna", "Ninguna", "Ninguna", "Ninguna", conexion);

                            if (ID_ENTREVISTA <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_SOLICITUD = 0;
                                ID_ENTREVISTA = 0;
                            }
                        }
                        else
                        {
                            DataRow filaEntrevista = tablaEntrevista.Rows[0];
                            ID_ENTREVISTA = Convert.ToDecimal(filaEntrevista["REGISTRO"]);
                        }

                        if (correcto == true)
                        {
                            if (_hoja.ActualizarFormacionAcademicaEntrevistado(ID_ENTREVISTA, listaFormacionAcademica, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_SOLICITUD = 0;
                            }
                            else
                            {
                                if (_hoja.ActualizarExperienciaLaboralEntrevistado(ID_ENTREVISTA, listaExperiencia, conexion) == false)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_SOLICITUD = 0;
                                }
                                else
                                {
                                    if (_hoja.ActulizarComposicionFamiliarEntrevista(ID_ENTREVISTA, listaComposicionFamiliar, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        correcto = false;
                                        ID_SOLICITUD = 0;
                                    }
                                }
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                correcto = false;
                MensajeError = ex.Message;
                ID_SOLICITUD = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_SOLICITUD;
        }

        #region FUNCION ACTUALIZAR APUNTANDO A ID_OCUPACION
        #endregion


        public Boolean ActualizarRegistroRegSolicitudIngreso(Decimal ID_SOLICITUD,
            String APELLIDOS,
            String NOMBRES,
            String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD,
            String CIU_CEDULA,
            String LIB_MILITAR,
            String CAT_LIC_COND,
            String DIR_ASPIRANTE,
            String SECTOR,
            String CIU_ASPIRANTE,
            String TEL_ASPIRANTE,
            String SEXO,
            DateTime FCH_NACIMIENTO,
            String ID_GRUPOS_PRIMARIOS,
            int ID_FUENTE,
            String CONDUCTO,
            int NIV_EDUCACION,
            String E_MAIL,
            int ID_AREASINTERES,
            Decimal ASPIRACION_SALARIAL,
            String EXPERIENCIA,
            Decimal ID_OCUPACION,
            String NUCLEO_FORMACION,
            String TALLA_CAMISA,
            String TALLA_PANTALON,
            String TALLA_ZAPATOS,
            int ESTRATO,
            int NRO_HIJOS,
            Boolean C_FMLIA,
            String CEL_ASPIRANTE,
            String ESTADO_CIVIL,
            Int32 ID_PAIS,
            String TIPO_VIVIENDA,
            String FUENTE_CONOCIMIENTO,
            String RH,
            Conexion conexion)
        {
            Boolean correcto = true;

            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_solicitudes_ingreso_actualizar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "APELLIDOS = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NOMBRES = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TIP_DOC_IDENTIDAD)))
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIP_DOC_IDENTIDAD = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUM_DOC_IDENTIDAD = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CIU_CEDULA)))
            {
                sql += "'" + CIU_CEDULA + "', ";
                informacion += "CIU_CEDULA = '" + CIU_CEDULA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CIU_CEDULA = 'NULL', ";
            }

            if (SEXO.Equals("M"))
            {
                if (!(String.IsNullOrEmpty(LIB_MILITAR)))
                {
                    sql += "'" + LIB_MILITAR + "', ";
                    informacion += "LIB_MILITAR = '" + LIB_MILITAR + "', ";
                }
                else
                {
                    sql += "null, ";
                    informacion += "LIB_MILITAR = 'null', ";
                }
            }
            else
            {
                sql += "NULL, ";
                informacion += "LIB_MILITAR = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CAT_LIC_COND)))
            {
                sql += "'" + CAT_LIC_COND + "', ";
                informacion += "CAT_LIC_COND = '" + CAT_LIC_COND + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CAT_LIC_COND = 'null', ";
            }

            if (!(String.IsNullOrEmpty(DIR_ASPIRANTE)))
            {
                sql += "'" + DIR_ASPIRANTE + "', ";
                informacion += "DIR_ASPIRANTE = '" + DIR_ASPIRANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DIR_ASPIRANTE = 'null', ";
            }

            if (!(String.IsNullOrEmpty(SECTOR)))
            {
                sql += "'" + SECTOR + "', ";
                informacion += "SECTOR = '" + SECTOR + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "SECTOR = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CIU_ASPIRANTE)))
            {
                sql += "'" + CIU_ASPIRANTE + "', ";
                informacion += "CIU_ASPIRANTE = '" + CIU_ASPIRANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CIU_ASPIRANTE = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TEL_ASPIRANTE)))
            {
                sql += "'" + TEL_ASPIRANTE + "', ";
                informacion += "TEL_ASPIRANTE = '" + TEL_ASPIRANTE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "TEL_ASPIRANTE = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "', ";
                informacion += "SEXO = '" + SEXO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "SEXO = 'null', ";
            }

            if (FCH_NACIMIENTO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_NACIMIENTO) + "', ";
                informacion += "FCH_NACIMIENTO = '" + FCH_NACIMIENTO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_NACIMIENTO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(ID_GRUPOS_PRIMARIOS)))
            {
                sql += "'" + ID_GRUPOS_PRIMARIOS + "', ";
                informacion += "ID_GRUPOS_PRIMARIOS= '" + ID_GRUPOS_PRIMARIOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_GRUPOS_PRIMARIOS= 'null', ";
            }

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE= '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_FUENTE= 'null', ";
            }

            if (!(String.IsNullOrEmpty(CONDUCTO)))
            {
                sql += "'" + CONDUCTO + "', ";
                informacion += "CONDUCTO = '" + CONDUCTO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONDUCTO = 'NULL', ";
            }

            if (NIV_EDUCACION != 0)
            {
                sql += NIV_EDUCACION + ", ";
                informacion += "NIV_EDUCACION = '" + NIV_EDUCACION.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NIV_EDUCACION = 'null', ";
            }

            if (!(String.IsNullOrEmpty(E_MAIL)))
            {
                sql += "'" + E_MAIL + "', ";
                informacion += "E_MAIL= '" + E_MAIL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "E_MAIL= 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            sql += ID_AREASINTERES.ToString() + ", ";
            informacion += "ID_AREASINTERES= '" + ID_AREASINTERES.ToString() + "', ";

            if (ASPIRACION_SALARIAL != 0)
            {
                sql += ASPIRACION_SALARIAL.ToString().Replace(",", ".") + ", ";
                informacion += "ASPIRACION_SALARIAL= '" + ASPIRACION_SALARIAL.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ASPIRACION_SALARIAL= 'null', ";
            }

            if (!(String.IsNullOrEmpty(EXPERIENCIA)))
            {
                sql += "'" + EXPERIENCIA + "', ";
                informacion += "EXPERIENCIA = '" + EXPERIENCIA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "EXPERIENCIA = 'null', ";
            }

            if (ID_OCUPACION != 0)
            {
                sql += ID_OCUPACION.ToString() + ", ";
                informacion += "ID_OCUPACION= '" + ID_OCUPACION.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_OCUPACION = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NUCLEO_FORMACION)))
            {
                sql += "'" + NUCLEO_FORMACION + "', ";
                informacion += "NUCLEO_FORMACION = '" + NUCLEO_FORMACION + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUCLEO_FORMACION = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TALLA_CAMISA)))
            {
                sql += "'" + TALLA_CAMISA + "', ";
                informacion += "TALLA_CAMISA = '" + TALLA_CAMISA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA_CAMISA = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TALLA_PANTALON)))
            {
                sql += "'" + TALLA_PANTALON + "', ";
                informacion += "TALLA_PANTALON = '" + TALLA_PANTALON + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA_PANTALON = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TALLA_ZAPATOS)))
            {
                sql += "'" + TALLA_ZAPATOS + "', ";
                informacion += "TALLA_ZAPATOS = '" + TALLA_ZAPATOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA_ZAPATOS = 'null', ";
            }

            sql += ESTRATO + ", ";
            informacion += "ESTRATO = '" + ESTRATO + "', ";

            sql += NRO_HIJOS + ", ";
            informacion += "NRO_HIJOS = '" + NRO_HIJOS + "', ";

            if (C_FMLIA == false)
            {
                sql += "'N', ";
                informacion += "C_FMLIA = 'N', ";
            }
            else
            {
                sql += "'S', ";
                informacion += "C_FMLIA = 'S', ";
            }

            if (String.IsNullOrEmpty(CEL_ASPIRANTE) == false)
            {
                sql += "'" + CEL_ASPIRANTE + "', ";
                informacion += "CEL_ASPIRANTE = '" + CEL_ASPIRANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CEL_ASPIRANTE = 'null', ";
            }

            if (String.IsNullOrEmpty(ESTADO_CIVIL) == false)
            {
                sql += "'" + ESTADO_CIVIL + "', ";
                informacion += "ESTADO_CIVIL = '" + ESTADO_CIVIL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ESTADO_CIVIL = 'null', ";
            }

            if (ID_PAIS != 0)
            {
                sql += ID_PAIS + ", ";
                informacion += "ID_PAIS = '" + ID_PAIS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_PAIS = 'null', ";
            }

            if (String.IsNullOrEmpty(TIPO_VIVIENDA) == false)
            {
                sql += "'" + TIPO_VIVIENDA + "', ";
                informacion += "TIPO_VIVIENDA = '" + TIPO_VIVIENDA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_VIVIENDA = 'null', ";
            }

            if (String.IsNullOrEmpty(FUENTE_CONOCIMIENTO) == false)
            {
                sql += "'" + FUENTE_CONOCIMIENTO + "', ";
                informacion += "FUENTE_CONOCIMIENTO = '" + FUENTE_CONOCIMIENTO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FUENTE_CONOCIMIENTO = 'null', ";
            }

            if (String.IsNullOrEmpty(RH) == false)
            {
                sql += "'" + RH + "'";
                informacion += "RH = '" + RH + "'";
            }
            else
            {
                sql += "null";
                informacion += "RH = 'null'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    correcto = false;
                }
            }
            else
            {
                correcto = false;
            }

            return correcto;
        }


        public Boolean ActualizarRegSolicitudesingreso(Decimal ID_SOLICITUD,
            String APELLIDOS,
            String NOMBRES,
            String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD,
            String CIU_CEDULA,
            String LIB_MILITAR,
            String CAT_LIC_COND,
            String DIR_ASPIRANTE,
            String SECTOR,
            String CIU_ASPIRANTE,
            String TEL_ASPIRANTE,
            String SEXO,
            DateTime FCH_NACIMIENTO,
            String ID_GRUPOS_PRIMARIOS,
            int ID_FUENTE,
            String CONDUCTO,
            int NIV_EDUCACION,
            String E_MAIL,
            int ID_AREASINTERES,
            Decimal ASPIRACION_SALARIAL,
            String EXPERIENCIA,
            Decimal ID_OCUPACION,
            String NUCLEO_FORMACION,
            String TALLA_CAMISA,
            String TALLA_PANTALON,
            String TALLA_ZAPATOS,
            int ESTRATO,
            int NRO_HIJOS,
            Boolean C_FMLIA,
            String CEL_ASPIRANTE,
            String ESTADO_CIVIL,
            Int32 ID_PAIS,
            String TIPO_VIVIENDA,
            String FUENTE_CONOCIMIENTO,
            List<FormacionAcademica> listaFormacionAcademica,
            List<ExperienciaLaboral> listaExperiencia,
            List<ComposicionFamiliar> listaComposicionFamiliar,
            String RH)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarRegistroRegSolicitudIngreso(ID_SOLICITUD, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, ID_GRUPOS_PRIMARIOS, ID_FUENTE, CONDUCTO, NIV_EDUCACION, E_MAIL, ID_AREASINTERES, ASPIRACION_SALARIAL, EXPERIENCIA, ID_OCUPACION, NUCLEO_FORMACION, TALLA_CAMISA, TALLA_PANTALON, TALLA_ZAPATOS, ESTRATO, NRO_HIJOS, C_FMLIA, CEL_ASPIRANTE, ESTADO_CIVIL, ID_PAIS, TIPO_VIVIENDA, FUENTE_CONOCIMIENTO, RH, conexion) == false)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    hojasVida _hoja = new hojasVida(Empresa, Usuario);

                    DataTable tablaEntrevista = _hoja.ObtenerSelRegEntrevistasPorIdSolicitud(ID_SOLICITUD, conexion);

                    if (_hoja.MensajeError != null)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                    else
                    {
                        Decimal ID_ENTREVISTA;

                        if (tablaEntrevista.Rows.Count <= 0)
                        {
                            ID_ENTREVISTA = _hoja.AdicionarSelRegEntrevistas(ID_SOLICITUD, DateTime.Now, "Ninguna", "Ninguna", "Ninguna", "Ninguna", conexion);

                            if (ID_ENTREVISTA <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                            }
                        }
                        else
                        {
                            DataRow filaEntrevista = tablaEntrevista.Rows[0];
                            ID_ENTREVISTA = Convert.ToDecimal(filaEntrevista["REGISTRO"]);
                        }

                        if (correcto == true)
                        {
                            if (_hoja.ActualizarFormacionAcademicaEntrevistado(ID_ENTREVISTA, listaFormacionAcademica, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                            }
                            else
                            {
                                if (_hoja.ActualizarExperienciaLaboralEntrevistado(ID_ENTREVISTA, listaExperiencia, conexion) == false)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                }
                                else
                                {
                                    if (_hoja.ActulizarComposicionFamiliarEntrevista(ID_ENTREVISTA, listaComposicionFamiliar, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        correcto = false;
                                    }
                                }
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                correcto = false;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public Boolean ActualizarRegSolicitudesingreso(Decimal ID_SOLICITUD,
            String APELLIDOS,
            String NOMBRES,
            String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD,
            String CIU_CEDULA,
            String LIB_MILITAR,
            String CAT_LIC_COND,
            String DIR_ASPIRANTE,
            String SECTOR,
            String CIU_ASPIRANTE,
            String TEL_ASPIRANTE,
            String SEXO,
            DateTime FCH_NACIMIENTO,
            String E_MAIL,
            Decimal SALARIO,
            Decimal ID_EMPLEADO,
            Boolean ACTUALIZAR_ESTADO_PROCESO)
        {
            Decimal ID_AUDITORIA = 0;
            Boolean ejecutar = true;

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (ActualizarSolicitudAuditoria(ID_SOLICITUD, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, E_MAIL, SALARIO, conexion) == true)
                    {
                        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Empresa, Usuario);

                        ID_AUDITORIA = _auditoriaContratos.AdicionarAuditoriaContratos(ID_EMPLEADO, tabla.REG_SOLICITUDES_INGRESO, ID_SOLICITUD, DateTime.Now, conexion);

                        if (ID_AUDITORIA <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            ejecutar = false;
                            MensajeError = _auditoriaContratos.MensajeError;
                        }
                        else
                        {
                            if (ACTUALIZAR_ESTADO_PROCESO == true)
                            {
                                if (ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_REG_SOLICITUDES_INGRESO, conexion) == false)
                                {
                                    conexion.DeshacerTransaccion();
                                    ejecutar = false;
                                }
                                else
                                {
                                    conexion.AceptarTransaccion();
                                }
                            }
                            else
                            {
                                conexion.AceptarTransaccion();
                            }
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        ejecutar = false;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return ejecutar;
        }

        public Boolean ActualizarSolicitudAuditoria(Decimal ID_SOLICITUD,
            String APELLIDOS,
            String NOMBRES,
            String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD,
            String CIU_CEDULA,
            String LIB_MILITAR,
            String CAT_LIC_COND,
            String DIR_ASPIRANTE,
            String SECTOR,
            String CIU_ASPIRANTE,
            String TEL_ASPIRANTE,
            String SEXO,
            DateTime FCH_NACIMIENTO,
            String E_MAIL,
            Decimal SALARIO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_solicitudes_ingreso_actualizar_desde_auditoria ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIP_DOC_IDENTIDAD)))
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NUMERO DOCUMENTO IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_CEDULA)))
            {
                sql += "'" + CIU_CEDULA + "', ";
                informacion += "CIU_CEDULA = '" + CIU_CEDULA + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD DE EXPEDICION DE LA CEDULA no puede ser nulo\n";
                ejecutar = false;
            }

            if (SEXO.Equals("M"))
            {
                if (!(String.IsNullOrEmpty(LIB_MILITAR)))
                {
                    sql += "'" + LIB_MILITAR + "', ";
                    informacion += "LIB_MILITAR = '" + LIB_MILITAR + "', ";
                }
                else
                {
                    MensajeError += "El campo LIBRETA MILITAR no puede ser nulo\n";
                    ejecutar = false;
                }
            }
            else
            {
                sql += "NULL, ";
                informacion += "LIB_MILITAR = NULL, ";
            }

            if (!(String.IsNullOrEmpty(CAT_LIC_COND)))
            {
                sql += "'" + CAT_LIC_COND + "', ";
                informacion += "CAT_LIC_COND = '" + CAT_LIC_COND + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CAT_LIC_COND = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(DIR_ASPIRANTE)))
            {
                sql += "'" + DIR_ASPIRANTE + "', ";
                informacion += "DIR_ASPIRANTE = '" + DIR_ASPIRANTE + "', ";
            }
            else
            {
                MensajeError += "El campo DIR_ASPIRANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SECTOR)))
            {
                sql += "'" + SECTOR + "', ";
                informacion += "SECTOR = '" + SECTOR + "', ";
            }
            else
            {
                MensajeError += "El campo SECTOR no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_ASPIRANTE)))
            {
                sql += "'" + CIU_ASPIRANTE + "', ";
                informacion += "CIU_ASPIRANTE = '" + CIU_ASPIRANTE + "', ";
            }
            else
            {
                MensajeError += "El campo CIU_ASPIRANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_ASPIRANTE)))
            {
                sql += "'" + TEL_ASPIRANTE + "', ";
                informacion += "TEL_ASPIRANTE = '" + TEL_ASPIRANTE + "', ";
            }
            else
            {
                MensajeError += "El campo TEL_ASPIRANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "', ";
                informacion += "SEXO = '" + SEXO + "', ";
            }
            else
            {
                MensajeError += "El campo SEXO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FCH_NACIMIENTO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_NACIMIENTO) + "', ";
                informacion += "FCH_NACIMIENTO = '" + FCH_NACIMIENTO + "', ";
            }
            else
            {
                MensajeError += "El campo FCH_NACIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(E_MAIL)))
            {
                sql += "'" + E_MAIL + "', ";
                informacion += "E_MAIL= '" + E_MAIL + "', ";
            }
            else
            {
                MensajeError += "El campo E_MAIL no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (SALARIO != 0)
            {
                sql += SALARIO;
                informacion += "ASPIRACION_SALARIAL = '" + SALARIO + "'";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
            }

            if (cantidadRegistrosActualizados > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean ActualizarEstadoSolicitud(Decimal ID_SOLICITUD)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_actualizar_estado_diponible ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString();
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString();
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }


        public Boolean ActualizarEstadoSolicitud(Decimal ID_SOLICITUD, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_actualizar_estado_diponible ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString();
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString();
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }




        public DataTable ObtenerRegSolicitudesingresoPorNumDocIdentidad(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorNumDocIdentidad_Reclutamiento(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }


        public DataTable ObtenerRegSolicitudesingresoPorNumDocIdentidadOmitiendoIdSolicitud(String NUM_DOC_IDENTIDAD,
            Decimal ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad_y_omitiendo_id_solicitud ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
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

            return _dataTable;
        }



        public DataTable ObtenerRegSolicitudesingresoPorNumDocIdentidadSoloSiDisponible(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad_disponible ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            _dataTable.Columns.Add("RESTRICCIONES_JURIDICAS");
            DataRow _datarow;
            Boolean verificador = false;
            String textoRestriccion = "";
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                verificador = false;
                textoRestriccion = "";
                _datarow = _dataTable.Rows[i];

                if (_datarow["TUTELAS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por tutela.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por tutela.";
                    }
                    verificador = true;
                }

                if (_datarow["DEMANDAS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por demandas.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por demandas.";
                    }
                    verificador = true;
                }

                if (_datarow["DERECHOS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por derechos de petición.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por derechos de petición.";
                    }
                    verificador = true;
                }

                if (verificador == false)
                {
                    textoRestriccion = "Ninguna";
                }

                _datarow["RESTRICCIONES_JURIDICAS"] = textoRestriccion;
            }

            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorApellidos(String APELLIDOS)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_apellidos ";

            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorNombres(String NOMBRES)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_nombres ";

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorNumDocIdentidadValAcoset(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            acoset _acoset = new acoset(Empresa, Usuario);

            if (_acoset.ObtenerRegAcosetPorNumeroID(NUM_DOC_IDENTIDAD).Rows.Count == 0)
            {
                if (ObtenerRegSolicitudesingresoPorNumDocIdentidadDescartadoMalo(NUM_DOC_IDENTIDAD).Rows.Count == 0)
                {
                    sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad_para_radicacion_hoja_vida ";

                    if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
                    {
                        sql += "'" + NUM_DOC_IDENTIDAD.Trim() + "'";
                        informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "'";
                    }
                    else
                    {
                        MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                        ejecutar = false;
                    }

                    if (ejecutar)
                    {
                        Conexion conexion = new Conexion(Empresa);
                        try
                        {
                            _dataSet = conexion.ExecuteReader(sql);
                            _dataView = _dataSet.Tables[0].DefaultView;
                            _dataTable = _dataView.Table;

                            #region auditoria
                            auditoria _auditoria = new auditoria(Empresa);
                            _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                            #endregion auditoria
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
                }
                else
                {
                    MensajeError = "El aspirante fue rechazado de forma negativa con anterioridad.";
                }

            }
            else
            {
                MensajeError = "Descartado limitante comuníquese con su jefe inmediato.";
            }

            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorNumDocIdentidadDescartadoMalo(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad_descartado_malo ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += NUM_DOC_IDENTIDAD.Trim();
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }

        public DataTable ObtenerAspirantesPorNumDocIdentidad(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_aspirantes_obtener_por_doc_identidad ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += NUM_DOC_IDENTIDAD.Trim();
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SERTEMPO_2_Recep_Aspiran_dbo_REG_ASPIRANTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorFiltroRequisicion(Decimal REGISTRO_PERFIL,
            Int32 EDAD_MAX,
            Int32 EDAD_MIN,
            String NIV_EDUCACION,
            String CIU_ASPIRANTE,
            String SEXO,
            String EXPERIENCIA,
            String FILTRO_CARGO,
            String FILTRO_CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_filtro_Requisicion ";

            if (REGISTRO_PERFIL != 0)
            {
                sql += REGISTRO_PERFIL + ", ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (EDAD_MAX != 0)
            {
                sql += "'" + EDAD_MAX + "', ";
            }
            else
            {
                MensajeError += "El campo EDAD_MAX no puede ser nulo\n";
                ejecutar = false;
            }

            if (EDAD_MIN != 0)
            {
                sql += "'" + EDAD_MIN + "', ";
            }
            else
            {
                MensajeError += "El campo EDAD_MIN no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NIV_EDUCACION) == false)
            {
                sql += "'" + NIV_EDUCACION + "', ";
            }
            else
            {
                MensajeError += "El campo NIV_EDUCACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_ASPIRANTE)))
            {
                sql += "'" + CIU_ASPIRANTE + "',";
            }
            else
            {
                MensajeError += "El campo CIU_ASPIRANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "', ";
            }
            else
            {
                MensajeError += "El campo SEXO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EXPERIENCIA)))
            {
                sql += "'" + EXPERIENCIA + "', "; ;
            }
            else
            {
                MensajeError += "El campo EXPERIENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FILTRO_CARGO)))
            {
                sql += "'" + FILTRO_CARGO + "', ";
            }
            else
            {
                MensajeError += "El campo FILTRO_CARGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FILTRO_CIUDAD)))
            {
                sql += "'" + FILTRO_CIUDAD + "'";
            }
            else
            {
                MensajeError += "El campo FILTRO_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
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

            _dataTable.Columns.Add("RESTRICCIONES_JURIDICAS");
            DataRow _datarow;
            Boolean verificador = false;
            String textoRestriccion = "";
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                verificador = false;
                textoRestriccion = "";
                _datarow = _dataTable.Rows[i];

                if (_datarow["TUTELAS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por tutela.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por tutela.";
                    }
                    verificador = true;
                }

                if (_datarow["DEMANDAS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por demandas.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por demandas.";
                    }
                    verificador = true;
                }

                if (_datarow["DERECHOS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por derechos de petición.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por derechos de petición.";
                    }
                    verificador = true;
                }

                if (verificador == false)
                {
                    textoRestriccion = "Ninguna";
                }

                _datarow["RESTRICCIONES_JURIDICAS"] = textoRestriccion;
            }

            return _dataTable;
        }

        public DataTable ObtenerAreasInteresLaboral()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_AREAS_INTERES_LABORAL ";


            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SERTEMPO_2_Recep_Aspiran_dbo_AREAS_INTERES_LABORAL, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorPerfil(int REGISTRO, String CIU_ASPIRANTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_Id_Perfil ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CIU_ASPIRANTE)))
            {
                sql += "'" + CIU_ASPIRANTE + "'";
                informacion += "CIU_ASPIRANTE = '" + CIU_ASPIRANTE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo CIU_ASPIRANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            _dataTable.Columns.Add("RESTRICCIONES_JURIDICAS");
            DataRow _datarow;
            Boolean verificador = false;
            String textoRestriccion = "";
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                verificador = false;
                textoRestriccion = "";
                _datarow = _dataTable.Rows[i];

                if (_datarow["TUTELAS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por tutela.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por tutela.";
                    }
                    verificador = true;
                }

                if (_datarow["DEMANDAS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por demandas.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por demandas.";
                    }
                    verificador = true;
                }

                if (_datarow["DERECHOS"].ToString().Trim() == "SI")
                {
                    if (verificador == false)
                    {
                        textoRestriccion = "Tiene restricción por derechos de petición.";
                    }
                    else
                    {
                        textoRestriccion += "</br>Tiene restricción por derechos de petición.";
                    }
                    verificador = true;
                }

                if (verificador == false)
                {
                    textoRestriccion = "Ninguna";
                }

                _datarow["RESTRICCIONES_JURIDICAS"] = textoRestriccion;
            }

            return _dataTable;
        }

        public Boolean ActualizarEstadoRegSolicitudesIngreso(int ID_REQUERIMIENTO, int ID_SOLICITUD, String ESTADO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_actualizar_estado_reg_solicitudes_ingreso ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += " '" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarEstadoRegSolicitudesIngreso(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD, String ESTADO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_actualizar_estado_reg_solicitudes_ingreso ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_REQUERIMIENTO= 'null', ";
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += " '" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {

                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarEstadoFechaIngresoYSueldoIngresoContratoRegSolicitudesIngreso(int ID_REQUERIMIENTO, int ID_SOLICITUD, String ESTADO, DateTime F_ING_C, Decimal SUELDO_C)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_actualizar_estado_y_fecha_contratacion_reg_solicitudes_ingreso ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += " '" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(F_ING_C) + "', ";
            informacion += "F_ING_C = '" + F_ING_C.ToString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (SUELDO_C != 0)
            {
                sql += SUELDO_C.ToString().Replace(',', '.');
                informacion += "SUELDO_C= '" + SUELDO_C.ToString().Replace(',', '.') + "'";
            }
            else
            {
                MensajeError += "El campo SUELDO_C no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerRegSolicitudesingresoPorIdRequisicionEnCliente(int ID_REQUISICION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_por_Id_requisicion_en_cliente ";

            if (ID_REQUISICION != 0)
            {
                sql += ID_REQUISICION + ", ";
                informacion += "ID_REQUISICION = '" + ID_REQUISICION.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUISICION no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }

        public Boolean ActualizarEstadoProcesoRegSolicitudesIngreso(int ID_REQUERIMIENTO, int ID_SOLICITUD, String ESTADO_PROCESO, String USU_PROCESO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_actualizar_estado_proceso_solicitud_ingreso ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO_PROCESO)))
            {
                sql += " '" + ESTADO_PROCESO + "',";
                informacion += "ESTADO_PROCESO= '" + ESTADO_PROCESO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ESTADO_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(USU_PROCESO)))
            {
                sql += " '" + USU_PROCESO + "',";
                informacion += "USU_PROCESO= '" + USU_PROCESO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo USU_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";


            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(Decimal ID_SOLICITUD, String ESTADO_PROCESO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_actualizar_estado_proceso_solicitud_ingreso_auditoria ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO_PROCESO)))
            {
                sql += " '" + ESTADO_PROCESO + "',";
                informacion += "ESTADO_PROCESO= '" + ESTADO_PROCESO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ESTADO_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarEstadoProcesoRegSolicitudesIngreso(int ID_REQUERIMIENTO, int ID_SOLICITUD, String ESTADO_PROCESO, String USU_PROCESO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_actualizar_estado_proceso_solicitud_ingreso ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO_PROCESO)))
            {
                sql += " '" + ESTADO_PROCESO + "',";
                informacion += "ESTADO_PROCESO= '" + ESTADO_PROCESO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ESTADO_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(USU_PROCESO)))
            {
                sql += " '" + USU_PROCESO + "',";
                informacion += "USU_PROCESO= '" + USU_PROCESO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo USU_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";


            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerRegSolicitudesingresoPorIdSolicitud(int ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_id ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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
            return _dataTable;
        }

        public DataTable ObtenerRegSolicitudesingresoPorIdSolicitud(Decimal ID_SOLICITUD, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_id ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }

        public Boolean ActualizarEntidadNumCuenta(int ID_SOLICITUD, int ID_ENTIDAD, String NUM_CUENTA, String FORMA_PAGO, String TIPO_CUENTA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_actualizar_entidad_numeroCuenta ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString() + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString();
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD.ToString() + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD.ToString();
            }
            else
            {
                sql += ID_ENTIDAD.ToString() + ", ";
                informacion += "ID_ENTIDAD= '0', ";
            }

            if (!(String.IsNullOrEmpty(NUM_CUENTA)))
            {
                sql += "'" + NUM_CUENTA.ToString() + "', ";
                informacion += "NUM_CUENTA = '" + NUM_CUENTA.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "NUM_CUENTA = 'null', ";
            }

            sql += "'" + Usuario + "',";
            informacion += "USU_MOD = '" + Usuario.ToString() + "',";

            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += "'" + FORMA_PAGO.ToString() + "', ";
                informacion += "FORMA_PAGO = '" + FORMA_PAGO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FORMA_PAGO = 'null', ";
            }
            if (!(String.IsNullOrEmpty(TIPO_CUENTA)))
            {
                sql += "'" + TIPO_CUENTA.ToString() + "' ";
                informacion += "TIPO_CUENTA = '" + TIPO_CUENTA.ToString() + "' ";
            }
            else
            {
                sql += "NULL ";
                informacion += "TIPO_CUENTA = 'null' ";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }


        public Boolean ActualizarEntidadNumCuenta(Decimal ID_SOLICITUD, Decimal ID_ENTIDAD, String NUM_CUENTA, String FORMA_PAGO, String TIPO_CUENTA, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_actualizar_entidad_numeroCuenta ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString() + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString();
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD.ToString() + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD.ToString();
            }
            else
            {
                sql += ID_ENTIDAD.ToString() + ", ";
                informacion += "ID_ENTIDAD= '0', ";
            }

            if (!(String.IsNullOrEmpty(NUM_CUENTA)))
            {
                sql += "'" + NUM_CUENTA.ToString() + "', ";
                informacion += "NUM_CUENTA = '" + NUM_CUENTA.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUM_CUENTA = 'null', ";
            }

            sql += "'" + Usuario + "',";
            informacion += "USU_MOD = '" + Usuario.ToString() + "',";

            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += "'" + FORMA_PAGO.ToString() + "', ";
                informacion += "FORMA_PAGO = '" + FORMA_PAGO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FORMA_PAGO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(TIPO_CUENTA)))
            {
                sql += "'" + TIPO_CUENTA.ToString() + "' ";
                informacion += "TIPO_CUENTA = '" + TIPO_CUENTA.ToString() + "' ";
            }
            else
            {
                sql += "NULL";
                informacion += "TIPO_CUENTA = 'null'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerRegSolicitudesingresoPorIdRequisicionContratado(int ID_REQUISICION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_por_Id_requisicion_contratadas ";

            if (ID_REQUISICION != 0)
            {
                sql += ID_REQUISICION + ", ";
                informacion += "ID_REQUISICION = '" + ID_REQUISICION.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUISICION no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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
            return _dataTable;
        }

        public DataTable ObtenerTemporalidad(int ID_EMPRESA, int ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_temporalidad ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ",";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

            return _dataTable;
        }


        public Boolean ActualizarRequisicionSolicitud(Decimal ID_SOLICITUD, Decimal ID_REQUISICION)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_actualiza_requisicion ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString() + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUISICION != 0)
            {
                sql += ID_REQUISICION.ToString();
                informacion += "ID_REQUISICION= '" + ID_REQUISICION.ToString();
            }
            else
            {
                MensajeError += "El campo ID_REQUISICION no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }



        public Boolean ActualizarRequisicionSolicitud(Decimal ID_SOLICITUD, Decimal ID_REQUISICION, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_actualiza_requisicion ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString() + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUISICION != 0)
            {
                sql += ID_REQUISICION.ToString();
                informacion += "ID_REQUISICION= '" + ID_REQUISICION.ToString();
            }
            else
            {
                MensajeError += "El campo ID_REQUISICION no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }



        #endregion solicitudesIngreso

        #endregion metodos

        public void ActualizarDataDeSeguridadDeAcceso(String NUM_DOC_IDENTIDAD, String PASSWORD, String PALABRACLAVE, String EMILE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "dbo.usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad_Actualiza ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', '" + PASSWORD + "', '" + PALABRACLAVE + "', '" + EMILE + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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
        }

    }
}
