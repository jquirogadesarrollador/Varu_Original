using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class referencia
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        public enum TiposActualizacionReferencia
        {
            Nunguna = 0,
            CreacionLimpia,
            Actualizacion,
            CreacionEHistorial
        }

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
        public referencia()
        {
        }
        public referencia(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #region sel_reg_referencias

        public Decimal AdicionarRegistroSelRegReferencias(Decimal ID_SOLICITUD,
            String EMPRESA_TRABAJO,
            DateTime FECHA_INGRESO,
            DateTime FECHA_RETIRO,
            String ULTIMO_CARGO,
            String NOMBRE_INFORMANTE,
            String CARGO_INFORMANTE,
            Decimal ULTIMO_SALARIO,
            String NUM_TELEFONO,
            String VOLVER_A_CONTRATAR,
            String POR_QUE,
            String CONCEPTO,
            Decimal ID_CATEGORIA,
            String CUALIDADES_CALIFICACION,
            String NOMBRE_JEFE,
            String CARGO_JEFE,
            String EMPRESA_TEMPORAL,
            String NUM_TELEFONO_TEMPOAL,
            String TIPO_CONTRATO,
            String COMISIONES,
            String BONOS,
            String MOTIVO_RETIRO,
            Conexion conexion)
        {
            String sql = null;
            Decimal ID_REFERENCIA = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_referencias_adicionar ";

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

            if (!(String.IsNullOrEmpty(EMPRESA_TRABAJO)))
            {
                sql += "'" + EMPRESA_TRABAJO + "', ";
                informacion += "EMPRESA_TRABAJO = '" + EMPRESA_TRABAJO + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "EMPRESA_TRABAJO = 'null', ";
            }

            if (FECHA_INGRESO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INGRESO) + "', ";
                informacion += "FECHA_INGRESO = '" + FECHA_INGRESO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_INGRESO = 'NULL', ";
            }

            if (FECHA_RETIRO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RETIRO) + "', ";
                informacion += "FECHA_RETIRO = '" + FECHA_RETIRO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_RETIRO = 'NULL', ";
            }
            if (!(String.IsNullOrEmpty(ULTIMO_CARGO)))
            {
                sql += "'" + ULTIMO_CARGO + "', ";
                informacion += "ULTIMO_CARGO = '" + ULTIMO_CARGO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ULTIMO_CARGO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NOMBRE_INFORMANTE)))
            {
                sql += "'" + NOMBRE_INFORMANTE + "', ";
                informacion += "NOMBRE_INFORMANTE = '" + NOMBRE_INFORMANTE + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DEL INFORMANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO_INFORMANTE)))
            {
                sql += "'" + CARGO_INFORMANTE + "', ";
                informacion += "CARGO_INFORMANTE = '" + CARGO_INFORMANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CARGO_INFORMANTE = 'null', ";
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

            if (!(String.IsNullOrEmpty(NUM_TELEFONO)))
            {
                sql += "'" + NUM_TELEFONO + "', ";
                informacion += "NUM_TELEFONO = '" + NUM_TELEFONO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUM_TELEFONO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(VOLVER_A_CONTRATAR)))
            {
                sql += "'" + VOLVER_A_CONTRATAR + "', ";
                informacion += "VOLVER_A_CONTRATAR = '" + VOLVER_A_CONTRATAR + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "VOLVER_A_CONTRATAR = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(POR_QUE)))
            {
                sql += "'" + POR_QUE + "', ";
                informacion += "POR_QUE = '" + POR_QUE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "POR_QUE = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CONCEPTO)))
            {
                sql += "'" + CONCEPTO + "', ";
                informacion += "CONCEPTO = '" + CONCEPTO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONCEPTO = 'NULL', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CUALIDADES_CALIFICACION) == false)
            {
                sql += "'" + CUALIDADES_CALIFICACION + "', ";
                informacion += "CUALIDADES_CALIFICACION = '" + CUALIDADES_CALIFICACION + "', ";
            }
            else
            {
                MensajeError += "El campo CUALIDADES_CALIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_JEFE) == false)
            {
                sql += "'" + NOMBRE_JEFE + "', ";
                informacion += "NOMBRE_JEFE = '" + NOMBRE_JEFE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NOMBRE_JEFE = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CARGO_JEFE) == false)
            {
                sql += "'" + CARGO_JEFE + "', ";
                informacion += "CARGO_JEFE = '" + CARGO_JEFE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CARGO_JEFE = 'NULL', ";
            }

            if (String.IsNullOrEmpty(EMPRESA_TEMPORAL) == false)
            {
                sql += "'" + EMPRESA_TEMPORAL + "', ";
                informacion += "EMPRESA_TEMPORAL = '" + EMPRESA_TEMPORAL + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "EMPRESA_TEMPORAL = 'NULL', ";
            }

            if (String.IsNullOrEmpty(NUM_TELEFONO_TEMPOAL) == false)
            {
                sql += "'" + NUM_TELEFONO_TEMPOAL + "', ";
                informacion += "NUM_TELEFONO_TEMPOAL = '" + NUM_TELEFONO_TEMPOAL + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NUM_TELEFONO_TEMPOAL = 'NULL', ";
            }

            if (String.IsNullOrEmpty(TIPO_CONTRATO) == false)
            {
                sql += "'" + TIPO_CONTRATO + "', ";
                informacion += "TIPO_CONTRATO = '" + TIPO_CONTRATO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "TIPO_CONTRATO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(COMISIONES) == false)
            {
                sql += "'" + COMISIONES + "', ";
                informacion += "COMISIONES = '" + COMISIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "COMISIONES = 'NULL', ";
            }

            if (String.IsNullOrEmpty(BONOS) == false)
            {
                sql += "'" + BONOS + "', ";
                informacion += "BONOS = '" + BONOS + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "BONOS = 'NULL', ";
            }

            if (String.IsNullOrEmpty(MOTIVO_RETIRO) == false)
            {
                sql += "'" + MOTIVO_RETIRO + "'";
                informacion += "MOTIVO_RETIRO = '" + MOTIVO_RETIRO + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "MOTIVO_RETIRO = 'NULL'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region adicionar referencia
                    ID_REFERENCIA = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    #endregion adicionar referencia

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_REFERENCIAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_REFERENCIA = 0;
                }
            }

            return ID_REFERENCIA;
        }

        public Decimal Adicionar(Decimal ID_SOLICITUD,
            String EMPRESA_TRABAJO,
            DateTime FECHA_INGRESO,
            DateTime FECHA_RETIRO,
            String ULTIMO_CARGO,
            String NOMBRE_INFORMANTE,
            String CARGO_INFORMANTE,
            Decimal ULTIMO_SALARIO,
            String NUM_TELEFONO,
            String VOLVER_A_CONTRATAR,
            String POR_QUE,
            String CONCEPTO,
            Decimal ID_CATEGORIA,
            List<respuestaReferencia> listaRespuestas,
            String CUALIDADES_CALIFICACION,
            String NOMBRE_JEFE,
            String CARGO_JEFE,
            String EMPRESA_TEMPORAL,
            String NUM_TELEFONO_TEMPOAL,
            String TIPO_CONTRATO,
            String COMISIONES,
            String BONOS,
            String MOTIVO_RETIRO)
        {
            Boolean correcto = true;

            Decimal ID_REFERENCIA = 0;
            Decimal ID_RESPUESTA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_REFERENCIA = AdicionarRegistroSelRegReferencias(ID_SOLICITUD, EMPRESA_TRABAJO, FECHA_INGRESO, FECHA_RETIRO, ULTIMO_CARGO, NOMBRE_INFORMANTE, CARGO_INFORMANTE, ULTIMO_SALARIO, NUM_TELEFONO, VOLVER_A_CONTRATAR, POR_QUE, CONCEPTO, ID_CATEGORIA, CUALIDADES_CALIFICACION, NOMBRE_JEFE, CARGO_JEFE, EMPRESA_TEMPORAL, NUM_TELEFONO_TEMPOAL, TIPO_CONTRATO, COMISIONES, BONOS, MOTIVO_RETIRO, conexion);

                if (ID_REFERENCIA <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    ID_REFERENCIA = 0;
                }
                else
                {
                    foreach (respuestaReferencia respuesta in listaRespuestas)
                    {
                        ID_RESPUESTA = AdicionarRegistroSelRegRespuestasReferencia(ID_REFERENCIA, respuesta.ID_PREGUNTA, respuesta.RESPUESTA, conexion);

                        if (ID_RESPUESTA <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            ID_REFERENCIA = 0;
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
                conexion.DeshacerTransaccion();
                correcto = false;
                ID_REFERENCIA = 0;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_REFERENCIA;
        }


        public Decimal CopiaHistorialSelRegReferencias(Decimal ID_SOLICITUD,
            Conexion conexion)
        {
            String sql = null;
            Decimal ID_REFERENCIA = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_referencias_copia_historial ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region adicionar referencia
                    ID_REFERENCIA = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    #endregion adicionar referencia

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_REFERENCIAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_REFERENCIA = 0;
                }
            }

            return ID_REFERENCIA;
        }

        public Decimal AdicionarConHistorial(Decimal ID_SOLICITUD,
            String EMPRESA_TRABAJO,
            DateTime FECHA_INGRESO,
            DateTime FECHA_RETIRO,
            String ULTIMO_CARGO,
            String NOMBRE_INFORMANTE,
            String CARGO_INFORMANTE,
            Decimal ULTIMO_SALARIO,
            String NUM_TELEFONO,
            String VOLVER_A_CONTRATAR,
            String POR_QUE,
            String CONCEPTO,
            List<respuestaReferencia> listaRespuestas,
            Decimal ID_CATEGORIA,
            String CUALIDADES_CALIFICACION,
            String NOMBRE_JEFE,
            String CARGO_JEFE,
            String EMPRESA_TEMPORAL,
            String NUM_TELEFONO_TEMPOAL,
            String TIPO_CONTRATO,
            String COMISIONES,
            String BONOS,
            String MOTIVO_RETIRO)
        {
            Boolean correcto = true;

            Decimal ID_REFERENCIA = 0;
            Decimal ID_RESPUESTA = 0;
            Decimal ID_REFERENCIA_NUEVA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_REFERENCIA = CopiaHistorialSelRegReferencias(ID_SOLICITUD, conexion);

                if (ID_REFERENCIA <= 0)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                    ID_REFERENCIA_NUEVA = 0;
                }
                else
                {
                    ID_REFERENCIA_NUEVA = AdicionarRegistroSelRegReferencias(ID_SOLICITUD, EMPRESA_TRABAJO, FECHA_INGRESO, FECHA_RETIRO, ULTIMO_CARGO, NOMBRE_INFORMANTE, CARGO_INFORMANTE, ULTIMO_SALARIO, NUM_TELEFONO, VOLVER_A_CONTRATAR, POR_QUE, CONCEPTO, ID_CATEGORIA, CUALIDADES_CALIFICACION, NOMBRE_JEFE, CARGO_JEFE, EMPRESA_TEMPORAL, NUM_TELEFONO_TEMPOAL, TIPO_CONTRATO, COMISIONES, BONOS, MOTIVO_RETIRO, conexion);

                    if (ID_REFERENCIA_NUEVA <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        ID_REFERENCIA_NUEVA = 0;
                    }
                    else
                    {
                        foreach (respuestaReferencia respuesta in listaRespuestas)
                        {
                            ID_RESPUESTA = AdicionarRegistroSelRegRespuestasReferencia(ID_REFERENCIA_NUEVA, respuesta.ID_PREGUNTA, respuesta.RESPUESTA, conexion);

                            if (ID_RESPUESTA <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_REFERENCIA_NUEVA = 0;
                                break;
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
                ID_REFERENCIA_NUEVA = 0;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_REFERENCIA_NUEVA;
        }

        public Boolean ActualizarRegistroSelRegReferencias(Decimal ID_REFERENCIA,
            String EMPRESA_TRABAJO,
            DateTime FECHA_INGRESO,
            DateTime FECHA_RETIRO,
            String ULTIMO_CARGO,
            String NOMBRE_INFORMANTE,
            String CARGO_INFORMANTE,
            Decimal ULTIMO_SALARIO,
            String NUM_TELEFONO,
            String VOLVER_A_CONTRATAR,
            String POR_QUE,
            String CONCEPTO,
            String CUALIDADES_CALIFICACION,
            String NOMBRE_JEFE,
            String CARGO_JEFE,
            String EMPRESA_TEMPORAL,
            String NUM_TELEFONO_TEMPOAL,
            String TIPO_CONTRATO,
            String COMISIONES,
            String BONOS,
            String MOTIVO_RETIRO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_referencias_actualizar ";

            #region validaciones

            if (ID_REFERENCIA != 0)
            {
                sql += ID_REFERENCIA + ", ";
                informacion += "ID_REFERENCIA = '" + ID_REFERENCIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REFERENCIA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EMPRESA_TRABAJO)))
            {
                sql += "'" + EMPRESA_TRABAJO + "', ";
                informacion += "EMPRESA_TRABAJO = '" + EMPRESA_TRABAJO + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "EMPRESA_TRABAJO = 'null', ";
            }

            if (FECHA_INGRESO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INGRESO) + "', ";
                informacion += "FECHA_INGRESO = '" + FECHA_INGRESO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_INGRESO = 'NULL', ";
            }

            if (FECHA_RETIRO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RETIRO) + "', ";
                informacion += "FECHA_RETIRO = '" + FECHA_RETIRO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_RETIRO = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(ULTIMO_CARGO)))
            {
                sql += "'" + ULTIMO_CARGO + "', ";
                informacion += "ULTIMO_CARGO = '" + ULTIMO_CARGO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ULTIMO_CARGO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(NOMBRE_INFORMANTE)))
            {
                sql += "'" + NOMBRE_INFORMANTE + "', ";
                informacion += "NOMBRE_INFORMANTE = '" + NOMBRE_INFORMANTE + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DEL INFORMANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO_INFORMANTE)))
            {
                sql += "'" + CARGO_INFORMANTE + "', ";
                informacion += "CARGO_INFORMANTE = '" + CARGO_INFORMANTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CARGO_INFORMANTE = 'null', ";
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

            if (!(String.IsNullOrEmpty(NUM_TELEFONO)))
            {
                sql += "'" + NUM_TELEFONO + "', ";
                informacion += "NUM_TELEFONO = '" + NUM_TELEFONO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUM_TELEFONO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(VOLVER_A_CONTRATAR)))
            {
                sql += "'" + VOLVER_A_CONTRATAR + "', ";
                informacion += "VOLVER_A_CONTRATAR = '" + VOLVER_A_CONTRATAR + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "VOLVER_A_CONTRATAR = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(POR_QUE)))
            {
                sql += "'" + POR_QUE + "', ";
                informacion += "POR_QUE = '" + POR_QUE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "POR_QUE = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CONCEPTO)))
            {
                sql += "'" + CONCEPTO + "', ";
                informacion += "CONCEPTO = '" + CONCEPTO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONCEPTO = 'NULL', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(CUALIDADES_CALIFICACION) == false)
            {
                sql += "'" + CUALIDADES_CALIFICACION + "', ";
                informacion += "CUALIDADES_CALIFICACION = '" + CUALIDADES_CALIFICACION + "', ";
            }
            else
            {
                MensajeError += "El campo CUALIDADES_CALIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_JEFE) == false)
            {
                sql += "'" + NOMBRE_JEFE + "', ";
                informacion += "NOMBRE_JEFE = '" + NOMBRE_JEFE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NOMBRE_JEFE = 'null', ";
            }

            if (String.IsNullOrEmpty(CARGO_JEFE) == false)
            {
                sql += "'" + CARGO_JEFE + "', ";
                informacion += "CARGO_JEFE = '" + CARGO_JEFE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CARGO_JEFE = 'null', ";
            }

            if (String.IsNullOrEmpty(EMPRESA_TEMPORAL) == false)
            {
                sql += "'" + EMPRESA_TEMPORAL + "', ";
                informacion += "EMPRESA_TEMPORAL = '" + EMPRESA_TEMPORAL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "EMPRESA_TEMPORAL = 'null', ";
            }

            if (String.IsNullOrEmpty(NUM_TELEFONO_TEMPOAL) == false)
            {
                sql += "'" + NUM_TELEFONO_TEMPOAL + "', ";
                informacion += "NUM_TELEFONO_TEMPOAL = '" + NUM_TELEFONO_TEMPOAL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUM_TELEFONO_TEMPOAL = 'null', ";
            }

            if (String.IsNullOrEmpty(TIPO_CONTRATO) == false)
            {
                sql += "'" + TIPO_CONTRATO + "', ";
                informacion += "TIPO_CONTRATO = '" + TIPO_CONTRATO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_CONTRATO = 'null', ";
            }

            if (String.IsNullOrEmpty(COMISIONES) == false)
            {
                sql += "'" + COMISIONES + "', ";
                informacion += "COMISIONES = '" + COMISIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "COMISIONES = 'null', ";
            }

            if (String.IsNullOrEmpty(BONOS) == false)
            {
                sql += "'" + BONOS + "', ";
                informacion += "BONOS = '" + BONOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "BONOS = 'null', ";
            }

            if (String.IsNullOrEmpty(MOTIVO_RETIRO) == false)
            {
                sql += "'" + MOTIVO_RETIRO + "'";
                informacion += "MOTIVO_RETIRO = '" + MOTIVO_RETIRO + "'";
            }
            else
            {
                sql += "null";
                informacion += "MOTIVO_RETIRO = 'null'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region actualiza referencia
                    conexion.ExecuteNonQuery(sql);
                    #endregion actualiza referencia

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_REFERENCIAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }

        public Boolean Actualizar(Decimal ID_REFERENCIA,
            String EMPRESA_TRABAJO,
            DateTime FECHA_INGRESO,
            DateTime FECHA_RETIRO,
            String ULTIMO_CARGO,
            String NOMBRE_INFORMANTE,
            String CARGO_INFORMANTE,
            Decimal ULTIMO_SALARIO,
            String NUM_TELEFONO,
            String VOLVER_A_CONTRATAR,
            String POR_QUE,
            String CONCEPTO,
            List<respuestaReferencia> listaRespuestas,
            String CUALIDADES_CALIFICAION,
            String NOMBRE_JEFE,
            String CARGO_JEFE,
            String EMPRESA_TEMPORAL,
            String NUM_TELEFONO_TEMPOAL,
            String TIPO_CONTRATO,
            String COMISIONES,
            String BONOS,
            String MOTIVO_RETIRO)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarRegistroSelRegReferencias(ID_REFERENCIA, EMPRESA_TRABAJO, FECHA_INGRESO, FECHA_RETIRO, ULTIMO_CARGO, NOMBRE_INFORMANTE, CARGO_INFORMANTE, ULTIMO_SALARIO, NUM_TELEFONO, VOLVER_A_CONTRATAR, POR_QUE, CONCEPTO, CUALIDADES_CALIFICAION, NOMBRE_JEFE, CARGO_JEFE, EMPRESA_TEMPORAL, NUM_TELEFONO_TEMPOAL, TIPO_CONTRATO, COMISIONES, BONOS, MOTIVO_RETIRO, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    foreach (respuestaReferencia respuesta in listaRespuestas)
                    {
                        if (ActualizarRegistroSelRegRespuestasReferencia(respuesta.ID_RESPUESTA, respuesta.RESPUESTA, conexion) == false)
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

        public Boolean Eliminar(Decimal ID_REFERENCIA)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_sel_reg_referencias_eliminar ";

            #region validaciones

            if (ID_REFERENCIA != 0)
            {
                sql += ID_REFERENCIA;
                informacion += "ID_REFERENCIA = " + ID_REFERENCIA + ", ";
            }
            else
            {
                MensajeError += "El campo ID REFERENCIA no puede ser 0\n";
                ejecutar = false;
            }

            informacion += "USU_ELI = '" + Usuario.ToString() + "' ";


            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar referencia
                    if (conexion.ExecuteNonQuery(sql) == 0) ejecutadoCorrectamente = false;
                    #endregion adicionar referencia

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(Usuario, tabla.SEL_REG_REFERENCIAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false) ejecutadoCorrectamente = false;
                    #endregion auditoria

                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }

        public DataTable ObtenerPorIdReferencia(Decimal ID_REFERENCIA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_referencias_obtenerPorIdReferencia ";

            if (ID_REFERENCIA != 0)
            {
                sql += ID_REFERENCIA;
                informacion += "ID_REFERENCIA = '" + ID_REFERENCIA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID REFERENCIA no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_REFERENCIAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerPorNumeroDocumento(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_referencias_obtenerPorNumeroDocumento ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUMERO DE DOCUMENTO DE IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_REFERENCIAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerPorNombre(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_referencias_obtenerPorNombre ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_REFERENCIAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        #endregion sel_reg_referencias

        #region sel_reg_preguntas_referencia
        public DataTable ObtenerPreguntasActivas(Decimal ID_CATEGORIA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_preguntas_obtener_todas ";

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA.ToString();
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CATEGORIA no puede ser nulo.";
            }

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

            _dataTable.Columns.Add("ID_RESPUESTA");

            foreach (DataRow fila in _dataTable.Rows)
            {
                fila["ID_RESPUESTA"] = 0;
            }

            return _dataTable;
        }

        public DataTable ObtenerCategoriasActivas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_cat_ref_obtener_todas_activas";

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


        public DataTable ObtenerCategoriasActivas(Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_cat_ref_obtener_todas_activas";

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
            }

            return _dataTable;
        }

        public DataTable ObtenerPreguntasActivas(Decimal ID_CATEGORIA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_preguntas_obtener_todas ";

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campos ID_CATEGORIA no puede ser nulo.";
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

        public Boolean EliminarPreguntaReferencia(Decimal ID_PREGUNTA, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_sel_reg_preguntas_referencia_desactivar ";
            informacion = sql;

            #region validaciones
            if (ID_PREGUNTA != 0)
            {
                sql += ID_PREGUNTA + ", ";
                informacion += "ID_PREGUNTA = '" + ID_PREGUNTA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PREGUNTA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_PREGUNTAS_REFERENCIA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean EliminarCategoriaReferencia(Decimal ID_CATEGORIA, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_sel_reg_cat_ref_desactivar ";
            informacion = sql;

            #region validaciones
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_CAT_REF, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Decimal AdicionarPreguntaReferencia(Decimal ID_CATEGORIA,
            String CONTENIDO,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_preguntas_adicionar ";

            #region validaciones

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CATEGORIA no puede ser vacio.";
            }

            if (!(String.IsNullOrEmpty(CONTENIDO)))
            {
                sql += "'" + CONTENIDO + "', ";
                informacion += "CONTENIDO = '" + CONTENIDO + "', ";
            }
            else
            {
                MensajeError += "El campo CONTENIDO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_PREGUNTAS_REFERENCIA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Decimal AdicionarCategoriaReferencia(String NOMBRE_CAT,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_cat_ref_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NOMBRE_CAT)))
            {
                sql += "'" + NOMBRE_CAT + "', ";
                informacion += "NOMBRE_CAT = '" + NOMBRE_CAT + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_CAT no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_CAT_REF, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarPreguntas(List<preguntaReferencia> listaPreguntas, Decimal ID_CATEGORIA)
        {
            Boolean correcto = true;
            Boolean verificado = true;
            Decimal ID_PREGUNTA_ANALIZADA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaPreguntasActuales = ObtenerPreguntasActivas(ID_CATEGORIA, conexion);

                foreach (DataRow preguntaAlmacenada in tablaPreguntasActuales.Rows)
                {
                    verificado = true;
                    ID_PREGUNTA_ANALIZADA = Convert.ToDecimal(preguntaAlmacenada["ID_PREGUNTA"]);

                    foreach (preguntaReferencia preguntaNueva in listaPreguntas)
                    {
                        if (preguntaNueva.ID_PREGUNTA == ID_PREGUNTA_ANALIZADA)
                        {
                            verificado = false;
                            break;
                        }
                    }

                    if (verificado == true)
                    {
                        if (EliminarPreguntaReferencia(ID_PREGUNTA_ANALIZADA, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }

                    }
                }

                foreach (preguntaReferencia pregunta in listaPreguntas)
                {
                    if (pregunta.ID_PREGUNTA == 0)
                    {
                        if (AdicionarPreguntaReferencia(pregunta.ID_CATEGORIA, pregunta.CONTENIDO, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                    else
                    {
                        if (ActualizarPreguntaReferencia(pregunta.ID_PREGUNTA, pregunta.CONTENIDO, conexion) == false)
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

        public Boolean ActualizarCategoriaReferencia(Decimal ID_CATEGORIA,
            String NOMBRE_CAT,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_cat_actualizar ";

            #region validaciones

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

            if (!(String.IsNullOrEmpty(NOMBRE_CAT)))
            {
                sql += "'" + NOMBRE_CAT + "', ";
                informacion += "NOMBRE_CAT = '" + NOMBRE_CAT + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_CAT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region actualiza referencia
                    conexion.ExecuteNonQuery(sql);
                    #endregion actualiza referencia

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_CAT_REF, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }


        public Boolean ActualizarPreguntaReferencia(Decimal ID_PREGUNTA,
            String CONTENIDO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_preguntas_referencia_actualizar ";

            #region validaciones

            if (ID_PREGUNTA != 0)
            {
                sql += ID_PREGUNTA + ", ";
                informacion += "ID_PREGUNTA = '" + ID_PREGUNTA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PREGUNTA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONTENIDO)))
            {
                sql += "'" + CONTENIDO + "', ";
                informacion += "CONTENIDO = '" + CONTENIDO + "', ";
            }
            else
            {
                MensajeError += "El campo CONTENIDO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region actualiza referencia
                    conexion.ExecuteNonQuery(sql);
                    #endregion actualiza referencia

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_PREGUNTAS_REFERENCIA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }


        public Boolean ActualizarCategorias(List<CategoriaReferencia> listaCategorias)
        {
            Boolean correcto = true;
            Boolean verificado = true;
            Decimal ID_CATEGORIA_ANALIZADA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaCategoriasActuales = ObtenerCategoriasActivas(conexion);

                foreach (DataRow categoriaAlmacenada in tablaCategoriasActuales.Rows)
                {
                    verificado = true;
                    ID_CATEGORIA_ANALIZADA = Convert.ToDecimal(categoriaAlmacenada["ID_CATEGORIA"]);

                    foreach (CategoriaReferencia categoriaNueva in listaCategorias)
                    {
                        if (categoriaNueva.ID_CATEGORIA == ID_CATEGORIA_ANALIZADA)
                        {
                            verificado = false;
                            break;
                        }
                    }

                    if (verificado == true)
                    {
                        if (EliminarCategoriaReferencia(ID_CATEGORIA_ANALIZADA, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }

                    }
                }

                foreach (CategoriaReferencia categoria in listaCategorias)
                {
                    if (categoria.ID_CATEGORIA == 0)
                    {
                        if (AdicionarCategoriaReferencia(categoria.NOMBRE_CAT, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                    else
                    {
                        if (ActualizarCategoriaReferencia(categoria.ID_CATEGORIA, categoria.NOMBRE_CAT, conexion) == false)
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



        public DataTable ObtenerPreguntasRespuestasReferencia(Decimal ID_REFERENCIA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_respuestas_referencia_obtenerPorIdReferencia " + ID_REFERENCIA.ToString();

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

        public Decimal AdicionarRegistroSelRegRespuestasReferencia(Decimal ID_REFERENCIA,
            Decimal ID_PREGUNTA,
            String RESPUESTA,
            Conexion conexion)
        {
            String sql = null;
            Decimal ID_RESPUESTA = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_respuestas_referencia_adicionar ";

            #region validaciones

            if (ID_REFERENCIA != 0)
            {
                sql += ID_REFERENCIA + ", ";
                informacion += "ID_REFERENCIA = '" + ID_REFERENCIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REFERENCIA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_PREGUNTA != 0)
            {
                sql += ID_PREGUNTA + ", ";
                informacion += "ID_PREGUNTA = '" + ID_PREGUNTA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PREGUNTA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RESPUESTA)))
            {
                sql += "'" + RESPUESTA + "', ";
                informacion += "RESPUESTA = '" + RESPUESTA + "', ";
            }
            else
            {
                MensajeError += "El campo RESPUESTA no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region adicionar respuesta
                    ID_RESPUESTA = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    #endregion adicionar respuesta

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_RESPUESTAS_REFERENCIA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_RESPUESTA = 0;
                }
            }

            return ID_RESPUESTA;
        }

        public Boolean ActualizarRegistroSelRegRespuestasReferencia(Decimal ID_RESPUESTA,
            String RESPUESTA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_respuestas_referencia_actualizar ";

            #region validaciones

            if (ID_RESPUESTA != 0)
            {
                sql += ID_RESPUESTA + ", ";
                informacion += "ID_RESPUESTA = '" + ID_RESPUESTA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_RESPUESTA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RESPUESTA)))
            {
                sql += "'" + RESPUESTA + "', ";
                informacion += "RESPUESTA = '" + RESPUESTA + "', ";
            }
            else
            {
                MensajeError += "El campo RESPUESTA no puede ser 0\n";
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

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_RESPUESTAS_REFERENCIA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }
        #endregion sel_reg_preguntas_referencia
        #endregion metodos
    }
}