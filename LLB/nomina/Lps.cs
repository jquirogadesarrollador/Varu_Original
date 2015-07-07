using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using Brainsbits.LDA;
using Brainsbits.LLB.nomina;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.nomina
{
    public class Lps
    {
        #region variables
        public enum Liquidaciones
        {
            Liquidacion = 0,
            PreLiquidacion,
            Reliquidacion
        }

        public enum Reportes
        {
            Lps = 0,
            Acumulados
        }

        public enum Estados
        {
            Liquidado = 0,
            Validado,
            Contabilizado,
            Facturado,
            Pagado
        }

        DataTable _cesantias = null;
        DataTable _primas = null;
        DataTable _vacaciones = null;
        DataTable _nomina = null;
        DataTable _novedades = null;
        DataTable _memorando = null;
        MemoryStream _reporte = null;
        MemoryStream _acumulados = null;

        private ReportDocument reportDocument;

        int _id_lps_empleado;
        decimal _id_empleado;
        string _tipo_lps;
        int id_periodo;
        DateTime _fecha;
        string estado = null;

        DateTime _fecha_ingreso;
        DateTime _fecha_liquidacion;
        decimal _salario;
        decimal _smmlv;
        decimal _subsidio_transporte;
        int _dias_vinculacion;
        int _dias_descontar;
        int _dias_cesantias;
        int _dias_prima;
        int _dias_vacaciones;
        int _dias_liquidacion_cesantias;
        int _dias_liquidacion_prima;
        int _dias_liquidacion_vacaciones;
        int _periodo_contable_cesantias;
        int _periodo_contable_prima;
        int _periodo_contable_vacaciones;

        decimal _acumulado_cesantias;
        decimal _acumulado_prima;
        decimal _acumulado_vacaciones;
        decimal _base_cesantias;
        decimal _base_prima;
        decimal _base_vacaciones;
        decimal _liquidacion_cesantias;
        decimal _liquidacion_intereses_cesantias;
        decimal _liquidacion_prima;
        decimal _liquidacion_vacaciones;

        decimal _total_liquidacion;
        decimal _total_devengado_memo;
        decimal _total_deducido_memo;
        decimal _neto_liquidacion;

        decimal _total_anticipos;
        decimal _total_creditos;
        decimal _total_pagar;

        decimal _id_empresa;
        string _id_ciudad;
        decimal _id_centro_costo;
        decimal _id_sub_centro_costo;
        int _dias_pendientes_pagar;
        bool _no_laboro = false;

        String _empresa = null;
        String _usuario = null;

        #endregion variables

        #region propiedades

        public DataTable Cesantias
        {
            get { return _cesantias; }
            set { _cesantias = value; }
        }

        public DataTable Primas
        {
            get { return _primas; }
            set { _primas = value; }
        }

        public DataTable Vacaciones
        {
            get { return _vacaciones; }
            set { _vacaciones = value; }
        }

        public DataTable Nomina
        {
            get { return _nomina; }
            set { _nomina = value; }
        }

        public DataTable Novedades
        {
            get { return _novedades; }
            set { _novedades = value; }
        }

        public DataTable Memorando
        {
            get { return _memorando; }
            set { _memorando = value; }
        }

        public MemoryStream Reporte
        {
            get { return _reporte; }
            set { _reporte = value; }
        }

        public MemoryStream Acumulados
        {
            get { return _acumulados; }
            set { _acumulados = value; }
        }

        public int IdLpsEmpleado
        {
            get { return _id_lps_empleado; }
            set { _id_lps_empleado = value; }
        }

        public decimal IdEmpleado
        {
            get { return _id_empleado; }
            set { _id_empleado = value; }
        }

        public string TipoLps
        {
            get { return _tipo_lps; }
            set { _tipo_lps = value; }
        }

        public int IdPeriodo
        {
            get { return id_periodo; }
            set { id_periodo = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public DateTime FechaIngreso
        {
            get { return _fecha_ingreso; }
            set { _fecha_ingreso = value; }
        }

        public DateTime FechaLiquidacion
        {
            get { return _fecha_liquidacion; }
            set { _fecha_liquidacion = value; }
        }

        public decimal Salario
        {
            get { return _salario; }
            set { _salario = value; }
        }

        public decimal Smmlv
        {
            get { return _smmlv; }
            set { _smmlv = value; }
        }

        public decimal SubsidioTransporte
        {
            get { return _subsidio_transporte; }
            set { _subsidio_transporte = value; }
        }

        public int DiasVinculacion
        {
            get { return _dias_vinculacion; }
            set { _dias_vinculacion = value; }
        }

        public int DiasDescontar
        {
            get { return _dias_descontar; }
            set { _dias_descontar = value; }
        }

        public int DiasCesantias
        {
            get { return _dias_cesantias; }
            set { _dias_cesantias = value; }
        }

        public int DiasPrima
        {
            get { return _dias_prima; }
            set { _dias_prima = value; }
        }

        public int DiasVacaciones
        {
            get { return _dias_vacaciones; }
            set { _dias_vacaciones = value; }
        }

        public int DiasLiquidacionCesantias
        {
            get { return _dias_liquidacion_cesantias; }
            set { _dias_liquidacion_cesantias = value; }
        }

        public int DiasLiquidacionPrima
        {
            get { return _dias_liquidacion_prima; }
            set { _dias_liquidacion_prima = value; }
        }

        public int DiasLiquidacionVacaciones
        {
            get { return _dias_liquidacion_vacaciones; }
            set { _dias_liquidacion_vacaciones = value; }
        }

        public int PeriodoContableCesantias
        {
            get { return _periodo_contable_cesantias; }
            set { _periodo_contable_cesantias = value; }
        }

        public int PeriodoContablePrima
        {
            get { return _periodo_contable_prima; }
            set { _periodo_contable_prima = value; }
        }

        public int PeriodoContableVacaciones
        {
            get { return _periodo_contable_vacaciones; }
            set { _periodo_contable_vacaciones = value; }
        }

        public decimal AcumuladoCesantias
        {
            get { return _acumulado_cesantias; }
            set { _acumulado_cesantias = value; }
        }

        public decimal AcumuladoPrima
        {
            get { return _acumulado_prima; }
            set { _acumulado_prima = value; }
        }

        public decimal AcumuladoVacaciones
        {
            get { return _acumulado_vacaciones; }
            set { _acumulado_vacaciones = value; }
        }

        public decimal BaseCesantias
        {
            get { return _base_cesantias; }
            set { _base_cesantias = value; }
        }

        public decimal BasePrima
        {
            get { return _base_prima; }
            set { _base_prima = value; }
        }

        public decimal BaseVacaciones
        {
            get { return _base_vacaciones; }
            set { _base_vacaciones = value; }
        }

        public decimal LiquidacionCesantias
        {
            get { return _liquidacion_cesantias; }
            set { _liquidacion_cesantias = value; }
        }

        public decimal LiquidacionInteresesCesantias
        {
            get { return _liquidacion_intereses_cesantias; }
            set { _liquidacion_intereses_cesantias = value; }
        }

        public decimal LiquidacionPrima
        {
            get { return _liquidacion_prima; }
            set { _liquidacion_prima = value; }
        }

        public decimal LiquidacionVacaciones
        {
            get { return _liquidacion_vacaciones; }
            set { _liquidacion_vacaciones = value; }
        }

        public decimal TotalLiquidacion
        {
            get { return _total_liquidacion; }
            set { _total_liquidacion = value; }

        }

        public decimal TotalDevengadoMemo
        {
            get { return _total_devengado_memo; }
            set { _total_devengado_memo = value; }
        }

        public decimal TotalDeducidoMemo
        {
            get { return _total_deducido_memo; }
            set { _total_deducido_memo = value; }
        }

        public decimal NetoLiquidacion
        {
            get { return _neto_liquidacion; }
            set { _neto_liquidacion = value; }
        }

        public decimal TotalAnticipos
        {
            get { return _total_anticipos; }
            set { _total_anticipos = value; }
        }

        public decimal TotalCreditos
        {
            get { return _total_creditos; }
            set { _total_creditos = value; }
        }

        public decimal TotalPagar
        {
            get { return _total_pagar; }
            set { _total_pagar = value; }
        }

        private decimal IdEmpresa
        {
            get { return _id_empresa; }
            set { _id_empresa = value; }
        }

        private string IdCiudad
        {
            get { return _id_ciudad; }
            set { _id_ciudad = value; }
        }

        private decimal IdCentroCosto
        {
            get { return _id_centro_costo; }
            set { _id_centro_costo = value; }
        }

        private decimal IdSubCentroCosto
        {
            get { return _id_sub_centro_costo; }
            set { _id_sub_centro_costo = value; }
        }

        public int DiasPendientesPagar
        {
            get { return _dias_pendientes_pagar; }
            set { _dias_pendientes_pagar = value; }
        }

        public bool NoLaboro
        {
            get { return _no_laboro; }
            set { _no_laboro = value; }
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
        #endregion propiedades

        #region constructores
        public Lps(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
            Configurar();
        }

        ~Lps()
        {
            try
            {
                reportDocument.Dispose();
                reportDocument = null;
                reportDocument.Close();
            }
            catch
            {
            }
        }
        #endregion constructores

        public void Dispose()
        {
            try
            {
                reportDocument.Dispose();
                reportDocument = null;
                reportDocument.Close();
            }
            catch
            {
            }
        }

        #region metodos

        private void Configurar()
        {
            Novedades = new DataTable();
            Novedades.Columns.Add("id_concepto");
            Novedades.Columns.Add("descripcion");
            Novedades.Columns.Add("cantidad");
            Novedades.Columns.Add("valor");
            Novedades.AcceptChanges();

            Memorando = new DataTable();
            Memorando.Columns.Add("codigo_concepto");
            Memorando.Columns.Add("descripcion");
            Memorando.Columns.Add("tipo");
            Memorando.Columns.Add("cantidad");
            Memorando.Columns.Add("valor_devengado");
            Memorando.Columns.Add("valor_deducido");
            Memorando.AcceptChanges();
        }

        public DataTable ObtenerPorNumeroDocumento(string numeroDocumento)
        {
            Conexion Datos = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtener_PorNumeroDocumento ";
            sql += "'" + numeroDocumento + "'";

            try
            {
                dataTable = Datos.ExecuteReader(sql).Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
            return dataTable;
        }

        public DataTable ObtenerPorNombre(string nombre)
        {
            Conexion Datos = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtener_PorNombre ";
            sql += "'" + nombre + "'";

            try
            {
                dataTable = Datos.ExecuteReader(sql).Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
            return dataTable;
        }

        public DataTable ObtenerPorIdEmpleado(string idEmpleado)
        {
            Conexion Datos = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_lps_obtener_PorIdEmpleado ";
            sql += "'" + idEmpleado + "'";

            try
            {
                dataTable = Datos.ExecuteReader(sql).Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
            return dataTable;
        }

        public void ObtenerPorId(decimal idEmpleado, Int32 idLpsEmpleado, string reporteLPS, string reporteAcumulados)
        {
            Conexion Datos = new Conexion(Empresa);
            string sql = null;
            IdLpsEmpleado = idLpsEmpleado;
            IdEmpleado = idEmpleado;

            sql = "usp_lps_obtenerPorId ";
            sql += IdLpsEmpleado.ToString();

            try
            {
                ObtenerEmpleado(Datos);
                Cargar(Datos.ExecuteReader(sql));
                Generar(Datos, reporteLPS, Reportes.Lps);
                Generar(Datos, reporteAcumulados, Reportes.Acumulados);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        public void Preliquidar(decimal idEmpleado, DateTime fechaLiquidacion, string reporteLps, string reporteAcumulados)
        {
            Conexion Datos = new Conexion(Empresa);

            IdEmpleado = idEmpleado;
            FechaLiquidacion = fechaLiquidacion;
            Estado = Estados.Liquidado.ToString();
            TipoLps = Liquidaciones.PreLiquidacion.ToString();
            try
            {
                Datos.IniciarTransaccion();
                Generar(Datos);
                Generar(Datos, reporteLps, Reportes.Lps);
                Generar(Datos, reporteAcumulados, Reportes.Acumulados);
                Datos.AceptarTransaccion();
            }
            catch (Exception e)
            {
                Datos.DeshacerTransaccion();
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        public void Preliquidar(bool noLaboro, decimal idEmpleado, DateTime fechaLiquidacion, string reporteLps, string reporteAcumulados)
        {
            Conexion Datos = new Conexion(Empresa);

            IdEmpleado = idEmpleado;
            FechaLiquidacion = fechaLiquidacion;
            Estado = Estados.Liquidado.ToString();
            NoLaboro = noLaboro;
            TipoLps = Liquidaciones.PreLiquidacion.ToString();
            try
            {
                Datos.IniciarTransaccion();

                ObtenerEmpleado(Datos);
                CrearPeriodo(Datos);
                Adicionar(Datos);

                Generar(Datos, reporteLps, Reportes.Lps);
                Generar(Datos, reporteAcumulados, Reportes.Acumulados);
                Datos.AceptarTransaccion();
            }
            catch (Exception e)
            {
                Datos.DeshacerTransaccion();
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        public void Liquidar(decimal idEmpleado, DateTime fechaLiquidacion, string reporteLPS, string reporteAcumulados)
        {
            Conexion Datos = new Conexion(Empresa);

            IdEmpleado = idEmpleado;
            FechaLiquidacion = fechaLiquidacion;
            Estado = Estados.Liquidado.ToString();
            TipoLps = Liquidaciones.Liquidacion.ToString();
            try
            {
                Datos.IniciarTransaccion();
                Generar(Datos);
                Generar(Datos, reporteLPS, Reportes.Lps);
                Generar(Datos, reporteAcumulados, Reportes.Acumulados);
                Retirar(Datos);
                Datos.AceptarTransaccion();
            }
            catch (Exception e)
            {
                Datos.DeshacerTransaccion();
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        public void Liquidar(bool noLaboro, decimal idEmpleado, DateTime fechaLiquidacion, string reporteLPS, string reporteAcumulados)
        {
            Conexion Datos = new Conexion(Empresa);

            IdEmpleado = idEmpleado;
            FechaLiquidacion = fechaLiquidacion;
            NoLaboro = noLaboro;
            Estado = Estados.Liquidado.ToString();
            TipoLps = Liquidaciones.Liquidacion.ToString();
            try
            {
                Datos.IniciarTransaccion();
                ObtenerEmpleado(Datos);
                CrearPeriodo(Datos);
                Adicionar(Datos);
                Generar(Datos, reporteLPS, Reportes.Lps);
                Generar(Datos, reporteAcumulados, Reportes.Acumulados);
                Retirar(Datos);
                Datos.AceptarTransaccion();
            }
            catch (Exception e)
            {
                Datos.DeshacerTransaccion();
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        private void Generar(Conexion Datos)
        {
            string sql;

            ObtenerEmpleado(Datos);
            CrearPeriodo(Datos);

            if (Novedades.Rows.Count > 0) Adicionar(Datos, Novedades);
            ObtenerDiasPendientesPagar(Datos);
            Liquidar(Datos);

            if (TipoLps.Equals(Liquidaciones.Reliquidacion.ToString()))
            {
                sql = "usp_lps_reliquidacion ";
                sql += IdLpsEmpleado.ToString();
                sql += ", " + id_periodo.ToString();

            }
            else
            {
                sql = "usp_lps_liquidar_rlg ";
                sql += IdEmpleado.ToString();
                sql += ", '" + Formatear(FechaLiquidacion) + "'";
                sql += ", '" + IdPeriodo.ToString() + "'";
            }

            Cargar(Datos.ExecuteReaderConTransaccion(sql));
            Adicionar(Datos);
        }

        private void ObtenerEmpleado(Conexion Datos)
        {
            DataRow dataRow = null;
            string sql = null;

            sql = "usp_lps_nom_empleados_obtenerPorIdEmpleado " + IdEmpleado.ToString();
            dataRow = Datos.ExecuteReaderConTransaccion(sql).Tables[0].DefaultView.Table.Rows[0];
            if (dataRow != null)
            {
                if (!DBNull.Value.Equals(dataRow["ID_EMPRESA"])) IdEmpresa = Convert.ToDecimal(dataRow["ID_EMPRESA"]);
                if (!DBNull.Value.Equals(dataRow["ID_CIUDAD"])) IdCiudad = dataRow["ID_CIUDAD"].ToString();
                if (!DBNull.Value.Equals(dataRow["ID_CENTRO_C"])) IdCentroCosto = Convert.ToDecimal(dataRow["ID_CENTRO_C"]);
                if (!DBNull.Value.Equals(dataRow["ID_SUB_C"])) IdSubCentroCosto = Convert.ToDecimal(dataRow["ID_SUB_C"]);
            }
        }

        private void CrearPeriodo(Conexion Datos)
        {
            string sql = null;
            sql = "usp_nom_periodo_memo_adicionar_por_cobertura ";
            sql += IdEmpresa.ToString();
            sql += ", '" + IdCiudad.ToString();
            sql += "', " + IdCentroCosto.ToString();
            sql += ", " + IdSubCentroCosto.ToString();
            sql += ", " + "'" + Usuario;
            sql += "', 'S', 'LPS'";
            IdPeriodo = Convert.ToInt32(Datos.ExecuteScalar(sql));
        }

        private void Adicionar(Conexion Datos, DataTable novedades)
        {
            novedadNomina novedad = new novedadNomina(Empresa, Usuario);

            foreach (DataRow dataRow in novedades.Rows)
            {
                novedad.Adicionar(IdEmpleado, Convert.ToInt32(dataRow["id_concepto"]), IdPeriodo,
                    Convert.ToDecimal(dataRow["cantidad"]), Convert.ToDecimal(dataRow["valor"]), "L", "ACTIVO", Datos);
            }
        }

        private void ObtenerDiasPendientesPagar(Conexion Datos)
        {
            tools Tools = new tools();
            string sql = null;

            sql = "usp_lps_nom_periodo_obtenerDiasPendientesPagar ";
            sql += IdEmpleado.ToString() + ",";
            sql += "'" + Tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "'";
            DiasPendientesPagar = Convert.ToInt32(Datos.ExecuteScalar(sql));
        }

        private void Liquidar(Conexion Datos)
        {
            string sql = null;
            sql = "usp_liquidacion_nomina ";
            sql += IdEmpresa.ToString() + ", ";
            sql += "1, ";
            sql += "'" + Usuario + "', ";
            sql += "'" + IdPeriodo.ToString() + "', ";
            sql += "'L', ";
            sql += "'N', ";
            sql += "'N', ";
            sql += IdEmpleado.ToString() + ", ";
            sql += DiasPendientesPagar.ToString() + "";
            Datos.ExecuteNonQuery(sql);
        }

        private void Cargar(DataSet dataSet)
        {
            if (dataSet.Tables[0].Rows.Count > 0) Nomina = dataSet.Tables[0];
            if (dataSet.Tables[1].Rows.Count > 0) Cesantias = dataSet.Tables[1];
            if (dataSet.Tables[2].Rows.Count > 0) Primas = dataSet.Tables[2];
            if (dataSet.Tables[3].Rows.Count > 0) Vacaciones = dataSet.Tables[3];
            if (dataSet.Tables[5].Rows.Count > 0) Memorando = dataSet.Tables[5];

            if (dataSet.Tables[4].Rows.Count > 0) Cargar(dataSet.Tables[4].Rows[0]);
        }

        private void Cargar(DataRow dataRow)
        {
            try
            {
                if (!DBNull.Value.Equals(dataRow["id_periodo"])) IdPeriodo = Convert.ToInt32(dataRow["id_periodo"]);
            }
            catch (Exception e)
            {
            }

            try
            {
                if (!DBNull.Value.Equals(dataRow["id_lps_empleado"])) IdLpsEmpleado = Convert.ToInt32(dataRow["id_lps_empleado"]);
            }
            catch (Exception e)
            {
            }

            try
            {
                if (!DBNull.Value.Equals(dataRow["estado"])) Estado = dataRow["estado"].ToString();
            }
            catch (Exception e)
            {
            }

            try
            {
                if (!DBNull.Value.Equals(dataRow["no_laboro"])) NoLaboro = Convert.ToBoolean(dataRow["no_laboro"]);
            }
            catch (Exception e)
            {
            }

            try
            {
                if (!DBNull.Value.Equals(dataRow["tipo_lps"])) TipoLps = dataRow["tipo_lps"].ToString();
            }
            catch (Exception e)
            {
            }

            if (!DBNull.Value.Equals(dataRow["fecha_liquidacion"])) FechaLiquidacion = Convert.ToDateTime(dataRow["fecha_liquidacion"]);
            if (!DBNull.Value.Equals(dataRow["dias_vinculacion"])) DiasVinculacion = Convert.ToInt32(dataRow["dias_vinculacion"]);

            if (!DBNull.Value.Equals(dataRow["periodo_contable_cesantias"])) PeriodoContableCesantias = Convert.ToInt32(dataRow["periodo_contable_cesantias"]);
            if (!DBNull.Value.Equals(dataRow["acumulado_cesantias"])) AcumuladoCesantias = Convert.ToDecimal(dataRow["acumulado_cesantias"]);
            if (!DBNull.Value.Equals(dataRow["base_cesantias"])) BaseCesantias = Convert.ToDecimal(dataRow["base_cesantias"]);
            if (!DBNull.Value.Equals(dataRow["dias_liquidacion_cesantias"])) DiasLiquidacionCesantias = Convert.ToInt32(dataRow["dias_liquidacion_cesantias"]);
            if (!DBNull.Value.Equals(dataRow["liquidacion_cesantias"])) LiquidacionCesantias = Convert.ToDecimal(dataRow["liquidacion_cesantias"]);
            if (!DBNull.Value.Equals(dataRow["liquidacion_intereses_cesantias"])) LiquidacionInteresesCesantias = Convert.ToDecimal(dataRow["liquidacion_intereses_cesantias"]);

            if (!DBNull.Value.Equals(dataRow["periodo_contable_prima"])) PeriodoContablePrima = Convert.ToInt32(dataRow["periodo_contable_prima"]);
            if (!DBNull.Value.Equals(dataRow["acumulado_prima"])) AcumuladoPrima = Convert.ToDecimal(dataRow["acumulado_prima"]);
            if (!DBNull.Value.Equals(dataRow["base_prima"])) BasePrima = Convert.ToDecimal(dataRow["base_prima"]);
            if (!DBNull.Value.Equals(dataRow["dias_liquidacion_prima"])) DiasLiquidacionPrima = Convert.ToInt32(dataRow["dias_liquidacion_prima"]);
            if (!DBNull.Value.Equals(dataRow["liquidacion_prima"])) LiquidacionPrima = Convert.ToDecimal(dataRow["liquidacion_prima"]);

            if (!DBNull.Value.Equals(dataRow["periodo_contable_vacaciones"])) PeriodoContableVacaciones = Convert.ToInt32(dataRow["periodo_contable_vacaciones"]);
            if (!DBNull.Value.Equals(dataRow["acumulado_vacaciones"])) AcumuladoVacaciones = Convert.ToDecimal(dataRow["acumulado_vacaciones"]);
            if (!DBNull.Value.Equals(dataRow["base_vacaciones"])) BaseVacaciones = Convert.ToDecimal(dataRow["base_vacaciones"]);
            if (!DBNull.Value.Equals(dataRow["dias_liquidacion_vacaciones"])) DiasLiquidacionVacaciones = Convert.ToInt32(dataRow["dias_liquidacion_vacaciones"]);
            if (!DBNull.Value.Equals(dataRow["liquidacion_vacaciones"])) LiquidacionVacaciones = Convert.ToDecimal(dataRow["liquidacion_vacaciones"]);

            if (!DBNull.Value.Equals(dataRow["total_devengado_memo"])) TotalDevengadoMemo = Convert.ToDecimal(dataRow["total_devengado_memo"]);
            if (!DBNull.Value.Equals(dataRow["total_deducido_memo"])) TotalDeducidoMemo = Convert.ToDecimal(dataRow["total_deducido_memo"]);
            if (!DBNull.Value.Equals(dataRow["fecha"])) Fecha = Convert.ToDateTime(dataRow["fecha"]);


            TotalLiquidacion = LiquidacionCesantias + LiquidacionInteresesCesantias + LiquidacionPrima + LiquidacionVacaciones;
            NetoLiquidacion = (TotalLiquidacion + TotalDevengadoMemo) - TotalDeducidoMemo;
            TotalPagar = NetoLiquidacion - (TotalAnticipos + TotalCreditos);
        }

        private void Adicionar(Conexion Datos)
        {
            tools Tools = new tools();
            string sql = null;

            sql = "usp_lps_adicionar ";
            sql += IdEmpleado.ToString();
            sql += ", '" + TipoLps + "'";
            sql += ", " + IdPeriodo.ToString();
            sql += ", '" + Tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "'";
            sql += ", " + DiasVinculacion.ToString();

            sql += ", " + PeriodoContableCesantias.ToString();
            sql += ", " + PeriodoContablePrima.ToString();
            sql += ", " + PeriodoContablePrima.ToString();

            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(AcumuladoCesantias);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(AcumuladoPrima);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(AcumuladoVacaciones);

            sql += ", " + DiasLiquidacionCesantias.ToString();
            sql += ", " + DiasLiquidacionPrima.ToString();
            sql += ", " + DiasLiquidacionVacaciones.ToString();

            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(BaseCesantias);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(BasePrima);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(BaseVacaciones);

            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(LiquidacionCesantias);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(LiquidacionInteresesCesantias);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(LiquidacionPrima);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(LiquidacionVacaciones);

            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalLiquidacion);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalDevengadoMemo);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalDeducidoMemo);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(NetoLiquidacion);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalAnticipos);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalCreditos);
            sql += ", " + Tools.convierteComaEnPuntoParaDecimalesEnSQL(TotalPagar);
            sql += ", '" + Usuario + "'";
            sql += ", '" + Estado + "'";
            sql += ", " + NoLaboro;

            IdLpsEmpleado = Convert.ToInt32(Datos.ExecuteScalar(sql));
        }

        public void Eliminar()
        {
            Conexion Datos = new Conexion(Empresa);

            string sql = null;
            sql = "usp_lps_eliminar ";
            sql += IdLpsEmpleado.ToString();
            try
            {
                Datos.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        public void Eliminar(Int32 idLpsEmpleado)
        {
            Conexion Datos = new Conexion(Empresa);

            string sql = null;
            sql = "usp_lps_eliminar ";
            sql += idLpsEmpleado.ToString();
            try
            {
                Convert.ToInt32(Datos.ExecuteNonQuery(sql));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        private void Generar(Conexion Datos, string reporte, Reportes report)
        {
            reportDocument = new ReportDocument();

            string sql = null;

            switch (report)
            {
                case Reportes.Lps:
                    sql = "usp_lps_reporte ";
                    sql += IdLpsEmpleado.ToString();
                    sql += ", " + IdEmpresa;
                    break;
                case Reportes.Acumulados:
                    sql = "usp_lps_acumulados_porIdEmpleado ";
                    sql += IdEmpleado.ToString();
                    break;
            }
            reportDocument.Load(reporte);
            reportDocument.SetDataSource(Datos.ExecuteReaderConTransaccion(sql).Tables[0]);
            reportDocument.DataSourceConnections[0].SetConnection("192.168.16.252", "siser_v3", "sa", "Acceso2013");

            switch (report)
            {
                case Reportes.Lps:
                    Reporte = (MemoryStream)reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    break;
                case Reportes.Acumulados:
                    Acumulados = (MemoryStream)reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    break;
            }

            try
            {
                reportDocument.Dispose();
                reportDocument = null;
                reportDocument.Close();
            }
            catch
            {
            }
        }

        private string Formatear(DateTime fecha)
        {
            return fecha.Year.ToString() + (fecha.Month + 100).ToString().Substring(1, 2) + (fecha.Day + 100).ToString().Substring(1, 2);
        }

        public void Reliquidar(decimal idEmpleado, int idLpsEmpleado, DateTime fechaLiquidacion, string reporteLPS, string reporteAcumulados)
        {
            Conexion Datos = new Conexion(Empresa);
            string sql = null;

            IdEmpleado = idEmpleado;
            IdLpsEmpleado = idLpsEmpleado;
            FechaLiquidacion = fechaLiquidacion;
            Estado = Estados.Liquidado.ToString();
            TipoLps = Liquidaciones.Reliquidacion.ToString();
            try
            {
                Datos.IniciarTransaccion();
                Generar(Datos);
                Generar(Datos, reporteLPS, Reportes.Lps);
                Generar(Datos, reporteAcumulados, Reportes.Acumulados);
                Datos.AceptarTransaccion();
            }
            catch (Exception e)
            {
                Datos.DeshacerTransaccion();
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        public DataTable ObtenerPorUsuario(string usuLog)
        {
            Conexion Datos = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_liq_lps_empleado_obtenerPorUsuario ";
            sql += "'" + usuLog + "'";

            try
            {
                dataTable = Datos.ExecuteReader(sql).Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
            return dataTable;
        }

        public void Validar(string validados)
        {
            Conexion Datos = new Conexion(Empresa);
            String sql = null;

            sql = "usp_liq_lps_empleado_validar ";
            sql += "'" + validados + "'";

            try
            {
                Datos.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }

        }

        private void Retirar(Conexion Datos)
        {
            tools Tools = new tools();
            string sql = null;

            sql = "usp_lps_retirar_empleado ";
            sql += IdEmpleado.ToString();
            sql += " , '" + Tools.obtenerStringConFormatoFechaSQLServer(FechaLiquidacion) + "'";
            sql += " , '" + Usuario.ToString() + "'";
            Datos.ExecuteNonQuery(sql);
        }

        public void Activar(decimal idEmpleado)
        {
            Conexion Datos = new Conexion(Empresa);

            string sql = null;
            sql = "usp_lps_activar_empleado ";
            sql += idEmpleado.ToString();
            sql += " , '" + Usuario.ToString() + "'";

            try
            {
                Datos.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Datos.Desconectar();
            }
        }

        #endregion metodos
    }
}
