using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.almacen;
using Brainsbits.LLB.comercial;

namespace Brainsbits.LLB.contratacion
{
    public class EntregaServicioComplementario
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
        public EntregaServicioComplementario(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarConRegEntregasServiciosComplementarios(int ID_ASIGNACION_SC, int ID_PRODUCTO, DateTime FECHA, String TALLA, int CANTIDAD_ENTREGADA,
            int CANTIDAD_A_ENTREGAR, String ESTADO, Decimal COSTO, Decimal AIU, Decimal IVA, Decimal VALOR, String FACTURADO, String NUMERO_FACTURA, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS_ADICIONAR ";

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
            if (CANTIDAD_A_ENTREGAR != 0)
            {
                sql += CANTIDAD_A_ENTREGAR + ", ";
                informacion += "CANTIDAD_A_ENTREGAR = '" + CANTIDAD_A_ENTREGAR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_A_ENTREGAR no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            if (AIU != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AIU) + ", ";
                informacion += "AIU = '" + AIU.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AIU) + ", ";
                informacion += "AIU = '" + AIU.ToString() + "', ";
            }
            if (IVA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA) + ", ";
                informacion += "IVA = '" + IVA.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA) + ", ";
                informacion += "IVA = '" + IVA.ToString() + "', ";
            }
            if (VALOR != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ", ";
                informacion += "VALOR = '" + VALOR.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ", ";
                informacion += "VALOR = '" + VALOR.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(FACTURADO)))
            {
                sql += "'" + FACTURADO + "', ";
                informacion += "FACTURADO= '" + FACTURADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FACTURADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NUMERO_FACTURA)))
            {
                sql += "'" + NUMERO_FACTURA + "', ";
                informacion += "NUMERO_FACTURA= '" + NUMERO_FACTURA.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUMERO_FACTURA= '" + NUMERO_FACTURA.ToString() + "', ";
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarConRegEntregasServiciosComplementarios(int ID_ENTREGAS, int ID_ASIGNACION_SC, int ID_PRODUCTO, DateTime FECHA, String TALLA, int CANTIDAD_ENTREGADA,
            int CANTIDAD_A_ENTREGAR, String ESTADO, Decimal COSTO, Decimal AIU, Decimal IVA, Decimal VALOR, String FACTURADO, String NUMERO_FACTURA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS_ACTUALIZAR ";

            #region validaciones
            if (ID_ENTREGAS != 0)
            {
                sql += ID_ENTREGAS + ", ";
                informacion += "ID_ENTREGAS = '" + ID_ENTREGAS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ENTREGAS no puede ser nulo\n";
                ejecutar = false;
            }
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
            if (CANTIDAD_A_ENTREGAR != 0)
            {
                sql += CANTIDAD_A_ENTREGAR + ", ";
                informacion += "CANTIDAD_A_ENTREGAR = '" + CANTIDAD_A_ENTREGAR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_A_ENTREGAR no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO= '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (COSTO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(COSTO) + ", ";
                informacion += "COSTO = '" + COSTO.ToString() + "', ";
            }
            if (AIU != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AIU) + ", ";
                informacion += "AIU = '" + AIU.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AIU) + ", ";
                informacion += "AIU = '" + AIU.ToString() + "', ";
            }
            if (IVA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA) + ", ";
                informacion += "IVA = '" + IVA.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA) + ", ";
                informacion += "IVA = '" + IVA.ToString() + "', ";
            }
            if (VALOR != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ", ";
                informacion += "VALOR = '" + VALOR.ToString() + "', ";
            }
            else
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ", ";
                informacion += "VALOR = '" + VALOR.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(FACTURADO)))
            {
                sql += "'" + FACTURADO + "', ";
                informacion += "FACTURADO= '" + FACTURADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FACTURADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NUMERO_FACTURA)))
            {
                sql += "'" + NUMERO_FACTURA + "', ";
                informacion += "NUMERO_FACTURA= '" + NUMERO_FACTURA.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUMERO_FACTURA= '" + NUMERO_FACTURA.ToString() + "', ";
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegEntregasServiciosComplementariosPorID(int ID_ENTREGAS)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS_OBTENER_POR_ID ";

            if (ID_ENTREGAS != 0)
            {
                sql += ID_ENTREGAS;
                informacion += "ID_ENTREGAS = '" + ID_ENTREGAS.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_ENTREGAS no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public String adicionarEntregas(int idempleado, DateTime fecha, int idBodega, DataTable tablaConfigurados, DataTable tablalotes, String[] datos, String faltantes)
        {
            int NumerosEntregas = 0;
            Decimal idDocumento = 0;
            Decimal idOrdenEntrega = 0;
            AsignacionServiciosComplementarios _OrdenEntrega = new AsignacionServiciosComplementarios(Empresa, Usuario);
            Brainsbits.LLB.almacen.documento _documento = new almacen.documento(Empresa, Usuario);
            Inventario _inventario = new Inventario(Empresa, Usuario);

            Conexion _dato = new LDA.Conexion(Empresa);
            _dato.IniciarTransaccion();

            #region validaciones
            foreach (DataRow filaConf in tablaConfigurados.Rows)
            {
                foreach (DataRow filaLote in tablalotes.Rows)
                {
                    if (filaConf["ID_PRODUCTO"].Equals(filaLote["ID_PRODUCTO"]) & filaConf["TALLA"].Equals(filaLote["TALLA"]) &
                        Convert.ToInt32(filaConf["CANTIDAD_A_ENTREGAR"]) > Convert.ToInt32(filaLote["CANTIDAD_CONFIGURADA"]))
                    {
                        MensajeError += "La cantidad que se desea entregar es superior a la cantidad que se debe entregar. Valide por favor";
                    }
                    else
                    {
                        idDocumento = _documento.AdicionarAlmDocumentos(idempleado, 0, "ENTREGA", "", "", fecha, new DateTime(), 0, 0, 0, "", "", _dato, 0, null);

                        if (idDocumento == 0)
                        {
                            _dato.DeshacerTransaccion();
                            MensajeError += "ADVERTENCIA: No fue posible crear el documento, por la siguiente razón \n" + _documento.MensajeError + "  \ncomuniquese con el administrador";
                        }
                        else
                        {

                            idOrdenEntrega = _OrdenEntrega.AdicionarAsignacionServiciosComplementarios(Convert.ToInt32(idDocumento), idempleado, fecha, "", _dato);
                            if (idOrdenEntrega == 0)
                            {
                                _dato.DeshacerTransaccion();
                                MensajeError += "ADVERTENCIA: No fue posible crear el documento de Orden de Entrega, por la siguiente razón \n" + _OrdenEntrega.MensajeError + "  \ncomuniquese con el administrador";
                            }
                            else
                            {
                                if (filaConf["ID_PRODUCTO"].Equals(filaLote["ID_PRODUCTO"]) & filaConf["TALLA"].Equals(filaLote["TALLA"]) &
                                    Convert.ToInt32(filaConf["CANTIDAD_A_ENTREGAR"]) <= Convert.ToInt32(filaLote["CONTENIDO_LOTE"]))
                                {
                                    int idproducto = Convert.ToInt32(filaConf["ID_PRODUCTO"]);
                                    int cantidad = Convert.ToInt32(filaConf["CANTIDAD_A_ENTREGAR"]);
                                    int idlote = Convert.ToInt32(filaLote["ID_LOTE"]);
                                    lote _lote = new lote(Empresa, Usuario);
                                    DataTable tablalo = _lote.ObtenerAlmLotePorId(idlote, _dato);
                                    DataRow filalo = tablalo.Rows[0];
                                    Decimal costo = Convert.ToInt32(filalo["COSTO"]);
                                    String talla = filaConf["TALLA"].ToString();
                                    Decimal idinventario = 0;
                                    Decimal idEntrega = 0;

                                    idinventario = _inventario.AdicionarAlmInventario(Convert.ToInt32(idDocumento), idproducto, idBodega, cantidad, costo, fecha, "SALIDA", _dato, idlote, talla, 0, 0, null, filalo["REEMBOLSO"].ToString());

                                    if (idinventario == 0)
                                    {
                                        _dato.DeshacerTransaccion();
                                        MensajeError += "El registro de inventario no fue posible crearlo por: " + _inventario.MensajeError + " Valide con el Administrador";
                                    }
                                    else
                                    {

                                        idEntrega = AdicionarConRegEntregasServiciosComplementarios(Convert.ToInt32(idOrdenEntrega), idproducto, fecha, talla, cantidad, cantidad, "COMPLETA", costo, 0, 0, 0, "N", "", _dato);
                                        if (idEntrega == 0)
                                        {
                                            _dato.DeshacerTransaccion();
                                            MensajeError += "El registro de inventario no fue posible crearlo por: " + MensajeError + " Valide con el Administrador";
                                        }
                                        else
                                        {
                                            NumerosEntregas++;
                                            break;
                                        }

                                    }
                                }
                                else
                                {
                                }
                            }
                        }
                    }
                }
            }
            if (NumerosEntregas == tablaConfigurados.Rows.Count)
            {
                if (faltantes.Equals(""))
                {
                    if (_OrdenEntrega.ActualizarAsignacionServiciosComplementarios(Convert.ToInt32(idOrdenEntrega), Convert.ToInt32(idDocumento), idempleado, fecha, "COMPLETO", _dato))
                    {
                        if (_documento.ActualizarAlmDocumento(Convert.ToInt32(idDocumento), idempleado, 0, "ENTREGA", "", "" + idOrdenEntrega.ToString(), fecha, Convert.ToDateTime("01/01/1900"), 0, 0, 0, "COMPLETO", "", 0, _dato))
                        {
                            _dato.AceptarTransaccion();
                        }
                        else
                        {
                            _dato.DeshacerTransaccion();
                            MensajeError += "No fue posible actualizar el documento por: " + _documento.MensajeError;
                        }
                    }
                    else
                    {
                        _dato.DeshacerTransaccion();
                        MensajeError += "No fue posible actualizar la orden de entrega por: " + _OrdenEntrega.MensajeError;
                    }
                }
                else
                {
                    if (_OrdenEntrega.ActualizarAsignacionServiciosComplementarios(Convert.ToInt32(idOrdenEntrega), Convert.ToInt32(idDocumento), idempleado, fecha, "INCOMPLETO", _dato))
                    {
                        if (_documento.ActualizarAlmDocumento(Convert.ToInt32(idDocumento), idempleado, 0, "ENTREGA", "", "" + idOrdenEntrega.ToString(), fecha, Convert.ToDateTime("01/01/1900"), 0, 0, 0, "INCOMPLETO", faltantes, 0, _dato))
                        {
                            _dato.AceptarTransaccion();
                        }
                        else
                        {
                            _dato.DeshacerTransaccion();
                            MensajeError += "No fue posible actualizar el documento por: " + _documento.MensajeError;
                        }
                    }
                    else
                    {
                        _dato.DeshacerTransaccion();
                        MensajeError += "No fue posible actualizar la orden de entrega por: " + _OrdenEntrega.MensajeError;
                    }
                }
            }
            else
            {
                NumerosEntregas = 0;
            }
            #endregion

            if (NumerosEntregas <= 0) return "";
            return idOrdenEntrega.ToString();
        }

        public String adicionarEntregasTemp(int idempleado, DateTime fecha, int idBodega, DataTable tablaConfigurados, String[] datos)
        {
            int NumerosEntregas = 0;
            Decimal idDocumento = 0;
            Decimal idOrdenEntrega = 0;

            Brainsbits.LLB.almacen.documento_temp _documento = new almacen.documento_temp(Empresa, Usuario);
            Inventario_temp _inventario = new Inventario_temp(Empresa, Usuario);
            Conexion _dato = new LDA.Conexion(Empresa);
            _dato.IniciarTransaccion();

            #region validaciones
            idDocumento = _documento.AdicionarAlmDocumentosTemp(idempleado, "ENTREGA", "", fecha, Convert.ToDateTime("01/01/1900"), 0, "CONFIGURADO", "", _dato);

            if (idDocumento == 0)
            {
                _dato.DeshacerTransaccion();
                MensajeError += "ADVERTENCIA: No fue posible crear el documento, por la siguiente razón \n" + _documento.MensajeError + "  \ncomuniquese con el administrador";
            }
            else
            {
                foreach (DataRow filaConf in tablaConfigurados.Rows)
                {
                    int idproducto = Convert.ToInt32(filaConf["ID_PRODUCTO"]);
                    int cantidad = Convert.ToInt32(filaConf["CANTIDAD"]);
                    int idlote = Convert.ToInt32(filaConf["ID_LOTE"]);
                    lote _lote = new lote(Empresa, Usuario);
                    DataTable tablalo = _lote.ObtenerAlmLotePorId(idlote);
                    DataRow filalo = tablalo.Rows[0];
                    Decimal costo = Convert.ToInt32(filalo["COSTO"]);
                    String talla = filaConf["TALLA"].ToString();
                    Decimal idinventario = 0;

                    idinventario = _inventario.AdicionarAlmInventarioTemp(Convert.ToInt32(idDocumento), idproducto, idBodega, idlote, 0, talla, cantidad, costo, fecha, "SALIDA", _dato);

                    if (idinventario == 0)
                    {
                        _dato.DeshacerTransaccion();
                        MensajeError += "El registro de inventario no fue posible crearlo por: " + _inventario.MensajeError + " Valide con el Administrador";
                    }
                    else
                    {
                        NumerosEntregas++;
                    }
                }
            }
            if (NumerosEntregas == tablaConfigurados.Rows.Count)
            {
                _dato.AceptarTransaccion();
            }
            else
            {
                _dato.DeshacerTransaccion();
                NumerosEntregas = 0;
            }
            #endregion

            if (NumerosEntregas <= 0) return "";
            return idDocumento.ToString();
        }

        public DataTable obtenerEntregasPorIdentificacion(String NUM_DOC_IDENTIFICACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS_OBTENER_ENTREGAS_POR_NUM_DOC_IDENTIDAD ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIFICACION)))
            {
                sql += "'" + NUM_DOC_IDENTIFICACION + "' ";
                informacion += "NUM_DOC_IDENTIFICACION= '" + NUM_DOC_IDENTIFICACION.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIFICACION no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable obtenerEntregasConfiguradasPorIdentificacion(String NUM_DOC_IDENTIFICACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS_OBTENER_CONFIGURADAS_POR_NUM_DOC_IDENTIDAD ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIFICACION)))
            {
                sql += "'" + NUM_DOC_IDENTIFICACION + "' ";
                informacion += "NUM_DOC_IDENTIFICACION= '" + NUM_DOC_IDENTIFICACION.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIFICACION no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable obtenerEntregasProximasPorIdentificacion(String NUM_DOC_IDENTIFICACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS_OBTENER_PROXIMAS_POR_NUM_DOC_IDENTIDAD ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIFICACION)))
            {
                sql += "'" + NUM_DOC_IDENTIFICACION + "' ";
                informacion += "NUM_DOC_IDENTIFICACION= '" + NUM_DOC_IDENTIFICACION.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIFICACION no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable obtenerEntregasFaltantesPorIdentificacion(String NUM_DOC_IDENTIFICACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS_OBTENER_FALTANTES_POR_NUM_DOC_IDENTIDAD ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIFICACION)))
            {
                sql += "'" + NUM_DOC_IDENTIFICACION + "' ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIFICACION no puede ser nulo\n";
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
    }
}
