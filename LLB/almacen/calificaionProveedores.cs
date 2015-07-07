using System;
using System.Collections.Generic;
using System.Data;
using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class calificacionProveedores
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private Decimal _ID_CRITERIO = 0;
        private Decimal _CALIFICACION = 0;
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

        public Decimal CALFICACION
        {
            get { return _CALIFICACION; }
            set { _CALIFICACION = value; }
        }
        #endregion propiedades

        #region constructores
        public calificacionProveedores(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region alm_calificacion_proveedores

        public DataTable ObtenerCalificacionProveedorPorTipo(Decimal ID_PROVEEDOR, String TIPO_PROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_CALIFICACION_PROVEEDORES_OBTENER_POR_PROVEEDOR_Y_TIPO ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROVEEDOR no puede ser 0";
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

        public DataTable ObtenerCalificacionPorProveedor(Decimal ID_PROVEEDOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_CALIFICACION_PROVEEDORES_OBTENER_POR_PROVEEDOR ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROVEEDOR no puede ser 0";
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



        public DataTable ObtenerDetalleDeUnaCalificacion(Decimal ID_CALIFICACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_detalle_calificacion_proveedores_obtener_detalle_completo_por_id_calificacion ";

            #region validaciones
            if (ID_CALIFICACION != 0)
            {
                sql += ID_CALIFICACION;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CALIFICACION no puede ser 0";
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

        public DataTable ObtenerAlmcalificacionProveedorPorIdCalificacion(Decimal ID_CALIFICACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_CALIFICACION_PROVEEDORES_OBTENER_POR_ID ";

            #region validaciones
            if (ID_CALIFICACION != 0)
            {
                sql += ID_CALIFICACION;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CALIFICACION no puede ser 0";
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

        public Decimal AdicionarCalificacionYDetallesParaUnProceso(Decimal ID_PROVEEDOR,
            Decimal TOTAL_CALIFICACION,
            String OBSERVACIONES,
            String TIPO_PROCESO,
            String APROBADO,
            DateTime FECHA,
            String CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR,
            List<calificacionProveedores> listaCriteriosEvaluados,
            Byte[] ARCHIVO,
            Int32 ARCHIVO_TAMANO,
            String ARCHIVO_EXTENSION,
            String ARCHIVO_TYPE)
        {
            Decimal ID_CALIFICACION = 0;
            Decimal ID_DETALLE = 0;
            Decimal ID_DOCUMENTO_EVALUACION = 0;
            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_CALIFICACION = AdicionarAlmCalificacionProveedor(ID_PROVEEDOR, TOTAL_CALIFICACION, OBSERVACIONES, TIPO_PROCESO, APROBADO, FECHA, CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR, conexion);

                if (ID_CALIFICACION <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_CALIFICACION = 0;
                }
                else
                {
                    foreach (calificacionProveedores calificacionCriterio in listaCriteriosEvaluados)
                    {
                        ID_DETALLE = AdicionarAlmDetalleCalificacion(calificacionCriterio.ID_CRITERIO, ID_CALIFICACION, calificacionCriterio.CALFICACION, conexion);

                        if (ID_DETALLE <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            ID_CALIFICACION = 0;
                            verificador = false;
                            break;
                        }
                    }

                    if (verificador == true)
                    {
                        if (ARCHIVO != null)
                        {
                            ID_DOCUMENTO_EVALUACION = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaDocumentoCalificacionProveedor(ID_CALIFICACION, TIPO_PROCESO, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, Usuario));

                            if (ID_DOCUMENTO_EVALUACION <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                ID_CALIFICACION = 0;
                                verificador = false;
                            }
                        }
                    }
                }

                if (verificador == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch
            {
                conexion.DeshacerTransaccion();
                ID_CALIFICACION = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_CALIFICACION;
        }


        public Decimal AdicionarAlmCalificacionProveedor(Decimal ID_PROVEEDOR,
            Decimal TOTAL_CALIFICACION,
            String OBSERVACIONES,
            String TIPO_PROCESO,
            String APROBADO,
            DateTime FECHA,
            String CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR,
            Conexion conexion)
        {
            Decimal ID_CALIFICACION = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_CALIFICACION_PROVEEDORES_ADICIONAR ";

            #region validaciones

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PROVEEDOR no puede ser vacio.";
                ejecutar = false;
            }

            if (TOTAL_CALIFICACION != 0)
            {
                sql += TOTAL_CALIFICACION + ", ";
                informacion += "TOTAL_CALIFICACION = '" + TOTAL_CALIFICACION + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "TOTAL_CALIFICACION = '0', ";
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_PROCESO) == false)
            {
                sql += "'" + TIPO_PROCESO + "', ";
                informacion += "TIPO = '" + TIPO_PROCESO + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PROCESO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(APROBADO) == false)
            {
                sql += "'" + APROBADO + "', ";
                informacion += "APROBADO = '" + APROBADO + "', ";
            }
            else
            {
                MensajeError = "El campo APROBADO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            if (String.IsNullOrEmpty(CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR) == false)
            {
                sql += "'" + CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR + "', ";
                informacion += "CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR = '" + CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR + "', ";
            }
            else
            {
                MensajeError = "El campo CONTROL_ACTUALIZACION_ESTADO_PROVEEDOR no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_CALIFICACION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_CALIFICACION <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para la calificacion del proveedor " + ID_PROVEEDOR + ".";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CALIFICACION_PROVEEDORES, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la calificaion al proveedor " + ID_PROVEEDOR + ".";
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
                return ID_CALIFICACION;
            }
            else
            {
                return 0;
            }
        }

        public Decimal AdicionarAlmDetalleCalificacion(Decimal ID_CRITERIO,
            Decimal ID_CALIFICACION,
            Decimal CALIFICACION,
            Conexion conexion)
        {
            Decimal ID_DETALLE = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_DETALLE_CALIFICACION_PROVEEDORES_ADICIONAR ";

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

            if (ID_CALIFICACION != 0)
            {
                sql += ID_CALIFICACION + ", ";
                informacion += "ID_CALIFICACION = '" + ID_CALIFICACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CALIFICACION no puede ser vacio.";
                ejecutar = false;
            }

            if (CALIFICACION != 0)
            {
                sql += CALIFICACION + ", ";
                informacion += "CALIFICACION = '" + CALIFICACION + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "CALIFICACION = '0', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_DETALLE = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_DETALLE <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para el detalle de la calificacion:" + ID_CALIFICACION + ".";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_DETALLE_CALIFICACION_PROVEEDORES, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro del detalle de calificación " + ID_CALIFICACION + ".";
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
                return ID_DETALLE;
            }
            else
            {
                return 0;
            }
        }

        #endregion alm_calificacion_proveedores

        #endregion metodos
    }
}