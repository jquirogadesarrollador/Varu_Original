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
    public class ordenExamenes
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
        public ordenExamenes(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarConRegOrdenExamen(int ID_SOLICITUD, int ID_REQUERIMIENTO, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_ORDEN_EXAMEN_ADICIONAR ";

            #region validaciones
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
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ORDEN_EXAMEN, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public DataTable ObtenerConRegOrdenExamenPorSolicitud(int ID_REQUERIMIENTO, int ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_ORDEN_EXAMEN_OBTENER_POR_REQUERIMINETO_SOLICITUD ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
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

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ORDEN_EXAMEN, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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