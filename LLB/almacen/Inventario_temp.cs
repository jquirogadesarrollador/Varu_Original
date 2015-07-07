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
    public class Inventario_temp
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
        public Inventario_temp(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarAlmInventarioTemp(int ID_DOCUMENTO, int ID_PRODUCTO, int ID_BODEGA, int ID_LOTE, int ID_EQUIPO, String TALLA,
            int CANTIDAD, Decimal COSTO, DateTime FECHA, String MOVIMIENTO, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_INVENTARIO_TEMP_ADICIONAR ";

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
            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO= '" + ID_EQUIPO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EQUIPO= '0', ";
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

        public Boolean ActualizarAlmInventarioTemp(int ID_INVENTARIO, int ID_DOCUMENTO, int ID_PRODUCTO, int ID_BODEGA, int ID_LOTE, int ID_EQUIPO, String TALLA,
            int CANTIDAD, Decimal COSTO, DateTime FECHA, String MOVIMIENTO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_INVENTARIO_TEMP_ACTUALIZAR ";

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
            if (ID_EQUIPO != 0)
            {
                sql += ID_EQUIPO + ", ";
                informacion += "ID_EQUIPO= '" + ID_EQUIPO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EQUIPO= '0', ";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_INVENTARIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmInventarioTempPorId(int ID_INVENTARIO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_INVENTARIO_TEMP_OBTENER_POR_ID ";

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

        public DataTable ObtenerAlmInventarioTempPorIdDocumentos(int ID_DOCUMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_INVENTARIO_TEMP_OBTENER_POR_ID_DOCUMENTO ";

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
            List<EquipoAjuste> listaEquiposAjuste,
            String NOMBRE_SERVICIO_COMPLEMENTARIO)
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
                documento_temp _doc = new documento_temp(Empresa, Usuario);
                idDoc = _doc.AdicionarAlmDocumentosTemp(0, "AJUSTE", "", fecha, new DateTime(), 0, "POR APROBAR", Motivo, _dato);

                if (idDoc <= 0)
                {
                    correcto = false;
                    MensajeError = _doc.MensajeError;
                    _dato.DeshacerTransaccion();
                }
                else
                {
                    bodega _bodega = new bodega(Empresa, Usuario);
                    _bodega.MensajeError = null;
                    DataTable tablaBodega = _bodega.ObtenerAlmRegBodegaPorIds(idRegional, idCiudad, idEmpresa, _dato);

                    if (_bodega.MensajeError != null)
                    {
                        correcto = false;
                        _dato.DeshacerTransaccion();
                        MensajeError = _bodega.MensajeError;
                    }
                    else
                    {
                        DataRow filaBodega = tablaBodega.Rows[0];

                        lote _lote = new lote(Empresa, Usuario);
                        _lote.MensajeError = null;
                        DataTable tablaLote = _lote.ObtenerAlmLotePorId(idLote, _dato);

                        if (_lote.MensajeError != null)
                        {
                            correcto = false;
                            _dato.DeshacerTransaccion();
                            MensajeError = _bodega.MensajeError;
                        }
                        else
                        {
                            DataRow filalote = tablaLote.Rows[0];
                            if (tipo.Equals("COSTO"))
                            {
                                idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, 0, talla, CantidadMod, costo, fecha, "COSTO", _dato);

                                if (idInventario == 0)
                                {
                                    correcto = false;
                                    _dato.DeshacerTransaccion();
                                    MensajeError = _bodega.MensajeError;
                                }
                            }
                            else if (tipo.Equals("ENTRADA"))
                            {
                                if (NOMBRE_SERVICIO_COMPLEMENTARIO != "EQUIPOS")
                                {
                                    idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, 0, talla, CantidadMod, Convert.ToDecimal(filalote["COSTO"]), fecha, "ENTRADA", _dato);

                                    if (idInventario == 0)
                                    {
                                        correcto = false;
                                        _dato.DeshacerTransaccion();
                                        MensajeError = _bodega.MensajeError;
                                    }
                                }
                                else
                                {
                                    foreach (EquipoAjuste equipo in listaEquiposAjuste)
                                    {
                                        EquipoEntradaTemp _equipoTemp = new EquipoEntradaTemp(Empresa, Usuario);
                                        Decimal ID_EQUIPO_TEMP = _equipoTemp.AdicionarAlmEquipoEntradaTemp(equipo.ID_DOCUMENTO, equipo.MARCA, equipo.MODELO, equipo.SERIE, equipo.IMEI, equipo.NUMERO_CELULAR, fecha, equipo.ID_LOTE, _dato);

                                        if (ID_EQUIPO_TEMP <= 0)
                                        {
                                            correcto = false;
                                            _dato.DeshacerTransaccion();
                                            MensajeError = _bodega.MensajeError;
                                            break;
                                        }
                                        else
                                        {
                                            idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), Convert.ToInt32(equipo.ID_PRODUCTO), Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, Convert.ToInt32(ID_EQUIPO_TEMP), talla, 1, Convert.ToDecimal(filalote["COSTO"]), fecha, "ENTRADA", _dato);

                                            if (idInventario == 0)
                                            {
                                                correcto = false;
                                                _dato.DeshacerTransaccion();
                                                MensajeError = _bodega.MensajeError;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (NOMBRE_SERVICIO_COMPLEMENTARIO != "EQUIPOS")
                                {
                                    idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, 0, talla, CantidadMod, Convert.ToDecimal(filalote["COSTO"]), fecha, "SALIDA", _dato);
                                    if (idInventario == 0)
                                    {
                                        correcto = false;
                                        _dato.DeshacerTransaccion();
                                        MensajeError = _bodega.MensajeError;
                                    }
                                }
                                else
                                {
                                    foreach (EquipoAjuste equipo in listaEquiposAjuste)
                                    {
                                        idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), Convert.ToInt32(equipo.ID_PRODUCTO), Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, Convert.ToInt32(equipo.ID_EQUIPO), talla, 1, Convert.ToDecimal(filalote["COSTO"]), fecha, "SALIDA", _dato);
                                        if (idInventario == 0)
                                        {
                                            correcto = false;
                                            _dato.DeshacerTransaccion();
                                            MensajeError = _bodega.MensajeError;
                                            break;
                                        }
                                    }
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
                correcto = false;
                MensajeError = ex.Message;
                _dato.DeshacerTransaccion();
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

            documento_temp _doc = new documento_temp(Empresa, Usuario);
            idDoc = _doc.AdicionarAlmDocumentosTemp(0, "AJUSTE", "", fecha, Convert.ToDateTime("01/01/1900"), 0, "POR APROBAR", Motivo, _dato);
            if (idDoc != 0)
            {
                bodega _bodega = new bodega(Empresa, Usuario);
                DataTable tablaBodega = _bodega.ObtenerAlmRegBodegaPorIds(idRegional, idCiudad, idEmpresa);
                DataRow filaBodega = tablaBodega.Rows[0];

                lote _lote = new lote(Empresa, Usuario);
                DataTable tablaLote = _lote.ObtenerAlmLotePorId(idLote);
                DataRow filalote = tablaLote.Rows[0];
                if (tipo.Equals("COSTO"))
                {
                    idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, idEquipo, talla, CantidadMod, costo, fecha, "MODIFICACION", _dato);
                    if (idInventario == 0)
                    {
                        _dato.DeshacerTransaccion();
                        idDoc = 0;
                    }
                    else
                    {
                        _dato.AceptarTransaccion();
                    }

                }
                else if (tipo.Equals("ENTRADA"))
                {
                    int contenido;
                    contenido = Convert.ToInt32(filalote["ENTRADAS"].ToString()) - Convert.ToInt32(filalote["SALIDAS"].ToString());
                    contenido = CantidadMod - contenido;

                    idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, idEquipo, talla, contenido, 0, fecha, "ENTRADA", _dato);

                    if (idInventario == 0)
                    {
                        _dato.DeshacerTransaccion();
                        idDoc = 0;
                    }
                    else
                    {

                        _dato.AceptarTransaccion();
                    }
                }
                else
                {
                    int contenido;
                    contenido = Convert.ToInt32(filalote["ENTRADAS"].ToString()) - Convert.ToInt32(filalote["SALIDAS"].ToString());
                    contenido = contenido - CantidadMod;

                    idInventario = AdicionarAlmInventarioTemp(Convert.ToInt32(idDoc), idProducto, Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()), idLote, idEquipo, talla, contenido, 0, fecha, "SALIDA", _dato);
                    if (idInventario == 0)
                    {
                        _dato.DeshacerTransaccion();
                        idDoc = 0;

                    }
                    else
                    {
                        _dato.AceptarTransaccion();
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
        #endregion metodos
    }
}
