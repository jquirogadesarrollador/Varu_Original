using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class fuentesReclutamiento
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
        public fuentesReclutamiento(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #region fuentesReclutamiento

        public Decimal AdicionarRecFuentes(String NOM_FUENTE,
            String DIR_FUENTE,
            String CIU_FUENTE,
            String TEL_FUENTE,
            String ENCARGADO,
            String CARGO_ENC,
            String OBS_FUENTE,
            String EMAIL_ENCARGADO)
        {
            String sql = null;
            String idRecFuentes = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_fuentes_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NOM_FUENTE)))
            {
                sql += "'" + NOM_FUENTE + "', ";
                informacion += "NOM_FUENTE = '" + NOM_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(DIR_FUENTE)))
            {
                sql += "'" + DIR_FUENTE + "', ";
                informacion += "DIR_FUENTE = '" + DIR_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIR_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CIU_FUENTE)))
            {
                sql += "'" + CIU_FUENTE + "', ";
                informacion += "CIU_FUENTE = '" + CIU_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIU_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_FUENTE)))
            {
                sql += "'" + TEL_FUENTE + "', ";
                informacion += "TEL_FUENTE = '" + TEL_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TEL_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ENCARGADO)))
            {
                sql += "'" + ENCARGADO + "', ";
                informacion += "ENCARGADO = '" + ENCARGADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ENCARGADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CARGO_ENC)))
            {
                sql += "'" + CARGO_ENC + "', ";
                informacion += "CARGO_ENC = '" + CARGO_ENC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO_ENC no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_FUENTE)))
            {
                sql += "'" + OBS_FUENTE + "', ";
                informacion += "OBS_FUENTE = '" + OBS_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(EMAIL_ENCARGADO)))
            {
                sql += "'" + EMAIL_ENCARGADO + "'";
                informacion += "EMAIL_ENCARGADO = '" + EMAIL_ENCARGADO + "'";
            }
            else
            {
                MensajeError += "El campo EMAIL_ENCARGADO no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecFuentes = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecFuentes))) return Convert.ToDecimal(idRecFuentes);
            else return 0;
        }

        public Boolean ActualizarRecFuentes(Decimal ID_FUENTE,
            String NOM_FUENTE,
            String DIR_FUENTE,
            String CIU_FUENTE,
            String TEL_FUENTE,
            String ENCARGADO,
            String CARGO_ENC,
            String OBS_FUENTE,
            String EMAIL_ENCARGADO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_fuentes_actualizar ";

            #region validaciones
            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_FUNCIONARIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NOM_FUENTE)))
            {
                sql += "'" + NOM_FUENTE + "', ";
                informacion += "NOM_FUENTE = '" + NOM_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(DIR_FUENTE)))
            {
                sql += "'" + DIR_FUENTE + "', ";
                informacion += "DIR_FUENTE = '" + DIR_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIR_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CIU_FUENTE)))
            {
                sql += "'" + CIU_FUENTE + "', ";
                informacion += "CIU_FUENTE = '" + CIU_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIU_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_FUENTE)))
            {
                sql += "'" + TEL_FUENTE + "', ";
                informacion += "TEL_FUENTE = '" + TEL_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TEL_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ENCARGADO)))
            {
                sql += "'" + ENCARGADO + "', ";
                informacion += "ENCARGADO = '" + ENCARGADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ENCARGADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CARGO_ENC)))
            {
                sql += "'" + CARGO_ENC + "', ";
                informacion += "CARGO_ENC = '" + CARGO_ENC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO_ENC no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_FUENTE)))
            {
                sql += "'" + OBS_FUENTE + "', ";
                informacion += "OBS_FUENTE = '" + OBS_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(EMAIL_ENCARGADO)))
            {
                sql += "'" + EMAIL_ENCARGADO + "'";
                informacion += "EMAIL_ENCARGADO = '" + EMAIL_ENCARGADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo EMAIL_ENCARGADO no puede ser nulo\n";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecFuentesPorNomFuente(String nom_fuente)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_fuentes_obtener_por_nom_fuente ";

            if (!(String.IsNullOrEmpty(nom_fuente)))
            {
                sql += "'" + nom_fuente + "'";
                informacion += "NOM_FUENTE = '" + nom_fuente.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NOM_FUENTE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        
        public DataTable ObtenerRecFuentesPorEncargado(String ENCARGADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_fuentes_obtener_por_encargado ";

            if (!(String.IsNullOrEmpty(ENCARGADO)))
            {
                sql += "'" + ENCARGADO + "'";
                informacion += "ENCARGADO = '" + ENCARGADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ENCARGADO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecFuentesPorCiuFuente(String CIU_FUENTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_fuentes_obtener_por_ciu_fuente ";

            if (!(String.IsNullOrEmpty(CIU_FUENTE)))
            {
                sql += "'" + CIU_FUENTE + "'";
                informacion += "CIU_FUENTE = '" + CIU_FUENTE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo CIU_FUENTE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        
        public DataTable ObtenerRecFuentesPorIdFuente(int ID_FUENTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_fuentes_obtener_por_id_fuente ";

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE;
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRecFuentesTodos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_fuentes_obtener_todos ";

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
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        #endregion fuentesReclutamiento

        #region cargosFuentes

        public Decimal AdicionarRecOcupacionesFuente(Decimal ID_FUENTE, Decimal ID_OCUPACION, String OBSERVACIONES, String LLAVE)
        {
            String sql = null;
            String idRecOcupacionesFuente = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_ocupaciones_fuente_adicionar ";

            #region validaciones

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
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
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LLAVE)))
            {
                sql += "'" + LLAVE + "', ";
                informacion += "LLAVE = '" + LLAVE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LLAVE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecOcupacionesFuente = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES_FUENTE, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecOcupacionesFuente))) return Convert.ToDecimal(idRecOcupacionesFuente);
            else return 0;
        }

        public Boolean ActualizarRecOcupacionesFuentes(int REGISTRO, int ID_FUENTE, int ID_OCUPACION, String OBSERVACIONES, String LLAVE)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_ocupaciones_fuente_actualizar ";

            #region validaciones

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
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
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LLAVE)))
            {
                sql += "'" + LLAVE + "', ";
                informacion += "LLAVE = '" + LLAVE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LLAVE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES_FUENTE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean OcultarRecFuentes(Decimal ID_FUENTE)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_fuentes_ocultar ";

            #region validaciones

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_FUENTES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public DataTable ObtenerRecOcupacionesFuentesPorFuente(Decimal ID_FUENTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_fuente_obtener_por_fuente ";

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE;
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES_FUENTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        
        public DataTable ObtenerRecOcupacionessFuentesPorOcupacion(int ID_OCUPACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_fuente_obtener_por_ocupacion ";

            if (ID_OCUPACION != 0)
            {
                sql += ID_OCUPACION;
                informacion += "ID_OCUPACION = '" + ID_OCUPACION.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_OCUPACION no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_OCUPACIONES_FUENTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        #endregion cargosFuentes


        #region comunicacionFuentes

        public Decimal AdicionarRecComFuentes(Decimal ID_FUENTE, DateTime FECHA_R, String OBSERVACIONES)
        {
            String sql = null;
            String idRecComFuentes = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_com_fuentes_adicionar ";

            #region validaciones

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE= '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecComFuentes = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_COM_FUENTES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecComFuentes))) return Convert.ToDecimal(idRecComFuentes);
            else return 0;
        }
        
        public Boolean ActualizarRecComFuentes(int REGISTRO, int ID_FUENTE, DateTime FECHA_R, String OBSERVACIONES)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_rec_com_fuentes_actualizar ";

            #region validaciones
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
            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE + ", ";
                informacion += "ID_FUENTE= '" + ID_FUENTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += FECHA_R + ", ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REC_COM_FUENTES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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
        
        public DataTable ObtenerRecComFuentesPorIdFuente(Decimal ID_FUENTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_rec_com_fuentes_obtener_por_id_fuente ";

            if (ID_FUENTE != 0)
            {
                sql += ID_FUENTE;
                informacion += "ID_FUENTE = '" + ID_FUENTE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_FUENTE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.REC_COM_FUENTES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        #endregion comunicacionFuentes

        #endregion metodos
    }
}
