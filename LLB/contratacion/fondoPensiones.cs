using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.contratacion
{
    public class fondoPensiones
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
        public fondoPensiones(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal Adicionar(String NIT, String DIG_VER, String COD_ENTIDAD, String NOM_ENTIDAD, String DIR_ENTIDAD,
            String TEL_ENTIDAD, String CONTACTO, String CARGO)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_con_ent_afp_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NIT)))
            {
                sql += "'" + NIT + "', ";
                informacion += "NIT = '" + NIT + "', ";
            }
            else
            {
                MensajeError += "El campo NIT no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIG_VER)))
            {
                sql += "'" + DIG_VER + "', ";
                informacion += "DIG_VER = '" + DIG_VER + "', ";
            }
            else
            {
                MensajeError += "El campo DIGITO DE VERIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COD_ENTIDAD)))
            {
                sql += "'" + COD_ENTIDAD + "', ";
                informacion += "COD_ENTIDAD = '" + COD_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo CODIGO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_ENTIDAD)))
            {
                sql += "'" + NOM_ENTIDAD + "', ";
                informacion += "NOM_ENTIDAD = '" + NOM_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIR_ENTIDAD)))
            {
                sql += "'" + DIR_ENTIDAD + "', ";
                informacion += "DIR_ENTIDAD = '" + DIR_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_ENTIDAD)))
            {
                sql += "'" + TEL_ENTIDAD + "', ";
                informacion += "TEL_ENTIDAD = '" + TEL_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONTACTO)))
            {
                sql += "'" + CONTACTO + "', ";
                informacion += "CONTACTO = '" + CONTACTO + "', ";
            }
            else
            {
                MensajeError += "El campo CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO)))
            {
                sql += "'" + CARGO + "', ";
                informacion += "CARGO = '" + CARGO + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar entidad
                    ID = conexion.ExecuteScalar(sql);
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_ENT_F_PENSIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
                    #endregion auditoria

                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente) return Convert.ToDecimal(ID);
            else return 0;
        }

        public Boolean Actualizar(Decimal ID_AFP, String NIT, String DIG_VER, String COD_ENTIDAD, String NOM_ENTIDAD, String DIR_ENTIDAD,
            String TEL_ENTIDAD, String CONTACTO, String CARGO, bool ESTADO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_con_ent_afp_actualizar ";

            #region validaciones

            if (ID_AFP != 0)
            {
                sql += ID_AFP + ", ";
                informacion += "ID_AFP = '" + ID_AFP + "', ";
            }
            else
            {
                MensajeError += "El campo ID_AFP no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NIT)))
            {
                sql += "'" + NIT + "', ";
                informacion += "NIT = '" + NIT + "', ";
            }
            else
            {
                MensajeError += "El campo NIT no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIG_VER)))
            {
                sql += "'" + DIG_VER + "', ";
                informacion += "DIG_VER = '" + DIG_VER + "', ";
            }
            else
            {
                MensajeError += "El campo DIGITO DE VERIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COD_ENTIDAD)))
            {
                sql += "'" + COD_ENTIDAD + "', ";
                informacion += "COD_ENTIDAD = '" + COD_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo CODIGO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_ENTIDAD)))
            {
                sql += "'" + NOM_ENTIDAD + "', ";
                informacion += "NOM_ENTIDAD = '" + NOM_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIR_ENTIDAD)))
            {
                sql += "'" + DIR_ENTIDAD + "', ";
                informacion += "DIR_ENTIDAD = '" + DIR_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_ENTIDAD)))
            {
                sql += "'" + TEL_ENTIDAD + "', ";
                informacion += "TEL_ENTIDAD = '" + TEL_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONTACTO)))
            {
                sql += "'" + CONTACTO + "', ";
                informacion += "CONTACTO = '" + CONTACTO + "', ";
            }
            else
            {
                MensajeError += "El campo CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO)))
            {
                sql += "'" + CARGO + "', ";
                informacion += "CARGO = '" + CARGO + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ESTADO) sql += "'S'";
            else sql += "'N'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar entidad
                    if (conexion.ExecuteNonQuery(sql) == 0) ejecutadoCorrectamente = false;
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_ENT_F_PENSIONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
                    #endregion auditoria

                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }

        public DataTable ObtenerTodosLosFondosDePensiones()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_con_ent_f_pensiones_obtenerTodas ";

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

        public DataTable ObtenerPorNombre(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_afp_obtenerPorNombre ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_ENT_F_PENSIONES, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorIdAFP(Decimal ID_AFP)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_afp_obtenerPorIdAFP ";

            if (ID_AFP != 0)
            {
                sql += ID_AFP;
                informacion += "ID_AFP = " + ID_AFP;
            }
            else
            {
                MensajeError += "El campo ID_AFP no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_ENT_F_PENSIONES, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorCiudad(String CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_f_pensiones_obtenerTodas_por_ciudad ";

            if (!(String.IsNullOrEmpty(CIUDAD)))
            {
                sql += "'" + CIUDAD + "'";
                informacion += "CIUDAD = '" + CIUDAD + "'";
            }
            else
            {
                MensajeError += "El campo CIUDAD no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_ENT_F_PENSIONES, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
