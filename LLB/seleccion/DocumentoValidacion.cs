using System;
using System.Collections.Generic;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class DocumentoValidacion
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        Decimal _id_sel_reg_documentos = 0;
        String _nombre = null;
        Boolean _vigente = true;

        #endregion varialbes

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Decimal id_sel_reg_documentos
        {
            get { return _id_sel_reg_documentos; }
            set { _id_sel_reg_documentos = value; }
        }
        public String nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public Boolean vigente
        {
            get { return _vigente; }
            set { _vigente = value; }
        }
        #endregion propiedades

        #region constructores
        public DocumentoValidacion(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        public DocumentoValidacion()
        {

        }
        #endregion constructores

        #region METODOS
        public DataTable ObtenerDocumentosTodos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_documentos_obtener_todos";

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

        public Boolean ActualizarRegistroConTransaccion(Decimal ID_SEL_REG_DOCUMENTOS,
            String NOMBRE,
            Boolean VIGENCIA,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_sel_reg_documentos_actualizar ";
            informacion = sql;

            #region validaciones
            if (ID_SEL_REG_DOCUMENTOS != 0)
            {
                sql += ID_SEL_REG_DOCUMENTOS + ", ";
                informacion += "ID_SEL_REG_DOCUMENTOS = '" + ID_SEL_REG_DOCUMENTOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SEL_REG_DOCUMENTOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (VIGENCIA == true)
            {
                sql += "'True', ";
                informacion += "VIGENCIA = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "VIGENCIA = 'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DOCUMENTOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
            }

            return ejecutar;
        }

        public Decimal AdicionarDocumentoConTransaccion(String NOMBRE,
            Boolean VIGENTE,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_documentos_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (VIGENTE == true)
            {
                sql += "'True', ";
                informacion += "VIGENCIA = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "VIGENCIA = 'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DOCUMENTOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarDocumentos(List<DocumentoValidacion> listaDocs)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                foreach (DocumentoValidacion documento in listaDocs)
                {
                    if (documento.id_sel_reg_documentos == 0)
                    {
                        if (AdicionarDocumentoConTransaccion(documento.nombre, documento.vigente, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                    else
                    {
                        if (ActualizarRegistroConTransaccion(documento.id_sel_reg_documentos, documento.nombre, documento.vigente, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }
        #endregion METODOS
    }
}
