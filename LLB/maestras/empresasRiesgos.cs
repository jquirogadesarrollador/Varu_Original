using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class empresasRiesgos
    {
        #region varialbes

        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        private Decimal _ID_EMPRESA = 0;
        private String _DESCRIPCION_RIESGO = null;
        #endregion varialbes

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Decimal ID_EMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }

        public String DESCRIPCION_RIESGO
        {
            get { return _DESCRIPCION_RIESGO; }
            set { _DESCRIPCION_RIESGO = value; }
        }
        #endregion propiedades

        #region constructores
        public empresasRiesgos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerRoesgosPorEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_riesgos_obtener_activos_por_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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

        public DataTable ObtenerRoesgosPorEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_riesgos_obtener_activos_por_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }
            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            return _dataTable;
        }

        public Decimal Adicionar(Decimal ID_EMPRESA, String DESCRIPCION_RIESGO, Conexion conexion)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_riesgos_adicionar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION_RIESGO)))
            {
                sql += "'" + DESCRIPCION_RIESGO + "', ";
                informacion += "DESCRIPCION_RIESGO = '" + DESCRIPCION_RIESGO + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION_RIESGO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS_RIESGOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean desactivarRiesgoParaEmpresa(Decimal ID_EMPRESA, String DESCRIPCION_RIESGO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_riesgos_desactivar ";

            #region validaciones
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

            if (!(String.IsNullOrEmpty(DESCRIPCION_RIESGO)))
            {
                sql += "'" + DESCRIPCION_RIESGO + "', ";
                informacion += "DESCRIPCION_RIESGO = '" + DESCRIPCION_RIESGO + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION_RIESGO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS_RIESGOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }
        #endregion metodos
    }
}