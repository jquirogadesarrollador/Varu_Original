using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.seleccion
{
    public class pruebaPerfil
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private Decimal _ID_EMPRESA = 0;
        private String _ID_PRUEBA = null;
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

        public String IDPRUEBA
        {
            get { return _ID_PRUEBA; }
            set { _ID_PRUEBA = value; }
        }

        public Decimal IDEMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }

        #endregion propiedades

        #region constructores
        public pruebaPerfil(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos
        public Boolean Adicionar(Decimal ID_PERFIL, Decimal ID_PRUEBA, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_sel_reg_pruebas_perfil_adicionar ";

            if (ID_PERFIL != 0) sql += ID_PERFIL + ", ";
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_PRUEBA != 0) sql += ID_PRUEBA + ", ";
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede vacio. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_PRUEBAS_PERFIL, tabla.ACCION_ADICIONAR, sql, null, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    MensajeError = "Error originado en logica de negocio(clase:. pruebaPerfil metodo:. adicionar): " + e.Message.ToString();
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }

        public Boolean Eliminar(Decimal ID_PERFIL, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_sel_reg_pruebas_perfil_eliminar ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
                informacion += "ID_PERFIL = " + ID_PERFIL.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID PERFIL no puede ser 0.";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_PRUEBAS_PERFIL, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    MensajeError = "Error originado en logica de negocio(clase:. pruebaPerfil metodo:. eliminar): " + e.Message.ToString();
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }


        public Boolean EliminarPruebaDePerfil(Decimal ID_PERFIL, Decimal ID_PRUEBA, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_sel_reg_pruebas_perfil_eliminar_por_id_prueba ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID PERFIL no puede ser 0.";
                ejecutar = false;
            }

            if (ID_PRUEBA != 0)
            {
                sql += ID_PRUEBA;
                informacion += "ID_PRUEBA = '" + ID_PRUEBA + "'";
            }
            else
            {
                MensajeError = "El campo ID_PRUEBA no puede ser 0.";
                ejecutar = false;
            }
            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_PRUEBAS_PERFIL, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    MensajeError = "Error originado en logica de negocio(clase:. pruebaPerfil metodo:. eliminar): " + e.Message.ToString();
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }

        public DataTable ObtenerPorIdPerfil(Decimal idPerfil)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_sel_reg_pruebas_perfil_buscarPorIdPefil " + idPerfil;
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



        public DataTable ObtenerPruebasVSCargoPorEmpresa(Decimal idEmpresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_pruebas_vs_cargos_por_id_empresa " + idEmpresa;
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

        public DataTable ObtenerPorIdPerfil(Decimal idPerfil, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_sel_reg_pruebas_perfil_buscarPorIdPefil " + idPerfil;
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

            return _dataTable;
        }

        public DataTable ObtenerPorIdPerfilConResultadosIdSolicitud(Decimal idPerfil, Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_sel_reg_pruebas_perfil_buscarPorIdPefilConResultadosDeIdSolicidtud " + idPerfil + ", " + ID_SOLICITUD.ToString();
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

        public DataTable ObtenerAplicadasAIdSolicitudConResultados(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_sel_reg_pruebas_buscarAplicadasASolicitudIngreso " + ID_SOLICITUD.ToString();
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


        #endregion metodos
    }
}
