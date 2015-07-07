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
    public class liquidarCesantias
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
        public liquidarCesantias(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerCesantiasLiquidadas(Decimal ID_EMPRESA, String FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_cesantias_liquidadas ";

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

            sql += "'" + FECHA.ToString() + "'";

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

        public DataTable ObtenerCesantiasInforme(Decimal ID_EMPRESA, String FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "RPT_CESANTIAS_PLANILLA_LIQUIDACION ";

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

            sql += "'" + FECHA.ToString() + "'";

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


        public DataTable LiquidarCesantias(Decimal ID_EMPRESA, String FECHA, Decimal PORCENTAJE_CESANTIAS, Decimal PORCENTAJE_INTERESES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_liquidar_cesantias ";

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

            sql += "'" + FECHA.ToString() + "', ";

            if (PORCENTAJE_CESANTIAS > 0)
            {
                sql += PORCENTAJE_CESANTIAS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE_CESANTIAS es requerido para la consulta.";
                ejecutar = false;
            }

            if (PORCENTAJE_INTERESES > 0)
            {
                sql += PORCENTAJE_INTERESES.ToString() + "";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE_INTERESES es requerido para la consulta.";
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


        public String ObtenerEmpresaCesantias(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String _empresa = "";

            sql = "usp_traer_empresa ";

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

            if (ejecutar)
            {
                try
                {
                    _empresa = conexion.ExecuteScalar(sql);
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
            return _empresa;
        }

        public Int32 TraerPeriodMemoCesantias(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            Int32 _periodo = 0;

            sql = "usp_obtener_periodo_memorando_para_cesantias ";

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

        public Int32 GrabarRegistrosTempCesantias(Decimal ID_EMPRESA, Int32 ID_EMPLEADO, Decimal DEVENGADO, Decimal SALARIO, Decimal VALOR_CESANTIAS, Decimal VALOR_INTERESES, Int32 CONTROL)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            Int32 _grabado = 0;

            sql = "usp_grabar_registros_temporales_cesantias ";

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

            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            if (DEVENGADO > 0)
            {
                sql += DEVENGADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo DEVENGADO es requerido para la consulta.";
                ejecutar = false;
            }

            if (SALARIO > 0)
            {
                sql += SALARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo SALARIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR_CESANTIAS > 0)
            {
                sql += VALOR_CESANTIAS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR_CESANTIAS es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR_INTERESES > 0)
            {
                sql += VALOR_INTERESES.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR_INTERESES es requerido para la consulta.";
                ejecutar = false;
            }

            if (CONTROL > 0)
            {
                sql += CONTROL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo CONTROL es requerido para la consulta.";
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

            if (ejecutar)
            {
                try
                {
                    _grabado = Convert.ToInt32(conexion.ExecuteScalar(sql));
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
            return _grabado;
        }

        public Boolean GrabarRegistrosDefCesantias(Decimal ID_EMPRESA, Int32 PERIODO, String FECHA, String TIPO_PAGO, Int32 ID_CONCEPTO_CESANTIAS, Int32 ID_CONCEPTO_INTERESES, Int32 CONTROL)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Boolean grabado = false;

            sql = "usp_grabar_cesantias_definitiva ";

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

            sql += PERIODO.ToString() + ", ";

            sql += "'" + FECHA.ToString() + "', ";

            sql += "'" + TIPO_PAGO.ToString() + "', ";

            sql += ID_CONCEPTO_CESANTIAS.ToString() + ", ";

            sql += ID_CONCEPTO_INTERESES.ToString() + ", ";

            sql += CONTROL.ToString() + ", ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
                informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar)
            {
                try
                {
                    grabado = Convert.ToBoolean(conexion.ExecuteScalar(sql));
                    if (grabado == true)
                    {
                        #region auditoria
                        #endregion auditoria
                        conexion.AceptarTransaccion();
                    }
                    else
                        conexion.DeshacerTransaccion();
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
            return grabado;
        }

        public DataTable ObtenerPorcentajes()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_obtener_porcentajes ";

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

        public Int32 ObtenerLiquidacionesCesantias(Decimal ID_EMPRESA, String FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            Int32 _grabado = 0;

            sql = "usp_buscar_liquidacion_cesantias ";

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

            sql += "'" + FECHA.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    _grabado = Convert.ToInt32(conexion.ExecuteScalar(sql));
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
            return _grabado;
        }

        #endregion metodos
    }
}
