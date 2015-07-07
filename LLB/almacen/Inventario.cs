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
    public class Inventario
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
        public Inventario(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarAlmInventario(int ID_DOCUMENTO,
            int ID_PRODUCTO,
            int ID_BODEGA,
            int CANTIDAD,
            Decimal COSTO,
            DateTime FECHA,
            String MOVIMIENTO,
            Conexion conexion,
            int ID_LOTE,
            String TALLA,
            int ID_EQUIPO,
            Decimal ID_DETELLE_ENTREGA,
            String DETALLE_MOVIMIENTO,
            String REEMBOLSO)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_INVENTARIO_ADICIONAR ";

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

            if (CANTIDAD != 0)
            {
                sql += CANTIDAD + ", ";
                informacion += "CANTIDAD= '" + CANTIDAD.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "CANTIDAD= '0', ";
            }

            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO= '" + COSTO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "COSTO= '0', ";
            }
            if (!(String.IsNullOrEmpty(FECHA.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
                informacion += "FECHA= '" + FECHA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOVIMIENTO)))
            {
                sql += "'" + MOVIMIENTO + "', ";
                informacion += "MOVIMIENTO= '" + MOVIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MOVIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

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

            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA= '" + TALLA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TALLA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO = '" + ID_EQUIPO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EQUIPO= '0', ";
            }

            if (ID_DETELLE_ENTREGA != 0)
            {
                sql += ID_DETELLE_ENTREGA + ", ";
                informacion += "ID_DETELLE_ENTREGA = '" + ID_DETELLE_ENTREGA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_DETELLE_ENTREGA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(DETALLE_MOVIMIENTO) == false)
            {
                sql += "'" + DETALLE_MOVIMIENTO + "', ";
                informacion += "DETALLE_MOVIMIENTO = '" + DETALLE_MOVIMIENTO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_DETELLE_ENTREGA = 'NULL', ";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_INVENTARIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarAlmInventario(int ID_INVENTARIO, int ID_DOCUMENTO, int ID_PRODUCTO, int ID_BODEGA, int CANTIDAD, Decimal COSTO,
            DateTime FECHA, String MOVIMIENTO, int ID_LOTE, String TALLA, int ID_EQUIPO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_INVENTARIO_ACTUALIZAR";

            #region validaciones
            if (ID_INVENTARIO != 0)
            {
                sql += ID_INVENTARIO + ", ";
                informacion += "ID_INVENTARIO= '" + ID_INVENTARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_INVENTARIO no puede ser nulo\n";
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

            if (CANTIDAD != 0)
            {
                sql += CANTIDAD + ", ";
                informacion += "CANTIDAD= '" + CANTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD no puede ser nulo\n";
                ejecutar = false;
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
            if (!(String.IsNullOrEmpty(FECHA.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
                informacion += "FECHA= '" + FECHA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOVIMIENTO)))
            {
                sql += "'" + MOVIMIENTO + "', ";
                informacion += "MOVIMIENTO= '" + MOVIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MOVIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

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

            if (!(String.IsNullOrEmpty(TALLA)))
            {
                sql += "'" + TALLA + "', ";
                informacion += "TALLA = '" + TALLA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TALLA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + " ";
                informacion += "ID_EQUIPO= '" + ID_EQUIPO.ToString() + "' ";
            }
            else
            {
                sql += "0 ";
                informacion += "ID_EQUIPO= '0' ";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_INVENTARIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmInventarioPorId(int ID_INVENTARIO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_INVENTARIO_OBTENER ";

            if (ID_INVENTARIO != 0)
            {
                sql += ID_INVENTARIO + " ";
                informacion += "ID_INVENTARIO= '" + ID_INVENTARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_INVENTARIO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_INVENTARIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerAlmInventarioFechaInicial()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_inventario_obtenerFechaInicial";

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


        public DataTable ObtenerAlmInventarioPorIdBodega(int ID_BODEGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_INVENTARIO_OBTENER_PRODUCTO_POR_BODEGA ";

            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + " ";
                informacion += "ID_BODEGA= '" + ID_BODEGA.ToString() + "', ";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_INVENTARIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public Decimal guardarTraslado(Decimal id_bodega_origen,
            List<int> lista_productos,
            List<int> lista_Cantidades,
            List<int> lista_lotes,
            Decimal id_bodega_destino,
            String Observaciones,
            List<String> talla,
            List<EquipoTraslado> listaEquiposTraslado)
        {
            int cantidadRegistrosActualizados = 0;
            int ConteLo;

            DateTime fecha = System.DateTime.Today;
            Decimal idInventario = 0;
            Decimal idDOC = 0;

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                lote _lote = new lote(Empresa, Usuario);

                documento _doc = new documento(Empresa, Usuario);
                idDOC = _doc.AdicionarAlmDocumentos(0, 0, "TRASLADO", "", "", fecha, new DateTime(), Convert.ToInt32(id_bodega_destino), 0, 0, "ELABORADO", Observaciones, conexion, 0, null);

                if (idDOC == 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    MensajeError = _doc.MensajeError;
                }
                else
                {
                    for (int i = 0; i < lista_productos.Count; i++)
                    {
                        _lote.MensajeError = null;
                        DataTable tablalo = _lote.ObtenerAlmLotePorId(lista_lotes[i], conexion);

                        if (_lote.MensajeError != null)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                        else
                        {
                            DataRow filalo = tablalo.Rows[0];
                            ConteLo = Convert.ToInt32(filalo["ENTRADAS"]) - Convert.ToInt32(filalo["SALIDAS"]);

                            if (lista_Cantidades[i] <= ConteLo)
                            {
                                idInventario = AdicionarAlmInventario(Convert.ToInt32(idDOC), lista_productos[i], Convert.ToInt32(id_bodega_origen), lista_Cantidades[i], Convert.ToDecimal(filalo["COSTO"]), fecha, "SALIDA", conexion, lista_lotes[i], talla[i], 0, 0, null, filalo["REEMBOLSO"].ToString());

                                if (idInventario == 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError += "ADVERTENCIA: El registro de inventario no pudo ser creado. Valide Por favor.";
                                    correcto = false;
                                    break;
                                }
                                else
                                {
                                    String activo = "S";
                                    if (ConteLo - lista_Cantidades[i] <= 0)
                                    {
                                        activo = "N";
                                    }

                                    if (_lote.ActualizarAlmLote(lista_lotes[i], Convert.ToInt32(filalo["ID_DOCUMENTO"]), lista_productos[i], Convert.ToInt32(id_bodega_origen), Convert.ToDateTime(filalo["FECHA_REGISTRO"]), Convert.ToInt32(filalo["ENTRADAS"]), Convert.ToInt32(filalo["SALIDAS"]) + lista_Cantidades[i], Convert.ToDecimal(filalo["COSTO"]), filalo["TALLA"].ToString(), activo, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        correcto = false;
                                        MensajeError += "ADVERTENCIA: El registro del lote no pudo ser actualizado. Verifique Por Favor";
                                        break;
                                    }
                                    else
                                    {
                                        cantidadRegistrosActualizados++;
                                    }
                                }
                            }
                            else
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                MensajeError += "ADVERTENCIA: La cantidad de producto (" + lista_productos[i].ToString() + ") solicitada no puede ser sacada del lote (" + lista_lotes[i].ToString() + ") seleccionado, cambie la cantidad o haga varios registros con cantidades posibles.";
                                break;
                            }
                        }
                    }

                    foreach (EquipoTraslado equipo in listaEquiposTraslado)
                    {
                        _lote.MensajeError = null;
                        DataTable tablalo = _lote.ObtenerAlmLotePorId(Convert.ToInt32(equipo.ID_LOTE), conexion);

                        if (_lote.MensajeError != null)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                        else
                        {
                            DataRow filalo = tablalo.Rows[0];
                            ConteLo = Convert.ToInt32(filalo["ENTRADAS"]) - Convert.ToInt32(filalo["SALIDAS"]);

                            if (1 <= ConteLo)
                            {
                                idInventario = AdicionarAlmInventario(Convert.ToInt32(idDOC), Convert.ToInt32(equipo.ID_PRODUCTO), Convert.ToInt32(id_bodega_origen), 1, Convert.ToDecimal(filalo["COSTO"]), fecha, "SALIDA", conexion, Convert.ToInt32(equipo.ID_LOTE), filalo["TALLA"].ToString(), Convert.ToInt32(equipo.ID_EQUIPO), 0, null, filalo["REEMBOLSO"].ToString());

                                if (idInventario == 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError += "ADVERTENCIA: El registro de inventario no pudo ser creado. Valide Por favor.";
                                    correcto = false;
                                    break;
                                }
                                else
                                {
                                    String activo = "S";
                                    if (ConteLo - 1 <= 0)
                                    {
                                        activo = "N";
                                    }

                                    if (_lote.ActualizarAlmLote(Convert.ToInt32(equipo.ID_LOTE), Convert.ToInt32(filalo["ID_DOCUMENTO"]), Convert.ToInt32(equipo.ID_PRODUCTO), Convert.ToInt32(filalo["ID_BODEGA"]), Convert.ToDateTime(filalo["FECHA_REGISTRO"]), Convert.ToInt32(filalo["ENTRADAS"]), Convert.ToInt32(filalo["SALIDAS"]) + 1, Convert.ToDecimal(filalo["COSTO"]), filalo["TALLA"].ToString(), activo, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        correcto = false;
                                        MensajeError += "ADVERTENCIA: El registro del lote no pudo ser actualizado. Verifique Por Favor";
                                        break;
                                    }
                                    else
                                    {
                                        _lote.MensajeError = null;
                                        if (_lote.ActualizarLoteActualDeUnEquipo(equipo.ID_EQUIPO, 0, 0, conexion) == false)
                                        {
                                            conexion.DeshacerTransaccion();
                                            correcto = false;
                                            MensajeError = _lote.MensajeError;
                                            break;
                                        }
                                        else
                                        {
                                            cantidadRegistrosActualizados++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                MensajeError += "ADVERTENCIA: La cantidad de (equipo(s)) solicitada no puede ser sacada del lote (" + equipo.ID_LOTE.ToString() + ") seleccionado, cambie la cantidad o haga varios registros con cantidades posibles.";
                                break;
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

            if (correcto == true)
            {
                return idDOC;
            }
            else
            {
                return 0;
            }
        }

        public Boolean guardarTrasladoIngreso(int idDocumento,
            List<int> listaProductos,
            int idbodegaDest,
            List<int> listacantidad,
            List<int> listalote,
            List<String> listaTallas,
            int falta,
            List<EquipoTraslado> listaEquiposTraslado)
        {
            int transaccion = 0;
            Boolean retorno = true;
            Decimal idLote = 0;
            String estado;
            Decimal id_inventario = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            lote _lote = new lote(Empresa, Usuario);

            try
            {
                for (int i = 0; i < listaProductos.Count; i++)
                {
                    _lote.MensajeError = null;
                    DataTable tablalote = _lote.ObtenerAlmLotePorId(listalote[i], conexion);

                    if (_lote.MensajeError != null)
                    {
                        retorno = false;
                        MensajeError = _lote.MensajeError;
                        conexion.DeshacerTransaccion();
                        break;
                    }
                    else
                    {
                        DataRow filalote = tablalote.Rows[0];
                        Decimal costo = Convert.ToDecimal(filalote["COSTO"]);
                        DateTime fecha = System.DateTime.Today;

                        idLote = _lote.AdicionarAlmLote(Convert.ToInt32(filalote["ID_DOCUMENTO"]), listaProductos[i], idbodegaDest, fecha, listacantidad[i], 0, costo, listaTallas[i], "S", conexion, filalote["REEMBOLSO"].ToString());
                        if (idLote <= 0)
                        {
                            retorno = false;
                            conexion.DeshacerTransaccion();
                            MensajeError += "No fue posible crear el lote por: " + _lote.MensajeError;
                            break;
                        }
                        else
                        {
                            id_inventario = AdicionarAlmInventario(idDocumento, listaProductos[i], idbodegaDest, listacantidad[i], costo, fecha, "ENTRADA", conexion, Convert.ToInt32(idLote), listaTallas[i], 0, 0, null, filalote["REEMBOLSO"].ToString());

                            if (id_inventario <= 0)
                            {
                                retorno = false;
                                conexion.DeshacerTransaccion();
                                break;
                            }
                            else
                            {
                                transaccion++;
                            }
                        }
                    }
                }

                if (retorno == true)
                {
                    foreach (EquipoTraslado equipo in listaEquiposTraslado)
                    {
                        _lote.MensajeError = null;
                        DataTable tablalote = _lote.ObtenerAlmLotePorId(Convert.ToInt32(equipo.ID_LOTE), conexion);

                        if (_lote.MensajeError != null)
                        {
                            retorno = false;
                            MensajeError = _lote.MensajeError;
                            conexion.DeshacerTransaccion();
                            break;
                        }
                        else
                        {
                            DataRow filalote = tablalote.Rows[0];
                            Decimal costo = Convert.ToDecimal(filalote["COSTO"]);
                            DateTime fecha = System.DateTime.Today;

                            idLote = _lote.AdicionarAlmLote(Convert.ToInt32(filalote["ID_DOCUMENTO"]), Convert.ToInt32(equipo.ID_PRODUCTO), idbodegaDest, fecha, 1, 0, costo, filalote["TALLA"].ToString(), "S", conexion, filalote["REEMBOLSO"].ToString());
                            if (idLote <= 0)
                            {
                                retorno = false;
                                conexion.DeshacerTransaccion();
                                MensajeError += "No fue posible crear el lote por: " + _lote.MensajeError;
                                break;
                            }
                            else
                            {
                                id_inventario = AdicionarAlmInventario(idDocumento, Convert.ToInt32(equipo.ID_PRODUCTO), idbodegaDest, 1, costo, fecha, "ENTRADA", conexion, Convert.ToInt32(idLote), filalote["TALLA"].ToString(), Convert.ToInt32(equipo.ID_EQUIPO), 0, null, filalote["REEMBOLSO"].ToString());

                                if (id_inventario <= 0)
                                {
                                    retorno = false;
                                    conexion.DeshacerTransaccion();
                                    break;
                                }
                                else
                                {
                                    if (_lote.ActualizarLoteActualDeUnEquipo(equipo.ID_EQUIPO, idLote, Convert.ToDecimal(idDocumento), conexion) == false)
                                    {
                                        retorno = false;
                                        conexion.DeshacerTransaccion();
                                        MensajeError = _lote.MensajeError;
                                        break;
                                    }
                                    else
                                    {
                                        transaccion++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (retorno == true)
                {
                    if (falta > 0)
                    {
                        estado = "RECIBIDO";
                    }
                    else
                    {
                        estado = "COMPLETO";
                    }

                    if (transaccion == (listaProductos.Count + listaEquiposTraslado.Count))
                    {
                        documento _doc = new documento(Empresa, Usuario);
                        DataTable tablaDoc = _doc.ObtenerAlmRegDocumentoPorId(idDocumento);
                        DataRow filaDoc = tablaDoc.Rows[0];

                        if (_doc.ActualizarAlmDocumento(idDocumento, 0, 0, "TRASLADO", "", "", Convert.ToDateTime(filaDoc["FECHA_DOCUMENTO"]), new DateTime(), idbodegaDest, 0, 0, estado, filaDoc["OBSERVACION_JUSTIFICACION"].ToString(), 0, conexion) == false)
                        {
                            retorno = false;
                            MensajeError = _doc.MensajeError;
                            conexion.DeshacerTransaccion();
                        }
                    }
                    else
                    {
                        MensajeError = "la transaccion arrojó un resultado inconcistente en cuanto a los productos que se tenian que procesar.";
                        retorno = false;
                        conexion.DeshacerTransaccion();
                    }
                }

                if (retorno == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                retorno = false;
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return retorno;
        }

        public Decimal guardarGarantia(int idbodega,
            int idLote,
            int idEmpresa,
            int idProducto,
            int Cantidad,
            String idRegional,
            String idCiudad,
            String talla,
            String motivo)
        {

            Boolean correcto = true;

            lote _lote = new lote(Empresa, Usuario);
            documento _doc = new documento(Empresa, Usuario);
            Decimal idDoc = 0;
            Decimal idInventario = 0;
            int idProveedor = 0;

            Conexion _dato = new Conexion(Empresa);
            _dato.IniciarTransaccion();

            try
            {
                _lote.MensajeError = null;
                DataTable tablalote = _lote.ObtenerAlmLotePorId(idLote, _dato);

                if (_lote.MensajeError != null)
                {
                    _dato.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    DataRow filalote = tablalote.Rows[0];
                    Decimal costo = Convert.ToDecimal(filalote["COSTO"]);
                    DateTime fecha = System.DateTime.Today;

                    _doc.MensajeError = null;
                    DataTable tabladoc = _doc.ObtenerAlmRegDocumentoPorId(Convert.ToInt32(filalote["ID_DOCUMENTO"]), _dato);

                    if (_doc.MensajeError != null)
                    {
                        _dato.DeshacerTransaccion();
                        correcto = false;
                    }
                    else
                    {
                        DataRow filaDoc = tabladoc.Rows[0];
                        idProveedor = Convert.ToInt32(filaDoc["ID_PROVEEDOR"]);

                        idDoc = _doc.AdicionarAlmDocumentos(0, 0, "GARANTIA", "", "", fecha, new DateTime(), idbodega, 0, 0, "ELABORADO", motivo, _dato, idProveedor, null);

                        if (idDoc <= 0)
                        {
                            _dato.DeshacerTransaccion();
                            MensajeError += _doc.MensajeError;
                            correcto = false;
                        }
                        else
                        {
                            idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, idbodega, Cantidad, costo, fecha, "SALIDA", _dato, idLote, talla, 0, 0, null, filalote["REEMBOLSO"].ToString());
                            if (idInventario <= 0)
                            {
                                _dato.DeshacerTransaccion();
                                idDoc = 0;
                                correcto = false;
                            }
                            else
                            {
                                Int32 salidas = Convert.ToInt32(filalote["SALIDAS"]);
                                Int32 entradas = Convert.ToInt32(filalote["ENTRADAS"]);

                                salidas = salidas + Cantidad;

                                String ACTIVO = "S";
                                if (entradas <= salidas)
                                {
                                    ACTIVO = "N";
                                }

                                if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(filalote["ID_DOCUMENTO"]), idProducto, Convert.ToInt32(idbodega), fecha, entradas, salidas, costo, filalote["TALLA"].ToString(), ACTIVO, _dato) == false)
                                {
                                    _dato.DeshacerTransaccion();
                                    correcto = false;
                                    MensajeError = _lote.MensajeError;
                                }
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    _dato.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                _dato.DeshacerTransaccion();
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                _dato.Desconectar();
            }

            if (correcto == true)
            {
                return idDoc;
            }
            else
            {
                return 0;
            }
        }

        public Decimal guardarDevolucion(List<int> ID_LOTE,
            int ID_BODEGA,
            int ID_EMPLEADO,
            List<int> ID_DOCUMENTO,
            List<int> ID_INVENTARIO,
            List<int> ID_PRODUCTO,
            List<int> cantidad,
            List<String> Estado,
            List<String> motivo,
            List<Decimal> ID_DETALLE_ENTREGAS,
            List<EquipoEntrega> listaEquiposDevueltos)
        {
            Decimal idDocumento = 0;
            Decimal idInventario = 0;
            Decimal idLote = 0;

            DateTime fecha = System.DateTime.Today;
            documento _doc = new documento(Empresa, Usuario);
            Inventario _inventario = new Inventario(Empresa, Usuario);
            Conexion _dato = new Conexion(Empresa);
            lote _lote = new lote(Empresa, Usuario);
            int cuenta = 0;

            Boolean correcto = true;

            _dato.IniciarTransaccion();

            try
            {
                idDocumento = _doc.AdicionarAlmDocumentos(ID_EMPLEADO, 0, "DEVOLUCION", "", "", fecha, new DateTime(), 0, 0, 0, "", "", _dato, 0, null);

                if (idDocumento == 0)
                {
                    correcto = false;
                    MensajeError = _doc.MensajeError;
                    _dato.DeshacerTransaccion();
                }
                else
                {
                    for (int i = 0; i < ID_PRODUCTO.Count; i++)
                    {
                        _lote.MensajeError = null;

                        DataTable tablalote = _lote.ObtenerAlmLotePorId(ID_LOTE[i], _dato);

                        if (_lote.MensajeError != null)
                        {
                            correcto = false;
                            _dato.DeshacerTransaccion();
                            MensajeError = _lote.MensajeError;
                            break;
                        }
                        else
                        {
                            DataRow filalote = tablalote.Rows[0];

                            String activoLote = "S";
                            if (Estado[i] != "BUENO")
                            {
                                activoLote = "N";
                            }

                            idLote = _lote.AdicionarAlmLote(ID_DOCUMENTO[i], ID_PRODUCTO[i], Convert.ToInt32(filalote["ID_BODEGA"]), Convert.ToDateTime(DateTime.Now.ToShortDateString()), cantidad[i], 0, Convert.ToDecimal(filalote["COSTO"]), filalote["TALLA"].ToString().Trim(), activoLote, _dato, filalote["REEMBOLSO"].ToString());

                            if (idLote == 0)
                            {
                                correcto = false;
                                MensajeError = _lote.MensajeError;
                                _dato.DeshacerTransaccion();
                                break;
                            }
                            else
                            {
                                _inventario.MensajeError = null;
                                idInventario = _inventario.AdicionarAlmInventario(Convert.ToInt32(idDocumento), ID_PRODUCTO[i], Convert.ToInt32(filalote["ID_BODEGA"]), cantidad[i], Convert.ToDecimal(filalote["COSTO"]), Convert.ToDateTime(DateTime.Now.ToShortDateString()), "ENTRADA", _dato, Convert.ToInt32(idLote), filalote["TALLA"].ToString(), 0, ID_DETALLE_ENTREGAS[i], motivo[i], filalote["REEMBOLSO"].ToString());

                                if (idInventario <= 0)
                                {
                                    correcto = false;
                                    MensajeError = _inventario.MensajeError;
                                    _dato.DeshacerTransaccion();
                                    break;
                                }
                                else
                                {
                                    cuenta++;
                                }
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (EquipoEntrega equipo in listaEquiposDevueltos)
                        {
                            _lote.MensajeError = null;

                            DataTable tablalote = _lote.ObtenerAlmLotePorId(Convert.ToInt32(equipo.ID_LOTE), _dato);

                            if (_lote.MensajeError != null)
                            {
                                correcto = false;
                                _dato.DeshacerTransaccion();
                                MensajeError = _lote.MensajeError;
                                break;
                            }
                            else
                            {
                                DataRow filalote = tablalote.Rows[0];

                                String activoLote = "S";
                                if (equipo.ESTADO_DEV != "BUENO")
                                {
                                    activoLote = "N";
                                }

                                idLote = _lote.AdicionarAlmLote(Convert.ToInt32(equipo.ID_DOCUMENTO), Convert.ToInt32(equipo.ID_PRODUCTO), Convert.ToInt32(filalote["ID_BODEGA"]), Convert.ToDateTime(DateTime.Now.ToShortDateString()), 1, 0, Convert.ToDecimal(filalote["COSTO"]), filalote["TALLA"].ToString().Trim(), activoLote, _dato, filalote["REEMBOLSO"].ToString());

                                if (idLote == 0)
                                {
                                    correcto = false;
                                    MensajeError = _lote.MensajeError;
                                    _dato.DeshacerTransaccion();
                                    break;
                                }
                                else
                                {
                                    _inventario.MensajeError = null;
                                    idInventario = _inventario.AdicionarAlmInventario(Convert.ToInt32(idDocumento), Convert.ToInt32(equipo.ID_PRODUCTO), Convert.ToInt32(filalote["ID_BODEGA"]), 1, Convert.ToDecimal(filalote["COSTO"]), Convert.ToDateTime(DateTime.Now.ToShortDateString()), "ENTRADA", _dato, Convert.ToInt32(idLote), filalote["TALLA"].ToString(), Convert.ToInt32(equipo.ID_EQUIPO), equipo.ID_DETALLE_ENTREGAS, equipo.DETALLE_DEV, filalote["REEMBOLSO"].ToString());

                                    if (idInventario <= 0)
                                    {
                                        correcto = false;
                                        MensajeError = _inventario.MensajeError;
                                        _dato.DeshacerTransaccion();
                                        break;
                                    }
                                    else
                                    {
                                        Entrega _entrega = new Entrega(Empresa, Usuario);
                                        if (_entrega.ActualizarAlmEquiposEntregaDevolucion(equipo.ID_EQUIPO_ENTREGA, equipo.ID_EQUIPO, idLote, idDocumento, equipo.ESTADO_DEV, equipo.DETALLE_DEV, _dato) == false)
                                        {
                                            correcto = false;
                                            MensajeError = _entrega.MensajeError;
                                            _dato.DeshacerTransaccion();
                                            break;
                                        }
                                        else
                                        {
                                            cuenta++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        if (cuenta != (ID_PRODUCTO.Count + listaEquiposDevueltos.Count))
                        {
                            correcto = false;
                            _dato.DeshacerTransaccion();
                            MensajeError = "Ocurrio un error inesperado, el numero de productos y equipos procesados no corresponde.";
                        }
                    }
                }

                if (correcto == true)
                {
                    _dato.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                _dato.DeshacerTransaccion();
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                _dato.Desconectar();
            }

            if (correcto == true)
            {
                return idDocumento;
            }
            else
            {
                return 0;
            }
        }

        public Decimal guardarAjuste(String tipo,
            String idRegional,
            String idCiudad,
            int idEmpresa,
            int idProducto,
            String talla,
            int idLote,
            int CantidadMod,
            Decimal costo,
            String Motivo,
            Decimal idDocTemp,
            List<EquipoAjuste> listaEquiposAjuste,
            String NOMBRE_SERVICIO_COMPLEMENTARIO,
            String OBS_AUTORIZACION)
        {
            Decimal idDoc = 0;
            DateTime fecha;
            Decimal idInventario = 0;

            Boolean correcto = true;

            fecha = System.DateTime.Today;

            Conexion _dato = new Conexion(Empresa);
            _dato.IniciarTransaccion();

            try
            {
                documento _doc = new documento(Empresa, Usuario);
                idDoc = _doc.AdicionarAlmDocumentos(0, Convert.ToInt32(idDocTemp), "AJUSTE", "", "", fecha, new DateTime(), 0, 0, 0, "APROBADO", Motivo, _dato, 0, OBS_AUTORIZACION);

                if (idDoc <= 0)
                {
                    _dato.DeshacerTransaccion();
                    correcto = false;
                    MensajeError = _doc.MensajeError;
                }
                else
                {
                    bodega _bodega = new bodega(Empresa, Usuario);
                    DataTable tablaBodega = _bodega.ObtenerAlmRegBodegaPorIds(idRegional, idCiudad, idEmpresa, _dato);

                    if (_bodega.MensajeError != null)
                    {
                        _dato.DeshacerTransaccion();
                        correcto = false;
                        MensajeError = _bodega.MensajeError;
                    }
                    else
                    {
                        DataRow filaBodega = tablaBodega.Rows[0];

                        lote _lote = new lote(Empresa, Usuario);
                        DataTable tablaLote = _lote.ObtenerAlmLotePorId(idLote, _dato);

                        if (_lote.MensajeError != null)
                        {
                            _dato.DeshacerTransaccion();
                            correcto = false;
                            MensajeError = _bodega.MensajeError;
                        }
                        else
                        {

                            DataRow filalote = tablaLote.Rows[0];

                            if (tipo.Equals("COSTO"))
                            {
                                idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"]), 0, costo, fecha, "COSTO", _dato, idLote, talla, 0, 0, null, filalote["REEMBOLSO"].ToString());
                                if (idInventario <= 0)
                                {
                                    _dato.DeshacerTransaccion();
                                    correcto = false;
                                }
                                else
                                {
                                    if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(filalote["ID_DOCUMENTO"]), idProducto, Convert.ToInt32(filalote["ID_BODEGA"]), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), Convert.ToInt32(filalote["ENTRADAS"]), Convert.ToInt32(filalote["SALIDAS"]), costo, filalote["TALLA"].ToString(), filalote["ACTIVO"].ToString(), _dato) == false)
                                    {
                                        _dato.DeshacerTransaccion();
                                        correcto = false;
                                        MensajeError = _lote.MensajeError;
                                    }
                                }
                            }
                            else if (tipo.Equals("ENTRADA"))
                            {
                                int contenido = 0;
                                int entradas = 0;
                                int salidas = 0;

                                if (NOMBRE_SERVICIO_COMPLEMENTARIO != "EQUIPOS")
                                {
                                    entradas = Convert.ToInt32(filalote["ENTRADAS"]) + CantidadMod;
                                    salidas = Convert.ToInt32(filalote["SALIDAS"]);
                                    
                                    contenido = entradas - salidas;

                                    String activo = "S";
                                    if (contenido <= 0)
                                    {
                                        activo = "N";
                                    }

                                    idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"]), CantidadMod, Convert.ToDecimal(filalote["COSTO"]), fecha, "ENTRADA", _dato, idLote, filalote["TALLA"].ToString(), 0, 0, null, filalote["REEMBOLSO"].ToString());

                                    if (idInventario <= 0)
                                    {
                                        _dato.DeshacerTransaccion();
                                        correcto = false;
                                    }
                                    else
                                    {
                                        if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(filalote["ID_DOCUMENTO"]), idProducto, Convert.ToInt32(filalote["ID_BODEGA"]), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), entradas, salidas, Convert.ToDecimal(filalote["COSTO"]), filalote["TALLA"].ToString(), activo, _dato) == false)
                                        {
                                            _dato.DeshacerTransaccion();
                                            correcto = false;
                                            MensajeError += _lote.MensajeError;
                                        }
                                    }
                                }
                                else
                                {
                                    producto _prod = new producto(Empresa, Usuario);

                                    entradas = Convert.ToInt32(filalote["ENTRADAS"]);
                                    salidas = Convert.ToInt32(filalote["SALIDAS"]);

                                    foreach (EquipoAjuste equipo in listaEquiposAjuste)
                                    {
                                        Decimal ID_NUEVO_EQUIPO = _prod.AdicionarAlmEquipo(Convert.ToInt32(equipo.ID_DOCUMENTO), equipo.MARCA, equipo.MODELO, equipo.SERIE, equipo.IMEI, equipo.NUMERO_CELULAR, "S", fecha, Convert.ToDecimal(idLote), _dato);

                                        if (ID_NUEVO_EQUIPO <= 0)
                                        {
                                            _dato.DeshacerTransaccion();
                                            correcto = false;
                                            MensajeError += _lote.MensajeError;
                                            break;
                                        }
                                        else
                                        {
                                            entradas += 1;
                                            contenido = entradas - salidas;

                                            String activo = "S";
                                            if (contenido <= 0)
                                            {
                                                activo = "N";
                                            }

                                            idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"]), 1, Convert.ToDecimal(filalote["COSTO"]), fecha, "ENTRADA", _dato, idLote, filalote["TALLA"].ToString(), Convert.ToInt32(ID_NUEVO_EQUIPO), 0, null, filalote["REEMBOLSO"].ToString());

                                            if (idInventario <= 0)
                                            {
                                                _dato.DeshacerTransaccion();
                                                correcto = false;
                                                break;
                                            }
                                            else
                                            {
                                                if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(filalote["ID_DOCUMENTO"]), idProducto, Convert.ToInt32(filalote["ID_BODEGA"]), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), entradas, salidas, Convert.ToDecimal(filalote["COSTO"]), filalote["TALLA"].ToString(), activo, _dato) == false)
                                                {
                                                    _dato.DeshacerTransaccion();
                                                    correcto = false;
                                                    MensajeError += _lote.MensajeError;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int contenido = 0;
                                int entradas = 0; 
                                int salidas = 0; 

                                if (NOMBRE_SERVICIO_COMPLEMENTARIO != "EQUIPOS")
                                {
                                    entradas = Convert.ToInt32(filalote["ENTRADAS"]);
                                    salidas = Convert.ToInt32(filalote["SALIDAS"]) + CantidadMod;

                                    contenido = entradas - salidas;

                                    String activo = "S";
                                    if (contenido <= 0)
                                    {
                                        activo = "N";
                                    }

                                    idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"]), CantidadMod, Convert.ToDecimal(filalote["COSTO"]), fecha, "SALIDA", _dato, idLote, filalote["TALLA"].ToString(), 0, 0, null, filalote["REEMBOLSO"].ToString());

                                    if (idInventario <= 0)
                                    {
                                        _dato.DeshacerTransaccion();
                                        correcto = false;
                                    }
                                    else
                                    {
                                        if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(filalote["ID_DOCUMENTO"]), idProducto, Convert.ToInt32(filalote["ID_BODEGA"]), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), entradas, salidas, Convert.ToDecimal(filalote["COSTO"]), filalote["TALLA"].ToString(), activo, _dato) == false)
                                        {
                                            _dato.DeshacerTransaccion();
                                            correcto = false;
                                            MensajeError += _lote.MensajeError;
                                        }
                                    }
                                }
                                else
                                {
                                    producto _prod = new producto(Empresa, Usuario);

                                    entradas = Convert.ToInt32(filalote["ENTRADAS"]);
                                    salidas = Convert.ToInt32(filalote["SALIDAS"]);

                                    foreach (EquipoAjuste equipo in listaEquiposAjuste)
                                    {
                                        if (_prod.ActualizarAlmEquipoIdLoteActualIDocumentoActualParaSalida(equipo.ID_EQUIPO, "N", 0, 0, _dato) == false)
                                        {
                                            _dato.DeshacerTransaccion();
                                            correcto = false;
                                            MensajeError += _prod.MensajeError;
                                            break;
                                        }
                                        else
                                        {
                                            salidas += 1;

                                            contenido = entradas - salidas;

                                            String activo = "S";
                                            if (contenido <= 0)
                                            {
                                                activo = "N";
                                            }

                                            idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"]), 1, Convert.ToDecimal(filalote["COSTO"]), fecha, "SALIDA", _dato, idLote, filalote["TALLA"].ToString(), Convert.ToInt32(equipo.ID_EQUIPO), 0, null, filalote["REEMBOLSO"].ToString());

                                            if (idInventario <= 0)
                                            {
                                                _dato.DeshacerTransaccion();
                                                correcto = false;
                                                break;
                                            }
                                            else
                                            {
                                                if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(filalote["ID_DOCUMENTO"]), idProducto, Convert.ToInt32(filalote["ID_BODEGA"]), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), entradas, salidas, Convert.ToDecimal(filalote["COSTO"]), filalote["TALLA"].ToString(), activo, _dato) == false)
                                                {
                                                    _dato.DeshacerTransaccion();
                                                    correcto = false;
                                                    MensajeError += _lote.MensajeError;
                                                    break;
                                                }
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
                    documento_temp _docTemp = new documento_temp(Empresa, Usuario);
                    if (_docTemp.ActualizarAlmDocumentoTemp(Convert.ToInt32(idDocTemp), Convert.ToDateTime(DateTime.Now.ToShortDateString()), "APROBADO", _dato, OBS_AUTORIZACION) == false)
                    {
                        _dato.DeshacerTransaccion();
                        correcto = false;
                        MensajeError = _docTemp.MensajeError;
                    }
                }

                if (correcto == true)
                {
                    _dato.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                _dato.DeshacerTransaccion();
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                _dato.Desconectar();
            }

            if (correcto == true)
            {
                return idDoc;
            }
            else
            {
                return 0;
            }
        }

        public Decimal guardarAjusteEquipo(String tipo, String idRegional, String idCiudad, int idEmpresa, int idProducto, int idEquipo, String talla, int idLote, int CantidadMod, Decimal costo, String Motivo)
        {
            Decimal idDoc = 0;
            DateTime fecha;
            Decimal idInventario = 0;

            fecha = System.DateTime.Today;
            Conexion _dato = new Conexion(Empresa);
            _dato.IniciarTransaccion();

            documento _doc = new documento(Empresa, Usuario);
            idDoc = _doc.AdicionarAlmDocumentos(0, 0, "AJUSTE", "", "", fecha, new DateTime(), 0, 0, 0, "", Motivo, _dato, 0, null);
            if (idDoc != 0)
            {
                bodega _bodega = new bodega(Empresa, Usuario);
                DataTable tablaBodega = _bodega.ObtenerAlmRegBodegaPorIds(idRegional, idCiudad, idEmpresa);
                DataRow filaBodega = tablaBodega.Rows[0];

                lote _lote = new lote(Empresa, Usuario);
                DataTable tablaLote = _lote.ObtenerAlmLotePorId(idLote, _dato);
                DataRow filalote = tablaLote.Rows[0];
                if (tipo.Equals("COSTO"))
                {
                    idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), 0, costo, fecha, "MODIFICACION", _dato, idLote, talla, idEquipo, 0, null, filalote["REEMBOLSO"].ToString());
                    if (idInventario == 0)
                    {
                        _dato.DeshacerTransaccion();

                    }
                    else
                    {
                        if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filalote["ID_BODEGA"].ToString()), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), Convert.ToInt32(filalote["ENTRADAS"]), Convert.ToInt32(filalote["SALIDAS"]), costo, talla, "S", _dato))
                        {
                            _dato.AceptarTransaccion();
                        }
                        else
                        {
                            _dato.DeshacerTransaccion();
                            MensajeError += _lote.MensajeError;
                        }
                    }
                }
                else if (tipo.Equals("ENTRADA"))
                {
                    int contenido;
                    contenido = Convert.ToInt32(filalote["ENTRADAS"].ToString()) - Convert.ToInt32(filalote["SALIDAS"].ToString());
                    contenido = CantidadMod - contenido;

                    idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), contenido, 0, fecha, "ENTRADA", _dato, idLote, talla, idEquipo, 0, null, filalote["REEMBOLSO"].ToString());

                    if (idInventario == 0)
                    {
                        _dato.DeshacerTransaccion();

                    }
                    else
                    {
                        if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filalote["ID_BODEGA"].ToString()), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), Convert.ToInt32(filalote["ENTRADAS"].ToString()) + contenido, Convert.ToInt32(filalote["SALIDAS"]), Convert.ToDecimal(filalote["COSTO"].ToString()), talla, "S", _dato))
                        {
                            _dato.AceptarTransaccion();
                        }
                        else
                        {
                            _dato.DeshacerTransaccion();
                            idDoc = 0;
                            MensajeError += _lote.MensajeError;
                        }
                    }
                }
                else
                {
                    int contenido;
                    contenido = Convert.ToInt32(filalote["ENTRADAS"].ToString()) - Convert.ToInt32(filalote["SALIDAS"].ToString());
                    contenido = contenido - CantidadMod;

                    idInventario = AdicionarAlmInventario(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), contenido, 0, fecha, "SALIDA", _dato, idLote, talla, idEquipo, 0, null, filalote["REEMBOLSO"].ToString());
                    if (idInventario == 0)
                    {
                        _dato.DeshacerTransaccion();
                        idDoc = 0;

                    }
                    else
                    {
                        if (_lote.ActualizarAlmLote(idLote, Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filalote["ID_BODEGA"].ToString()), Convert.ToDateTime(filalote["FECHA_REGISTRO"]), Convert.ToInt32(filalote["ENTRADAS"].ToString()), Convert.ToInt32(filalote["SALIDAS"].ToString()) + contenido, Convert.ToDecimal(filalote["COSTO"]), talla, "S", _dato))
                        {
                            _dato.AceptarTransaccion();
                        }
                        else
                        {
                            _dato.DeshacerTransaccion();
                            idDoc = 0;
                            MensajeError += _lote.MensajeError;
                        }
                    }
                }
            }
            else
            {
                _dato.DeshacerTransaccion();
                MensajeError = _doc.MensajeError;
                idDoc = 0;
            }

            return idDoc;
        }

        public DataTable ObtenerAlmInventarioPorIdDocumentos(int ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_INVENTARIO_OBTENER_PRODUCTOS_POR_ID_DOC ";

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
                    _auditoria.Adicionar(Usuario, tabla.ALM_INVENTARIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerInformacionProductosParaRecepcionDeTrasladosPorIdDoc(int ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_inventario_obtener_info_para_recepcion_traslado_por_id_doc ";

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO;
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

        public DataTable ObtenerAlmInventarioPorPeriodo(DateTime FECHA_INICIO, DateTime FECHA_FIN)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "USP_ALM_INVENTARIO_OBTENER_POR_PERIODO ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "', ";
            informacion += "FECHA_INICIO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN) + "' ";
            informacion += "FECHA_FIN = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN) + "' ";

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
                    _auditoria.Adicionar(Usuario, tabla.ALM_INVENTARIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmInventarioPorPeriodo(DateTime FECHA_INICIO,
            DateTime FECHA_FIN,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "USP_ALM_INVENTARIO_OBTENER_POR_PERIODO ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN) + "' ";

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
        #endregion metodos
    }
}
