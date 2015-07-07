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
    public class documentosLegales
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
        public documentosLegales(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region reg_documentos_legales
        public DataTable ObtenerDocumentoActualPorTipoDoc(String TIPO_DOCUMENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_documentos_legales_obtener_documento_activo";

            #region validaciones
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


        public DataTable ObtenerDocumentoLegalPorId(Decimal ID_DOCUMENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_documentos_legales_obtener_documento_po_id ";

            #region validaciones
            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO;
            }
            else
            {
                MensajeError = "El campo ID_DOCUMENTO no puede ser vacio.";
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


        public Decimal AdicionarNuevoDocumentoLegal(DateTime FECHA_R,
            DateTime FECHA_INICIAL,
            DateTime FECHA_FINAL,
            String TIPO_DOCUMENTO,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String ACTIVO)
        {
            Decimal ID_DOCUMENTO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_documentos_legales_adicionar_documento_nuevo ";

            #region validaciones

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";
            informacion += "FECHA_INICIAL = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";

            if (FECHA_FINAL != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "', ";
                informacion += "FECHA_FINAL = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_FINAL = 'NULL', ";
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
                    ID_DOCUMENTO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaNuevoDocumentoLegal(FECHA_R, FECHA_INICIAL, FECHA_FINAL, TIPO_DOCUMENTO, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, ACTIVO, Usuario));

                    if (ID_DOCUMENTO <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar el nuevo documento.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_DOCUMENTOS_LEGALES, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de el nuevo documento legal.";
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
                return ID_DOCUMENTO;
            }
            else
            {
                return 0;
            }
        }
        #endregion reg_documentos_legales
        #endregion metodos
    }
}