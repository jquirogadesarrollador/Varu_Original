using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class documento
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_ORDEN = 0;
        private Decimal _ID_PRODUCTO = 0;
        private Decimal _ID_BODEGA = 0;
        private Decimal _ID_EMPRESA = 0;
        private Decimal _NUMERO_ORDEN = 0;
        private String _REFERENCIA_PRODUCTO = null;
        private String _TALLA = null;
        private Int32 _CANTIDAD = 0;
        private Decimal _VALOR_UNIDAD = 0;
        private Decimal _ID_DETALLE = 0;
        private String _reembolso = null;
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

        public Decimal ID_ORDEN
        {
            get { return _ID_ORDEN; }
            set { _ID_ORDEN = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _ID_PRODUCTO; }
            set { _ID_PRODUCTO = value; }
        }

        public Decimal ID_BODEGA
        {
            get { return _ID_BODEGA; }
            set { _ID_BODEGA = value; }
        }

        public Decimal ID_EMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }

        public Decimal NUMERO_ORDEN
        {
            get { return _NUMERO_ORDEN; }
            set { _NUMERO_ORDEN = value; }
        }

        public String REFERENCIA_PRODUCTO
        {
            get { return _REFERENCIA_PRODUCTO; }
            set { _REFERENCIA_PRODUCTO = value; }
        }

        public String TALLA
        {
            get { return _TALLA; }
            set { _TALLA = value; }
        }

        public Int32 CANTIDAD
        {
            get { return _CANTIDAD; }
            set { _CANTIDAD = value; }
        }

        public Decimal VALOR_UNIDAD
        {
            get { return _VALOR_UNIDAD; }
            set { _VALOR_UNIDAD = value; }
        }
        public Decimal ID_DETALLE
        {
            get { return _ID_DETALLE; }
            set { _ID_DETALLE = value; }
        }

        public String REEMBOLSO
        {
            get { return _reembolso; }
            set { _reembolso = value; }
        }
        #endregion propiedades

        #region constructores
        public documento(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos


        public Decimal AdicionarAlmDocumentos(int ID_EMPLEADO,
            int DOCUMENTO_ORIGEN,
            String TIPO_DOC,
            String ORIGEN,
            String NUMERO_DOCUMENTO,
            DateTime FECHA_DOCUMENTO,
            DateTime FECHA_VENCE,
            int ID_BODEGA_DESTINO,
            Decimal VALOR,
            int CONSECUTIVO_TIPO_DOC,
            String ESTADO,
            String OBSERVACION_JUSTIFICACION,
            Conexion _dato,
            int ID_PROVEEDOR,
            String OBS_AUTORIZACION)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_DOCUMENTOS_ADICIONAR ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EMPLEADO= 0, ";
            }

            if (DOCUMENTO_ORIGEN != 0)
            {
                sql += DOCUMENTO_ORIGEN + ", ";
                informacion += "DOCUMENTO_ORIGEN= '" + DOCUMENTO_ORIGEN.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DOCUMENTO_ORIGEN= 0, ";
            }


            if (!(String.IsNullOrEmpty(TIPO_DOC)))
            {
                sql += "'" + TIPO_DOC + "', ";
                informacion += "TIPO_DOC= '" + TIPO_DOC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_DOC no puede ser nulo\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(ORIGEN)))
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN= '" + ORIGEN.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ORIGEN= 'null', ";
            }

            if (!(String.IsNullOrEmpty(NUMERO_DOCUMENTO)))
            {
                sql += "'" + NUMERO_DOCUMENTO + "', ";
                informacion += "NUMERO_DOCUMENTO= '" + NUMERO_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUMERO_DOCUMENTO= 'null', ";
            }

            if (FECHA_DOCUMENTO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_DOCUMENTO) + "', ";
                informacion += "FECHA_DOCUMENTO = '" + FECHA_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (FECHA_VENCE != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_VENCE) + "', ";
                informacion += "FECHA_VENCE = '" + FECHA_VENCE.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_VENCE= 'null', ";
            }

            if (ID_BODEGA_DESTINO != 0)
            {
                sql += ID_BODEGA_DESTINO + ", ";
                informacion += "ID_BODEGA_DESTINO= '" + ID_BODEGA_DESTINO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_BODEGA_DESTINO = 0, ";
            }

            if (VALOR != 0)
            {
                sql += _tools.conviertePuntoEnComa(VALOR.ToString()) + ", ";
                informacion += "VALOR= '" + VALOR.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "VALOR = 0, ";
            }

            if (CONSECUTIVO_TIPO_DOC != 0)
            {
                sql += CONSECUTIVO_TIPO_DOC + ", ";
                informacion += "CONSECUTIVO_TIPO_DOC= '" + CONSECUTIVO_TIPO_DOC.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "CONSECUTIVO_TIPO_DOC = 0, ";
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ESTADO= 'null', ";
            }

            if (!(String.IsNullOrEmpty(OBSERVACION_JUSTIFICACION)))
            {
                sql += "'" + OBSERVACION_JUSTIFICACION + "', ";
                informacion += "OBSERVACION_JUSTIFICACION= '" + OBSERVACION_JUSTIFICACION.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBSERVACION_JUSTIFICACION= 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_PROVEEDOR = 0, ";
            }

            if (!(String.IsNullOrEmpty(OBS_AUTORIZACION)))
            {
                sql += "'" + OBS_AUTORIZACION + "'";
                informacion += "OBS_AUTORIZACION= '" + OBS_AUTORIZACION + "'";
            }
            else
            {
                sql += "null";
                informacion += "OBSERVACION_JUSTIFICACION= 'null'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = _dato.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_DOCUMENTOS, tabla.ACCION_ADICIONAR, sql, informacion, _dato);
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


        public Decimal AdicionarAlmDocumentosParaFactura(int ID_EMPLEADO,
            int DOCUMENTO_ORIGEN,
            String TIPO_DOC,
            String ORIGEN,
            String NUMERO_DOCUMENTO,
            DateTime FECHA_DOCUMENTO,
            DateTime FECHA_VENCE,
            int ID_BODEGA_DESTINO,
            Decimal VALOR,
            String ESTADO,
            String OBSERVACION_JUSTIFICACION,
            Conexion _dato,
            int ID_PROVEEDOR)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_DOCUMENTOS_ADICIONAR_PARA_FACTURA ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EMPLEADO = '0', ";
            }

            if (DOCUMENTO_ORIGEN != 0)
            {
                sql += DOCUMENTO_ORIGEN + ", ";
                informacion += "DOCUMENTO_ORIGEN  = '" + DOCUMENTO_ORIGEN.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DOCUMENTO_ORIGEN = '0', ";
            }

            if (!(String.IsNullOrEmpty(TIPO_DOC)))
            {
                sql += "'" + TIPO_DOC + "', ";
                informacion += "TIPO_DOC= '" + TIPO_DOC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_DOC no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ORIGEN)))
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN= '" + ORIGEN.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "ORIGEN= 'null', ";
            }

            if (!(String.IsNullOrEmpty(NUMERO_DOCUMENTO)))
            {
                sql += "'" + NUMERO_DOCUMENTO + "', ";
                informacion += "NUMERO_DOCUMENTO= '" + NUMERO_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "NUMERO_DOCUMENTO= 'null', ";
            }

            if (!(String.IsNullOrEmpty(FECHA_DOCUMENTO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_DOCUMENTO) + "', ";
                informacion += "FECHA_DOCUMENTO= '" + FECHA_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_VENCE.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_VENCE) + "', ";
                informacion += "FECHA_VENCE= '" + FECHA_VENCE.ToString() + "', ";
            }
            else
            {
                sql += "'01011990', ";
                informacion += "FECHA_VENCE= '01011990', ";
            }

            if (ID_BODEGA_DESTINO != 0)
            {
                sql += ID_BODEGA_DESTINO + ", ";
                informacion += "ID_BODEGA_DESTINO= '" + ID_BODEGA_DESTINO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_BODEGA_DESTINO = 0, ";
            }

            if (VALOR != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ", ";
                informacion += "VALOR = '" + VALOR.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "VALOR = 0, ";
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ESTADO= 'null', ";
            }

            if (!(String.IsNullOrEmpty(OBSERVACION_JUSTIFICACION)))
            {
                sql += "'" + OBSERVACION_JUSTIFICACION + "', ";
                informacion += "OBSERVACION_JUSTIFICACION= '" + OBSERVACION_JUSTIFICACION.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBSERVACION_JUSTIFICACION= 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR.ToString() + "'";
            }
            else
            {
                sql += "0";
                informacion += "ID_PROVEEDOR = '0'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = _dato.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_DOCUMENTOS, tabla.ACCION_ADICIONAR, sql, informacion, _dato);
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

        public Boolean ActualizarAlmDocumento(int ID_DOCUMENTO, int ID_EMPLEADO, int DOCUMENTO_ORIGEN, String TIPO_DOC, String ORIGEN, String NUMERO_DOCUMENTO,
            DateTime FECHA_DOCUMENTO, DateTime FECHA_VENCE, int ID_BODEGA_DESTINO, Decimal VALOR, int CONSECUTIVO_TIPO_DOC, String ESTADO,
            String OBSERVACION_JUSTIFICACION, int ID_PROVEEDOR, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_DOCUMENTOS_ACTUALIZAR ";

            #region validaciones
            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO= '" + ID_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EMPLEADO= 0, ";
            }
            if (DOCUMENTO_ORIGEN != 0)
            {
                sql += DOCUMENTO_ORIGEN + ", ";
                informacion += "DOCUMENTO_ORIGEN= '" + DOCUMENTO_ORIGEN.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DOCUMENTO_ORIGEN= 0, ";
            }
            if (!(String.IsNullOrEmpty(TIPO_DOC)))
            {
                sql += "'" + TIPO_DOC + "', ";
                informacion += "TIPO_DOC= '" + TIPO_DOC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_DOC no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ORIGEN)))
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN= '" + ORIGEN.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "ORIGEN= 'null', ";
            }

            if (!(String.IsNullOrEmpty(NUMERO_DOCUMENTO)))
            {
                sql += "'" + NUMERO_DOCUMENTO + "', ";
                informacion += "NUMERO_DOCUMENTO= '" + NUMERO_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "NUMERO_DOCUMENTO= 'null', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_DOCUMENTO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_DOCUMENTO) + "', ";
                informacion += "FECHA_DOCUMENTO= '" + FECHA_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (FECHA_VENCE != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_VENCE) + "', ";
                informacion += "FECHA_VENCE= '" + FECHA_VENCE.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_VENCE= 'null', ";
            }
            if (ID_BODEGA_DESTINO != 0)
            {
                sql += ID_BODEGA_DESTINO + ", ";
                informacion += "ID_BODEGA_DESTINO= '" + ID_BODEGA_DESTINO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_BODEGA_DESTINO = 0, ";
            }

            if (VALOR != 0)
            {
                sql += _tools.conviertePuntoEnComa(VALOR.ToString()) + ", ";
                informacion += "VALOR= '" + VALOR.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "VALOR = 0, ";
            }

            if (CONSECUTIVO_TIPO_DOC != 0)
            {
                sql += CONSECUTIVO_TIPO_DOC + ", ";
                informacion += "CONSECUTIVO_TIPO_DOC= '" + CONSECUTIVO_TIPO_DOC.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "CONSECUTIVO_TIPO_DOC = 0, ";
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "ESTADO= 'null', ";
            }
            if (!(String.IsNullOrEmpty(OBSERVACION_JUSTIFICACION)))
            {
                sql += "'" + OBSERVACION_JUSTIFICACION + "', ";
                informacion += "OBSERVACION_JUSTIFICACION= '" + OBSERVACION_JUSTIFICACION.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "OBSERVACION_JUSTIFICACION= 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD= '" + Usuario.ToString() + "', ";

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + " ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR.ToString() + "' ";
            }
            else
            {
                sql += "0 ";
                informacion += "ID_PROVEEDOR = 0 ";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_DOCUMENTOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegDocumentoPorId(int ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_OBTENER_POR_ID ";

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + " ";
                informacion += "ID_DOCUMENTO= '" + ID_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_DOCUMENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegDocumentoPorId(int ID_DOCUMENTO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_OBTENER_POR_ID ";

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + " ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
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

        public DataTable ObtenerTrasladosAlmRegDocumentoPorUsuLog(String USU_LOG)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_OBTENER_TRASLADOS_POR_USU_LOG ";

            if (!(String.IsNullOrEmpty(USU_LOG)))
            {
                sql += "'" + USU_LOG + "' ";
                informacion += "USU_LOG= '" + USU_LOG.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_DOCUMENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        #region metodos utilizados para factura
        public DataTable ObtenerAlmRegDocumentoPorTipoDoc(String TIPO_DOC)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_documentos_obtener_por_tipo_doc ";

            #region validaciones
            if (String.IsNullOrEmpty(TIPO_DOC) == false)
            {
                sql += "'" + TIPO_DOC + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_DOC no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion

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

            _dataTable.Columns.Add("NOMBRE_PROVEEDOR");

            ordenCompra _ordenCompra = new ordenCompra(Empresa, Usuario);
            proveedor _proveedor = new proveedor(Empresa, Usuario);
            Decimal ID_ORDEN = 0;
            Decimal ID_PROVEEDOR = 0;
            String[] ordenesCompraPorDocumento;
            DataTable tablaInfoOrden;
            DataRow filaInfoOrdenCompra;
            DataTable tablaInfoProveedor;
            DataRow filaInfoProveedor;

            foreach (DataRow fila in _dataTable.Rows)
            {
                try
                {
                    ordenesCompraPorDocumento = fila["ORIGEN"].ToString().Split(',');
                    ID_ORDEN = Convert.ToDecimal(ordenesCompraPorDocumento[0]);
                    tablaInfoOrden = _ordenCompra.ObtenerOrdenCompraPorId(ID_ORDEN);
                    filaInfoOrdenCompra = tablaInfoOrden.Rows[0];
                    ID_PROVEEDOR = Convert.ToDecimal(filaInfoOrdenCompra["ID_PROVEEDOR"]);
                    tablaInfoProveedor = _proveedor.ObtenerAlmRegProveedorPorRegistro(ID_PROVEEDOR);
                    filaInfoProveedor = tablaInfoProveedor.Rows[0];
                    fila["NOMBRE_PROVEEDOR"] = filaInfoProveedor["RAZON_SOCIAL"];
                }
                catch (Exception ex)
                {
                    fila["NOMBRE_PROVEEDOR"] = ex.Message;
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerInformacionFacturaConDetallesPorIdDocumento(Decimal ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_documentos_obtener_info_factura_por_id_documento ";

            #region validaciones
            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO;
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion

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

            _dataTable.Columns.Add("NOMBRE_PROVEEDOR");

            ordenCompra _ordenCompra = new ordenCompra(Empresa, Usuario);
            proveedor _proveedor = new proveedor(Empresa, Usuario);
            Decimal ID_ORDEN = 0;
            Decimal ID_PROVEEDOR = 0;
            String[] ordenesCompraPorDocumento;
            DataTable tablaInfoOrden;
            DataRow filaInfoOrdenCompra;
            DataTable tablaInfoProveedor;
            DataRow filaInfoProveedor;

            foreach (DataRow fila in _dataTable.Rows)
            {
                try
                {
                    ordenesCompraPorDocumento = fila["ORIGEN"].ToString().Split(',');
                    ID_ORDEN = Convert.ToDecimal(ordenesCompraPorDocumento[0]);
                    tablaInfoOrden = _ordenCompra.ObtenerOrdenCompraPorId(ID_ORDEN);
                    filaInfoOrdenCompra = tablaInfoOrden.Rows[0];
                    ID_PROVEEDOR = Convert.ToDecimal(filaInfoOrdenCompra["ID_PROVEEDOR"]);
                    tablaInfoProveedor = _proveedor.ObtenerAlmRegProveedorPorRegistro(ID_PROVEEDOR);
                    filaInfoProveedor = tablaInfoProveedor.Rows[0];
                    fila["NOMBRE_PROVEEDOR"] = filaInfoProveedor["RAZON_SOCIAL"];
                }
                catch (Exception ex)
                {
                    fila["NOMBRE_PROVEEDOR"] = ex.Message;
                }
            }

            return _dataTable;
        }



        public DataTable ObtenerDocumentosPorTipoDocIdProveedor(String TIPO_DOC, Decimal ID_PROVEEDOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_OBTENER_POR_TIPO_DOC_ID_PROVEEDOR ";

            #region validaciones
            if (String.IsNullOrEmpty(TIPO_DOC) == false)
            {
                sql += "'" + TIPO_DOC + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_DOC no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion

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

        public Decimal AdicionarDatosFactura(Int32 ID_EMPLEADO,
            Int32 DOCUMENTO_ORIGEN,
            String TIPO_DOC,
            String ORIGEN,
            String NUMERO_DOCUMENTO,
            DateTime FECHA_DOCUMENTO,
            DateTime FECHA_VENCE,
            Int32 ID_BODEGA_DESTINO,
            Decimal VALOR,
            String ESTADO,
            String OBSERVACION_JUSTIFICACION,
            Int32 ID_PROVEEDOR,
            List<documento> listaDetalleFactura)
        {
            Decimal ID_DOCUMENTO = 0;
            Decimal ID_LOTE = 0;
            Decimal ID_INVENTARIO = 0;
            Decimal ID_DESCARGUE = 0;

            Boolean verificador = true;

            List<Decimal> listaDeOrdenesParaDescargadas = new List<Decimal>();

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_DOCUMENTO = AdicionarAlmDocumentosParaFactura(ID_EMPLEADO, DOCUMENTO_ORIGEN, TIPO_DOC, ORIGEN, NUMERO_DOCUMENTO, FECHA_DOCUMENTO, FECHA_VENCE, ID_BODEGA_DESTINO, VALOR, ESTADO, OBSERVACION_JUSTIFICACION, conexion, ID_PROVEEDOR);

                if (ID_DOCUMENTO == 0)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = MensajeError;
                    verificador = false;
                }
                else
                {
                    lote _lote = new lote(Empresa, Usuario);
                    Inventario _inventario = new Inventario(Empresa, Usuario);
                    crtDescargueFactura _crtDescargueFactura = new crtDescargueFactura(Empresa, Usuario);
                    foreach (documento detalleFactura in listaDetalleFactura)
                    {
                        ID_LOTE = _lote.AdicionarAlmLote(Convert.ToInt32(ID_DOCUMENTO), Convert.ToInt32(detalleFactura.ID_PRODUCTO), Convert.ToInt32(detalleFactura.ID_BODEGA), FECHA_DOCUMENTO, detalleFactura.CANTIDAD, 0, detalleFactura.VALOR_UNIDAD, detalleFactura.TALLA, "S", conexion, detalleFactura.REEMBOLSO);

                        if (ID_LOTE == 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Ingresando el lote: " + _lote.MensajeError;
                            verificador = false;
                            break;
                        }
                        else
                        {
                            ID_INVENTARIO = _inventario.AdicionarAlmInventario(Convert.ToInt32(ID_DOCUMENTO), Convert.ToInt32(detalleFactura.ID_PRODUCTO), Convert.ToInt32(detalleFactura.ID_BODEGA), detalleFactura.CANTIDAD, detalleFactura.VALOR_UNIDAD, FECHA_DOCUMENTO, "ENTRADA", conexion, Convert.ToInt32(ID_LOTE), detalleFactura.TALLA, 0, 0, null, detalleFactura.REEMBOLSO);

                            if (ID_INVENTARIO == 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Ingresando el inventario: " + _inventario.MensajeError;
                                verificador = false;
                                break;
                            }
                            else
                            {
                                ID_DESCARGUE = _crtDescargueFactura.AdicionarAlmCrtDescargueInventario(ID_DOCUMENTO, detalleFactura.ID_ORDEN, detalleFactura.ID_PRODUCTO, detalleFactura.ID_DETALLE, detalleFactura.ID_BODEGA, ID_LOTE, detalleFactura.REFERENCIA_PRODUCTO, detalleFactura.TALLA, detalleFactura.CANTIDAD, conexion, detalleFactura.REEMBOLSO);

                                if (ID_DESCARGUE == 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "ERROR: Ingresando el descargue de inventario: " + _crtDescargueFactura.MensajeError;
                                    verificador = false;
                                    break;
                                }
                            }
                        }

                        if (listaDeOrdenesParaDescargadas.Contains(detalleFactura.ID_ORDEN) == false)
                        {
                            listaDeOrdenesParaDescargadas.Add(detalleFactura.ID_ORDEN);
                        }
                    }
                }

                if (verificador == true)
                {
                    ordenCompra _ordenCompra = new ordenCompra(Empresa, Usuario);
                    DataTable tablaDetalleOrden;
                    Int32 CANTIDAD_TOTAL_PRODUCTOS = 0;
                    Int32 CANTIDAD_TOTAL_DESCARGADA = 0;

                    foreach (Decimal orden in listaDeOrdenesParaDescargadas)
                    {
                        CANTIDAD_TOTAL_PRODUCTOS = 0;
                        CANTIDAD_TOTAL_DESCARGADA = 0;
                        tablaDetalleOrden = _ordenCompra.ObtenerDetallesOrdenCompraPorIdOrdenConCantidadDescargada(orden, conexion);

                        foreach (DataRow filaTabla in tablaDetalleOrden.Rows)
                        {
                            try
                            {
                                CANTIDAD_TOTAL_PRODUCTOS += Convert.ToInt32(filaTabla["CANTIDAD"]);
                                CANTIDAD_TOTAL_DESCARGADA += Convert.ToInt32(filaTabla["CANTIDAD_DESCARGADA"]);
                            }
                            catch (Exception ex)
                            {
                                MensajeError = "ERROR: Al intentar calcular estado de descarga de la orden de compra " + orden.ToString() + ". " + ex.Message;
                                conexion.DeshacerTransaccion();
                                verificador = false;
                                break;
                            }
                        }

                        if (verificador == true)
                        {
                            if ((CANTIDAD_TOTAL_PRODUCTOS - CANTIDAD_TOTAL_DESCARGADA) <= 0)
                            {
                                if (_ordenCompra.ActualizarestadoOrdenCompra(orden, tabla.VAR_ESTADO_ORDEN_COMPRA_FINALIZADA, null) == false)
                                {
                                    MensajeError = _ordenCompra.MensajeError;
                                    conexion.DeshacerTransaccion();
                                    verificador = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (_ordenCompra.ActualizarestadoOrdenCompra(orden, tabla.VAR_ESTADO_ORDEN_COMPRA_ADJUNTADO_FACTURA, null) == false)
                                {
                                    MensajeError = _ordenCompra.MensajeError;
                                    conexion.DeshacerTransaccion();
                                    verificador = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
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
                return ID_DOCUMENTO;
            }
            else
            {
                return 0;
            }
        }

        #endregion metodos utilizados para factura

        #endregion
    }
}
