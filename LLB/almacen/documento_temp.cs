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
    public class documento_temp
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
        #endregion propiedades

        #region constructores
        public documento_temp(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarAlmDocumentosTemp(int ID_EMPLEADO,
            String TIPO_DOC,
            String NUMERO_DOCUMENTO,
            DateTime FECHA_DOCUMENTO, DateTime FECHA_AUTORIZACION, Decimal VALOR, String ESTADO,
            String OBSERVACION_JUSTIFICACION, Conexion _dato)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "ALM_DOCUMENTOS_TEMP_ADICIONAR ";

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
            if (FECHA_AUTORIZACION != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AUTORIZACION) + "', ";
                informacion += "FECHA_AUTORIZACION= '" + FECHA_AUTORIZACION.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_AUTORIZACION= 'null', ";
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

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";


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


        public Boolean ActualizarAlmDocumentoTemp(int ID_DOCUMENTO,
            DateTime FECHA_AUTORIZACION,
            String ESTADO,
            String OBS_AUTORIZACION)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "ALM_DOCUMENTOS_TEMP_ACUTALIZAR_ESTADO ";

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

            if (FECHA_AUTORIZACION != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AUTORIZACION) + "', ";
                informacion += "FECHA_AUTORIZACION= '" + FECHA_AUTORIZACION.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_AUTORIZACION= 'null', ";
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

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(OBS_AUTORIZACION)))
            {
                sql += "'" + OBS_AUTORIZACION + "'";
                informacion += "OBS_AUTORIZACION = '" + OBS_AUTORIZACION + "'";
            }
            else
            {
                sql += "null";
                informacion += "OBS_AUTORIZACION = 'null'";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_DOCUMENTOS_TEMP, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarAlmDocumentoTemp(int ID_DOCUMENTO,
            DateTime FECHA_AUTORIZACION,
            String ESTADO,
            Conexion conexion,
            String OBS_AUTORIZACION)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "ALM_DOCUMENTOS_TEMP_ACUTALIZAR_ESTADO ";

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

            if (FECHA_AUTORIZACION != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AUTORIZACION) + "', ";
                informacion += "FECHA_AUTORIZACION= '" + FECHA_AUTORIZACION.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_AUTORIZACION= 'null', ";
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ESTADO = 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(OBS_AUTORIZACION)))
            {
                sql += "'" + OBS_AUTORIZACION + "'";
                informacion += "OBS_AUTORIZACION = '" + OBS_AUTORIZACION + "'";
            }
            else
            {
                sql += "null";
                informacion += "OBS_AUTORIZACION = 'null'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_DOCUMENTOS_TEMP, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegDocumentoTempPorId(int ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_TEMP_OBTENER_POR_ID ";

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

        public DataTable ObtenerTrasladosAlmRegDocumentoTempPorAutorizado(String AUTORIZADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_TEMP_OBTENER_POR_AUTORIZACION ";

            if (!(String.IsNullOrEmpty(AUTORIZADO)))
            {
                sql += "'" + AUTORIZADO + "' ";
                informacion += "AUTORIZADO= '" + AUTORIZADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo AUTORIZADO no puede ser nulo\n";
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

        public DataTable ObtenerTrasladosAlmRegDocumentoTempPorEstado(String ESTADO, String TIPO_DOC)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_TEMP_OBTENER_POR_ESTADO ";

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_DOC)))
            {
                sql += "'" + TIPO_DOC + "' ";
                informacion += "TIPO_DOC = '" + TIPO_DOC.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo TIPO_DOC no puede ser nulo\n";
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

        public DataTable ObtenerAlmRegDocumentoTempConfigurados(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_DOCUMENTOS_TEMP_OBTENER_CONFIGURADO ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "' ";
                informacion += "NUM_DOC_IDENTIDAD= '" + NUM_DOC_IDENTIDAD.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
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

        #endregion
    }
}
