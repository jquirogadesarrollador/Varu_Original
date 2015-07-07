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

namespace Brainsbits.LLB.programasRseGlobal
{
    public class Programa
    {
        #region variables

        public enum Areas
        {
            RSE = 0,
            GLOBALSALUD,
            BS,
            OPERACIONES,
            GESTIONHUMANA
        }

        public enum EstadosDetalleActividad
        {
            CREADA = 1,
            APROBADA,
            CANCELADA,
            TERMINADA
        }

        public enum TemasEncuesta
        {
            LOGISTICA = 1,
            INSTRUCTOR,
            INSTALACIONES
        }

        public enum CalificacionesTemasEncuesta
        {
            BUENA = 1,
            REGULAR,
            MALA
        }

        public enum TiposArchivo
        {
            ENCUESTA = 1,
            ASISTENCIA,
            ADJUNTO
        }

        public enum TiposGeneraCompromiso
        {
            ACTIVIDAD = 0,
            ACCIDENTE,
            CASOSEVERO
        }
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _id_programa = 0;
        private Decimal _id_detalle_sub_programa = 0;
        private Decimal _id_detalle_actividad = 0;
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

        public Decimal IdPrograma
        {
            get { return _id_programa; }
            set { _id_programa = value; }
        }

        public Decimal IdDetalleSubPrograma
        {
            get { return _id_detalle_sub_programa; }
            set { _id_detalle_sub_programa = value; }
        }

        public Decimal IdDetalleActividad
        {
            get { return _id_detalle_actividad; }
            set { _id_detalle_actividad = value; }
        }
        #endregion propiedades

        #region constructores
        public Programa()
        {

        }
        public Programa(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerProgramasGeneralesPorArea(Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_maestra_obtenerTodosPorIdArea ";

            sql += "'" + AREA.ToString() + "'";

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

        public DataTable ObtenerDetalleProgramaGeneralPorIdDetalle(Decimal ID_DETALLE_GENERAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_detalle_obtenerPorId ";

            sql += ID_DETALLE_GENERAL;

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

        public Decimal AdicionarRegistromaestraProgramaGeneral(String TITULO,
            String TEXTO_CABECERA,
            String TEXTO_FINAL,
            Int32 ANNO,
            Areas ID_AREA,
            String DIR_IMAGEN)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_PROGRAMA_GENERAL = 0;

            sql = "usp_prog_general_maestra_adicionar ";

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TITULO = 'null', ";
            }

            if (String.IsNullOrEmpty(TEXTO_CABECERA) == false)
            {
                sql += "'" + TEXTO_CABECERA + "', ";
                informacion += "TEXTO_CABECERA = '" + TEXTO_CABECERA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TEXTO_CABECERA = 'null', ";
            }

            if (String.IsNullOrEmpty(TEXTO_FINAL) == false)
            {
                sql += "'" + TEXTO_FINAL + "', ";
                informacion += "TEXTO_FINAL = '" + TEXTO_FINAL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TEXTO_FINAL = 'null', ";
            }


            if (ANNO != 0)
            {
                sql += ANNO + ", ";
                informacion += "ANNO = '" + ANNO + "', ";
            }
            else
            {
                MensajeError += "El campo AÑO no puede ser nulo ni cero 0.\n";
                ejecutar = false;
            }

            sql += "'" + ID_AREA.ToString() + "', ";
            informacion += "ID_AREA = '" + ID_AREA.ToString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(DIR_IMAGEN) == false)
            {
                sql += "'" + DIR_IMAGEN + "'";
                informacion += "DIR_IMAGEN = '" + DIR_IMAGEN + "'";
            }
            else
            {
                sql += "null";
                informacion += "DIR_IMAGEN = 'null'";
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    ID_PROGRAMA_GENERAL = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_GENERAL_MAESTRA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    IdDetalleActividad = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return ID_PROGRAMA_GENERAL;
        }

        public Boolean AdicionarRegistroDetalleProgramaGeneral(Decimal ID_PROGRAMA_GENERAL,
            String TIPO,
            Decimal ID_DETALLE_GENERAL_PADRE,
            Decimal ID_SUBPROGRAMA,
            Decimal ID_ACTIVIDAD,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_DETALLE_GENERAL = 0;

            sql = "usp_prog_general_detalle_adicionar ";

            if (ID_PROGRAMA_GENERAL != 0)
            {
                sql += ID_PROGRAMA_GENERAL + ", ";
                informacion += "ID_PROGRAMA_GENERAL = '" + ID_PROGRAMA_GENERAL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PROGRAMA_GENERAL no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO) == false)
            {
                sql += "'" + TIPO + "', ";
                informacion += "TIPO = '" + TIPO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }


            if (ID_DETALLE_GENERAL_PADRE != 0)
            {
                sql += ID_DETALLE_GENERAL_PADRE + ", ";
                informacion += "ID_DETALLE_GENERAL_PADRE = '" + ID_DETALLE_GENERAL_PADRE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_DETALLE_GENERAL_PADRE = 'NULL', ";
            }

            if (ID_SUBPROGRAMA != 0)
            {
                sql += ID_SUBPROGRAMA + ", ";
                informacion += "ID_SUBPROGRAMA = '" + ID_SUBPROGRAMA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SUBPROGRAMA = 'NULL', ";
            }

            if (ID_ACTIVIDAD != 0)
            {
                sql += ID_ACTIVIDAD + ", ";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_ACTIVIDAD = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_DETALLE_GENERAL = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_GENERAL_DETALLE, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_DETALLE_GENERAL = 0;
                }
            }

