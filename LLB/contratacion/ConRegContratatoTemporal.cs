using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.almacen;
using Brainsbits.LLB.seleccion;

namespace Brainsbits.LLB.contratacion
{
    public class ConRegContratoTemporal
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
        public ConRegContratoTemporal()
        {

        }

        public ConRegContratoTemporal(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarConRegContratoTemporal(Decimal ID_REQUERIMIENTO,
            Decimal ID_SOLICITUD,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            Decimal ID_SERVICIO,
            Decimal ID_EMPRESA,
            Boolean TIENE_CUENTA,
            Conexion conexion)
        {
            String sql = null;
            Decimal identificador = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_contratos_temporal_adicionar ";

            #region validaciones
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CIUDAD = 'NULL', ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CENTRO_C = 'NULL', ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SUB_C = 'NULL', ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SERVICIO = 'NULL', ";
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (TIENE_CUENTA == true)
            {
                sql += "'True'";
                informacion += "TIENE_CUENTA = 'True', ";
            }
            else
            {
                sql += "'False'";
                informacion += "TIENE_CUENTA = 'False', ";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS_TEMPORAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    identificador = 0;
                }
            }

            return identificador;
        }


        public Decimal AdicionarConRegContratoTemporal(Decimal ID_REQUERIMIENTO,
            Decimal ID_SOLICITUD,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            Decimal ID_SERVICIO,
            Decimal ID_EMPRESA,
            Boolean TIENE_CUENTA)
        {
            String sql = null;
            Decimal identificador = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_contratos_temporal_adicionar ";

            #region validaciones
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CIUDAD = 'NULL', ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CENTRO_C = 'NULL', ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SUB_C = 'NULL', ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SERVICIO = 'NULL', ";
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (TIENE_CUENTA == true)
            {
                sql += "'True'";
                informacion += "TIENE_CUENTA = 'True', ";
            }
            else
            {
                sql += "'False'";
                informacion += "TIENE_CUENTA = 'False', ";
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    identificador = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS_TEMPORAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    identificador = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return identificador;
        }

        public DataTable ObtenerConRegContratosTemporalPorIdRequerimientoIdSolicitud(Decimal ID_REQUERIMIENTO,
            Decimal ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_temporal_obtener_por_id_requerimiento_id_solicitud ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
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

        public Boolean ActualizarDescarte(Decimal ID_REQUERIMIENTO,
            Decimal ID_SOLICITUD,
            String OBSERVACIONES,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_reg_contratos_temporal_descartar ";

            #region validaciones

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }


            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region adicionar entidad
                    if (conexion.ExecuteNonQuery(sql) == 0) ejecutadoCorrectamente = false;
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS_TEMPORAL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }

        public Boolean DescartarConRegContratosTemporal(Decimal ID_REQUERIMIENTO,
            Decimal ID_SOLICITUD,
            String OBSERVACIONES)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarDescarte(ID_REQUERIMIENTO, ID_SOLICITUD, OBSERVACIONES, conexion) == false)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    radicacionHojasDeVida _rad = new radicacionHojasDeVida(Empresa, Usuario);

                    if (_rad.ActualizarEstadoProcesoRegSolicitudesIngreso(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD), "EN DESCARTE", Usuario, conexion) == false)
                    {
                        correcto = false;
                        conexion.DeshacerTransaccion();
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch
            {
                correcto = false;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        #endregion metodos
    }
}
