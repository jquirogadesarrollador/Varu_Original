using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.almacen
{
    public class criteriosEvaluacionProveedores
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_CRITERIO = 0;
        private String _NOMBRE = null;
        private Int32 _MAXIMO_VALOR = 0;
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

        public Decimal ID_CRITERIO
        {
            get { return _ID_CRITERIO; }
            set { _ID_CRITERIO = value; }
        }

        public String NOMBRE
        {
            get { return _NOMBRE; }
            set { _NOMBRE = value; }
        }

        public Int32 MAXIMO_VALOR
        {
            get { return _MAXIMO_VALOR; }
            set { _MAXIMO_VALOR = value; }
        }
        #endregion propiedades

        #region constructores
        public criteriosEvaluacionProveedores(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region alm_criterios_calificacion
        public DataTable ObtenerCriteriosCalificacionPorIdCategoriaYTipo(Decimal ID_CATEGORIA, String TIPO_PROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_CRITERIOS_CALIFICACION_OBTENER_POR_ID_CATEGORIA_Y_TIPO ";

            #region validaciones
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CATEGORIA no puede ser 0";
            }

            if (String.IsNullOrEmpty(TIPO_PROCESO) == false)
            {
                sql += "'" + TIPO_PROCESO + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo TIPO no puede ser 0";
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

        public Boolean AdicionarCriteriosACategoriaYProceso(Decimal ID_CATEGORIA, String TIPO_PROCESO, List<criteriosEvaluacionProveedores> listaCriterios)
        {
            Boolean ejecutar = true;

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    DataTable tablaCriteriosActuales = ObtenerCriteriosCalificacionPorIdCategoriaYTipo(ID_CATEGORIA, TIPO_PROCESO);
                    Boolean verificador = false;

                    foreach (DataRow fila in tablaCriteriosActuales.Rows)
                    {
                        verificador = false;
                        foreach (criteriosEvaluacionProveedores criterio in listaCriterios)
                        {
                            if (Convert.ToDecimal(fila["ID_CRITERIO"]) == criterio.ID_CRITERIO)
                            {
                                verificador = true;
                                break;
                            }
                        }

                        if (verificador == false)
                        {
                            if (DesactivarCriterio(Convert.ToDecimal(fila["ID_CRITERIO"]), conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                ejecutar = false;
                                break;
                            }
                        }
                    }

                    if (ejecutar == true)
                    {
                        foreach (criteriosEvaluacionProveedores criterio in listaCriterios)
                        {
                            if (criterio.ID_CRITERIO == 0)
                            {
                                if (AdicionarnuevoCriterio(criterio.NOMBRE, TIPO_PROCESO, ID_CATEGORIA, criterio.MAXIMO_VALOR, conexion) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    ejecutar = false;
                                    break;
                                }
                            }
                        }

                        if (ejecutar == true)
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
                catch (Exception ex)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = ex.Message;
                    ejecutar = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return ejecutar;
        }

        public Decimal AdicionarnuevoCriterio(String NOMBRE, String TIPO, Decimal ID_CATEGORIA, Int32 MAXIMO_VALOR, Conexion conexion)
        {
            Decimal ID_CRITERIO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_CRITERIOS_CALIFICACION_ADICIONAR ";

            #region validaciones

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO) == false)
            {
                sql += "'" + TIPO + "', ";
                informacion += "TIPO = '" + TIPO + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CATEGORIA no puede ser vacio.";
                ejecutar = false;
            }

            if (MAXIMO_VALOR != 0)
            {
                sql += MAXIMO_VALOR + ", ";
                informacion += "MAXIMO_VALOR = '" + MAXIMO_VALOR + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "MAXIMO_VALOR = '0', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_CRITERIO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_CRITERIO <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para EL criterio " + NOMBRE + ".";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CRITERIOS_CALIFICACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro del criterio " + NOMBRE + ".";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_CRITERIO;
            }
            else
            {
                return 0;
            }
        }

        public Boolean DesactivarCriterio(Decimal ID_CRITERIO, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_CRITERIOS_CALIFICACION_DESACTIVAR ";

            #region validaciones

            if (ID_CRITERIO != 0)
            {
                sql += ID_CRITERIO + ", ";
                informacion += "ID_CRITERIO = '" + ID_CRITERIO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CRITERIO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar desactivar el registro del criterio " + ID_CRITERIO + ".";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CRITERIOS_CALIFICACION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la desactivación del criterio " + ID_CRITERIO + ".";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion alm_criterios_calificacion

        #endregion metodos
    }
}