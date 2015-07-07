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
    public class Entrega
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        Decimal _id_index = 0;
        Decimal _id_detalle_entrega = 0;
        Decimal _id_asignacion_sc = 0;
        Decimal _id_lote = 0;
        Decimal _id_documento = 0;
        Int32 _cantidad = 0;
        Decimal _sc_aiu = 0;
        Decimal _sc_iva = 0;
        Decimal _sc_valor = 0;
        String _talla = null;
        Decimal _costo_total = 0;
        Decimal _costo_unidad = 0;
        Decimal _id_producto = 0;
        Int32 _cantidad_total = 0;
        DateTime _fecha_proyecta_entrega = new DateTime();
        String _tipo_entrega = null;

        private enum TiposDeDocumento
        {
            ENTREGA = 0,
            FACTURA
        }

        private enum TiposMovimientos
        {
            ENTRADA,
            SALIDA
        }
        private enum EstadosDocumento
        {
            COMPLETO = 0
        }
        private enum EstadosAsignacionSC
        {
            ABIERTA = 0,
            TERMINADA,
            CANCELADA
        }
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

        public Decimal ID_INDEX
        {
            get { return _id_index; }
            set { _id_index = value; }
        }

        public Decimal ID_DETALLE_ENTREGA
        {
            get { return _id_detalle_entrega; }
            set { _id_detalle_entrega = value; }
        }

        public Decimal ID_ASIGNACION_SC
        {
            get { return _id_asignacion_sc; }
            set { _id_asignacion_sc = value; }
        }
        public Decimal ID_LOTE
        {
            get { return _id_lote; }
            set { _id_lote = value; }
        }

        public Decimal ID_DOCUMENTO
        {
            get { return _id_documento; }
            set { _id_documento = value; }
        }

        public Int32 CANTIDAD
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public Decimal SC_AIU
        {
            get { return _sc_aiu; }
            set { _sc_aiu = value; }
        }

        public Decimal SC_IVA
        {
            get { return _sc_iva; }
            set { _sc_iva = value; }
        }

        public Decimal SC_VALOR
        {
            get { return _sc_valor; }
            set { _sc_valor = value; }
        }

        public String TALLA
        {
            get { return _talla; }
            set { _talla = value; }
        }

        public Decimal COSTO_TOTAL
        {
            get { return _costo_total; }
            set { _costo_total = value; }
        }

        public Decimal COSTO_UNIDAD
        {
            get { return _costo_unidad; }
            set { _costo_unidad = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }

        public Int32 CANTIDAD_TOTAL
        {
            get { return _cantidad_total; }
            set { _cantidad_total = value; }
        }

        public DateTime FECHA_PROYECTADA_ENTREGA
        {
            get { return _fecha_proyecta_entrega; }
            set { _fecha_proyecta_entrega = value; }
        }

        public String TIPO_ENTREGA
        {
            get { return _tipo_entrega; }
            set { _tipo_entrega = value; }
        }
        #endregion propiedades

        #region constructores
        public Entrega(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public Entrega()
        {
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerConfiguracionEntregasPorEmpleado(Decimal ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_esc_crt_ntregas_sc ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
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

        public DataTable ObtenerInformacionPrimeraEntregaProductoYEmpleado(Decimal ID_EMPLEADO, Decimal ID_PRODUCTO, String TIPO_ENTREGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_esc_asignacion_servicios_complementarios_obtenerPorEmpleadoYProducto ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_ENTREGA) == false)
            {
                sql += "'" + TIPO_ENTREGA + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_ENTREGA no puede ser nulo\n";
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

        public DataTable ObtenerEntradasSalidasProductoPorCiudadYEmpresa(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_PRODUCTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_inventario_obtenerEntradasSalidasProductoPorCiudadYEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO;
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

        public DataTable ObtenerProveedoresPorProductoCiudadEmpresaYTalla(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_PRODUCTO, String TALLA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_inventario_obtenerProveedoresPorTallaCiudadYEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TALLA) == false)
            {
                sql += "'" + TALLA + "'";
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

        public DataTable ObtenerFacturasPorProveedorProductoCiudadEmpresaYTalla(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_PRODUCTO, String TALLA, Decimal ID_PROVEEDOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_inventario_obtenerFacturasPorProveedorTallaCiudadYEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TALLA) == false)
            {
                sql += "'" + TALLA + "', ";
            }
            else
            {
                MensajeError += "El campo TALLA no puede ser nulo\n";
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

        public DataTable ObtenerCantidadesEnLote(Decimal ID_DOCUMENTO,
            Decimal ID_LOTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_inventario_obtenerCantidadDisponibleLote_PorIdDocumentoIdLote ";

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE;
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


        public DataTable ObtenerEquiposDisponiblesEnLote(Decimal ID_DOCUMENTO,
            Decimal ID_LOTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_inventario_obtenerEquiposDisponiblesEnLote_PorIdDocumentoIdLote ";

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE;
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

        public DataTable ObtenerInformacionEntregasPorEmpleadoyTipoEntrega(Decimal ID_EMPLEADO, String TIPO_ENTREGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_esc_asignacion_servicios_complementarios_obtenerPorEmpleadoYTipoEntrega ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_ENTREGA) == false)
            {
                sql += "'" + TIPO_ENTREGA + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_ENTREGA no puede ser nulo\n";
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


        public Decimal AdicionarEntregaProductos(Decimal id_empleado,
            List<Entrega> listaProductosEntrega,
            List<Equipo> listaEquiposEntrega)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;

            Decimal id_documento_entrega = 0;

            try
            {
                documento _documento = new documento(Empresa, Usuario);
                id_documento_entrega = _documento.AdicionarAlmDocumentos(Convert.ToInt32(id_empleado), 0, TiposDeDocumento.ENTREGA.ToString(), null, null, DateTime.Now, new DateTime(), 0, 0, 0, EstadosDocumento.COMPLETO.ToString(), null, conexion, 0, null);

                if (id_documento_entrega <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    MensajeError = _documento.MensajeError;
                    id_documento_entrega = 0;
                }
                else
                {
                    lote _lote = new lote(Empresa, Usuario);
                    Inventario _inventario = new Inventario(Empresa, Usuario);

                    Dictionary<Decimal, Decimal> listaIndices = new Dictionary<Decimal, Decimal>();

                    foreach (Entrega productoEntrega in listaProductosEntrega)
                    {
                        DataTable tablaLote = _lote.ObtenerAlmLotePorId(Convert.ToInt32(productoEntrega.ID_LOTE), conexion);
                        DataRow filaLote = tablaLote.Rows[0];

                        Decimal ID_BODEGA = Convert.ToDecimal(filaLote["ID_BODEGA"]);
                        Decimal COSTO_UNIDAD = Convert.ToDecimal(filaLote["COSTO"]);
                        Decimal COSTO_TOTAL = (COSTO_UNIDAD * productoEntrega.CANTIDAD);

                        Decimal ID_INVENTARIO = _inventario.AdicionarAlmInventario(Convert.ToInt32(id_documento_entrega), Convert.ToInt32(productoEntrega.ID_PRODUCTO), Convert.ToInt32(ID_BODEGA), productoEntrega.CANTIDAD, Convert.ToDecimal(filaLote["COSTO"]), DateTime.Now, TiposMovimientos.SALIDA.ToString(), conexion, Convert.ToInt32(productoEntrega.ID_LOTE), productoEntrega.TALLA, 0, 0, null, filaLote["REEMBOLSO"].ToString());

                        if (ID_INVENTARIO <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            MensajeError = _inventario.MensajeError;
                            id_documento_entrega = 0;
                            break;
                        }
                        else
                        {
                            if (_lote.ActualizarCantidadesInventario(productoEntrega.ID_LOTE, productoEntrega.CANTIDAD, TiposMovimientos.SALIDA.ToString(), conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                MensajeError = _inventario.MensajeError;
                                id_documento_entrega = 0;
                                break;
                            }
                            else
                            {
                                Decimal ID_ASIGNACION_SC = 0;

                                if (productoEntrega.ID_ASIGNACION_SC == 0)
                                {
                                    if (listaIndices.ContainsKey(productoEntrega.ID_INDEX) == true)
                                    {
                                        ID_ASIGNACION_SC = listaIndices[productoEntrega.ID_INDEX];
                                    }
                                    else
                                    {
                                        ID_ASIGNACION_SC = AdicionarEscAsignacionSC(id_empleado, productoEntrega.ID_PRODUCTO, productoEntrega.CANTIDAD_TOTAL, 0, productoEntrega.FECHA_PROYECTADA_ENTREGA, productoEntrega.TIPO_ENTREGA, EstadosAsignacionSC.ABIERTA.ToString(), conexion);

                                        if (ID_ASIGNACION_SC <= 0)
                                        {
                                            conexion.DeshacerTransaccion();
                                            correcto = false;
                                            id_documento_entrega = 0;
                                            break;
                                        }
                                        else
                                        {
                                            listaIndices.Add(productoEntrega.ID_INDEX, ID_ASIGNACION_SC);
                                        }
                                    }
                                }
                                else
                                {
                                    ID_ASIGNACION_SC = productoEntrega.ID_ASIGNACION_SC;
                                }

                                Decimal ID_DETALLE_ASIGNACION = AdicionarEscDetalleContenidoAsignacion(ID_ASIGNACION_SC, id_documento_entrega, productoEntrega.CANTIDAD, productoEntrega.TALLA, COSTO_UNIDAD, COSTO_TOTAL, productoEntrega.ID_LOTE, conexion, ID_INVENTARIO);

                                if (ID_DETALLE_ASIGNACION <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    id_documento_entrega = 0;
                                    break;
                                }
                                else
                                {
                                    if (ActualizarCantidadesAsignacion(ID_ASIGNACION_SC, productoEntrega.CANTIDAD, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        correcto = false;
                                        id_documento_entrega = 0;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    listaIndices = new Dictionary<Decimal, Decimal>();

                    foreach (Equipo equipo in listaEquiposEntrega)
                    {
                        DataTable tablaLote = _lote.ObtenerAlmLotePorId(Convert.ToInt32(equipo.ID_LOTE), conexion);
                        DataRow filaLote = tablaLote.Rows[0];

                        Decimal ID_BODEGA = Convert.ToDecimal(filaLote["ID_BODEGA"]);
                        Decimal COSTO_UNIDAD = Convert.ToDecimal(filaLote["COSTO"]);

                        Decimal ID_INVENTARIO = _inventario.AdicionarAlmInventario(Convert.ToInt32(id_documento_entrega), Convert.ToInt32(equipo.ID_PRODUCTO), Convert.ToInt32(ID_BODEGA), 1, Convert.ToDecimal(filaLote["COSTO"]), DateTime.Now, TiposMovimientos.SALIDA.ToString(), conexion, Convert.ToInt32(equipo.ID_LOTE), filaLote["TALLA"].ToString(), Convert.ToInt32(equipo.ID_EQUIPO), 0, null, filaLote["REEMBOLSO"].ToString());

                        if (ID_INVENTARIO <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            MensajeError = _inventario.MensajeError;
                            id_documento_entrega = 0;
                            break;
                        }
                        else
                        {
                            if (_lote.ActualizarCantidadesInventario(equipo.ID_LOTE, 1, TiposMovimientos.SALIDA.ToString(), conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                MensajeError = _inventario.MensajeError;
                                id_documento_entrega = 0;
                                break;
                            }
                            else
                            {
                                Decimal ID_ASIGNACION_SC = 0;

                                if (equipo.ID_ASIGNACION_SC == 0)
                                {
                                    if (listaIndices.ContainsKey(equipo.ID_INDEX) == true)
                                    {
                                        ID_ASIGNACION_SC = listaIndices[equipo.ID_INDEX];
                                    }
                                    else
                                    {
                                        ID_ASIGNACION_SC = AdicionarEscAsignacionSC(id_empleado, equipo.ID_PRODUCTO, equipo.CANTIDAD_TOTAL, 0, equipo.FECHA_PROYECTADA_ENTREGA, equipo.TIPO_ENTREGA, EstadosAsignacionSC.ABIERTA.ToString(), conexion);

                                        if (ID_ASIGNACION_SC <= 0)
                                        {
                                            conexion.DeshacerTransaccion();
                                            correcto = false;
                                            id_documento_entrega = 0;
                                            break;
                                        }
                                        else
                                        {
                                            listaIndices.Add(equipo.ID_INDEX, ID_ASIGNACION_SC);
                                        }
                                    }
                                }
                                else
                                {
                                    ID_ASIGNACION_SC = equipo.ID_ASIGNACION_SC;
                                }

                                Decimal ID_DETALLE_ASIGNACION = AdicionarEscDetalleContenidoAsignacion(ID_ASIGNACION_SC, id_documento_entrega, 1, filaLote["TALLA"].ToString(), Convert.ToDecimal(filaLote["COSTO"]), Convert.ToDecimal(filaLote["COSTO"]), equipo.ID_LOTE, conexion, ID_INVENTARIO);

                                if (ID_DETALLE_ASIGNACION <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    id_documento_entrega = 0;
                                    break;
                                }
                                else
                                {
                                    if (ActualizarCantidadesAsignacion(ID_ASIGNACION_SC, 1, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        correcto = false;
                                        id_documento_entrega = 0;
                                        break;
                                    }
                                    else
                                    {
                                        Decimal ID_EQUIPO_ENTREGA = AdicionarAlmEquiposEntrega(equipo.ID_EQUIPO, ID_DETALLE_ASIGNACION, DateTime.Now, conexion);

                                        if (ID_EQUIPO_ENTREGA <= 0)
                                        {
                                            conexion.DeshacerTransaccion();
                                            correcto = false;
                                            id_documento_entrega = 0;
                                            break;
                                        }
                                        else
                                        {
                                            if (ActualizarDisponibilidadDeEquipo(equipo.ID_EQUIPO, "N", conexion) == false)
                                            {
                                                conexion.DeshacerTransaccion();
                                                correcto = false;
                                                id_documento_entrega = 0;
                                                break;
                                            }
                                        }
                                    }
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
                id_documento_entrega = 0;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return id_documento_entrega;
        }

        public Decimal AdicionarEscAsignacionSC(Decimal ID_EMPLEADO,
            Decimal ID_PRODUCTO,
            Int32 CANTIDAD_TOTAL,
            Int32 CANTIDAD_ENTREGADA,
            DateTime FCH_PROYECTA_ENTREGA,
            String TIPO_ENTREGA,
            String ESTADO,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_esc_asignacion_servicios_complementarios ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

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

            if (CANTIDAD_TOTAL != 0)
            {
                sql += CANTIDAD_TOTAL + ", ";
                informacion += "CANTIDAD_TOTAL = '" + CANTIDAD_TOTAL + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_TOTAL no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD_ENTREGADA != 0)
            {
                sql += CANTIDAD_ENTREGADA + ", ";
                informacion += "CANTIDAD_ENTREGADA = '" + CANTIDAD_ENTREGADA + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "CANTIDAD_ENTREGADA = '0', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_PROYECTA_ENTREGA) + "', ";
            informacion += "FCH_PROYECTA_ENTREGA = '" + FCH_PROYECTA_ENTREGA.ToShortDateString() + "', ";

            if (String.IsNullOrEmpty(TIPO_ENTREGA) == false)
            {
                sql += "'" + TIPO_ENTREGA + "', ";
                informacion += "TIPO_ENTREGA = '" + TIPO_ENTREGA + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_ENTREGA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CR E= '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ESC_ASIGNACION_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Decimal AdicionarEscDetalleContenidoAsignacion(Decimal ID_ASIGNACION_SC,
            Decimal ID_DOCUMENTO,
            Int32 CANTIDAD_ENTREGADA,
            String TALLA,
            Decimal COSTO_UNIDAD,
            Decimal COSTO_TOTAL,
            Decimal ID_LOTE,
            Conexion conexion,
            Decimal id_inventario)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_esc_detalle_contenido_asignacion_adicionar ";

            #region validaciones
            if (ID_ASIGNACION_SC != 0)
            {
                sql += ID_ASIGNACION_SC + ", ";
                informacion += "ID_ASIGNACION_SC = '" + ID_ASIGNACION_SC + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ASIGNACION_SC no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO = '" + ID_DOCUMENTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD_ENTREGADA != 0)
            {
                sql += CANTIDAD_ENTREGADA + ", ";
                informacion += "CANTIDAD_ENTREGADA = '" + CANTIDAD_ENTREGADA + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_ENTREGADA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TALLA) == false)
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA = '" + TALLA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TALLA = 'null', ";
            }

            if (COSTO_UNIDAD != 0)
            {
                sql += COSTO_UNIDAD.ToString().Replace(",", ".") + ", ";
                informacion += "COSTO_UNIDAD = '" + COSTO_UNIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COSTO_UNIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (COSTO_TOTAL != 0)
            {
                sql += COSTO_TOTAL.ToString().Replace(",", ".") + ", ";
                informacion += "COSTO_TOTAL = '" + COSTO_TOTAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COSTO_TOTAL no puede ser nulo\n";
                ejecutar = false;
            }

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

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (id_inventario != 0)
            {
                sql += id_inventario;
                informacion += "ID_INVENTARIO = '" + id_inventario + "'";
            }
            else
            {
                MensajeError += "El campo ID_INVENTARIO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ESC_DETALLE_CONTENIDO_ASIGNACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Decimal AdicionarAlmEquiposEntrega(Decimal ID_EQUIPO,
            Decimal ID_DETALLE_ENTREGA,
            DateTime FECHA_ENTREGA,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_equipos_entrega_adicionar ";

            #region validaciones
            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO = '" + ID_EQUIPO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EQUIPO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_DETALLE_ENTREGA != 0)
            {
                sql += ID_DETALLE_ENTREGA + ", ";
                informacion += "ID_DETALLE_ENTREGA = '" + ID_DETALLE_ENTREGA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE_ENTREGA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ENTREGA) + "', ";
            informacion += "FECHA_ENTREGA = '" + FECHA_ENTREGA.ToShortDateString() + "', ";

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
                    _auditoria.Adicionar(Usuario, tabla.ALM_EQUIPOS_ENTREGA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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




        public Boolean ActualizarCantidadesAsignacion(Decimal ID_ASIGNACION_SC,
            Int32 CANTIDAD_ENTREGADA,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_esc_asignacion_servicios_complementarios_actualizar_cantidad ";

            #region validaciones
            if (ID_ASIGNACION_SC != 0)
            {
                sql += ID_ASIGNACION_SC + ", ";
                informacion += "ID_ASIGNACION_SC = '" + ID_ASIGNACION_SC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ASIGNACION_SC no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD_ENTREGADA != 0)
            {
                sql += CANTIDAD_ENTREGADA + ", ";
                informacion += "CANTIDAD_ENTREGADA = '" + CANTIDAD_ENTREGADA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_ENTREGADA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ESC_ASIGNACION_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarDisponibilidadDeEquipo(Decimal ID_EQUIPO,
            String DISPONIBILIDAD,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_equipos_actualizar_disponibilidad ";

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

            if (String.IsNullOrEmpty(DISPONIBILIDAD) == false)
            {
                sql += "'" + DISPONIBILIDAD + "', ";
                informacion += "DISPONIBILIDAD = '" + DISPONIBILIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo DISPONIBILIDAD no puede ser nulo\n";
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

        public Boolean ActualizarAlmEquiposEntregaDevolucion(Decimal ID_EQUIPO_ENTREGA,
            Decimal ID_EQUIPO,
            Decimal ID_LOTE,
            Decimal ID_DOCUMENTO,
            String ESTADO_DEV,
            String DETALLE_DEV,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_equipos_entrega_actualizar_estado_devolucion ";

            #region validaciones
            if (ID_EQUIPO_ENTREGA != 0)
            {
                sql += ID_EQUIPO_ENTREGA + ", ";
                informacion += "ID_EQUIPO_ENTREGA = '" + ID_EQUIPO_ENTREGA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EQUIPO_ENTREGA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO = '" + ID_EQUIPO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EQUIPO no puede ser nulo\n";
                ejecutar = false;
            }

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

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO = '" + ID_DOCUMENTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO_DEV) == false)
            {
                sql += "'" + ESTADO_DEV + "', ";
                informacion += "ESATDO_DEV = '" + ESTADO_DEV + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO_DEV no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DETALLE_DEV) == false)
            {
                sql += "'" + DETALLE_DEV + "', ";
                informacion += "DETALLE_DEV = '" + DETALLE_DEV + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DETALLE_DEV = 'NULL', ";
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


        #endregion metodos
    }
}
