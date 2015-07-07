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
    public class liquidarVacaciones
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
        public liquidarVacaciones(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerEmpleadosVacaciones(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_obtener_empleados_vacaciones ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + "";
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

        public Int32 TraerPeriodMemoVacaciones(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            Int32 _periodo = 0;

            sql = "usp_obtener_periodo_memorando_para_vacaciones ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";

            #endregion

            if (ejecutar)
            {
                try
                {
                    _periodo = Convert.ToInt32(conexion.ExecuteScalar(sql));
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
            return _periodo;
        }

        public DataTable LiquidarVacaciones(Int32 ID_EMPLEADO, String FECHA_INICIO, String FECHA_FIN, Int32 DIAS_DISF_PER_ANT, Int32 DIAS_LIQ_DINERO_PER_ANT,
            Int32 DIAS_DISF_PER_ACT, Int32 DIAS_LIQ_DINERO, String FECHA_SALIDA, String TIPO_PAGO, Int32 DIAS_NOMINA, Int32 ID_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_liquidar_periodo_vacacional ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_INICIO)))
            {
                sql += "'" + FECHA_INICIO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHA_INICIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_FIN)))
            {
                sql += "'" + FECHA_FIN.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHA_FIN es requerido para la consulta.";
                ejecutar = false;
            }

            if (DIAS_DISF_PER_ANT >= 0)
            {
                sql += DIAS_DISF_PER_ANT.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo DIAS_DISF_PER_ANT es requerido para la consulta.";
                ejecutar = false;
            }

            if (DIAS_LIQ_DINERO_PER_ANT >= 0)
            {
                sql += DIAS_LIQ_DINERO_PER_ANT.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo DIAS_LIQ_DINERO_PER_ANT es requerido para la consulta.";
                ejecutar = false;
            }

            if (DIAS_DISF_PER_ACT >= 0)
            {
                sql += DIAS_DISF_PER_ACT.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo DIAS_DISF_PER_ACT es requerido para la consulta.";
                ejecutar = false;
            }

            if (DIAS_LIQ_DINERO >= 0)
            {
                sql += DIAS_LIQ_DINERO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo DIAS_LIQ_DINERO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_SALIDA)))
            {
                sql += "'" + FECHA_SALIDA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHA_SALIDA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PAGO)))
            {
                sql += "'" + TIPO_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PAGO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += DIAS_NOMINA.ToString() + ", ";

            sql += "'" + Usuario + "' ";

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

        public DataTable ObtenerEmpleadoVacaciones(Decimal ID_EMPRESA, String CEDULA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleado_vacaciones ";

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

            sql += "'" + CEDULA.ToString() + "'";

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

        public DataTable ObtenerPeriodosVacaciones(Decimal ID_EMPRESA, String CEDULA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_periodos_vacaciones ";

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

            sql += "'" + CEDULA.ToString() + "'";

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


        public DataTable ObtenerUltimoPeriodoNomnaEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_trae_ultima_nomina_liquidada ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
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

        public Boolean GrabarVacaciones(Int32 ID_EMPLEADO, Decimal SALARIO, String FECHA_INICIO, String FECHA_TERMINA, String FECHA_SALIDA, String FECHA_REINTEGRO,
            Int32 DIAS_PERIODO, Int32 DIAS_DISFRUTADOS_PERIODO, Int32 DIAS_PAGADOS_PERIODO, Int32 DIAS_PENDIENTES_PERIODO, Int32 DIAS_DISFRUTADOS_PERIODOS_ANT,
            Int32 DIAS_PAGADOS_PERIODOS_ANT, Int32 DIAS_VACACIONES_MES_ACT, Int32 DIAS_VACACIONES_MES_SGTE, Decimal VALOR_ORDINARIAS_MES_ACT,
            Decimal VALOR_FESTIVOS_MES_SGTE, Decimal VALOR_ORDINARIAS_MES_SGTE, Decimal VALOR_FESTIVOS_MES_ACT, Decimal VALOR_DIAS_LIQ_DINERO_PER_ANT, String TIPO_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean guardado = false;
            Int32 _guardado = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_guardar_periodo_vacacional ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += SALARIO.ToString() + ", ";

            if (!(String.IsNullOrEmpty(FECHA_INICIO)))
            {
                sql += "'" + FECHA_INICIO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHA_INICIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_TERMINA)))
            {
                sql += "'" + FECHA_TERMINA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHA_TERMINA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_SALIDA)))
            {
                sql += "'" + FECHA_SALIDA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHA_SALIDA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_REINTEGRO)))
            {
                sql += "'" + FECHA_REINTEGRO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FECHA_REINTEGRO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += DIAS_PERIODO.ToString() + ", ";
            sql += DIAS_DISFRUTADOS_PERIODO.ToString() + ", ";
            sql += DIAS_PAGADOS_PERIODO.ToString() + ", ";
            sql += DIAS_PENDIENTES_PERIODO.ToString() + ", ";
            sql += DIAS_DISFRUTADOS_PERIODOS_ANT.ToString() + ", ";
            sql += DIAS_PAGADOS_PERIODOS_ANT.ToString() + ", ";
            sql += DIAS_VACACIONES_MES_ACT.ToString() + ", ";
            sql += DIAS_VACACIONES_MES_SGTE.ToString() + ", ";
            sql += VALOR_ORDINARIAS_MES_ACT.ToString() + ", ";
            sql += VALOR_FESTIVOS_MES_SGTE.ToString() + ", ";
            sql += VALOR_ORDINARIAS_MES_SGTE.ToString() + ", ";
            sql += VALOR_FESTIVOS_MES_ACT.ToString() + ", ";
            sql += VALOR_DIAS_LIQ_DINERO_PER_ANT.ToString() + ", ";
            if (!(String.IsNullOrEmpty(TIPO_PAGO)))
            {
                sql += "'" + TIPO_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PAGO es requerido para la consulta.";
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

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _guardado = Convert.ToInt32(conexion.ExecuteScalar(sql));
                    if (_guardado > 0) guardado = true;
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
            return guardado;
        }



        #endregion metodos
    }
}