            if (ID_DETALLE_GENERAL == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean AdicionarDetalleGeneral(Decimal ID_PROGRAMA_GENERAL,
            String TIPO,
            Decimal ID_DETALLE_GENERAL_PADRE,
            Decimal ID_SUBPROGRAMA,
            Decimal ID_ACTIVIDAD)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (AdicionarRegistroDetalleProgramaGeneral(ID_PROGRAMA_GENERAL, TIPO, ID_DETALLE_GENERAL_PADRE, ID_SUBPROGRAMA, ID_ACTIVIDAD, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                correcto = false;
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public DataTable ObtenerDetallesProgramaGeneralPorIdPadre(Decimal ID_PROGRAMA_GENERAL, Decimal ID_DETALLE_GENERAL_PADRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_detalle_obtenerPorIdpadre ";

            if (ID_PROGRAMA_GENERAL != 0)
            {
                sql += ID_PROGRAMA_GENERAL + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROGRAMA_GENERAL no puede ser 0.";
            }

            if (ID_DETALLE_GENERAL_PADRE != 0)
            {
                sql += ID_DETALLE_GENERAL_PADRE;
            }
            else
            {
                sql += "NULL";
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

        public Boolean ActualizarProgramaGeneral(Decimal ID_PROGRAMA_GENERAL,
            String TITULO,
            String TEXTO_CABECERA,
            String TEXTO_FINAL,
            String DIR_IMAGEN)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_general_maestra_actualizar ";

            #region validaciones

            if (ID_PROGRAMA_GENERAL != 0)
            {
                sql += ID_PROGRAMA_GENERAL + ", ";
                informacion += "ID_PROGRAMA_GENERAL = '" + ID_PROGRAMA_GENERAL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PROGRAMA_GENERAL no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError = "El campo TITULO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TEXTO_CABECERA) == false)
            {
                sql += "'" + TEXTO_CABECERA + "', ";
                informacion += "TEXTO_CABECERA = '" + TEXTO_CABECERA + "', ";
            }
            else
            {
                MensajeError = "El campo TEXTO_CABECERA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TEXTO_FINAL) == false)
            {
                sql += "'" + TEXTO_FINAL + "', ";
                informacion += "TEXTO_FINAL = '" + TEXTO_FINAL + "', ";
            }
            else
            {
                MensajeError = "El campo TEXTO_FINAL no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(DIR_IMAGEN) == false)
            {
                sql += "'" + DIR_IMAGEN + "'";
                informacion += "DIR_IMAGEN = '" + DIR_IMAGEN + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "DIR_IMAGEN = 'NULL'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_GENERAL_MAESTRA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
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

        public DataTable ObtenerPrograGeneralMaestraPorId(Decimal ID_PROGRAMA_GENERAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_maestra_obtenerPorId ";

            if (ID_PROGRAMA_GENERAL != 0)
            {
                sql += ID_PROGRAMA_GENERAL;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROGRAMA_GENERAL no puede ser 0.";
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


        public DataTable ObtenerPresupuestosYProgramasPorEmpresaYArea(Decimal ID_EMPRESA, Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_presupuestos_obtenerProgramas ";

            sql += ID_EMPRESA + ", '" + AREA.ToString() + "'";

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

            _dataTable.Columns.Add("CON_PROGRAMA");

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                DataRow fila = _dataTable.Rows[i];

                if (DBNull.Value.Equals(fila["ID_PROGRAMA"]) == false)
                {
                    fila["CON_PROGRAMA"] = "Tiene Programa Asignado";
                }
                else
                {
                    fila["CON_PROGRAMA"] = "No Tiene Programa Asignado";
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerActividadesProgramaEspecificoPorIdPrograma(Decimal ID_PROGRAMA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_actividads_obtenerPorIdPrograma ";

            sql += ID_PROGRAMA + ", ";

            sql += "'" + Usuario + "'";

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

            _dataTable.Columns.Add("TRIMESTRE");

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                DataRow _fila = _dataTable.Rows[i];
                DateTime _fecha = Convert.ToDateTime(_fila["FECHA_ACTIVIDAD"]);
                if ((_fecha.Month >= 1) && (_fecha.Month <= 3))
                {
                    _fila["TRIMESTRE"] = "Trimestre 1";
                }
                else
                {
                    if ((_fecha.Month >= 4) && (_fecha.Month <= 6))
                    {
                        _fila["TRIMESTRE"] = "Trimestre 2";
                    }
                    else
                    {
                        if ((_fecha.Month >= 7) && (_fecha.Month <= 9))
                        {
                            _fila["TRIMESTRE"] = "Trimestre 3";
                        }
                        else
                        {
                            _fila["TRIMESTRE"] = "Trimestre 4";
                        }
                    }
                }

                _dataTable.AcceptChanges();
            }

            return _dataTable;
        }

        public DataTable ObtenerProgramaAlQuePerteneceUnaActividadProgramada(Decimal ID_DETALLE_GENERAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_detalle_obtenerSubprogramaPadre ";

            sql += ID_DETALLE_GENERAL;

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


        public DataTable ObtenerSubProgramasDeUnProgramaGeneral(Decimal ID_PROGRAMA_GENERAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_detalle_obtenerSubProgramasPorIdProgGeneral ";

            sql += ID_PROGRAMA_GENERAL;

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

        public DataTable ObtenerActividadesPorDetalleGeneralPadre(Decimal ID_DETALLE_GENERAL_PADRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_detalle_obtenerActividadesPorDetallePadre ";

            sql += ID_DETALLE_GENERAL_PADRE;

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

        public Boolean AdicionarProgramaAPresupuesto(Decimal ID_PROGRAMA_GENERAL,
            Decimal ID_EMPRESA,
            Areas AREA,
            Decimal ID_PRESUPUESTO,
            Int32 ANNO,
            String DESCRIPCION,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            IdPrograma = 0;
            IdDetalleSubPrograma = 0;
            IdDetalleActividad = 0;

            sql = "usp_prog_maestra_programas_adicionar ";

            if (ID_PROGRAMA_GENERAL != 0)
            {
                sql += ID_PROGRAMA_GENERAL + ", ";
                informacion += "ID_PROGRAMA_GENERAL = '" + ID_PROGRAMA_GENERAL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PROGRAMA_GENERAL no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + AREA.ToString() + "', ";
            informacion += "AREA = '" + AREA.ToString() + "', ";

            if (ID_PRESUPUESTO != 0)
            {
                sql += ID_PRESUPUESTO + ", ";
                informacion += "ID_PRESUPUESTO = '" + ID_PRESUPUESTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRESUPUESTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ANNO != 0)
            {
                sql += ANNO + ", ";
                informacion += "ANNO = '" + ANNO + "', ";
            }
            else
            {
                MensajeError += "El campo ANNO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DESCRIPCION = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    IdPrograma = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_MAESTRA_PROGRAMAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    IdPrograma = 0;
                }
            }

            if (IdPrograma == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Boolean AdicionarRegistroDetalleActividad(Decimal ID_PROGRAMA,
            Decimal ID_DETALLE_GENERAL,
            Decimal ID_ACTIVIDAD,
            String RESUMEN_ACTIVIDAD,
            DateTime FECHA_ACTIVIDAD,
            String HORA_INICIO,
            String HORA_FIN,
            Decimal PRESUPUESTO_APROBADO,
            Int32 PERSONAL_CITADO,
            String ENCARGADO,
            String ID_CIUDAD,
            Conexion conexion)
        {

            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            IdPrograma = ID_PROGRAMA;
            IdDetalleSubPrograma = 0;
            IdDetalleActividad = 0;

            sql = "usp_prog_detalle_actividades_adicionar ";

            if (ID_PROGRAMA != 0)
            {
                sql += ID_PROGRAMA + ", ";
                informacion += "ID_PROGRAMA = '" + ID_PROGRAMA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_DETALLE_GENERAL != 0)
            {
                sql += ID_DETALLE_GENERAL + ", ";
                informacion += "ID_DETALLE_GENERAL = '" + ID_DETALLE_GENERAL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE_GENERAL no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ACTIVIDAD != 0)
            {
                sql += ID_ACTIVIDAD + ", ";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ACTIVIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RESUMEN_ACTIVIDAD) == false)
            {
                sql += "'" + RESUMEN_ACTIVIDAD + "', ";
                informacion += "RESUMEN_ACTIVIDAD = '" + RESUMEN_ACTIVIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo RESUMEN_ACTIVIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTIVIDAD) + "', ";
            informacion += "FECHA_ACTIVIDAD = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTIVIDAD) + "', ";

            if (String.IsNullOrEmpty(HORA_INICIO) == false)
            {
                sql += "'" + HORA_INICIO + "', ";
                informacion += "HORA_INICIO = '" + HORA_INICIO + "', ";
            }
            else
            {
                MensajeError += "El campo HORA_INICIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(HORA_FIN) == false)
            {
                sql += "'" + HORA_FIN + "', ";
                informacion += "HORA_FIN = '" + HORA_FIN + "', ";
            }
            else
            {
                MensajeError += "El campo HORA_FIN no puede ser nulo\n";
                ejecutar = false;
            }

            sql += PRESUPUESTO_APROBADO.ToString().Replace(',', '.') + ", ";
            informacion += "PRESUPUESTO_APROBADO = '" + PRESUPUESTO_APROBADO.ToString().Replace(',', '.') + "', ";

            sql += PERSONAL_CITADO.ToString() + ", ";
            informacion += "PERSONAL_CITADO = '" + PERSONAL_CITADO.ToString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(ENCARGADO) == false)
            {
                sql += "'" + ENCARGADO + "', ";
                informacion += "ENCARGADO = '" + ENCARGADO + "', ";
            }
            else
            {
                MensajeError += "El campo ENCARGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "'";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "'";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    IdDetalleActividad = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DETALLE_ACTIVIDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    IdDetalleActividad = 0;
                }
            }

            if (IdDetalleActividad == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean AdicionarDetalleActividad(Decimal ID_PROGRAMA_GENERAL,
            Decimal ID_PROGRAMA,
            Decimal ID_EMPRESA,
            Areas AREA,
            Decimal ID_PRESUPUESTO,
            Int32 ANNO,
            Decimal ID_ACTIVIDAD,
            DateTime FECHA_ACTIVIDAD,
            String HORA_INICIO,
            String HORA_FIN,
            Decimal PRESUPUESTO_APROBADO,
            Int32 PERSONAL_CITADO,
            String ENCARGADO,
            String ID_CIUDAD,
            String RESUMEN_ACTIVIDAD,
            Decimal ID_DETALLE_GENERAL)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ID_PROGRAMA == 0)
                {
                    if (AdicionarProgramaAPresupuesto(ID_PROGRAMA_GENERAL, ID_EMPRESA, AREA, ID_PRESUPUESTO, ANNO, null, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                    else
                    {
                        ID_PROGRAMA = IdPrograma;
                    }
                }
                else
                {
                    IdPrograma = ID_PROGRAMA;
                }

                if (correcto == true)
                {
                    if (AdicionarRegistroDetalleActividad(ID_PROGRAMA, ID_DETALLE_GENERAL, ID_ACTIVIDAD, RESUMEN_ACTIVIDAD, FECHA_ACTIVIDAD, HORA_INICIO, HORA_FIN, PRESUPUESTO_APROBADO, PERSONAL_CITADO, ENCARGADO, ID_CIUDAD, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                correcto = false;
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public DataTable ObtenerDetalleActividadesPorFecha(DateTime fecha,
            Areas area,
            String encargado,
            String idEmpresa,
            String regional,
            String ciudad,
            String estado)
        {
            tools _tools = new tools();

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_actividades_obtenerPorFecha ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fecha) + "', '" + area.ToString() + "', ";
            sql += "'" + encargado + "', '" + idEmpresa + "', '" + regional + "', '" + ciudad + "', '" + estado + "', ";
            sql += "'" + Usuario + "'";

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

        public DataTable ObtenerDetalleActividadesPorIdDetalle(Decimal ID_DETALLE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_actividades_obtenerPorId ";

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_DETALLE no puede ser 0.";
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

        public Boolean CancelarDetalleActividad(Decimal ID_DETALLE,
            EstadosDetalleActividad ID_ESTADO,
            String MOTIVO_CANCELACION,
            String TIPO_CANCELACION,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            int numRegistrosActualizados = 0;

            try
            {
                numRegistrosActualizados = conexion.ExecuteNonQueryParaCancelacionDeActividad(ID_DETALLE, ID_ESTADO.ToString(), MOTIVO_CANCELACION, Usuario, TIPO_CANCELACION, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

                if (numRegistrosActualizados <= 0)
                {
                    MensajeError = "No se actualizó ningún registro enla base de datos. Error en USP. de actualización.";
                    conexion.DeshacerTransaccion();
                    numRegistrosActualizados = 0;
                }
                else
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                numRegistrosActualizados = 0;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            if (numRegistrosActualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean TerminarDetalleActividad(Decimal ID_DETALLE,
            Decimal PRESUPUESTO_FINAL,
            Int32 PERSONLA_FINAL,
            EstadosDetalleActividad ID_ESTADO,
            String RESULTADOS_ENCUESTA,
            String CONCLUSIONES,
            String DIR_IMAGEN_REPRESENTATIVA,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_prog_detalle_actividades_terminar ";

            #region validaciones

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE + ", ";
                informacion += "ID_DETALLE = '" + ID_DETALLE + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE no puede ser vacio.";
                ejecutar = false;
            }

            sql += PRESUPUESTO_FINAL.ToString().Replace(",", ".") + ", ";
            informacion += "PRESUPUESTO_FINAL = '" + PRESUPUESTO_FINAL.ToString() + "', ";

            sql += PERSONLA_FINAL.ToString() + ", ";
            informacion += "PERSONLA_FINAL = '" + PERSONLA_FINAL.ToString() + "', ";

            sql += "'" + ID_ESTADO.ToString() + "', ";
            informacion += "ID_ESTADO = '" + ID_ESTADO.ToString() + "', ";

            if (String.IsNullOrEmpty(RESULTADOS_ENCUESTA) == false)
            {
                sql += "'" + RESULTADOS_ENCUESTA + "', ";
                informacion += "RESULTADOS_ENCUESTA = '" + RESULTADOS_ENCUESTA + "', ";
            }
            else
            {
                MensajeError = "El campo RESULTADOS_ENCUESTA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(CONCLUSIONES) == false)
            {
                sql += "'" + CONCLUSIONES + "', ";
                informacion += "CONCLUSIONES = '" + CONCLUSIONES + "', ";
            }
            else
            {
                MensajeError = "El campo CONCLUSIONES no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DIR_IMAGEN_REPRESENTATIVA) == false)
            {
                sql += "'" + DIR_IMAGEN_REPRESENTATIVA + "'";
                informacion += "DIR_IMAGEN_REPRESENTATIVA = '" + DIR_IMAGEN_REPRESENTATIVA + "'";
            }
            else
            {
                sql += "null";
                informacion += "DIR_IMAGEN_REPRESENTATIVA = 'null'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DETALLE_ACTIVIDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean AdicionarRegistroControlAsistencia(Decimal ID_DETALLE,
            Decimal ID_EMPLEADO,
            Decimal ID_SOLICITUD,
            Conexion conexion)
        {

            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_ASISTENCIA = 0;

            sql = "usp_prog_control_asistencia_actividades_adicionar ";

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE + ", ";
                informacion += "ID_DETALLE = '" + ID_DETALLE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_ASISTENCIA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_CONTROL_ASISTENCIA_ACTIVIDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_ASISTENCIA = 0;
                }
            }

            if (ID_ASISTENCIA == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean AdicionarRegistroEntidadQueColaboroEnActividad(Decimal ID_DETALLE,
            Decimal ID_ENTIDAD,
            String DESCRIPCION,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_ENTIDAD_COLABORA = 0;

            sql = "usp_prog_entidades_actividades_adicionar ";

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE + ", ";
                informacion += "ID_DETALLE = '" + ID_DETALLE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_ENTIDAD_COLABORA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_ENTIDADES_ACTIVIDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_ENTIDAD_COLABORA = 0;
                }
            }

            if (ID_ENTIDAD_COLABORA == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Decimal AdicionarRegistroCompromisoEnActividad(Decimal ID_ACTIVIDAD_GENERA,
            String NOMBRE_ACTIVIDAD_GENERA,
            String TIPO_GENERA,
            String COMPROMISO,
            String USU_LOG_RESPONSABLE,
            DateTime FECHA_P,
            String OBSERVACIONES,
            String ESTADO,
            String ID_AREA,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal idMaestraCompromiso = 0;

            sql = "usp_prog_compromisos_adicionar ";

            if (ID_ACTIVIDAD_GENERA != 0)
            {
                sql += ID_ACTIVIDAD_GENERA + ", ";
                informacion += "ID_ACTIVIDAD_GENERA = '" + ID_ACTIVIDAD_GENERA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ACTIVIDAD_GENERA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_ACTIVIDAD_GENERA) == false)
            {
                sql += "'" + NOMBRE_ACTIVIDAD_GENERA + "', ";
                informacion += "NOMBRE_ACTIVIDAD_GENERA = '" + NOMBRE_ACTIVIDAD_GENERA + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_ACTIVIDAD_GENERA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + TIPO_GENERA.ToString() + "', ";
            informacion += "TIPO_GENERA = '" + TIPO_GENERA.ToString() + "', ";

            if (String.IsNullOrEmpty(COMPROMISO) == false)
            {
                sql += "'" + COMPROMISO + "', ";
                informacion += "COMPROMISO = '" + COMPROMISO + "', ";
            }
            else
            {
                MensajeError += "El campo COMPROMISO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(USU_LOG_RESPONSABLE) == false)
            {
                sql += "'" + USU_LOG_RESPONSABLE + "', ";
                informacion += "USU_LOG_RESPONSABLE = '" + USU_LOG_RESPONSABLE + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG_RESPONSABLE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_P) + "', ";
            informacion += "FECHA_P = '" + FECHA_P.ToShortDateString() + "', ";

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(ID_AREA) == false)
            {
                sql += "'" + ID_AREA + "'";
                informacion += "ID_AREA = '" + ID_AREA + "'";
            }
            else
            {
                MensajeError += "El campo ID_AREA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    idMaestraCompromiso = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_MAESTRA_COMPROMISOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    idMaestraCompromiso = 0;
                }
            }

            return idMaestraCompromiso;
        }

        public Boolean TerminarProgDetallesActividad(Decimal ID_DETALLE,
            Decimal PRESUPUESTO_FINAL,
            Int32 PERSONALFINAL,
            EstadosDetalleActividad ID_ESTADO,
            String RESULTADOS_ENCUESTA,
            List<Asistencia> listaAsistencia,
            Byte[] ARCHIVO_ENCUESTA,
            Int32 ARCHIVO_ENCUESTA_TAMANO,
            String ARCHIVO_ENCUESTA_EXTENSION,
            String ARCHIVO_ENCUESTA_TYPE,
            Byte[] ARCHIVO_ASISTENCIA,
            Int32 ARCHIVO_ASISTENCIA_TAMANO,
            String ARCHIVO_ASISTENCIA_EXTENSION,
            String ARCHIVO_ASISTENCIA_TYPE,
            List<EntidadColaboradora> listaEntidadesColaboradoras,
            String CONCLUSIONES,
            String DIR_IMAGEN_REPRESENTATIVA,
            List<MaestraCompromiso> listaCompromisos,
            Areas ID_AREA)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (TerminarDetalleActividad(ID_DETALLE, PRESUPUESTO_FINAL, PERSONALFINAL, ID_ESTADO, RESULTADOS_ENCUESTA, CONCLUSIONES, DIR_IMAGEN_REPRESENTATIVA, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    foreach (Asistencia a in listaAsistencia)
                    {
                        if (AdicionarRegistroControlAsistencia(ID_DETALLE, a.ID_EMPLEADO, a.ID_SOLICITUD, conexion) == false)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
                            break;
                        }
                    }

                    if (correcto == true)
                    {
                        if (ARCHIVO_ENCUESTA != null)
                        {
                            if (conexion.ExecuteEscalarParaAdicionarDocsAdjuntosDetalleActividad(ID_DETALLE, TiposArchivo.ENCUESTA.ToString(), ARCHIVO_ENCUESTA, ARCHIVO_ENCUESTA_EXTENSION, ARCHIVO_ENCUESTA_TAMANO, ARCHIVO_ENCUESTA_TYPE, Usuario, null, null) <= 0)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        if (ARCHIVO_ASISTENCIA != null)
                        {
                            if (conexion.ExecuteEscalarParaAdicionarDocsAdjuntosDetalleActividad(ID_DETALLE, TiposArchivo.ASISTENCIA.ToString(), ARCHIVO_ASISTENCIA, ARCHIVO_ASISTENCIA_EXTENSION, ARCHIVO_ASISTENCIA_TAMANO, ARCHIVO_ASISTENCIA_TYPE, Usuario, null, null) <= 0)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (EntidadColaboradora e in listaEntidadesColaboradoras)
                        {
                            if (AdicionarRegistroEntidadQueColaboroEnActividad(ID_DETALLE, e.ID_ENTIDAD, e.DESCRIPCION, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                                break;
                            }
                        }

                        if (correcto == true)
                        {
                            foreach (MaestraCompromiso compromiso in listaCompromisos)
                            {
                                if (AdicionarRegistroCompromisoEnActividad(compromiso.ID_ACTIVIDAD_GENERA, compromiso.NOMBRE_ACTIVIDAD_GENERA, compromiso.TIPO_GENERA, compromiso.COMPROMISO, compromiso.USU_LOG_RESPONSABLE, compromiso.FECHA_P, compromiso.OBSERVACIONES, compromiso.ESTADO, ID_AREA.ToString(), conexion) <= 0)
                                {
                                    correcto = false;
                                    conexion.DeshacerTransaccion();
                                    break;
                                }
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                correcto = false;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerEmpleadosQueAsistieronAActividad(Decimal ID_DETALLE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_control_asistencia_actividades_obtenerPorActividad ";

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_DETALLE no puede ser 0.";
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

        public DataTable ObtenerArchivoAdjuntoActividad(Decimal ID_ADJUNTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_archivos_adjuntos_actividades_obtenerPorId ";

            if (ID_ADJUNTO != 0)
            {
                sql += ID_ADJUNTO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_ADJUNTO no puede ser 0.";
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

        public DataTable ObtenerEntidadesQueCOlaboraronEnActividad(Decimal ID_DETALLE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_entidades_actividades_obtenerPorIdDetalle ";

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_DETALLE no puede ser 0.";
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


        public DataTable ObtenerCompromisosDeActividad(Decimal idDetalle,
            TiposGeneraCompromiso tipoGenera)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_compromisos_obtenerPorIdDetalleTipoGenera ";

            if (idDetalle != 0)
            {
                sql += idDetalle + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_DETALLE no puede ser 0.";
            }

            sql += "'" + tipoGenera.ToString() + "'";

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


        public DataTable ObtenerIdProgramaGeneralDesdeIdMaestraCompromiso(Decimal idMaestraCompromiso)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_general_maestra_obtenerPorIdMaestraCompromiso ";

            if (idMaestraCompromiso != 0)
            {
                sql += idMaestraCompromiso;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_MAESTRA_COMPROMISO no puede ser 0.";
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


        public DataTable ObtenerInformacionCompromiso(Decimal idMaestraCompromiso)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_maestra_compromisos_obtenerPorIdMaestraCompromiso ";

            if (idMaestraCompromiso != 0)
            {
                sql += idMaestraCompromiso;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo idMaestraCompromiso no puede ser 0.";
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


        public DataTable ObtenerSeguimientosCompromiso(Decimal idMaestraCompromiso)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_seguimiento_compromisos_obtenerPorIdMaestraCompromiso ";

            if (idMaestraCompromiso != 0)
            {
                sql += idMaestraCompromiso;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo idMaestraCompromiso no puede ser 0.";
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



        public DataTable ObtenerCompromisosPendientes(Areas ID_AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_compromisos_obtenerPendientes_para_semaforo ";

            sql += "'" + ID_AREA.ToString() + "', ";

            sql += "'" + Usuario + "'";

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





        public Decimal AdicionarAdjuntoResultadoActividad(Decimal ID_DETALLE,
            String TITULO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            Int32 ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            String ARCHIVO_ADJUNTO_TYPE)
        {
            Decimal ID_ADJUNTO = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_ADJUNTO = conexion.ExecuteEscalarParaAdicionarDocsAdjuntosDetalleActividad(ID_DETALLE, TiposArchivo.ADJUNTO.ToString(), ARCHIVO_ADJUNTO, ARCHIVO_ADJUNTO_EXTENSION, ARCHIVO_ADJUNTO_TAMANO, ARCHIVO_ADJUNTO_TYPE, Usuario, TITULO, DESCRIPCION);

                if (ID_ADJUNTO <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_ADJUNTO = 0;
                }
                else
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                ID_ADJUNTO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_ADJUNTO;
        }


        public Decimal AdicionarSeguimientoCompromiso(Decimal ID_MAESTRA_COMPROMISO,
            String SEGUIMIENTO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            Int32 ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            String ARCHIVO_ADJUNTO_TYPE,
            DateTime FCH_CRE)
        {
            Decimal ID_SEGUIMIENTO = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_SEGUIMIENTO = conexion.ExecuteEscalarParaAdicionarDocsSeguimientoCompromiso(ID_MAESTRA_COMPROMISO, SEGUIMIENTO, DESCRIPCION, ARCHIVO_ADJUNTO, ARCHIVO_ADJUNTO_EXTENSION, ARCHIVO_ADJUNTO_TAMANO, ARCHIVO_ADJUNTO_TYPE, Usuario, FCH_CRE);

                if (ID_SEGUIMIENTO <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_SEGUIMIENTO = 0;
                }
                else
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                ID_SEGUIMIENTO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_SEGUIMIENTO;
        }



        public DataTable ObtenerDetallesSubProgramasPorIdPrograma(Decimal ID_PROGRAMA, Decimal ID_SUB_PROGRAMA_PADRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_sub_programas_obtenerPorProgramaYArea ";

            if (ID_PROGRAMA != 0)
            {
                sql += ID_PROGRAMA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROGRAMA no puede ser 0.";
            }

            if (ID_SUB_PROGRAMA_PADRE != 0)
            {
                sql += ID_SUB_PROGRAMA_PADRE;
            }
            else
            {
                sql += "NULL";
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

        public DataTable ObtenerDetalleSubProgramasPorIdDetalle(Decimal ID_DETALLE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_sub_programas_obtenerPorId ";

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_DETALLE no puede ser 0.";
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

        public DataTable ObtenerHistorialReprogramaciones(Decimal ID_DETALLE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_hist_reprog_actividades_obtenerPorIdDetalle ";

            sql += ID_DETALLE;

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


        public DataTable ObtenerHistorialAjustesPresupuesto(Decimal ID_DETALLE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_hist_ajustes_presupuesto_actividades ";

            sql += ID_DETALLE;

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

        public Boolean ActualizarDetalleActividad(Decimal ID_DETALLE,
            String RESUMEN_ACTIVIDAD,
            Decimal PRESUPUESTO_ASIGNADO,
            Int32 PERSONAL_CITADO,
            String ENCARGADO,
            String ID_CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_detalle_actividades_actualizar ";

            #region validaciones

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE + ", ";
                informacion += "ID_DETALLE = '" + ID_DETALLE + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RESUMEN_ACTIVIDAD) == false)
            {
                sql += "'" + RESUMEN_ACTIVIDAD + "', ";
                informacion += "RESUMEN_ACTIVIDAD = '" + RESUMEN_ACTIVIDAD + "', ";
            }
            else
            {
                MensajeError = "El campo RESUMEN_ACTIVIDAD no puede ser vacio.";
                ejecutar = false;
            }

            sql += PRESUPUESTO_ASIGNADO.ToString().Replace(',', '.') + ", ";
            informacion += "PRESUPUESTO_ASIGNADO = '" + PRESUPUESTO_ASIGNADO + "', ";

            if (PERSONAL_CITADO != 0)
            {
                sql += PERSONAL_CITADO + ", ";
                informacion += "PERSONAL_CITADO = '" + PERSONAL_CITADO + "', ";
            }
            else
            {
                MensajeError = "El campo PERSONAL_CITADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ENCARGADO) == false)
            {
                sql += "'" + ENCARGADO + "', ";
                informacion += "ENCARGADO = '" + ENCARGADO + "', ";
            }
            else
            {
                MensajeError = "El campo ENCARGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DETALLE_ACTIVIDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
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

        public Boolean ReprogramarDetalleActividad(Decimal ID_DETALLE,
            DateTime FECHA_ACTIVIDAD,
            String HORA_INICIO,
            String HORA_FIN,
            String MOTIVO_REPROGRAMACION,
            String TIPO_REPROGRAMACION,
            byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Int32 ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            int numRegistrosActualizados = 0;

            try
            {
                numRegistrosActualizados = conexion.ExecuteNonQueryParaReprogramarActividad(ID_DETALLE, FECHA_ACTIVIDAD, HORA_INICIO, HORA_FIN, MOTIVO_REPROGRAMACION, Usuario, TIPO_REPROGRAMACION, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

                if (numRegistrosActualizados <= 0)
                {
                    MensajeError = "No se actualizó ningún registro enla base de datos. Error en USP. de actualización.";
                    conexion.DeshacerTransaccion();
                    numRegistrosActualizados = 0;
                }
                else
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                numRegistrosActualizados = 0;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            if (numRegistrosActualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean AprobarDetalleActividad(Decimal ID_DETALLE,
            Decimal PRESUPUESTO_APROBADO,
            EstadosDetalleActividad ID_ESTADO)
        {
            tools _tools = new tools();

            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_prog_detalle_actividades_aprobar ";

            #region validaciones

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE + ", ";
                informacion += "ID_DETALLE = '" + ID_DETALLE + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE no puede ser vacio.";
                ejecutar = false;
            }

            sql += PRESUPUESTO_APROBADO.ToString().Replace(',', '.') + ", ";
            informacion += "FECHA_ACTIVIDAD = '" + PRESUPUESTO_APROBADO.ToString() + "', ";

            sql += "'" + ID_ESTADO.ToString() + "', ";
            informacion += "ID_ESTADO = '" + ID_ESTADO.ToString() + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DETALLE_ACTIVIDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
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

        public Boolean AjustarPresupuestoDetalleActividad(Decimal ID_DETALLE,
            Decimal PRESUPUESTO_APROBADO,
            EstadosDetalleActividad ID_ESTADO,
            Byte[] ARCHIVO,
            Int32 ARCHIVO_TAMANO,
            String ARCHIVO_EXTENSION,
            String ARCHIVO_TYPE)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            int numRegistrosActualizados = 0;

            try
            {
                numRegistrosActualizados = conexion.ExecuteNonQueryParaAjustarPresupuestoDetalleActividad(ID_DETALLE, PRESUPUESTO_APROBADO, ID_ESTADO.ToString(), Usuario, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

                if (numRegistrosActualizados <= 0)
                {
                    MensajeError = "No se actualizó ningún registro enla base de datos. Error en USP. de actualziar documento de Proveedor.";
                    conexion.DeshacerTransaccion();
                    numRegistrosActualizados = 0;
                }
                else
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                numRegistrosActualizados = 0;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            if (numRegistrosActualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public DataTable ObtenerArchivoAdjuntoPorTipoYDetalle(Decimal ID_DETALLE,
            Programa.TiposArchivo tipoArchivo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_archivos_adjuntos_actividades_obtenerPorIdDetalleYTipoArchivo ";

            sql += ID_DETALLE + ", ";
            sql += "'" + tipoArchivo.ToString() + "'";

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


        public DataTable ObtenerActividadesProgramadasParaUnMesAñoEmpresaYAreaEspecificos(Decimal ID_EMPRESA,
            Int32 ANIO,
            Int32 MES,
            Programa.Areas ID_AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_actividads_obtenerPorAnioMesEmpresaArea ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El Campo ID_EMPRESA no puede ser vacio.";
            }

            if (ANIO != 0)
            {
                sql += ANIO + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El Campo ANIO no puede ser vacio.";
            }

            if (MES != 0)
            {
                sql += MES + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El Campo MES no puede ser vacio.";
            }

            sql += "'" + ID_AREA.ToString() + "', ";

            sql += "'" + Usuario + "'";

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

            _dataTable.Columns.Add("TRIMESTRE");

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                DataRow _fila = _dataTable.Rows[i];
                DateTime _fecha = Convert.ToDateTime(_fila["FECHA_ACTIVIDAD"]);
                if ((_fecha.Month >= 1) && (_fecha.Month <= 3))
                {
                    _fila["TRIMESTRE"] = "Trimestre 1";
                }
                else
                {
                    if ((_fecha.Month >= 4) && (_fecha.Month <= 6))
                    {
                        _fila["TRIMESTRE"] = "Trimestre 2";
                    }
                    else
                    {
                        if ((_fecha.Month >= 7) && (_fecha.Month <= 9))
                        {
                            _fila["TRIMESTRE"] = "Trimestre 3";
                        }
                        else
                        {
                            _fila["TRIMESTRE"] = "Trimestre 4";
                        }
                    }
                }

                _dataTable.AcceptChanges();
            }

            return _dataTable;
        }



        public DataTable ObtenerRegistroHistorialAjustePresupuesto(Decimal ID_HIST_AJUSTE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_hist_ajustes_presupuesto_actividades_obtenerPoridHistAjuste ";

            sql += ID_HIST_AJUSTE.ToString();

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

        public DataTable ObtenerRegistroHistorialReprogramacionActividad(Decimal ID_HISTORIAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_hist_reprog_actividades_obtenerPorIdHistorial ";

            sql += ID_HISTORIAL.ToString();

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


        public DataTable ObtenerUsuariosProgramasRseSaludBienestar(Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtenerUsuariosRseBienestarSaludOperaciones ";

            sql += "'" + AREA.ToString() + "'";

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


        public DataTable ObtenerUsuariosSistemaActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtenerUsuariosActivosSistema";

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

        public DataTable ObtenerUsuariosSistemaActivosPorUnidadNegocioEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtenerUsuariosActivosUnidadNegocioEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString();
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_EMPRESA no puede ser 0.";
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

        public Boolean ActualizacionFinalDetalleActividad(Decimal ID_DETALLE,
            String CONCLUSIONES,
            String DIR_IMAGEN_REPRESENTATIVA)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_detalle_actividades_actualizarFinal ";

            #region validaciones
            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE + ", ";
                informacion += "ID_DETALLE = '" + ID_DETALLE + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONCLUSIONES) == false)
            {
                sql += "'" + CONCLUSIONES + "', ";
                informacion += "CONCLUSIONES = '" + CONCLUSIONES + "', ";
            }
            else
            {
                MensajeError = "El campo CONCLUSIONES no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DIR_IMAGEN_REPRESENTATIVA) == false)
            {
                sql += "'" + DIR_IMAGEN_REPRESENTATIVA + "', ";
                informacion += "DIR_IMAGEN_REPRESENTATIVA = '" + DIR_IMAGEN_REPRESENTATIVA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DIR_IMAGEN_REPRESENTATIVA = 'null', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DETALLE_ACTIVIDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
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


        public DataTable ObtenerHistorialProgramasEmpresaYArea(Areas area, Decimal id_empresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_maestra_programas_obtenerPorIdEmpresaIdArea ";

            sql += "'" + area.ToString() + "', " + id_empresa.ToString();

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

        public DataTable ObtenerListaActividadesParaPDFInformeClientes(Decimal ID_PROGRAMA,
            Decimal ID_CORTE,
            DateTime FECHA_INICIAL,
            DateTime FECHA_FINAL)
        {

            tools _tools = new tools();

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "RPT_PROGRAMAS_PDF_INFORME_CLIENTES ";
            if (ID_PROGRAMA != 0)
            {
                sql += ID_PROGRAMA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROGRAMA no puede ser 0.";
            }

            if (ID_CORTE != 0)
            {
                sql += ID_CORTE + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CORTE no puede ser 0.";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "'";

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

        public DataTable ObtenerActividadesALasQueAsistioEmpleadoPorArea(Areas AREA, Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_control_asistencia_actividades_obtenerPorIdEMpleadoIdArea ";

            sql += "'" + AREA.ToString() + "', " + ID_EMPLEADO.ToString();

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

        public DataTable ObtenerAsignacionesDeUnaActividadAProgramaEspecifico(Decimal ID_DETALLE_GENERAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_actividades_obtener_por_id_detalle_general ";

            sql += ID_DETALLE_GENERAL;

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


        public Boolean DesactivarProgGeneralDetalle(Decimal ID_DETALLE_GENERAL)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_general_detalle_desactivar ";

            #region validaciones
            if (ID_DETALLE_GENERAL != 0)
            {
                sql += ID_DETALLE_GENERAL + ", ";
                informacion += "ID_DETALLE_GENERAL = '" + ID_DETALLE_GENERAL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE_GENERAL no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_GENERAL_DETALLE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
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

        public DataTable ObtenerCorteinformeClientePorIdProgramaYPeriodo(Decimal ID_PROGRAMA, DateTime FECHA_INICIAL, DateTime FECHA_FINAL)
        {
            tools _tools = new tools();

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_cortes_informes_clientes_obtenerPorIdProgramaYPeriodo ";

            if (ID_PROGRAMA != 0)
            {
                sql += ID_PROGRAMA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROGRAMA no puede ser vacio.";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "'";

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

        public Decimal AdicionarCorteInformeCliente(Decimal ID_PROGRAMA,
            DateTime FECHA_INICIAL,
            DateTime FECHA_FINAL,
            String CONCLUSIONES)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_CORTE = 0;

            sql = "usp_prog_cortes_informes_clientes_adicionar_o_actualziar ";
            if (ID_PROGRAMA != 0)
            {
                sql += ID_PROGRAMA + ", ";
                informacion += "ID_PROGRAMA = '" + ID_PROGRAMA + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROGRAMA no puede ser vacio.";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";
            informacion += "FECHA_INICIAL = '" + FECHA_INICIAL.ToShortDateString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "', ";
            informacion += "FECHA_FINAL = '" + FECHA_FINAL.ToShortDateString() + "', ";

            if (String.IsNullOrEmpty(CONCLUSIONES) == false)
            {
                sql += "'" + CONCLUSIONES + "', ";
                informacion += "CONCLUSIONES = '" + CONCLUSIONES + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo CONCLUSIONES no puede ser vacio.";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    ID_CORTE = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_CORTES_INFORMES_CLIENTES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_CORTE = 0;
                    conexion.DeshacerTransaccion();
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                ID_CORTE = 0;
            }

            return ID_CORTE;
        }


        public Boolean FinalizarCompromiso(Decimal ID_MAESTRA_COMPROMISO,
            String ESTADO,
            DateTime FECHA_EJECUCION,
            String HORA_EJECUCION,
            String DESCRIPCION_FINAL)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_maestra_compromisos_finalizar ";

            #region validaciones
            if (ID_MAESTRA_COMPROMISO != 0)
            {
                sql += ID_MAESTRA_COMPROMISO + ", ";
                informacion += "ID_MAESTRA_COMPROMISO = '" + ID_MAESTRA_COMPROMISO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_MAESTRA_COMPROMISO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_EJECUCION) + "', ";
            informacion += "FECHA_EJECUCION = '" + FECHA_EJECUCION.ToShortDateString() + "', ";

            if (String.IsNullOrEmpty(HORA_EJECUCION) == false)
            {
                sql += "'" + HORA_EJECUCION + "', ";
                informacion += "HORA_EJECUCION = '" + HORA_EJECUCION + "', ";
            }
            else
            {
                MensajeError = "El campo HORA_EJECUCION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION_FINAL) == false)
            {
                sql += "'" + DESCRIPCION_FINAL + "', ";
                informacion += "DESCRIPCION_FINAL = '" + DESCRIPCION_FINAL + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION_FINAL no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_MAESTRA_COMPROMISOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
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

        public DataTable ObtenerDiagnonsticosAgrupadosParaUnIdSolicitud(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_reg_incapacidades_obtener_diagnosticos_por_id_solicitud ";
            sql += ID_SOLICITUD;

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


        public DataTable ObtenerIncapacidadesParaUnIdSolicitud(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_reg_incapacidades_obtener_incapacidades_por_id_solicitud ";
            sql += ID_SOLICITUD;

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


        public DataTable ObtenerFactoresAccidentalidadPorFactor(String factor)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_so_par_causas_basicas_perdidas_obtenerPorFactor ";

            sql += "'" + factor + "'";

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

        public DataTable ObtenerFactoresPorAccidenteYTipoFactor(Decimal idAccidente, String factor)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_so_causas_basicas_perdidas_por_accidente_obtenerPorIdAccidenteYTipoFactor ";

            sql += idAccidente + ", '" + factor + "'";

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

    }
}
