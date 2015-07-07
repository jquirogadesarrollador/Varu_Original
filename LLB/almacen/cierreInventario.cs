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
    public class cierreInventario
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
        public cierreInventario(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #region inv_cierre_inventario
        public Decimal AdicionarInvCierre(int ID_USUARIO, DateTime FECHA_CIERRE, DateTime FECHA_CORTE, DateTime FECHA_INICIAL, String OBSERVACIONES, String ESTADO, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_INV_CIERRE_INVENTARIO_ADICIONAR ";

            #region validaciones

            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
                informacion += "ID_USUARIO= '" + ID_USUARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_USUARIO no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
            informacion += "FECHA_CIERRE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CORTE) + "', ";
            informacion += "FECHA_CORTE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CORTE) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";
            informacion += "FECHA_INICIAL = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";


            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.INV_CIERRE_INVENTARIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarInvCierre(int ID_CIERRE, int ID_USUARIO, DateTime FECHA_CIERRE, DateTime FECHA_CORTE, String OBSERVACIONES, String ESTADO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_INV_CIERRE_INVENTARIO_ACTUALIZAR";

            #region validaciones

            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE + ", ";
                informacion += "ID_CIERRE= '" + ID_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
                informacion += "ID_USUARIO= '" + ID_USUARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_USUARIO no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
            informacion += "FECHA_CIERRE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CORTE) + "', ";
            informacion += "FECHA_CORTE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CORTE) + "', ";


            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }


            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.INV_CIERRE_INVENTARIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarInvCierre(int ID_CIERRE, String ESTADO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_INV_CIERRE_INVENTARIO_ACTUALIZAR_ESTADO ";

            #region validaciones

            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE + ", ";
                informacion += "ID_CIERRE= '" + ID_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }


            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.INV_CIERRE_INVENTARIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerInvCierrePorId(int ID_CIERRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_INV_CIERRE_INVENTARIO_OBTENER_POR_ID ";

            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE + " ";
                informacion += "ID_CIERRE= '" + ID_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.INV_CIERRE_INVENTARIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerInvCierrePorFechaCierre(DateTime FECHA_CIERRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "USP_INV_CIERRE_INVENTARIO_OBTENER_POR_FECHA_CIERRE ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
            informacion += "FECHA_CIERRE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";

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
                    _auditoria.Adicionar(Usuario, tabla.INV_CIERRE_INVENTARIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerInvCierreTodos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "USP_INV_CIERRE_INVENTARIO_OBTENER_TODOS ";

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

        public DataTable ObtenerInvCierreTodos(Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "USP_INV_CIERRE_INVENTARIO_OBTENER_TODOS ";

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
        #endregion inv_cierre_inventario

        #region inv_detalle_cierre

        public Decimal AdicionarInvDetalleCierre(int ID_CIERRE, int ID_PRODUCTO, int CANTIDAD_INVENTARIO, int CANTIDAD_FISICA,
            int ID_BODEGA, String TALLA, Decimal COSTO_PROMEDIO, String ESTADO, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_INV_DETALLE_CIERRE_ADICIONAR ";

            #region validaciones
            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE + ", ";
                informacion += "ID_CIERRE= '" + ID_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
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
            if (CANTIDAD_INVENTARIO != 0)
            {
                sql += CANTIDAD_INVENTARIO + ", ";
                informacion += "CANTIDAD_INVENTARIO= '" + CANTIDAD_INVENTARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_INVENTARIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (CANTIDAD_FISICA != 0)
            {
                sql += CANTIDAD_FISICA + ", ";
                informacion += "CANTIDAD_FISICA= '" + CANTIDAD_FISICA.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "CANTIDAD_FISICA= '0', ";
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
            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA= '" + TALLA.ToString() + "', ";
            }
            else
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA= '" + TALLA.ToString() + "', ";
            }
            if (COSTO_PROMEDIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO_PROMEDIO) + ", ";
                informacion += "COSTO_PROMEDIO = '" + COSTO_PROMEDIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COSTO_PROMEDIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.INV_DETALLE_CIERRE, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarInvDetalleCierre(int ID_DETALLE_CIERRE, int ID_CIERRE, int ID_PRODUCTO, int CANTIDAD_INVENTARIO, int CANTIDAD_FISICA,
            int ID_BODEGA, String TALLA, Decimal COSTO_PROMEDIO, String ESTADO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_INV_DETALLE_CIERRE_ACTUALIZAR ";

            #region validaciones
            if (ID_DETALLE_CIERRE != 0)
            {
                sql += ID_DETALLE_CIERRE + ", ";
                informacion += "ID_DETALLE_CIERRE= '" + ID_DETALLE_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE_CIERRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE + ", ";
                informacion += "ID_CIERRE= '" + ID_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
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
            if (CANTIDAD_INVENTARIO != 0)
            {
                sql += CANTIDAD_INVENTARIO + ", ";
                informacion += "CANTIDAD_INVENTARIO= '" + CANTIDAD_INVENTARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_INVENTARIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (CANTIDAD_FISICA != 0)
            {
                sql += CANTIDAD_FISICA + ", ";
                informacion += "CANTIDAD_FISICA= '" + CANTIDAD_FISICA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_FISICA no puede ser nulo\n";
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
            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA= '" + TALLA.ToString() + "', ";
            }
            else
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA= '" + TALLA.ToString() + "', ";
            }
            if (COSTO_PROMEDIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO_PROMEDIO) + ", ";
                informacion += "COSTO_PROMEDIO= '" + COSTO_PROMEDIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COSTO_PROMEDIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.INV_DETALLE_CIERRE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        /*
         @ID_DETALLE_CIERRE numeric,
         @ID_CIERRE numeric,
         @ID_PRODUCTO numeric,
         @CANTIDAD_FISICA numeric,
         @ID_BODEGA numeric,
         @ESTADO nvarchar(20),
         @USU_MOD nvarchar(50)
        */
        public Boolean ActualizarinvDetalleCierreCantidadFisicaIndividual(int ID_DETALLE_CIERRE,
            int ID_CIERRE,
            int ID_PRODUCTO,
            int CANTIDAD_FISICA,
            int ID_BODEGA,
            String ESTADO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_INV_DETALLE_CIERRE_ACTUALIZAR_CANTIDAD_FISICA ";

            #region validaciones

            if (ID_DETALLE_CIERRE != 0)
            {
                sql += ID_DETALLE_CIERRE + ", ";
                informacion += "ID_DETALLE_CIERRE = '" + ID_DETALLE_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE_CIERRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE + ", ";
                informacion += "ID_CIERRE = '" + ID_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD_FISICA != 0)
            {
                sql += CANTIDAD_FISICA + ", ";
                informacion += "CANTIDAD_FISICA = '" + CANTIDAD_FISICA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_FISICA no puede ser nulo\n";
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

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.INV_DETALLE_CIERRE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarInvDetalleCierreCantidadFisica(List<Int32> listaIdDetalleCierre,
            List<Int32> listaIdCierre,
            List<Int32> listaIdProducto,
            List<Int32> listaCantidadFisica,
            String ESTADO,
            List<Int32> listaBodega)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                for (int i = 0; i < listaIdProducto.Count; i++)
                {
                    if (ActualizarinvDetalleCierreCantidadFisicaIndividual(listaIdDetalleCierre[i], listaIdCierre[i], listaIdProducto[i], listaCantidadFisica[i], listaBodega[i], ESTADO, conexion) == false)
                    {
                        correcto = false;
                        conexion.DeshacerTransaccion();
                        break;
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerInvDetalleCierrePorId(int ID_DETALLE_CIERRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_INV_DETALLE_CIERRE_OBTENER_POR_ID ";

            if (ID_DETALLE_CIERRE != 0)
            {
                sql += ID_DETALLE_CIERRE + " ";
                informacion += "ID_DETALLE_CIERRE= '" + ID_DETALLE_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE_CIERRE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.INV_DETALLE_CIERRE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerInvDetalleCierrePorIdCierre(int ID_CIERRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_INV_DETALLE_CIERRE_OBTENER_POR_ID_CIERRE_INVENTARIO ";

            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE + " ";
                informacion += "ID_CIERRE= '" + ID_CIERRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.INV_DETALLE_CIERRE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerInvDetalleCierrePorIdCierre(int ID_CIERRE,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_INV_DETALLE_CIERRE_OBTENER_POR_ID_CIERRE_INVENTARIO ";

            if (ID_CIERRE != 0)
            {
                sql += ID_CIERRE;
            }
            else
            {
                MensajeError += "El campo ID_CIERRE no puede ser nulo\n";
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

        #endregion inv_detalle_cierre

        public DataTable ObtenerPlanillaFisica(String id_regional, String id_ciudad, int id_Bodega)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_INV_CIERRE_INVENTARIO_OBTENER_FISICO ";

            if (!(String.IsNullOrEmpty(id_regional)))
            {
                sql += "'" + id_regional + "', ";
                informacion += "id_regional= '" + id_regional.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo id_regional no puede ser nulo\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(id_ciudad)))
            {
                sql += "'" + id_ciudad + "', ";
                informacion += "id_ciudad= '" + id_ciudad.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo id_ciudad no puede ser nulo\n";
                ejecutar = false;
            }
            if (id_Bodega != 0)
            {
                sql += id_Bodega + " ";
                informacion += "id_Bodega= '" + id_Bodega.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo id_Bodega no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.INV_DETALLE_CIERRE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public Decimal guardarCierreInventario(DateTime FECHA_INICIO, DateTime FECHA_FIN)
        {
            Decimal idCierre = 0;
            Decimal idDetalle = 0;
            int idCierreActivo = 0;
            DataTable tablaCierres = new DataTable();
            Boolean correcto = true;
            bool ejecuta;

            Conexion _dates = new Conexion(Empresa);
            _dates.IniciarTransaccion();

            try
            {

                if (FECHA_INICIO < FECHA_FIN)
                {
                    MensajeError = null;
                    tablaCierres = ObtenerInvCierreTodos(_dates);

                    if (MensajeError != null)
                    {
                        correcto = false;
                        ejecuta = false;
                        _dates.DeshacerTransaccion();
                    }
                    else
                    {
                        if (tablaCierres.Rows.Count == 0)
                        {
                            if (FECHA_FIN <= System.DateTime.Today)
                            {
                                ejecuta = true;
                            }
                            else
                            {
                                ejecuta = false;
                                correcto = false;
                                _dates.DeshacerTransaccion();
                                MensajeError += "La fecha final no debe ser mayor a la fecha de hoy. Valide por favor";
                            }
                        }
                        else
                        {
                            DataRow filaCierres = tablaCierres.Rows[0];

                            DateTime FECHA_CORTE = Convert.ToDateTime(filaCierres["FECHA_CORTE"].ToString());
                            idCierreActivo = Convert.ToInt32(filaCierres["ID_CIERRE"].ToString());

                            if (FECHA_INICIO > FECHA_CORTE)
                            {
                                if (FECHA_FIN <= System.DateTime.Today)
                                {
                                    ejecuta = true;
                                }
                                else
                                {
                                    ejecuta = false;
                                    correcto = false;
                                    _dates.DeshacerTransaccion();
                                    MensajeError += "La fecha final no debe ser mayor a la fecha de hoy. Valide por favor";
                                }
                            }
                            else
                            {
                                MensajeError = "ADVERTENCIA: La fecha de inicio está contenida en el último cierre. Valide por favor.";
                                ejecuta = false;
                                correcto = false;
                                _dates.DeshacerTransaccion();
                            }
                        }
                    }
                }
                else
                {
                    MensajeError += "La fecha de fin debe ser mayor que la fecha de inicio. Valide por favor.";
                    correcto = false;
                    ejecuta = false;
                    _dates.DeshacerTransaccion();
                }

                if (ejecuta)
                {
                    Inventario _inventario = new Inventario(Empresa, Usuario);
                    DataTable tablaInventario = _inventario.ObtenerAlmInventarioPorPeriodo(FECHA_INICIO, FECHA_FIN, _dates);

                    if (_inventario.MensajeError != null)
                    {
                        ejecuta = false;
                        correcto = false;
                        MensajeError = _inventario.MensajeError;
                        _dates.DeshacerTransaccion();
                    }
                    else
                    {
                        DataTable tablaCierre = new DataTable();

                        tablaCierre.Columns.Add("ID_PRODUCTO", typeof(Decimal));
                        tablaCierre.Columns.Add("ID_BODEGA", typeof(Decimal));
                        tablaCierre.Columns.Add("TALLA", typeof(String));
                        tablaCierre.Columns.Add("CANTIDAD", typeof(Int32));
                        tablaCierre.Columns.Add("COSTO", typeof(Decimal));

                        if (idCierreActivo != 0)
                        {
                            DataTable tablaDetalles = ObtenerInvDetalleCierrePorIdCierre(idCierreActivo, _dates);

                            foreach (DataRow filac in tablaDetalles.Rows)
                            {
                                DataRow filan = tablaCierre.NewRow();

                                filan["ID_PRODUCTO"] = Convert.ToDecimal(filac["ID_PRODUCTO"]);
                                filan["ID_BODEGA"] = Convert.ToDecimal(filac["ID_BODEGA"]);
                                filan["TALLA"] = filac["TALLA"];
                                filan["CANTIDAD"] = Convert.ToInt32(filac["CANTIDAD_INVENTARIO"]);
                                filan["COSTO"] = Convert.ToDecimal(filac["COSTO_PROMEDIO"]);
                                tablaCierre.Rows.Add(filan);
                            }
                        }


                        DataRow filam;
                        usuario _usuario = new usuario(Empresa);
                        DataTable tablaUsuario = _usuario.ObtenerUsuarioPorUsuLog(Usuario, _dates);

                        if (_usuario.MensajeError != null)
                        {
                            ejecuta = false;
                            correcto = false;
                            MensajeError = _usuario.MensajeError;
                            _dates.DeshacerTransaccion();
                        }
                        else
                        {
                            DataRow filaUsuario = tablaUsuario.Rows[0];

                            /*
                             * ES MUY IMPORTANTE ESTO,
                             * EN LA UTLIMA REUNION SE DECLARO QUE EL BOTON DE CIERRE FISICO NO SERVIA
                             * POR LO TANTO AUTOMATICAMENTE EL ACREAR EL CIERRE DE UNA LO DEJAMOS EN ESTADO 'ACTIVO', 
                             * ANTES SE GUARDABA EN ESTADO 'ABIERTO'
                            */
                            idCierre = AdicionarInvCierre(Convert.ToInt32(filaUsuario["ID_USUARIO"]), System.DateTime.Today, FECHA_FIN, FECHA_INICIO, "", "ACTIVO", _dates);

                            if (idCierre != 0)
                            {
                                foreach (DataRow fila in tablaInventario.Rows)
                                {
                                    if (tablaCierre.Rows.Count <= 0)
                                    {
                                        filam = tablaCierre.NewRow();

                                        filam["ID_PRODUCTO"] = fila["ID_PRODUCTO"];
                                        filam["ID_BODEGA"] = fila["ID_BODEGA"];
                                        filam["TALLA"] = fila["TALLA"];

                                        if (fila["MOVIMIENTO"].Equals("ENTRADA"))
                                        {
                                            filam["CANTIDAD"] = Convert.ToInt32(fila["CANTIDAD"]);
                                            filam["COSTO"] = Convert.ToDecimal(fila["COSTO"]);
                                        }
                                        else if (fila["MOVIMIENTO"].Equals("SALIDA"))
                                        {
                                            filam["CANTIDAD"] = -Convert.ToInt32(fila["CANTIDAD"]);
                                            filam["COSTO"] = 1; 
                                        }

                                        tablaCierre.Rows.Add(filam);
                                    }
                                    else
                                    {


                                        DataRow[] filasCoinciden = tablaCierre.Select("ID_PRODUCTO = '" + fila["ID_PRODUCTO"].ToString() + "' AND ID_BODEGA = '" + fila["ID_BODEGA"].ToString() + "' AND TALLA = '" + fila["TALLA"].ToString() + "' AND COSTO = '" + fila["COSTO"].ToString() + "'");

                                        if (filasCoinciden.Length <= 0)
                                        {
                                            filam = tablaCierre.NewRow();

                                            filam["ID_PRODUCTO"] = fila["ID_PRODUCTO"];
                                            filam["ID_BODEGA"] = fila["ID_BODEGA"];
                                            filam["TALLA"] = fila["TALLA"];

                                            if (fila["MOVIMIENTO"].Equals("ENTRADA"))
                                            {
                                                filam["CANTIDAD"] = Convert.ToInt32(fila["CANTIDAD"]);
                                                filam["COSTO"] = Convert.ToDecimal(fila["COSTO"]);
                                            }
                                            else if (fila["MOVIMIENTO"].Equals("SALIDA"))
                                            {
                                                filam["CANTIDAD"] = -Convert.ToInt32(fila["CANTIDAD"]);
                                                filam["COSTO"] = 1;
                                            }

                                            tablaCierre.Rows.Add(filam);
                                        }
                                        else
                                        {
                                            DataRow filaCoincide = filasCoinciden[0];

                                            Decimal costoN = 0;
                                            Int32 cantidadN = 0;
                                            Decimal CostoA = 0;
                                            Int32 cantidadA = 0;

                                            if (!(String.IsNullOrEmpty(fila["CANTIDAD"].ToString())))
                                            {
                                                cantidadN = Convert.ToInt32(fila["CANTIDAD"]);
                                            }

                                            if (!(String.IsNullOrEmpty(fila["COSTO"].ToString())))
                                            {
                                                costoN = Convert.ToDecimal(fila["COSTO"]);
                                            }

                                            if (!(String.IsNullOrEmpty(filaCoincide["CANTIDAD"].ToString())))
                                            {
                                                cantidadA = Convert.ToInt32(filaCoincide["CANTIDAD"]);
                                            }

                                            if (!(String.IsNullOrEmpty(filaCoincide["COSTO"].ToString())))
                                            {
                                                CostoA = Convert.ToDecimal(filaCoincide["COSTO"]);
                                            }

                                            if (fila["MOVIMIENTO"].Equals("ENTRADA"))
                                            {
                                                filaCoincide["CANTIDAD"] = cantidadA + cantidadN;

                                                if (cantidadA < 0)
                                                {
                                                    cantidadA = cantidadA * (-1);
                                                }

                                                if (cantidadN < 0)
                                                {
                                                    cantidadN = cantidadN * (-1);
                                                }

                                                filaCoincide["COSTO"] = (((cantidadA * CostoA) + (cantidadN * costoN)) / (cantidadA + cantidadN));
                                            }
                                            else if (fila["MOVIMIENTO"].Equals("SALIDA"))
                                            {
                                                filaCoincide["CANTIDAD"] = cantidadA - cantidadN;
                                            }
                                        }
                                    }
                                }

                                if (correcto == true)
                                {

                                    int x = 0;

                                    if (tablaCierre.Rows.Count != 0)
                                    {
                                        foreach (DataRow filax in tablaCierre.Rows)
                                        {
                                            Decimal costo = 0;

                                            if (!(String.IsNullOrEmpty(filax["COSTO"].ToString())))
                                            {
                                                costo = Convert.ToDecimal(filax["COSTO"]);
                                            }

                                            /*
                                             * ACA TAMBIEN ES IMPORTANTE ACLARAR 
                                             * QUE DESPUES DE LA ULTIMA REUNION EL BOTON DE CIERRE FISICO SE DEBIA QUITAR
                                             * ENTONCES LOS DETALLES DEL CIERRE DE UNA VEZ SE DEJAN EN ESTADO 'COMPLETADO'
                                             * ADEMAS EL VALOR DE LA  CANTIDAD FISICA SE DEJA IGUAL QUE LA CANTIDAD INVENTARIO
                                             * ANTES SE CREABA EL REGISTRO EN ESTADO 'ABIERTO' Y LA CANTIDAD FISICA EN CERO
                                             * PARA QUE DESPUES POR CIERRE FISICO SE VALIDARA DICHA CANTIDAD
                                             * 
                                            */
                                            idDetalle = AdicionarInvDetalleCierre(Convert.ToInt32(idCierre), Convert.ToInt32(filax["ID_PRODUCTO"]), Convert.ToInt32(filax["CANTIDAD"]), Convert.ToInt32(filax["CANTIDAD"]), Convert.ToInt32(filax["ID_BODEGA"]), filax["TALLA"].ToString(), costo, "COMPLETADO", _dates);
                                            if (idDetalle != 0)
                                            {
                                                x++;
                                            }
                                            else
                                            {
                                                _dates.DeshacerTransaccion();
                                                correcto = false;
                                                ejecuta = false;
                                                idCierre = 0;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MensajeError += "No hay detalles en el cierre.";
                                        idCierre = 0;
                                        correcto = false;
                                        ejecuta = false;
                                        _dates.DeshacerTransaccion();
                                    }
                                }
                            }
                            else
                            {
                                _dates.DeshacerTransaccion();
                                correcto = false;
                                ejecuta = false;
                                idCierre = 0;
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    _dates.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                correcto = false;
                ejecuta = false;
                _dates.DeshacerTransaccion();
            }
            finally
            {
                _dates.Desconectar();
            }

            if (correcto == true)
            {
                return idCierre;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
