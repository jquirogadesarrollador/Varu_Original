using System;

using System.Data;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.comercial;

namespace Brainsbits.LLB.nomina
{
    public class novedadMemo
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private DataTable _dataTable;
        private String _registro = null;
        private String _numeroDocumento = null;
        private String _nombre = null;
        private String _codigoConcepto = null;
        private String _cantidad = null;
        private String _valor = null;
        private Decimal _idEmpresa = 0;
        private Decimal _idEmpleado = 0;
        private Decimal _idConcepto = 0;
        private String _idCiudad = null;
        private Decimal _idCentroCosto = 0;
        private Decimal _idSubCentroCosto = 0;
        private Decimal _idPeriodo = 0;
        private String _periodoContable = null;
        private Int32 _periodo = 0;
        private DateTime _fechaInicial = new DateTime();
        private DateTime _fechaFinal = new DateTime();
        private Int32 _IdParametroModeloNomina = 0;
        private Int32 _diasPagoNomina = 0;
        private DateTime _fechaInicioPrimerPeriodoNomina = new DateTime();

        private Guid _codigoUnico;

        private enum estado
        {
            ACTIVO = 0,
            LIQUIDACION = 1
        }

        private enum origen
        {
            I = 0,
            M = 1
        }

        private enum estadoEmpleado
        {
            Si = 0,
            No = 1
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

        #endregion propiedades

