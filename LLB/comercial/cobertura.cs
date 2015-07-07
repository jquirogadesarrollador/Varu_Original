using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.comercial
{
    public class cobertura
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        private Decimal _ID_EMPRESA = 0;
        private String _ID_CIUDAD = null;
        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String IDCIUDAD
        {
            get { return _ID_CIUDAD; }
            set { _ID_CIUDAD = value; }
        }

        public Decimal IDEMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }

        #endregion propiedades

        #region constructores
        public cobertura(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion

        #region metodos
        public Boolean Adicionar(Decimal ID_EMPRESA, String ID_CIUDAD, String USU_CRE, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_ven_cobertura_adicionar ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA + ", ";
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD))) sql += "'" + ID_CIUDAD + "'";
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_CRE, tabla.VEN_COBERTURA, tabla.ACCION_ADICIONAR, sql, null, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    throw new Exception("Error originado en logica de negocio(clase:. cobertura metodo:. adicionar): " + e.Message.ToString());
                }
            }
            return ejecutadoCorrectamente;
        }

        public Boolean ActualizarPermisosParaCiudaYEmpresaEnGeneral(Decimal ID_EMPRESA, String ID_CIUDAD, String USU_CRE, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_actualizar_permisos_todos_usuarios_para_empresa_y_ciudad ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA + ", ";
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD))) sql += "'" + ID_CIUDAD + "'";
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_CRE, tabla.VEN_COBERTURA, tabla.ACCION_ADICIONAR, sql, null, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    throw new Exception("Error originado en logica de negocio(clase:. cobertura metodo:. adicionar): " + e.Message.ToString());
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }
            return ejecutadoCorrectamente;
        }

        public Boolean Eliminar(Decimal ID_EMPRESA, String USU_ELI, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_ven_cobertura_eliminar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0.";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_ELI, tabla.VEN_COBERTURA, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    throw new Exception("Error originado en logica de negocio(clase:. cobertura metodo:. eliminar): " + e.Message.ToString());
                }
            }
            return ejecutadoCorrectamente;
        }

        public Boolean EliminarUnaCiudadDeLaCoberturaDeCliente(Decimal ID_EMPRESA, String ID_CIUDAD, String USU_ELI, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_ven_cobertura_eliminar_una_ciudad ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "'";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser VACIO.";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_ELI, tabla.VEN_COBERTURA, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    throw new Exception("Error originado en logica de negocio(clase:. cobertura metodo:. eliminar): " + e.Message.ToString());
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }
            return ejecutadoCorrectamente;
        }

        public DataTable obtenerCoberturaDeUnCliente(Decimal idEmpresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_cobertura_buscar_por_id_empresa ";
            if (idEmpresa != 0)
            {
                sql += idEmpresa;

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
                MensajeError = "ADVERTENCIA: El campo ID_EMPRESA no puede ser 0. \n";
            }

            return _dataTable;
        }

        public DataTable obtenerCoberturaDeUnCliente(Decimal idEmpresa, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_cobertura_buscar_por_id_empresa ";
            if (idEmpresa != 0)
            {
                sql += idEmpresa;

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
            else
            {
                MensajeError = "ADVERTENCIA: El campo ID_EMPRESA no puede ser 0. \n";
            }

            return _dataTable;
        }

        public DataTable obtenerNombreCiudadPorIdCiudad(String ID_CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ciudad_obtenerPorIdCiudad ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "'";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser 0\n";
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

        public DataTable obtenerCoberturaPorIdCiudad(String ID_CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_cobertura_buscar_por_id_ciudad ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "'";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser 0\n";
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