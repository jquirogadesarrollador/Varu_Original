using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.contratacion
{
    public class ControlPersonalNomina
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
        public ControlPersonalNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        public ControlPersonalNomina()
        {

        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerEmpleadosActivosSinAuditarDeUnaEmpresa(Decimal idEmpresa)
        {
            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Conexion conexion = new Conexion(Empresa);

            sql = "usp_nom_empleados_activos_sin_auditar_por_id_empresa_nomina ";
            sql += idEmpresa;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
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

        public DataTable ObtenerFechasPeriodosActivosPorIdEmpresa(Decimal idEmpresa)
        {
            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Conexion conexion = new Conexion(Empresa);

            sql = "usp_nom_periodo_activos_por_id_empresa_nomina ";
            sql += idEmpresa;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
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

        public DataTable ObtenerEmpleadosEntraNomina(Decimal idEmpresa, DateTime fechaIni, DateTime fechaFin)
        {
            tools _tools = new tools();

            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Conexion conexion = new Conexion(Empresa);

            sql = "usp_nom_obtener_personal_para_nomina_control_personal_por_id_empresa_y_periodo ";
            sql += idEmpresa + ", ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaIni) + "',";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaFin) + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
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

        public DataTable ObtenerEmpleadosEntraNomina1(Decimal idEmpresa, DateTime fechaIni, DateTime fechaFin)
        {
            tools _tools = new tools();

            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Conexion conexion = new Conexion(Empresa);

            sql = "usp_nom_obtener_personal_para_nomina_control_personal_por_id_empresa_y_periodo_2 ";
            sql += idEmpresa + ", ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaIni) + "',";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaFin) + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
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


        public Boolean ActualizarEntraNominaConRegContratos(Decimal idEmpleado,
            Boolean entraNomina)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_contratos_actualiza_entra_nomina_por_id_empleado ";

            sql += idEmpleado + ", ";

            if (entraNomina == true)
            {
                sql += "'true'";
            }
            else
            {
                sql += "'false'";
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarEntraNominaConRegContratos1(Decimal idEmpleado,
            Boolean entraNomina,
            DateTime fechaIni,
            DateTime fechaFin,
            String controlPeriodo)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_contratos_actualiza_entra_nomina_por_id_empleado_2 ";

            sql += idEmpleado + ", ";

            if (entraNomina == true)
            {
                sql += "'true', ";
            }
            else
            {
                sql += "'false', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaIni) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaFin) + "', ";

            sql += "'" + controlPeriodo + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public DataTable ObtenerPlanillaConCreacionDePeriodos(Decimal idEmpresa)
        {
            tools _tools = new tools();

            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Conexion conexion = new Conexion(Empresa);

            sql = "usp_reporte_nomina_planilla_porIdEmpresa ";
            sql += idEmpresa;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
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

        #endregion
    }
}
