using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.contratacion
{
    public class configuracionElementosPerfil
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
        public configuracionElementosPerfil(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        #region con_reg_servicio_perfil

        public DataTable ObtenerConRegServicioPerfilServicio(int ID_PERFIL, String ID_CIUDAD, int ID_CENTRO_C, int ID_SUB_C)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_SERVICIO_PERFIL_OBTENER_SERVICIO ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL= '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "'";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = 'null'";
            }
            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CENTRO_C = '0', ";
            }
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_SUB_C = '0', ";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_SERVICIO_PERFIL, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegServicioPerfilCiudadCCSubC(int ID_PERFIL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_SERVICIO_PERFIL_OBTENER_CIUDADES_CC_SUBCC ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
                informacion += "ID_PERFIL= '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_SERVICIO_PERFIL, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegServicioPerfilCiudad(int ID_PERFIL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_SERVICIO_PERFIL_OBTENER_CIUDADES ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
                informacion += "ID_PERFIL= '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_SERVICIO_PERFIL, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegServicioPerfilCentroC(int ID_PERFIL, String ID_CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_SERVICIO_PERFIL_OBTENER_CC ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ",";
                informacion += "ID_PERFIL= '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += ID_CIUDAD;
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_SERVICIO_PERFIL, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegServicioPerfilSubCentroC(int ID_PERFIL, String ID_CIUDAD, int ID_CC)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_SERVICIO_PERFIL_OBTENER_SUBCC ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL= '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += ID_CIUDAD + ", ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_CC != 0)
            {
                sql += ID_CC;
                informacion += "ID_CC= '" + ID_CC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CC no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_SERVICIO_PERFIL, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        #endregion

        #region con_reg_elementos_trabajo

        public DataTable ObtenerConRegElementosTrabajoExamenes(int ID_PERFIL, String ID_CIUDAD, int ID_CENTRO_C, int ID_SUB_C, int ID_SERVICIO, String SEXO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_SERVICIO_PERFIL_OBTENER_EXAMENES ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "'";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = 'null'";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = 'null', ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SUB_C = 'null', ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SERVICIO = '0', ";
            }

            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "' ";
                informacion += "SEXO = '" + SEXO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo SEXO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_SERVICIO_PERFIL + ", " + tabla.CON_REG_ELEMEMENTOS_TRABAJO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegElementosTrabajoEntregas(int ID_PERFIL, String ID_CIUDAD, int ID_CENTRO_C, int ID_SUB_C, int ID_SERVICIO, String SEXO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_SERVICIO_PERFIL_OBTENER_ENTREGAS ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL= '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "'";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "'";
            }
            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = 'null', ";
            }
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SUB_C = 'null', ";
            }
            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SERVICIO = '0', ";
            }
            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "' ";
                informacion += "SEXO = '" + SEXO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo SEXO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_SERVICIO_PERFIL + ", " + tabla.CON_REG_ELEMEMENTOS_TRABAJO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        #endregion
        #endregion metodos

    }
}
