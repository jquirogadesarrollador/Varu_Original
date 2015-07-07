using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.contratacion
{
    public class examenesPorPerfil
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
        public examenesPorPerfil(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos
        public Decimal AdicionarCondicionContratacion(Decimal ID_EMPRESA, String DOC_TRAB, Decimal ID_PERFIL, Decimal RIESGO, String OBS_CTE)
        {
            String registro = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_contratacion_adicionar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DOC_TRAB)))
            {
                sql += "'" + DOC_TRAB + "', ";
                informacion += "DOC_TRAB = '" + DOC_TRAB.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DOC_TRAB no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = " + ID_PERFIL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (RIESGO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(RIESGO) + ", ";
                informacion += "RIESGO = " + _tools.convierteComaEnPuntoParaDecimalesEnSQL(RIESGO) + ", ";
            }
            else
            {
                MensajeError = "El campo RIESGO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_CTE)))
            {
                sql += "'" + OBS_CTE + "', ";
                informacion += "OBS_CTE = '" + OBS_CTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CTE no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    registro = conexion.ExecuteScalar(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_P_CONTRATACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(registro))) return Convert.ToDecimal(registro);
            else return 0;
        }

        public Boolean ActualizarCondicionContratacion(Decimal ID_PERFIL, Decimal ID_EMPRESA, String DOC_TRAB, Decimal RIESGO, String OBS_CTE)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_contratacion_actualizar ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = " + ID_PERFIL.ToString() + ",";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DOC_TRAB)))
            {
                sql += "'" + DOC_TRAB + "', ";
                informacion += "DOC_TRAB = '" + DOC_TRAB.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DOC_TRAB no puede ser nulo. \n";
                ejecutar = false;
            }

            if (RIESGO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(RIESGO) + ", ";
                informacion += "RIESGO = " + _tools.convierteComaEnPuntoParaDecimalesEnSQL(RIESGO) + ",";
            }
            else
            {
                MensajeError = "El campo RIESGO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_CTE)))
            {
                sql += "'" + OBS_CTE + "', ";
                informacion += "OBS_CTE = '" + OBS_CTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CTE no puede ser nulo. \n";
                ejecutar = false;

            }
            sql += "'" + Usuario + "' ";

            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_P_CONTRATACION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerCondicionComercialPorIdPerfil(Decimal ID_PERFIL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfil ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
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
        #endregion metodos
    }
}
