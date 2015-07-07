using System;
using System.Collections.Generic;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class hojasVida
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
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
        #endregion propiedades

        #region constructores
        public hojasVida(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region METODOS

        #region ENTREVISTAS

        public Decimal AdicionarSelRegEntrevistas(Decimal ID_SOLICITUD,
            DateTime FCH_ENTREVISTA,
            String COM_C_FAM,
            String COM_F_LAB,
            String COM_C_ACA,
            String COM_C_GEN,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Decimal registro = 0;

            tools _tools = new tools();

            sql = "usp_sel_reg_entrevistas_adicionar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_ENTREVISTA) + "', ";
            informacion += "FCH_ENTREVISTA = '" + FCH_ENTREVISTA + "', ";

            sql += "'" + COM_C_FAM + "', ";
            informacion += "COM_C_FAM = '" + COM_C_FAM + "', ";

            sql += "'" + COM_F_LAB + "', ";
            informacion += "COM_F_LAB = '" + COM_F_LAB + "', ";

            sql += "'" + COM_C_ACA + "', ";
            informacion += "COM_C_ACA = '" + COM_C_ACA + "', ";

            sql += "'" + COM_C_GEN + "', ";
            informacion += "COM_C_GEN = '" + COM_C_GEN + "', ";

            sql += "'" + Usuario + "'";
            informacion += "Usuario = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_ENTREVISTAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }

            return registro;
        }

        public Boolean InhabilitarComposicionFamiliarEntrevista(Decimal ID_COMPOSICION,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_composicion_familiar_inhabilitar ";

            #region validaciones

            if (ID_COMPOSICION != 0)
            {
                sql += ID_COMPOSICION + ", ";
                informacion += "ID_COMPOSICION = '" + ID_COMPOSICION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_COMPOSICION no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_COMPOSICION_FAMILIAR, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }

        public Boolean InhabilitarFormacionAcademicaEntrevista(Decimal ID_INFO_ACADEMICA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_informacion_academica_inhabilitar ";

            #region validaciones

            if (ID_INFO_ACADEMICA != 0)
            {
                sql += ID_INFO_ACADEMICA + ", ";
                informacion += "ID_INFO_ACADEMICA = '" + ID_INFO_ACADEMICA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_INFO_ACADEMICA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_INFORMACION_ACADEMICA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }

        public Boolean InhabilitarExperienciaLaboralEntrevista(Decimal ID_EXPERIENCIA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_experiencia_laboral_inhabilitar ";

            #region validaciones

            if (ID_EXPERIENCIA != 0)
            {
                sql += ID_EXPERIENCIA + ", ";
                informacion += "ID_EXPERIENCIA = '" + ID_EXPERIENCIA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EXPERIENCIA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_EXPERIENCIA_LABORAL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }


        public Boolean ActualizarComposicionFamiliarEntrevista(Decimal ID_COMPOSICION,
            String ID_TIPO_FAMILIAR,
            String NOMBRES,
            String APELLIDOS,
            DateTime FECHA_NACIMIENTO,
            String PROFESION,
            String ID_CIUDAD,
            Boolean VIVE_CON_EL,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_composicion_familiar_actualizar ";

            #region validaciones

            if (ID_COMPOSICION != 0)
            {
                sql += ID_COMPOSICION + ", ";
                informacion += "ID_COMPOSICION = '" + ID_COMPOSICION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_COMPOSICION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_TIPO_FAMILIAR) == false)
            {
                sql += "'" + ID_TIPO_FAMILIAR + "', ";
                informacion += "ID_TIPO_FAMILIAR = '" + ID_TIPO_FAMILIAR + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_TIPO_FAMILIAR = 'NULL', ";
            }


            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NOMBRES = 'NULL', ";
            }

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "APELLIDOS = 'NULL', ";
            }

            if (FECHA_NACIMIENTO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NACIMIENTO) + "', ";
                informacion += "FECHA_NACIMIENTO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NACIMIENTO) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_NACIMIENTO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(PROFESION) == false)
            {
                sql += "'" + PROFESION + "', ";
                informacion += "PROFESION = '" + PROFESION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PROFESION = 'NULL', ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CIUDAD = 'NULL', ";
            }

            if (VIVE_CON_EL == true)
            {
                sql += "'True', ";
                informacion += "VIVE_CON_EL = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "VIVE_CON_EL = 'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_COMPOSICION_FAMILIAR, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }

        public Boolean ActualizarInformacionAcademicaEntrevista(Decimal ID_INFO_ACADEMICA,
            String NIVEL_ACADEMICO,
            String INSTITUCION,
            Int32 ANNO,
            String OBSERVACIONES,
            String CURSO,
            Decimal DURACION,
            String UNIDAD_DURACION,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_informacion_academica_actualizar ";

            #region validaciones

            if (ID_INFO_ACADEMICA != 0)
            {
                sql += ID_INFO_ACADEMICA + ", ";
                informacion += "ID_INFO_ACADEMICA = '" + ID_INFO_ACADEMICA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_INFO_ACADEMICA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NIVEL_ACADEMICO) == false)
            {
                sql += "'" + NIVEL_ACADEMICO + "', ";
                informacion += "NIVEL_ACADEMICO = '" + NIVEL_ACADEMICO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NIVEL_ACADEMICO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(INSTITUCION) == false)
            {
                sql += "'" + INSTITUCION + "', ";
                informacion += "INSTITUCION = '" + INSTITUCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "INSTITUCION = 'NULL', ";
            }

            if (ANNO != 0)
            {
                sql += ANNO + ", ";
                informacion += "ANNO = '" + ANNO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ANNO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBSERVACIONES = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CURSO) == false)
            {
                sql += "'" + CURSO + "', ";
                informacion += "CURSO = '" + CURSO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CURSO = 'NULL', ";
            }

            if (DURACION != 0)
            {
                sql += DURACION.ToString().Replace(',', '.') + ", ";
                informacion += "DURACION = '" + DURACION.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DURACION = 'NULL', ";
            }

            if (String.IsNullOrEmpty(UNIDAD_DURACION) == false)
            {
                sql += "'" + UNIDAD_DURACION + "', ";
                informacion += "UNIDAD_DURACION = '" + UNIDAD_DURACION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "UNIDAD_DURACION = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_INFORMACION_ACADEMICA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }

        public Boolean ActualizarExperienciaLAboralEntrevista(Decimal ID_EXPERIENCIA,
            String EMPRESA,
            String CARGO,
            String FUNCIONES,
            DateTime FECHA_INGRESO,
            DateTime FECHA_RETIRO,
            String MOTIVO_RETIRO,
            Decimal ULTIMO_SALARIO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_experiencia_laboral_actualizar ";

            #region validaciones

            if (ID_EXPERIENCIA != 0)
            {
                sql += ID_EXPERIENCIA + ", ";
                informacion += "ID_EXPERIENCIA = '" + ID_EXPERIENCIA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EXPERIENCIA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(EMPRESA) == false)
            {
                sql += "'" + EMPRESA + "', ";
                informacion += "EMPRESA = '" + EMPRESA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "EMPRESA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CARGO) == false)
            {
                sql += "'" + CARGO + "', ";
                informacion += "CARGO = '" + CARGO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CARGO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(FUNCIONES) == false)
            {
                sql += "'" + FUNCIONES + "', ";
                informacion += "FUNCIONES = '" + FUNCIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FUNCIONES = 'NULL', ";
            }

            if (FECHA_INGRESO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INGRESO) + "', ";
                informacion += "FECHA_INGRESO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INGRESO) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_INGRESO = 'NULL', ";
            }

            if (FECHA_RETIRO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RETIRO) + "', ";
                informacion += "FECHA_RETIRO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RETIRO) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_RETIRO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(MOTIVO_RETIRO) == false)
            {
                sql += "'" + MOTIVO_RETIRO + "', ";
                informacion += "MOTIVO_RETIRO = '" + MOTIVO_RETIRO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "MOTIVO_RETIRO = 'NULL', ";
            }

            if (ULTIMO_SALARIO != 0)
            {
                sql += ULTIMO_SALARIO.ToString().Replace(',', '.') + ", ";
                informacion += "ULTIMO_SALARIO = '" + ULTIMO_SALARIO.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ULTIMO_SALARIO = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_EXPERIENCIA_LABORAL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }

        public Decimal AdicionarComposicionFamiliarEntrevista(Decimal REGISTRO_ENTREVISTA,
            String ID_TIPO_FAMILIAR,
            String NOMBRES,
            String APELLIDOS,
            DateTime FECHA_NACIMIENTO,
            String PROFESION,
            String ID_CIUDAD,
            Boolean VIVE_CON_EL,
            Conexion conexion)
        {

            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_COMPOSICION = 0;

            sql = "usp_sel_reg_composicion_familiar_adicionar ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA + ", ";
                informacion += "REGISTRO_ENTREVISTA = '" + REGISTRO_ENTREVISTA + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_TIPO_FAMILIAR) == false)
            {
                sql += "'" + ID_TIPO_FAMILIAR + "', ";
                informacion += "ID_TIPO_FAMILIAR = '" + ID_TIPO_FAMILIAR + "', ";
            }
            else
            {
                MensajeError += "El campo ID_TIPO_FAMILIAR no puede ser nulo\n";
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

            if (FECHA_NACIMIENTO == new DateTime())
            {
                sql += "null, ";
                informacion += "FECHA_NACIMIENTO = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NACIMIENTO) + "', ";
                informacion += "FECHA_NACIMIENTO = '" + FECHA_NACIMIENTO.ToShortDateString() + "', ";
            }

            if (String.IsNullOrEmpty(PROFESION) == false)
            {
                sql += "'" + PROFESION + "', ";
                informacion += "PROFESION = '" + PROFESION + "', ";
            }
            else
            {
                MensajeError += "El campo PROFESION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CIUDAD = 'NULL', ";
            }

            if (VIVE_CON_EL == true)
            {
                sql += "'True', ";
                informacion += "VIVE_CON_EL = '" + VIVE_CON_EL + "', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "VIVE_CON_EL = 'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_COMPOSICION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_COMPOSICION_FAMILIAR, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_COMPOSICION = 0;
                }
            }

            return ID_COMPOSICION;
        }


        public Decimal AdicionarFormacionAcademicaEntrevista(Decimal REGISTRO_ENTREVISTA,
            String TIPO_EDUCACION,
            String NIVEL_ACADEMICO,
            String INSTITUCION,
            Int32 ANNO,
            String OBSERVACIONES,
            String CURSO,
            Decimal DURACION,
            String UNIDAD_DURACION,
            Conexion conexion)
        {

            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_INFO_ACADEMICA = 0;

            sql = "usp_sel_reg_informacion_academica_adicionar ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA + ", ";
                informacion += "REGISTRO_ENTREVISTA = '" + REGISTRO_ENTREVISTA + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_EDUCACION) == false)
            {
                sql += "'" + TIPO_EDUCACION + "', ";
                informacion += "TIPO_EDUCACION = '" + TIPO_EDUCACION + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_EDUCACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NIVEL_ACADEMICO) == false)
            {
                sql += "'" + NIVEL_ACADEMICO + "', ";
                informacion += "NIVEL_ACADEMICO = '" + NIVEL_ACADEMICO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NIVEL_ACADEMICO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(INSTITUCION) == false)
            {
                sql += "'" + INSTITUCION + "', ";
                informacion += "INSTITUCION = '" + INSTITUCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "INSTITUCION = 'NULL', ";
            }

            if (ANNO != 0)
            {
                sql += ANNO + ", ";
                informacion += "ANNO = '" + ANNO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ANNO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBSERVACIONES = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CURSO) == false)
            {
                sql += "'" + CURSO + "', ";
                informacion += "CURSO = '" + CURSO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CURSO = 'NULL', ";
            }

            if (DURACION != 0)
            {
                sql += DURACION.ToString().Replace(',', '.') + ", ";
                informacion += "DURACION = '" + DURACION.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DURACION = 'NULL', ";
            }

            if (String.IsNullOrEmpty(UNIDAD_DURACION) == false)
            {
                sql += "'" + UNIDAD_DURACION + "', ";
                informacion += "UNIDAD_DURACION = '" + UNIDAD_DURACION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "UNIDAD_DURACION = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_INFO_ACADEMICA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_INFORMACION_ACADEMICA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_INFO_ACADEMICA = 0;
                }
            }

            return ID_INFO_ACADEMICA;
        }

        public Decimal AdicionarExperienciaLaboralEntrevista(Decimal REGISTRO_ENTREVISTA,
            String EMPRESA,
            String CARGO,
            String FUNCIONES,
            DateTime FECHA_INGRESO,
            DateTime FECHA_RETIRO,
            String MOTIVO_RETIRO,
            Decimal ULTIMO_SALARIO,
            Conexion conexion)
        {

            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_EXPERIENCIA = 0;

            sql = "usp_sel_reg_experiencia_laboral_adicionar ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA + ", ";
                informacion += "REGISTRO_ENTREVISTA = '" + REGISTRO_ENTREVISTA + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(EMPRESA) == false)
            {
                sql += "'" + EMPRESA + "', ";
                informacion += "EMPRESA = '" + EMPRESA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "EMPRESA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CARGO) == false)
            {
                sql += "'" + CARGO + "', ";
                informacion += "CARGO = '" + CARGO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CARGO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(FUNCIONES) == false)
            {
                sql += "'" + FUNCIONES + "', ";
                informacion += "FUNCIONES = '" + FUNCIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FUNCIONES = 'NULL', ";
            }

            if (FECHA_INGRESO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INGRESO) + "', ";
                informacion += "FECHA_INGRESO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INGRESO) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_INGRESO = 'NULL', ";
            }

            if (FECHA_RETIRO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RETIRO) + "', ";
                informacion += "FECHA_RETIRO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RETIRO) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_RETIRO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(MOTIVO_RETIRO) == false)
            {
                sql += "'" + MOTIVO_RETIRO + "', ";
                informacion += "MOTIVO_RETIRO = '" + MOTIVO_RETIRO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "MOTIVO_RETIRO = 'NULL', ";
            }

            if (ULTIMO_SALARIO != 0)
            {
                sql += ULTIMO_SALARIO.ToString().Replace(',', '.') + ", ";
                informacion += "ULTIMO_SALARIO = '" + ULTIMO_SALARIO.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ULTIMO_SALARIO = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_EXPERIENCIA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_EXPERIENCIA_LABORAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_EXPERIENCIA = 0;
                }
            }

            return ID_EXPERIENCIA;
        }

        public Boolean ActulizarComposicionFamiliarEntrevista(Decimal REGISTRO_ENTREVISTA,
            List<ComposicionFamiliar> listaComposicionFamiliar,
            Conexion conexion)
        {
            Boolean correcto = true;

            hojasVida _hojasVida = new hojasVida(Empresa, Usuario);
            DataTable tablainfofamiliarActual = _hojasVida.ObtenerSelRegComposicionFamiliar(REGISTRO_ENTREVISTA, conexion);

            Boolean registroEncontrado = false;

            foreach (DataRow filaFamiliarActual in tablainfofamiliarActual.Rows)
            {
                registroEncontrado = false;

                Decimal ID_COMPOSICION_ACTUAL = Convert.ToDecimal(filaFamiliarActual["ID_COMPOSICION"]);

                foreach (ComposicionFamiliar c in listaComposicionFamiliar)
                {
                    if (ID_COMPOSICION_ACTUAL == c.ID_COMPOSICION)
                    {
                        registroEncontrado = true;
                        break;
                    }
                }

                if (registroEncontrado == false)
                {
                    if (InhabilitarComposicionFamiliarEntrevista(ID_COMPOSICION_ACTUAL, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            foreach (ComposicionFamiliar c in listaComposicionFamiliar)
            {
                if (c.ID_COMPOSICION <= 0)
                {
                    Decimal ID_COMPOSICION = AdicionarComposicionFamiliarEntrevista(REGISTRO_ENTREVISTA, c.ID_TIPO_FAMILIAR, c.NOMBRES, c.APELLIDOS, c.FECHA_NACIMIENTO, c.PROFESION, c.ID_CIUDAD, c.VIVE_CON_EL, conexion);

                    if (ID_COMPOSICION <= 0)
                    {
                        correcto = false;
                        break;
                    }
                }
                else
                {
                    if (ActualizarComposicionFamiliarEntrevista(c.ID_COMPOSICION, c.ID_TIPO_FAMILIAR, c.NOMBRES, c.APELLIDOS, c.FECHA_NACIMIENTO, c.PROFESION, c.ID_CIUDAD, c.VIVE_CON_EL, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            return correcto;
        }

        public Boolean ActualizarFormacionAcademicaEntrevistado(Decimal REGISTRO_ENTREVISTA,
            List<FormacionAcademica> listaFormacionAcademica,
            Conexion conexion)
        {
            Boolean correcto = true;

            hojasVida _hojasVida = new hojasVida(Empresa, Usuario);
            DataTable tablInfoFormacionActual = _hojasVida.ObtenerSelRegInformacionAcademicaFormalYNoFormal(REGISTRO_ENTREVISTA, conexion);

            Boolean registroEncontrado = false;

            foreach (DataRow filaFormacionActual in tablInfoFormacionActual.Rows)
            {
                registroEncontrado = false;

                Decimal ID_INFO_ACADEMICA_ACTUAL = Convert.ToDecimal(filaFormacionActual["ID_INFO_ACADEMICA"]);

                foreach (FormacionAcademica f in listaFormacionAcademica)
                {
                    if (ID_INFO_ACADEMICA_ACTUAL == f.ID_INFO_ACADEMICA)
                    {
                        registroEncontrado = true;
                        break;
                    }
                }

                if (registroEncontrado == false)
                {
                    if (InhabilitarFormacionAcademicaEntrevista(ID_INFO_ACADEMICA_ACTUAL, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            foreach (FormacionAcademica f in listaFormacionAcademica)
            {
                if (f.ID_INFO_ACADEMICA <= 0)
                {
                    Decimal ID_INFO_ACADEMICA = AdicionarFormacionAcademicaEntrevista(REGISTRO_ENTREVISTA, f.TIPO_EDUCACION, f.NIVEL_ACADEMICO, f.INSTITUCION, f.ANNO, f.OBSERVACIONES, f.CURSO, f.DURACION, f.UNIDAD_DURACION, conexion);

                    if (ID_INFO_ACADEMICA <= 0)
                    {
                        correcto = false;
                        break;
                    }
                }
                else
                {
                    if (ActualizarInformacionAcademicaEntrevista(f.ID_INFO_ACADEMICA, f.NIVEL_ACADEMICO, f.INSTITUCION, f.ANNO, f.OBSERVACIONES, f.CURSO, f.DURACION, f.UNIDAD_DURACION, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            return correcto;
        }


        public Boolean ActualizarExperienciaLaboralEntrevistado(Decimal REGISTRO_ENTREVISTA,
            List<ExperienciaLaboral> listaExperiencialaboral,
            Conexion conexion)
        {
            Boolean correcto = true;

            hojasVida _hojasVida = new hojasVida(Empresa, Usuario);
            DataTable tablInfoExperienciaActual = _hojasVida.ObtenerSelRegExperienciaLaboral(REGISTRO_ENTREVISTA, conexion);

            Boolean registroEncontrado = false;

            foreach (DataRow filaExperienciaActual in tablInfoExperienciaActual.Rows)
            {
                registroEncontrado = false;

                Decimal ID_EXPERIENCIA_ACTUAL = Convert.ToDecimal(filaExperienciaActual["ID_EXPERIENCIA"]);

                foreach (ExperienciaLaboral e in listaExperiencialaboral)
                {
                    if (ID_EXPERIENCIA_ACTUAL == e.ID_EXPERIENCIA)
                    {
                        registroEncontrado = true;
                        break;
                    }
                }

                if (registroEncontrado == false)
                {
                    if (InhabilitarExperienciaLaboralEntrevista(ID_EXPERIENCIA_ACTUAL, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            foreach (ExperienciaLaboral e in listaExperiencialaboral)
            {
                if (e.ID_EXPERIENCIA <= 0)
                {
                    Decimal ID_EXPERIRNCIA = AdicionarExperienciaLaboralEntrevista(REGISTRO_ENTREVISTA, e.EMPRESA_CLIENTE, e.CARGO, e.FUNCIONES, e.FECHA_INGRESO, e.FECHA_RETIRO, e.MOTIVO_RETIRO, e.ULTIMO_SALARIO, conexion);

                    if (ID_EXPERIRNCIA <= 0)
                    {
                        correcto = false;
                        break;
                    }
                }
                else
                {
                    if (ActualizarExperienciaLAboralEntrevista(e.ID_EXPERIENCIA, e.EMPRESA_CLIENTE, e.CARGO, e.FUNCIONES, e.FECHA_INGRESO, e.FECHA_RETIRO, e.MOTIVO_RETIRO, e.ULTIMO_SALARIO, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            return correcto;
        }

        public Decimal AdicionarEntrevista(Decimal ID_SOLICITUD,
            DateTime FCH_ENTREVISTA,
            String COM_C_FAM,
            String COM_F_LAB,
            String COM_C_ACA,
            String COM_C_GEN,
            List<listaPruebasAplicados> listaPrubas,
            Decimal ID_REQUERIMIENTO,
            List<ComposicionFamiliar> listaComposicionFamiliar,
            List<FormacionAcademica> listaFormacionAcademica,
            List<ExperienciaLaboral> listaExperienciaLaboral,
            Decimal ID_PERFIL,
            List<AplicacionCompetencia> listaCompetencias)
        {
            Boolean correcto = true;

            Decimal ID_ENTREVISTA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_ENTREVISTA = AdicionarSelRegEntrevistas(ID_SOLICITUD, FCH_ENTREVISTA, COM_C_FAM, COM_F_LAB, COM_C_ACA, COM_C_GEN, conexion);

                if (ID_ENTREVISTA < 0)
                {
                    correcto = false;
                    ID_ENTREVISTA = 0;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    if (guardarPruebas(listaPrubas, conexion) == false)
                    {
                        correcto = false;
                        ID_ENTREVISTA = 0;
                        conexion.DeshacerTransaccion();
                    }
                    else
                    {
                        if (guardarCompetencias(listaCompetencias, conexion) == false)
                        {
                            correcto = false;
                            ID_ENTREVISTA = 0;
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            if (ActulizarComposicionFamiliarEntrevista(ID_ENTREVISTA, listaComposicionFamiliar, conexion) == false)
                            {
                                correcto = false;
                                ID_ENTREVISTA = 0;
                                conexion.DeshacerTransaccion();
                            }
                            else
                            {
                                if (ActualizarFormacionAcademicaEntrevistado(ID_ENTREVISTA, listaFormacionAcademica, conexion) == false)
                                {
                                    correcto = false;
                                    ID_ENTREVISTA = 0;
                                    conexion.DeshacerTransaccion();
                                }
                                else
                                {
                                    if (ActualizarExperienciaLaboralEntrevistado(ID_ENTREVISTA, listaExperienciaLaboral, conexion) == false)
                                    {
                                        correcto = false;
                                        ID_ENTREVISTA = 0;
                                        conexion.DeshacerTransaccion();
                                    }
                                    else
                                    {
                                        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                                        if (ID_REQUERIMIENTO > 0)
                                        {
                                            if (!_radicacionHojasDeVida.ActualizarRequisicionSolicitud(ID_SOLICITUD, ID_REQUERIMIENTO, conexion))
                                            {
                                                MensajeError = _radicacionHojasDeVida.MensajeError;
                                                correcto = false;
                                                conexion.DeshacerTransaccion();
                                            }
                                        }

                                        if (correcto == true)
                                        {
                                            DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(ID_SOLICITUD, conexion);
                                            DataRow filaSolicitud = tablaSolicitud.Rows[0];

                                            if (filaSolicitud["ARCHIVO"].ToString().Trim() != "EN CLIENTE")
                                            {
                                                if (_radicacionHojasDeVida.ActualizarEstadoSolicitud(ID_SOLICITUD, conexion) == false)
                                                {
                                                    correcto = false;
                                                    MensajeError = _radicacionHojasDeVida.MensajeError;
                                                    conexion.DeshacerTransaccion();
                                                }
                                            }
                                        }
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
                conexion.DeshacerTransaccion();
                ID_ENTREVISTA = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_ENTREVISTA;
        }

        public Boolean guardarPruebas(List<listaPruebasAplicados> listaPruebas, Conexion conexion)
        {
            foreach (listaPruebasAplicados fila in listaPruebas)
            {
                if (fila.REGISTRO == 0)
                {
                    if (fila.ARCHIVO_PRUEBA == null)
                    {
                        if (AdicionarPruebaAplicada(fila.ID_SOLICITUD, fila.ID_PRUEBA, fila.ID_CATEGORIA, fila.FECHA_R, "Ninguna", fila.RESULTADOS, conexion) == 0)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (AdicionarPruebaAplicadaConImagen(fila.ID_SOLICITUD, fila.ID_PRUEBA, fila.ID_CATEGORIA, fila.FECHA_R, "Ninguna", fila.RESULTADOS, fila.ARCHIVO_PRUEBA, fila.ARCHIVO_EXTENSION, fila.ARCHIVO_TAMANO, fila.ARCHIVO_TYPE, conexion) == 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (fila.ARCHIVO_PRUEBA == null)
                    {
                        if (ActualizarPruebaAplicada(fila.REGISTRO, fila.ID_PRUEBA, fila.ID_CATEGORIA, fila.FECHA_R, "Ninguna", fila.RESULTADOS, conexion) == false)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (ActualizarPruebaAplicadaConImagen(fila.REGISTRO, fila.ID_PRUEBA, fila.ID_CATEGORIA, fila.FECHA_R, "Ninguna", fila.RESULTADOS, fila.ARCHIVO_PRUEBA, fila.ARCHIVO_EXTENSION, fila.ARCHIVO_TAMANO, fila.ARCHIVO_TYPE, conexion) == false)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public Boolean guardarCompetencias(List<AplicacionCompetencia> listaCompetencias, Conexion conexion)
        {
            foreach (AplicacionCompetencia fila in listaCompetencias)
            {
                if (fila.ID_APLICACION_COMPETENCIA == 0)
                {
                    if (AdicionarCompetenciaAplicada(fila.ID_SOLICITUD, fila.ID_COMPETENCIA_ASSESMENT, fila.NIVEL_CALIFICACION, fila.CALIFICACION, fila.OBSERVACIONES, conexion) == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    if (ActualizarCompetenciaAplicada(fila.ID_APLICACION_COMPETENCIA, fila.ID_COMPETENCIA_ASSESMENT, fila.NIVEL_CALIFICACION, fila.CALIFICACION, fila.OBSERVACIONES, conexion) == false)
                    {
                        return false;
                    }

                }
            }

            return true;
        }


        public Decimal AdicionarCompetenciaAplicada(Decimal ID_SOLICITUD,
            Decimal ID_COMPETENCIA_ASSESMENT,
            int NIVEL_CALIFICACION,
            String CALIFICACION,
            String OBSERVACIONES,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Int32 registro = 0;

            tools _tools = new tools();

            sql = "usp_sel_reg_aplicacion_competencias_adicionar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_COMPETENCIA_ASSESMENT != 0)
            {
                sql += ID_COMPETENCIA_ASSESMENT + ", ";
                informacion += "ID_COMPETENCIA_ASSESMENT = '" + ID_COMPETENCIA_ASSESMENT + "', ";
            }
            else
            {
                MensajeError += "El campo ID_COMPETENCIA_ASSESMENT no puede ser 0\n";
                ejecutar = false;
            }

            if (NIVEL_CALIFICACION != 0)
            {
                sql += NIVEL_CALIFICACION + ", ";
                informacion += "NIVEL_CALIFICACION = '" + NIVEL_CALIFICACION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NIVEL_CALIFICACION = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CALIFICACION)))
            {
                sql += "'" + CALIFICACION + "', ";
                informacion += "CALIFICACION = '" + CALIFICACION + "', ";
            }
            else
            {
                MensajeError += "El campo CALIFICACION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBSERVACIONES = 'NULL', ";
            }

            sql += "'" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = conexion.ExecuteNonQuery(sql);
                    return Convert.ToDecimal(registro);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        public Decimal AdicionarPruebaAplicada(Decimal ID_SOLICITUD,
            Decimal ID_PRUEBA,
            Decimal ID_CATEGORIA,
            DateTime FECHA_R,
            String OBS_PRUEBA,
            String RESULTADOS,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Int32 registro = 0;

            tools _tools = new tools();

            sql = "usp_sel_reg_aplicacion_pruebas_adicionar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }
            if (ID_PRUEBA != 0)
            {
                sql += ID_PRUEBA + ", ";
                informacion += "ID_PRUEBA = '" + ID_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (!(String.IsNullOrEmpty(OBS_PRUEBA)))
            {
                sql += "'" + OBS_PRUEBA + "', ";
                informacion += "OBS_PRUEBA = '" + OBS_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo COM_C_FAM no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RESULTADOS)))
            {
                sql += "'" + RESULTADOS + "', ";
                informacion += "RESULTADOS = '" + RESULTADOS + "', ";
            }
            else
            {
                MensajeError += "El campo COM_F_LAB no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = conexion.ExecuteNonQuery(sql);
                    return Convert.ToDecimal(registro);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public Decimal AdicionarPruebaAplicadaConImagen(Decimal ID_SOLICITUD,
            Decimal ID_PRUEBA,
            Decimal ID_CATEGORIA,
            DateTime FECHA_R,
            String OBS_PRUEBA,
            String RESULTADOS,
            byte[] ARCHIVO_PRUEBA,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            Conexion conexion)
        {
            String informacion = null;
            Boolean ejecutar = true;
            Int32 registro = 0;

            tools _tools = new tools();

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_PRUEBA != 0)
            {
                informacion += "ID_PRUEBA = '" + ID_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CATEGORIA != 0)
            {
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (!(String.IsNullOrEmpty(OBS_PRUEBA)))
            {
                informacion += "OBS_PRUEBA = '" + OBS_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo COM_C_FAM no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RESULTADOS)))
            {
                informacion += "RESULTADOS = '" + RESULTADOS + "', ";
            }
            else
            {
                MensajeError += "El campo COM_F_LAB no puede ser nulo. \n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = conexion.ExecuteNonQueryParaAdicionarPruebaConImagen(ID_SOLICITUD, ID_PRUEBA, ID_CATEGORIA, FECHA_R, OBS_PRUEBA, RESULTADOS, Usuario, ARCHIVO_PRUEBA, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

                    if (registro == 0)
                    {
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    return 0;
                }
            }

            return Convert.ToDecimal(registro);
        }


        public Boolean ActualizarCompetenciaAplicada(Decimal ID_APLICACION_COMPETENCIA,
            Decimal ID_COMPETENCIA_ASSESMENT,
            int NIVEL_CALIFICAION,
            String CALIFICACION,
            String OBSERVACIONES,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_aplicacion_competencias_actualizar ";

            #region validaciones

            if (ID_APLICACION_COMPETENCIA != 0)
            {
                sql += ID_APLICACION_COMPETENCIA + ", ";
                informacion += "ID_APLICACION_COMPETENCIA = '" + ID_APLICACION_COMPETENCIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_APLICACION_COMPETENCIA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_COMPETENCIA_ASSESMENT != 0)
            {
                sql += ID_COMPETENCIA_ASSESMENT + ", ";
                informacion += "ID_COMPETENCIA_ASSESMENT = '" + ID_COMPETENCIA_ASSESMENT + "', ";
            }
            else
            {
                MensajeError += "El campo ID_COMPETENCIA_ASSESMENT no puede ser 0\n";
                ejecutar = false;
            }

            if (NIVEL_CALIFICAION != 0)
            {
                sql += NIVEL_CALIFICAION + ", ";
                informacion += "NIVEL_CALIFICAION = '" + NIVEL_CALIFICAION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NIVEL_CALIFICAION = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CALIFICACION) == false)
            {
                sql += "'" + CALIFICACION + "', ";
                informacion += "CALIFICACION = '" + CALIFICACION + "', ";
            }
            else
            {
                MensajeError += "El campo CALIFICACION no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBSERVACIONES = 'NULL', ";
            }

            sql += "'" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarPruebaAplicada(Decimal REGISTRO, Decimal ID_PRUEBA, Decimal ID_CATEGORIA, DateTime FECHA_R, String OBS_PRUEBA, String RESULTADOS, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_aplicacion_pruebas_actualizar ";

            #region validaciones

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }
            if (ID_PRUEBA != 0)
            {
                sql += ID_PRUEBA + ", ";
                informacion += "ID_PRUEBA = '" + ID_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede ser 0\n";
                ejecutar = false;
            }
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString() + "', ";

            if (!(String.IsNullOrEmpty(OBS_PRUEBA)))
            {
                sql += "'" + OBS_PRUEBA + "', ";
                informacion += "OBS_PRUEBA = '" + OBS_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_PRUEBA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RESULTADOS)))
            {
                sql += "'" + RESULTADOS + "', ";
                informacion += "RESULTADOS = '" + RESULTADOS + "', ";
            }
            else
            {
                MensajeError += "El campo RESULTADOS no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarPruebaAplicadaConImagen(Decimal REGISTRO,
            Decimal ID_PRUEBA,
            Decimal ID_CATEGORIA,
            DateTime FECHA_R,
            String OBS_PRUEBA,
            String RESULTADOS,
            byte[] ARCHIVO_PRUEBA,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            Conexion conexion)
        {
            String informacion = null;
            Boolean ejecutar = true;
            Int32 registro = 0;

            tools _tools = new tools();
            #region validaciones

            if (REGISTRO != 0)
            {
                informacion += "REGISTRO = '" + REGISTRO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }
            if (ID_PRUEBA != 0)
            {
                informacion += "ID_PRUEBA = '" + ID_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede ser 0\n";
                ejecutar = false;
            }
            if (ID_CATEGORIA != 0)
            {
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser 0\n";
                ejecutar = false;
            }

            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (!(String.IsNullOrEmpty(OBS_PRUEBA)))
            {
                informacion += "OBS_PRUEBA = '" + OBS_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_PRUEBA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RESULTADOS)))
            {
                informacion += "RESULTADOS = '" + RESULTADOS + "', ";
            }
            else
            {
                MensajeError += "El campo RESULTADOS no puede ser nulo. \n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = conexion.ExecuteNonQueryParaActualizarPruebaConImagen(REGISTRO, ID_PRUEBA, ID_CATEGORIA, FECHA_R, OBS_PRUEBA, RESULTADOS, Usuario, ARCHIVO_PRUEBA, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

                    if (registro == 0)
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    return false;
                }
            }

            if (registro == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean ActualizarSelRegEntrevistas(Decimal ID_SOLICITUD,
            DateTime FCH_ENTREVISTA,
            String COM_C_FAM,
            String COM_F_LAB,
            String COM_C_ACA,
            String COM_C_GEN,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_entrevistas_actualizar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_ENTREVISTA) + "', ";
            informacion += "FCH_ENTREVISTA = '" + FCH_ENTREVISTA.ToString() + "', ";

            sql += "'" + COM_C_FAM + "', ";
            informacion += "COM_C_FAM = '" + COM_C_FAM + "', ";

            sql += "'" + COM_F_LAB + "', ";
            informacion += "COM_F_LAB = '" + COM_F_LAB + "', ";

            sql += "'" + COM_C_ACA + "', ";
            informacion += "COM_C_ACA = '" + COM_C_ACA + "', ";

            sql += "'" + COM_C_GEN + "', ";
            informacion += "COM_C_GEN = '" + COM_C_GEN + "', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "Usuario = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_ENTREVISTAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarEntrevista(Decimal ID_SOLICITUD,
            DateTime FCH_ENTREVISTA,
            String COM_C_FAM,
            String COM_F_LAB,
            String COM_C_ACA,
            String COM_C_GEN,
            List<listaPruebasAplicados> listaPruebas,
            Decimal ID_REQUERIMIENTO,
            List<ComposicionFamiliar> listaComposicionFamiliar,
            List<FormacionAcademica> listaFormacionAcademica,
            List<ExperienciaLaboral> listaExperienciaLaboral,
            Decimal REGISTRO_ENTREVISTA,
            Decimal ID_PERFIL,
            List<AplicacionCompetencia> listaCompetencias)
        {
            Boolean correcto = true;
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarSelRegEntrevistas(ID_SOLICITUD, FCH_ENTREVISTA, COM_C_FAM, COM_F_LAB, COM_C_ACA, COM_C_GEN, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    if (guardarPruebas(listaPruebas, conexion) == false)
                    {
                        correcto = false;
                        conexion.DeshacerTransaccion();
                    }
                    else
                    {
                        if (guardarCompetencias(listaCompetencias, conexion) == false)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            if (ActulizarComposicionFamiliarEntrevista(REGISTRO_ENTREVISTA, listaComposicionFamiliar, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                            }
                            else
                            {
                                if (ActualizarFormacionAcademicaEntrevistado(REGISTRO_ENTREVISTA, listaFormacionAcademica, conexion) == false)
                                {
                                    correcto = false;
                                    conexion.DeshacerTransaccion();
                                }
                                else
                                {
                                    if (ActualizarExperienciaLaboralEntrevistado(REGISTRO_ENTREVISTA, listaExperienciaLaboral, conexion) == false)
                                    {
                                        correcto = false;
                                        conexion.DeshacerTransaccion();
                                    }
                                    else
                                    {
                                        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                                        if (ID_REQUERIMIENTO > 0)
                                        {
                                            if (!_radicacionHojasDeVida.ActualizarRequisicionSolicitud(ID_SOLICITUD, ID_REQUERIMIENTO, conexion))
                                            {
                                                MensajeError = _radicacionHojasDeVida.MensajeError;
                                                correcto = false;
                                                conexion.DeshacerTransaccion();
                                            }
                                        }

                                        if (correcto == true)
                                        {
                                            DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(ID_SOLICITUD, conexion);
                                            DataRow filaSolicitud = tablaSolicitud.Rows[0];

                                            if (filaSolicitud["ARCHIVO"].ToString().Trim() != "EN CLIENTE")
                                            {
                                                if (_radicacionHojasDeVida.ActualizarEstadoSolicitud(ID_SOLICITUD, conexion) == false)
                                                {
                                                    correcto = false;
                                                    MensajeError = _radicacionHojasDeVida.MensajeError;
                                                    conexion.DeshacerTransaccion();
                                                }
                                            }
                                        }
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
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerSelRegEntrevistasPorIdSolicitud(Decimal ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_entrevistas_obner_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString();
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_ENTREVISTAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public DataTable ObtenerSelRegEntrevistasPorIdSolicitud(Decimal ID_SOLICITUD, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_entrevistas_obner_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString();
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


        #endregion

        #region PRUEBAS APLICADAS
        public DataTable ObtenerSelRegAplicacionPrueebasObtenerPorIdSolicitud(Decimal ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_aplicacion_pruebas_obtener_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString();
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_APLICACION_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerSelRegAplicacionPrueebasObtenerPorRegistro(Decimal REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_aplicacion_pruebas_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO.ToString();
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_APLICACION_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerSelRegAplicacionPrueebasObtenerPorIdSolicitudIdPrueba(Decimal ID_SOLICITUD, Decimal ID_PRUEBA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_aplicacion_pruebas_obtener_por_id_solicitud_id_prueba ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD.ToString() + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRUEBA != 0)
            {
                sql += ID_PRUEBA.ToString();
                informacion += "ID_PRUEBA = '" + ID_PRUEBA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_APLICACION_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        #endregion

        #region PRUEBAS POR PERFIL
        public DataTable ObtenerSelRegPruebasPerfilObtenerPorIdPerfil(Decimal ID_PERFIL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_pruebas_perfil_obtener_por_id_perfil ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL.ToString();
                informacion += "ID_PERFIL = '" + ID_PERFIL.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_PRUEBAS_PERFIL, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        #endregion

        #region SEL_REG_DICCIONARIO_COMPETENCIAS

        public DataTable ObtenerCompetenciasActivas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_diccionario_competencias_obtener_todas";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DICCIONARIO_COMPETENCIAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerCompetenciasActivas(Decimal ID_PERFIL,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_diccionario_competencias_obtener_todas ";
            sql += ID_PERFIL;

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DICCIONARIO_COMPETENCIAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }



        public DataTable ObtenerCompetenciasAssesmentActivas(Decimal ID_ASSESMENT,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_competencias_assesment_obtenerPorIdAssesment_Activas ";
            sql += ID_ASSESMENT;

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

        public DataTable ObtenerCompetenciaPorId(Decimal ID_COMPETENCIA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_diccionario_competencias_obtener_por_id ";

            sql += ID_COMPETENCIA;

            if (ejecutar)
            {
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

        public Boolean EliminarCompetenciaEntrevista(Decimal ID_COMPETENCIA, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_sel_reg_diccionario_competencias_desactivar ";
            informacion = sql;

            #region validaciones
            if (ID_COMPETENCIA != 0)
            {
                sql += ID_COMPETENCIA + ", ";
                informacion += "ID_COMPETENCIA = '" + ID_COMPETENCIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_COMPETENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DICCIONARIO_COMPETENCIAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
            }

            return ejecutar;
        }

        public Decimal AdicionarCompetenciaEntrevista(String COMPETENCIA,
            String DEFINICION,
            String AREA,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_diccionario_competencias_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(COMPETENCIA)))
            {
                sql += "'" + COMPETENCIA + "', ";
                informacion += "COMPETENCIA = '" + COMPETENCIA + "', ";
            }
            else
            {
                MensajeError += "El campo COMPETENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DEFINICION)))
            {
                sql += "'" + DEFINICION + "', ";
                informacion += "DEFINICION = '" + DEFINICION + "', ";
            }
            else
            {
                MensajeError += "El campo DEFINICION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(AREA)))
            {
                sql += "'" + AREA + "'";
                informacion += "AREA = '" + AREA + "'";
            }
            else
            {
                MensajeError += "El campo AREA no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DICCIONARIO_COMPETENCIAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Boolean ActualizarCompetencias(List<competenciaEntrevista> listaCompetencias)
        {
            Boolean correcto = true;
            Boolean verificado = true;
            Decimal ID_COMPETENCIA_ANALIZADA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaCompetenciasActuales = ObtenerCompetenciasActivas(0, conexion);

                foreach (DataRow competenciaAlmacenada in tablaCompetenciasActuales.Rows)
                {
                    verificado = true;
                    ID_COMPETENCIA_ANALIZADA = Convert.ToDecimal(competenciaAlmacenada["ID_COMPETENCIA"]);

                    foreach (competenciaEntrevista competenciaNueva in listaCompetencias)
                    {
                        if (competenciaNueva.ID_COMPETENCIA == ID_COMPETENCIA_ANALIZADA)
                        {
                            verificado = false;
                            break;
                        }
                    }

                    if (verificado == true)
                    {
                        if (EliminarCompetenciaEntrevista(ID_COMPETENCIA_ANALIZADA, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                }

                foreach (competenciaEntrevista competencia in listaCompetencias)
                {
                    if (competencia.ID_COMPETENCIA == 0)
                    {
                        if (AdicionarCompetenciaEntrevista(competencia.COMPETENCIA, competencia.DEFINICION, competencia.AREA, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
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
                conexion.DeshacerTransaccion();
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public Decimal AdicionarAssesmentCenter(String NOMBRE_ASSESMENT,
            String DESCRIPCION_ASSESMENT,
            Boolean ACTIVO,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_assesment_center_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NOMBRE_ASSESMENT)))
            {
                sql += "'" + NOMBRE_ASSESMENT + "', ";
                informacion += "NOMBRE_ASSESMENT = '" + NOMBRE_ASSESMENT + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_ASSESMENT no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION_ASSESMENT)))
            {
                sql += "'" + DESCRIPCION_ASSESMENT + "', ";
                informacion += "DESCRIPCION_ASSESMENT = '" + DESCRIPCION_ASSESMENT + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION_ASSESMENT no puede ser nulo.\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_ASSESMENT_CENTER, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }


        public Boolean EliminarCompetenciaAssesment(Decimal ID_ASSESMENT_CENTER, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_sel_reg_competencias_assesment_desactivar ";
            informacion = sql;

            #region validaciones
            if (ID_ASSESMENT_CENTER != 0)
            {
                sql += ID_ASSESMENT_CENTER + ", ";
                informacion += "ID_ASSESMENT_CENTER = '" + ID_ASSESMENT_CENTER.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ASSESMENT_CENTER no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_COMPETENCIAS_ASSESMENT, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
            }

            return ejecutar;
        }

        public Decimal AdicionarCompetenciaAssesment(Decimal ID_ASSESMENT,
            Decimal ID_COMPETENCIA,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_competencias_assesment_adicionar ";

            #region validaciones

            if (ID_ASSESMENT != 0)
            {
                sql += ID_ASSESMENT + ", ";
                informacion += "ID_ASSESMENT = '" + ID_ASSESMENT + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ASSESMENT no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_COMPETENCIA != 0)
            {
                sql += ID_COMPETENCIA + ", ";
                informacion += "ID_COMPETENCIA = '" + ID_COMPETENCIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_COMPETENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_COMPETENCIAS_ASSESMENT, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }


        public Boolean ActualizarSelRegAssesmentCenter(Decimal ID_ASSESMENT_CENTER,
            String NOMBRE_ASSESMENT,
            String DESCRIPCION_ASSESMENT,
            Boolean ACTIVO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_assesment_center_actualizar ";

            #region validaciones

            if (ID_ASSESMENT_CENTER != 0)
            {
                sql += ID_ASSESMENT_CENTER + ", ";
                informacion += "ID_ASSESMENT_CENTER = '" + ID_ASSESMENT_CENTER + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ASSESMENT_CENTER no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_ASSESMENT) == false)
            {
                sql += "'" + NOMBRE_ASSESMENT + "', ";
                informacion += "NOMBRE_ASSESMENT = '" + NOMBRE_ASSESMENT + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_ASSESMENT no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION_ASSESMENT) == false)
            {
                sql += "'" + DESCRIPCION_ASSESMENT + "', ";
                informacion += "DESCRIPCION_ASSESMENT = '" + DESCRIPCION_ASSESMENT + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION_ASSESMENT no puede ser 0\n";
                ejecutar = false;
            }

            if (ACTIVO == true)
            {
                sql += "'True', ";
                informacion += "ACTIVO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ACTIVO = 'False', ";
            }

            sql += "'" + Usuario.ToString() + "'";
            informacion += "Usuario = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_ASSESMENT_CENTER, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public Decimal ActualizarAssesmentCenter(Decimal ID_ASSESMENT,
            String NOMBRE_ASSESMENT,
            String DESCRIPCION_ASSESMENT,
            Boolean ACTIVO,
            List<CompetenciaAssesment> listaCompetencias,
            Byte[] ARCHIVO_DOCUMENTO,
            Int32 ARCHIVO_TAMANO,
            String ARCHIVO_EXTENSION,
            String ARCHIVO_TYPE)
        {
            Boolean correcto = true;
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ID_ASSESMENT <= 0)
                {
                    ID_ASSESMENT = AdicionarAssesmentCenter(NOMBRE_ASSESMENT, DESCRIPCION_ASSESMENT, ACTIVO, conexion);

                    if (ID_ASSESMENT <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        ID_ASSESMENT = 0;
                    }
                }
                else
                {
                    if (ActualizarSelRegAssesmentCenter(ID_ASSESMENT, NOMBRE_ASSESMENT, DESCRIPCION_ASSESMENT, ACTIVO, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        ID_ASSESMENT = 0;
                    }
                }

                if (correcto == true)
                {
                    if (ARCHIVO_DOCUMENTO != null)
                    {
                        Decimal ID_DOCUMENTO = conexion.ExecuteEscalarParaAdicionarDocsParaAssesmentCenter(ID_ASSESMENT, ARCHIVO_DOCUMENTO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, Usuario);

                        if (ID_DOCUMENTO <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            ID_ASSESMENT = 0;
                            MensajeError = "Error intentando insertar o actualizar el documento relacionado con el Assesment Center.";
                        }
                    }
                }

                if (correcto == true)
                {
                    DataTable tablaCompetenciasActuales = ObtenerCompetenciasAssesmentActivas(ID_ASSESMENT, conexion);

                    Boolean competenciaExiste = false;

                    foreach (DataRow filaCompetenciaActual in tablaCompetenciasActuales.Rows)
                    {
                        Decimal ID_COMPETENCIA_ASSESMENT_ACTUAL = Convert.ToDecimal(filaCompetenciaActual["ID_COMPETENCIA_ASSESMENT"]);

                        foreach (CompetenciaAssesment competencia in listaCompetencias)
                        {
                            if (ID_COMPETENCIA_ASSESMENT_ACTUAL == competencia.ID_COMPETENCIA_ASSESMENT)
                            {
                                competenciaExiste = true;
                                break;
                            }
                        }

                        if (competenciaExiste == false)
                        {
                            if (EliminarCompetenciaAssesment(ID_COMPETENCIA_ASSESMENT_ACTUAL, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_ASSESMENT = 0;
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (CompetenciaAssesment competencia in listaCompetencias)
                        {
                            if (competencia.ID_COMPETENCIA_ASSESMENT <= 0)
                            {
                                if (AdicionarCompetenciaAssesment(ID_ASSESMENT, competencia.ID_COMPETENCIA, conexion) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_ASSESMENT = 0;
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
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                correcto = false;
                ID_ASSESMENT = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_ASSESMENT;
        }



        #endregion SEL_REG_DICCIONARIO_COMPETENCIAS

        #region SEL_REG_COMPOSICION_FAMILIAR
        public DataTable ObtenerSelRegComposicionFamiliar(Decimal REGISTRO_ENTREVISTA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_composicion_familiar_obtener_por_registroEntrevista ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA.ToString();
                informacion += "REGISTRO_ENTREVISTA = '" + REGISTRO_ENTREVISTA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
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


        public DataTable ObtenerSelRegComposicionFamiliar(Decimal REGISTRO_ENTREVISTA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_composicion_familiar_obtener_por_registroEntrevista ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA.ToString();
                informacion += "REGISTRO_ENTREVISTA = '" + REGISTRO_ENTREVISTA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
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
        #endregion SEL_REG_COMPOSICION_FAMILIAR

        #region SEL_REG_INFORMACION_ACADEMICA
        public DataTable ObtenerSelRegInformacionAcademica(Decimal REGISTRO_ENTREVISTA, String TIPO_EDUCACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_informacion_academica_obtener_por_registroEntrevistaYTipoEducacion ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_EDUCACION) == false)
            {
                sql += "'" + TIPO_EDUCACION + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_EDUCACION no puede ser nulo\n";
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

        public DataTable ObtenerSelRegInformacionAcademicaFormalYNoFormal(Decimal REGISTRO_ENTREVISTA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_informacion_academica_obtener_por_registroEntrevista_formal_y_no_formal ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA.ToString();
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
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
        #endregion SEL_REG_INFORMACION_ACADEMICA

        #region SEL_REG_EXPERIENCIA_LABORAL
        public DataTable ObtenerSelRegExperienciaLaboral(Decimal REGISTRO_ENTREVISTA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_experiencia_laboral_obtener_por_registroEntrevista ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA;
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
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

        public DataTable ObtenerSelRegExperienciaLaboral(Decimal REGISTRO_ENTREVISTA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_experiencia_laboral_obtener_por_registroEntrevista ";

            if (REGISTRO_ENTREVISTA != 0)
            {
                sql += REGISTRO_ENTREVISTA;
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENTREVISTA no puede ser nulo\n";
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
        #endregion SEL_REG_EXPERIENCIA_LABORAL
        #endregion
    }
}
