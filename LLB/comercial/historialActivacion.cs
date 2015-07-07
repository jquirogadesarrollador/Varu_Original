using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.comercial
{
    public class historialActivacion
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        private String _CLASE_REGISTRO = null;
        private String _COMENTARIO = null;
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

        public String CLASE_REGISTRO
        {
            get { return _CLASE_REGISTRO; }
            set { _CLASE_REGISTRO = value; }
        }

        public String COMENTARIO
        {
            get { return _COMENTARIO; }
            set { _COMENTARIO = value; }
        }
        #endregion propiedades

        #region constructores
        public historialActivacion(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion

        #region metodos
        public DataTable ObtenerHistorialPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_reg_hv_empresa_buscarPorIdEmpresa ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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

        public Boolean Adicionar(Decimal ID_EMPRESA, String CLASE_REGISTRO, String COMENTARIOS, String USU_CRE, Conexion conexion)
        {
            Int32 cantidadRegistrosAfectados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "ven_reg_hv_empresa_adicionar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CLASE_REGISTRO)))
            {
                sql += "'" + CLASE_REGISTRO + "', ";
                informacion += "CLASE_REGISTRO = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CLASE DE REGISTRO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COMENTARIOS)))
            {
                sql += "'" + COMENTARIOS + "', ";
                informacion += "COMENTARIOS = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COMENTARIOS no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_CRE)))
            {
                sql += "'" + USU_CRE + "'";
                informacion += "COMENTARIOS = '" + ID_EMPRESA.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo COMENTARIOS no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosAfectados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_CRE, tabla.VEN_REG_HV_EMPRESA, tabla.ACCION_ADICIONAR.ToString(), sql, informacion, conexion);

                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            if (cantidadRegistrosAfectados != 0) return true;
            else return false;
        }
        #endregion metodos
    }
}
