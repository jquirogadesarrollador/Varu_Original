using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class liquidacionPrestacionesSociales
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

        enum Liquidaciones
        {
            LIQUIDACION = 0,
            RELIQUIDACION
        }
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        Int32 _id_periodo;
        private Decimal _total_liquidacion;
        private Decimal _total_devengado_memorando;
        private Decimal _total_deducciones_memorando;
        private Decimal _neto_liquidado;
        private Decimal _total_anticipos;
        private Decimal _total_descuentos;
        private Decimal _neto_pagar;
        private Boolean _caso_severo;
        private Decimal _cesantias = 0;
        private Decimal _intereses_cesantias = 0;
        private Decimal _prima = 0;
        private Decimal _vacaciones = 0;
        private Decimal _promedio_cesantias = 0;
        private Decimal _dias_cesantias = 0;
        private Decimal _promedio_prima = 0;
        private Decimal _dias_prima = 0;
        private Decimal _promedio_vacaciones = 0;
        private Decimal _dias_vacaciones = 0;

        private Decimal _base_cesantias = 0;
        private Decimal _base_prima = 0;
        private Decimal _base_vacaciones = 0;

        private Int32 ID_LPS_EMPLEADO = 0;

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

        public Int32 ID_PERIODO
        {
            get { return _id_periodo; }
            set { _id_periodo = value; }
        }

        public Decimal TotalLiquidacion
        {
            get { return _total_liquidacion; }
            set { _total_liquidacion = value; }
        }

        public Decimal TotalDevengadoMemorando
        {
            get { return _total_devengado_memorando; }
            set { _total_devengado_memorando = value; }
        }

        public Decimal TotalDeduccionesMemorando
        {
            get { return _total_deducciones_memorando; }
            set { _total_deducciones_memorando = value; }
        }

        public Decimal NetoLiquidado
        {
            get { return _neto_liquidado; }
            set { _neto_liquidado = value; }
        }

        public Decimal TotalAnticipos
        {
            get { return _total_anticipos; }
            set { _total_anticipos = value; }
        }

        public Decimal TotalDescuentos
        {
            get { return _total_descuentos; }
            set { _total_descuentos = value; }
        }

        public Decimal NetoPagar
        {
            get { return _neto_pagar; }
            set { _neto_pagar = value; }
        }

        public Int32 IDLPSEMPLEADO
        {
            get { return ID_LPS_EMPLEADO; }
            set { ID_LPS_EMPLEADO = value; }
        }

        public Decimal Cesantias
        {
            get { return _cesantias; }
            set { _cesantias = value; }
        }

        public Decimal InteresesCesantias
        {
            get { return _intereses_cesantias; }
            set { _intereses_cesantias = value; }
        }

        public Decimal Prima
        {
            get { return _prima; }
            set { _prima = value; }
        }

        public Decimal Vacaciones
        {
            get { return _vacaciones; }
            set { _vacaciones = value; }
        }

        public Decimal BaseCesantias
        {
            get { return _base_cesantias; }
            set { _base_cesantias = value; }
        }

        public Decimal BasePrima
        {
            get { return _base_prima; }
            set { _base_prima = value; }
        }

        public Decimal BaseVacaciones
        {
            get { return _base_vacaciones; }
            set { _base_vacaciones = value; }
        }

        public Decimal PromedioCesantias
        {
            get { return _promedio_cesantias; }
            set { _promedio_cesantias = value; }
        }

        public Decimal DiasCesantias
        {
            get { return _dias_cesantias; }
            set { _dias_cesantias = value; }
        }

        public Decimal PromedioPrima
        {
            get { return _promedio_prima; }
            set { _promedio_prima = value; }
        }

        public Decimal DiasPrima
        {
            get { return _dias_prima; }
            set { _dias_prima = value; }
        }

        public Decimal PromedioVacaciones
        {
            get { return _promedio_vacaciones; }
            set { _promedio_vacaciones = value; }
        }

        public Decimal DiasVacaciones
        {
            get { return _dias_vacaciones; }
            set { _dias_vacaciones = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        #endregion propiedades

        #region constructores
        public liquidacionPrestacionesSociales(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        private void Adicionar(Decimal ID_EMPLEADO, DateTime FECHA_LIQUIDACION, String CASO_SEVERO, Conexion conexion, Boolean PAZYSALVO, Int32 DIAS_DESCONTAR_LPS,
            Decimal VALOR_DESCONTAR_BASES, String OBSERVACIONES, Boolean NO_LABORO)
        {
            tools _tools = new tools();
            String sql = null;


            sql = "usp_liq_lps_empleado_adicionar ";
            sql += ID_EMPLEADO.ToString() + ", ";
            sql += ID_PERIODO.ToString() + ", ";
            sql += "'" + Liquidaciones.LIQUIDACION + "', ";
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_LIQUIDACION) + "', ";
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
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(PromedioCesantias) + ", ";
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(DiasCesantias) + ", ";
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(PromedioPrima) + ", ";
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(DiasPrima) + ", ";
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(PromedioVacaciones) + ", ";
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(DiasVacaciones) + ", ";
            sql += "'" + CASO_SEVERO + "', ";
            sql += "'" + Estados.LIQUIDADO.ToString() + "',";
            sql += "'" + PAZYSALVO + "', ";
            sql += "'" + Usuario + "', ";
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(DIAS_DESCONTAR_LPS) + ", ";
            sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR_DESCONTAR_BASES) + ", ";
            sql += "'" + OBSERVACIONES + "', ";
            sql += "'" + NO_LABORO + "'";
            try
            {
                IDLPSEMPLEADO = Convert.ToInt32(conexion.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DataTable ObtenerPorNumeroIdentificacion(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtenerEmpleadoPorDocumento '" + NUM_DOC_IDENTIDAD + "'";

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
            return _dataTable;
        }

        public DataTable ObtenerPorNombre(String NOMBRES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtenerEmpleadoPorNombre '" + NOMBRES + "'";

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
            return _dataTable;
        }

        public DataTable ObtenerContratoPorIdEmpleado(Int32 ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_contrato_por_idempleado " + ID_EMPLEADO.ToString() + "";

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
            return _dataTable;
        }

        public DataTable ObtenerPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtenerPorIdEmpleado " + ID_EMPLEADO.ToString();

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
            return _dataTable;
        }

        public DataTable ObtenerCreditosPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtenerCreditosPorIdEmpleado " + ID_EMPLEADO.ToString();

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
            CalcularTotalDescuentos(_dataTable);
            CalcularNetoLiquidado();
            CalcularNetoPagar();
            return _dataTable;
        }

        public void ObtenerCreditosPorIdEmpleado(Decimal ID_EMPLEADO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtenerCreditosPorIdEmpleado " + ID_EMPLEADO.ToString() + ", ";
            sql += ID_PERIODO + "";
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

        public DataTable ObtenerAcumulados(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtenerAcumulados " + ID_EMPLEADO.ToString();

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
            return _dataTable;
        }

        public DataTable ObtenerAnticipos(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_anticipos " + ID_EMPLEADO.ToString();

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
            CalcularTotalAnticipos(_dataTable);
            CalcularNetoLiquidado();
            CalcularNetoPagar();
            return _dataTable;
        }

        public void ObtenerAnticipos(Decimal ID_EMPLEADO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_anticipos " + ID_EMPLEADO.ToString() + ", ";
            sql += ID_PERIODO.ToString();
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

        public Boolean Liquidar(Decimal ID_EMPLEADO, DateTime FCH_LQUIDA, List<novedadNomina> novedades, String CASO_SEVERO, Boolean PAZYSALVO, Int32 DIAS_DESCONTAR_LPS,
            Decimal VALOR_DESCONTAR_EN_BASES, String OBSERVACIONES, Boolean NO_LABORO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean liquidado = true;
            Decimal ID_EMPRESA = 0;
            String ID_CIUDAD = null;
            Decimal ID_CENTRO_C = 0;
            Decimal ID_SUB_C = 0;

            DataRow dataRow = ObtenerEmpleadoPorIdEmpleado(ID_EMPLEADO);
            if (dataRow != null)
            {
                if (!DBNull.Value.Equals(dataRow["ID_EMPRESA"])) ID_EMPRESA = Convert.ToDecimal(dataRow["ID_EMPRESA"]);
                if (!DBNull.Value.Equals(dataRow["ID_CIUDAD"])) ID_CIUDAD = dataRow["ID_CIUDAD"].ToString();
                if (!DBNull.Value.Equals(dataRow["ID_CENTRO_C"])) ID_CENTRO_C = Convert.ToDecimal(dataRow["ID_CENTRO_C"]);
                if (!DBNull.Value.Equals(dataRow["ID_SUB_C"])) ID_SUB_C = Convert.ToDecimal(dataRow["ID_SUB_C"]);


                try
                {
                    CrearPeriodo(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, conexion);

                    if (ID_PERIODO > 0)
                    {

                        if (novedades.Count > 0 && NO_LABORO == false)
                        {
                            foreach (novedadNomina novedad in novedades)
                            {
                                novedad.Adicionar(novedad.IdEmpleado, Convert.ToInt32(novedad.IdConcepto), Convert.ToInt32(ID_PERIODO),
                                    Convert.ToDecimal(novedad.Cantidad), Convert.ToDecimal(novedad.Valor), "L", "ACTIVO", conexion);
                            }
                        }

                        if (NO_LABORO == false)
                        {
                            Int32 DIAS_PENDIENTES_PAGO = ObtenerDiasPendientesPagar(ID_EMPLEADO, FCH_LQUIDA, conexion);
                            LiquidarNovedades(ID_EMPRESA, ID_EMPLEADO, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, ID_PERIODO, DIAS_PENDIENTES_PAGO, conexion);
                            ObtenerAnticipos(ID_EMPLEADO, conexion);
                            ObtenerCreditosPorIdEmpleado(ID_EMPLEADO, conexion);
                        }

                        InactivarEmpleado(ID_EMPLEADO, FCH_LQUIDA, conexion);

                        Liquidar(ID_EMPLEADO, FCH_LQUIDA, conexion, DIAS_DESCONTAR_LPS, VALOR_DESCONTAR_EN_BASES, OBSERVACIONES, NO_LABORO);

                        ObtenerMemorando(ID_EMPLEADO, conexion);

                        CalcularNetoLiquidado();

                        CalcularNetoPagar();

                        Adicionar(ID_EMPLEADO, FCH_LQUIDA, CASO_SEVERO, conexion, PAZYSALVO, DIAS_DESCONTAR_LPS, VALOR_DESCONTAR_EN_BASES, OBSERVACIONES, NO_LABORO);
                    }
                    else
                    {
                        liquidado = false;
                        MensajeError = "Error al generar el memorando para esta LPS, Consulta con el administrador del sistema.";
                        if (ID_PERIODO == -1)
                        {
                            MensajeError = "Error al generar el memorando para esta LPS, Tiene un memorando en estado LIQUIDADO.";
                        }
                        if (ID_PERIODO == -1)
                        {
                            MensajeError = "Error al generar el memorando para esta LPS, Tiene un memorando en estado validado.";
                        }
                    }
                }
                catch (Exception e)
                {
                    liquidado = false;
                    MensajeError = e.Message;
                }
                finally
                {
                }
            }
            else
            {
                liquidado = false;
                MensajeError = "El ID de Empleado no se encuentra registrado en la base de datos";
            }
            return liquidado;
        }

        public Boolean Actualizar(Decimal ID_LPS, Boolean PAZYSALVO, Boolean BLOQUEADA, String TIPO, Boolean NO_LABORO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean actualizar = true;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_liq_lps_empleado_actualizar ";

            #region validaciones
            if (ID_LPS > 0)
            {
                sql += ID_LPS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_LPS es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PAZYSALVO + "', ";
            sql += "'" + BLOQUEADA + "', ";
            sql += "'" + TIPO + "', ";
            sql += "'" + NO_LABORO + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    if (Convert.ToInt32(conexion.ExecuteScalar(sql)) > 0) actualizar = true;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    conexion.DeshacerTransaccion();
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return actualizar;
        }

        public String CalcularDiasLps(DateTime FECHA_INICIAL, DateTime FECHA_FINAL)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String _dias = "0";
            tools _tools = new tools();
            Boolean ejecutar = true;

            sql = "usp_calculas_dias_lps ";

            #region validaciones

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    _dias = conexion.ExecuteScalar(sql);
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
            return _dias;
        }


        private DataRow ObtenerEmpleadoPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            DataRow _dataRow = null;
            String sql = null;

            sql = "usp_lps_nom_empleados_obtenerPorIdEmpleado " + ID_EMPLEADO.ToString();
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
                _dataRow = _dataTable.Rows[0];
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
            if (_dataView != null) _dataView.Dispose();
            if (_dataTable != null) _dataTable.Dispose();
            return _dataRow;
        }

        private void CrearPeriodo(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, Conexion conexion)
        {
            ID_PERIODO = 0;
            String sql = null;
            sql = "usp_nom_periodo_memo_adicionar_por_cobertura " + ID_EMPRESA.ToString() + ", '" + ID_CIUDAD.ToString() + "', " + ID_CENTRO_C.ToString() + ", " + ID_SUB_C.ToString() + ", " + "'" + Usuario + "', 'S', 'LPS'";
            try
            {
                ID_PERIODO = Convert.ToInt32(conexion.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
        }

        private Int32 ObtenerDiasPendientesPagar(Decimal ID_EMPLEADO, DateTime FCH_LIQUIDA, Conexion conexion)
        {
            tools _tools = new tools();

            Int32 DIAS_PENDIENTES = 0;
            String sql = null;

            sql = "usp_lps_nom_periodo_obtenerDiasPendientesPagar ";
            sql += ID_EMPLEADO.ToString() + ",";
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_LIQUIDA) + "'";

            try
            {
                DIAS_PENDIENTES = Convert.ToInt32(conexion.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                conexion.Desconectar();
                MensajeError = e.Message;
            }
            return DIAS_PENDIENTES;
        }

        public DataRow ObtenerPorId(Int32 ID_LPS_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            DataRow _dataRow = null;
            String sql = null;

            sql = "usp_lps_empleado_obtenerPorId ";
            sql += ID_LPS_EMPLEADO.ToString();

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
                if (_dataTable.Rows.Count > 0) _dataRow = _dataTable.Rows[0];
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }
            return _dataRow;
        }

        public void ValidarGeneracionLPS(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            DataRow _dataRow = null;
            String sql = null;

            sql = "usp_lps_validarGeneracionLPS ";
            sql += ID_EMPLEADO.ToString();

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
                if (_dataTable.Rows.Count > 0) _dataRow = _dataTable.Rows[0];
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (_dataRow != null)
            {
                if (_dataRow["ESTADO"].ToString() == Estados.LIQUIDADO.ToString()) MensajeError = "El trabajador se encuentra actualmente en una nómina en proceso de VALIDACION, y no puede ser generada la LPS";
                if (_dataRow["ESTADO"].ToString() == Estados.VALIDADO.ToString()) MensajeError = "El trabajador se encuentra actualmente en una nómina en proceso de CONTABILIZACION, y no puede ser generada la LPS";
            }
        }

        public void LiquidarNovedades(Decimal ID_EMPRESA, Decimal ID_EMPLEADO, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, Int32 ID_PERIODO, Int32 DIAS_PENDIENTES_PAGO, Conexion conexion)
        {
            String sql = null;
            sql = "usp_liquidacion_nomina ";
            sql += ID_EMPRESA.ToString() + ", ";
            sql += "1, ";
            sql += "'" + Usuario + "', ";
            sql += "'" + ID_PERIODO.ToString() + "', ";
            sql += "'L', ";
            sql += "'N', ";
            sql += "'N', ";
            sql += ID_EMPLEADO.ToString() + ", ";
            sql += DIAS_PENDIENTES_PAGO.ToString() + "";
            try
            {
                conexion.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
        }

        public DataTable ObtenerLiquidacion(Decimal ID_EMPLEADO, DateTime FCH_LQUIDA, Decimal ID_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            tools _tools = new tools();

            sql = "usp_obtener_lps " + ID_EMPLEADO.ToString() + ", ";
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_LQUIDA).ToString() + "', ";
            sql += ID_PERIODO.ToString() + "";
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
            return _dataTable;
        }

        public DataTable CargarLpsParalelo()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            tools _tools = new tools();

            sql = "usp_obtener_lps_paralelo ";
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
            return _dataTable;
        }


        public DataTable CargarLpsPrint()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            tools _tools = new tools();

            sql = "usp_obtener_lps_paralelo_print ";
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
            return _dataTable;
        }

        private void Liquidar(Decimal ID_EMPLEADO, DateTime FCH_LQUIDA, Conexion conexion, Int32 DIAS_DESCONTAR_LPS, Decimal VALOR_DESCONTAR_BASES, String OBSERVACIONES, Boolean NO_LABORO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            Cesantias = 0;
            InteresesCesantias = 0;
            Prima = 0;
            Vacaciones = 0;
            tools _tools = new tools();

            String sql = null;

            sql = "usp_lps_liquidar " + ID_EMPLEADO.ToString() + ", " + ID_PERIODO.ToString() + ", '" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_LQUIDA).ToString() + "', ";
            sql = sql + DIAS_DESCONTAR_LPS.ToString() + ", " + VALOR_DESCONTAR_BASES.ToString() + ", '" + NO_LABORO + "'";
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

            if (_dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in _dataTable.Rows)
                {
                    if (dataRow["Descripcion"].Equals("CESANTIAS")) Cesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("INTERESES DE CESANTIAS")) InteresesCesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("PRIMA")) Prima = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("VACACIONES")) Vacaciones = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;

                    if (dataRow["Descripcion"].Equals("BASE CESANTIAS")) BaseCesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("BASE PRIMA")) BasePrima = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("BASE VACACIONES")) BaseVacaciones = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;

                    if (dataRow["Descripcion"].Equals("PROMEDIO CESANTIAS")) PromedioCesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("DIAS CESANTIAS")) DiasCesantias = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("PROMEDIO PRIMA")) PromedioPrima = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("DIAS PRIMA")) DiasPrima = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("PROMEDIO VACACIONES")) PromedioVacaciones = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                    if (dataRow["Descripcion"].Equals("DIAS VACACIONES")) DiasVacaciones = !String.IsNullOrEmpty(dataRow["Valor"].ToString()) ? Convert.ToDecimal(dataRow["Valor"]) : 0;
                }
            }

            CalcularTotalLiquidacion(_dataTable);
        }

        public DataTable ObtenerMemorando(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_memorando " + ID_EMPLEADO.ToString() + ", ";
            sql += ID_PERIODO.ToString();

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
            finally
            {
                conexion.Desconectar();
            }
            CalcularTotalesMemorando(_dataTable);
            CalcularNetoLiquidado();
            CalcularNetoPagar();
            return _dataTable;
        }

        public DataTable ObtenerMemorando(Decimal ID_EMPLEADO, Int32 ID_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_memorando " + ID_EMPLEADO.ToString() + ", ";
            sql += ID_PERIODO.ToString();

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
            finally
            {
                conexion.Desconectar();
            }
            CalcularTotalesMemorando(_dataTable);
            CalcularNetoLiquidado();
            CalcularNetoPagar();
            return _dataTable;
        }

        public void ObtenerMemorando(Decimal ID_EMPLEADO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_memorando " + ID_EMPLEADO.ToString() + ", ";
            sql += ID_PERIODO.ToString();

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
            CalcularTotalesMemorando(_dataTable);
        }

        private void CalcularTotalLiquidacion(DataTable dataTable)
        {
            Decimal _decimal = 0;
            TotalLiquidacion = 0;
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if ((dataRow["Descripcion"].Equals("CESANTIAS"))
                        || (dataRow["Descripcion"].Equals("INTERESES DE CESANTIAS"))
                        || (dataRow["Descripcion"].Equals("PRIMA"))
                        || (dataRow["Descripcion"].Equals("VACACIONES"))
                        ) TotalLiquidacion += Decimal.TryParse(dataRow["VALOR"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["VALOR"]) : 0;

                }
            }
        }

        private void CalcularTotalesMemorando(DataTable dataTable)
        {
            Decimal _decimal = 0;
            TotalDevengadoMemorando = 0;
            TotalDeduccionesMemorando = 0;

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TotalDevengadoMemorando += Decimal.TryParse(dataRow["DEVENGADOS"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["DEVENGADOS"]) : 0;
                    TotalDeduccionesMemorando += Decimal.TryParse(dataRow["DEDUCCIONES"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["DEDUCCIONES"]) : 0;
                }
            }
        }

        private void CalcularNetoLiquidado()
        {
            NetoLiquidado = 0;
            NetoLiquidado = (TotalLiquidacion + TotalDevengadoMemorando) - TotalDeduccionesMemorando;
        }

        private void CalcularTotalAnticipos(DataTable dataTable)
        {
            Decimal _decimal = 0;
            TotalAnticipos = 0;

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TotalAnticipos += Decimal.TryParse(dataRow["VALOR"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["VALOR"]) : 0;
                }
            }
        }

        private void CalcularTotalDescuentos(DataTable dataTable)
        {
            Decimal _decimal = 0;
            TotalAnticipos = 0;

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    TotalAnticipos += Decimal.TryParse(dataRow["SALDO"].ToString(), out _decimal) ? Convert.ToDecimal(dataRow["SALDO"]) : 0;
                }
            }
        }

        private void CalcularNetoPagar()
        {
            NetoPagar = 0;
            NetoPagar = NetoLiquidado - (TotalAnticipos + TotalDescuentos);
        }

        public void InactivarEmpleado(Decimal ID_EMPLEADO, DateTime FCH_LQUIDA, Conexion conexion)
        {
            tools _tools = new tools();
            String sql = null;

            sql = "usp_lps_nom_empleados_inactivar " + ID_EMPLEADO.ToString() + ",";
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_LQUIDA) + "'";

            try
            {
                conexion.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ActivarSolicitudIngreso(Decimal ID_EMPLEADO, Conexion conexion)
        {
            tools _tools = new tools();
            String sql = null;

            sql = "usp_lps_reg_solicitudes_ingreso_activar " + ID_EMPLEADO.ToString();

            try
            {
                conexion.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DataTable ObtenerLpsHojaTrabajo(String TIPO, String VALOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_lps_hoja_de_trabajo ";

            sql += "'" + TIPO + "', ";
            sql += "'" + VALOR + "'";
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
            return _dataTable;
        }
        #endregion metodos
    }
}