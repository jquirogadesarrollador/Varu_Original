using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.comercial;

namespace Brainsbits.LLB.nomina
{
    public class liquidacionNomina
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
        public liquidacionNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerNominasProceso(Decimal ID_EMPRESA, String FECHASCORTE, String FILTRO, String CAMPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_nomina_proceso_por_usuario_1 ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHASCORTE)))
            {
                sql += "'" + FECHASCORTE.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHASCORTE es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + FILTRO.ToString() + "', ";

            sql += "'" + CAMPO.ToString() + "' ";
            #endregion

            if (ejecutar == true)
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

        public DataTable ObtenerNominasLiquidadas(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_nominas_liquidadas ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar == true)
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

        public DataTable ObtenerEmpresasLiqNominas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_obtener_empresas_liq_nominas ";

            #region validaciones
            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
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

        public DataTable ObtenerEmpleadosLiqNomina(Int32 ID_LIQ_NOMINA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_obtener_empleados_reliq_nomina ";

            #region validaciones
            if (ID_LIQ_NOMINA > 0)
            {
                sql += "'" + ID_LIQ_NOMINA.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_LIQ_NOMINA es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar == true)
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

        public DataTable ObtenerEmpleadoLiquidado(String DOC_IDENTIDAD, Int32 ID_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_obtener_empleado_liquidado ";

            #region validaciones
            if (ID_PERIODO > 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DOC_IDENTIDAD)))
            {
                sql += "'" + DOC_IDENTIDAD.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo DOC_IDENTIDAD es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
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

        public String LiquidarNominasEmpresa(Decimal ID_EMPRESA, Int32 ID_USUARIO, String PERIODOSPROCESO, String TIPO_LIQUIDACION, String SIN_PAGO = "N", String PAGO_NOMINA = "N")
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            String liquidado = "N";

            sql = "usp_liquidacion_nomina_v2 ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_USUARIO > 0)
            {
                sql += ID_USUARIO.ToString() + ", ";
                informacion += "ID_USUARIO = '" + ID_USUARIO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
                informacion += "USU_CRE = '" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + PERIODOSPROCESO.ToString() + "', ";
            informacion += "PERIODOSPROCESO = '" + PERIODOSPROCESO.ToString() + "', ";

            if (!(String.IsNullOrEmpty(TIPO_LIQUIDACION)))
            {
                sql += "'" + TIPO_LIQUIDACION.ToString() + "', ";
                informacion += "TIPO_LIQUIDACION = '" + TIPO_LIQUIDACION.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_LIQUIDACION es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + SIN_PAGO + "', ";
            informacion += "SIN_PAGO = '" + SIN_PAGO.ToString() + "', ";

            sql += "'" + PAGO_NOMINA + "'";
            informacion += "PAGO_NOMINA = '" + PAGO_NOMINA.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    liquidado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_LIQUIDAR, sql, informacion, conexion);
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
            return liquidado;
        }

        public String ActualizarFechaPeriodo(Decimal ID_EMPRESA, String PERIODOSPROCESO, DateTime FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            String actualizado = "N";
            tools fecha = new tools();

            sql = "usp_actualizar_fecha_memorando ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PERIODOSPROCESO.ToString() + "', ";
            informacion += "PERIODOSPROCESO = '" + PERIODOSPROCESO.ToString() + "', ";

            sql += "'" + fecha.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + FECHA.ToString() + "', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    actualizado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_LIQUIDAR, sql, informacion, conexion);
                    #endregion auditoria
                    conexion.AceptarTransaccion();
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
            }
            return actualizado;
        }

        public String LimpiarPeriodosMemos(Decimal ID_EMPRESA, String PERIODOSPROCESO, DateTime FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            String actualizado = "N";
            tools fecha = new tools();

            sql = "usp_limpiar_periodos_memos ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PERIODOSPROCESO.ToString() + "', ";
            informacion += "PERIODOSPROCESO = '" + PERIODOSPROCESO.ToString() + "', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    actualizado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_LIQUIDAR, sql, informacion, conexion);
                    #endregion auditoria
                    conexion.AceptarTransaccion();
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
            }
            return actualizado;
        }

        public String LiquidarNominaPeriodo(Decimal ID_EMPRESA, Int32 ID_PERIODO, Decimal ID_EMPLEADO, String TIPO_LIQUIDACION, Int32 DIAS_PARA_CALCULAR)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            sql = "usp_liquidar_nomina_periodo ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            sql += ID_EMPLEADO.ToString() + ", ";

            sql += "'" + TIPO_LIQUIDACION.ToString() + "', ";

            sql += DIAS_PARA_CALCULAR.ToString() + "";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    conexion.AceptarTransaccion();
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
            }
            return "S";
        }

        public String ReliquidarNominaEmpleados(Decimal ID_PERIODO, Decimal ID_USUARIO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String reLiquidado = "S";

            sql = "usp_reliquidar_nomina_empleados ";

            #region validaciones
            sql += ID_PERIODO.ToString() + ", ";

            if (ID_USUARIO > 0)
            {
                sql += ID_USUARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PERIODOSPROCESO.ToString() + "'";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    reLiquidado = "N";
                    MensajeError = e.Message;
                    conexion.DeshacerTransaccion();
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
                reLiquidado = "N";

            return reLiquidado;
        }

        public String ReliquidarNominaEmpresaTotal(Int32 ID_PERIODO, Decimal ID_EMPRESA, Decimal ID_USUARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            String Reliquidado = "N";

            sql = "usp_reliquidar_nomina_empresa ";

            #region validaciones
            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + ", ";
                informacion += "ID_PERIODO= '" + ID_PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_USUARIO > 0)
            {
                sql += ID_USUARIO.ToString() + ", ";
                informacion += "ID_USUARIO= '" + ID_USUARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }
            informacion += "Usuario = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    Reliquidado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_RELIQUIDAR, sql, informacion, conexion);
                    #endregion auditoria
                    conexion.AceptarTransaccion();
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
            }
            return Reliquidado;
        }

        public DataTable ObtenerReliquidar(Int32 ID_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleados_reliquidar ";

            #region validaciones
            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
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

        public String MarcadoReliquidar(Int32 ID_PERIODO, Decimal ID_EMPLEADO, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_actualizar_empleados_reliquidar ";

            #region validaciones
            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Tipo es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
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
            return "S";
        }

        public String MarcadoReliquidarTodos(Int32 ID_PERIODO, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_actualizar_empleados_reliquidar_todos ";

            #region validaciones
            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + TIPO + "' ";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
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
            return "S";
        }

        public String ValidarNomina(Decimal ID_EMPRESA, Int32 ID_PERIODO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            String Validado = "N";
            String informacion = null;

            sql = "usp_validar_nomina ";

            #region validaciones
            sql += ID_PERIODO.ToString() + ", ";
            informacion = "ID_PERIODO ='" + ID_PERIODO.ToString() + "', ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA ='" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
                informacion += "Usuario ='" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + PERIODOSPROCESO.ToString() + "'";
            informacion += "PERIODOSPROCESO ='" + PERIODOSPROCESO.ToString() + "";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    Validado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_VALIDAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    Validado = "N";
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return Validado;
        }

        public String PagarNomina(Decimal ID_EMPRESA, Int32 ID_PERIODO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_pagar_nomina ";

            #region validaciones
            sql += ID_PERIODO.ToString() + ", ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + PERIODOSPROCESO + "'";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
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
            return "S";
        }

        public String Autorizar_Alertas(Decimal ID_EMPRESA, Int32 ID_PERIODO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            String Autorizado = "N";
            String informacion = null;

            sql = "usp_autorizar_alertas ";

            #region validaciones
            sql += ID_PERIODO.ToString() + ", ";
            informacion = "ID_PERIODO ='" + ID_PERIODO.ToString() + "', ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA ='" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
                informacion += "Usuario ='" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + PERIODOSPROCESO + "'";
            informacion += "PERIODOSPROCESO = '" + PERIODOSPROCESO + "'";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    Autorizado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_AUTORIZAR, sql, informacion, conexion);
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
            return Autorizado;
        }

        public String EliminarNomina(Decimal ID_EMPRESA, Int32 ID_PERIODO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            String Eliminado = "N";
            String informacion = null;

            sql = "usp_eliminar_nomina ";

            #region validaciones
            sql += ID_PERIODO.ToString() + ", ";
            informacion = "ID_PERIODO ='" + ID_PERIODO.ToString() + "', ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA ='" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PERIODOSPROCESO + "'";
            informacion += "PERIODOSNOMINA = '" + "',";
            informacion += "Usuario ='" + Usuario.ToString() + "'";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    Eliminado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
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
            return Eliminado;
        }

        public String ReversarNomina(Decimal ID_EMPRESA, String ESTADO, Int32 ID_PERIODO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            String Reversado = "N";
            String informacion = null;

            sql = "usp_reversar_nomina ";

            #region validaciones
            sql += "'" + ID_PERIODO.ToString() + "', ";
            informacion = "ID_PERIODO ='" + ID_PERIODO.ToString() + "', ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA ='" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }


            sql += "'" + ESTADO + "', ";
            informacion += "ESTADO ='" + ESTADO.ToString() + "', ";

            sql += "'" + PERIODOSPROCESO + "'";
            informacion += "PERIODOSPROCESO ='" + PERIODOSPROCESO + "', ";
            informacion += "Usuario ='" + Usuario.ToString() + "'";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    Reversado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_REVERSAR, sql, informacion, conexion);
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
            return Reversado;
        }

        public DataTable ObtenerNombreEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_nombre_empresa ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
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

        public DataTable ObtenerEmpleadosNomina(Decimal ID_PERIODO, String TIPO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleados_nomina ";

            #region validaciones
            sql += ID_PERIODO.ToString() + "";
            sql += ", '" + TIPO + "' ";
            sql += ",'" + PERIODOSPROCESO + "'";
            #endregion

            if (ejecutar == true)
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

        public DataTable ObtenerNominasCorte(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_fechaCierre_nomina_1 ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";

            #endregion


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

        public DataTable ObtenerNovedadesNominaIdPeriodo(Int32 ID_PERIODO, Decimal ID_EMPRESA, String PeriodosProceso)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_nomina_novedades_obtener_todos ";

            #region validaciones
            sql += ID_PERIODO.ToString() + ", ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PeriodosProceso + "'";

            #endregion

            if (ejecutar == true)
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

        public String DesbloquearNomina(Int32 ID_PERIODO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            String DesBloqueado = "N";
            String informacion = null;

            sql = "usp_desbloquear_nomina ";

            #region validaciones
            sql += ID_PERIODO.ToString() + " ";
            informacion = "ID_PERIODO ='" + ID_PERIODO.ToString() + "', ";

            sql += "'" + PERIODOSPROCESO + "'";
            informacion += "PERIODOSPROCESO = '" + "', ";
            informacion += "Usuario ='" + Usuario.ToString() + "'";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    DesBloqueado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_DESBLOQUEAR, sql, informacion, conexion);
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
            return DesBloqueado;
        }


        public DataTable ObtenerDatosFiltro(Decimal ID_EMPRESA, String FILTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_filtro_nomina ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FILTRO)))
            {
                sql += "'" + FILTRO + "'";
            }
            else
            {
                MensajeError = "El campo FILTRO es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

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

        public DataTable ObtenerEmpresasParaValidacionMasiva()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_validar_periodos_masivo_lista_de_periodos";

            #region validaciones
            #endregion

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

        #endregion metodos
    }
}
