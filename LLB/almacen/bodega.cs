using System;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class bodega
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
        public bodega(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarAlmRegBodega(String ID_REGIONAL, String ID_CIUDAD, int ID_EMPRESA)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_REG_BODEGA_ADICIONAR ";

            #region validaciones
            if (!(String.IsNullOrEmpty(ID_REGIONAL)))
            {
                sql += "'" + ID_REGIONAL + "', ";
                informacion += "ID_REGIONAL= '" + ID_REGIONAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    Conexion conexion = new Conexion(Empresa);
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Boolean ActualizarAlmRegBodega(int ID_BODEGA, String ID_REGIONAL, String ID_CIUDAD, int ID_EMPRESA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_REG_BODEGA_ACTUALIZAR";

            #region validaciones
            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + ", ";
                informacion += "ID_BODEGA= '" + ID_BODEGA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_REGIONAL)))
            {
                sql += "'" + ID_REGIONAL + "', ";
                informacion += "ID_REGIONAL= '" + ID_REGIONAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegBodegaPorId(int ID_BODEGA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_ID ";

            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + " ";
                informacion += "ID_BODEGA= '" + ID_BODEGA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegBodegaPorIdEmpresa(String ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_ID_EMPRESA ";

            if (!(String.IsNullOrEmpty(ID_EMPRESA)))
            {
                sql += "'" + ID_EMPRESA + "' ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegBodegaPorIdCiudad(String ID_CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_ID_CIUDAD ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "' ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegBodegaPorIds(String ID_REGIONAL, String ID_CIUDAD, int ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_IDS ";

            if (!(String.IsNullOrEmpty(ID_REGIONAL)))
            {
                sql += "'" + ID_REGIONAL + "', ";
                informacion += "ID_REGIONAL= '" + ID_REGIONAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + " ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerAlmRegBodegaPorIds(String ID_REGIONAL,
            String ID_CIUDAD,
            int ID_EMPRESA,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_IDS ";

            if (!(String.IsNullOrEmpty(ID_REGIONAL)))
            {
                sql += "'" + ID_REGIONAL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + " ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
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

        public DataTable ObtenerAlmRegBodegaPorNomCiudad(String NOM_CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_NOM_CIUDAD ";

            if (!(String.IsNullOrEmpty(NOM_CIUDAD)))
            {
                sql += "'" + NOM_CIUDAD + "' ";
                informacion += "NOM_CIUDAD= '" + NOM_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_CIUDAD no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegBodegaPorNomRegional(String NOM_REGIONAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_NOM_REGIONAL ";

            if (!(String.IsNullOrEmpty(NOM_REGIONAL)))
            {
                sql += "'" + NOM_REGIONAL + "' ";
                informacion += "NOM_REGIONAL= '" + NOM_REGIONAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_REGIONAL no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerAlmRegBodegaPorRazSocial(String RAZ_SOCIAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_POR_RAZ_SOCIAL ";

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "' ";
                informacion += "RAZ_SOCIAL= '" + RAZ_SOCIAL.ToString() + "', ";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_BODEGA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRegionalesDondeEmpresaTieneBodega(Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_NOM_REGIONAL_POR_ID_EMPRESA ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
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

        public DataTable ObtenerDepartamenosPorRegionalyDondeEmpresaTieneBodega(Decimal ID_EMPRESA, String ID_REGIONAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_NOM_DEPARTAMENTO_POR_ID_EMPRESA_ID_REGIONAL ";

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

            if (String.IsNullOrEmpty(ID_REGIONAL) == false)
            {
                sql += "'" + ID_REGIONAL + "'";
                informacion += "ID_REGIONAL = '" + ID_REGIONAL + "'";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
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

        public DataTable ObtenerCiudadesPorRegionalDepartamentoyDondeEmpresaTieneBodega(Decimal ID_EMPRESA, String ID_REGIONAL, String ID_DEPARTAMENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_BODEGA_OBTENER_NOM_CIUDADES_POR_ID_EMPRESA_ID_REGIONAL_ID_DEPARTAMENTO ";

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

            if (String.IsNullOrEmpty(ID_REGIONAL) == false)
            {
                sql += "'" + ID_REGIONAL + "', ";
                informacion += "ID_REGIONAL = '" + ID_REGIONAL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_DEPARTAMENTO) == false)
            {
                sql += "'" + ID_DEPARTAMENTO + "'";
                informacion += "ID_DEPARTAMENTO = '" + ID_REGIONAL + "'";
            }
            else
            {
                MensajeError += "El campo ID_DEPARTAMENTO no puede ser nulo\n";
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


        #endregion
    }
}
