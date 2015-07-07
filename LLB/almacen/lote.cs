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
    public class lote
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
        public lote(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarAlmLote(int ID_DOCUMENTO,
            int ID_PRODUCTO,
            int ID_BODEGA,
            DateTime FECHA_REGISTRO,
            int ENTRADAS,
            int SALIDAS,
            Decimal COSTO,
            String TALLA,
            String ACTIVO,
            Conexion conexion,
            String REEMBOLSO)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_LOTE_ADICIONAR ";

            #region validaciones

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO = '" + ID_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO= '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + ", ";
                informacion += "ID_BODEGA= '" + ID_BODEGA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_REGISTRO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REGISTRO) + "', ";
                informacion += "FECHA_REGISTRO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REGISTRO) + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ENTRADAS != 0)
            {
                sql += ENTRADAS + ", ";
                informacion += "ENTRADAS = '" + ENTRADAS.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ENTRADAS = '0', ";
            }

            if (SALIDAS != 0)
            {
                sql += SALIDAS + ", ";
                informacion += "SALIDAS = '" + SALIDAS.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "SALIDAS = '0', ";
            }

            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + "', ";
            }
            else
            {
                MensajeError += "El campo COSTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(TALLA)))
            {
                if ((TALLA == "N/A") || (TALLA == "NA"))
                {
                    sql += "'N/A', ";
                    informacion += "TALLA= 'N/A', ";
                }
                else
                {
                    sql += "'" + TALLA + "', ";
                    informacion += "TALLA = '" + TALLA.ToString() + "', ";
                }
            }
            else
            {
                sql += "'N/A', ";
                informacion += "TALLA= 'N/A', ";
            }

            if (!(String.IsNullOrEmpty(ACTIVO)))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO= '" + ACTIVO + "', ";
            }
            else
            {
                sql += "'S', ";
                informacion += "ACTIVO = 'S', ";
            }

            if (String.IsNullOrEmpty(REEMBOLSO) == false)
            {
                sql += "'" + REEMBOLSO + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo REEMBOLSO no puede ser vacio.";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarAlmLote(int ID_LOTE, int ID_DOCUMENTO, int ID_PRODUCTO, int ID_BODEGA, DateTime FECHA_REGISTRO,
            int ENTRADAS, int SALIDAS, Decimal COSTO, String TALLA, String ACTIVO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_LOTE_ACTUALIZAR ";

            #region validaciones

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE + ", ";
                informacion += "ID_LOTE= '" + ID_LOTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_LOTE no puede ser nulo\n";
                ejecutar = false;
            }
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
            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO= '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + ", ";
                informacion += "ID_BODEGA= '" + ID_BODEGA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_REGISTRO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REGISTRO) + "', ";
                informacion += "FECHA_REGISTRO= '" + FECHA_REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ENTRADAS != 0)
            {
                sql += ENTRADAS + ", ";
                informacion += "ENTRADAS= '" + ENTRADAS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ENTRADAS no puede ser nulo\n";
                ejecutar = false;
            }
            if (SALIDAS != 0)
            {
                sql += SALIDAS + ", ";
                informacion += "SALIDAS= '" + SALIDAS.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "SALIDAS= '0', ";
            }

            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO= '" + COSTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COSTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD= '" + Usuario.ToString() + "', ";


            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA= '" + TALLA.ToString() + "', ";
            }
            else
            {
                sql += "'N/A', ";
                informacion += "TALLA= 'N/A', ";
            }
            if (!(String.IsNullOrEmpty(ACTIVO)))
            {
                sql += "'" + ACTIVO + "' ";
                informacion += "ACTIVO= '" + ACTIVO + "' ";
            }
            else
            {
                sql += "'S' ";
                informacion += "ACTIVO = 'S' ";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarCantidadesInventario(Decimal ID_LOTE,
            Int32 CANTIDAD,
            String TIPO_MOOVIMIENTO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_lote_actualizar_cantidad ";

            #region validaciones

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE + ", ";
                informacion += "ID_LOTE = '" + ID_LOTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_LOTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD != 0)
            {
                sql += CANTIDAD + ", ";
                informacion += "CANTIDAD = '" + CANTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_MOOVIMIENTO) == false)
            {
                sql += "'" + TIPO_MOOVIMIENTO + "', ";
                informacion += "TIPO_MOOVIMIENTO = '" + TIPO_MOOVIMIENTO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_MOVIMIETNO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public Boolean ActualizarLoteActualDeUnEquipo(Decimal ID_EQUIPO,
             Decimal ID_LOTE,
             Decimal ID_DOCUMENTO,
             Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_equipo_actualizar_LoteYDocumento_Actual ";

            #region validaciones
            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO = '" + ID_EQUIPO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EQUIPO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE + ", ";
                informacion += "ID_LOTE = '" + ID_LOTE.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_LOTE = 'NULL', ";
            }

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO = '" + ID_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_DOCUMENTO = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_EQUIPOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmLotePorId(int ID_LOTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_LOTE_OBTENER_POR_ID ";

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE + " ";
                informacion += "ID_LOTE= '" + ID_LOTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_LOTE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmLotePorId(int ID_LOTE, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_LOTE_OBTENER_POR_ID ";

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE + " ";
                informacion += "ID_LOTE = '" + ID_LOTE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_LOTE no puede ser nulo\n";
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
        
        public DataTable ObtenerAlmLotePorIdProductoBodega(int ID_PRODUCTO, int ID_BODEGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_LOTE_OBTENER_POR_ID_PRODUCTO_BODEGA ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO= '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + " ";
                informacion += "ID_BODEGA = '" + ID_BODEGA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmLotePorIdBodega(int ID_BODEGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_LOTE_OBTENER_ID_PRODUCTO_POR_ID_BODEGA ";

            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + " ";
                informacion += "ID_BODEGA = '" + ID_BODEGA.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmLoteTallaPorIdProductoBodega(int ID_PRODUCTO, int ID_BODEGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_LOTE_OBTENER_TALLA_POR_ID_BODEGA_ID_PRODUCTO ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO= '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + " ";
                informacion += "ID_BODEGA = '" + ID_BODEGA.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmLotePorIdProductoBodegaTalla(int ID_PRODUCTO, int ID_BODEGA, String TALLA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_LOTE_OBTENER_TALLA_POR_ID_BODEGA_ID_PRODUCTO_TALLA ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO= '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + ", ";
                informacion += "ID_BODEGA = '" + ID_BODEGA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "'";
                informacion += "TALLA = '" + TALLA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TALLA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmLoteProductoPorIdEmpresaCiudad(int ID_EMPRESA, String ID_CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_LOTE_OBTENER_PRODUCTOS_POR_ID_EMPRESA_ID_CIUDAD ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "'";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_LOTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        #endregion metodos

    }
}
