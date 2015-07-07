using System;

using System.Data;

using Brainsbits.LDA;

using System.Data;
using System.IO;

using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class liquidacionMemorando
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
        public liquidacionMemorando(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores
        #region metodos

        public String ValidarMemorando(Decimal ID_EMPRESA, Int32 ID_PERIODO, String PERIODOSPROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;
            String Validado = "N";
            String informacion = null;


            sql = "usp_validar_memorando ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA ='" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }
            sql += ID_PERIODO.ToString() + ", ";
            informacion = "ID_PERIODO ='" + ID_PERIODO.ToString() + "', ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
                informacion += "Usuario ='" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + PERIODOSPROCESO.ToString() + "'";
            informacion += "PERIODOS_PROCESO ='" + PERIODOSPROCESO.ToString() + "'";
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                    Validado = "S";
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_VALIDAR, sql, informacion, conexion);
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
            return Validado;
        }

        public String PagarMemorando(Int32 ID_PERIODO, Int32 ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_pagar_memorando ";

            #region validaciones
            if (ID_PERIODO > 0)
            {
                sql += ID_PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
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
            return "S";
        }

        public String EliminarMemorando(String PERIODOS_MEMOS)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_eliminar_memorando ";

            #region validaciones
            if (!(String.IsNullOrEmpty(PERIODOS_MEMOS)))
            {
                sql += "'" + PERIODOS_MEMOS.ToString() + "' ";
            }
            else
            {
                MensajeError = "El campo PERIODOS_MEMOS es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
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
            return "S";
        }

        public DataTable ObtenerMemorando(Int32 ID_EMPRESA)
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
                MensajeError = "El campo ID_EMPRESA es requerido para la eliminacion.";
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

        public String ReversarMemo(String ESTADO, Int32 ID_PERIODO, Decimal idEmpresa)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean ejecutar = true;
            String sql = null;

            sql = "usp_reversar_memorando ";

            #region validaciones
            if (ID_PERIODO != 0)
            {
                sql += "'" + ID_PERIODO.ToString() + "' ";
            }
            else
            {
                MensajeError = "El campo ID_PERIODO es requerido para la reversion.";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += ", '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + idEmpresa.ToString() + "'";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
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
            return "S";
        }

        #endregion metodos
    }
}
