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
    public class ControlPeriodos
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        decimal _id_control_periodo_anterior;
        decimal _id_periodo_pago;
        string _descripcion;
        int _periodo_mes;
        string _per_cont;
        DateTime _fecha_ini;
        DateTime _fecha_fin;
        int _dias_periodo;
        string _tipo;
        string _llave_periodo;
        DateTime _fecha_corte_auditoria;

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

        public decimal id_control_periodo_anterior
        {
            get { return _id_control_periodo_anterior; }
            set { _id_control_periodo_anterior = value; }
        }

        public decimal id_periodo_pago
        {
            get { return _id_periodo_pago; }
            set { _id_periodo_pago = value; }
        }

        public String descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public int periodo_mes
        {
            get { return _periodo_mes; }
            set { _periodo_mes = value; }
        }

        public String per_cont
        {
            get { return _per_cont; }
            set { _per_cont = value; }
        }

        public DateTime fecha_ini
        {
            get { return _fecha_ini; }
            set { _fecha_ini = value; }
        }

        public DateTime fecha_fin
        {
            get { return _fecha_fin; }
            set { _fecha_fin = value; }
        }

        public int dias_periodo
        {
            get { return _dias_periodo; }
            set { _dias_periodo = value; }
        }

        public String tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        
        public String llave_periodo
        {
            get { return _llave_periodo; }
            set { _llave_periodo = value; }
        }

        public DateTime fecha_corte_auditoria
        {
            get { return _fecha_corte_auditoria; }
            set { _fecha_corte_auditoria = value; }
        }
        #endregion propiedades

        #region constructores
        public ControlPeriodos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        public ControlPeriodos()
        {
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerEstadoPeriodosActuales()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_control_periodos_obtener_estados_actuales";

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

        private DataTable ObtenerEstructuraTablaDetallePeriodos()
        {
            DataTable tablaTemporal = new DataTable();
            tablaTemporal.Columns.Add("ID_CONTROL_PERIODO", typeof(decimal));
            tablaTemporal.Columns.Add("ID_PERIODO_PAGO", typeof(decimal));
            tablaTemporal.Columns.Add("DESCRIPCION", typeof(String));
            tablaTemporal.Columns.Add("PERIODO_MES", typeof(int));
            tablaTemporal.Columns.Add("PER_CONT", typeof(String));
            tablaTemporal.Columns.Add("FECHA_INI", typeof(DateTime));
            tablaTemporal.Columns.Add("FECHA_FIN", typeof(DateTime));
            tablaTemporal.Columns.Add("DIAS_PERIODO", typeof(int));
            tablaTemporal.Columns.Add("TIPO", typeof(String));
            tablaTemporal.Columns.Add("LLAVE_PERIODO", typeof(String));
            tablaTemporal.Columns.Add("FECHA_CORTE_AUDITORIA", typeof(DateTime));
            tablaTemporal.Columns.Add("ID_EMPRESA", typeof(decimal));
            tablaTemporal.Columns.Add("RAZ_SOCIAL", typeof(String));
            tablaTemporal.Columns.Add("NOM_CC", typeof(String));
            tablaTemporal.Columns.Add("PERIODO", typeof(int));
            tablaTemporal.Columns.Add("ESTADO_PERIODO", typeof(String));

            return tablaTemporal;
        }

        public DataTable ObtenerDetalleControlPeriodos(List<decimal> listaPeriodosSeleciconados)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            DataTable tablaFinal = ObtenerEstructuraTablaDetallePeriodos();
            try
            {
                foreach (decimal p in listaPeriodosSeleciconados)
                {
                    sql = "usp_nom_control_periodos_obtener_detalle_perido " + p.ToString();

                    _dataSet = new DataSet();
                    _dataView = new DataView();
                    _dataTable = new DataTable();

                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    if (_dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow filaTabla in _dataTable.Rows)
                        {
                            DataRow filaFinal = tablaFinal.NewRow();

                            filaFinal["ID_CONTROL_PERIODO"] = filaTabla["ID_CONTROL_PERIODO"];
                            filaFinal["ID_PERIODO_PAGO"] = filaTabla["ID_PERIODO_PAGO"];
                            filaFinal["DESCRIPCION"] = filaTabla["DESCRIPCION"];
                            filaFinal["PERIODO_MES"] = filaTabla["PERIODO_MES"];
                            filaFinal["PER_CONT"] = filaTabla["PER_CONT"];
                            filaFinal["FECHA_INI"] = filaTabla["FECHA_INI"];
                            filaFinal["FECHA_FIN"] = filaTabla["FECHA_FIN"];
                            filaFinal["DIAS_PERIODO"] = filaTabla["DIAS_PERIODO"];
                            filaFinal["TIPO"] = filaTabla["TIPO"];
                            filaFinal["LLAVE_PERIODO"] = filaTabla["LLAVE_PERIODO"];
                            filaFinal["FECHA_CORTE_AUDITORIA"] = filaTabla["FECHA_CORTE_AUDITORIA"];
                            filaFinal["ID_EMPRESA"] = filaTabla["ID_EMPRESA"];
                            filaFinal["RAZ_SOCIAL"] = filaTabla["RAZ_SOCIAL"];
                            filaFinal["NOM_CC"] = filaTabla["NOM_CC"];
                            filaFinal["PERIODO"] = filaTabla["PERIODO"];
                            filaFinal["ESTADO_PERIODO"] = filaTabla["ESTADO_PERIODO"];

                            tablaFinal.Rows.Add(filaFinal);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return tablaFinal;
        }


        private Boolean ActualizarRegistroControlPeriodos(decimal ID_CONTROL_PERIODO_ANTERIOR,
            int PERIODO_MES,
            string PER_CONT,
            DateTime FECHA_INI,
            DateTime FECHA_FIN,
            int DIAS_PERIODO,
            string TIPO,
            string LLAVE_PERIODO,
            DateTime FECHA_CORTE_AUDITORIA,
            decimal ID_PERIODO_PAGO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;
            tools _tools = new tools();

            sql = "usp_nom_control_periodos_actualizar_registro ";

            #region validaciones
            if (ID_CONTROL_PERIODO_ANTERIOR != 0)
            {
                sql += ID_CONTROL_PERIODO_ANTERIOR.ToString() + ", ";
                informacion += "ID_CONTROL_PERIODO_ANTERIOR = '" + ID_CONTROL_PERIODO_ANTERIOR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONTROL_PERIODO_ANTERIOR no puede ser cero.<br>";
                ejecutar = false;
            }

            if (PERIODO_MES != 0)
            {
                sql += PERIODO_MES.ToString() + ", ";
                informacion += "PERIODO_MES = '" + PERIODO_MES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PERIODO_MES no puede ser cero.<br>";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PER_CONT)))
            {
                sql += "'" + PER_CONT + "', ";
                informacion += "PER_CONT = '" + PER_CONT + "', ";
            }
            else
            {
                MensajeError += "El campo PER_CONT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INI) + "', ";
            informacion += "FECHA_INI = '" + FECHA_INI.ToShortDateString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN) + "', ";
            informacion += "FECHA_FIN = '" + FECHA_FIN.ToShortDateString() + "', ";

            if (DIAS_PERIODO != 0)
            {
                sql += DIAS_PERIODO.ToString() + ", ";
                informacion += "DIAS_PERIODO = '" + DIAS_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIAS_PERIODO no puede ser cero.<br>";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO + "', ";
                informacion += "TIPO = '" + PER_CONT + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LLAVE_PERIODO)))
            {
                sql += "'" + LLAVE_PERIODO + "', ";
                informacion += "LLAVE_PERIODO = '" + LLAVE_PERIODO + "', ";
            }
            else
            {
                MensajeError += "El campo LLAVE_PERIODO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CORTE_AUDITORIA) + "', ";
            informacion += "FECHA_CORTE_AUDITORIA = '" + FECHA_CORTE_AUDITORIA.ToShortDateString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario + "', ";

            if (ID_PERIODO_PAGO != 0)
            {
                sql += ID_PERIODO_PAGO.ToString();
                informacion += "ID_PERIODO_PAGO = '" + ID_PERIODO_PAGO.ToString();
            }
            else
            {
                MensajeError += "El campo ID_PERIODO_PAGO no puede ser cero.<br>";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region actualizarInsertar
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        ejecutadoCorrectamente = false;
                    }
                    #endregion actualizarInsertar

                    #region auditoria
                    if (ejecutadoCorrectamente == true)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.NOM_CONTROL_PERIODOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                        }
                    }
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ActualizarControlPeriodos(List<ControlPeriodos> listaPeriodosNuevos)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                foreach (ControlPeriodos p in listaPeriodosNuevos)
                {
                    if (ActualizarRegistroControlPeriodos(p.id_control_periodo_anterior, p.periodo_mes, p.per_cont, p.fecha_ini, p.fecha_fin, p.dias_periodo, p.tipo, p.llave_periodo, p.fecha_corte_auditoria, p.id_periodo_pago, conexion) == false)
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
            catch (Exception e)
            {
                conexion.DeshacerTransaccion();
                correcto = false;
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }
        #endregion metodos

        #region reglas
        #endregion reglas
    }
}
