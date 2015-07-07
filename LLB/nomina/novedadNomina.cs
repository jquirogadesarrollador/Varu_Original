using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class novedadNomina
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

        public Decimal IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public Decimal IdConcepto
        {
            get { return _idConcepto; }
            set { _idConcepto = value; }
        }

        public Decimal IdPeriodo
        {
            get { return _idPeriodo; }
            set { _idPeriodo = value; }
        }

        public String Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public String Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        #endregion propiedades

        #region constructores
        public novedadNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;

            _codigoUnico = Guid.NewGuid();
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodos(Int32 ID_PERIODO, String ESTADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_nomina_novedades_obtener_todos ";

            #region validaciones
            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += ", '" + ESTADO + "'";
            }
            else
            {
                MensajeError = "El campo ESTADO es requerido para la consulta.";
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

        public DataTable ObtenerNovedadesNominaEmpleado(Int32 ID_EMPLEADO, Int32 ID_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_nomina_novedades_obtener_por_empleado ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
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

            sql = "usp_nom_nomina_novedades_obtener_registro ";

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

        public Decimal Adicionar(Decimal ID_EMPLEADO,
            Int32 ID_CONCEPTO,
            Int32 ID_PERIODO,
            String CANTIDAD,
            String VALOR,
            String ORIGEN,
            String ESTADO,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            String CAMBIO_UBICACION)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_nom_nomina_novedades_adicionar ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO.ToString() + "', ";
                informacion += "ID_CONCEPTO= '" + ID_CONCEPTO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_PERIODO != 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "', ";
                informacion += "ID_PERIODO= '" + ID_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += CANTIDAD.Replace(',', '.') + ", ";

            sql += VALOR.Replace(',', '.') + ", ";


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

            sql += "'" + ID_CIUDAD.ToString() + "', ";
            informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            sql += "'" + ID_CENTRO_C + "', ";
            informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            sql += "'" + ID_SUB_C + "', ";
            informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "', ";
            sql += "'" + CAMBIO_UBICACION + "', ";
            informacion += "CAMBIO_UBICACION = '" + CAMBIO_UBICACION.ToString() + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (!(String.IsNullOrEmpty(idRecuperado))) return Convert.ToDecimal(idRecuperado);
            else return 0;
        }

        public Decimal Adicionar(Decimal ID_EMPLEADO, Int32 ID_CONCEPTO, Int32 ID_PERIODO, Decimal CANTIDAD, Decimal VALOR, String ORIGEN, String ESTADO, Conexion conexion)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_nom_nomina_novedades_adicionar ";

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
            if (ID_PERIODO != 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "', ";
                informacion += "ID_PERIODO= '" + ID_PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + CANTIDAD.ToString() + "', ";
            informacion += "CANTIDAD= '" + CANTIDAD.ToString() + ", ";

            sql += "'" + VALOR.ToString() + "', ";
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

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

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

        public DataTable Adicionar(string idEmpresa, string usuario, string nombre, string numeroDocumento, string codigoConcepto, string cantidad, string valor, Conexion conexion)
        {
            MensajeError = null;

            string sql = null;

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            sql = "usp_tmp_nomina_novedades_adicionar_v2 ";
            sql += "'" + idEmpresa.ToString() + "',";
            sql += "'" + usuario.ToString() + "',";
            sql += "'" + nombre.ToString() + "',";
            sql += "'" + numeroDocumento.ToString() + "',";
            sql += "'" + codigoConcepto.ToString() + "',";
            sql += "'" + cantidad.ToString() + "',";
            sql += "'" + valor.ToString() + "', ";
            sql += "'" + _codigoUnico.ToString() + "'";

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

        public Decimal EliminarNovedadPorId(Int32 REGISTRO)
        {
            String sql = null;
            Decimal rowsDeleted = 0;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_nomina_novedades_eliminar ";

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
                    if (rowsDeleted > 0)
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        _auditoria.Adicionar(Usuario, tabla.NOM_NOMINA_NOVEDADES, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
                        #endregion auditoria
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
            }
            return rowsDeleted;
        }

        public Decimal EliminarNovedadPorOrigen(Decimal ID_PERIODO, String ORIGEN, Decimal ID_EMPRESA, String PERIODOSPROCESO, String TIPOPERIODO)
        {
            String sql = null;
            Decimal rowsDeleted = 0;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_nomina_novedades_eliminar_por_origen ";

            #region validaciones
            sql += "'" + ID_PERIODO.ToString() + "', ";
            informacion += "ID_PERIODO= '" + ID_PERIODO.ToString() + ", ";

            if (!(String.IsNullOrEmpty(ORIGEN)))
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN = '" + ORIGEN.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ORIGEN es requerido para la eliminacion.";
                ejecutar = false;
            }

            sql += "'" + ID_EMPRESA.ToString() + "', ";
            informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + ", ";
            sql += "'" + PERIODOSPROCESO.ToString() + "', ";
            sql += "'" + TIPOPERIODO + "'";
            informacion += "PERIODOSPROCESO= '" + PERIODOSPROCESO.ToString() + ", ";
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

        public Decimal Adicionar(Decimal ID_EMPLEADO, Int32 ID_CONCEPTO, Int32 ID_PERIODO, Decimal CANTIDAD, Decimal VALOR, String ORIGEN, String ESTADO, DateTime FECHA, Conexion conexion)
        {
            Decimal id = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_nom_nomina_novedades_adicionar ";

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
            if (ID_PERIODO != 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "', ";
                informacion += "ID_PERIODO= '" + ID_PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(CANTIDAD).ToString() + "', ";
            informacion += "CANTIDAD= '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(CANTIDAD).ToString() + ", ";
            sql += "'" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR).ToString() + "', ";
            informacion += "VALOR= '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR).ToString() + ", ";

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

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    id = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_NOMINA_NOVEDADES, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    id = 0;
                }
            }
            return id;
        }

        public DataTable Importar(StreamReader streamReader, Decimal ID_EMPRESA)
        {
            Configurar();

            Cargar(streamReader, ID_EMPRESA.ToString());

            if (_dataTable.Rows.Count > 0)
            {
                _dataTable.DefaultView.Sort = "nombre, codigoConcepto ASC";
                MensajeError = "El cargue del archivo plano se realizo pero arrojó inconsistencias.";
            }
            else MensajeError = "El proceso de cargue de novedades finalizao satisfactoriamente.";

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

        public Boolean Actualizar(Int32 REGISTRO, Decimal ID_EMPLEADO, Int32 ID_CONCEPTO, Decimal CANTIDAD, Decimal VALOR, DateTime FECHA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_nom_nomina_novedades_actualizar ";

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
                    _auditoria.Adicionar(Usuario, tabla.NOM_NOMINA_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            sql = "usp_nom_nomina_novedades_actualizar_estado ";

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
                    _auditoria.Adicionar(Usuario, tabla.NOM_NOMINA_NOVEDADES, tabla.ACCION_ANULAR, sql, informacion, conexion);
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

        public DataTable ObtenerDropTable(Int32 TableDrop, Int32 Id_Empresa, Int32 Id_Usuario, String Destino = "")
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_Obtener_Maestros_Novedades_1 ";

            #region validaciones
            if (TableDrop != 0)
            {
                sql += "'" + TableDrop + "', ";
            }
            else
            {
                MensajeError += "El campo tableDrop no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "" + Id_Empresa.ToString() + ",";
            sql += "" + Id_Usuario.ToString() + ", ";
            sql += "'" + Destino.ToString() + "'";
            #endregion validaciones

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

        public DataTable ObtenerDropUbicacion(String TipoDrop, Int32 Id_Empresa, String Ciudad, Decimal Centro)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_Obtener_Ubicacion_Tipo ";

            #region validaciones
            if (!(String.IsNullOrEmpty(TipoDrop)))
            {
                sql += "'" + TipoDrop + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }

            if (Id_Empresa != 0)
            {
                sql += "'" + Id_Empresa.ToString() + "',";
            }
            else
            {
                MensajeError += "El campo Empresa debe ser mayor a cero\n";
                ejecutar = false;
            }

            sql += "'" + Ciudad.ToString() + "',";

            sql += "'" + Centro.ToString() + "'";
            #endregion validaciones

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


        public DataTable ObtenerDropConceptos(String Filtro, String Tipo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_Obtener_Conceptos_Busqueda ";

            #region validaciones
            if (!(String.IsNullOrEmpty(Filtro)))
            {
                sql += "'" + Filtro + "', ";
            }
            else
            {
                sql += "'', ";
            }

            if (!(String.IsNullOrEmpty(Tipo)))
            {
                sql += "'" + Tipo + "'";
            }
            else
            {
                MensajeError += "El campo Tipo es requerido para la consulta.\n";
                ejecutar = false;
            }
            #endregion validaciones

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


        public DataTable ObtenerCantidadReportada(Int32 ID_CONCEPTO, Int32 ID_PERIODO, Int32 ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_nom_conceptos_nomina_Obtener_Cantidad_Reportada ";

            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_PERIODO != 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERIODO no puede ser nulo\n";
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

        public DataTable ObtenerCedulaEmpleado(Int32 Id_Empleado)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "ObtenerCedulaPorIdEmpleado";

            if (Id_Empleado != 0)
            {
                sql += "'" + Id_Empleado + "'";
            }
            else
            {
                MensajeError += "El campo Id_Empleado no puede ser nulo\n";
                ejecutar = false;
            }

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

        public String ValideNomina(Int32 ID_PERIODO, Decimal ID_EMPLEADO)
        {
            String sql = null;
            String Validado = "";
            Boolean ejecutar = false;

            sql = "usp_validar_empleado_reliquidar ";

            #region validaciones
            if (ID_PERIODO != 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "' ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la eliminacion.";
                ejecutar = false;
            }
            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "' ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la eliminacion.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    Validado = conexion.ExecuteScalar(sql);
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
            return Validado;
        }

        public Decimal Retirar_empleado(Decimal ID_EMPLEADO, DateTime FECHA_RETIRO, Int32 ID_PERIODO)
        {
            String sql = null;
            Decimal rowsRet = 0;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();


            sql = "usp_nom_nomina_novedades_retirar_1 ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "' ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la eliminacion.";
                ejecutar = false;
            }

            sql += ", '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RETIRO) + "', ";
            informacion += "FECHA_RETIRO = '" + FECHA_RETIRO.ToString() + "', ";


            if (ID_PERIODO != 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "', ";
                informacion += "ID_PERIODO= '" + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la eliminacion.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    rowsRet = conexion.ExecuteNonQuery(sql);

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
            return rowsRet;
        }

        public Int32 ObtenerRegistroContratoNovedadesPorCedula(Decimal ID_EMPRESA, String CEDULA)
        {
            String sql = null;
            Int32 Registro = 0;
            Boolean ejecutar = true;

            sql = "usp_retorna_registro_ultimo_contrato_activo_desde_cedula ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CEDULA)))
            {
                sql += "'" + CEDULA.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo CEDULA es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    Registro = Convert.ToInt32(conexion.ExecuteScalar(sql));
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
            return Registro;
        }

        public String ObtenertextDrop(Int32 REGISTRO)
        {
            String sql = null;
            String textDrop = "";
            Boolean ejecutar = true;

            sql = "usp_retorna_text_drp ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo REGISTRO es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    textDrop = conexion.ExecuteScalar(sql);
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
            return textDrop;
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
                informacion += "REGISTRO = " + REGISTRO + "";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo ni cero. \n";
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



        public DataTable ObtenerContratoEmpleadoPorRegistroParaNomina(Int32 REGISTRO, String tipoPeriodo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_contrato_empleado_por_registro_contrato_para_nomina_1 ";

            if (REGISTRO > 0)
            {
                sql += REGISTRO.ToString();
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo ni cero. \n";
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




        #endregion metodos

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

        private void Cargar(StreamReader streamReader, string idEmpresa)
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
                                            DataTable tablaResultado = Adicionar(idEmpresa, Usuario, datos[0].ToString(), datos[1].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), conexion);

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

        private void Cargar(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Adicionar(dataRow["numero_documento"].ToString(), dataRow["nombre"].ToString(), dataRow["codigo_concepto"].ToString(), dataRow["cantidad"].ToString(), dataRow["valor"].ToString(), dataRow["inconsistencia"].ToString());
            }
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
                foreach (DataRow _dataRow_Origen in _dataTable.Rows)
                {
                    if (_dataRow_Origen["ACTIVO"].ToString() == "Si")
                    {
                        _idCiudad = !String.IsNullOrEmpty(_dataRow_Origen["ID_CIUDAD"].ToString()) ? _dataRow_Origen["ID_CIUDAD"].ToString() : null;
                        _idCentroCosto = !String.IsNullOrEmpty(_dataRow_Origen["ID_CENTRO_C"].ToString()) ? Convert.ToDecimal(_dataRow_Origen["ID_CENTRO_C"].ToString()) : 0;
                        _idSubCentroCosto = !String.IsNullOrEmpty(_dataRow_Origen["ID_SUB_C"].ToString()) ? Convert.ToDecimal(_dataRow_Origen["ID_SUB_C"].ToString()) : 0;

                        return _dataRow_Origen;
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

        public DataTable ObtenerPeriodoNominaEmpleado(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, Decimal ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_novedades_trae_periodo_empleado ";

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
                sql += "'" + ID_EMPLEADO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

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

            _dataTablePeriodoNomina = _periodoNomina.ObtenerPorIdEmpresaCiudadCCSubCC(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C);
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
        #endregion reglas
    }
}