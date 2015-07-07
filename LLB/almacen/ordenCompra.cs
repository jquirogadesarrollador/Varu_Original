using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.almacen
{
    public class ordenCompra
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private int _CANTIDAD = 0;
        private Decimal _ID_PRODUCTO = 0;
        private String _REFERENCIA = null;
        private String _TALLA = null;
        private String _DESCRIPCION = null;
        private Decimal _VALOR_UNIDAD = 0;
        private Decimal _VALOR_TOTAL = 0;
        private Decimal _DESCUENTO = 0;
        private Decimal _IVA_APLICADO = 0;

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

        public int CANTIDAD
        {
            get { return _CANTIDAD; }
            set { _CANTIDAD = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _ID_PRODUCTO; }
            set { _ID_PRODUCTO = value; }
        }

        public String REFERENCIA
        {
            get { return _REFERENCIA; }
            set { _REFERENCIA = value; }
        }

        public String TALLA
        {
            get { return _TALLA; }
            set { _TALLA = value; }
        }

        public String DESCRIPCION
        {
            get { return _DESCRIPCION; }
            set { _DESCRIPCION = value; }
        }

        public Decimal VALOR_UNIDAD
        {
            get { return _VALOR_UNIDAD; }
            set { _VALOR_UNIDAD = value; }
        }

        public Decimal VALOR_TOTAL
        {
            get { return _VALOR_TOTAL; }
            set { _VALOR_TOTAL = value; }
        }

        public Decimal DESCUENTO
        {
            get { return _DESCUENTO; }
            set { _DESCUENTO = value; }
        }

        public Decimal IVA_APLICADO
        {
            get { return _IVA_APLICADO; }
            set { _IVA_APLICADO = value; }
        }
        #endregion propiedades

        #region constructores
        public ordenCompra(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region alm_orden_compra

        public DataTable ObtenerOrdenesCompraAbiertas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_ORDEN_COMPRA_OBTENER_ABIERTAS ";

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

        public DataTable ObtenerOrdenCompraPorId(Decimal ID_ORDEN)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_ORDEN_COMPRA_OBTENER_POR_ID ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
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

        public DataTable ObtenerNombresUsuarioTramitoAutorizoOrdenCompra(Decimal ID_ORDEN)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_orden_compra_obtener_nombres_usuarios_tramito_autorizo ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
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

        public Decimal AdicionarAlmOrdenCompra(DateTime FECHA,
            Decimal ID_EMPRESA,
            String ARTICULO,
            String MOTIVO,
            String PROCESO_AREA,
            Decimal ID_PROVEEDOR,
            int PERIODOENTREGA,
            int GARANTIA,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String CONDICIONES_ENTREGA,
            Decimal SUBTOTAL,
            Decimal DESCUENTO_APLICADO,
            Decimal IVA_APLICADO,
            Decimal VALOR_TOTAL,
            String ESPECIFICACIONES_TECNICAS,
            String ESTADO,
            String OBSERVACIONES,
            List<ordenCompra> listaDetalles,
            Byte[] ARCHIVO_COTIZACION,
            Int32 ARCHIVO_COTIZACION_TAMANO,
            String ARCHIVO_COTIZACION_EXTENSION,
            String ARCHIVO_COTIZACION_TYPE,
            String NOMBRE_SOLICITO,
            String CARGO_SOLICITO,
            String TIPO_COMPRA,
            String FACTURAR_A)
        {
            Decimal ID_ORDEN = 0;
            Decimal ID_ARCHIVO = 0;
            Decimal ID_DETALLE = 0;
            Decimal ID_REF_PRODUCTO_PROVEEDOR = 0;
            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_ORDEN = AdicionarOrdenComra(FECHA, ID_EMPRESA, ARTICULO, MOTIVO, PROCESO_AREA, ID_PROVEEDOR, PERIODOENTREGA, GARANTIA, FORMA_PAGO, ID_ENTIDAD_BANCARIA, CUENTA_BANCARIA, CONDICIONES_ENTREGA, SUBTOTAL, DESCUENTO_APLICADO, IVA_APLICADO, VALOR_TOTAL, ESPECIFICACIONES_TECNICAS, ESTADO, OBSERVACIONES, NOMBRE_SOLICITO, CARGO_SOLICITO, TIPO_COMPRA, FACTURAR_A, conexion);

                if (ID_ORDEN <= 0)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = MensajeError;
                    verificador = false;
                }
                else
                {
                    foreach (ordenCompra detalle in listaDetalles)
                    {
                        ID_DETALLE = AdicionarDetalleOrdenCompra(ID_ORDEN, detalle.ID_PRODUCTO, detalle.REFERENCIA, detalle.TALLA, detalle.DESCRIPCION, detalle.CANTIDAD, detalle.VALOR_UNIDAD, detalle.VALOR_TOTAL, detalle.DESCUENTO, detalle.IVA_APLICADO, conexion);

                        if (ID_DETALLE <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = MensajeError;
                            verificador = false;
                            break;
                        }
                        else
                        {
                            ID_REF_PRODUCTO_PROVEEDOR = verificarReferenciaProductoProveedor(ID_PROVEEDOR, detalle.ID_PRODUCTO, detalle.REFERENCIA, conexion);

                            if (ID_REF_PRODUCTO_PROVEEDOR <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = MensajeError;
                                verificador = false;
                                break;
                            }
                        }
                    }

                    if (verificador == true)
                    {
                        if (ARCHIVO_COTIZACION != null)
                        {
                            ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaArchivoCotizacionOrdenCompra(ID_ORDEN, FECHA, ARCHIVO_COTIZACION, ARCHIVO_COTIZACION_EXTENSION, ARCHIVO_COTIZACION_TAMANO, ARCHIVO_COTIZACION_TYPE, Usuario));

                            if (ID_ARCHIVO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = MensajeError;
                                verificador = false;
                            }
                        }
                    }

                    if (verificador == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (verificador == true)
            {
                return ID_ORDEN;
            }
            else
            {
                return 0;
            }
        }

        public Decimal AdicionarOrdenComra(DateTime FECHA,
            Decimal ID_EMPRESA,
            String ARTICULO,
            String MOTIVO,
            String PROCESO_AREA,
            Decimal ID_PROVEEDOR_PARAM,
            int PERIODOENTREGA,
            int GARANTIA,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String CONDICIONES_ENTREGA,
            Decimal SUBTOTAL,
            Decimal DESCUENTO_APLICADO,
            Decimal IVA_APLICADO,
            Decimal VALOR_TOTAL,
            String ESPECIFICACIONES_TECNICAS,
            String ESTADO,
            String OBSERVACIONES,
            String NOMBRE_SOLICITO,
            String CARGO_SOLICITO,
            String TIPO_COMPRA,
            String FACTURAR_A,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_ORDEN_COMPRA_ADICIONAR ";

            #region validaciones
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ARTICULO)))
            {
                sql += "'" + ARTICULO + "', ";
                informacion += "ARTICULO = '" + ARTICULO + "', ";
            }
            else
            {
                MensajeError += "El campo ARTICULO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOTIVO)))
            {
                sql += "'" + MOTIVO + "', ";
                informacion += "MOTIVO = '" + MOTIVO + "', ";
            }
            else
            {
                MensajeError += "El campo MOTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PROCESO_AREA)))
            {
                sql += "'" + PROCESO_AREA + "', ";
                informacion += "PROCESO_AREA = '" + PROCESO_AREA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PROCESO_AREA = 'NULL', ";
            }

            if (ID_PROVEEDOR_PARAM != 0)
            {
                sql += ID_PROVEEDOR_PARAM + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR_PARAM + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            sql += PERIODOENTREGA + ", ";
            informacion += "PERIODOENTREGA = '" + PERIODOENTREGA + "', ";

            sql += GARANTIA + ", ";
            informacion += "GARANTIA = '" + GARANTIA + "', ";

            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += "'" + FORMA_PAGO + "', ";
                informacion += "FORMA_PAGO = '" + FORMA_PAGO + "', ";
            }
            else
            {
                MensajeError += "El campo FORMA_PAGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ENTIDAD_BANCARIA != 0)
            {
                sql += ID_ENTIDAD_BANCARIA + ", ";
                informacion += "ID_ENTIDAD_BANCARIA = '" + ID_ENTIDAD_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_ENTIDAD_BANCARIA = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CUENTA_BANCARIA)))
            {
                sql += "'" + CUENTA_BANCARIA + "', ";
                informacion += "CUENTA_BANCARIA = '" + CUENTA_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CUENTA_BANCARIA = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CONDICIONES_ENTREGA)))
            {
                sql += "'" + CONDICIONES_ENTREGA + "', ";
                informacion += "CONDICIONES_ENTREGA = '" + CONDICIONES_ENTREGA + "', ";
            }
            else
            {
                MensajeError += "El campo CONDICIONES_ENTREGA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SUBTOTAL) + ", ";
            informacion += "SUBTOTAL = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(SUBTOTAL) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(DESCUENTO_APLICADO) + ", ";
            informacion += "DESCUENTO_APLICADO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(DESCUENTO_APLICADO) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA_APLICADO) + ", ";
            informacion += "IVA_APLICADO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA_APLICADO) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_TOTAL) + ", ";
            informacion += "VALOR_TOTAL = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_TOTAL) + "', ";

            if (!(String.IsNullOrEmpty(ESPECIFICACIONES_TECNICAS)))
            {
                sql += "'" + ESPECIFICACIONES_TECNICAS + "', ";
                informacion += "ESPECIFICACIONES_TECNICAS = '" + ESPECIFICACIONES_TECNICAS + "', ";
            }
            else
            {
                MensajeError += "El campo ESPECIFICACIONES_TECNICAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ESTADO = 'NULL', ";
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

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(NOMBRE_SOLICITO) == false)
            {
                sql += "'" + NOMBRE_SOLICITO + "', ";
                informacion += "NOMBRE_SOLICITO = '" + NOMBRE_SOLICITO + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_SOLICITO no puede ser nulo.\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CARGO_SOLICITO) == false)
            {
                sql += "'" + CARGO_SOLICITO + "', ";
                informacion += "CARGO_SOLICITO = '" + CARGO_SOLICITO + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO_SOLICITO no puede ser nulo.\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_COMPRA) == false)
            {
                sql += "'" + TIPO_COMPRA + "', ";
                informacion += "TIPO_COMPRA = '" + TIPO_COMPRA + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_COMPRA no puede ser nulo.\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(FACTURAR_A) == false)
            {
                sql += "'" + FACTURAR_A + "'";
                informacion += "FACTURAR_A = '" + FACTURAR_A + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "FACTURAR_A = 'NULL'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_ORDEN_COMPRA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Boolean ActualizarAlmOrdenCompra(Decimal ID_ORDEN,
            DateTime FECHA,
            String ARTICULO,
            String MOTIVO,
            String PROCESO_AREA,
            Decimal ID_EMPRESA,
            Decimal ID_PROVEEDOR_PARAM,
            int PERIODOENTREGA,
            int GARANTIA,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String CONDICIONES_ENTREGA,
            Decimal SUBTOTAL,
            Decimal DESCUENTO_APLICADO,
            Decimal IVA_APLICADO,
            Decimal VALOR_TOTAL,
            String ESPECIFICACIONES_TECNICAS,
            String OBSERVACIONES,
            List<ordenCompra> listaDetalles,
            String TIPO_COMPRA,
            String NOMBRE_SOLICITO,
            String CARGO_SOLICITO,
            String FACTURAR_A)
        {
            Boolean ordenActualizada = false;
            Decimal ID_DETALLE = 0;
            Decimal ID_REF_PRODUCTO_PROVEEDOR = 0;
            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ordenActualizada = ActualizarOrdenCompra(ID_ORDEN, FECHA, ARTICULO, MOTIVO, PROCESO_AREA, ID_EMPRESA, ID_PROVEEDOR_PARAM, PERIODOENTREGA, GARANTIA, FORMA_PAGO, ID_ENTIDAD_BANCARIA, CUENTA_BANCARIA, CONDICIONES_ENTREGA, SUBTOTAL, DESCUENTO_APLICADO, IVA_APLICADO, VALOR_TOTAL, ESPECIFICACIONES_TECNICAS, OBSERVACIONES, TIPO_COMPRA, NOMBRE_SOLICITO, CARGO_SOLICITO, FACTURAR_A, conexion);

                if (ordenActualizada == false)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = MensajeError;
                    verificador = false;
                }
                else
                {
                    if (eliminarDetallesDeOrdenCompra(ID_ORDEN, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = MensajeError;
                        verificador = false;
                    }
                    else
                    {
                        foreach (ordenCompra detalle in listaDetalles)
                        {
                            ID_DETALLE = AdicionarDetalleOrdenCompra(ID_ORDEN, detalle.ID_PRODUCTO, detalle.REFERENCIA, detalle.TALLA, detalle.DESCRIPCION, detalle.CANTIDAD, detalle.VALOR_UNIDAD, detalle.VALOR_TOTAL, detalle.DESCUENTO, detalle.IVA_APLICADO, conexion);

                            if (ID_DETALLE <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = MensajeError;
                                verificador = false;
                                break;
                            }
                            else
                            {
                                ID_REF_PRODUCTO_PROVEEDOR = verificarReferenciaProductoProveedor(ID_PROVEEDOR_PARAM, detalle.ID_PRODUCTO, detalle.REFERENCIA, conexion);

                                if (ID_REF_PRODUCTO_PROVEEDOR <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = MensajeError;
                                    verificador = false;
                                    break;
                                }
                            }
                        }

                        if (verificador == true)
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (verificador == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public Boolean ActualizarOrdenCompra(Decimal ID_ORDEN,
            DateTime FECHA,
            String ARTICULO,
            String MOTIVO,
            String PROCESO_AREA,
            Decimal ID_EMPRESA,
            Decimal ID_PROVEEDOR_PARAM,
            int PERIODOENTREGA,
            int GARANTIA,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String CONDICIONES_ENTREGA,
            Decimal SUBTOTAL,
            Decimal DESCUENTO_APLICADO,
            Decimal IVA_APLICADO,
            Decimal VALOR_TOTAL,
            String ESPECIFICACIONES_TECNICAS,
            String OBSERVACIONES,
            String TIPO_COMPRA,
            String NOMBRE_SOLICITO,
            String CARGO_SOLICITO,
            String FACTURAR_A,
            Conexion conexion)
        {
            String sql = null;
            int numRegistrosAfectados = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_ORDEN_COMPRA_ACTUALIZAR ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            if (!(String.IsNullOrEmpty(ARTICULO)))
            {
                sql += "'" + ARTICULO + "', ";
                informacion += "ARTICULO = '" + ARTICULO + "', ";
            }
            else
            {
                MensajeError += "El campo ARTICULO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOTIVO)))
            {
                sql += "'" + MOTIVO + "', ";
                informacion += "MOTIVO = '" + MOTIVO + "', ";
            }
            else
            {
                MensajeError += "El campo MOTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PROCESO_AREA)))
            {
                sql += "'" + PROCESO_AREA + "', ";
                informacion += "PROCESO_AREA = '" + PROCESO_AREA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PROCESO_AREA = 'NULL', ";
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PROVEEDOR_PARAM != 0)
            {
                sql += ID_PROVEEDOR_PARAM + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR_PARAM + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            sql += PERIODOENTREGA + ", ";
            informacion += "PERIODOENTREGA = '" + PERIODOENTREGA + "', ";

            sql += GARANTIA + ", ";
            informacion += "GARANTIA = '" + GARANTIA + "', ";

            if (!(String.IsNullOrEmpty(FORMA_PAGO)))
            {
                sql += "'" + FORMA_PAGO + "', ";
                informacion += "FORMA_PAGO = '" + FORMA_PAGO + "', ";
            }
            else
            {
                MensajeError += "El campo FORMA_PAGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ENTIDAD_BANCARIA != 0)
            {
                sql += ID_ENTIDAD_BANCARIA + ", ";
                informacion += "ID_ENTIDAD_BANCARIA = '" + ID_ENTIDAD_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_ENTIDAD_BANCARIA = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CUENTA_BANCARIA)))
            {
                sql += "'" + CUENTA_BANCARIA + "', ";
                informacion += "CUENTA_BANCARIA = '" + CUENTA_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CUENTA_BANCARIA = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(CONDICIONES_ENTREGA)))
            {
                sql += "'" + CONDICIONES_ENTREGA + "', ";
                informacion += "CONDICIONES_ENTREGA = '" + CONDICIONES_ENTREGA + "', ";
            }
            else
            {
                MensajeError += "El campo CONDICIONES_ENTREGA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SUBTOTAL) + ", ";
            informacion += "SUBTOTAL = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(SUBTOTAL) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(DESCUENTO_APLICADO) + ", ";
            informacion += "DESCUENTO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(DESCUENTO_APLICADO) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA_APLICADO) + ", ";
            informacion += "IVA_APLICADO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA_APLICADO) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_TOTAL) + ", ";
            informacion += "VALOR_TOTAL = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_TOTAL) + "', ";

            if (!(String.IsNullOrEmpty(ESPECIFICACIONES_TECNICAS)))
            {
                sql += "'" + ESPECIFICACIONES_TECNICAS + "', ";
                informacion += "ESPECIFICACIONES_TECNICAS = '" + ESPECIFICACIONES_TECNICAS + "', ";
            }
            else
            {
                MensajeError += "El campo ESPECIFICACIONES_TECNICAS no puede ser nulo\n";
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

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(TIPO_COMPRA)))
            {
                sql += "'" + TIPO_COMPRA + "', ";
                informacion += "TIPO_COMPRA = '" + TIPO_COMPRA + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_COMPRA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRE_SOLICITO)))
            {
                sql += "'" + NOMBRE_SOLICITO + "', ";
                informacion += "NOMBRE_SOLICITO = '" + NOMBRE_SOLICITO + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_SOLICITO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO_SOLICITO)))
            {
                sql += "'" + CARGO_SOLICITO + "', ";
                informacion += "CARGO_SOLICITO = '" + CARGO_SOLICITO + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO_SOLICITO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FACTURAR_A)))
            {
                sql += "'" + FACTURAR_A + "'";
                informacion += "FACTURAR_A = '" + FACTURAR_A + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "FACTURAR_A = 'NULL'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_ORDEN_COMPRA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    numRegistrosAfectados = 0;
                }
            }

            if (numRegistrosAfectados > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ActualizarUsuarioActualizoOrdenCompra(Decimal ID_ORDEN,
            String USU_LOG_AUTORIZO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_NEGOCIACION_PROVEEDOR_ACTUALIZAR_ID_USUARIO_AUTORIZA ";

            #region validaciones

            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ORDEN no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(USU_LOG_AUTORIZO) == false)
            {
                sql += "'" + USU_LOG_AUTORIZO + "', ";
                informacion += "USU_LOG_AUTORIZO = '" + USU_LOG_AUTORIZO + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG_AUTORIZO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar actualizar el usuario que autorizo la orden de compra.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_ORDEN_COMPRA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el la orden de compra.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
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
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ActualizarestadoOrdenCompra(Decimal ID_ORDEN,
            String ESTADO,
            String OBSERVACIONES)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_NEGOCIACION_PROVEEDOR_ACTUALIZAR_ESTADO_ORDEN ";

            #region validaciones

            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ORDEN no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG_AUTOESTADORIZO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBSERVACIONES = 'null', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar actualizar el estado de la orden de compra.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_ORDEN_COMPRA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la orden de compra.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
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
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ActualizarestadoOrdenCompra(Decimal ID_ORDEN,
            String ESTADO,
            String OBSERVACIONES,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_NEGOCIACION_PROVEEDOR_ACTUALIZAR_ESTADO_ORDEN ";

            #region validaciones

            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ORDEN no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG_AUTOESTADORIZO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBSERVACIONES = 'null', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el estado de la orden de compra numero " + ID_ORDEN.ToString();
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_ORDEN_COMPRA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la orden de compra numero " + ID_ORDEN.ToString();
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = "ERROR: Al intentar actualizar estado de orden de compra numero " + ID_ORDEN.ToString() + e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable ObtenerOrdenCompraAbiertasyPorProveedor(Decimal ID_PROVEEDOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_ORDEN_COMPRA_OBTENER_ABIERTAS_Y_POR_PROVEEDOR ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
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

        public DataTable ObtenerOrdenCompraSegunCriteriosBusquedaSencilla(String RAZON_SOCIAL, String NUMERO_IDENTIFICACION, String RAZ_SOCIAL, String NIT_EMPRESA, String PROCESO_AREA, String ESTADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_orden_compra_obtener_para_hoja_trabajo_busqueda_sencilla ";

            #region validaciones
            if (String.IsNullOrEmpty(RAZON_SOCIAL) == false)
            {
                sql += "'" + RAZON_SOCIAL + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(NUMERO_IDENTIFICACION) == false)
            {
                sql += "'" + NUMERO_IDENTIFICACION + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(RAZ_SOCIAL) == false)
            {
                sql += "'" + RAZ_SOCIAL + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(NIT_EMPRESA) == false)
            {
                sql += "'" + NIT_EMPRESA + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(PROCESO_AREA) == false)
            {
                sql += "'" + PROCESO_AREA + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "'";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
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

        public DataTable ObtenerOrdenCompraSegunCriteriosBusquedaAvanzada(String ID_REGIONAL,
            String ID_DEPARTAMENTO,
            String ID_CIUDAD,
            Decimal ID_EMPRESA_CLIENTE,
            Decimal ID_PROVEEDOR,
            DateTime FECHA_ESPECIFICA,
            DateTime FECHA_DESDE,
            DateTime FECHA_HASTA,
            String ESTADO_FALTA_AUTORIZACION,
            String ESTADO_ENVIADA_A_PROVEEDOR,
            String ESTADO_ADJUNTADO_FACTURA,
            String ESTADO_FINALIZADA,
            String ESTADO_CANCELADA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_orden_compra_obtener_para_hoja_trabajo_busqueda_avanzada ";

            #region validaciones
            if (String.IsNullOrEmpty(ID_REGIONAL) == false)
            {
                sql += "'" + ID_REGIONAL + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ID_DEPARTAMENTO) == false)
            {
                sql += "'" + ID_DEPARTAMENTO + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (ID_EMPRESA_CLIENTE != 0)
            {
                sql += ID_EMPRESA_CLIENTE + ", ";
            }
            else
            {
                sql += "null, ";
            }

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
            }
            else
            {
                sql += "null, ";
            }

            if (FECHA_ESPECIFICA != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ESPECIFICA) + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (FECHA_DESDE != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_DESDE) + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (FECHA_HASTA != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_HASTA) + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ESTADO_FALTA_AUTORIZACION) == false)
            {
                sql += "'" + ESTADO_FALTA_AUTORIZACION + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ESTADO_ENVIADA_A_PROVEEDOR) == false)
            {
                sql += "'" + ESTADO_ENVIADA_A_PROVEEDOR + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ESTADO_ADJUNTADO_FACTURA) == false)
            {
                sql += "'" + ESTADO_ADJUNTADO_FACTURA + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ESTADO_FINALIZADA) == false)
            {
                sql += "'" + ESTADO_FINALIZADA + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ESTADO_CANCELADA) == false)
            {
                sql += "'" + ESTADO_CANCELADA + "'";
            }
            else
            {
                sql += "null";
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

        #endregion alm_orden_compra




        #region alm_detalle_orden_compra
        public DataTable ObtenerDetallesOrdenCompraPorIdOrden(Decimal ID_ORDEN)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DETALLE_ORDEN_OBTENER_ID_ORDEN ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
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

        public DataTable ObtenerCantidadTotalProductosDeOrdenCompra(Decimal ID_ORDEN)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DETALLE_ORDEN_OBTENER_CANTIDAD_TOTAL_PRODUCTO_DE_ORDEN_COMPRA ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
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

        public DataTable ObtenerDetallesOrdenCompraPorIdOrdenConCantidadDescargada(Decimal ID_ORDEN)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DETALLE_ORDEN_OBTENER_POR_ID_ORDEN_CON_CONTROL_DESCARGUE ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
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


        public DataTable ObtenerDetallesOrdenCompraPorIdOrdenConCantidadDescargada(Decimal ID_ORDEN, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DETALLE_ORDEN_OBTENER_POR_ID_ORDEN_CON_CONTROL_DESCARGUE ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

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

        public Boolean eliminarDetallesDeOrdenCompra(Decimal ID_ORDEN, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            sql = "USP_ALM_DETALLE_ORDEN_ELIMINAR ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
            }

            return ejecutar;
        }

        public Decimal AdicionarDetalleOrdenCompra(Decimal ID_ORDEN_COMPRA,
            Decimal ID_PRODUCTO_PARAM,
            String REFERENCIA_PRODUCTO_PARAM,
            String TALLA_PARAM,
            String DESCRIPCION_PARAM,
            int CANTIDAD_PARAM,
            Decimal VALOR_UNITARIO_PARAM,
            Decimal VALOR_TOTAL_PARAM,
            Decimal DESCUENTO_PARAM,
            Decimal IVA_APLICADO_PARAM,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_DETALLE_ORDEN_ADICIONAR ";

            #region validaciones
            if (ID_ORDEN_COMPRA != 0)
            {
                sql += ID_ORDEN_COMPRA + ", ";
                informacion += "ID_ORDEN_COMPRA = '" + ID_ORDEN_COMPRA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ORDEN_COMPRA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO_PARAM != 0)
            {
                sql += ID_PRODUCTO_PARAM + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO_PARAM + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REFERENCIA_PRODUCTO_PARAM)))
            {
                sql += "'" + REFERENCIA_PRODUCTO_PARAM + "', ";
                informacion += "REFERENCIA_PRODUCTO = '" + REFERENCIA_PRODUCTO_PARAM + "', ";
            }
            else
            {
                MensajeError += "El campo REFERENCIA_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TALLA_PARAM)))
            {
                if (TALLA_PARAM == "N/A")
                {
                    sql += "'N/A', ";
                    informacion += "TALLA = 'N/A', ";
                }
                else
                {
                    sql += "'" + TALLA_PARAM + "', ";
                    informacion += "TALLA = '" + TALLA_PARAM + "', ";
                }

            }
            else
            {
                sql += "'N/A', ";
                informacion += "TALLA = 'N/A', ";
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION_PARAM)))
            {
                sql += "'" + DESCRIPCION_PARAM + "', ";
                informacion += "DESCRIPCION_PARAM = '" + DESCRIPCION_PARAM + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION_PARAM no puede ser nulo\n";
                ejecutar = false;
            }

            sql += CANTIDAD_PARAM + ", ";
            informacion += "CANTIDAD = '" + CANTIDAD_PARAM + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_UNITARIO_PARAM) + ", ";
            informacion += "VALOR_UNITARIO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_UNITARIO_PARAM) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_TOTAL_PARAM) + ", ";
            informacion += "VALOR_TOTAL = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_TOTAL_PARAM) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(DESCUENTO_PARAM) + ", ";
            informacion += "DESCUENTO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(DESCUENTO_PARAM) + "', ";

            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA_APLICADO_PARAM) + ", ";
            informacion += "IVA_APLICADO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA_APLICADO_PARAM) + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_DETALLE_ORDEN, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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
        #endregion alm_detalle_orden_compra


        #region alm_referencia_producto_proveedor
        public Decimal verificarReferenciaProductoProveedor(Decimal ID_PROVEEDOR,
            Decimal ID_PRODUCTO_PARAM,
            String REFERENCIA_PARAM,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_ref_productos_proveedor_comprobar_referencia ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO_PARAM != 0)
            {
                sql += ID_PRODUCTO_PARAM + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO_PARAM + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REFERENCIA_PARAM)))
            {
                sql += "'" + REFERENCIA_PARAM + "', ";
                informacion += "REFERENCIA = '" + REFERENCIA_PARAM + "', ";
            }
            else
            {
                MensajeError += "El campo REFERENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REF_PRODUCTOS_PROVEEDOR, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        #endregion alm_referencia_producto_proveedor




        #region alm_firmas_orden_compra
        public DataTable ObtenerAutorizacionesOrdenCompra()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_firmas_orden_compra_obtener_lista_permisos";

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

        public DataTable ObtenerAutorizacionParaUsuLog(String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_firmas_orden_compra_obtener_por_usu_log ";

            #region validaciones
            if (String.IsNullOrEmpty(USU_LOG) == false)
            {
                sql += "'" + USU_LOG + "'";
            }
            else
            {
                MensajeError = "El campo USU_LOG no puede ser nulo.";
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

        #endregion alm_firmas_orden_compra

        #region ALM_REG_COTIZACIONES
        public DataTable ObtenerCotizacionPorIDOrden(Decimal ID_ORDEN)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_cotizaciones_obtener_por_id_orden ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN;
            }
            else
            {
                MensajeError = "El campo ID_ORDEN no puede ser nulo.";
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
        #endregion ALM_REG_COTIZACIONES

        public DataTable obtenerServiciosConReembolsoGastos(Decimal ID_EMPRESA,
            Decimal ID_BODEGA,
            Decimal ID_SERVICIO_COMPLEMENTARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reembolso_gastos_obtener_para_ciudad ";


            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
            }

            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_BODEGA no puede ser vacio.";
            }

            if (ID_SERVICIO_COMPLEMENTARIO != 0)
            {
                sql += ID_SERVICIO_COMPLEMENTARIO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_SERVICIO_COMPLEMENTARIO no puede ser vacio.";
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

        #endregion metodos
    }
}