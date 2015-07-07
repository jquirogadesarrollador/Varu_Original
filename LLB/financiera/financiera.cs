using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
namespace Brainsbits.LLB.financiera
{
    public class financiera
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        private Int32 _num_factura = 0;
        private Decimal _valor_abono = 0;
        private String _tipo_docomento = "";
        private String _cod_contable = "";
        private String _id_ciudad = "";
        private Int32 _id_centro_c = 0;
        private Int32 _id_sub_c = 0;

        private Decimal _valor_adicional = 0;
        private String _codigo_adicional = "";
        private String _concepto_adicional = "";
        private String _aplicacion_adicional = "";

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

        public Int32 Num_Factura
        {
            get { return _num_factura; }
            set { _num_factura = value; }
        }

        public Decimal Valor_Abono
        {
            get { return _valor_abono; }
            set { _valor_abono = value; }
        }

        public String Tipo_Documento
        {
            get { return _tipo_docomento; }
            set { _tipo_docomento = value; }
        }

        public String Cod_Contable
        {
            get { return _cod_contable; }
            set { _cod_contable = value; }
        }

        public String ID_CIUDAD
        {
            get { return _id_ciudad; }
            set { _id_ciudad = value; }
        }

        public Int32 ID_CENTRO_C
        {
            get { return _id_centro_c; }
            set { _id_centro_c = value; }
        }

        public Int32 ID_SUB_C
        {
            get { return _id_sub_c; }
            set { _id_sub_c = value; }
        }

        public Decimal ValorAdicional
        {
            get { return _valor_adicional; }
            set { _valor_adicional = value; }
        }

        public String CodigoAdicional
        {
            get { return _codigo_adicional; }
            set { _codigo_adicional = value; }
        }

        public String ConceptoAdicional
        {
            get { return _concepto_adicional; }
            set { _concepto_adicional = value; }
        }

        public String AplicacionAdicional
        {
            get { return _aplicacion_adicional; }
            set { _aplicacion_adicional = value; }
        }

        #endregion propiedades

