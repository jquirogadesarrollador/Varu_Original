using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class incapadadConceptosNomina
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        Decimal _id_concepto = 0;
        String _porcentaje = "0";
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

        public Decimal IdConcepto
        {
            get { return _id_concepto; }
            set { _id_concepto = value; }
        }

        public String Porcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        #endregion propiedades

        #region constructores
        public incapadadConceptosNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        #endregion constructores

        #region metodos
        public Int32 Adicionar(Decimal REGISTRO, Decimal ID_CONCEPTO, String PORCENTAJE, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            String id = null;

            tools _tools = new tools();

            sql = "usp_ven_d_nomina_incapacidades_adicionar ";

            #region validaciones

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CONCEPTO != 0)
            {
                sql += ID_CONCEPTO + ", ";
                informacion += "ID_CONCEPTO = '" + ID_CONCEPTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONCEPTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!String.IsNullOrEmpty(PORCENTAJE))
            {
                sql += "'" + PORCENTAJE + "', ";
                informacion += "PORCENTAJE = '" + PORCENTAJE + "',";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    id = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_D_NOMINA_INCAPACIDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(id))) return Convert.ToInt32(id);
            else return 0;
        }

        public Boolean Eliminar(Decimal REGISTRO, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_nomina_incapacidades_delete ";

            #region validaciones

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if ((String.IsNullOrEmpty(MensajeError))) return true;
            else return false;
        }

        public DataTable ObtenerPorRegistro(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            sql = "usp_ven_d_nomina_incapacidades_ObtenerPorRegistro '" + REGISTRO + "'";

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