        #region constructores
        public novedadMemo(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;

            _codigoUnico = Guid.NewGuid();
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodos(Int32 ID_PERIODO, String ESTADO, String PeriodosProceso)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_memos_novedades_obtener_todos ";

            #region validaciones
            sql += ID_PERIODO.ToString() + " ";
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += ",'" + ESTADO.ToString().Trim() + "'";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += ", '" + PeriodosProceso + "'";

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


        public DataTable ObtenerPorPeriodo(Int32 ID_EMPRESA, Int32 PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_memos_novedades_obtener_todos_regnovedades ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString().Trim();
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += ", " + PERIODO.ToString().Trim() + "";
            }
            else
            {
                MensajeError += "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }
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

        public DataTable ObtenerPorId(Int32 REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_memos_novedades_obtener_registro ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo REGISTRO es requerido para la consulta.";
                ejecutar = false;
            }
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

        public Decimal Adicionar(Decimal ID_EMPLEADO, Int32 ID_CONCEPTO, Int32 ID_PERIODO, Decimal CANTIDAD, Decimal VALOR, String ORIGEN, String ESTADO, Conexion conexion, Int32 NUM_PERIODO, String NUEVO, DateTime FECHA)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_memos_novedades_adicionar  ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO.ToString() + "', ";
                informacion += "ID_CONCEPTO= '" + ID_CONCEPTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + NUM_PERIODO.ToString() + "', ";
            informacion += "NUM_PERIODO= '" + NUM_PERIODO.ToString() + ", ";
            sql += "'" + CANTIDAD.ToString().Replace(",", ".") + "', ";
            informacion += "CANTIDAD= '" + CANTIDAD.ToString().Replace(",", ".") + ", ";
            sql += "'" + VALOR.ToString().Replace(",", ".") + "', ";
            informacion += "VALOR= '" + VALOR.ToString() + ", ";

            if (!(String.IsNullOrEmpty(ORIGEN)))
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN = '" + ORIGEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ORIGEN no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUEVO)))
            {
                sql += "'" + NUEVO + "', ";
                informacion += "NUEVO = '" + NUEVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUEVO no puede ser nulo\n";
                ejecutar = false;
            }

            tools _tools = new tools();
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + FECHA.ToString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            sql += "''"; 

            #endregion

            if (ejecutar)
            {
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_NOMINA_NOVEDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            if (!(String.IsNullOrEmpty(idRecuperado))) return Convert.ToDecimal(idRecuperado);
            else return 0;
        }

        public Decimal Adicionar(Decimal ID_EMPLEADO,
            Int32 ID_CONCEPTO,
            Int32 PERIODO,
            String CANTIDAD,
            String VALOR,
            String ORIGEN,
            String ESTADO,
            String NUEVO,
            DateTime FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_nom_memos_novedades_adicionar  ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO.ToString() + "', ";
                informacion += "ID_CONCEPTO= '" + ID_CONCEPTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PERIODO.ToString() + "', ";
            informacion += "PERIODO= '" + PERIODO.ToString() + ", ";

            sql += CANTIDAD.Replace(',', '.') + ", ";
            informacion += "CANTIDAD = '" + CANTIDAD.Replace(',', '.') + ", ";

            sql += VALOR.Replace(',', '.') + ", ";
            informacion += "VALOR = '" + VALOR.Replace(',', '.') + ", ";

            if (!(String.IsNullOrEmpty(ORIGEN)))
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN = '" + ORIGEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ORIGEN no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUEVO)))
            {
                sql += "'" + NUEVO + "', ";
                informacion += "_nuevo = '" + NUEVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUEVO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + FECHA.ToString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            sql += "''";
            #endregion

            if (ejecutar)
            {
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_NOMINA_NOVEDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            if (!(String.IsNullOrEmpty(idRecuperado))) return Convert.ToDecimal(idRecuperado);
            else return 0;
        }

        public DataTable Adicionar(StreamReader streamReader, Int32 PERIODO)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            String sql = null;

            if (ValidarEstructura(streamReader))
            {
                if (ValidarInformacion(streamReader))
                {
                    Conexion conexion = new Conexion(Empresa);
                    conexion.IniciarTransaccion();
                    foreach (DataRow dataRow in _dataTable.Rows)
                    {
                        sql = "usp_nom_memos_novedades_adicionar ";
                        sql += dataRow["idEmpleado"].ToString() + ",";
                        sql += "'" + dataRow["idConcepto"].ToString() + "',";
                        sql += "'" + dataRow["idConcepto"].ToString() + "',";

                        dataRow["numeroDocumento"].ToString();
                        dataRow["nombre"].ToString();
                        dataRow["codigoConcepto"].ToString();
                        dataRow["cantidad"].ToString();
                        dataRow["valor"].ToString();
                        dataRow["inconsistencia"].ToString();
                    }
                }
            }
            if (_dataTable.Rows.Count > 0) MensajeError = "Existen inconsistencias favor revisar";
            else MensajeError = String.Empty;

            return _dataTable;
        }

        public Boolean Actualizar(Int32 REGISTRO, Int32 ID_EMPLEADO, Int32 ID_CONCEPTO, Decimal CANTIDAD, Decimal VALOR, DateTime FECHA, String OBSERVACIONES)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_nom_memos_novedades_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += "'" + REGISTRO.ToString() + "', ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO.ToString() + "', ";
                informacion += "ID_CONCEPTO= '" + ID_CONCEPTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }
            if (CANTIDAD != 0)
            {
                sql += "'" + CANTIDAD.ToString() + "', ";
                informacion += "CANTIDAD= '" + CANTIDAD.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo CANTIDAD es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + VALOR.ToString() + "', ";
            informacion += "VALOR= '" + VALOR.ToString() + ", ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + FECHA.ToString() + "', ";

            sql += "'" + OBSERVACIONES + "', ";
            informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_MEMOS_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean Actualizar(Int32 REGISTRO, String ESTADO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_memos_novedades_actualizar_estado ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += "'" + REGISTRO.ToString() + "', ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO es requerido para la consulta.";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_MEMOS_NOVEDADES, tabla.ACCION_ANULAR, sql, informacion, conexion);
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

        public Decimal EliminarNovedadPorId(Int32 REGISTRO)
        {
            String sql = null;
            Decimal rowsDeleted = 0;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_memos_novedades_eliminar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += "'" + REGISTRO.ToString() + "' ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO es requerido para la eliminacion.";
                ejecutar = false;
            }

            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    rowsDeleted = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_NOMINA_NOVEDADES, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
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
            return rowsDeleted;
        }

        public DataTable ObtenerEmpresaConPeriodo(Int32 ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_memos_empresa_periodo ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA.ToString() + "' ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += ", '" + Usuario.ToString() + "'";


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


        public DataTable ObtenerDatosPeriodo(Int32 ID_EMPRESA, Int32 PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_datos_periodo ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += "'" + PERIODO.ToString() + "' ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += ", '" + Usuario.ToString() + "'";


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


        public DataTable ObtenerPeriodosMemorandos(Int32 ID_EMPRESA, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_periodos_memorandos ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la eliminacion.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario.ToString() + "'";


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



        public DataTable ObtenerDatosPeriodoNuevo(Int32 ID_EMPRESA, Int32 ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_periodos_memorandos_nuevo ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

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

        public Int32 EliminarPeriodoMemorando(Int32 ID_EMPRESA, Int32 PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Int32 _rowsDeleted = 0;
            Boolean ejecutar = true;
            String informacion = null;

            sql = "usp_elimnar_periodo_memorando ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA.ToString() + "', ";
                informacion = "ID_EMPRESA= '" + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la eliminacion.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += "'" + PERIODO.ToString() + "' ";
                informacion += "PERIODO= '" + PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la eliminacion.";
                ejecutar = false;
            }

            sql += ", '" + Usuario.ToString() + "'";
            informacion += "USUARIO= '" + Usuario.ToString() + "'";

            #endregion


            if (ejecutar == true)
            {
                try
                {
                    _rowsDeleted = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
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
            return _rowsDeleted;
        }

        public DataTable ObtenerPeriodoMemoEmpleado(Decimal ID_EMPRESA,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            Decimal ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_novedades_memos_trae_periodo_empleado ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += "'" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)) | (ID_CENTRO_C > 0) | (ID_SUB_C > 0))
            {
                {
                    if (!(String.IsNullOrEmpty(ID_CIUDAD)))
                        sql += "'" + ID_CIUDAD.ToString() + "', ";
                    else
                        sql += "'', ";

                    sql += "'" + ID_CENTRO_C.ToString() + "', ";
                    sql += "'" + ID_SUB_C.ToString() + "', ";
                }
            }
            else
            {
                MensajeError = "los campos ID_CIUDAD o ID_CENTRO_C o ID_SUB_C son requeridos para esta consulta";
                ejecutar = false;
            }


            if (ID_EMPLEADO > 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";

            #endregion

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


        public DataRow ObtenerPeriodoNomina(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C)
        {
            periodoNomina _periodoNomina = new periodoNomina(Empresa, Usuario);
            DataTable _dataTablePeriodoNomina = new DataTable();
            DataRow _dataRowPeriodoNomina = null;

            _dataTablePeriodoNomina = _periodoNomina.ObtenerPeriodoMemorando(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C);
            if (String.IsNullOrEmpty(_periodoNomina.MensajeError))
            {
                if (_dataTablePeriodoNomina.Rows.Count > 0)
                {
                    _dataRowPeriodoNomina = _dataTablePeriodoNomina.Rows[0];
                    if (_dataRowPeriodoNomina != null)
                    {
                        _periodo = Convert.ToInt32(_dataRowPeriodoNomina["PERIODO"].ToString());
                        _periodoContable = _dataRowPeriodoNomina["PER_CONT"].ToString();
                        _fechaInicial = Convert.ToDateTime(_dataRowPeriodoNomina["FECHA_INI"].ToString());
                        _fechaFinal = Convert.ToDateTime(_dataRowPeriodoNomina["FECHA_FIN"].ToString());
                        _idPeriodo = Convert.ToDecimal(_dataRowPeriodoNomina["ID_PERIODO"].ToString());
                    }
                }
                else MensajeError = "No existe un período de nomina con las características del trabajador";
            }
            if (_dataTablePeriodoNomina != null) _dataTablePeriodoNomina.Dispose();
            return _dataRowPeriodoNomina;
        }

        public DataTable ObtenerContratoEmpleadoPorNumeroIdentificacion(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_contrato_empleado_por_num_identidad ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
                ejecutar = false;
            }


            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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


        public DataTable ObtenerEmpleadoPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleado_obtener_por_id_empleado_para_memorandos ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO.ToString();
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ejecutar)
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


        public DataTable ObtenerContratoEmpleadoPorRegistro(Int32 REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_contrato_empleado_por_registro_contrato ";

            if (REGISTRO > 0)
            {
                sql += REGISTRO.ToString() + "";
                informacion += "ID_EMPLEADO = " + REGISTRO + "";
            }
            else
            {
                MensajeError += "El campo REGISTRO debe swer mayor que Cero. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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


        #endregion metodos

        #region importar





        public DataTable Adicionar(string idEmpresa, int periodo, DateTime fechaPeriodo, string usuario, string nombre, string numeroDocumento, string codigoConcepto, string cantidad, string valor, Conexion conexion)
        {
            MensajeError = null;

            string sql = null;

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            tools _t = new tools();

            sql = "usp_tmp_nomina_novedades_adicionar_memo_v1 ";
            sql += "'" + idEmpresa.ToString() + "',";
            sql += "'" + usuario.ToString() + "',";
            sql += "'" + nombre.ToString() + "',";
            sql += "'" + numeroDocumento.ToString() + "',";
            sql += "'" + codigoConcepto.ToString() + "',";
            sql += "'" + cantidad.ToString() + "',";
            sql += "'" + valor.ToString() + "', ";
            sql += "'" + _codigoUnico.ToString() + "', ";
            sql += "'" + periodo.ToString() + "', ";
            sql += "'" + _t.obtenerStringConFormatoFechaSQLServer(fechaPeriodo) + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                MensajeError = "Error originado en capa de datos. " + e.Message;
            }

            return _dataTable;
        }




        private void Adicionar(string numeroDocumento, string nombre, string codigoConcepto, string cantidad, string valor, string inconsistencia)
        {
            if (numeroDocumento == "79040803" || numeroDocumento == "79568105" || numeroDocumento == "75084239")
            {
                int wert = 0;
            }

            DataRow dataRow = _dataTable.NewRow();
            dataRow["numeroDocumento"] = numeroDocumento;
            dataRow["nombre"] = nombre;
            dataRow["codigoConcepto"] = codigoConcepto;
            dataRow["cantidad"] = cantidad;
            dataRow["valor"] = valor;
            dataRow["inconsistencia"] = inconsistencia;
            _dataTable.Rows.Add(dataRow);
            _dataTable.AcceptChanges();
        }



        private DataTable Procesar(string idEmpresa, string usuario, Conexion conexion)
        {
            MensajeError = null;

            string sql = null;
            DataSet dataSet = new DataSet();
            try
            {
                sql = "usp_tmp_nomina_novedades_procesar_V2 ";
                sql += "'" + idEmpresa.ToString() + "',";
                sql += "'" + usuario.ToString() + "', ";
                sql += "'" + _codigoUnico.ToString() + "'";
                dataSet = conexion.ExecuteReader(sql);
            }
            catch (Exception e)
            {
                MensajeError = "Error originado en capa de datos. " + e.Message;
            }

            return dataSet.Tables[0].DefaultView.Table;
        }

        private void Cargar(StreamReader streamReader, string idEmpresa, int periodo, DateTime fechaPeriodo)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            DataTable dataTable = new DataTable();
            int _int;
            Int32 registro = 0;
            Decimal _decimal;
            String linea = "";
            char[] delimitadores = { ';' };
            Conexion conexion = new Conexion(Empresa);


            MensajeError = null;

            try
            {

                while (linea != null)
                {
                    linea = streamReader.ReadLine();
                    registro++;
                    if (linea != null)
                    {
                        arrayList.Add(linea);
                        string[] datos = linea.Split(delimitadores);
                        if (datos.Length == 5)
                        {

                            if (Decimal.TryParse(datos[1].ToString(), out _decimal))
                            {
                                if (Int32.TryParse(datos[2].ToString(), out _int))
                                {
                                    if (Decimal.TryParse(datos[3].ToString().Replace('.', ','), out _decimal))
                                    {
                                        if (Decimal.TryParse(datos[4].ToString().Replace('.', ','), out _decimal))
                                        {
                                            DataTable tablaResultado = Adicionar(idEmpresa, periodo, fechaPeriodo, Usuario, datos[0].ToString(), datos[1].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), conexion);

                                            if (tablaResultado.Rows.Count <= 0)
                                            {
                                                if (String.IsNullOrEmpty(MensajeError) == true)
                                                {
                                                    Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: No se controló el error en el cargue temporal de la novedad, consulte al administrador del sistema.");
                                                }
                                                else
                                                {
                                                    Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: " + MensajeError);
                                                }
                                            }
                                            else
                                            {
                                                DataRow filaResultado = tablaResultado.Rows[0];

                                                if (filaResultado["TIPO"].ToString().ToUpper() == "ERROR")
                                                {
                                                    Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), filaResultado["MENSAJE"].ToString());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: El valor no puede estar vacio, sino existe debe estar en cero(0)");
                                        }
                                    }
                                    else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: La cantidad no puede estar vacia, sino existe debe estar en cero(0)");
                                }
                                else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: El concepto no es númerico");
                            }
                            else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: El número de documento no es númerico");
                        }
                        else Adicionar(datos[0].ToString(), "", "", "", "", "Inconsistencia: El registro " + registro.ToString() + ", no tienen la cantidad de columnas requerida, por favor validar que los campos en el archivo plano esten separados por (;).");
                    }
                }

                dataTable = Procesar(idEmpresa, Usuario, conexion);

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

        public DataTable ObtenerUltimoPeriodoMemoEmpresa(Decimal idEmpresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_periodo_obtener_ultimo_periodo_memo_empresa_v2 " + idEmpresa.ToString();

            if (ejecutar)
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


        public DataTable Importar(StreamReader streamReader, Decimal ID_EMPRESA, Int32 PERIODO, String CREAR, DateTime FECHA)
        {
            Configurar();

            if (CREAR.ToUpper() == "S")
            {
                DataTable tablaUltimoPeriodo = ObtenerUltimoPeriodoMemoEmpresa(ID_EMPRESA);
                try
                {
                    DataRow filaUltimoPeriodo = tablaUltimoPeriodo.Rows[0];

                    PERIODO = Convert.ToInt32(filaUltimoPeriodo["PERIODO_ATUAL"]) - 1;
                    MensajeError = null;
                }
                catch
                {
                    MensajeError = "No se pudo determinar el periodo actual de memo para la empresa seleccionada.";
                }
            }

            if (String.IsNullOrEmpty(MensajeError) == true)
            {
                Cargar(streamReader, ID_EMPRESA.ToString(), PERIODO, FECHA);

                if (_dataTable.Rows.Count > 0)
                {
                    _dataTable.DefaultView.Sort = "nombre, codigoConcepto ASC";
                    MensajeError = "El cargue de novedades presento inconsistencias, corregir e intentar nuevamente.";
                }
                else MensajeError = "El proceso de cargue de novedades finalizao satisfactoriamente.";
            }
            return _dataTable;
        }

        private DataTable Limpiar(DataTable dataTable)
        {
            foreach (DataRow _dataRow in dataTable.Rows)
            {
                if (String.IsNullOrEmpty(_dataRow["Inconsistencia"].ToString())) _dataRow.Delete();
            }
            return dataTable;
        }
        #endregion importar

        #region reglas
        private void Configurar()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("registro");
            _dataTable.Columns.Add("numeroDocumento");
            _dataTable.Columns.Add("nombre");
            _dataTable.Columns.Add("codigoConcepto");
            _dataTable.Columns.Add("cantidad");
            _dataTable.Columns.Add("valor");
            _dataTable.Columns.Add("inconsistencia");
            _dataTable.Columns.Add("idEmpleado");
            _dataTable.Columns.Add("idConcepto");
            _dataTable.Columns.Add("idEmpresa");
            _dataTable.Columns.Add("idCiudad");
            _dataTable.Columns.Add("idPeriodo");
            _dataTable.Columns.Add("periodoContable");
            _dataTable.Columns.Add("periodo");
            _dataTable.Columns.Add("idCentorCosto");
            _dataTable.Columns.Add("idSubCentorCosto");
        }

        private Boolean ValidarEstructura(StreamReader streamReader)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            int _int;
            Int32 registro = 0;
            Decimal _decimal;
            String linea = "";
            char[] delimitadores = { ';' };

            while (linea != null)
            {
                linea = streamReader.ReadLine();
                registro++;
                if (linea != null)
                {
                    arrayList.Add(linea);
                    string[] datos = linea.Split(delimitadores);
                    _registro = registro.ToString();
                    if (datos.Length == 5)
                    {
                        _nombre = datos[0].ToString();
                        _numeroDocumento = datos[1].ToString();
                        _codigoConcepto = datos[2].ToString();
                        _cantidad = datos[3].ToString();
                        _valor = datos[4].ToString();

                        if (!Decimal.TryParse(datos[1].ToString(), out _decimal)) Adicionar("Inconsistencia: El número de documento no es númerico");
                        if (!Int32.TryParse(datos[2].ToString(), out _int)) Adicionar("Inconsistencia: El concepto no es númerico");
                        if (!Decimal.TryParse(datos[3].ToString(), out _decimal)) Adicionar("Inconsistencia: La cantidad no es númerica");
                        if (!Int32.TryParse(datos[4].ToString(), out _int)) Adicionar("Inconsistencia: El valor no es númerico");

                        Limpiar();
                    }
                    else Adicionar("Inconsistencia: El registro " + registro.ToString() + ", no tienen la cantidad de columnas requerida");
                }
            }
            if (_dataTable.Rows.Count == 0) return true;
            else return false;
        }

        private void Limpiar()
        {
            _registro = String.Empty;
            _nombre = String.Empty;
            _numeroDocumento = String.Empty;
            _codigoConcepto = String.Empty;
            _cantidad = String.Empty;
            _valor = String.Empty;
            _idEmpleado = 0;
            _idConcepto = 0;
            _idCiudad = String.Empty;
            _idCentroCosto = 0;
            _idSubCentroCosto = 0;
            _idPeriodo = 0;
            _periodoContable = String.Empty;
            _periodo = 0;
        }

        private Boolean ValidarInformacion(StreamReader streamReader)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            Int32 registro = 0;
            String linea = "";
            char[] delimitadores = { ';' };
            string[] datos;
            while (linea != null)
            {
                linea = streamReader.ReadLine();
                registro++;
                if (linea != null)
                {
                    arrayList.Add(linea);
                    datos = linea.Split(delimitadores);
                    _registro = registro.ToString();
                    _nombre = datos[0].ToString();
                    _numeroDocumento = datos[1].ToString();
                    _codigoConcepto = datos[2].ToString();
                    _cantidad = datos[3].ToString();
                    _valor = datos[4].ToString();

                    if (ValidarNumeroDocumento(datos[1].ToString()))
                    {
                        DataRow _dataRow = ObtenerEmpleado(datos[1].ToString());
                        if (_dataRow != null)
                        {
                            _idEmpleado = !String.IsNullOrEmpty(_dataRow["ID_EMPLEADO"].ToString()) ? Convert.ToDecimal(_dataRow["ID_EMPLEADO"]) : 0;

                            if (Convert.ToDecimal(_dataRow["ID_EMPRESA"].ToString()) == _idEmpresa)
                            {
                                DataRow _dataRowModeloNomina = null;
                                _dataRowModeloNomina = ObtenerModeloNomina(_idEmpresa, _idCiudad, _idCentroCosto, _idSubCentroCosto);
                                _idConcepto = ObtenerIdConcepto(datos[2].ToString());
                                if (_idConcepto != 0)
                                {
                                    Adicionar();
                                }
                                else Adicionar("Inconsistencia: El Concepto NO existe.");
                            }
                            else Adicionar("Inconsistencia: El trabajador NO pertenece a la empresa seleccionada");
                        }
                        else Adicionar("Inconsistencia: El trabajador NO tiene un contrato activo o tiene dos o mas activos.");
                    }
                    else Adicionar("Inconsistencia: El número de documento, NO se encuentra registrado");
                }
                Limpiar();
            }

            DataRow[] _dataRowInconsistencias = _dataTable.Select("inconsistencia LIKE '%Inconsistencia%' ");
            if (_dataRowInconsistencias.Length == 0) return true;
            else return false;
        }

        private void Adicionar(String inconsistencia)
        {
            DataRow _dataRow = _dataTable.NewRow();
            _dataRow["registro"] = _registro;
            _dataRow["numeroDocumento"] = _numeroDocumento;
            _dataRow["nombre"] = _nombre;
            _dataRow["codigoConcepto"] = _codigoConcepto;
            _dataRow["cantidad"] = _cantidad;
            _dataRow["valor"] = _valor;
            _dataRow["idEmpresa"] = _idEmpresa;
            _dataRow["idEmpleado"] = _idEmpleado;
            _dataRow["idConcepto"] = _idConcepto;
            _dataRow["idCiudad"] = _idCiudad;
            _dataRow["idCentorCosto"] = _idCentroCosto;
            _dataRow["idSubCentorCosto"] = _idSubCentroCosto;
            _dataRow["idPeriodo"] = _idPeriodo;
            _dataRow["periodoContable"] = _periodoContable;
            _dataRow["periodo"] = _periodo;
            _dataRow["inconsistencia"] = inconsistencia;
            _dataTable.Rows.Add(_dataRow);
            _dataTable.AcceptChanges();
        }

        private void Adicionar()
        {
            DataRow _dataRow = _dataTable.NewRow();
            _dataRow["registro"] = _registro;
            _dataRow["numeroDocumento"] = _numeroDocumento;
            _dataRow["nombre"] = _nombre;
            _dataRow["codigoConcepto"] = _codigoConcepto;
            _dataRow["cantidad"] = _cantidad;
            _dataRow["valor"] = _valor;
            _dataRow["idEmpleado"] = _idEmpleado;
            _dataRow["idConcepto"] = _idConcepto;
            _dataRow["idEmpresa"] = _idEmpresa;
            _dataRow["idCiudad"] = _idCiudad;
            _dataRow["idCentorCosto"] = _idCentroCosto;
            _dataRow["idSubCentorCosto"] = _idSubCentroCosto;
            _dataRow["idPeriodo"] = _idPeriodo;
            _dataRow["periodo"] = _periodo;
            _dataTable.Rows.Add(_dataRow);
            _dataTable.AcceptChanges();
        }

        public Boolean ValidarNumeroDocumento(String numeroDocumento)
        {
            Boolean encontrado = true;
            radicacionHojasDeVida solicitudIngreso = new radicacionHojasDeVida(Empresa, Usuario);
            DataTable _dataTable = solicitudIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(numeroDocumento);
            if (_dataTable.Rows.Count == 0) encontrado = false;
            return encontrado;
        }

        public DataRow ObtenerModeloNomina(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C)
        {
            condicionComercial _condicionComercial = new condicionComercial(Empresa, Usuario);
            DataTable _dataTable = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C);
            DataRow _dataRow = null;
            if (_dataTable.Rows.Count > 0) _dataRow = _dataTable.Rows[0];
            return _dataRow;
        }

        public DataRow ObtenerEmpleado(String numeroDocumento)
        {
            registroContrato _contrato = new registroContrato(Empresa, Usuario);
            DataTable _dataTable = _contrato.ObtenerPorNumeroIdentificacion(numeroDocumento);
            DataRow _dataRow = null;
            if (_dataTable.Rows.Count > 0)
            {
                DataRow[] filasEncontradas = _dataTable.Select("ACTIVO = 'Si'");

                if (filasEncontradas.Length == 1)
                {
                    _dataRow = filasEncontradas[0];

                    if (_dataRow != null)
                    {
                        _idCiudad = !String.IsNullOrEmpty(_dataRow["ID_CIUDAD"].ToString()) ? _dataRow["ID_CIUDAD"].ToString() : null;
                        _idCentroCosto = !String.IsNullOrEmpty(_dataRow["ID_CENTRO_C"].ToString()) ? Convert.ToDecimal(_dataRow["ID_CENTRO_C"].ToString()) : 0;
                        _idSubCentroCosto = !String.IsNullOrEmpty(_dataRow["ID_SUB_C"].ToString()) ? Convert.ToDecimal(_dataRow["ID_SUB_C"].ToString()) : 0;
                    }
                }
            }
            _dataTable.Dispose();
            return _dataRow;
        }

        public Decimal ObtenerIdConcepto(String codigoConcepto)
        {
            Decimal idConcepto = 0;
            conceptosNomina _conceptosNomina = new conceptosNomina(Empresa, Usuario);
            DataTable _dataTable = _conceptosNomina.ObtenerPorCodigo(codigoConcepto);
            if (_dataTable.Rows.Count > 0)
            {
                DataRow _dataRow = _dataTable.Rows[0];
                _dataTable.Dispose();
                idConcepto = Convert.ToDecimal(_dataRow["ID_CONCEPTO"]);
            }
            return idConcepto;
        }

        #endregion reglas

    }
}
