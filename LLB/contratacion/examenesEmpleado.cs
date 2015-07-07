using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.almacen;
using Brainsbits.LLB.seleccion;

namespace Brainsbits.LLB.contratacion
{
    public class examenesEmpleado
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private decimal _registro = 0;
        private decimal _idOrden = 0;
        private int idLab = 0;
        private int idExamen = 0;
        private int registroAlmacen = 0;
        private DateTime fecha = new DateTime();
        private int idSolIngreso = 0;
        private int idRequerimientos = 0;
        private Boolean valida = false;

        private byte[] _ARCHIVO_EXAMEN = null;
        private String _ARCHIVO_EXTENSION = null;
        private Decimal _ARCHIVO_TAMANO = 0;
        private String _ARCHIVO_TYPE = null;
        private String _AutoRecomendacion = null;

        #endregion variables

        #region propiedades
        public Decimal registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        public Decimal IdOrden
        {
            get { return _idOrden; }
            set { _idOrden = value; }
        }

        public int IdLab
        {
            get { return idLab; }
            set { idLab = value; }
        }
        public int IdExamen
        {
            get { return idExamen; }
            set { idExamen = value; }
        }
        public int RegistroAlmacen
        {
            get { return registroAlmacen; }
            set { registroAlmacen = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        public int IdSolIngreso
        {
            get { return idSolIngreso; }
            set { idSolIngreso = value; }
        }
        public int IdRequerimientos
        {
            get { return idRequerimientos; }
            set { idRequerimientos = value; }
        }
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
        public Boolean Valida
        {
            get { return valida; }
            set { valida = value; }
        }

        public byte[] ARCHIVO_EXAMEN
        {
            get { return _ARCHIVO_EXAMEN; }
            set { _ARCHIVO_EXAMEN = value; }
        }

        public String ARCHIVO_EXTENSION
        {
            get { return _ARCHIVO_EXTENSION; }
            set { _ARCHIVO_EXTENSION = value; }
        }

        public Decimal ARCHIVO_TAMANO
        {
            get { return _ARCHIVO_TAMANO; }
            set { _ARCHIVO_TAMANO = value; }
        }

        public String ARCHIVO_TYPE
        {
            get { return _ARCHIVO_TYPE; }
            set { _ARCHIVO_TYPE = value; }
        }

        public String AutoRecomendacion
        {
            get { return _AutoRecomendacion; }
            set { _AutoRecomendacion = value; }
        }
        #endregion propiedades

        #region constructores
        public examenesEmpleado()
        {

        }

        public examenesEmpleado(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        #region con_reg_examenes_empleado

        public Decimal AdicionarConRegExamenesEmpleado(int ID_ORDEN, int ID_EXAMEN, Decimal COSTO, String VALIDADO, DateTime FECHA_EXAMEN, Conexion conexion)
        {
            String sql = null;
            Decimal identificador = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_EXAMENES_EMPLEADO_ADICIONAR ";

            #region validaciones
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EXAMEN != 0)
            {
                sql += ID_EXAMEN + ", ";
                informacion += "ID_EXAMEN = '" + ID_EXAMEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EXAMEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "COSTO = '0', ";
            }
            if (!(String.IsNullOrEmpty(VALIDADO)))
            {
                sql += "'" + VALIDADO + "', ";
                informacion += "VALIDADO = '" + VALIDADO.ToString() + "', ";
            }
            else
            {
                sql += "'N', ";
                informacion += "VALIDADO = 'N', ";
            }

            if (!(String.IsNullOrEmpty(FECHA_EXAMEN.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_EXAMEN) + "', ";
                informacion += "FECHA_EXAMEN = '" + FECHA_EXAMEN.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_EXAMENES_EMPLEADO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    identificador = 0;
                }
            }

            return identificador;
        }

        public Boolean ActualizarConRegExamenesEmpleado(int REGISTRO,
            int ID_ORDEN,
            int ID_EXAMEN,
            Decimal COSTO,
            String VALIDADO,
            DateTime FECHA_EXAMEN)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_EXAMENES_EMPLEADO_ACTUALIZAR ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EXAMEN != 0)
            {
                sql += ID_EXAMEN + ", ";
                informacion += "ID_EXAMEN = '" + ID_EXAMEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EXAMEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "COSTO = '0', ";
            }
            if (!(String.IsNullOrEmpty(VALIDADO)))
            {
                sql += "'" + VALIDADO + "', ";
                informacion += "VALIDADO = '" + VALIDADO.ToString() + "', ";
            }
            else
            {
                sql += "'N', ";
                informacion += "VALIDADO = 'N', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_EXAMEN.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_EXAMEN) + "', ";
                informacion += "FECHA_EXAMEN = '" + FECHA_EXAMEN.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_EXAMENES_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public Boolean ActualizarConRegExamenesEmpleado(Decimal REGISTRO,
            Decimal ID_ORDEN,
            Decimal ID_EXAMEN,
            Decimal COSTO,
            String VALIDADO,
            DateTime FECHA_EXAMEN,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_EXAMENES_EMPLEADO_ACTUALIZAR ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EXAMEN != 0)
            {
                sql += ID_EXAMEN + ", ";
                informacion += "ID_EXAMEN = '" + ID_EXAMEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EXAMEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "COSTO = '0', ";
            }
            if (!(String.IsNullOrEmpty(VALIDADO)))
            {
                sql += "'" + VALIDADO + "', ";
                informacion += "VALIDADO = '" + VALIDADO.ToString() + "', ";
            }
            else
            {
                sql += "'N', ";
                informacion += "VALIDADO = 'N', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_EXAMEN.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_EXAMEN) + "', ";
                informacion += "FECHA_EXAMEN = '" + FECHA_EXAMEN.ToString() + "', ";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_EXAMENES_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }






        public Boolean ActualizarConRegExamenesEmpleadoConArchivo(Decimal REGISTRO,
            Decimal ID_ORDEN,
            Decimal ID_EXAMEN,
            Decimal COSTO,
            String VALIDADO,
            DateTime FECHA_EXAMEN,
            byte[] ARCHIVO_EXAMEN,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            Conexion conexion)
        {
            String informacion = null;
            Boolean ejecutar = true;
            Int32 registro = 0;

            tools _tools = new tools();

            String sql = "USP_CON_REG_EXAMENES_EMPLEADO_ACTUALIZAR ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EXAMEN != 0)
            {
                sql += ID_EXAMEN + ", ";
                informacion += "ID_EXAMEN = '" + ID_EXAMEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EXAMEN no puede ser nulo\n";
                ejecutar = false;
            }
            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "COSTO = '0', ";
            }
            if (!(String.IsNullOrEmpty(VALIDADO)))
            {
                sql += "'" + VALIDADO + "', ";
                informacion += "VALIDADO = '" + VALIDADO.ToString() + "', ";
            }
            else
            {
                sql += "'N', ";
                informacion += "VALIDADO = 'N', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_EXAMEN.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_EXAMEN) + "', ";
                informacion += "FECHA_EXAMEN = '" + FECHA_EXAMEN.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = conexion.ExecuteNonQueryParaActualizarConRegExamenesEmpleadoConArchivo(REGISTRO, ID_ORDEN, ID_EXAMEN, COSTO, VALIDADO, FECHA_EXAMEN, Usuario, ARCHIVO_EXAMEN, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

                    if (registro == 0)
                    {
                        return false;
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        _auditoria.Adicionar(Usuario, tabla.CON_REG_EXAMENES_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                        #endregion auditoria

                        return true;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        public Boolean actualizarExamenesYFormaPago(List<examenesEmpleado> listaExamenes,
            Decimal ID_SOLICITUD,
            Decimal ID_ENTIDAD,
            String NUM_CUENTA,
            String FORMA_PAGO,
            String TIPO_CUENTA)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);
                autoRecomendaciones _auto = new autoRecomendaciones(Empresa, Usuario);

                if (_radicacionHojasDeVida.ActualizarEntidadNumCuenta(ID_SOLICITUD, ID_ENTIDAD, NUM_CUENTA, FORMA_PAGO, TIPO_CUENTA, conexion) == false)
                {
                    correcto = false;
                    MensajeError = _radicacionHojasDeVida.MensajeError;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    foreach (examenesEmpleado examen in listaExamenes)
                    {
                        if (examen.ARCHIVO_EXAMEN == null)
                        {
                            if (ActualizarConRegExamenesEmpleado(examen.registro, examen.IdOrden, examen.IdExamen, 0, "S", examen.Fecha, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                                break;
                            }
                        }
                        else
                        {
                            if (ActualizarConRegExamenesEmpleadoConArchivo(examen.registro, examen.IdOrden, examen.IdExamen, 0, "S", examen.Fecha, examen.ARCHIVO_EXAMEN, examen.ARCHIVO_EXTENSION, examen.ARCHIVO_TAMANO, examen.ARCHIVO_TYPE, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                                break;
                            }
                        }

                        if (correcto == true)
                        {
                            if (String.IsNullOrEmpty(examen.AutoRecomendacion) == false)
                            {
                                if (_auto.AdicionarConRegAutoRecomendaciones(examen.registro, examen.AutoRecomendacion, examen.Fecha, conexion) <= 0)
                                {
                                    correcto = false;
                                    MensajeError = _auto.MensajeError;
                                    conexion.DeshacerTransaccion();
                                    break;
                                }
                            }
                        }
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

        public DataTable ObtenerConRegExamenesEmpleadoPorRegistro(int REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_EXAMENES_EMPLEADO_OBTENER_POR_REGISTRO ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_EXAMENES_EMPLEADO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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




        #region alm_reg_productos_proveedor
        public DataTable ObtenerAlmRegProductosProveedorPorProductoCiudad(int ID_PRODUCTO, String ID_CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTOS_PROVEEDOR_OBTENER_PROVEEDORES_POR_EXAMEN ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "' ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "'";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO_PROVEEDOR, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegProductosProveedorPorProductoRegional(int ID_PRODUCTO, String ID_CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PRODUCTOS_PROVEEDOR_OBTENER_PROVEEDORES_POR_EXAMEN_POR_REGIONAL ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "' ";
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


        public DataTable ObtenerAlmRegProductosProveedorPorIdProveedor(Decimal ID_PROVEEDOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_productos_proveedor_por_id_proveedor ";

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
        public DataTable ObtenerAlmRegProductosProveedorPorIdProveedor(Decimal ID_PROVEEDOR, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_productos_proveedor_por_id_proveedor ";

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

        public Boolean AdicionarAlmRegProductosProveedor(Decimal ID_PROVEEDOR,
            List<producto> listaProductos)
        {
            Boolean resultado = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                producto _producto = new producto(Empresa, Usuario);
                DataTable tablaProductosProveedor = ObtenerAlmRegProductosProveedorPorIdProveedor(ID_PROVEEDOR, conexion);
                Boolean verificador = true;
                foreach (DataRow fila in tablaProductosProveedor.Rows)
                {
                    verificador = true;
                    foreach (producto infoProdProv in listaProductos)
                    {
                        if (infoProdProv.REGISTRO_P_P == Convert.ToDecimal(fila["REGISTRO_P_P"]))
                        {
                            verificador = false;
                            break;
                        }
                    }

                    if (verificador == true)
                    {
                        if (EliminarRegistroAlmRegProductoProveedor(Convert.ToDecimal(fila["REGISTRO_P_P"]), conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            resultado = false;
                            break;
                        }
                    }
                }

                if (resultado == true)
                {
                    foreach (producto infoProdProv in listaProductos)
                    {
                        if (infoProdProv.REGISTRO_P_P == 0)
                        {
                            if (AdicionarAlmRegProdProv(infoProdProv.ID_PRODUCTO, ID_PROVEEDOR, conexion) <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                resultado = false;
                                break;
                            }
                        }
                    }
                }

                if (resultado == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                resultado = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return resultado;
        }


        public Decimal AdicionarAlmRegProdProv(Decimal ID_PRODUCTO,
            Decimal REGISTRO_PROVEEDOR,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_REG_PRODUCTOS_PROVEEDOR_ADICIONAR ";

            #region validaciones
            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (REGISTRO_PROVEEDOR != 0)
            {
                sql += REGISTRO_PROVEEDOR + ", ";
                informacion += "REGISTRO_PROVEEDOR = '" + REGISTRO_PROVEEDOR + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO_PROVEEDOR, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Boolean EliminarRegistroAlmRegProductoProveedor(Decimal REGISTRO, Conexion conexion)
        {
            int cantidadRegistrosActualizados = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_productos_proveedor_eliminar_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Decimal ObtenerAlmRegProductosProveedorPorProductoProveedor(int ID_PRODUCTO, int ID_PROVEEDOR, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            String identificador = null;

            sql = "USP_ALM_REG_PRODUCTOS_PROVEEDOR_OBTENER_REGISTRO_POR_PRODUCTO_PROVEEDOR ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + " ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO_PROVEEDOR, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public Decimal ObtenerAlmRegProductosProveedorPorProductoProveedor(int ID_PRODUCTO, int ID_PROVEEDOR)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            String identificador = null;

            sql = "USP_ALM_REG_PRODUCTOS_PROVEEDOR_OBTENER_REGISTRO_POR_PRODUCTO_PROVEEDOR ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + " ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR.ToString() + "'";
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
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PRODUCTO_PROVEEDOR, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        #endregion

        public Boolean adicionarOrdenesExamenes(List<examenesEmpleado> examenesLab,
            Decimal ID_SOLICITUD,
            Decimal ID_ENTIDAD,
            String NUM_CUENTA,
            String FORMA_PAGO,
            String TIPO_CUENTA,
            Decimal ID_REQUERIMIENTO,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            Decimal ID_SERVICIO,
            Decimal ID_EMPRESA,
            Boolean TIENE_CUENTA)
        {
            Boolean correcto = true;

            ordenExamenes orden = new ordenExamenes(Empresa, Usuario);
            decimal idOrden = 0;
            int idLab = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                foreach (examenesEmpleado item in examenesLab)
                {
                    if (item.valida == false)
                    {
                        idLab = item.idLab;
                        idOrden = orden.AdicionarConRegOrdenExamen(item.idSolIngreso, item.idRequerimientos, conexion);
                        if (idOrden <= 0)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
                            MensajeError = orden.MensajeError;
                            break;
                        }
                        else
                        {
                            Decimal ID_EXAMEN_EMPLEADO = AdicionarConRegExamenesEmpleado(Convert.ToInt32(idOrden), item.registroAlmacen, 0, "N", item.fecha, conexion);

                            if (ID_EXAMEN_EMPLEADO <= 0)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                                break;
                            }
                            else
                            {
                                item.valida = true;
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);
                    if (_radicacionHojasDeVida.ActualizarEntidadNumCuenta(Convert.ToInt32(ID_SOLICITUD), Convert.ToInt32(ID_ENTIDAD), NUM_CUENTA, FORMA_PAGO, TIPO_CUENTA, conexion) == false)
                    {
                        correcto = false;
                        MensajeError = _radicacionHojasDeVida.MensajeError;
                        conexion.DeshacerTransaccion();
                    }
                }

                if (correcto == true)
                {
                    ConRegContratoTemporal _contratoTemporal = new ConRegContratoTemporal(Empresa, Usuario);

                    Decimal ID_TEMPORAL = _contratoTemporal.AdicionarConRegContratoTemporal(ID_REQUERIMIENTO, ID_SOLICITUD, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, ID_SERVICIO, ID_EMPRESA, TIENE_CUENTA, conexion);

                    if (ID_TEMPORAL <= 0)
                    {
                        correcto = false;
                        MensajeError = _contratoTemporal.MensajeError;
                        conexion.DeshacerTransaccion();
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (ExecutionEngineException ex)
            {
                correcto = false;
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }
        #endregion
    }
}
