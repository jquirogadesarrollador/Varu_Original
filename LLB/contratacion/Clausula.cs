using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;


namespace Brainsbits.LLB.contratacion
{
    public class Clausula
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje = null;

        private decimal _id_clausula;
        private decimal _id_empleado;
        private Byte[] _archivo;
        private Int32 _archivo_tamaño;
        private string _archivo_extension;
        private string _archivo_tipo;
        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public decimal IdClausua
        {
            get { return _id_clausula; }
            set { _id_clausula = value; }
        }

        public decimal IdEmpleado
        {
            get { return _id_empleado; }
            set { _id_empleado = value; }
        }

        public Byte[] Archivo
        {
            get { return _archivo; }
            set { _archivo = value; }
        }

        public Int32 ArchivoTamaño
        {
            get { return _archivo_tamaño; }
            set { _archivo_tamaño = value; }
        }

        public string ArchivoExtension
        {
            get { return _archivo_extension; }
            set { _archivo_extension = value; }
        }

        public string ArchivoTipo
        {
            get { return _archivo_tipo; }
            set { _archivo_tipo = value; }
        }
        #endregion propiedades

        #region constructor
        public Clausula(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        #endregion constructor

        #region metodos
        public decimal Adicionar(string idTipoClausula, string idEstado, string descripcion,
            decimal idEmpresa, decimal idOcupacion, Byte[] archivo, Int32 archivoTamaño, string archivoExtension,
            string archivoTipo, bool aplicarTodosLosCargos)
        {
            Conexion conexion = new Conexion(Empresa);
            Decimal id_clausula = 0;
            try
            {
                if (aplicarTodosLosCargos.Equals(false))
                {
                    id_clausula = conexion.ExecuteEscalarParaAdicionarClausula(idTipoClausula, idEstado, descripcion, idEmpresa, idOcupacion,
                        archivo, archivoTamaño, archivoExtension, archivoTipo, Usuario);
                }
                else
                {
                    cargo _cargo = new cargo(Empresa, Usuario);
                    DataTable dataTable = ObtenerOcupacionesPorIdEmpresa(idEmpresa);
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        id_clausula = conexion.ExecuteEscalarParaAdicionarClausula(idTipoClausula, idEstado, descripcion, idEmpresa, Convert.ToDecimal(dataRow["ID_OCUPACION"]),
                            archivo, archivoTamaño, archivoExtension, archivoTipo, Usuario);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al adicionar clausula." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return id_clausula;
        }

        public Decimal AdicionarEmpleadoClausulaRegistro(Decimal idContrato,
            Decimal idClausula,
            Conexion conexion)
        {
            Decimal idEmpleadoClausula = 0;

            String sql = "usp_empleado_clausulas_adicionar_a_empleado ";
            sql += idContrato;
            sql += ", " + idClausula;
            sql += ", '" + Usuario + "'";

            try
            {
                idEmpleadoClausula = Convert.ToDecimal(conexion.ExecuteNonQuery(sql));
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                idEmpleadoClausula = 0;
            }

            return idEmpleadoClausula;
        }

        public Decimal AdicionarClausulaYClausulaEmpleado(String idTipoClausula,
            String idEstado,
            String descripcion,
            Decimal idEmpresa,
            Decimal idOcupacion,
            Byte[] archivo,
            Int32 archivoTamaño,
            String archivoExtension,
            String archivoTipo,
            Decimal idContrato)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;
            Decimal id_clausula = 0;

            try
            {
                id_clausula = conexion.ExecuteEscalarParaAdicionarClausulaYEmpleadoClausula(idTipoClausula, idEstado, descripcion, idEmpresa, idOcupacion, archivo, archivoTamaño, archivoExtension, archivoTipo, Usuario, idContrato);

                if (id_clausula <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception e)
            {
                mensaje = e.Message;
                correcto = false;
                throw new Exception("Error al adicionar clausula. " + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return id_clausula;
        }



        public bool Actualizar(decimal idClausula, string idTipoClausula, string IdEstado, string descripcion,
            decimal idEmpresa, decimal idOcupacion, Byte[] archivo, Int32 archivoTamaño, string archivoExtension,
            string archivoTipo)
        {
            Conexion conexion = new Conexion(Empresa);
            bool actualizado = true;
            try
            {
                actualizado = conexion.ExecuteEscalarParaActualizarClausula(idClausula, idTipoClausula, IdEstado, descripcion, idEmpresa, idOcupacion,
                    archivo, archivoTamaño, archivoExtension, archivoTipo, Usuario);
            }
            catch (Exception e)
            {
                actualizado = false;
                throw new Exception("Error al adicionar clausula." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return actualizado;
        }

        public DataRow ObtenerPorId(decimal idClausula)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            DataRow dataRow = null;
            string sql = null;

            sql = "usp_clausulas_obtenerPorId ";
            sql += idClausula;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
                if (_dataTable.Rows.Count > 0) dataRow = _dataTable.Rows[0];
            }
            catch (Exception e)
            {
                throw new Exception("Error al adicionar clausula." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataRow;
        }

        public DataTable ObtenerPorIdEmpresa(decimal idEmpresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_clausulas_obtenerPorIdEmpresa ";
            sql += idEmpresa.ToString();

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error obtener clausualas por Id empresa." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable Buscar(decimal idEmpresa, string dato)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_clausulas_buscar ";
            sql += idEmpresa.ToString();
            sql += ", '" + dato + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error buscar clausualas para la empresa." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ObtenerOcupacionesPorIdEmpresa(decimal idEmpresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_clausulas_ocupaciones_obtenerPorIdEmpresa ";
            sql += idEmpresa.ToString();

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                dataView = dataSet.Tables[0].DefaultView;
                dataTable = dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar ocupaciones por IdEmpresa." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ObtenerPorPerfil(decimal idEmpresa, decimal idPerfil)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_clausulas_obtenerPorPefil ";
            sql += idEmpresa.ToString() + ", ";
            sql += idPerfil.ToString();

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                dataView = dataSet.Tables[0].DefaultView;
                dataTable = dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar clausulas para la empresa y perfil." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }

        public DataTable ObtenerContratacionPorId(decimal idClausula, decimal idEmpleado)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_empleado_clausulas_obtenerPorId ";
            sql += idClausula.ToString() + ", ";
            sql += idEmpleado.ToString();

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                dataView = dataSet.Tables[0].DefaultView;
                dataTable = dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar las clausualas del trabajador en contratación." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ObtenerContratacionPorIdEmpleado(decimal idEmpleado)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_empleado_clausulas_obtenerPorIdEmpleado ";
            sql += idEmpleado.ToString();

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                dataView = dataSet.Tables[0].DefaultView;
                dataTable = dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar las clausualas del trabajador en contratación." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }

        public DataTable ObtenerClausulasAplicadasAlPerfil(decimal idPerfil)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_con_reg_clausulas_obtenerPorIdPefil ";
            sql += idPerfil.ToString();

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                dataView = dataSet.Tables[0].DefaultView;
                dataTable = dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar las clausulas asignadas al perfil." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }

        public bool Actualizar(List<Clausula> clausulas)
        {
            Conexion conexion = new Conexion(Empresa);
            bool actualizado = true;

            conexion.IniciarTransaccion();

            try
            {
                foreach (Clausula c in clausulas)
                {
                    conexion.ExecuteEscalarParaActualizarClausulasEmpleado(c.IdEmpleado, c.IdClausua, c.Archivo, c.ArchivoExtension, c.ArchivoTamaño, c.ArchivoTipo, Usuario);
                }
            }
            catch (Exception e)
            {
                conexion.DeshacerTransaccion();
                conexion.Desconectar();
                actualizado = false;
                throw new Exception("Se presentado un error al intentar actualizar clausulas del empleado. " + e.Message);
            }

            if (actualizado)
            {
                conexion.AceptarTransaccion();
                conexion.Desconectar();
                return true;
            }
            else return false;
        }
        #endregion metodos
    }
}
