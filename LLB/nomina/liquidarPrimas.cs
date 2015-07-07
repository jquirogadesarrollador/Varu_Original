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
    public class liquidarPrimas
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
        public liquidarPrimas(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerEmpleadosPrima(Decimal ID_EMPRESA, String FECHA_LIQUIDACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_obtener_empleados_prima ";

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

            sql += "'" + FECHA_LIQUIDACION + "'";

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

        public Int32 TraerPeriodMemoPrima(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            Int32 _periodo = 0;

            sql = "usp_obtener_periodo_memorando_para_prima ";

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

        public Int32 LimpiarRegistrosTempPrima(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            Int32 _grabado = 0;

            sql = "usp_limpiar_registros_temporales_primas ";

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


        public Int32 GrabarRegistrosTempPrima(Decimal ID_EMPRESA, Int32 ID_EMPLEADO, Int32 DIAS, Decimal BASE, Decimal VALOR, Int32 ID_CONCEPTO, Int32 REGISTROS, Decimal SALARIO, Int32 AUSENCIAS)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            Int32 _grabado = 0;

            sql = "usp_grabar_registros_temporales_primas ";

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

            if (DIAS > 0)
            {
                sql += DIAS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo DIAS es requerido para la consulta.";
                ejecutar = false;
            }

            if (BASE > 0)
            {
                sql += BASE.ToString().Replace(',', '.') + ", ";
            }
            else
            {
                MensajeError = "El campo BASE es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR > 0)
            {
                sql += VALOR.ToString().Replace(',', '.') + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_CONCEPTO > 0)
            {
                sql += ID_CONCEPTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += REGISTROS.ToString() + ", ";
            sql += SALARIO.ToString().Replace(',', '.') + ", ";
            sql += AUSENCIAS.ToString() + ", ";
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

        public Boolean GrabarRegistrosDefPrima(Decimal ID_EMPRESA, String FECHA_CORTE, Int32 DIAS_BASE, String TIPO_PRIMA, String TIPO_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Boolean grabado = false;

            sql = "usp_grabar_prima_definitiva ";

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

            sql += "'" + FECHA_CORTE.ToString() + "', ";

            sql += DIAS_BASE.ToString() + ", ";

            sql += "'" + TIPO_PRIMA.ToString() + "', ";
            sql += "'" + TIPO_PAGO.ToString() + "', ";

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

        public DataTable LiquidarPrima(Decimal ID_EMPRESA, String EMPLEADOS_EXCLUIR, String FECHA_PRIMA, Int32 DIAS_BASE, String TIPO_PRIMA, String TIPO_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_liquidar_prima ";

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

            sql += "'" + EMPLEADOS_EXCLUIR.ToString() + "', ";
            sql += "'" + FECHA_PRIMA.ToString() + "', ";
            sql += DIAS_BASE.ToString() + ", ";
            sql += "'" + TIPO_PRIMA.ToString() + "', ";
            sql += "'" + TIPO_PAGO.ToString() + "'";

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

        public DataTable ValidarPeriodosLiqPrimas(String TIPO_PRIMA, String EMPLEADOS, Int32 ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_validar_periodos_primas ";

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

            sql += "'" + EMPLEADOS.ToString() + "', ";
            sql += "'" + TIPO_PRIMA.ToString() + "'";

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


        public String ObtenerEmpresaPrima(Decimal ID_EMPRESA)
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


        public DataTable ObtenerEmpleadoExcluir(Decimal ID_EMPRESA, String CEDULA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleado_excluir ";

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


        #endregion metodos
    }
}