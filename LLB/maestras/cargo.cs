using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Brainsbits.LLB.seguridad;
using Brainsbits.LDA;

namespace Brainsbits.LLB.maestras
{
    public class cargo
    {
        #region variables
        public enum Empresas
        {
            Sertempo = 1,
            EYS = 3
        }

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
        public cargo(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        #endregion constructores

        #region metodos

        public DataTable ObtenerTodosLosGrandesGrupos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_grandes_grupo_buscar_todos ";
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

        public DataTable ObtenerSubGruposPrincipalesPorIdGrandeGrupo(String idGrandeGrupo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_subgrupo_principal_buscarPorIdGrandeGrupo ";

            if (!(String.IsNullOrEmpty(idGrandeGrupo)))
            {
                sql += "'" + idGrandeGrupo + "'";

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
            else
            {
                MensajeError = "El campo ID GRANDE GRUPO no puede ser null. \n";
            }


            return _dataTable;
        }

        public DataTable ObtenerSubGruposPorIdSubGrupoPrincipal(String idSubGrupoPrincipal)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_subgrupo_buscarPorIdSubGrupoPrincipal ";

            if (!(String.IsNullOrEmpty(idSubGrupoPrincipal)))
            {
                sql += "'" + idSubGrupoPrincipal + "'";

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
            else
            {
                MensajeError = "El campo ID SUBGRUPO PRINCIPAL no puede ser null. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerGruposPrimariosPorIdSubGrupo(String idSubGrupo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_grupo_primarios_buscarPorIdSubGrupo ";

            if (!(String.IsNullOrEmpty(idSubGrupo)))
            {
                sql += "'" + idSubGrupo + "'";

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
            else
            {
                MensajeError = "El campo ID SUBGRUPO no puede ser null. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerGruposPrimariosPorDescripcion(String datoABuscar)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_grupo_primarios_buscarPorDescripcion ";

            if (!(String.IsNullOrEmpty(datoABuscar)))
            {
                sql += "'" + datoABuscar + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo DATOS A BUSCAR no puede ser nulo.";
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

        public DataTable ObtenerGruposPrimariosPorIdGrupoPrimario(String idGrupoPrimario)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_grupo_primarios_buscarPorIdGrupoPrimario ";

            if (!(String.IsNullOrEmpty(idGrupoPrimario)))
            {
                sql += "'" + idGrupoPrimario + "'";

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
            else
            {
                MensajeError = "El campo idGrupoPrimario no puede ser null. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerTodoPorIdGrupoPrimario(String idGrupoPrimario)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_todo_por_id_grupo_primario ";

            if (!(String.IsNullOrEmpty(idGrupoPrimario)))
            {
                sql += "'" + idGrupoPrimario + "'";

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
            else
            {
                MensajeError = "El campo idGrupoPrimario no puede ser null. \n";
            }

            return _dataTable;
        }

        #region cargos

        public Decimal AdicionarRecOcupaciones(Decimal ID_EMP,
            String COD_OCUPACION,
            String NOM_OCUPACION,
            String DSC_FUNCIONES,
            String COMISIONA,
            String OBLIGACIONES,
            String RESPONSABILIDADES)
        {
            String sql = null;
            String idRecOcupaciones = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_adicionar ";

            #region validaciones

            if (ID_EMP != 0)
            {
                sql += ID_EMP + ", ";
                informacion += "ID_EMP = '" + ID_EMP.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EMP = '0', ";
            }

            if (!(String.IsNullOrEmpty(COD_OCUPACION)))
            {
                sql += "'" + COD_OCUPACION + "', ";
                informacion += "COD_OCUPACION = '" + COD_OCUPACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COD_OCUPACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_OCUPACION)))
            {
                sql += "'" + NOM_OCUPACION + "', ";
                informacion += "NOM_OCUPACION = '" + NOM_OCUPACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_OCUPACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DSC_FUNCIONES)))
            {
                sql += "'" + DSC_FUNCIONES + "', ";
                informacion += "DSC_FUNCIONES = '" + DSC_FUNCIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DSC_FUNCIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COMISIONA)))
            {
                sql += "'" + COMISIONA + "', ";
                informacion += "COMISIONA = '" + COMISIONA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COMISIONA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(OBLIGACIONES) == false)
            {
                sql += "'" + OBLIGACIONES + "', ";
                informacion += "OBLIGACIONES = '" + OBLIGACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBLIGACIONES = 'null', ";
            }

            if (String.IsNullOrEmpty(RESPONSABILIDADES) == false)
            {
                sql += "'" + RESPONSABILIDADES + "'";
                informacion += "RESPONSABILIDADES = '" + RESPONSABILIDADES + "'";
            }
            else
            {
                sql += "null";
                informacion += "RESPONSABILIDADES = 'null'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecOcupaciones = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecOcupaciones))) return Convert.ToDecimal(idRecOcupaciones);
            else return 0;
        }

        public Boolean ActualizarRecOcupaciones(Decimal ID_OCUPACION,
            Decimal ID_EMP,
            String COD_OCUPACION,
            String NOM_OCUPACION,
            String DSC_FUNCIONES,
            String ACTIVO,
            String COMISIONA,
            String OBLIGACIONES,
            String RSPONSABILIDADES)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_ocupaciones_actualizar ";

            #region validaciones
            if (ID_OCUPACION != 0)
            {
                sql += ID_OCUPACION + ", ";
                informacion += "ID_OCUPACION = '" + ID_OCUPACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_OCUPACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMP != 0)
            {
                sql += ID_EMP + ", ";
                informacion += "ID_EMP = '" + ID_EMP.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_EMP = '0', ";
            }

            if (!(String.IsNullOrEmpty(COD_OCUPACION)))
            {
                sql += "'" + COD_OCUPACION + "', ";
                informacion += "COD_OCUPACION = '" + COD_OCUPACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COD_OCUPACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_OCUPACION)))
            {
                sql += "'" + NOM_OCUPACION + "', ";
                informacion += "NOM_OCUPACION = '" + NOM_OCUPACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_OCUPACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DSC_FUNCIONES)))
            {
                sql += "'" + DSC_FUNCIONES + "', ";
                informacion += "DSC_FUNCIONES = '" + DSC_FUNCIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DSC_FUNCIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ACTIVO)))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COMISIONA)))
            {
                sql += "'" + COMISIONA + "', ";
                informacion += "COMISIONA = '" + COMISIONA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COMISIONA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(OBLIGACIONES) == false)
            {
                sql += "'" + OBLIGACIONES + "', ";
                informacion += "OBLIGACIONES = '" + OBLIGACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBLIGACIONES = 'NULL', ";
            }

            if (String.IsNullOrEmpty(RSPONSABILIDADES) == false)
            {
                sql += "'" + RSPONSABILIDADES + "'";
                informacion += "RSPONSABILIDADES = '" + RSPONSABILIDADES + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "RSPONSABILIDADES = 'NULL'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecOcupacionesPorCodOcupacion(String COD_OCUPACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_por_cod_ocupacion ";

            if (!(String.IsNullOrEmpty(COD_OCUPACION)))
            {
                sql += "'" + COD_OCUPACION + "'";
                informacion += "COD_OCUPACION  = '" + COD_OCUPACION.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo COD_OCUPACION  no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecOcuacionesPorCodOcupacionActivos(String COD_OCUPACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_por_cod_ocupacion_activos ";

            if (!(String.IsNullOrEmpty(COD_OCUPACION)))
            {
                sql += "'" + COD_OCUPACION + "'";
                informacion += "COD_OCUPACION  = '" + COD_OCUPACION.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo COD_OCUPACION  no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecOcupacionesPorNomOcupacion(String NOM_OCUPACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_por_nom_ocupacion ";

            if (!(String.IsNullOrEmpty(NOM_OCUPACION)))
            {
                sql += "'" + NOM_OCUPACION + "'";
                informacion += "NOM_OCUPACION = '" + NOM_OCUPACION.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NOM_OCUPACION no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecOcupacionesPorNomOcupacionSoloGenericos(String NOM_OCUPACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_por_nom_ocupacion_solo_genericos ";

            if (!(String.IsNullOrEmpty(NOM_OCUPACION)))
            {
                sql += "'" + NOM_OCUPACION + "'";
                informacion += "NOM_OCUPACION = '" + NOM_OCUPACION.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NOM_OCUPACION no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecOcupacionesPorIdEmp(Decimal ID_EMP)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_por_id_emp ";

            if (ID_EMP != 0)
            {
                sql += ID_EMP;
                informacion += "ID_EMP = '" + ID_EMP.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMP no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecOcupacionesPorRazSocial(String RAZ_SOCIAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_por_raz_social ";

            if (String.IsNullOrEmpty(RAZ_SOCIAL) == false)
            {
                sql += "'" + RAZ_SOCIAL + "'";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL + "'";
            }
            else
            {
                MensajeError += "El campo RAZ_SOCIAL no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerRecOcupacionesPorTodo()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_todo ";

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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecOcupacionesPorTodoSoloCargosGenericos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_todo_solo_genericos ";

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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerOcupacionPorIdOcupacion(Decimal ID_OCUPACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtener_por_id ";

            if (ID_OCUPACION != 0) sql += ID_OCUPACION;
            else
            {
                MensajeError = "El campo ID_OCUPACION no puede ser 0\n";
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

        #endregion cargos

        #endregion metodos
    }
}