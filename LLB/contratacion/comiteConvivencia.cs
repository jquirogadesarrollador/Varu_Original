using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.contratacion
{
    public class comiteConvivencia
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
        public comiteConvivencia(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region reg_comite_convivencia
        public DataTable ObtenerActaComiteActual()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_comite_convivencia_obtener_acta_comite_actual";

            #region validaciones
            #endregion validaciones

            if (ejecutar)
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

        public DataTable ObtenerHistorialActaComite()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_comite_convivencia_obtener_historial_actas_comite";

            #region validaciones
            #endregion validaciones

            if (ejecutar)
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

        public DataTable ObtenerHistorialDocumentos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_comite_convivencia_obtener_historial_documentos";

            #region validaciones
            #endregion validaciones

            if (ejecutar)
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

        public Decimal AdicionarNuevaActaComite(DateTime FECHA_R,
            DateTime FECHA_ACTA,
            String TITULO,
            String TIPO_DOCUMENTO,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String ACTIVO)
        {
            Decimal ID_ACTA_COMITE = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_comite_convivencia_adicionar_acta_comite ";

            #region validaciones

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTA) + "', ";
            informacion += "FECHA_ACTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTA) + "', ";

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TITULO = 'null', ";
            }

            if (String.IsNullOrEmpty(TIPO_DOCUMENTO) == false)
            {
                sql += "'" + TIPO_DOCUMENTO + "', ";
                informacion += "TIPO_DOCUMENTO = '" + TIPO_DOCUMENTO + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_DOCUMENTO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'[DATOS_BINARIOS]', ";
            informacion += "ARCHIVO = '[DATOS_BINARIOS]', ";

            sql += "'" + ARCHIVO_EXTENSION + "', ";
            informacion += "ARCHIVO_EXTENSION = '" + ARCHIVO_EXTENSION + "', ";

            sql += ARCHIVO_TAMANO + ", ";
            informacion += "ARCHIVO_TAMANO = '" + ARCHIVO_TAMANO + "', ";

            sql += "'" + ARCHIVO_TYPE + "', ";
            informacion += "ARCHIVO_TYPE = '" + ARCHIVO_TYPE + "', ";

            sql += "'" + ACTIVO + "', ";
            informacion += "ACTIVO = '" + ACTIVO + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    ID_ACTA_COMITE = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaNuevaActaComite(FECHA_R, FECHA_ACTA, TITULO, TIPO_DOCUMENTO, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, ACTIVO, Usuario));

                    if (ID_ACTA_COMITE <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar la nueva acta de comite.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_COMITE_CONVIVENCIA, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de fallecidos.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_ACTA_COMITE;
            }
            else
            {
                return 0;
            }
        }

        public DataTable ObtenerArchivoComiteCOnvivenciaPorId(Decimal ID_ACTA_COMITE, String TIPO_DOCUMENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_comite_convivencia_obtener_archivo_por_id ";

            #region validaciones
            if (ID_ACTA_COMITE != 0)
            {
                sql += ID_ACTA_COMITE + ", ";
            }
            else
            {
                MensajeError = "El campo ID_ACTA_COMITE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_DOCUMENTO) == false)
            {
                sql += "'" + TIPO_DOCUMENTO + "'";
            }
            else
            {
                MensajeError = "El campo TIPO_DOCUMENTO no puede ser vacio.";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
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
        #endregion reg_comite_convivencia
        #endregion metodos
    }
}