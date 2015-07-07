using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.seleccion;

namespace Brainsbits.LLB.contratacion
{
    public class ConceptosNominaEmpleado
    {
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _REGISTRO = 0;
        private int _CAN_PRE = 0;
        private Decimal _VAL_PRE = 0;
        private String _LIQ_Q_1 = null;
        private String _LIQ_Q_2 = null;
        private String _LIQ_Q_3 = null;
        private String _LIQ_Q_4 = null;
        private Decimal _ID_CONCEPTO = 0;
        private Decimal _ID_CLAUSULA = 0;


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

        public Decimal REGISTRO
        {
            get { return _REGISTRO; }
            set { _REGISTRO = value; }
        }

        public int CAN_PRE
        {
            get { return _CAN_PRE; }
            set { _CAN_PRE = value; }
        }

        public Decimal VAL_PRE
        {
            get { return _VAL_PRE; }
            set { _VAL_PRE = value; }
        }

        public String LIQ_Q_1
        {
            get { return _LIQ_Q_1; }
            set { _LIQ_Q_1 = value; }
        }

        public String LIQ_Q_2
        {
            get { return _LIQ_Q_2; }
            set { _LIQ_Q_2 = value; }
        }

        public String LIQ_Q_3
        {
            get { return _LIQ_Q_3; }
            set { _LIQ_Q_3 = value; }
        }

        public String LIQ_Q_4
        {
            get { return _LIQ_Q_4; }
            set { _LIQ_Q_4 = value; }
        }

        public Decimal ID_CONCEPTO
        {
            get { return _ID_CONCEPTO; }
            set { _ID_CONCEPTO = value; }
        }

        public Decimal ID_CLAUSULA
        {
            get { return _ID_CLAUSULA; }
            set { _ID_CLAUSULA = value; }
        }


        public ConceptosNominaEmpleado(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }


