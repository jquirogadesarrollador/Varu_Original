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

namespace Brainsbits.LLB.facturacion
{
    public class facturacion
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private String _tipo_provision = "";
        private String _tabla_detalle = "";
        private String _cod_contable = "";
        private Int32 _id_prestacional = 0;
        private Int32 _id_parafiscal = 0;
        private Int32 _id_seguridadsocial = 0;
        private Int32 _id_liq_nomina_detalle = 0;
        private Decimal _valor = 0;
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

        public Decimal _valorItem
        {
            get { return _valorItem; }
            set { _valorItem = value; }
        }

        public Decimal _aiuItem
        {
            get { return _aiuItem; }
            set { _aiuItem = value; }
        }

        public String _conceptoItem
        {
            get { return _conceptoItem; }
            set { _conceptoItem = value; }
        }

        public String _codigoItem
        {
            get { return _codigoItem; }
            set { _codigoItem = value; }
        }

        public String _baseIvaItem
        {
            get { return _baseIvaItem; }
            set { _baseIvaItem = value; }
        }

        public String _baseIcaItem
        {
            get { return _baseIcaItem; }
            set { _baseIcaItem = value; }
        }

        public String _Tipo_Provision
        {
            get { return _tipo_provision; }
            set { _tipo_provision = value; }
        }

        public String _Tabla_Detalle
        {
            get { return _tabla_detalle; }
            set { _tabla_detalle = value; }
        }

        public String _Cod_Contable
        {
            get { return _cod_contable; }
            set { _cod_contable = value; }
        }

        public Int32 _Id_Prestacional
        {
            get { return _id_prestacional; }
            set { _id_prestacional = value; }
        }

        public Int32 _Id_Parafiscal
        {
            get { return _id_parafiscal; }
            set { _id_parafiscal = value; }
        }

        public Int32 _Id_SeguridadSocial
        {
            get { return _id_seguridadsocial; }
            set { _id_seguridadsocial = value; }
        }

        public Int32 _Id_Liq_Nomina_Detalle
        {
            get { return _id_liq_nomina_detalle; }
            set { _id_liq_nomina_detalle = value; }
        }

        public Decimal _Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        #endregion propiedades

