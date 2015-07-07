using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class tercero
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        public enum Empresas
        {
            Sertempo = 1,
            EYS = 3
        }
        #endregion variables

        #region propiedades
        public String Empresa
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
        public tercero(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public Decimal Adicionar(String NATURALAEZA_JURIDICA,
            String NUMERO_IDENTIFICACION,
            String RAZON_SOCIAL,
            String PRIMER_NOMBRE,
            String SEGUNDO_NOMBRE,
            String PRIMER_APELLIDO,
            String SEGUNDO_APELLIDO,
            String ESTADO,
            Conexion conexion)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_terceros_adicionar ";

            #region validaciones
            if (!(String.IsNullOrEmpty(NATURALAEZA_JURIDICA)))
            {
                sql += "'" + NATURALAEZA_JURIDICA + "', ";
                informacion += "NATURALAEZA_JURIDICA = '" + NATURALAEZA_JURIDICA + "', ";
            }
            else
            {
                MensajeError += "El campo NATURALAEZA_JURIDICA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUMERO_IDENTIFICACION)))
            {
                sql += "'" + NUMERO_IDENTIFICACION + "', ";
                informacion += "NUMERO_IDENTIFICACION = '" + NUMERO_IDENTIFICACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUMERO_IDENTIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RAZON_SOCIAL)))
            {
                sql += "'" + RAZON_SOCIAL + "', ";
                informacion += "RAZON_SOCIAL = '" + RAZON_SOCIAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RAZON_SOCIAL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PRIMER_NOMBRE)))
            {
                sql += "'" + PRIMER_NOMBRE + "', ";
                informacion += "PRIMER_NOMBRE = '" + PRIMER_NOMBRE.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PRIMER_NOMBRE = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(SEGUNDO_NOMBRE)))
            {
                sql += "'" + SEGUNDO_NOMBRE + "', ";
                informacion += "SEGUNDO_NOMBRE = '" + SEGUNDO_NOMBRE.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "SEGUNDO_NOMBRE = 'NULL', ";
            }


            if (!(String.IsNullOrEmpty(PRIMER_APELLIDO)))
            {
                sql += "'" + PRIMER_APELLIDO + "', ";
                informacion += "PRIMER_APELLIDO = '" + PRIMER_APELLIDO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PRIMER_APELLIDO = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(SEGUNDO_APELLIDO)))
            {
                sql += "'" + SEGUNDO_APELLIDO + "', ";
                informacion += "SEGUNDO_APELLIDO = '" + SEGUNDO_APELLIDO.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "SEGUNDO_APELLIDO = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.TERCEROS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(idRecuperado))) return Convert.ToDecimal(idRecuperado);
            else return 0;
        }

        public DataTable ObtenerPorNombre(String nombre)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_terceros_obtenerPorNombre ";
            if (!(String.IsNullOrEmpty(nombre))) sql += "'" + nombre + "'";
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

            return _dataTable;
        }

        public DataTable ObtenerPorNumeroIdentificacion(String numeroIdentificacion)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_terceros_obtenerPorNumeroIdentificacion ";

            if (!(String.IsNullOrEmpty(numeroIdentificacion)))
            {
                sql += "'" + numeroIdentificacion + "'";
            }

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

            return _dataTable;
        }
        #endregion metodos
    }
}