        public Decimal AdicionarNomConceptosEmpleados(int ID_EMPLEADO, int ID_CONCEPTO, int CAN_PRE, Decimal VAL_PRE, String LIQ_Q_1, String LIQ_Q_2)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_conceptos_empleados_adicionar ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_CONCEPTO != 0)
            {
                sql += ID_CONCEPTO + ", ";
                informacion += "ID_CONCEPTO = '" + ID_CONCEPTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONCEPTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (CAN_PRE != 0)
            {
                sql += CAN_PRE + ", ";
                informacion += "CAN_PRE = '" + CAN_PRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CAN_PRE no puede ser nulo\n";
                ejecutar = false;
            }
            if (VAL_PRE != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VAL_PRE) + ", ";
                informacion += "VAL_PRE = '" + VAL_PRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo VAL_PRE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LIQ_Q_1)))
            {
                sql += "'" + LIQ_Q_1 + "', ";
                informacion += "LIQ_Q_1= '" + LIQ_Q_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_1 no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LIQ_Q_2)))
            {
                sql += "'" + LIQ_Q_2 + "', ";
                informacion += "LIQ_Q_2= '" + LIQ_Q_2.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_2 no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_EMPLEADO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarNomConceptosEmpleados(int REGISTRO, int ID_EMPLEADO, int ID_CONCEPTO, int CAN_PRE, Decimal VAL_PRE, String LIQ_Q_1, String LIQ_Q_2)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_conceptos_empleados_actualizar ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_CONCEPTO != 0)
            {
                sql += ID_CONCEPTO + ", ";
                informacion += "ID_CONCEPTO = '" + ID_CONCEPTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONCEPTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (CAN_PRE != 0)
            {
                sql += CAN_PRE + ", ";
                informacion += "CAN_PRE = '" + CAN_PRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CAN_PRE no puede ser nulo\n";
                ejecutar = false;
            }
            if (VAL_PRE != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VAL_PRE) + ", ";
                informacion += "VAL_PRE = '" + VAL_PRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo VAL_PRE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LIQ_Q_1)))
            {
                sql += "'" + LIQ_Q_1 + "', ";
                informacion += "LIQ_Q_1= '" + LIQ_Q_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_1 no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LIQ_Q_2)))
            {
                sql += "'" + LIQ_Q_2 + "', ";
                informacion += "LIQ_Q_2= '" + LIQ_Q_2.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_2 no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";


            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerNomConceptosEmpleadosPorIdEmpleado(int ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_conceptos_empleados_obtener_por_id_empleado ";

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

            _dataTable.Columns.Add("PERIODOS_LIQUIDACION");

            DataRow fila;
            String PERIODOS_LIQUIDACION = "";
            int contador_periodos = 0;

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                fila = _dataTable.Rows[i];

                if (fila["LIQ_Q_1"].ToString() == "S")
                {
                    PERIODOS_LIQUIDACION = "1";
                    contador_periodos += 1;
                }

                if (fila["LIQ_Q_2"].ToString() == "S")
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 2";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "2";
                    }
                    contador_periodos += 1;
                }

                if (fila["LIQ_Q_3"].ToString() == "S")
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 3";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "3";
                    }
                    contador_periodos += 1;
                }

                if (fila["LIQ_Q_4"].ToString() == "S")
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 4";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "4";
                    }
                    contador_periodos += 1;
                }

                fila["PERIODOS_LIQUIDACION"] = PERIODOS_LIQUIDACION;
            }

            return _dataTable;
        }

        public DataTable ObtenerNomConceptosEmpleadosPorRegistro(int REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_conceptos_empleados_obtener_registro ";

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

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_EMPLEADO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerNomConceptosEmpleadosPorIdEmpleadoConceptosFijos(int ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_conceptos_empleados_obtener_por_id_empleado_conceptos_fijos ";

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

            _dataTable.Columns.Add("PERIODOS_LIQUIDACION");

            DataRow fila;
            String PERIODOS_LIQUIDACION = "";
            int contador_periodos = 0;

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                fila = _dataTable.Rows[i];

                if (fila["LIQ_Q_1"].ToString() == "S")
                {
                    PERIODOS_LIQUIDACION = "1";
                    contador_periodos += 1;
                }

                if (fila["LIQ_Q_2"].ToString() == "S")
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 2";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "2";
                    }
                    contador_periodos += 1;
                }

                if (fila["LIQ_Q_3"].ToString() == "S")
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 3";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "3";
                    }
                    contador_periodos += 1;
                }

                if (fila["LIQ_Q_4"].ToString() == "S")
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 4";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "4";
                    }
                    contador_periodos += 1;
                }

                fila["PERIODOS_LIQUIDACION"] = PERIODOS_LIQUIDACION;
            }

            return _dataTable;
        }

        public Boolean desactivarConceptosFijoDeEmpleado(Decimal REGISTRO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_conceptos_empleado_desactivar_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerConceptosFijosActivos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_conceptos_nomina_obtener_concetos_fijos_activos";
            informacion = "consulta todos los conceptos fijos activos";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_NOMINA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public Decimal AdicionarConceptoFijoAEmpleado(Decimal ID_EMPLEADO,
    Decimal ID_CONCEPTO,
    int CAN_PRE,
    Decimal VAL_PRE,
    String LIQ_Q_1_VALOR,
    String LIQ_Q_2_VALOR,
    String LIQ_Q_3_VALOR,
    String LIQ_Q_4_VALOR,
    Decimal ID_CLAUSULA,
    Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_conceptos_empleados_adicionar ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CONCEPTO != 0)
            {
                sql += ID_CONCEPTO + ", ";
                informacion += "ID_CONCEPTO = '" + ID_CONCEPTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONCEPTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (CAN_PRE != 0)
            {
                sql += CAN_PRE + ", ";
                informacion += "CAN_PRE = '" + CAN_PRE.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "CAN_PRE = '0', ";
            }

            if (VAL_PRE != 0)
            {
                sql += VAL_PRE + ", ";
                informacion += "VAL_PRE = '" + VAL_PRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo VAL_PRE no puede ser nulo\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(LIQ_Q_1_VALOR)))
            {
                sql += "'" + LIQ_Q_1_VALOR + "', ";
                informacion += "LIQ_Q_1 = '" + LIQ_Q_1_VALOR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_1 no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LIQ_Q_2_VALOR)))
            {
                sql += "'" + LIQ_Q_2_VALOR + "', ";
                informacion += "LIQ_Q_2 = '" + LIQ_Q_2_VALOR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_2 no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LIQ_Q_3_VALOR)))
            {
                sql += "'" + LIQ_Q_3_VALOR + "', ";
                informacion += "LIQ_Q_3 = '" + LIQ_Q_3_VALOR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_3 no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(LIQ_Q_4_VALOR)))
            {
                sql += "'" + LIQ_Q_4_VALOR + "', ";
                informacion += "LIQ_Q_4 = '" + LIQ_Q_4_VALOR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQ_Q_4 no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CLAUSULA != 0)
            {
                sql += ID_CLAUSULA + ", ";
                informacion += "ID_CLAUSULA = '" + ID_CLAUSULA.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CLAUSULA = 'null', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_EMPLEADO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        
        public Decimal ActualizarConceptosFijosEmpleado(Decimal ID_EMPLEADO, List<ConceptosNominaEmpleado> listaConceptos, Boolean ACTUALIZAR_ESTADO_PROCESO, Decimal ID_SOLICITUD)
        {
            Boolean ejecutar = true;

            if (ID_EMPLEADO == 0)
            {
                MensajeError = "El campo ID_EMPLEADO no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    DataTable tablaConceptosActuales = ObtenerNomConceptosEmpleadosPorIdEmpleadoConceptosFijos(Convert.ToInt32(ID_EMPLEADO));

                    foreach (DataRow fila in tablaConceptosActuales.Rows)
                    {
                        ejecutar = true;
                        foreach (ConceptosNominaEmpleado filaDeLaLista in listaConceptos)
                        {
                            if (filaDeLaLista.REGISTRO == Convert.ToDecimal(fila["REGISTRO"]))
                            {
                                ejecutar = false;
                                break;
                            }
                        }

                        if (ejecutar == true)
                        {
                            if (desactivarConceptosFijoDeEmpleado(Convert.ToDecimal(fila["REGISTRO"]), conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                return 0;
                            }
                        }
                    }

                    foreach (ConceptosNominaEmpleado filaDeLaLista in listaConceptos)
                    {
                        if (filaDeLaLista.REGISTRO == 0)
                        {
                            if (AdicionarConceptoFijoAEmpleado(ID_EMPLEADO, filaDeLaLista.ID_CONCEPTO, filaDeLaLista.CAN_PRE, filaDeLaLista.VAL_PRE, filaDeLaLista.LIQ_Q_1, filaDeLaLista.LIQ_Q_2, filaDeLaLista.LIQ_Q_3, filaDeLaLista.LIQ_Q_4, filaDeLaLista.ID_CLAUSULA, conexion) == 0)
                            {
                                conexion.DeshacerTransaccion();
                                return 0;
                            }
                        }
                    }

                    auditoriaContratos _auditoriaContratos = new auditoriaContratos(Empresa, Usuario);
                    Decimal ID_AUDITORIA = _auditoriaContratos.AdicionarAuditoriaContratos(ID_EMPLEADO, tabla.NOM_CONCEPTOS_EMPLEADO, 1, DateTime.Now, conexion);

                    if (ID_AUDITORIA <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = _auditoriaContratos.MensajeError;
                        return 0;
                    }

                    if (ACTUALIZAR_ESTADO_PROCESO == true)
                    {
                        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                        if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CONCEPTOS_FIJOS, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = _radicacionHojasDeVida.MensajeError;
                            return 0;
                        }
                    }

                    conexion.AceptarTransaccion();
                    return 1;
                }
                catch
                {
                    conexion.DeshacerTransaccion();
                    return 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                return 0;
            }
        }
    }
}

