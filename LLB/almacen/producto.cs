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
    public class producto
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private String _MARCA = null;
        private String _MODELO = null;
        private String _SERIE = null;
        private String _IMEI = null;
        private Decimal _NUMERO_CELULAR = 0;

        private Decimal _REGISTRO_P_P = 0;
        private Decimal _ID_PRODUCTO = 0;
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
        public Decimal REGISTRO_P_P
        {
            get { return _REGISTRO_P_P; }
            set { _REGISTRO_P_P = value; }
        }
        public Decimal ID_PRODUCTO
        {
            get { return _ID_PRODUCTO; }
            set { _ID_PRODUCTO = value; }
        }

        public String MARCA
        {
            get { return _MARCA; }
            set { _MARCA = value; }
        }
        public String MODELO
        {
            get { return _MODELO; }
            set { _MODELO = value; }
        }
        public String SERIE
        {
            get { return _SERIE; }
            set { _SERIE = value; }
        }
        public String IMEI
        {
            get { return _IMEI; }
            set { _IMEI = value; }
        }
        public Decimal NUMERO_CELULAR
        {
            get { return _NUMERO_CELULAR; }
            set { _NUMERO_CELULAR = value; }
        }
        #endregion propiedades

        #region constructores
        public producto(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerTodosExamenesMedicosPorTipoServicioComplemetario(String TIPO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_producto_obtener_examenes_medicos_por_tipo_servicio_complementario ";

            if (String.IsNullOrEmpty(TIPO) == false)
            {
                sql += "'" + TIPO + "'";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
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

        public DataTable ObtenerEquiposConfiguradosParaUnLote(Decimal ID_LOTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_equipos_obtener_por_id_lote ";

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE;
            }
            else
            {
                MensajeError = "El campo ID_LOTE no puede ser 0.";
            }

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

        public DataTable ObtenerEquiposEntradaTempParaAjusteEntrada(Decimal ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_equipos_entrada_temp_obtener_por_id_documento ";

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO;
            }
            else
            {
                MensajeError = "El campo ID_DOCUMENTO no puede ser 0.";
            }

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

        public DataTable ObtenerEquiposParaAjusteSalida(Decimal ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_equipos_obtener_ajuste_salida_por_id_documento ";

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO;
            }
            else
            {
                MensajeError = "El campo ID_DOCUMENTO no puede ser 0.";
            }

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

        public DataTable ObtenerTipoProductoPorIdProducto(Decimal ID_PRODUCTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_tipo_producto_por_id_producto ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO.ToString();
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
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
        #region alm_reg_producto


        public Decimal AdicionarAlmRegProducto(int ID_CATEGORIA,
            String NOMBRE,
            String DESCRIPCION,
            int TIPO,
            String BASICO,
            String APLICA_A,
            String TALLA,
            Int32 PLAZO_SOLICITUD_PLANTA,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_REG_PRODUCTO_ADICIONAR ";

            #region validaciones
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA= '" + ID_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE= '" + NOMBRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION= '" + DESCRIPCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            if (TIPO != 0)
            {
                sql += TIPO + ", ";
                informacion += "TIPO = '" + TIPO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "TIPO = '0', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(BASICO)))
            {
                sql += "'" + BASICO + "', ";
                informacion += "BASICO = '" + BASICO.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "BASICO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(APLICA_A)))
            {
                sql += "'" + APLICA_A + "', ";
                informacion += "APLICA_A = '" + APLICA_A.ToString() + "', ";
            }
            else
            {
                sql += "'N/A', ";
                informacion += "APLICA_A = 'N/A', ";
            }

            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA = '" + APLICA_A.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA = 'null', ";
            }

            if (PLAZO_SOLICITUD_PLANTA != 0)
            {
                sql += PLAZO_SOLICITUD_PLANTA;
                informacion += "PLAZO_SOLICITUD_PLANTA = '" + PLAZO_SOLICITUD_PLANTA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo PLAZO_SOLICITUD_PLANTA no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarAlmRegProducto(int ID_PRODUCTO,
            int ID_CATEGORIA,
            String NOMBRE,
            String DESCRIPCION,
            int TIPO,
            String BASICO,
            String APLICA_A,
            String TALLA,
            Int32 PLAZO_SOLICITUD_PLANTA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_REG_PRODUCTO_ACTUALIZAR";

            #region validaciones
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

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA= '" + ID_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE= '" + NOMBRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION= '" + DESCRIPCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            if (TIPO != 0)
            {
                sql += TIPO + ", ";
                informacion += "TIPO = '" + TIPO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(BASICO)))
            {
                sql += "'" + BASICO + "', ";
                informacion += "BASICO = '" + BASICO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo BASICO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(APLICA_A)))
            {
                sql += "'" + APLICA_A + "', ";
                informacion += "APLICA_A = '" + APLICA_A.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo APLICA_A no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA = '" + APLICA_A.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA = 'null', ";
            }

            if (PLAZO_SOLICITUD_PLANTA != 0)
            {
                sql += PLAZO_SOLICITUD_PLANTA;
                informacion += "PLAZO_SOLICITUD_PLANTA = '" + PLAZO_SOLICITUD_PLANTA.ToString() + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo PLAZO_SOLICITUD_PLANTA no puede ser vacio.";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegProductoPorId(int ID_PRODUCTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTO_OBTENER_POR_ID ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + " ";
                informacion += "ID_PRODUCTO= '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegProductoPorTipo(int TIPO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTO_OBTENER_POR_TIPO ";

            if (TIPO != 0)
            {
                sql += TIPO + " ";
                informacion += "TIPO= '" + TIPO.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegProductoPorIdCategoria(int ID_CATEGORIA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTO_OBTENER_POR_ID_CATEGORIA ";

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + " ";
                informacion += "ID_CATEGORIA= '" + ID_CATEGORIA.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegProductoPorNomCategoria(String NOMBRE_CATEGORIA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTO_OBTENER_POR_NOM_CATEGORIA ";

            if (!(String.IsNullOrEmpty(NOMBRE_CATEGORIA)))
            {
                sql += "'" + NOMBRE_CATEGORIA + "' ";
                informacion += "NOMBRE_CATEGORIA= '" + NOMBRE_CATEGORIA.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegProductoPorNomTipo(String NOMBRE_TIPO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTO_OBTENER_POR_NOM_TIPO ";

            if (!(String.IsNullOrEmpty(NOMBRE_TIPO)))
            {
                sql += "'" + NOMBRE_TIPO + "' ";
                informacion += "NOMBRE_TIPO= '" + NOMBRE_TIPO.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_TIPO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegProductoPorNombre(String NOMBRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTO_OBTENER_POR_NOMBRE ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "' ";
                informacion += "NOMBRE= '" + NOMBRE.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public void adicionarProductos(int ID_CATEGORIA,
            String NOMBRE,
            String DESCRIPCION,
            int TIPO,
            String BASICO,
            String APLICA_A,
            List<String> TALLA,
            Int32 PLAZO_SOLICITUD_PLANTA)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            Decimal ID_PRODUCTO = 0;
            if (TALLA.Count == 0)
            {
                ID_PRODUCTO = AdicionarAlmRegProducto(ID_CATEGORIA, NOMBRE, DESCRIPCION, TIPO, BASICO, APLICA_A, "N/A", PLAZO_SOLICITUD_PLANTA, conexion);
                if (ID_PRODUCTO == 0)
                {
                    conexion.DeshacerTransaccion();
                }
                else if (!(String.IsNullOrEmpty(MensajeError)))
                {
                    conexion.DeshacerTransaccion();
                }
            }
            else
            {
                for (int i = 0; i < TALLA.Count; i++)
                {
                    ID_PRODUCTO = AdicionarAlmRegProducto(ID_CATEGORIA, NOMBRE, DESCRIPCION, TIPO, BASICO, APLICA_A, TALLA[i], PLAZO_SOLICITUD_PLANTA, conexion);
                    if (ID_PRODUCTO == 0)
                    {
                        conexion.DeshacerTransaccion();
                    }
                    else if (!(String.IsNullOrEmpty(MensajeError)))
                    {
                        conexion.DeshacerTransaccion();
                    }
                }
            }
            if (!(String.IsNullOrEmpty(MensajeError)))
            {
                conexion.DeshacerTransaccion();
            }
            else
            {
                conexion.AceptarTransaccion();

            }
            conexion.Desconectar();
        }

        public DataTable ObtenerProductosPorIdProveedor(Decimal ID_PROVEEDOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_producto_obtener_por_id_proveedor ";

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
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

        public DataTable ObtenerTodosProductosActivos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_producto_obtener_todosActivos";

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

        public DataTable ObtenerReferenciasProductosPorPrefijo(Decimal ID_PROVEEDOR, String prefijo)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_ref_productos_proveedor_obtener_por_prefijo ";

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(prefijo) == false)
            {
                sql += "'" + prefijo + "'";
            }
            else
            {
                MensajeError += "El campo PREFIJO no puede ser nulo\n";
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

        #region alm_equipos

        public Decimal AdicionarAlmEquipo(int ID_DOCUMENTO, String MARCA, String MODELO, String SERIE, String IMEI, Decimal NUMERO_CELULAR,
            String DISPONIBLE, DateTime FECHA, Decimal ID_LOTE, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_EQUIPOS_ADICIONAR ";

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

            if (!(String.IsNullOrEmpty(MARCA)))
            {
                sql += "'" + MARCA + "', ";
                informacion += "MARCA = '" + MARCA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MARCA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MODELO)))
            {
                sql += "'" + MODELO + "', ";
                informacion += "MODELO= '" + MODELO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MODELO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SERIE)))
            {
                sql += "'" + SERIE + "', ";
                informacion += "SERIE= '" + SERIE.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "SERIE= 'null', ";
            }

            if (!(String.IsNullOrEmpty(IMEI)))
            {
                sql += "'" + IMEI + "', ";
                informacion += "IMEI= '" + IMEI.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "IMEI= 'null', ";
            }

            if (NUMERO_CELULAR != 0)
            {
                sql += NUMERO_CELULAR + ", ";
                informacion += "NUMERO_CELULAR = '" + NUMERO_CELULAR.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "NUMERO_CELULAR = '0', ";
            }

            if (!(String.IsNullOrEmpty(DISPONIBLE)))
            {
                sql += "'" + DISPONIBLE + "', ";
                informacion += "DISPONIBLE = '" + DISPONIBLE.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DISPONIBLE = 'null', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA_REGISTRO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE + ", ";
                informacion += "ID_LOTE = '" + ID_LOTE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_LOTE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE= '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Boolean adicionarAlmEquiposMasivo(int ID_DOCUMENTO, Decimal ID_LOTE, List<producto> listaEquipos)
        {
            Decimal ID_EQUIPO = 0;
            DateTime FECHA = DateTime.Now;
            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                foreach (producto equipo in listaEquipos)
                {
                    ID_EQUIPO = AdicionarAlmEquipo(ID_DOCUMENTO, equipo.MARCA, equipo.MODELO, equipo.SERIE, equipo.IMEI, equipo.NUMERO_CELULAR, "S", FECHA, ID_LOTE, conexion);
                    if (ID_EQUIPO == 0)
                    {
                        conexion.DeshacerTransaccion();
                        verificador = false;
                        break;
                    }
                }

                if (verificador == true)
                {
                    conexion.AceptarTransaccion();
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

            return verificador;
        }

        public Boolean ActualizarAlmEquipo(int ID_EQUIPO, int ID_DOCUMENTO, String MARCA, String MODELO, String SERIE, String IMEI, Decimal NUMERO_CELULAR,
            String DISPONIBLE, DateTime FECHA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_EQUIPOS_ACTUALIZAR";

            #region validaciones

            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO= '" + ID_EQUIPO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EQUIPO no puede ser nulo\n";
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

            if (!(String.IsNullOrEmpty(MARCA)))
            {
                sql += "'" + MARCA + "', ";
                informacion += "MARCA= '" + MARCA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MARCA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MODELO)))
            {
                sql += "'" + MODELO + "', ";
                informacion += "MODELO= '" + MODELO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MODELO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(SERIE)))
            {
                sql += "'" + SERIE + "', ";
                informacion += "SERIE= '" + SERIE.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "SERIE= 'null', ";
            }
            if (!(String.IsNullOrEmpty(IMEI)))
            {
                sql += "'" + IMEI + "', ";
                informacion += "IMEI= '" + IMEI.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "IMEI= 'null', ";
            }
            if (NUMERO_CELULAR != 0)
            {
                sql += NUMERO_CELULAR + ", ";
                informacion += "NUMERO_CELULAR = '" + NUMERO_CELULAR.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "NUMERO_CELULAR = '0', ";
            }
            if (!(String.IsNullOrEmpty(DISPONIBLE)))
            {
                sql += "'" + DISPONIBLE + "', ";
                informacion += "DISPONIBLE = '" + DISPONIBLE.ToString() + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "DISPONIBLE = 'null', ";
            }
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA_REGISTRO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD= '" + Usuario.ToString() + "', ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmEquipoPorId(int ID_EQUIPO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_EQUIPOS_OBTENER_ID  ";

            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO;
            }
            else
            {
                MensajeError += "El campo ID_EQUIPO no puede ser nulo\n";
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

        public DataTable ObtenerAlmEquipoPorIMEI(String IMEI)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_EQUIPOS_OBTENER_IMEI ";

            if (!(String.IsNullOrEmpty(IMEI)))
            {
                sql += "'" + IMEI + "' ";
                informacion += "IMEI= '" + IMEI.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo IMEI no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmEquipoPorIMEIBodega(String IMEI, int ID_BODEGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_EQUIPOS_OBTENER_IMEI_BODEGA ";

            if (!(String.IsNullOrEmpty(IMEI)))
            {
                sql += "'" + IMEI + "', ";
                informacion += "IMEI= '" + IMEI.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo IMEI no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA;
                informacion += "ID_BODEGA= '" + ID_BODEGA.ToString() + "'";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_EQUIPOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public Boolean ActualizarAlmEquipoIdLoteActualIDocumentoActualParaSalida(Decimal ID_EQUIPO,
            String DISPONIBLE,
            Decimal ID_LOTE_ACTUAL,
            Decimal ID_DOCUMENTO_ACTUAL,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_equipos_actualizar_lote_actual_y_id_documento_actual_para_saida ";

            #region validaciones
            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO= '" + ID_EQUIPO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EQUIPO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DISPONIBLE)))
            {
                sql += "'" + DISPONIBLE + "', ";
                informacion += "DISPONIBLE = '" + DISPONIBLE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DISPONIBLE = 'null', ";
            }

            if (ID_LOTE_ACTUAL != 0)
            {
                sql += ID_LOTE_ACTUAL + ", ";
                informacion += "ID_LOTE_ACTUAL = '" + ID_LOTE_ACTUAL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_LOTE_ACTUAL = 'null', ";
            }

            if (ID_DOCUMENTO_ACTUAL != 0)
            {
                sql += ID_DOCUMENTO_ACTUAL + ", ";
                informacion += "ID_DOCUMENTO_ACTUAL = '" + ID_DOCUMENTO_ACTUAL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_DOCUMENTO_ACTUAL = 'null', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

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

        #endregion
    }
}