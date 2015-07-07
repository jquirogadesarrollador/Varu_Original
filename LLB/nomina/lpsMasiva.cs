using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.nomina
{
    public class lpsMasiva
    {
        #region variables
        enum Estados
        {
            LIQUIDADO = 0,
            PAGADO,
            ANULADO,
            VALIDADO,
            CONTABILIZADO,
            BLOQUEADO,
        }

        private String _empresa = null;
        private DateTime _fecha_liquidacion;
        private String _usuario = null;
        private Decimal _id_empresa;
        private String _mensaje_error = null;
        private Int32 _idPeriodoLps = 0;
        private Int32 _periodoLps = 0;

        private DataTable _inconsistencias = null;
        private DataTable _listado;
        private DataTable _empleados;
        private DataTable _contratos;
        private DataTable _novedades;

        private DataRow _solicitud;
        private DataRow _empleado;
        private DataRow _periodo;
        private DataRow _contrato;
        private Int32 _dias_pendientes_pago;
        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public DateTime FechaLiquidacion
        {
            get { return _fecha_liquidacion; }
            set { _fecha_liquidacion = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Decimal IdEmpresa
        {
            get { return _id_empresa; }
            set { _id_empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public DataTable Inconsistencias
        {
            get { return _inconsistencias; }
            set { _inconsistencias = value; }
        }

        public DataTable Listado
        {
            get { return _listado; }
            set { _listado = value; }
        }

        public DataTable Empleados
        {
            get { return _empleados; }
            set { _empleados = value; }
        }

        public DataTable Contratos
        {
            get { return _contratos; }
            set { _contratos = value; }
        }

        public DataTable Novedades
        {
            get { return _novedades; }
            set { _novedades = value; }
        }

        public DataRow Solicitud
        {
            get { return _solicitud; }
            set { _solicitud = value; }
        }

        public DataRow Empleado
        {
            get { return _empleado; }
            set { _empleado = value; }
        }

        public DataRow Periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }

        public Int32 DiasPendientesPago
        {
            get { return _dias_pendientes_pago; }
            set { _dias_pendientes_pago = value; }
        }

        public DataRow Contrato
        {
            get { return _contrato; }
            set { _contrato = value; }
        }

        #endregion propiedades

        #region constructores
        public lpsMasiva(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
            Configurar();
        }
        #endregion constructores

        #region metodos
        public Boolean Liquidar(Decimal ID_EMPRESA, DateTime fechaLiquidacion, DataTable _empleados, DataTable _novedades)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            Boolean liquidado = true;
            String sql = null;
            tools _tools = new tools();

            _periodoLps = 0;
            _idPeriodoLps = 0;
            IdEmpresa = ID_EMPRESA;
            FechaLiquidacion = fechaLiquidacion;
            conexion.IniciarTransaccion();

            try
            {
                nomina.novedadNomina novedad = new novedadNomina(Empresa, Usuario);
                nomina.liquidacionPrestacionesSociales _liqLps = new liquidacionPrestacionesSociales(Empresa, Usuario);
                foreach (DataRow dataRowEmpleados in _empleados.Rows)
                {
                    CrearPeriodoLPS(Convert.ToInt32(dataRowEmpleados["ID_EMPLEADO"].ToString()), _periodoLps, conexion);
                    if (_idPeriodoLps > 0)
                    {
                        foreach (DataRow dataRowNovedades in _novedades.Rows)
                        {
                            if (Convert.ToInt32(dataRowNovedades["ID_EMPLEADO"].ToString()) == Convert.ToInt32(dataRowEmpleados["ID_EMPLEADO"].ToString()))
                            {
                                novedad.Adicionar(Convert.ToDecimal(dataRowEmpleados["ID_EMPLEADO"]),
                                    Convert.ToInt32(dataRowNovedades["ID_CONCEPTO"]),
                                    _idPeriodoLps,
                                    Convert.ToDecimal(dataRowNovedades["CANTIDAD"].ToString()),
                                    Convert.ToDecimal(dataRowNovedades["VALOR"]), "L", "ACTIVO", conexion);
                            }
                        }
                        Empleado = ObtenerEmpleadoPorIdEmpleado(Convert.ToDecimal(dataRowEmpleados["ID_EMPLEADO"]), conexion);
                        DiasPendientesPago = ObtenerDiasPendientesPagar(Convert.ToDecimal(dataRowEmpleados["ID_EMPLEADO"]), FechaLiquidacion, conexion);

                        _liqLps.LiquidarNovedades(ID_EMPRESA, Convert.ToDecimal(dataRowEmpleados["ID_EMPLEADO"].ToString()), Empleado["ID_CIUDAD"].ToString(),
                        Convert.ToDecimal(Empleado["ID_CENTRO_C"]), Convert.ToDecimal(Empleado["ID_SUB_C"]), _idPeriodoLps, DiasPendientesPago, conexion);

                        _liqLps.InactivarEmpleado(Convert.ToDecimal(dataRowEmpleados["ID_EMPLEADO"].ToString()), FechaLiquidacion, conexion);
                        _liqLps.ActivarSolicitudIngreso(Convert.ToDecimal(dataRowEmpleados["ID_EMPLEADO"].ToString()), conexion);

                        DataTable _dataTable_1 = new DataTable();

                        Decimal Cesantias = 0;
                        Decimal InteresesCesantias = 0;
                        Decimal Prima = 0;
                        Decimal Vacaciones = 0;
                        Decimal BaseCesantias = 0;
                        Decimal BasePrima = 0;
                        Decimal BaseVacaciones = 0;

                        sql = "usp_lps_liquidar " + dataRowEmpleados["ID_EMPLEADO"].ToString() + ", '" + _tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "'";
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _dataTable_1 = _dataView.Table;

                        if (_dataTable_1.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in _dataTable_1.Rows)
                            {
                                if (dataRow["Descripcion"].Equals("CESANTIAS")) Cesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                                if (dataRow["Descripcion"].Equals("INTERESES DE CESANTIAS")) InteresesCesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                                if (dataRow["Descripcion"].Equals("PRIMA")) Prima = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                                if (dataRow["Descripcion"].Equals("VACACIONES")) Vacaciones = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;

                                if (dataRow["Descripcion"].Equals("BASE CESANTIAS")) BaseCesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                                if (dataRow["Descripcion"].Equals("BASE PRIMA")) BasePrima = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                                if (dataRow["Descripcion"].Equals("BASE VACACIONES")) BaseVacaciones = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                            }
                        }
                        Decimal TotalLiquidacion = Cesantias + InteresesCesantias + Prima + Vacaciones;

                        DataTable _dataTable_2 = new DataTable();

                        sql = "usp_lps_memorando " + dataRowEmpleados["ID_EMPLEADO"].ToString() + ", " + _idPeriodoLps.ToString() + "";
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _dataTable_2 = _dataView.Table;

                        Decimal _decimal = 0;
                        Decimal TotalDevengadoMemorando = 0;
                        Decimal TotalDeduccionesMemorando = 0;

                        if (_dataTable_2.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in _dataTable_2.Rows)
                            {
                                TotalDevengadoMemorando += Decimal.TryParse(dataRow["DEVENGADOS"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["DEVENGADOS"]) : 0;
                                TotalDeduccionesMemorando += Decimal.TryParse(dataRow["DEDUCCIONES"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["DEDUCCIONES"]) : 0;
                            }
                        }

                        DataTable _dataTable_3 = new DataTable();

                        sql = "usp_lps_anticipos " + dataRowEmpleados["ID_EMPLEADO"].ToString();
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _dataTable_3 = _dataView.Table;

                        Decimal TotalAnticipos = 0;

                        if (_dataTable_3.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in _dataTable_3.Rows)
                            {
                                TotalAnticipos += Decimal.TryParse(dataRow["VALOR"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["VALOR"]) : 0;
                            }
                        }

                        DataTable _dataTable_4 = new DataTable();

                        sql = "usp_lps_obtenerCreditosPorIdEmpleado " + dataRowEmpleados["ID_EMPLEADO"].ToString();
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _dataTable_4 = _dataView.Table;

                        Decimal TotalDescuentos = 0;

                        if (_dataTable_4.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in _dataTable_4.Rows)
                            {
                                TotalAnticipos += Decimal.TryParse(dataRow["SALDO"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["SALDO"]) : 0;
                            }
                        }

                        Decimal NetoLiquidado = (TotalLiquidacion + TotalDevengadoMemorando) - TotalDeduccionesMemorando;

                        Decimal NetoPagar = NetoLiquidado - (TotalAnticipos + TotalDescuentos);

                        sql = "usp_liq_lps_empleado_adicionar ";
                        sql += dataRowEmpleados["ID_EMPLEADO"].ToString() + ", ";
                        sql += _idPeriodoLps.ToString() + ", ";
                        sql += "'MASIVA', ";
                        sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "', ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(BaseCesantias) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(BasePrima) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(BaseVacaciones) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(Cesantias) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(InteresesCesantias) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(Prima) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(Vacaciones) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalLiquidacion) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalDevengadoMemorando) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalDeduccionesMemorando) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(NetoLiquidado) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalAnticipos) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalDescuentos) + ", ";
                        sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(NetoPagar) + ", ";
                        sql += "'', ";
                        sql += "'" + Estados.LIQUIDADO.ToString() + "',";
                        sql += "'', ";
                        sql += "'" + Usuario + "'";

                        Int32 IDLPSEMPLEADO = Convert.ToInt32(conexion.ExecuteScalar(sql).ToString());

                    }
                }

            }
            catch (Exception e)
            {
                liquidado = false;
                conexion.DeshacerTransaccion();
                MensajeError = e.Message;
            }
            finally
            {
                if (liquidado)
                {
                    conexion.AceptarTransaccion();
                }
                else
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = "Error en el proceso de liquidacion masiva de LPS";
                }
            }
            return liquidado;
        }

        private void CrearPeriodoLPS(Int32 ID_EMPLEADO, Int32 PERIODO, Conexion conexion)
        {
            tools _tools = new tools();
            DataSet dataSet = new DataSet();
            StringBuilder sql = new StringBuilder();

            try
            {
                sql.Remove(0, sql.Length);
                sql.Append("usp_lps_masivo_adicionarPeriodoNomina_2 ");
                sql.Append(IdEmpresa + ", ");
                sql.Append("'" + _tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "', ");
                sql.Append(ID_EMPLEADO.ToString() + ", ");
                sql.Append(PERIODO.ToString() + ", ");
                sql.Append("'" + Usuario + "'");

                dataSet = conexion.ExecuteReaderConTransaccion(sql.ToString());
                if (!dataSet.Tables[0].Rows.Count.Equals(0))
                {
                    DataRow _periodoNew = dataSet.Tables[0].Rows[0];
                    _periodoLps = Convert.ToInt32(_periodoNew["PERIODO"].ToString());
                    _idPeriodoLps = Convert.ToInt32(_periodoNew["ID_PERIODO"].ToString());
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (dataSet != null) dataSet.Dispose();
            }
        }

        private void Configurar()
        {
            Listado = new DataTable();
            Listado.Columns.Add("idEmpleado");
            Listado.Columns.Add("numeroDocumento");
            Listado.Columns.Add("nombre");
            Listado.Columns.Add("ingreso");
            Listado.Columns.Add("retiro");

            Inconsistencias = new DataTable();
            Inconsistencias.Columns.Add("idEmpleado");
            Inconsistencias.Columns.Add("numeroDocumento");
            Inconsistencias.Columns.Add("nombre");
            Inconsistencias.Columns.Add("inconsistencia");

            Novedades = new DataTable();
            Novedades.Columns.Add("idConcepto");
            Novedades.Columns.Add("idEmpleado");
            Novedades.Columns.Add("empleado");
            Novedades.Columns.Add("codigo");
            Novedades.Columns.Add("descripcion");
            Novedades.Columns.Add("cantidad");
            Novedades.Columns.Add("valor");
        }

        private DataRow ObtenerEmpleadoPorIdEmpleado(Decimal ID_EMPLEADO, Conexion conexion)
        {
            DataSet dataSet = new DataSet();
            DataRow dataRow = null;
            String sql = null;

            sql = "usp_lps_masivo_obtenerEmpleadoPorIdEmpleado " + ID_EMPLEADO.ToString();
            try
            {
                dataSet = conexion.ExecuteReaderConTransaccion(sql);
                if (!dataSet.Tables[0].Rows.Count.Equals(0)) dataRow = dataSet.Tables[0].Rows[0];
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                if (dataSet != null) dataSet.Dispose();
            }
            return dataRow;
        }

        private DataRow ObtenerContratoPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataRow _dataRow = null;
            String sql = null;

            sql = "usp_lps_masivo_obtenerContratoPorIdEmpleado " + ID_EMPLEADO.ToString();
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                if (!_dataSet.Tables[0].Rows.Count.Equals(0)) _dataRow = _dataSet.Tables[0].Rows[0];

            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }
            if (_dataSet != null) _dataSet.Dispose();
            return _dataRow;
        }

        public DataTable ValidarConcepto(String COD_CONCEPTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_conceptos_nomina_ObtenerPorCodigo '" + COD_CONCEPTO + "'";
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                if (!_dataSet.Tables[0].Rows.Count.Equals(0)) dataTable = _dataSet.Tables[0];
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }
            if (_dataSet != null) _dataSet.Dispose();
            return dataTable;

        }

        public DataTable ObtenerLiquidacionesMasivas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_masivo_obtenerLiquidacionesMasivos ";
            sql += Convert.ToInt32(Periodo["ID_PERIODO"]);

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                if (!dataSet.Tables[0].Rows.Count.Equals(0)) dataTable = dataSet.Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
                if (dataSet != null) dataSet.Dispose();
            }
            return dataTable;
        }

        private void Validar(DataRow dataRow)
        {
            ObtenerSolicitudPorNumeroDocumento(dataRow["numeroDocumento"].ToString());
            if (Solicitud != null)
            {
                if (EsContratado())
                {
                    ObtenerEmpleadoPorIdSolicitud(Solicitud["ID_SOLICITUD"].ToString());
                    if (!Empleados.Rows.Count.Equals(0))
                    {
                        if (EsEmpleadoActivo())
                        {
                            Contrato = ObtenerContratoPorIdEmpleado(Convert.ToDecimal(Empleado["ID_EMPLEADO"]));
                            if (EsContratoVigente())
                            {
                                if (EsEmpleadoDeEmpresa())
                                {
                                    if (!EsEmpleadoLiquidado())
                                    {
                                        if (!EsCasoSevero())
                                        {
                                            if (!EsEmbarazada())
                                            {
                                                if (!EstaEnNomina()) Asignar(dataRow["numeroDocumento"].ToString(), Convert.ToDecimal(Empleado["ID_EMPLEADO"]));

                                                else AgregarInconsistencia(Convert.ToDecimal(Empleado["ID_EMPLEADO"]), dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                                                + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado no puede ser liquidado por estar en una nomina VALIDADA/LIQUIDADA");
                                            }
                                            else AgregarInconsistencia(Convert.ToDecimal(Empleado["ID_EMPLEADO"]), dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                                            + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado no puede ser liquidado por estar en estado de EMBARAZO");
                                        }
                                        else AgregarInconsistencia(Convert.ToDecimal(Empleado["ID_EMPLEADO"]), dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                                        + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado es un CASO SEVERO");
                                    }
                                    else AgregarInconsistencia(Convert.ToDecimal(Empleado["ID_EMPLEADO"]), dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                                    + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado esta LIQUIDADO");
                                }
                                else AgregarInconsistencia(Convert.ToDecimal(Empleado["ID_EMPLEADO"]), dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                                + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado NO pertenece a esta empresa");
                            }
                            else AgregarInconsistencia(Convert.ToDecimal(Empleado["ID_EMPLEADO"]), dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                            + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado NO cuenta con un contrato VIGENTE");
                        }
                        else AgregarInconsistencia(0, dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                        + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado NO esta ACTIVO ");
                    }
                    else AgregarInconsistencia(0, dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                    + " " + Solicitud["APELLIDOS"].ToString().Trim(), "No ha sido registrado un empleado con el numero de solicitud " + Solicitud["ID_SOLICITUD"].ToString());
                }
                else AgregarInconsistencia(0, dataRow["numeroDocumento"].ToString(), Solicitud["NOMBRES"].ToString().Trim()
                    + " " + Solicitud["APELLIDOS"].ToString().Trim(), "El empleado no puede ser liquidado. El estado de la solicitud DEBE ser CONTRATADO");
            }
            else AgregarInconsistencia(0, dataRow["numeroDocumento"].ToString(), null, "El numero de documento NO esta registrado en el sistema");
        }

        private void Asignar(String numeroDocumento, Decimal idEmpleado)
        {
            foreach (DataRow dataRow in Listado.Rows)
            {
                if (dataRow["numeroDocumento"].Equals(numeroDocumento))
                {
                    dataRow["idEmpleado"] = idEmpleado;
                    Listado.AcceptChanges();
                    break;
                }
            }
        }

        private void CrearPeriodoDeNomina()
        {
            Conexion conexion = new Conexion(Empresa);
            tools _tools = new tools();
            DataSet dataSet = new DataSet();
            StringBuilder sql = new StringBuilder();

            try
            {
                sql.Remove(0, sql.Length);
                sql.Append("usp_lps_masivo_adicionarPeriodoNomina ");
                sql.Append(IdEmpresa + ", ");
                sql.Append("'" + _tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "', ");
                sql.Append("'" + Usuario + "'");

                dataSet = conexion.ExecuteReader(sql.ToString());
                if (!dataSet.Tables[0].Rows.Count.Equals(0)) Periodo = dataSet.Tables[0].Rows[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
                if (dataSet != null) dataSet.Dispose();
            }
        }

        private void AgregarInconsistencia(Decimal idEmpleado, String numeroDocumento, String nombre, String inconsistencia)
        {
            DataRow dataRow = Inconsistencias.NewRow();
            dataRow["idEmpleado"] = idEmpleado.ToString();
            dataRow["numeroDocumento"] = numeroDocumento;
            dataRow["nombre"] = nombre;
            dataRow["inconsistencia"] = inconsistencia;
            Inconsistencias.Rows.Add(dataRow);
            Inconsistencias.AcceptChanges();
        }

        public Int32 ObtenerEmpleadoLista(String CEDULA)
        {
            String _empleado = "";
            Int32 _idEmpleado = 0;
            foreach (DataRow listado in Listado.Rows)
            {
                _empleado = listado["numeroDocumento"].ToString();
                if (_empleado == CEDULA)
                {
                    return Convert.ToInt32(listado["idEmpleado"].ToString());
                }
            }
            return _idEmpleado;
        }

        private void ObtenerSolicitudPorNumeroDocumento(String numeroDocumento)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_lps_masivo_obtenerSolicitudPorNumeroDocumento ";
            sql += "'" + numeroDocumento + "'";

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                if (!dataSet.Tables[0].Rows.Count.Equals(0)) Solicitud = dataSet.Tables[0].Rows[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
                if (dataSet != null) dataSet.Dispose();
            }
        }

        private void ObtenerEmpleadoPorIdSolicitud(String idSolicitud)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_lps_masivo_obtenerEmpleadoPorIdSolicitud ";
            sql += idSolicitud;

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                if (!dataSet.Tables[0].Rows.Count.Equals(0)) Empleados = dataSet.Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
                if (dataSet != null) dataSet.Dispose();
            }
        }

        private Int32 ObtenerDiasPendientesPagar(Decimal ID_EMPLEADO, DateTime FCH_LIQUIDA, Conexion conexion)
        {
            tools _tools = new tools();
            Int32 diasPendientesPago = 0;
            String sql = null;

            sql = "usp_lps_nom_periodo_obtenerDiasPendientesPagar ";
            sql += ID_EMPLEADO.ToString() + ",";
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "'";

            try
            {
                diasPendientesPago = Convert.ToInt32(conexion.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            return diasPendientesPago;
        }

        public DataTable ObtenerIdEmpleadoLpsMasiva(Decimal ID_EMPRESA, String CEDULA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empleado_lps_masiva ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + CEDULA.ToString() + "'";

            #endregion

            if (ejecutar == true)
            {
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
        #endregion metodos

        #region reglas
        private Boolean EsContratado()
        {
            Boolean contratado = true;

            if (!String.IsNullOrEmpty(Solicitud["ARCHIVO"].ToString()))
            {
                if (!Solicitud["ARCHIVO"].ToString().Trim().Equals("CONTRATADO")) return false;
            }
            return contratado;
        }

        private Boolean EsEmpleadoActivo()
        {
            Boolean activo = true;

            DataRow[] dataRow = Empleados.Select("ACTIVO = 'S'");
            Int32 maximo = 0;

            if (!dataRow.Length.Equals(0))
            {
                if (dataRow.Length > 0)
                {
                    for (int i = 0; i < dataRow.Length; i++)
                    {
                        if (Convert.ToInt32(dataRow[i]["ID_EMPLEADO"]) > maximo)
                        {
                            maximo = Convert.ToInt32(dataRow[i]["ID_EMPLEADO"]);
                            Empleado = dataRow[i];
                        }
                    }
                }
                else Empleado = dataRow[0];
            }
            else activo = false;
            return activo;
        }

        private Boolean EsEmpleadoDeEmpresa()
        {
            if (Empleado["ID_EMPRESA"].Equals(IdEmpresa)) return true;
            else return false;
        }

        private Boolean EsEmpleadoLiquidado()
        {
            if (Empleado["LIQUIDADO"].ToString().Trim().Equals("LIQUIDADO")) return true;
            else return false;
        }

        private Boolean EsCasoSevero()
        {
            if (Empleado["CASO_SEVERO"].ToString().Trim().Equals("S")) return true;
            else return false;
        }

        private Boolean EsEmbarazada()
        {
            if (Empleado["EMBARAZADO"].ToString().Trim().Equals("S")) return true;
            else return false;
        }

        private Boolean EstaIncapacitado()
        {
            Boolean incapacitado = true;
            return incapacitado;
        }

        private Boolean EsContratoVigente()
        {
            Boolean esContratoVigente = false;
            if (Contrato != null)
            {
                if (!String.IsNullOrEmpty(Contrato["VIGENTE"].ToString()))
                {
                    if (Contrato["VIGENTE"].ToString().Trim().Equals("S")) esContratoVigente = true;
                }
            }
            return esContratoVigente;
        }

        private Boolean EstaEnNomina()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;
            Boolean empleadoEnNomina = false;

            sql = "usp_lps_masivo_obtenerPeriodoNomina ";

            sql += !String.IsNullOrEmpty(Empleado["ID_EMPRESA"].ToString()) ? Empleado["ID_EMPRESA"].ToString() + ", " : "Null, ";
            sql += !String.IsNullOrEmpty(Empleado["ID_EMPLEADO"].ToString()) ? Empleado["ID_EMPLEADO"].ToString() + ", " : "Null, ";
            sql += !String.IsNullOrEmpty(Empleado["ID_CIUDAD"].ToString()) ? Empleado["ID_CIUDAD"].ToString() + ", " : "Null, ";
            sql += !String.IsNullOrEmpty(Empleado["ID_CENTRO_C"].ToString()) ? Empleado["ID_CENTRO_C"].ToString() + ", " : "Null, ";
            sql += !String.IsNullOrEmpty(Empleado["ID_SUB_C"].ToString()) ? Empleado["ID_SUB_C"].ToString() : "Null";

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                if (!dataSet.Tables[0].Rows.Count.Equals(0)) empleadoEnNomina = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
                if (dataSet != null) dataSet.Dispose();
            }
            return empleadoEnNomina;
        }

        #endregion reglas
    }
}