        #region constructores
        public financiera(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerCarteraGeneral()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_datos_cartera_general ";

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

        public DataTable ObtenerFacturasEmpresa(Int32 ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_facturas_empresa ";

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

        public DataTable ObtenerDatosFactura(Int32 ID_FACTURA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_datos_factura ";

            #region validaciones
            if (ID_FACTURA > 0)
            {
                sql += ID_FACTURA.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_FACTURA es requerido para la consulta.";
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

        public DataTable ObtenerBancos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_bancos ";

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

        public DataTable ObtenerParametrosFinanciera()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_parametros_financieros ";

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

        public DataTable ObtenerCuentasPorBanco(Int32 BANCO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_cuentas_por_banco ";

            sql += BANCO.ToString() + "";
        #endregion
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

        public Int32 GuardarComprobante(Int32 ID_EMPRESA, String FECHA, String FORMA, Int32 BANCO, String CUENTA, Decimal VALOR, List<financiera> ABONOS, List<financiera> ADICIONALES, String NUM_CHEQUE)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Int32 guardado = 0;
            sql = "usp_grabar_comprobante_pago_factura ";

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

            sql += "'" + FECHA + "', '" + FORMA + "', " + BANCO.ToString() + ", '" + CUENTA + "', " + VALOR.ToString() + ", '" + NUM_CHEQUE.ToString() + "', '" + Usuario + "' ";

            #endregion

            if (ejecutar)
            {
                try
                {
                    guardado = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());
                    #region auditoria
                    #endregion auditoria

                    if (ABONOS.Count > 0)
                    {
                        foreach (financiera abono in ABONOS)
                        {
                            sql = "usp_grabar_comprobante_pago_factura_detalle " + guardado.ToString() + ", " + abono.Num_Factura.ToString() + ", " + abono.Valor_Abono.ToString() + ",'RECAJA', '" + abono.Tipo_Documento + "', '', '', '', '" + Usuario + "'";
                            conexion.ExecuteNonQuery(sql);
                        }
                    }

                    if (ADICIONALES.Count > 0)
                    {
                        foreach (financiera adicional in ADICIONALES)
                        {
                            sql = "usp_grabar_comprobante_pago_factura_detalle " + guardado.ToString() + ", " + "0" + ", " + adicional.ValorAdicional.ToString() + ",'RECAJA', '" + "Adicional" + "', '" + adicional.CodigoAdicional + "', '" + adicional.ConceptoAdicional + "', '" + adicional.AplicacionAdicional + "', '" + Usuario + "'";
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

        public DataTable ObtenerDocumentosNotaContable(Int32 ID_EMPRESA, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_documentos_por_empresa ";

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

            sql += "'" + TIPO + "'";
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

        public DataTable ObtenerDatosDetalleDocumento(Int32 NUM_DOCUMENTO, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_detalle_documento_numero_tipo ";

            #region validaciones
            if (NUM_DOCUMENTO > 0)
            {
                sql += NUM_DOCUMENTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo NUM_DOCUMENTO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + TIPO + "'";
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

        public Int32 GuardarNotaContable(Int32 ID_EMPRESA, String FECHA, String TIPO, Decimal VALOR, String OBSERVACIONES, String CODCONTABLE, Int32 ID_PERIODO, Int32 FACTURA, List<financiera> ABONOS_NOTA, Int32 _CONSECUTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Int32 guardado = 0;
            Int32 _factura_abono = 0;
            Decimal _valor_abono = 0;
            sql = "usp_grabar_nota_contable ";

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

            sql += "'" + FECHA + "', '" + TIPO + "', " + VALOR.ToString() + ", '" + OBSERVACIONES + "', '" + CODCONTABLE + "', " + _CONSECUTIVO.ToString() + ", '" + Usuario + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    guardado = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());
                    #region auditoria
                    #endregion auditoria

                    if (ABONOS_NOTA.Count > 0)
                    {
                        foreach (financiera abono in ABONOS_NOTA)
                        {
                            _factura_abono = Convert.ToInt32(abono.Num_Factura);
                            _valor_abono = Convert.ToDecimal(abono.Valor_Abono);
                            sql = "usp_grabar_nota_contable_detalle " + guardado.ToString() + ", " + abono.Num_Factura.ToString() + ", '"
                                + abono.Tipo_Documento + "', '" + abono.Cod_Contable + "', '" + abono.ID_CIUDAD + "', " + abono.ID_CENTRO_C.ToString() + ", " + abono.ID_SUB_C.ToString() + ", "
                                + abono.Valor_Abono.ToString() + ", " + ID_PERIODO.ToString() + ", '" + Usuario + "'";
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

        public DataTable ObtenerCodigoContableFiltrado(String DATO, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_cod_contable_obtener_por_filtro ";

            #region validaciones
            if (!(String.IsNullOrEmpty(DATO)))
            {
                sql += "'" + DATO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo DATO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + TIPO + "'";
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

        public DataTable ObtenerConsecutivosRecibosdeCajaImprimir()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_consecutivos_recibos_de_caja_sin_imprimir ";

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

        public DataTable ObtenerNumerosRecibosdeCajaImprimir(Decimal FACTURA_INICIAL, Decimal FACTURA_FINAL, String TEXTO_PIE_UNO, String TEXTO_PIE_DOS, String REIMPRIMIR,
            Int32 DIAS_VENCE, String FECHA, String PROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_recibos_de_caja_para_imprimir ";

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

        public DataTable ObtenerCupoFinanciero(Decimal ID_CUPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_cupo_financiero_por_id ";

            #region validaciones
            if (ID_CUPO > 0)
            {
                sql += ID_CUPO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_CUPO es requerido para la consulta.";
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

        public Int32 guardar_cupo_financiero(Int32 ANIO, Int32 MES, Decimal VALOR, Int32 BANCO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Int32 guardado = 0;
            sql = "usp_insertar_cupo_financiero ";

            #region validaciones
            if (ANIO > 0)
            {
                sql += ANIO.ToString() + ", ";
                informacion += "ANIO= " + ANIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ANIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (MES > 0)
            {
                sql += MES.ToString() + ", ";
                informacion += "MES= " + MES.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo MES es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR > 0)
            {
                sql += VALOR.ToString() + ", ";
                informacion += "VALOR= " + ANIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR es requerido para la consulta.";
                ejecutar = false;
            }

            if (BANCO > 0)
            {
                sql += BANCO.ToString() + ", ";
                informacion += "BANCO= " + BANCO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo BANCO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";

            #endregion

            if (ejecutar)
            {
                try
                {
                    guardado = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());
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
            return guardado;

        }

        public DataTable ObtenerCuposFinancieros()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_cupos_financieros ";

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

        public DataTable ObtenerTransportadoras()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_transportadoras ";

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

        public Int32 guardar_radicaciones(Decimal FACTURA, String FECHA, Decimal TRANSPORTADORA, String GUIA)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Int32 guardado = 0;
            sql = "usp_insertar_radicaciones ";

            #region validaciones
            if (FACTURA > 0)
            {
                sql += FACTURA.ToString() + ", ";
                informacion += "FACTURA= " + FACTURA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo FACTURA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + FECHA + "', " + TRANSPORTADORA + ", '" + GUIA + "', '" + Usuario + "'";
            informacion += "FECHA= " + FECHA.ToString() + ", TRANSPORTADORA=" + TRANSPORTADORA.ToString() + ", GUIA=" + GUIA.ToString() + ", '" + Usuario + "'";

            #endregion

            if (ejecutar)
            {
                try
                {
                    guardado = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());
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
            return guardado;
        }

        public Int32 actualizar_radicaciones(Decimal ID_RADICACION, Decimal FACTURA, String FECHA, Decimal TRANSPORTADORA, String GUIA)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;
            Int32 guardado = 0;
            sql = "usp_actualizar_radicaciones ";

            #region validaciones
            if (ID_RADICACION > 0)
            {
                sql += ID_RADICACION.ToString() + ", ";
                informacion += "ID_RADICACION= " + ID_RADICACION.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_RADICACION es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + FECHA + "', " + TRANSPORTADORA + ", '" + GUIA + "', '" + Usuario + "'";
            informacion += "FECHA= " + FECHA.ToString() + ", TRANSPORTADORA=" + TRANSPORTADORA.ToString() + ", GUIA=" + GUIA.ToString() + "";

            #endregion

            if (ejecutar)
            {
                try
                {
                    guardado = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());
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
            return guardado;
        }

        public Int32 anular_radicaciones(Decimal ID_RADICACION)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Boolean ejecutar = true;
            String sql = null;
            Int32 guardado = 0;
            sql = "usp_radicaciones_anular ";

            #region validaciones
            if (ID_RADICACION > 0)
            {
                sql += ID_RADICACION.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_RADICACION es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar)
            {
                try
                {
                    guardado = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());
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
            return guardado;
        }

        public DataTable ObtenerRadicaciones(Decimal FACTURA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_radicaciones ";

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

        public DataTable ObtenerFactura(Decimal FACTURA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_factura_radicaciones ";

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
    }
}
