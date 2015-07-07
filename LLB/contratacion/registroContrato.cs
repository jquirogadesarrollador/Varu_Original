using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.contratacion
{
    public class registroContrato
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
        public registroContrato(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #region con_reg_contratos


        public DataRow ObtenerContratoPorIdEmpleado(decimal idEmpleado)
        {
            DataSet _dataSet = new DataSet();
            DataRow dataRow;
            String sql = null;

            Conexion conexion = new Conexion(Empresa);

            sql = "usp_con_reg_contratos_obtener_PorIdEmpleado ";
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                dataRow = _dataSet.Tables[0].DefaultView.Table.Rows[0];
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los datos de contrato por id empleado");
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataRow;
        }

        public Decimal AdicionarConRegContratos(int ID_EMPLEADO, int ID_REQUERIMIENTO, int ID_SOLICITUD,
            int ID_CENTRO_C, int ID_SUB_C, String CLASE_CONTRATO, DateTime FECHA_INICIA, DateTime FECHA_TERMINA,
            String VIGENTE, String PAGO_LIQ, String SAL_INT, String TIPO_CONTRATO, int ID_SERVICIO_RESPECTIVO,
            String ID_CIUDAD, int ID_SERVICIO, Conexion conexion, String PAGO_DIAS_PRODUCTIVIDAD, String SENA_PRODICTIVO,
            String SENA_ELECTIVO, String PRACTICANTE_UNIVERSITARIO, Decimal VALOR_NOMINA, Decimal VALOR_CONTRATO,
            DateTime FECHA_INICIO_PERIODO, DateTime FECHA_FIN_PERIODO, String PERIODO_PAGO)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_contratos_adicionar ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
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
            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CENTRO_C = 'null', ";
            }
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_SUB_C = '0', ";
            }
            if (!(String.IsNullOrEmpty(CLASE_CONTRATO)))
            {
                sql += "'" + CLASE_CONTRATO + "', ";
                informacion += "CLASE_CONTRATO = '" + CLASE_CONTRATO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CLASE_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_INICIA.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIA) + "', ";
                informacion += "FECHA_INICIA = '" + FECHA_INICIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_INICIA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_TERMINA.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_TERMINA) + "', ";
                informacion += "FECHA_TERMINA = '" + FECHA_TERMINA.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_TERMINA = 'null', ";
            }
            if (!(String.IsNullOrEmpty(VIGENTE)))
            {
                sql += "'" + VIGENTE + "', ";
                informacion += "VIGENTE = '" + VIGENTE.ToString() + "', ";
            }
            else
            {
                sql += "'S', ";
                informacion += "VIGENTE = '" + VIGENTE.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(PAGO_LIQ)))
            {
                sql += "'" + PAGO_LIQ + "', ";
                informacion += "PAGO_LIQ = '" + PAGO_LIQ.ToString() + "', ";
            }
            else
            {
                sql += "'N', ";
                informacion += "PAGO_LIQ = '" + PAGO_LIQ.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(SAL_INT)))
            {
                sql += "'" + SAL_INT + "', ";
                informacion += "SAL_INT = '" + SAL_INT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SAL_INT no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(TIPO_CONTRATO)))
            {
                sql += "'" + TIPO_CONTRATO + "', ";
                informacion += "TIPO_CONTRATO = '" + TIPO_CONTRATO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SERVICIO_RESPECTIVO != 0)
            {
                sql += ID_SERVICIO_RESPECTIVO + ", ";
                informacion += "ID_SERVICIO_RESPECTIVO = '" + ID_SERVICIO_RESPECTIVO.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO_RESPECTIVO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                sql += "Null,";
                informacion += "ID_CIUDAD = '0', ";
            }
            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_SERVICIO = '0', ";
            }
            if (!(String.IsNullOrEmpty(PAGO_DIAS_PRODUCTIVIDAD)))
            {
                sql += "'" + PAGO_DIAS_PRODUCTIVIDAD + "', ";
                informacion += "PAGO_DIAS_PRODUCTIVIDAD = '" + PAGO_DIAS_PRODUCTIVIDAD.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PAGO_DIAS_PRODUCTIVIDAD = 'NULL', ";
            }
            if (!(String.IsNullOrEmpty(SENA_PRODICTIVO)))
            {
                sql += "'" + SENA_PRODICTIVO + "', ";
                informacion += "SENA_PRODICTIVO = '" + SENA_PRODICTIVO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "SENA_PRODICTIVO = 'NULL', ";
            }
            if (!(String.IsNullOrEmpty(SENA_ELECTIVO)))
            {
                sql += "'" + SENA_ELECTIVO + "', ";
                informacion += "SENA_ELECTIVO = '" + SENA_ELECTIVO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "SENA_ELECTIVO = 'NULL', ";
            }
            if (!(String.IsNullOrEmpty(PRACTICANTE_UNIVERSITARIO)))
            {
                sql += "'" + PRACTICANTE_UNIVERSITARIO + "', ";
                informacion += "PRACTICANTE_UNIVERSITARIO = '" + PRACTICANTE_UNIVERSITARIO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PRACTICANTE_UNIVERSITARIO = 'NULL', ";
            }
            if (VALOR_NOMINA != 0)
            {
                sql += VALOR_NOMINA + ", ";
                informacion += "VALOR_NOMINA = '" + VALOR_NOMINA.ToString() + "', ";
            }
            else
            {
                sql += "0 ,";
                informacion += "VALOR_NOMINA = '0', ";
            }
            if (VALOR_CONTRATO != 0)
            {
                sql += VALOR_CONTRATO + " ,";
                informacion += "VALOR_CONTRATO = '" + VALOR_CONTRATO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "VALOR_CONTRATO = '0', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_INICIO_PERIODO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO_PERIODO) + "', ";
                informacion += "FECHA_INICIO_PERIODO = '" + FECHA_INICIO_PERIODO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_INICIO_PERIODO = 'null', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_FIN_PERIODO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN_PERIODO) + "', ";
                informacion += "FECHA_FIN_PERIODO = '" + FECHA_FIN_PERIODO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_INICIO_PERIODO = 'null', ";
            }
            if (!(String.IsNullOrEmpty(PERIODO_PAGO)))
            {
                sql += "'" + PERIODO_PAGO + "' ";
                informacion += "PERIODO_PAGO = '" + PERIODO_PAGO.ToString() + "' ";
            }
            else
            {
                sql += "NULL ";
                informacion += "PERIODO_PAGO = 'NULL'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        private DataTable ConfigurarTablaResultadoNomina()
        {
            DataTable tablaTemp = new DataTable();

            tablaTemp.Columns.Add("valorResultado");
            tablaTemp.Columns.Add("descripcionResultado");

            tablaTemp.AcceptChanges();

            return tablaTemp;
        }

        private DataTable ComprobarRestriccionNomina(Decimal idEmpleado,
            String idCiudad,
            Decimal idCentroC,
            Decimal idSubC,
            Conexion conexion)
        {
            DataTable tablaResultado = new DataTable();

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();

            String sql = "usp_cambio_sub_c_empleado_comprobar_restricciones_nomina ";

            sql += idEmpleado.ToString();
            sql += ", '" + idCiudad + "'";
            sql += ", " + idCentroC;
            sql += ", " + idSubC;

            try
            {
                _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                tablaResultado = _dataView.Table;
            }
            catch (Exception ex)
            {
                tablaResultado = ConfigurarTablaResultadoNomina();

                DataRow filaTabla = tablaResultado.NewRow();
                filaTabla["valorResultado"] = "ERROR";
                filaTabla["descripcionResultado"] = "ERROR: " + ex.Message;

                tablaResultado.Rows.Add(filaTabla);
            }

            return tablaResultado;
        }
        public Boolean ActualizarContratoDesdeAuditoria(Decimal ID_CONTRATO,
            Decimal ID_EMPLEADO,
            Decimal RIESGO,
            String TIPO_CONTRATO,
            Decimal ID_PERFIL,
            Decimal ID_SUB_C,
            Decimal ID_CENTRO_C,
            String ID_CIUDAD,
            String PAGO_DIAS_PRODUCTIVIDAD,
            Decimal VALOR_NOMINA,
            Decimal VALOR_CONTRATO,
            Decimal SALARIO,
            String SAL_INT,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD,
            String NUM_CUENTA,
            Decimal ID_SOLICITUD,
            Boolean ACTUALIZAR_ESTADO_PROCESO,
            String TIPO_CUENTA,
            DateTime FECHA_INICIO_PERIODO,
            DateTime FECHA_FIN_PERIODO,
            String PERIODO_PAGO,
            String CLASE_CONTRATO,
            String CONTROL_CONTRATO,
            String CHEQUE_REG)
        {
            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            DataTable tablaResultadoNomina = new DataTable();

            try
            {
                tablaResultadoNomina = ComprobarRestriccionNomina(ID_EMPLEADO, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, conexion);

                if (tablaResultadoNomina.Rows.Count <= 0)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                    MensajeError = "No se puedieron determinar las restriciones de nomina. No se pudo actualizar la información del contrato del trabajador seleccionado.";
                }
                else
                {
                    DataRow filaRestriccionesNomina = tablaResultadoNomina.Rows[0];

                    if (filaRestriccionesNomina["valorResultado"].ToString().ToUpper() == "ERROR")
                    {
                        conexion.DeshacerTransaccion();
                        verificador = false;
                        MensajeError = filaRestriccionesNomina["descripcionResultado"].ToString();
                    }
                    else
                    {
                        if (ActualizarConRegContratosAuditoria(ID_CONTRATO, TIPO_CONTRATO, PAGO_DIAS_PRODUCTIVIDAD, VALOR_NOMINA, VALOR_CONTRATO, FECHA_INICIO_PERIODO, FECHA_FIN_PERIODO, PERIODO_PAGO, CLASE_CONTRATO, CONTROL_CONTRATO, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            verificador = false;
                        }
                        else
                        {
                            if (ActualizarNomEmpleadosAuditoria(ID_EMPLEADO, SALARIO, RIESGO, ID_ENTIDAD, NUM_CUENTA, SAL_INT, ID_PERFIL, FORMA_PAGO, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, conexion, TIPO_CUENTA, FECHA_INICIO_PERIODO, CHEQUE_REG) == false)
                            {
                                conexion.DeshacerTransaccion();
                                verificador = false;
                            }
                            else
                            {
                                auditoriaContratos _auditoriaContratos = new auditoriaContratos(Empresa, Usuario);
                                Decimal ID_AUDITORIA = _auditoriaContratos.AdicionarAuditoriaContratos(ID_EMPLEADO, tabla.CON_REG_CONTRATOS, ID_CONTRATO, DateTime.Now, conexion);

                                if (ID_AUDITORIA <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = _auditoriaContratos.MensajeError;
                                    verificador = false;
                                }
                                else
                                {
                                    if (ACTUALIZAR_ESTADO_PROCESO == true)
                                    {
                                        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                                        if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CONTRATO, conexion) == false)
                                        {
                                            conexion.DeshacerTransaccion();
                                            MensajeError = _radicacionHojasDeVida.MensajeError;
                                            verificador = false;
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }

        public Boolean ActualizarConRegContratosAuditoria(Decimal REGISTRO,
            String TIPO_CONTRATO,
            String PAGO_DIAS_PRODUCTIVIDAD,
            Decimal VALOR_NOMINA,
            Decimal VALOR_CONTRATO,
            DateTime FECHA_INICIO_PERIODO,
            DateTime FECHA_FIN_PERIODO,
            String PERIODO_PAGO,
            String CLASE_CONTRATO,
            String CONTROL_CONTRATO,
            Conexion conexion)
        {

            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_contratos_actualizar_auditoria_v3 ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_CONTRATO) == false)
            {
                sql += "'" + TIPO_CONTRATO + "', ";
                informacion += "TIPO_CONTRATO = '" + TIPO_CONTRATO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(PAGO_DIAS_PRODUCTIVIDAD) == false)
            {
                sql += "'" + PAGO_DIAS_PRODUCTIVIDAD + "', ";
                informacion += "PAGO_DIAS_PRODUCTIVIDAD = '" + PAGO_DIAS_PRODUCTIVIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo PAGO_DIAS_PRODUCTIVIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (VALOR_NOMINA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_NOMINA) + ", ";
                informacion += "VALOR_NOMINA = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_NOMINA) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "VALOR_NOMINA = 'NULL', ";
            }

            if (VALOR_CONTRATO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_CONTRATO) + ", ";
                informacion += "VALOR_CONTRATO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_CONTRATO) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "VALOR_CONTRATO = 'NULL', ";
            }

            if (!string.IsNullOrEmpty(FECHA_INICIO_PERIODO.ToString()))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO_PERIODO) + "', ";
                informacion += "FECHA_INICIO_PERIODO = '" + FECHA_INICIO_PERIODO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_INICIO_PERIODO = 'NULL', ";
            }


            if (!string.IsNullOrEmpty(FECHA_FIN_PERIODO.ToString()))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN_PERIODO) + "', ";
                informacion += "FECHA_FIN_PERIODO = '" + FECHA_FIN_PERIODO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_FIN_PERIODO = 'NULL', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(PERIODO_PAGO) == false)
            {
                sql += "'" + PERIODO_PAGO + "', ";
                informacion += "PERIODO_PAGO = '" + PERIODO_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PERIODO_PAGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CLASE_CONTRATO) == false)
            {
                sql += "'" + CLASE_CONTRATO + "', ";
                informacion += "CLASE_CONTRATO = '" + CLASE_CONTRATO + "', ";
            }
            else
            {
                MensajeError += "El campo CLASE_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }


            if (String.IsNullOrEmpty(CONTROL_CONTRATO) == false)
            {
                sql += "'" + CONTROL_CONTRATO + "'";
                informacion += "CONTROL_CONTRATO = '" + CONTROL_CONTRATO + "'";
            }
            else
            {
                MensajeError += "El campo CONTROL_CONTRATO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarNomEmpleadosAuditoria(Decimal ID_EMPLEADO,
            Decimal SALARIO,
            Decimal RIESGO,
            Decimal ID_ENTIDAD,
            String NUM_CUENTA,
            String SAL_INT,
            Decimal ID_PERFIL,
            String FORMA_PAGO,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            Conexion conexion,
            String TIPO_CUENTA,
            DateTime FECHA_INICIO_PERIODO,
            String CHEQUE_REG)
        {

            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_actualizar_auditoria_V3 ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (SALARIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO) + ", ";
                informacion += "SALARIO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO) + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "SALARIO = '0', ";
            }

            if (RIESGO != 0)
            {
                sql += RIESGO + ", ";
                informacion += "RIESGO = '" + RIESGO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "RIESGO = '0', ";
            }

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_ENTIDAD = 'NULL', ";
            }

            if (String.IsNullOrEmpty(NUM_CUENTA) == false)
            {
                sql += "'" + NUM_CUENTA + "', ";
                informacion += "NUM_CUENTA = '" + NUM_CUENTA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NUM_CUENTA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(SAL_INT) == false)
            {
                sql += "'" + SAL_INT + "', ";
                informacion += "SAL_INT = '" + SAL_INT + "', ";
            }
            else
            {
                MensajeError += "El campo SAL_INT no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = '" + ID_PERFIL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(FORMA_PAGO) == false)
            {
                sql += "'" + FORMA_PAGO + "', ";
                informacion += "FORMA_PAGO = '" + FORMA_PAGO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FORMA_PAGO = 'NULL', ";
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

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CENTRO_C = 'NULL', ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SUB_C = 'NULL', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(TIPO_CUENTA) == false)
            {
                sql += "'" + TIPO_CUENTA + "', ";
                informacion += "TIPO_CUENTA = '" + TIPO_CUENTA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CIUDAD = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(FECHA_INICIO_PERIODO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO_PERIODO) + "', ";
                informacion += "FECHA_INICIO_PERIODO = '" + FECHA_INICIO_PERIODO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_INICIO_PERIODO = null, ";
            }


            if (String.IsNullOrEmpty(CHEQUE_REG) == false)
            {
                sql += "'" + CHEQUE_REG + "'";
                informacion += "CHEQUE_REG = '" + CHEQUE_REG.ToString() + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "CHEQUE_REG = 'NULL'";
            }


            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_EMPLEADOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegContratosPorRequerimiento(int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_obtener_por_id_requerimiento ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
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

        public DataTable obtenerInfoNomEmpleadoPorIdSolicitudIdRequerimiento(Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO, Decimal ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtener_por_idSolicitud_idRequerimiento ";

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
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
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

        public DataTable obtenerInfoAfilaicionesDeUnContrato(Decimal ID_EMPLEADO, Decimal ID_CONTRATO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtener_afiliaciones_contrato ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
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


        public DataTable ObtenerConRegContratosPorSolicitud(int ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_obtener_por_id_solicitud ";

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

        public DataTable ObtenerConRegContratosPorRegistro(int REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
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

        public DataTable ObtenerInfoParaImprimirContrato(Decimal REGISTRO_CONTRATO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_ObtenerInfo_para_imprimir_contrato ";

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO;
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser nulo\n";
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

        public DataTable ObtenerInfoParaImprimirClausulas(Decimal REGISTRO_CONTRATO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_ObtenerInfo_para_imprimir_clausulas ";

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO;
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser nulo\n";
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

        public DataTable ObtenerInformacionContratoPorIdSolicitudIdREquerimiento(Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_obtener_datos_unificados_contrato ";

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
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
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

        public DataTable ObtenerInformacionContratoPorVencer(Int32 VALOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_obtener_datos_unificados_contrato_por_vencer ";

            if (VALOR != 0)
            {
                sql += VALOR;
            }
            else
            {
                MensajeError += "El campo VALOR no puede ser nulo\n";
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

            if (_dataTable.Rows.Count > 0)
            {
                Int32 division = (VALOR / 3);
                Int32 rangoALTA = division;
                Int32 rangoMEDIA = division + division;
                Int32 rangoBAJA = VALOR;

                _dataTable.Columns.Add("ALERTA");

                DateTime fechaActual = DateTime.Now;
                DateTime fechaTermina;
                foreach (DataRow fila in _dataTable.Rows)
                {
                    fechaTermina = Convert.ToDateTime(fila["FECHA_TERMINA"]);
                    if (fechaTermina >= fechaActual)
                    {
                        if (fechaTermina <= (fechaActual.AddDays(rangoALTA)))
                        {
                            fila["ALERTA"] = "ALTA";
                        }
                        else
                        {
                            if (fechaTermina <= (fechaActual.AddDays(rangoMEDIA)))
                            {
                                fila["ALERTA"] = "MEDIA";
                            }
                            else
                            {
                                fila["ALERTA"] = "BAJA";
                            }
                        }
                    }
                }
            }

            return _dataTable;
        }

        public Boolean ActualizarConRegContratosImpresos(int REGISTRO, String CONTRATO_IMPRESO, String CLAUSULAS_IMPRESO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_contratos_actualizar_impresion ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONTRATO_IMPRESO) == false)
            {
                sql += "'" + CONTRATO_IMPRESO + "', ";
                informacion += "CONTRATO_IMPRESO = '" + CONTRATO_IMPRESO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONTRATO_IMPRESO = NULL, ";
            }

            if (String.IsNullOrEmpty(CLAUSULAS_IMPRESO) == false)
            {
                sql += "'" + CLAUSULAS_IMPRESO + "' ";
                informacion += "CLAUSULAS_IMPRESO = '" + CLAUSULAS_IMPRESO + "' ";
            }
            else
            {
                sql += "NULL ";
                informacion += "CLAUSULAS_IMPRESO = NULL ";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        #endregion

        public DataTable ObtenerDatosNuevoContrato(int ID_REQUERIMIENTO, int ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_nuevo_contrato ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ",";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerPorNumeroIdentificacion(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_ObtenerPorNumeroIdentificacion ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

            foreach (DataRow fila in _dataTable.Rows)
            {
                if (fila["ACTIVO"].ToString().Trim() == "S")
                {
                    fila["ACTIVO"] = "Si";
                }
                else
                {
                    fila["ACTIVO"] = "No";
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

            sql = "usp_con_reg_contratos_ObtenerPorNombre ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorApellido(String APELLIDO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_ObtenerPorApellidos ";

            if (!(String.IsNullOrEmpty(APELLIDO)))
            {
                sql += "'" + APELLIDO + "'";
                informacion += "APELLIDO = '" + APELLIDO + "'";
            }
            else
            {
                MensajeError += "El campo APELLIDO no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public String ElaborarContrato(int id_requerimiento, int id_solicitud, int id_empresa, int id_centro_Costo, int id_sub_cc, String ciudad,
            int ID_SERVICIO_RESPECTIVO, int servicio, int id_ARP, int id_caja_c, int id_EPS, int id_Pensiones, Decimal riesgo, String pensionado,
            String Clase_Contrato, String Tipo_Contrato, String Tipo_Pago, DateTime fecha_Inicia, DateTime fecha_termina, String sal_int,
            Decimal Salario, String vigente, String activo, String liquidado, String pago_Liquidacion, int id_entidad, String Num_Cuenta, String Forma_pago,
            String PAGO_DIAS_PRODUCTIVIDAD, String SENA_PRODICTIVO, String SENA_ELECTIVO, String PRACTICANTE_UNIVERSITARIO, Decimal VALOR_NOMINA, Decimal VALOR_CONTRATO,
            DateTime FECHA_INICIO_PERIODO, DateTime FECHA_FIN_PERIODO, String Periodo_Pago, String tipo_Cuenta, string descripcion_salario, decimal idPerfil)
        {

            String datosG = "";
            int id_perfil = 0;
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                requisicion _req = new requisicion(Empresa, Usuario);
                DataTable tablaReq = _req.ObtenerComRequerimientoPorIdRequerimiento(Convert.ToDecimal(id_requerimiento), conexion);

                DataRow filaReq = tablaReq.Rows[0];
                id_perfil = Convert.ToInt32(filaReq["REGISTRO_PERFIL"].ToString());
                usuario _empleado = new usuario(Empresa);
                radicacionHojasDeVida _sol = new radicacionHojasDeVida(Empresa, Usuario);

                Decimal idEmpleado = _empleado.AdicionarNomEmpleados(0, id_solicitud, id_empresa, id_centro_Costo, id_sub_cc, fecha_Inicia, Salario,
                    pensionado, activo, liquidado, riesgo, id_ARP, id_caja_c, id_EPS, id_Pensiones, "C", id_entidad, Num_Cuenta, sal_int, ciudad, id_perfil, tipo_Cuenta, descripcion_salario, Forma_pago, conexion);

                if (idEmpleado <= 0)
                {
                    _mensaje_error = "\n El empleado no fue creado, " + _empleado.MensajeError;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    registroContrato _contrato = new registroContrato(Empresa, Usuario);

                    Decimal id_Contrato = 0;
                    /* revisado por cambio en riesgo */
                    id_Contrato = _contrato.AdicionarConRegContratos(Convert.ToInt32(idEmpleado), id_requerimiento, id_solicitud, id_centro_Costo,
                        id_sub_cc, Clase_Contrato, fecha_Inicia, fecha_termina, vigente, pago_Liquidacion, sal_int, Tipo_Contrato,
                        ID_SERVICIO_RESPECTIVO, ciudad, servicio, conexion, PAGO_DIAS_PRODUCTIVIDAD, SENA_PRODICTIVO, SENA_ELECTIVO, PRACTICANTE_UNIVERSITARIO,
                        VALOR_NOMINA, VALOR_CONTRATO, FECHA_INICIO_PERIODO, FECHA_FIN_PERIODO, Periodo_Pago);

                    if (String.IsNullOrEmpty(_contrato.MensajeError))
                    {
                        _empleado.ActualizarNomEmpleados(Convert.ToInt32(idEmpleado), Convert.ToInt32(id_Contrato), id_solicitud, id_empresa, id_centro_Costo, id_sub_cc, fecha_Inicia, Salario,
                        pensionado, activo, liquidado, riesgo, id_ARP, id_caja_c, id_EPS, id_Pensiones, "C", id_entidad, Num_Cuenta, sal_int, ciudad, id_perfil, Forma_pago, conexion);

                        if (String.IsNullOrEmpty(_empleado.MensajeError))
                        {

                            if (_sol.ActualizarEstadoProcesoRegSolicitudesIngreso(id_requerimiento, id_solicitud, "CONTRATADO", Usuario, conexion))
                            {
                                if (_sol.ActualizarEstadoRegSolicitudesIngreso(id_requerimiento, id_solicitud, "CONTRATADO", conexion))
                                {
                                    datosG = id_Contrato + "," + idEmpleado;

                                    try
                                    {
                                        conexion.ExecuteNonQuery("usp_ESC_CRT_ENTREGAS_SC_adicionar " + idEmpleado + ", '" + Usuario + "'");
                                        conexion.ExecuteNonQuery("usp_empleado_clausulas_contratar " + idEmpleado + ", " + idPerfil + ", '" + Usuario + "'");

                                        Int32 requerimientoActualizado = Convert.ToInt32(conexion.ExecuteScalar("usp_comprobar_cierre_requisicion_por_sistema " + id_requerimiento.ToString() + ", '" + Usuario + "'"));

                                        if (requerimientoActualizado <= 0)
                                        {
                                            conexion.DeshacerTransaccion();
                                            MensajeError = "El empleado no fue creado, Ocurrio un error al momento de determinar si la requisición debe ser cuplida por sistema.";
                                        }
                                        else
                                        {
                                            conexion.AceptarTransaccion();
                                        }
                                    }
                                    catch
                                    {
                                        _mensaje_error += "\n No fue posible registrar los servicios complementarios o clausulas para el perfil contratado " + _sol.MensajeError;
                                        conexion.AceptarTransaccion();
                                    }
                                }
                                else
                                {
                                    _mensaje_error += "\n No fue posible actualizar el estado de la solicitud: " + _sol.MensajeError;
                                    conexion.DeshacerTransaccion();
                                }
                            }
                            else
                            {
                                _mensaje_error += "\n No fue posible actualizar el estado del proceso de la soliciutd: " + _sol.MensajeError;
                                conexion.DeshacerTransaccion();
                            }

                        }
                        else
                        {
                            _mensaje_error += "\n Se presentó el siguiente error en la actualización del empleado: " + _empleado.MensajeError;
                            conexion.DeshacerTransaccion();
                        }
                    }
                    else
                    {
                        _mensaje_error += "\n Se presentó el siguiente error en la creación del contrato: " + _contrato.MensajeError;
                        conexion.DeshacerTransaccion();
                    }
                }
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return datosG;
        }

        public DataTable ObtenerContratoPorNumeroIdentificacion(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_num_identidad ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerContratoPorNombreSoloActivos(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_nombres ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo. \n";
                ejecutar = false;
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

            return _dataTable;
        }

        public DataTable ObtenerContratoPorApellidosSoloActivos(String APELLIDOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_apellidos ";

            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "'";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo. \n";
                ejecutar = false;
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

            return _dataTable;
        }

        public DataTable ObtenerContratoPorNumDocIdentidadSoloActivos(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_num_doc_identidad ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
                ejecutar = false;
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

            return _dataTable;
        }


        public DataTable ObtenerContratoSoloActivosConEntregasSinAprobar()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_num_doc_identidad_con_entregas_por_aprobar";

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


        public DataTable ObtenerContratoPorRazSocialSoloActivos(String RAZ_SOCIAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_raz_social ";

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "'";
            }
            else
            {
                MensajeError += "El campo RAZ_SOCIAL no puede ser nulo. \n";
                ejecutar = false;
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

            return _dataTable;
        }

        public DataTable ObtenerContratoPorREGISTRO(int REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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



        public DataTable ObtenerInformacionCompletaIdCiudadIdCentroCIdSubC(String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ciudad_ven_cc_empresas_ven_sub_centros_obtener_informacion_completa ";

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                sql += "null, ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + "";
            }
            else
            {
                sql += "null";
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

            return _dataTable;
        }


        public DataTable ObtenerPorNumIdentificacionTodos(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_por_num_id ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorNumIdentificacionActivo(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_Obtener_contrato_empleado_por_num_id ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
        public DataTable ObtenerContratosPorIdSolicitud(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_obtener_todos_contratos_por_id_solicitud_v2 ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", '" + Usuario + "'";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUID no puede ser vacio.";
                ejecutar = false;
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

            _dataTable.Columns.Add("CONTRATO_ESTADO");

            foreach (DataRow fila in _dataTable.Rows)
            {
                if ((fila["CONTRATO_VIGENTE"].ToString() == "S") && (fila["EMPLEADO_ACTIVO"].ToString() == "S"))
                {
                    fila["CONTRATO_ESTADO"] = "Activo";
                }
                else
                {
                    fila["CONTRATO_ESTADO"] = "Cerrado";
                }
            }

            return _dataTable;
        }



        public DataTable ObtenerDatosContratoParaAuditar(Decimal ID_EMPLEADO)
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_obtener_informacion_auditoria ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO no puede ser vacio.";
                ejecutar = false;
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

            return _dataTable;
        }


        public DataTable ObtenerNomEmpleadoPorIDSolicitudYFechaIngreso(int idSolicitud, DateTime fchIngreso)
        {
            tools _tools = new tools();

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtener_contrato_por_id_solicitud_fch_ingreso ";

            if (idSolicitud != 0)
            {
                sql += idSolicitud + ", ";
            }
            else
            {
                MensajeError += "El campo idSolicitud no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fchIngreso) + "'";

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


        #region NOM_EMPLEADOS
        public DataTable ObtenerEmpleadosActivosPorEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtener_activos_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
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

            return _dataTable;
        }



        public DataTable ObtenerEmpleadosActivosPorEmpresaSinUsuarioAsignado(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtener_activos_por_id_empresa_sin_usuario_asignado ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
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

            return _dataTable;
        }



        public DataTable ObtenerInformacionCotratoParaEntregas(Decimal ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerInformacionContratoPorIdEmpleado ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
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


        public DataTable ObtenerInformacionCotratosParaProximasEntregas()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerInformacionContratoParaProximasEntregas";

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


        public DataTable ObtenerInformacionContratosParaProximasEntregasFiltroPorEmpresa(Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerInformacionContratoParaProximasEntregas_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
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
        #endregion NOM_EMPLEADOS


        #endregion
    }
}
