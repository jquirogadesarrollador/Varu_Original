using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class acoset
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _REGISTRO = 0;
        private String _APELLIDOS = null;
        private String _NOMBRES = null;
        private String _TIP_DOC_IDENTIDAD = null;
        private String _NUM_DOC_IDENTIDAD = null;
        private String _OBS_ACOSET = null;
        private String _ENTIDAD_REPORTA = null;
        private Boolean _ACTIVO = true;
        private String _MOTIVO_ESTADO = null;

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
        public Decimal REGISTRO
        {
            get { return _REGISTRO; }
            set { _REGISTRO = value; }
        }
        public String APELLIDOS
        {
            get { return _APELLIDOS; }
            set { _APELLIDOS = value; }
        }
        public String NOMBRES
        {
            get { return _NOMBRES; }
            set { _NOMBRES = value; }
        }
        public String TIP_DOC_IDENTIDAD
        {
            get { return _TIP_DOC_IDENTIDAD; }
            set { _TIP_DOC_IDENTIDAD = value; }
        }
        public String NUM_DOC_IDENTIDAD
        {
            get { return _NUM_DOC_IDENTIDAD; }
            set { _NUM_DOC_IDENTIDAD = value; }
        }
        public String OBS_ACOSET
        {
            get { return _OBS_ACOSET; }
            set { _OBS_ACOSET = value; }
        }
        public String ENTIDAD_REPORTA
        {
            get { return _ENTIDAD_REPORTA; }
            set { _ENTIDAD_REPORTA = value; }
        }
        public Boolean ACTIVO
        {
            get { return _ACTIVO; }
            set { _ACTIVO = value; }
        }
        public String MOTIVO_ESTADO
        {
            get { return _MOTIVO_ESTADO; }
            set { _MOTIVO_ESTADO = value; }
        }
        #endregion propiedades

        #region constructores
        public acoset(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarRegAcoset(String APELLIDOS, String NOMBRES, String TIP_DOC_IDENTIDAD, String NUM_DOC_IDENTIDAD, String OBS_ACOSET, String ENTIDAD_REPORTA)
        {
            String sql = null;
            String idRegAcoset = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_acoset_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIP_DOC_IDENTIDAD)))
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_ACOSET)))
            {
                sql += "'" + OBS_ACOSET + "', ";
                informacion += "OBS_ACOSET = '" + OBS_ACOSET.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_ACOSET = 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(ENTIDAD_REPORTA)))
            {
                sql += "'" + ENTIDAD_REPORTA + "'";
                informacion += "ENTIDAD_REPORTA = '" + ENTIDAD_REPORTA + "'";
            }
            else
            {
                MensajeError += "El campo ENTIDAD_REPORTA no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRegAcoset = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_ACOSET, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRegAcoset))) return Convert.ToDecimal(idRegAcoset);
            else return 0;
        }





        public Decimal AdicionarRegAcoset(String APELLIDOS, String NOMBRES, String TIP_DOC_IDENTIDAD, String NUM_DOC_IDENTIDAD, String OBS_ACOSET, String ENTIDAD_REPORTA, Conexion conexion)
        {
            String sql = null;
            String idRegAcoset = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_acoset_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIP_DOC_IDENTIDAD)))
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_ACOSET)))
            {
                sql += "'" + OBS_ACOSET + "', ";
                informacion += "OBS_ACOSET = '" + OBS_ACOSET.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_ACOSET = 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(ENTIDAD_REPORTA)))
            {
                sql += "'" + ENTIDAD_REPORTA + "'";
                informacion += "ENTIDAD_REPORTA = '" + ENTIDAD_REPORTA + "'";
            }
            else
            {
                MensajeError += "El campo ENTIDAD_REPORTA no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    idRegAcoset = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_ACOSET, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(idRegAcoset))) return Convert.ToDecimal(idRegAcoset);
            else return 0;
        }

        public Boolean AdicionarRegAcosetMasivo(List<acoset> listaRegistrosAcoset)
        {
            String sql = null;
            String idRegAcoset = null;
            String informacion = null;
            Boolean ejecutar = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;

            Decimal REGISTRO = 0;

            try
            {
                foreach (acoset objAcoset in listaRegistrosAcoset)
                {
                    REGISTRO = AdicionarRegAcoset(objAcoset._APELLIDOS, objAcoset.NOMBRES, objAcoset.TIP_DOC_IDENTIDAD, objAcoset.NUM_DOC_IDENTIDAD, objAcoset.OBS_ACOSET, objAcoset.ENTIDAD_REPORTA, conexion);

                    if (REGISTRO <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        break;
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                correcto = false;
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public Boolean ActualizarAcoset(Decimal REGISTRO, String APELLIDOS, String NOMBRES, String TIP_DOC_IDENTIDAD, String NUM_DOC_IDENTIDAD, String OBS_ACOSET, String ENTIDAD_REPORTA, Boolean ACTIVO, String MOTIVO_ESTADO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_acoset_actualizar ";

            #region validaciones

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = " + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIP_DOC_IDENTIDAD)))
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_ACOSET)))
            {
                sql += "'" + OBS_ACOSET + "', ";
                informacion += "OBS_ACOSET = '" + OBS_ACOSET.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBS_ACOSET = 'NULL', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(ENTIDAD_REPORTA)))
            {
                sql += "'" + ENTIDAD_REPORTA + "', ";
                informacion += "ENTIDAD_REPORTA = '" + ENTIDAD_REPORTA + "', ";
            }
            else
            {
                MensajeError += "El campo ENTIDAD_REPORTA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ACTIVO == true)
            {
                sql += "'True', ";
                informacion += "ACTIVO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ACTIVO = 'False', ";
            }

            if (!(String.IsNullOrEmpty(MOTIVO_ESTADO)))
            {
                sql += "'" + MOTIVO_ESTADO + "'";
                informacion += "MOTIVO_ESTADO = '" + MOTIVO_ESTADO + "'";
            }
            else
            {
                sql += "null";
                informacion += "MOTIVO_ESTADO = 'NULL'";
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
                    _auditoria.Adicionar(Usuario, tabla.REG_ACOSET, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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
        public DataTable ObtenerRegAcosetPorApellido(String APELLIDO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String informacion = null;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_acoset_obtener_por_apellido ";

            if (!(String.IsNullOrEmpty(APELLIDO)))
            {
                sql += "'" + APELLIDO + "'";
                informacion += "APELLIDO = " + APELLIDO.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo APELLIDO no puede ser vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_ACOSET, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerRegAcosetPorNombre(String NOMBRES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String informacion = null;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_acoset_obtener_por_nombre ";

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "'";
                informacion += "NOMBRES = " + NOMBRES.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_ACOSET, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerRegAcosetPorNumeroID(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String informacion = null;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_acoset_obtener_por_numeroId ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = " + NUM_DOC_IDENTIDAD.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_ACOSET, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerRegAcosetPorRegistro(int REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String informacion = null;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_acoset_obtener_por_id ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_ACOSET, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
