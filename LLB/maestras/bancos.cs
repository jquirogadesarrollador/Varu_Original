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
    public class bancos
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private Decimal _REGISTRO_BANCO = 0;
        private Decimal _REGISTRO_CON_REG_BANCOS_EMPRESA;
        private Decimal _ID_EMPRESA = 0;
        private String _ID_CIUDAD = null;

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
        public Decimal REGISTRO_BANCO
        {
            get { return _REGISTRO_BANCO; }
            set { _REGISTRO_BANCO = value; }
        }
        public Decimal REGISTRO_CON_REG_BANCOS_EMPRESA
        {
            get { return _REGISTRO_CON_REG_BANCOS_EMPRESA; }
            set { _REGISTRO_CON_REG_BANCOS_EMPRESA = value; }
        }
        public Decimal ID_EMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }
        public String ID_CIUDAD
        {
            get { return _ID_CIUDAD; }
            set { _ID_CIUDAD = value; }
        }
        #endregion propiedades

        #region constructores
        public bancos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodosLosBancos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_df_bancos_buscar_todos";
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

            return _dataTable;
        }

        public DataTable ObtenerCiudadesCuentas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_df_ciudades_obtener_todas";
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

            return _dataTable;
        }

        public DataTable ObtenerBancosPorId(int ID_ENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_df_bancos_buscar_por_id ";

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD;
                informacion += "ID_ENTIDAD  = '" + ID_ENTIDAD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_ENTIDAD  no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.DF_BANCOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
    }
}
