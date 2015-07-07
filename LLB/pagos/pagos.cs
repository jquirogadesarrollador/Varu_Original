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

namespace Brainsbits.LLB.pagos
{
    public class pagos
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private Int32 _num_recaja = 0;
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
        public pagos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerPagosCorte(String NOMINAS, String MEMOS, String LPS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_fechaPago_nomina ";

            #region validaciones
            if (!(String.IsNullOrEmpty(NOMINAS)))
            {
                sql += "'" + NOMINAS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NOMINASPROCESO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MEMOS)))
            {
                sql += "'" + MEMOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo MEMOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LPS)))
            {
                sql += "'" + LPS.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo LPS proceso es requerido para la consulta.";
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

        public DataTable ObtenerBancosPagos(String FECHASPROCESO, String NOMINAS, String MEMOS, String LPS, String TIPO_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_obtener_Bancos_Pagos ";

            #region validaciones
            if (!(String.IsNullOrEmpty(FECHASPROCESO)))
            {
                sql += "'" + FECHASPROCESO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo fechas proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMINAS)))
            {
                sql += "'" + NOMINAS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NOMINASPROCESO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MEMOS)))
            {
                sql += "'" + MEMOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo MEMOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LPS)))
            {
                sql += "'" + LPS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo LPS proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PAGO)))
            {
                sql += "'" + TIPO_PAGO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo TIPO_PAGO proceso es requerido para la consulta.";
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

        public DataTable ObtenerBancosPagos(String FECHASPROCESO, String NOMINAS, String MEMOS, String LPS, String TIPO_PAGO, Int32 ID_CUENTA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_obtener_Bancos_Pagos_Cuenta ";

            #region validaciones
            if (!(String.IsNullOrEmpty(FECHASPROCESO)))
            {
                sql += "'" + FECHASPROCESO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo fechas proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMINAS)))
            {
                sql += "'" + NOMINAS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NOMINASPROCESO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MEMOS)))
            {
                sql += "'" + MEMOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo MEMOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LPS)))
            {
                sql += "'" + LPS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo LPS proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PAGO)))
            {
                sql += "'" + TIPO_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PAGO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_CUENTA > 0)
            {
                sql += ID_CUENTA.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_CUENTA proceso es requerido para la consulta.";
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

        public DataTable ObtenerEmpresasPagos(String FECHASPROCESO, String NOMINAS, String MEMOS, String LPS, Decimal ID_BANCO, String TIPO_PAGO, String PAGO_HEL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            if (TIPO_PAGO == "DISPERSION")
            {
                sql = "usp_obtener_Empresas_Pagos_Dispersion ";
            }
            else
            {
                if (TIPO_PAGO == "ACH")
                {
                    sql = "usp_obtener_Empresas_Pagos_ACH ";
                }
                else
                {
                    sql = "usp_obtener_Empresas_Pagos_VARIOS ";
                }
            }
            #region validaciones
            if (!(String.IsNullOrEmpty(FECHASPROCESO)))
            {
                sql += "'" + FECHASPROCESO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo fechas proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMINAS)))
            {
                sql += "'" + NOMINAS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NOMINASPROCESO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MEMOS)))
            {
                sql += "'" + MEMOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo MEMOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LPS)))
            {
                sql += "'" + LPS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo LPS proceso es requerido para la consulta.";
                ejecutar = false;
            }

            sql += ID_BANCO.ToString() + ", ";

            if (!(String.IsNullOrEmpty(TIPO_PAGO)))
            {
                sql += "'" + TIPO_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PAGO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PAGO_HEL.ToString() + "'";

            #endregion validaciones

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





        public DataTable ObtenerEmpleadoPagoIndividual(String CEDULA, String PERIODOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "ObtenerEmpleadoPagoIndividual ";
            #region validaciones
            if (!(String.IsNullOrEmpty(CEDULA)))
            {
                sql += "'" + CEDULA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo CEDULA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PERIODOS)))
            {
                sql += "'" + PERIODOS.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo PERIODOS es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion validaciones

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

        public DataTable ObtenerBancosCuentasEmpresa()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_Bancos_Cuentas_Empresa ";

            #region validaciones
            #endregion validaciones

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

        public DataTable ObtenerCuentasEmpresaPorBanco(Int32 ID_BANCO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_Cuentas_Empresa_Por_Banco ";

            #region validaciones
            if (ID_BANCO > 0)
            {
                sql += "'" + ID_BANCO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_BANCO es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion validaciones

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

        public DataTable ObtenerPeriodosPagos(String FECHASPROCESO, String NOMINAS, String MEMOS, String LPS, Decimal ID_BANCO, Decimal ID_EMPRESA, String TIPO_PAGO, String HELBANK)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_Periodos_Pagos ";

            #region validaciones
            if (!(String.IsNullOrEmpty(FECHASPROCESO)))
            {
                sql += "'" + FECHASPROCESO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo fechas proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMINAS)))
            {
                sql += "'" + NOMINAS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NOMINASPROCESO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MEMOS)))
            {
                sql += "'" + MEMOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo MEMOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LPS)))
            {
                sql += "'" + LPS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo LPS proceso es requerido para la consulta.";
                ejecutar = false;
            }

            sql += ID_BANCO.ToString() + ", ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PAGO)))
            {
                sql += "'" + TIPO_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PAGO proceso es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + HELBANK.ToString() + "'";
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

        public DataTable ObtenerCuentasBancos(Decimal ID_BANCO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_Cuentas_Bancarias ";

            #region validaciones
            if (ID_BANCO > 0)
            {
                sql += ID_BANCO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_BANCO es requerido para la consulta.";
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

        public DataTable ObtenerDatosPagos(Decimal ID_BANCO, Int32 ID_PERIODO, String ID_EMPLEADOS, Int32 ID_CUENTA, String FORMA_PAGO, String PAGO_HEL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_pagos ";

            #region validaciones
            if (ID_BANCO > 0)
            {
                sql += ID_BANCO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_BANCO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += ID_PERIODO.ToString() + ", ";

            sql += "'" + ID_EMPLEADOS + "', ";

            if (ID_CUENTA > 0)
            {
                sql += ID_CUENTA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CUENTA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + FORMA_PAGO + "', '" + PAGO_HEL + "'";
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

        public DataTable ObtenerDatosPagosResumen(Decimal ID_BANCO, String PERIODOS, String ID_EMPLEADOS, Int32 ID_CUENTA, String FORMA_PAGO, String PAGO_HEL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_pagos_resumen ";

            #region validaciones
            sql += ID_BANCO.ToString() + ", ";

            sql += "'" + PERIODOS.ToString() + "', ";

            sql += "'" + ID_EMPLEADOS + "', ";

            if (ID_CUENTA > 0)
            {
                sql += ID_CUENTA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CUENTA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + FORMA_PAGO + "', '" + PAGO_HEL + "'";
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

        public DataTable ObtenerDetallePagos(Int32 PERIODO, Int32 BANCO, Int32 CUENTA, String FORMA_PAGO, String PAGO_HEL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_pagos_detalle ";

            #region validaciones
            if (PERIODO > 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += BANCO.ToString() + ", ";
            sql += CUENTA + ", ";


            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += "'" + FORMA_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FORMA_PAGO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PAGO_HEL + "'";
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

        public DataTable ObtenerDetallePagosResumen(String PERIODOS, Int32 BANCO, Int32 CUENTA, String FORMA_PAGO, String PAGO_HEL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_pagos_detalle_resumen ";

            #region validaciones
            if (!(String.IsNullOrEmpty(PERIODOS)))
            {
                sql += "'" + PERIODOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo PERIODOS proceso es requerido para la consulta.";
                ejecutar = false;
            }
            sql += BANCO.ToString() + ", ";
            sql += CUENTA + ", ";


            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += "'" + FORMA_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo FORMA_PAGO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PAGO_HEL + "'";
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



        public DataTable ObtenerEmpleadosPeriodo(Int32 ID_PERIODO, Decimal ID_BANCO, String TIPO, String PAGO_ESPECIAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleados_periodo ";

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

            if (ID_BANCO > 0)
            {
                sql += ID_BANCO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_BANCO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + TIPO.ToString() + "', ";
            sql += "'" + PAGO_ESPECIAL.ToString() + "'";
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


        public DataTable ObtenerDatoPagoEmpleado(Int32 ID_LIQ_NOMINA_EMPLEADOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_dato_pago_empleado ";

            #region validaciones
            if (ID_LIQ_NOMINA_EMPLEADOS > 0)
            {
                sql += ID_LIQ_NOMINA_EMPLEADOS.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_LIQ_NOMINA_EMPLEADOS es requerido para la consulta.";
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

        public DataTable ObtenerDatosPeriodo(Int32 ID_PERIODO, Decimal ID_BANCO, Int32 ID_CUENTA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_periodo ";

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

            if (ID_BANCO > 0)
            {
                sql += ID_BANCO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_BANCO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_CUENTA > 0)
            {
                sql += ID_CUENTA.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_CUENTA es requerido para la consulta.";
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

        public Int32 CrearPagoTempo(Int32 ID_PAGO_TEMPO, Decimal BANCO, Int32 CUENTA, Int32 ID_PERIODO, String ID_EMPLEADOS)
        {
            Conexion conexion = new Conexion(Empresa);
            Int32 Creados = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crear_pago_tempo ";

            #region validaciones
            sql += ID_PAGO_TEMPO.ToString() + ", ";

            if (BANCO > 0)
            {
                sql += BANCO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo Banco es requerido para el pago.";
                ejecutar = false;
            }

            if (CUENTA > 0)
            {
                sql += CUENTA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo Cuenta es requerido para el pago.";
                ejecutar = false;
            }

            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
            }

            if (!(String.IsNullOrEmpty(ID_EMPLEADOS)))
            {
                sql += "'" + ID_EMPLEADOS.ToString() + "', ";
            }
            else
            {
                sql += "'', ";
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
                    conexion.IniciarTransaccion();
                    Creados = Convert.ToInt32(conexion.ExecuteScalar(sql));
                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return Creados;
        }

        public String GenerarPagos(Int32 ID_PAGO_TEMPO, String FORMA_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            sql = "usp_generar_pago_nominas ";

            #region validaciones
            if (ID_PAGO_TEMPO > 0)
            {
                sql += ID_PAGO_TEMPO.ToString();
                informacion = "ID_PAGO_TEMPO=" + ID_PAGO_TEMPO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PAGO_TEMPO es requerido para el proceso de pagos.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += ", '" + FORMA_PAGO.ToString() + "'";
                informacion += "FORMA_PAGO=" + FORMA_PAGO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo FORMA_PAGO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += ", '" + Usuario.ToString() + "'";
                informacion += "Usuario=" + Usuario.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Empresa)))
            {
                sql += ", '" + Empresa.ToString() + "', ";
                informacion += "Empresa=" + Empresa.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            if (Empresa == "1")
            {
                sql += "'" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + "', ";
                sql += "'" + tabla.VAR_DIGITO_VER_SERTEMPO.ToString() + "', ";
                sql += "'" + tabla.VAR_NOMBRE_SERTEMPO.ToString() + "'";
                informacion += "VAR_NIT_SERTEMPO_CONTABLE=" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + ", ";
                informacion += "VAR_DIGITO_VER_SERTEMPO=" + tabla.VAR_DIGITO_VER_SERTEMPO.ToString() + ", ";
                informacion += "VAR_NOMBRE_SERTEMPO=" + tabla.VAR_NOMBRE_SERTEMPO.ToString() + "";
            }
            if (Empresa == "2")
            {
                sql += "'" + tabla.VAR_NIT_EYS_CONTABLE.ToString() + "', ";
                sql += "'" + tabla.VAR_DIGITO_VER_EYS.ToString() + "'";
                sql += "'" + tabla.VAR_NOMBRE_EYS.ToString() + "'";
                informacion += "VAR_NIT_SERTEMPO_CONTABLE=" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + ", ";
                informacion += "VAR_DIGITO_VER_SERTEMPO=" + tabla.VAR_DIGITO_VER_SERTEMPO.ToString() + ", ";
                informacion += "VAR_NOMBRE_SERTEMPO=" + tabla.VAR_NOMBRE_SERTEMPO.ToString() + "";
            }
            #endregion

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql); 
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAGOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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
            if (_dataTable.Rows.Count > 0)
            {
                DataRow row = _dataTable.Rows[0];
                return Convert.ToString(row["ARCHIVO"]);
            }
            else
            {
                return "";
            }
        }

        public String GenerarPagos(Int32 ID_BANCO, Int32 ID_CUENTA, String FORMA_PAGO, String PERIODOS_PAGO, String EMPLEADOS_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            sql = "usp_generar_pagos ";

            #region validaciones
            if (ID_BANCO > 0)
            {
                sql += ID_BANCO.ToString() + ", ";
                informacion = "ID_BANCO=" + ID_BANCO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_BANCO es requerido para el proceso de pagos.";
                ejecutar = false;
            }

            if (ID_CUENTA > 0)
            {
                sql += ID_CUENTA.ToString() + ", ";
                informacion = "ID_CUENTA=" + ID_CUENTA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CUENTA es requerido para el proceso de pagos.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += "'" + FORMA_PAGO.ToString() + "', ";
                informacion += "FORMA_PAGO=" + FORMA_PAGO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo FORMA_PAGO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PERIODOS_PAGO)))
            {
                sql += "'" + PERIODOS_PAGO.ToString() + "', ";
                informacion += "PERIODOS_PAGO=" + PERIODOS_PAGO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODOS_PAGO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EMPLEADOS_PAGO)))
            {
                sql += "'" + EMPLEADOS_PAGO.ToString() + "', ";
                informacion += "EMPLEADOS_PAGO=" + EMPLEADOS_PAGO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo EMPLEADOS_PAGO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
                informacion += "Usuario=" + Usuario.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Empresa)))
            {
                sql += "'" + Empresa.ToString() + "', ";
                informacion += "Empresa=" + Empresa.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo Empresa es requerido para la consulta.";
                ejecutar = false;
            }

            if (Empresa == "1")
            {
                sql += "'" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + "', ";
                sql += "'" + tabla.VAR_DIGITO_VER_SERTEMPO.ToString() + "', ";
                sql += "'" + tabla.VAR_NOMBRE_SERTEMPO.ToString() + "'";
                informacion += "VAR_NIT_SERTEMPO_CONTABLE=" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + ", ";
                informacion += "VAR_DIGITO_VER_SERTEMPO=" + tabla.VAR_DIGITO_VER_SERTEMPO.ToString() + ", ";
                informacion += "VAR_NOMBRE_SERTEMPO=" + tabla.VAR_NOMBRE_SERTEMPO.ToString() + "";
            }
            if (Empresa == "2")
            {
                sql += "'" + tabla.VAR_NIT_EYS_CONTABLE.ToString() + "', ";
                sql += "'" + tabla.VAR_DIGITO_VER_EYS.ToString() + "'";
                sql += "'" + tabla.VAR_NOMBRE_EYS.ToString() + "'";
                informacion += "VAR_NIT_SERTEMPO_CONTABLE=" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + ", ";
                informacion += "VAR_DIGITO_VER_SERTEMPO=" + tabla.VAR_DIGITO_VER_SERTEMPO.ToString() + ", ";
                informacion += "VAR_NOMBRE_SERTEMPO=" + tabla.VAR_NOMBRE_SERTEMPO.ToString() + "";
            }
            #endregion

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAGOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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
            if (_dataTable.Rows.Count > 0)
            {
                DataRow row = _dataTable.Rows[0];
                return Convert.ToString(row["ARCHIVO"]);
            }
            else
            {
                return "";
            }
        }



        public Int32 EliminarPagoTempo(Int32 ID_PAGO_TEMPO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_eliminar_pago_tempo ";

            #region validaciones
            if (ID_PAGO_TEMPO > 0)
            {
                sql += ID_PAGO_TEMPO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_PAGO_TEMPO es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar)
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
            return 0;
        }

        public DataTable ObtenerDatosArchivoPlanoPagos(String ARCHIVO_PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_plano_pagos ";

            #region validaciones
            if (!(String.IsNullOrEmpty(ARCHIVO_PLANO)))
            {
                sql += "'" + ARCHIVO_PLANO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ARCHIVO_PLANO es requerido para la consulta.";
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

        public DataTable ObtenerDatosHojaTrabajoPagos(String FILTRO, String VALOR, String ANIO, String MES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_datos_hoja_trabajo_pagos ";

            sql += "'" + FILTRO + "', '" + VALOR + "', '" + ANIO + "', '" + MES + "'";

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
            return _dataTable;
        }

        public DataTable ObtenerListaRechazos(Int32 ID_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_rechazos_lista ";

            sql += ID_PAGO.ToString() + "";

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
            return _dataTable;
        }

        public DataTable ObtenerDetallePago(Int32 ID_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_detalles_pago ";

            sql += ID_PAGO.ToString() + "";

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
            return _dataTable;
        }

        public DataTable ObtenerAniosPagos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_anios_pagos ";

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
            return _dataTable;
        }

        public Int32 ReversarPago(Int32 ID_PAGOS, String OBSERVACIONES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;
            Int32 Reversado = 0;
            String informacion = null;
            String mensaje = null;


            sql = "usp_reversar_pago ";

            #region validaciones

            if (ID_PAGOS > 0)
            {
                sql += ID_PAGOS.ToString() + ", ";
                informacion = "ID_PAGOS ='" + ID_PAGOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PAGOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (OBSERVACIONES.Length >= 20)
            {
                sql += "'" + OBSERVACIONES.ToString() + "', ";
                informacion = "OBSERVACIONES ='" + ID_PAGOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "Usuario ='" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    Decimal VALOR_PAGO = 0;
                    String sqlPago = "usp_obtener_valor_pago " + ID_PAGOS.ToString() + "";

                    _dataSet = conexion.ExecuteReader(sqlPago);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    if (_dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow fila in _dataTable.Rows)
                        {
                            VALOR_PAGO = Convert.ToDecimal(fila["VALOR_TOTAL"].ToString());
                        }
                    }

                    conexion.IniciarTransaccion();
                    Reversado = conexion.ExecuteNonQuery(sql);
                    if (Reversado > 0)
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        _auditoria.Adicionar(Usuario, tabla.PAGOS, tabla.ACCION_REVERSAR, sql, informacion, conexion);
                        #endregion auditoria
                        conexion.AceptarTransaccion();

                        sql = "usp_obtener_usuarios_mail_pagos ";

                        _dataSet = conexion.ExecuteReader(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _dataTable = _dataView.Table;

                        if (_dataTable.Rows.Count > 0)
                        {
                            tools _tools = new tools();
                            foreach (DataRow filaInfoUsuario in _dataTable.Rows)
                            {
                                mensaje = "El usuario " + Usuario.ToString() + " ha realizado una reversion al pago numero " + ID_PAGOS.ToString() + "";
                                mensaje += "<br />";
                                mensaje += "Por valor de " + VALOR_PAGO;
                                mensaje += "<br />";
                                mensaje += "Con la siguiente justificacion: " + OBSERVACIONES;
                                mensaje += "<br />";
                                mensaje += "<br />";
                                mensaje += "Por favor no responder a este correo, es un correo de administración del Sistema.";
                                mensaje += "<br />";
                                mensaje += "<br />";
                                mensaje += "<br />";
                                mensaje += "<B>SISER WEB</B>";
                                _tools.enviarCorreoConCuerpoHtml(filaInfoUsuario["USU_MAIL"].ToString(), "Reversion de un pago en liquidacion de SISER WEB", mensaje);
                            }
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    Reversado = 0;
                    conexion.DeshacerTransaccion();
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return Reversado;
        }

        public DataTable ObtenerCausalesRechazos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_causales_rechazos ";

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
            return _dataTable;
        }

        public DataTable ObtenerEmpleadoRechazo(Int32 ID_PAGOS, String CEDULA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleado_para_rechazo ";

            #region validaciones

            if (ID_PAGOS > 0)
            {
                sql += ID_PAGOS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PAGOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (CEDULA.Length > 0)
            {
                sql += "'" + CEDULA.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo CEDULA es requerido para la consulta.";
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

        public Int32 GuardarRechazo(Int32 ID_PAGOS, Int32 ID_EMPLEADO, Decimal VALOR, String CAUSAL, String OBSERVACIONES, Int32 ID_PAGOS_EMPLEADOS)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            Int32 Rechazado = 0;
            String informacion = null;
            tools _tools = new tools();

            sql = "usp_agregar_rechazo ";

            #region validaciones

            if (ID_PAGOS > 0)
            {
                sql += ID_PAGOS.ToString() + ", ";
                informacion = "ID_PAGOS ='" + ID_PAGOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PAGOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
                informacion = "ID_EMPLEADO ='" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR > 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR).ToString() + ", ";
                informacion = "VALOR ='" + VALOR.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo VALOR es requerido para la consulta.";
                ejecutar = false;
            }

            if (CAUSAL.Length > 0)
            {
                sql += "'" + CAUSAL.ToString() + "', ";
                informacion = "CAUSAL ='" + CAUSAL.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo CAUSAL es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + OBSERVACIONES.ToString() + "', ";
            informacion = "OBSERVACIONES ='" + OBSERVACIONES.ToString() + "', ";

            if (ID_PAGOS_EMPLEADOS > 0)
            {
                sql += ID_PAGOS_EMPLEADOS.ToString() + ", ";
                informacion = "ID_PAGOS_EMPLEADOS ='" + ID_PAGOS_EMPLEADOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PAGOS_EMPLEADOS es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "Usuario ='" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    Rechazado = conexion.ExecuteNonQuery(sql);
                    if (Rechazado > 0)
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        _auditoria.Adicionar(Usuario, tabla.PAGOS, tabla.ACCION_RECHAZAR, sql, informacion, conexion);
                        #endregion auditoria
                        conexion.AceptarTransaccion();
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
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
            }
            return Rechazado;
        }

        public Int32 ActivarRechazo(Int32 ID_RECHAZO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            Int32 Rechazado = 0;
            String informacion = null;

            sql = "usp_reactivar_rechazo_empleado ";

            #region validaciones

            if (ID_RECHAZO > 0)
            {
                sql += ID_RECHAZO.ToString() + ", ";
                informacion = "ID_RECHAZO ='" + ID_RECHAZO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_RECHAZO es requerido para la consulta.";
                ejecutar = false;
            }


            sql += "'" + Usuario + "'";
            informacion += "Usuario ='" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    Rechazado = conexion.ExecuteNonQuery(sql);
                    if (Rechazado > 0)
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        _auditoria.Adicionar(Usuario, tabla.PAGOS, tabla.ACCION_REACTIVAR_RECHAZO, sql, informacion, conexion);
                        #endregion auditoria
                        conexion.AceptarTransaccion();
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
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
            }
            return Rechazado;
        }

        #endregion metodos
    }
}