        #region constructores
        public facturacion(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerPeriodosFact(Decimal ID_EMPRESA, String NOMINAS, String MEMOS, String LPS, String SERVICIOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_periodos_facturacion ";

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

            if (!(String.IsNullOrEmpty(SERVICIOS)))
            {
                sql += "'" + SERVICIOS.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo SERVICIOS proceso es requerido para la consulta.";
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

        public DataTable ObtenerPeriodosCruce(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_periodos_cruce ";

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


        public DataTable ObtenerCiudadesFact(Decimal ID_EMPRESA, Int32 NUM_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_ciudades_facturacion ";

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

            if (NUM_PERIODO != 0)
            {
                sql += "'" + NUM_PERIODO + "'";
            }
            else
            {
                MensajeError = "El campo NUM_PERIODO es requerido para la consulta.";
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

        public DataTable ObtenerCentrosFact(Decimal ID_EMPRESA, String ID_CIUDAD, Int32 NUM_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_centros_facturacion ";

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

            if (NUM_PERIODO != 0)
            {
                sql += "'" + NUM_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NUN_PERIODO proceso es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD es requerido para la consulta.";
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

        public DataTable ObtenerSubCentrosFact(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO, Int32 NUM_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_subcentros_facturacion ";

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

            if (NUM_PERIODO != 0)
            {
                sql += "'" + NUM_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NUM_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_CENTRO > 0)
            {
                sql += Convert.ToInt32(ID_CENTRO).ToString();
            }
            else
            {
                MensajeError = "El campo ID_CENTRO es requerido para la consulta.";
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

        public DataTable ObtenerDatosFact(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO, String ID_SUBCENTRO, String PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            Decimal ID_SUB_C = 0;

            try
            {
                ID_SUB_C = Convert.ToDecimal(ID_SUBCENTRO);
            }
            catch
            {
                ID_SUB_C = 0;
            }

            sql = "usp_obtener_datos_facturar ";

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

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_CENTRO > 0)
            {
                sql += Convert.ToInt32(ID_CENTRO).ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CENTRO es requerido para la consulta.";
                ejecutar = false;
            }


            sql += Convert.ToInt32(ID_SUB_C).ToString() + ", ";


            if (!(String.IsNullOrEmpty(PERIODO)))
            {
                sql += "'" + PERIODO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
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

        public DataTable ObtenerDatosEmpresas(Decimal ID_EMPRESA, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empresas_facturacion ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
            }

            if (String.IsNullOrEmpty(TIPO) == false)
            {
                sql += "'" + TIPO.ToString() + "'";
            }
            else
            {
                sql += "null";
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

        public DataTable ObtenerEmpleadosPeriodo(Decimal ID_EMPRESA, Decimal ID_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleados_periodo_facturacion ";

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
                sql += ID_PERIODO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
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

        public DataTable ObtenerDatosEmpleadoFact(Decimal ID_PERIODO, String DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleados_facturar ";

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

        public DataTable CalcularPorEmpleados(Decimal ID_EMPRESA, String EMPLEADOSCALCULAR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_calcular_empleados ";

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

            if (!(String.IsNullOrEmpty(EMPLEADOSCALCULAR)))
            {
                sql += "'" + EMPLEADOSCALCULAR.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo EMPLEADOSCALCULAR es requerido para la consulta.";
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

        public DataTable CalcularPorPeriodos(Decimal ID_EMPRESA, String PERIODOSCALCULAR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_calcular_periodos ";

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

            if (!(String.IsNullOrEmpty(PERIODOSCALCULAR)))
            {
                sql += "'" + PERIODOSCALCULAR.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo PERIODOSCALCULAR es requerido para la consulta.";
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

        public DataTable ObtenerServiciosEspeciales()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_servicios_especiales ";

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


        public DataTable CalcularFacturas(Decimal ID_EMPRESA, String PERIODOS_CALCULAR, String EMPLEADOS_CALCULAR, String MODELO_FACTURA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_calcular_facturas "; 

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

            if (!(String.IsNullOrEmpty(PERIODOS_CALCULAR)))
            {
                sql += "'" + PERIODOS_CALCULAR.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo PERIODOS_CALCULAR es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + EMPLEADOS_CALCULAR.ToString() + "', ";

            if (!(String.IsNullOrEmpty(MODELO_FACTURA)))
            {
                sql += "'" + MODELO_FACTURA.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo MODELO_FACTURA es requerido para la consulta.";
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

        public DataTable CalcularCrucePeriodos(Decimal ID_EMPRESA, String PERIODOS_CALCULAR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_calcular_cruce_periodos_negativos ";

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

            if (!(String.IsNullOrEmpty(PERIODOS_CALCULAR)))
            {
                sql += "'" + PERIODOS_CALCULAR.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo PERIODOS_CALCULAR es requerido para la consulta.";
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

        public DataTable ObtenerfacturaIndividual(Decimal FACTURA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_factura_individual ";

            #region validaciones
            if (FACTURA > 0)
            {
                sql += FACTURA.ToString() + "";
            }
            else
            {
                MensajeError = "El campo FACTURA es requerido para la consulta.";
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

        public DataTable Facturar(Decimal ID_EMPRESA, String TIPO, String PROCESAR_PERIODOS, String PROCESAR_EMPLEADOS, String PROCESAR_TIPO, String TEXTO_ALIANZA,
            String TEXTO_CONCEPTO, String TEXTO_CUERPO_1, String TEXTO_CUERPO_2, String TEXTO_SERV_EXCLUIDOS, String FACT_PARAFISCALES, List<facturacion> ITEMS_FACTURAR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;
            Int32 identificador = 0;


            sql = "usp_facturar ";

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

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PROCESAR_PERIODOS)))
            {
                sql += "'" + PROCESAR_PERIODOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo PROCESAR_PERIODOS es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PROCESAR_EMPLEADOS.ToString() + "', ";

            if (!(String.IsNullOrEmpty(PROCESAR_TIPO)))
            {
                sql += "'" + PROCESAR_TIPO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo PROCESAR_TIPO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + TEXTO_ALIANZA.ToString() + "', ";
            sql += "'" + TEXTO_CONCEPTO.ToString() + "', ";
            sql += "'" + TEXTO_CUERPO_1.ToString() + "', ";
            sql += "'" + TEXTO_CUERPO_2.ToString() + "', ";
            sql += "'" + TEXTO_SERV_EXCLUIDOS.ToString() + "', ";
            sql += "'" + FACT_PARAFISCALES.ToString() + "', ";
            sql += identificador.ToString() + ", ";

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
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                    #region auditoria
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

        private void AdicionarItemsFacturacionEspecial(Int32 IDENTIFICADOR, String CONCEPTO, String CODIGO, Decimal VALOR, Decimal AIU, String BASE_IVA, String BASE_ICA, Conexion conexion)
        {

            String sql = null;
            sql = "usp_adicionar_items_facturacion_especial ";

            sql += IDENTIFICADOR.ToString() + ", '" + CONCEPTO + "', '" + CODIGO + "', " + VALOR.ToString() + ", " + AIU.ToString() + ", '" + BASE_IVA + "', '" + BASE_ICA + "'";
            try
            {
                conexion.ExecuteNonQuery(sql);
                #region auditoria
                #endregion auditoria
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
        }

        private void LimpiarItemsFacturacionEspecial(Int32 IDENTIFICADOR, Conexion conexion)
        {

            String sql = null;
            sql = "usp_eliminar_items_facturacion_especial ";

            sql += IDENTIFICADOR.ToString();
            try
            {
                conexion.ExecuteNonQuery(sql);
                #region auditoria
                #endregion auditoria
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
        }

        public Decimal Cruzar(Decimal ID_EMPRESA, String PROCESAR_PERIODOS)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            Decimal _notaDebito = 0;

            sql = "usp_cruzar_periodos_negativos ";

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

            if (!(String.IsNullOrEmpty(PROCESAR_PERIODOS)))
            {
                sql += "'" + PROCESAR_PERIODOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo PROCESAR_PERIODOS es requerido para la consulta.";
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
                    _notaDebito = Convert.ToDecimal(conexion.ExecuteNonQuery(sql).ToString());
                    #region auditoria
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
            return _notaDebito;
        }


        public Boolean AnularFactura(Decimal FACTURA)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Boolean anulado = false;

            sql = "usp_anular_factura ";

            #region validaciones
            if (FACTURA > 0)
            {
                sql += FACTURA.ToString() + ", ";
                informacion += "FACTURA = '" + FACTURA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo FACTURA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario;

            #endregion

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    #endregion auditoria

                    conexion.AceptarTransaccion();
                    anulado = true;
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

            return anulado;
        }

        public DataTable ObtenerConsecutivosFacturasImprimir()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_consecutivos_facturas_sin_imprimir ";

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

        public DataTable ObtenerNumerosFacturasImprimir(Decimal FACTURA_INICIAL, Decimal FACTURA_FINAL, String TEXTO_PIE_UNO, String TEXTO_PIE_DOS, String REIMPRIMIR,
            Int32 DIAS_VENCE, String FECHA, String PROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_facturas_para_imprimir ";

            #region validaciones
            if (FACTURA_INICIAL > 0)
            {
                sql += FACTURA_INICIAL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo FACTURA_INICIAL es requerido para la consulta.";
                ejecutar = false;
            }

            if (FACTURA_FINAL > 0)
            {
                sql += FACTURA_FINAL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo FACTURA_FINALL es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + TEXTO_PIE_UNO.ToString() + "', ";
            sql += "'" + TEXTO_PIE_DOS.ToString() + "', ";
            sql += "'" + REIMPRIMIR.ToString() + "', ";
            sql += DIAS_VENCE.ToString() + ", ";
            sql += "'" + FECHA.ToString() + "', ";
            sql += "'" + PROCESO.ToString() + "'";
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

        public DataTable ObtenerPieFacturas(Int32 FACTURA_INICIAL, Int32 FACTURA_FINAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_pies_de_facturas ";
            sql += FACTURA_INICIAL.ToString() + ", ";
            sql += FACTURA_FINAL.ToString() + "";

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

        public Boolean ImprimirFactura(Decimal FACTURA_INICIAL, Decimal FACTURA_FINAL, String TEXTO_PIE_UNO, String TEXTO_PIE_DOS)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Boolean impresa = false;

            sql = "usp_imprimir_factura ";

            #region validaciones
            if (FACTURA_INICIAL > 0)
            {
                sql += FACTURA_INICIAL.ToString() + ", ";
                informacion += "FACTURA>= '" + FACTURA_INICIAL.ToString() + "";
            }
            else
            {
                MensajeError = "El campo FACTURA_INICIAL es requerido para la consulta.";
                ejecutar = false;
            }

            if (FACTURA_FINAL > 0)
            {
                sql += FACTURA_FINAL.ToString() + "";
                informacion += " AND FACTURA<= '" + FACTURA_FINAL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo FACTURA_FINAL es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + TEXTO_PIE_UNO.ToString() + "'";
            sql += "'" + TEXTO_PIE_DOS.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    #endregion auditoria
                    conexion.AceptarTransaccion();
                    impresa = true;
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
            return impresa;
        }

        public DataTable CargarAlianzas(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_alianzas_facturacion ";

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

        public DataTable ObtenerPeriodosFacturacion()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_periodos_facturacion ";

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

        public DataTable ObtenerNuevoPeriodoFacturacion()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_nuevo_periodo_facturacion ";

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

        public String CerrarPeriodo()
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String cerrado = "N";

            sql = "usp_cerrar_periodo_facturacion ";

            #region validaciones
            sql += "'" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    conexion.AceptarTransaccion();
                    cerrado = "S";
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
            return cerrado;
        }

        public String AbrirPeriodo()
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String abierto = "N";

            sql = "usp_abrir_periodo_facturacion ";

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    conexion.AceptarTransaccion();
                    abierto = "S";
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
            return abierto;
        }

        public DataTable ObtenerPeriodosSinFacturar(Int32 ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_periodos_sin_facturar ";

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

        public DataTable ObtenerProvisionesAjustes(Int32 ID_EMPRESA, Int32 PERIODO, String PROVISION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_provisiones_ajuste ";

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

            if (PERIODO != 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + PROVISION + "'";

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

        public Int32 GuardarAjusteProvisiones(Int32 ID_EMPRESA, String FECHA, Decimal VALOR, String OBSERVACIONES, String CODCONTABLE, Int32 PERIODO, List<facturacion> AJUSTES, String PROVISION)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Int32 guardado = 0;
            sql = "usp_grabar_ajuste_provisiones ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + FECHA + "', " + VALOR.ToString() + ", '" + OBSERVACIONES + "', '" + CODCONTABLE + "', '" + Usuario + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    guardado = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());
                    #region auditoria
                    #endregion auditoria

                    if (AJUSTES.Count > 0)
                    {
                        foreach (facturacion ajuste in AJUSTES)
                        {
                            sql = "usp_grabar_ajuste_detalle " + guardado.ToString() + ", ";
                            sql += "'" + ajuste._Tipo_Provision + "', ";
                            sql += "'" + ajuste._Tabla_Detalle + "', ";
                            sql += ajuste._Id_Prestacional.ToString() + ", ";
                            sql += ajuste._Id_Parafiscal.ToString() + ", ";
                            sql += ajuste._Id_SeguridadSocial.ToString() + ", ";
                            sql += ajuste._Valor.ToString() + ", ";
                            sql += "'" + ajuste._Cod_Contable + "', ";
                            sql += ajuste._Id_Liq_Nomina_Detalle.ToString() + ", ";
                            sql += "'" + Usuario + "'";
                            conexion.ExecuteNonQuery(sql);
                        }
                    }
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
            return guardado;
        }

    }
        #endregion metodos
}