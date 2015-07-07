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
    public class Abogados
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
        public Abogados(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region ABOGADOS
        public DataTable ObtenerAbogadosActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_abogados_obtener_todos_activos ";

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

        public Decimal AdicionarAbogado(String TIP_DOC_IDENTIDAD, String NUM_DOC_IDENTIDAD, String NOMBRES, String APELLIDOS, String ID_CIUDAD,
            String DIRECCION, String TELEFONO, String CELULAR, String ACTIVO, String FAX, String EMAIL, Decimal TARIFA)
        {
            String ID = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_abogados_adicionar ";

            #region validaciones

            if (String.IsNullOrEmpty(TIP_DOC_IDENTIDAD) == false)
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError = "El campo TIP_DOC_IDENTIDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError = "El campo NUM_DOC_IDENTIDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES= '" + NOMBRES + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRES no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS= '" + APELLIDOS + "', ";
            }
            else
            {
                MensajeError = "El campo APELLIDOS no puede ser vacio.";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DIRECCION) == false)
            {
                sql += "'" + DIRECCION + "', ";
                informacion += "DIRECCION= '" + DIRECCION + "', ";
            }
            else
            {
                MensajeError = "El campo DIRECCION no puede ser vacio.";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(TELEFONO) == false)
            {
                sql += "'" + TELEFONO + "', ";
                informacion += "TELEFONO= '" + TELEFONO + "', ";
            }
            else
            {
                MensajeError = "El campo TELEFONO no puede ser vacio.";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(CELULAR) == false)
            {
                sql += "'" + CELULAR + "', ";
                informacion += "CELULAR= '" + CELULAR + "', ";
            }
            else
            {
                MensajeError = "El campo CELULAR no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ACTIVO) == false)
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO= '" + ACTIVO + "', ";
            }
            else
            {
                MensajeError = "El campo ACTIVO no puede ser vacio.";
                ejecutar = false;
            }
            sql += "'" + Usuario.ToString() + "',";
            informacion += "USU_CRE = '" + Usuario.ToString() + "',";

            if (String.IsNullOrEmpty(FAX) == false)
            {
                sql += "'" + FAX + "', ";
                informacion += "FAX= '" + FAX + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "FAX= 'null', ";
            }

            if (String.IsNullOrEmpty(EMAIL) == false)
            {
                sql += "'" + EMAIL + "', ";
                informacion += "EMAIL= '" + EMAIL + "', ";
            }
            else
            {
                MensajeError = "El campo EMAIL no puede ser vacio.";
                ejecutar = false;
            }

            if (TARIFA != 0)
            {
                sql += TARIFA.ToString().Replace(',', '.');
                informacion += "TARIFA = '" + TARIFA.ToString().Replace(',', '.') + "'";
            }
            else
            {
                sql += "0";
                informacion += "TARIFA = '0'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar tutela
                    ID = conexion.ExecuteScalar(sql);
                    #endregion adicionar tutela

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ABOGADOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: intenatar ingresar en la bd la tutela.";
                        ejecutadoCorrectamente = false;
                    }
                    #endregion auditoria
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

            if (ejecutadoCorrectamente)
            {
                return Convert.ToDecimal(ID);
            }
            else
            {
                return 0;
            }
        }

        public Boolean Actualizar(Decimal ID_ABOGADO, String TIP_DOC_IDENTIDAD, String NUM_DOC_IDENTIDAD, String NOMBRES, String APELLIDOS, String ID_CIUDAD,
            String DIRECCION, String TELEFONO, String CELULAR, String ACTIVO, String FAX, String EMAIL, Decimal TARIFA)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Int32 cantidadRegistrosActualizados = 0;

            sql = "usp_reg_abogados_actualizar ";

            #region validaciones
            if (ID_ABOGADO != 0)
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO = '" + ID_ABOGADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ABOGADO no puede ser 0\n";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(TIP_DOC_IDENTIDAD) == false)
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError = "El campo TIP_DOC_IDENTIDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError = "El campo NUM_DOC_IDENTIDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES= '" + NOMBRES + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRES no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS= '" + APELLIDOS + "', ";
            }
            else
            {
                MensajeError = "El campo APELLIDOS no puede ser vacio.";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DIRECCION) == false)
            {
                sql += "'" + DIRECCION + "', ";
                informacion += "DIRECCION= '" + DIRECCION + "', ";
            }
            else
            {
                MensajeError = "El campo DIRECCION no puede ser vacio.";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(TELEFONO) == false)
            {
                sql += "'" + TELEFONO + "', ";
                informacion += "TELEFONO= '" + TELEFONO + "', ";
            }
            else
            {
                MensajeError = "El campo TELEFONO no puede ser vacio.";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(CELULAR) == false)
            {
                sql += "'" + CELULAR + "', ";
                informacion += "CELULAR= '" + CELULAR + "', ";
            }
            else
            {
                MensajeError = "El campo CELULAR no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ACTIVO) == false)
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO= '" + ACTIVO + "', ";
            }
            else
            {
                MensajeError = "El campo ACTIVO no puede ser vacio.";
                ejecutar = false;
            }
            sql += "'" + Usuario.ToString() + "',";
            informacion += "USU_CRE = '" + Usuario.ToString() + "',";

            if (String.IsNullOrEmpty(FAX) == false)
            {
                sql += "'" + FAX + "', ";
                informacion += "FAX= '" + FAX + "', ";
            }
            else
            {
                sql += "'null', ";
                informacion += "FAX= 'null', ";
            }

            if (String.IsNullOrEmpty(EMAIL) == false)
            {
                sql += "'" + EMAIL + "', ";
                informacion += "EMAIL= '" + EMAIL + "', ";
            }
            else
            {
                MensajeError = "El campo EMAIL no puede ser vacio.";
                ejecutar = false;
            }

            if (TARIFA != 0)
            {
                sql += TARIFA.ToString().Replace(',', '.');
                informacion += "TARIFA = '" + TARIFA.ToString().Replace(',', '.') + "'";
            }
            else
            {
                sql += "0";
                informacion += "TARIFA = '0'";
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
                    _auditoria.Adicionar(Usuario, tabla.REG_ABOGADOS, tabla.ACCION_ACTUALIZAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerAbogadosPorId(Decimal ID_ABOGADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_abogados_obtener_id ";

            if (ID_ABOGADO != 0)
            {
                sql += ID_ABOGADO;
                informacion += "ID_ABOGADO = " + ID_ABOGADO;
            }
            else
            {
                MensajeError += "El campo ID_ABOGADO no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.REG_ABOGADOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerAbogadosPorDocIdentidad(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_abogados_obtener_doc_identidad ";

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = " + NUM_DOC_IDENTIDAD;
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.REG_ABOGADOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerAbogadosPorNombre(String NOMBRES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_abogados_obtener_nombre ";

            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "'";
                informacion += "NOMBRES = " + NOMBRES;
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.REG_ABOGADOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
        public DataTable ObtenerAbogadosPorApellido(String APELLIDOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_abogados_obtener_apellido ";

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "'";
                informacion += "APELLIDOS = " + APELLIDOS;
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.REG_ABOGADOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        #endregion ABOGADOS

        #region ABOGADO_ARCHIVO
        #endregion
    }
}
