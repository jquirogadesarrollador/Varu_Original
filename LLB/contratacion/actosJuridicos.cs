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
    public class actosjuridicos
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
        public actosjuridicos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region TUTELAS
        public DataTable ObtenerTutelasPorIdSolidcitud(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_tutelas_obtener_por_id_solicitud ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = " + ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
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

        public DataTable ObtenerEmbargosPorIdSolidcitud(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_embargos_obtener_por_id_solicitud ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = " + ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
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

        public DataTable ObtenerTutelasPorIdSolidcitudRegistroContrato(Decimal ID_SOLICITUD, Decimal REGISTRO_CONTRATO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_tutelas_obtener_por_id_solicitud_registro_contrato ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO;
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser 0\n";
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

        public DataTable ObtenerTutelasActivas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_tutelas_obtener_activas";

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


        public DataTable ObtenerDescargosActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_acta_desc_obtener_abiertos";

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

        public DataTable ObtenerTutelasPorIdTutela(Decimal ID_TUTELA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_tutelas_ObtenerPorIdTutela ";

            #region validaciones

            if (ID_TUTELA != 0)
            {
                sql += ID_TUTELA;
                informacion += "ID_TUTELA = " + ID_TUTELA;
            }
            else
            {
                MensajeError += "El campo ID_TUTELA no puede ser 0\n";
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

        public Decimal AdicionarTutela(Decimal ID_SOLICITUD,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            Byte[] ARCHIVO_PETICION,
            String ARCHIVO_PETICION_EXTENSION,
            Int32 ARCHIVO_PETICION_TAMANO,
            String ARCHIVO_PETICION_TYPE,
            String FALLO,
            String RESULTADO_FALLO,
            Byte[] ARCHIVO_FALLO,
            String ARCHIVO_FALLO_EXTENSION,
            Int32 ARCHIVO_FALLO_TAMANO,
            String ARCHIVO_FALLO_TYPE,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES
        )
        {
            String ID = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_tutelas_adicionar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + FECHA_R.ToString("u") + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString("u") + "', ";

            sql += "'" + FECHA_NOTIFICACION.ToString("u") + "', ";
            informacion += "FECHA_NOTIFICACION = '" + FECHA_NOTIFICACION.ToString("u") + "', ";

            sql += "'" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";


            if (FECHA_CONTESTACION == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NULL', ";
            }
            else
            {
                sql += "'" + FECHA_CONTESTACION.ToString("u") + "', ";
                informacion += "FECHA_CONTESTACION = '" + FECHA_CONTESTACION.ToString("u") + "', ";
            }

            if (String.IsNullOrEmpty(PETICION) == false)
            {
                sql += "'" + PETICION + "', ";
                informacion += "PETICION = '" + PETICION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PETICION = 'NULL', ";
            }

            if (ARCHIVO_PETICION != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_PETICION = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_PETICION_EXTENSION + "', ";
                informacion += "ARCHIVO_PETICION_EXTENSION = '" + ARCHIVO_PETICION_EXTENSION + "', ";

                sql += ARCHIVO_PETICION_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_PETICION_TAMANO = '" + ARCHIVO_PETICION_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_PETICION_TYPE + "', ";
                informacion += "ARCHIVO_PETICION_TYPE = '" + ARCHIVO_PETICION_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_TYPE = 'NULL', ";
            }

            if (String.IsNullOrEmpty(FALLO) == false)
            {
                sql += "'" + FALLO + "', ";
                informacion += "FALLO = '" + FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FALLO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == false)
            {
                sql += "'" + RESULTADO_FALLO + "', ";
                informacion += "RESULTADO_FALLO = '" + RESULTADO_FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "RESULTADO_FALLO = 'NULL', ";
            }


            if (ARCHIVO_FALLO != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_FALLO = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_FALLO_EXTENSION + "', ";
                informacion += "ARCHIVO_FALLO_EXTENSION = '" + ARCHIVO_FALLO_EXTENSION + "', ";

                sql += ARCHIVO_FALLO_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_FALLO_TAMANO = '" + ARCHIVO_FALLO_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_FALLO_TYPE + "', ";
                informacion += "ARCHIVO_FALLO_TYPE = '" + ARCHIVO_FALLO_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_TYPE = 'NULL', ";
            }

            if (RESPONSABLE != 0)
            {
                sql += "'" + RESPONSABLE + "', ";
                informacion += "RESPONSABLE = '" + RESPONSABLE + "', ";
            }
            else
            {
                MensajeError = "El campo ABOGADO RESPONSABLE no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO_CONTRATO no puede ser vacio.";
                ejecutar = false;
            }


            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONSIDERACIONES = 'NULL', ";
            }

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar tutela
                    ID = conexion.ExecuteEscalarParaAdicionarTutela(ID_SOLICITUD, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, ARCHIVO_PETICION, ARCHIVO_PETICION_EXTENSION, ARCHIVO_PETICION_TAMANO, ARCHIVO_PETICION_TYPE, FALLO, RESULTADO_FALLO, ARCHIVO_FALLO, ARCHIVO_FALLO_EXTENSION, ARCHIVO_FALLO_TAMANO, ARCHIVO_FALLO_TYPE, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES, Usuario);
                    #endregion adicionar tutela

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_TUTELAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
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

        public Boolean ActualizarTutela(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            Byte[] ARCHIVO_PETICION,
            String ARCHIVO_PETICION_EXTENSION,
            Int32 ARCHIVO_PETICION_TAMANO,
            String ARCHIVO_PETICION_TYPE,
            String FALLO,
            String RESULTADO_FALLO,
            Byte[] ARCHIVO_FALLO,
            String ARCHIVO_FALLO_EXTENSION,
            Int32 ARCHIVO_FALLO_TAMANO,
            String ARCHIVO_FALLO_TYPE,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES)
        {
            #region validaciones
            if (ID_TUTELA == 0)
            {
                MensajeError += "El campo ID_TUTELA no puede ser 0\n";
                return false;
            }

            if (RESPONSABLE == 0)
            {
                MensajeError += "El campo RESPONSABLE no puede ser 0\n";
                return false;
            }

            if (ID_EMPRESA == 0)
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
                return false;
            }

            if (REGISTRO_CONTRATO == 0)
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser 0\n";
                return false;
            }
            #endregion validaciones

            if ((ARCHIVO_PETICION == null) && (ARCHIVO_FALLO == null))
            {
                return actualizarTutelaSinArchivos(ID_TUTELA, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, FALLO, RESULTADO_FALLO, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES);
            }
            else
            {
                if ((ARCHIVO_PETICION != null) && (ARCHIVO_FALLO != null))
                {
                    if (actualizarTutelaConArchivos(ID_TUTELA, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, ARCHIVO_PETICION, ARCHIVO_PETICION_EXTENSION, ARCHIVO_PETICION_TAMANO, ARCHIVO_PETICION_TYPE, FALLO, RESULTADO_FALLO, ARCHIVO_FALLO, ARCHIVO_FALLO_EXTENSION, ARCHIVO_FALLO_TAMANO, ARCHIVO_FALLO_TYPE, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if ((ARCHIVO_PETICION != null) && (ARCHIVO_FALLO == null))
                    {
                        if (actualizarTutelaConArchivoPeticion(ID_TUTELA, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, ARCHIVO_PETICION, ARCHIVO_PETICION_EXTENSION, ARCHIVO_PETICION_TAMANO, ARCHIVO_PETICION_TYPE, FALLO, RESULTADO_FALLO, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES) == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (actualizarTutelaConArchivoFallo(ID_TUTELA, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, FALLO, RESULTADO_FALLO, ARCHIVO_FALLO, ARCHIVO_FALLO_EXTENSION, ARCHIVO_FALLO_TAMANO, ARCHIVO_FALLO_TYPE, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES) == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        private Boolean actualizarTutelaSinArchivos(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            String FALLO,
            String RESULTADO_FALLO,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_tutelas_actualizar_sin_archivos ";

            #region validaciones
            if (ID_TUTELA != 0)
            {
                sql += ID_TUTELA + ", ";
                informacion += "ID_TUTELA = '" + ID_TUTELA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_TUTELA no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + FECHA_R.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";

            sql += "'" + FECHA_NOTIFICACION.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";
            informacion += "FECHA_NOTIFICACION = '" + FECHA_NOTIFICACION.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";

            sql += "'" + FECHA_PLAZO_CONTESTA.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + FECHA_PLAZO_CONTESTA.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";

            if (FECHA_CONTESTACION == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NULL', ";
            }
            else
            {
                sql += "'" + FECHA_CONTESTACION.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";
                informacion += "FECHA_CONTESTACION = '" + FECHA_CONTESTACION.ToString("yyyy-MM-dd HH:mm:ss.f") + "', ";
            }

            if (String.IsNullOrEmpty(PETICION) == false)
            {
                sql += "'" + PETICION + "', ";
                informacion += "PETICION = '" + PETICION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PETICION = 'NULL', ";
            }

            if (String.IsNullOrEmpty(FALLO) == false)
            {
                sql += "'" + FALLO + "', ";
                informacion += "FALLO = '" + FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FALLO = 'NULL', ";
            }


            if (String.IsNullOrEmpty(RESULTADO_FALLO) == false)
            {
                sql += "'" + RESULTADO_FALLO + "', ";
                informacion += "RESULTADO_FALLO = '" + RESULTADO_FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "RESULTADO_FALLO = 'NULL', ";
            }

            if (RESPONSABLE != 0)
            {
                sql += "'" + RESPONSABLE + "', ";
                informacion += "RESPONSABLE = '" + RESPONSABLE + "', ";
            }
            else
            {
                MensajeError += "El campo ABOGADO RESPONSABLE no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONSIDERACIONES = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        ejecutadoCorrectamente = false;
                        conexion.DeshacerTransaccion();
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_TUTELAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                            MensajeError = "ERROR: Al intentar realizar la auditoría.";
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
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
                return true;
            }
            else
            {
                return false;
            }
        }
        private Decimal actualizarTutelaConArchivos(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            Byte[] ARCHIVO_PETICION,
            String ARCHIVO_PETICION_EXTENSION,
            Int32 ARCHIVO_PETICION_TAMANO,
            String ARCHIVO_PETICION_TYPE,
            String FALLO,
            String RESULTADO_FALLO,
            Byte[] ARCHIVO_FALLO,
            String ARCHIVO_FALLO_EXTENSION,
            Int32 ARCHIVO_FALLO_TAMANO,
            String ARCHIVO_FALLO_TYPE,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES)
        {
            Int32 actualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_tutelas_actualizar_con_archivos ";

            #region validaciones
            sql += ID_TUTELA + ", ";
            informacion += "ID_TUTELA = '" + ID_TUTELA + "', ";

            sql += "'" + FECHA_R.ToString("u") + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString("u") + "', ";

            sql += "'" + FECHA_NOTIFICACION.ToString("u") + "', ";
            informacion += "FECHA_NOTIFICACION = '" + FECHA_NOTIFICACION.ToString("u") + "', ";

            sql += "'" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";

            if (FECHA_CONTESTACION == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NULL', ";
            }
            else
            {
                sql += "'" + FECHA_CONTESTACION.ToString("u") + "', ";
                informacion += "FECHA_CONTESTACION = '" + FECHA_CONTESTACION.ToString("u") + "', ";
            }

            if (String.IsNullOrEmpty(PETICION) == false)
            {
                sql += "'" + PETICION + "', ";
                informacion += "PETICION = '" + PETICION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PETICION = 'NULL', ";
            }

            if (ARCHIVO_PETICION != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_PETICION = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_PETICION_EXTENSION + "', ";
                informacion += "ARCHIVO_PETICION_EXTENSION = '" + ARCHIVO_PETICION_EXTENSION + "', ";

                sql += ARCHIVO_PETICION_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_PETICION_TAMANO = '" + ARCHIVO_PETICION_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_PETICION_TYPE + "', ";
                informacion += "ARCHIVO_PETICION_TYPE = '" + ARCHIVO_PETICION_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_TYPE = 'NULL', ";
            }

            if (String.IsNullOrEmpty(FALLO) == false)
            {
                sql += "'" + FALLO + "', ";
                informacion += "FALLO = '" + FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FALLO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == false)
            {
                sql += "'" + RESULTADO_FALLO + "', ";
                informacion += "RESULTADO_FALLO = '" + RESULTADO_FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "RESULTADO_FALLO = 'NULL', ";
            }

            if (ARCHIVO_FALLO != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_FALLO = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_FALLO_EXTENSION + "', ";
                informacion += "ARCHIVO_FALLO_EXTENSION = '" + ARCHIVO_FALLO_EXTENSION + "', ";

                sql += ARCHIVO_FALLO_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_FALLO_TAMANO = '" + ARCHIVO_FALLO_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_FALLO_TYPE + "', ";
                informacion += "ARCHIVO_FALLO_TYPE = '" + ARCHIVO_FALLO_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_TYPE = 'NULL', ";
            }

            if (RESPONSABLE != 0)
            {
                sql += "'" + RESPONSABLE + "', ";
                informacion += "RESPONSABLE = '" + RESPONSABLE + "', ";
            }
            else
            {
                MensajeError = "El campo ABOGADO RESPONSABLE no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO_CONTRATO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONSIDERACIONES = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region actualizar tutelas
                    actualizados = conexion.ExecuteNonQueryParaActualizarTutelaConArchivos(ID_TUTELA, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, ARCHIVO_PETICION, ARCHIVO_PETICION_EXTENSION, ARCHIVO_PETICION_TAMANO, ARCHIVO_PETICION_TYPE, FALLO, RESULTADO_FALLO, ARCHIVO_FALLO, ARCHIVO_FALLO_EXTENSION, ARCHIVO_FALLO_TAMANO, ARCHIVO_FALLO_TYPE, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES, Usuario);
                    #endregion adicionar tutelas

                    #region auditoria
                    if (actualizados > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_TUTELAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
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
                        MensajeError = "ERROR: intenatar actualizar en la bd la tutela.";
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
                return actualizados;
            }
            else
            {
                return 0;
            }
        }


        private Decimal actualizarTutelaConArchivoPeticion(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            Byte[] ARCHIVO_PETICION,
            String ARCHIVO_PETICION_EXTENSION,
            Int32 ARCHIVO_PETICION_TAMANO,
            String ARCHIVO_PETICION_TYPE,
            String FALLO,
            String RESULTADO_FALLO,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES)
        {
            Int32 actualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_tutelas_actualizar_con_archivo_peticion ";

            #region validaciones
            sql += ID_TUTELA + ", ";
            informacion += "ID_TUTELA = '" + ID_TUTELA + "', ";

            sql += "'" + FECHA_R.ToString("u") + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString("u") + "', ";

            sql += "'" + FECHA_NOTIFICACION.ToString("u") + "', ";
            informacion += "FECHA_NOTIFICACION = '" + FECHA_NOTIFICACION.ToString("u") + "', ";

            sql += "'" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";

            if (FECHA_CONTESTACION == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NULL', ";
            }
            else
            {
                sql += "'" + FECHA_CONTESTACION.ToString("u") + "', ";
                informacion += "FECHA_CONTESTACION = '" + FECHA_CONTESTACION.ToString("u") + "', ";
            }

            if (String.IsNullOrEmpty(PETICION) == false)
            {
                sql += "'" + PETICION + "', ";
                informacion += "PETICION = '" + PETICION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PETICION = 'NULL', ";
            }

            if (ARCHIVO_PETICION != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_PETICION = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_PETICION_EXTENSION + "', ";
                informacion += "ARCHIVO_PETICION_EXTENSION = '" + ARCHIVO_PETICION_EXTENSION + "', ";

                sql += ARCHIVO_PETICION_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_PETICION_TAMANO = '" + ARCHIVO_PETICION_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_PETICION_TYPE + "', ";
                informacion += "ARCHIVO_PETICION_TYPE = '" + ARCHIVO_PETICION_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_PETICION_TYPE = 'NULL', ";
            }

            if (String.IsNullOrEmpty(FALLO) == false)
            {
                sql += "'" + FALLO + "', ";
                informacion += "FALLO = '" + FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FALLO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == false)
            {
                sql += "'" + RESULTADO_FALLO + "', ";
                informacion += "RESULTADO_FALLO = '" + RESULTADO_FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "RESULTADO_FALLO = 'NULL', ";
            }

            if (RESPONSABLE != 0)
            {
                sql += "'" + RESPONSABLE + "', ";
                informacion += "RESPONSABLE = '" + RESPONSABLE + "', ";
            }
            else
            {
                MensajeError = "El campo ABOGADO RESPONSABLE no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO_CONTRATO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONSIDERACIONES = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region actualizar tutelas
                    actualizados = conexion.ExecuteNonQueryParaActualizarTutelaConArchivoPeticion(ID_TUTELA, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, ARCHIVO_PETICION, ARCHIVO_PETICION_EXTENSION, ARCHIVO_PETICION_TAMANO, ARCHIVO_PETICION_TYPE, FALLO, RESULTADO_FALLO, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES, Usuario);
                    #endregion adicionar tutelas

                    #region auditoria
                    if (actualizados > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_TUTELAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
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
                        MensajeError = "ERROR: intenatar actualizar en la bd la tutela.";
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
                return actualizados;
            }
            else
            {
                return 0;
            }
        }

        private Decimal actualizarTutelaConArchivoFallo(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            String FALLO,
            String RESULTADO_FALLO,
            Byte[] ARCHIVO_FALLO,
            String ARCHIVO_FALLO_EXTENSION,
            Int32 ARCHIVO_FALLO_TAMANO,
            String ARCHIVO_FALLO_TYPE,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES)
        {
            Int32 actualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_tutelas_actualizar_con_archivo_fallo ";

            #region validaciones
            sql += ID_TUTELA + ", ";
            informacion += "ID_TUTELA = '" + ID_TUTELA + "', ";

            sql += "'" + FECHA_R.ToString("u") + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString("u") + "', ";

            sql += "'" + FECHA_NOTIFICACION.ToString("u") + "', ";
            informacion += "FECHA_NOTIFICACION = '" + FECHA_NOTIFICACION.ToString("u") + "', ";

            sql += "'" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + FECHA_PLAZO_CONTESTA.ToString("u") + "', ";

            if (FECHA_CONTESTACION == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NULL', ";
            }
            else
            {
                sql += "'" + FECHA_CONTESTACION.ToString("u") + "', ";
                informacion += "FECHA_CONTESTACION = '" + FECHA_CONTESTACION.ToString("u") + "', ";
            }

            if (String.IsNullOrEmpty(PETICION) == false)
            {
                sql += "'" + PETICION + "', ";
                informacion += "PETICION = '" + PETICION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "PETICION = 'NULL', ";
            }

            if (String.IsNullOrEmpty(FALLO) == false)
            {
                sql += "'" + FALLO + "', ";
                informacion += "FALLO = '" + FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FALLO = 'NULL', ";
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == false)
            {
                sql += "'" + RESULTADO_FALLO + "', ";
                informacion += "RESULTADO_FALLO = '" + RESULTADO_FALLO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "RESULTADO_FALLO = 'NULL', ";
            }

            if (ARCHIVO_FALLO != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_FALLO = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_FALLO_EXTENSION + "', ";
                informacion += "ARCHIVO_FALLO_EXTENSION = '" + ARCHIVO_FALLO_EXTENSION + "', ";

                sql += ARCHIVO_FALLO_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_FALLO_TAMANO = '" + ARCHIVO_FALLO_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_FALLO_TYPE + "', ";
                informacion += "ARCHIVO_FALLO_TYPE = '" + ARCHIVO_FALLO_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_FALLO_TYPE = 'NULL', ";
            }

            if (RESPONSABLE != 0)
            {
                sql += "'" + RESPONSABLE + "', ";
                informacion += "RESPONSABLE = '" + RESPONSABLE + "', ";
            }
            else
            {
                MensajeError = "El campo ABOGADO RESPONSABLE no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO_CONTRATO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CONSIDERACIONES = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region actualizar tutelas
                    actualizados = conexion.ExecuteNonQueryParaActualizarTutelaConArchivoFallo(ID_TUTELA, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, PETICION, FALLO, RESULTADO_FALLO, ARCHIVO_FALLO, ARCHIVO_FALLO_EXTENSION, ARCHIVO_FALLO_TAMANO, ARCHIVO_FALLO_TYPE, RESPONSABLE, ID_EMPRESA, REGISTRO_CONTRATO, CONSIDERACIONES, Usuario);
                    #endregion adicionar tutelas

                    #region auditoria
                    if (actualizados > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_TUTELAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
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
                        MensajeError = "ERROR: intenatar actualizar en la bd la tutela.";
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
                return actualizados;
            }
            else
            {
                return 0;
            }
        }

        public Boolean terminarProcesoTutela(Decimal ID_TUTELA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_tutelas_terminar_proceso ";

            #region validaciones
            if (ID_TUTELA != 0)
            {
                sql += ID_TUTELA + ", ";
                informacion += "ID_TUTELA= '" + ID_TUTELA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_TUTELA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_TUTELAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        #endregion TUTELAS

        #region DEMANDAS
        public DataTable ObtenerDemandasPorIdSolidcitudRegistroContrato(Decimal ID_SOLICITUD, Decimal REGISTRO_CONTRATO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_demandas_obtener_por_id_solicitud_registro_contrato ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO;
                informacion += "REGISTRO_CONTRATO = " + REGISTRO_CONTRATO;
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser 0\n";
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


        public DataTable ObtenerDemandasActivas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_demandas_obtener_activas";

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

        public DataTable ObtenerDemandasPorIdDemanda(Decimal ID_DEMANDA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_demandas_ObtenerPorIdDemanda ";

            #region validaciones

            if (ID_DEMANDA != 0)
            {
                sql += ID_DEMANDA;
                informacion += "ID_DEMANDA = " + ID_DEMANDA;
            }
            else
            {
                MensajeError += "El campo ID_DEMANDA no puede ser 0\n";
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


        public Decimal AdicionarDemanda(Decimal REGISTRO_CONTRATO,
            Decimal ID_EMPRESA,
            String RADICADO,
            Decimal ID_SOLICITUD,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String NUM_JUZGADO,
            String ESPECIALIDAD_JUZGADO,
            String CAT_JUZGADO,
            String ID_CIUDAD_JUZGADO,
            Decimal ID_ABOGADO,
            String PAZYPALVO,
            String DEMANDANTE,
            String DEMANDADO,
            String MAS_DEMANDANTES,
            Decimal PRETENSIONES,
            String TIPO_PROCESO,
            String CONSIDERACIONES,
            Byte[] ARCHIVO_CONDICION,
            String ARCHIVO_CONDICION_EXTENSION,
            Decimal ARCHIVO_CONDICION_TAMANO,
            String ARCHIVO_CONDICION_TYPE,
            Decimal VALOR_CONDICION,
            String MOTIVO_DEMANDA)
        {
            Boolean ejecutar = true;
            String registro_demanda = null;
            String registro_condicion = null;
            String sql = null;
            String informacion = null;

            tools _tools = new tools();

            if (REGISTRO_CONTRATO == 0)
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "', ";
            }

            if (ID_EMPRESA == 0)
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }

            if (String.IsNullOrEmpty(RADICADO) == true)
            {
                MensajeError += "El campo RADICADO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + RADICADO + "', ";
                informacion += "RADICADO = '" + RADICADO + "', ";
            }

            if (ID_SOLICITUD == 0)
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";

            if (FECHA_CONTESTACION != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
                informacion += "FECHA_CONTESTACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NNULL', ";
            }

            if (String.IsNullOrEmpty(NUM_JUZGADO) == true)
            {
                MensajeError += "El campo NUM_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + NUM_JUZGADO + "', ";
                informacion += "NUM_JUZGADO = '" + NUM_JUZGADO + "', ";
            }

            if (String.IsNullOrEmpty(ESPECIALIDAD_JUZGADO) == true)
            {
                MensajeError += "El campo ESPECIALIDAD_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + ESPECIALIDAD_JUZGADO + "', ";
                informacion += "ESPECIALIDAD_JUZGADO = '" + ESPECIALIDAD_JUZGADO + "', ";
            }

            if (String.IsNullOrEmpty(CAT_JUZGADO) == true)
            {
                MensajeError += "El campo CAT_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + CAT_JUZGADO + "', ";
                informacion += "CAT_JUZGADO = '" + CAT_JUZGADO + "', ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD_JUZGADO) == true)
            {
                MensajeError += "El campo ID_CIUDAD_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + ID_CIUDAD_JUZGADO + "', ";
                informacion += "ID_CIUDAD_JUZGADO = '" + ID_CIUDAD_JUZGADO + "', ";
            }

            if (ID_ABOGADO == 0)
            {
                MensajeError += "El campo ID_ABOGADO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO = '" + ID_ABOGADO + "', ";
            }

            if (String.IsNullOrEmpty(DEMANDANTE) == true)
            {
                MensajeError += "El campo DEMANDANTE no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + DEMANDANTE + "', ";
                informacion += "DEMANDANTE = '" + DEMANDANTE + "', ";
            }

            if (String.IsNullOrEmpty(DEMANDADO) == true)
            {
                MensajeError += "El campo DEMANDADO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + DEMANDADO + "', ";
                informacion += "DEMANDADO = '" + DEMANDADO + "', ";
            }

            if (String.IsNullOrEmpty(MAS_DEMANDANTES) == true)
            {
                sql += "null, ";
                informacion += "MAS_DEMANDANTES = 'null', ";
            }
            else
            {
                sql += "'" + MAS_DEMANDANTES + "', ";
                informacion += "MAS_DEMANDANTES = '" + MAS_DEMANDANTES + "', ";
            }

            if (String.IsNullOrEmpty(TIPO_PROCESO) == true)
            {
                MensajeError += "El campo TIPO_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }
            else
            {
                sql += "'" + TIPO_PROCESO + "', ";
                informacion += "TIPO_PROCESO = '" + TIPO_PROCESO + "', ";
            }


            if (String.IsNullOrEmpty(CONSIDERACIONES) == true)
            {
                sql += "null, ";
                informacion += "CONSIDERACIONES = 'null', ";
            }
            else
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar == true)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    registro_demanda = conexion.ExecuteEscalarParaAdicionarDemanda(REGISTRO_CONTRATO, ID_EMPRESA, RADICADO, ID_SOLICITUD, FECHA_R, FECHA_NOTIFICACION, FECHA_PLAZO_CONTESTA, FECHA_CONTESTACION, NUM_JUZGADO, ESPECIALIDAD_JUZGADO, CAT_JUZGADO, ID_CIUDAD_JUZGADO, ID_ABOGADO, PAZYPALVO, DEMANDANTE, DEMANDADO, MAS_DEMANDANTES, PRETENSIONES, TIPO_PROCESO, CONSIDERACIONES, Usuario, MOTIVO_DEMANDA);

                    if (Convert.ToDecimal(registro_demanda) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_DEMANDAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            registro_demanda = "0";
                        }
                        else
                        {
                            registro_condicion = conexion.ExecuteEscalarParaAdicionarCondicionNegociacionAbogado(Convert.ToDecimal(registro_demanda), FECHA_R, ARCHIVO_CONDICION, ARCHIVO_CONDICION_EXTENSION, ARCHIVO_CONDICION_TAMANO, ARCHIVO_CONDICION_TYPE, VALOR_CONDICION, Usuario);
                            if (Convert.ToDecimal(registro_condicion) > 0)
                            {
                                conexion.AceptarTransaccion();
                            }
                            else
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                                registro_demanda = "0";
                            }
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                        registro_demanda = "0";
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    registro_demanda = "0";
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return Convert.ToDecimal(registro_demanda);
        }

        public Boolean ActualizarDemanda(Decimal ID_DEMANDA,
            String RADICADO,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String NUM_JUZGADO,
            String ESPECIALIZADES_JUZGADO,
            String CAT_JUZGADO,
            String ID_CIUDAD_JUZGADO,
            Decimal ID_ABOGADO,
            String PAZYSALVO,
            String DEMANDANTE,
            String DEMANDADO,
            String MAS_DEMANDANTES,
            Decimal PRETENSIONES,
            String TIPO_PROCESO,
            String CONSIDERACIONES,
            String MOTIVO_DEMANDA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_demandas_actualizar ";

            #region validaciones
            if (ID_DEMANDA != 0)
            {
                sql += ID_DEMANDA + ", ";
                informacion += "ID_DEMANDA= '" + ID_DEMANDA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DEMANDA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RADICADO) == false)
            {
                sql += "'" + RADICADO + "', ";
                informacion += "RADICADO = '" + RADICADO + "', ";
            }
            else
            {
                MensajeError += "El campo RADICADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";

            if (FECHA_CONTESTACION == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
                informacion += "FECHA_CONTESTACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
            }

            if (String.IsNullOrEmpty(NUM_JUZGADO) == false)
            {
                sql += "'" + NUM_JUZGADO + "', ";
                informacion += "NUM_JUZGADO = '" + NUM_JUZGADO + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESPECIALIZADES_JUZGADO) == false)
            {
                sql += "'" + ESPECIALIZADES_JUZGADO + "', ";
                informacion += "ESPECIALIZADES_JUZGADO = '" + ESPECIALIZADES_JUZGADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESPECIALIZADES_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CAT_JUZGADO) == false)
            {
                sql += "'" + CAT_JUZGADO + "', ";
                informacion += "CAT_JUZGADO = '" + CAT_JUZGADO + "', ";
            }
            else
            {
                MensajeError += "El campo CAT_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD_JUZGADO) == false)
            {
                sql += "'" + ID_CIUDAD_JUZGADO + "', ";
                informacion += "ID_CIUDAD_JUZGADO = '" + ID_CIUDAD_JUZGADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD_JUZGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ABOGADO != 0)
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO= '" + ID_ABOGADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ABOGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(PAZYSALVO) == false)
            {
                sql += "'" + PAZYSALVO + "', ";
                informacion += "PAZYSALVO = '" + PAZYSALVO + "', ";
            }
            else
            {
                MensajeError += "El campo PAZYSALVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DEMANDANTE) == false)
            {
                sql += "'" + DEMANDANTE + "', ";
                informacion += "DEMANDANTE = '" + DEMANDANTE + "', ";
            }
            else
            {
                MensajeError += "El campo DEMANDANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DEMANDADO) == false)
            {
                sql += "'" + DEMANDADO + "', ";
                informacion += "DEMANDADO = '" + DEMANDADO + "', ";
            }
            else
            {
                MensajeError += "El campo DEMANDADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(MAS_DEMANDANTES) == false)
            {
                sql += "'" + MAS_DEMANDANTES + "', ";
                informacion += "MAS_DEMANDANTES = '" + MAS_DEMANDANTES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "MAS_DEMANDANTES = 'NULL', ";
            }

            if (PRETENSIONES != 0)
            {
                sql += PRETENSIONES + ", ";
                informacion += "PRETENSIONES = '" + PRETENSIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PRETENSIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_PROCESO) == false)
            {
                sql += "'" + TIPO_PROCESO + "', ";
                informacion += "TIPO_PROCESO = '" + TIPO_PROCESO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                MensajeError += "El campo CONSIDERACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario + "', ";

            if (String.IsNullOrEmpty(MOTIVO_DEMANDA) == false)
            {
                sql += "'" + MOTIVO_DEMANDA + "'";
                informacion += "MOTIVO_DEMANDA = '" + MOTIVO_DEMANDA + "'";
            }
            else
            {
                MensajeError += "El campo MOTIVO_DEMANDA no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_DEMANDAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean terminarProcesoDemanda(Decimal ID_DEMANDA, String RESULTADO_PROCESO, Decimal VALOR_RESULTADO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_demandas_terminar_proceso ";

            #region validaciones
            if (ID_DEMANDA != 0)
            {
                sql += ID_DEMANDA + ", ";
                informacion += "ID_DEMANDA = '" + ID_DEMANDA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DEMANDA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RESULTADO_PROCESO) == false)
            {
                sql += "'" + RESULTADO_PROCESO + "', ";
                informacion += "RESULTADO_PROCESO = '" + RESULTADO_PROCESO + "', ";
            }
            else
            {
                MensajeError += "El campo RESULTADO_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }

            if (VALOR_RESULTADO != 0)
            {
                sql += VALOR_RESULTADO + ", ";
                informacion += "VALOR_RESULTADO = '" + VALOR_RESULTADO + "', ";
            }
            else
            {
                sql += VALOR_RESULTADO + ", ";
                informacion += "VALOR_RESULTADO = '" + VALOR_RESULTADO + "', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_DEMANDAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        #endregion DEMANDAS

        #region DERECHOS PETICION
        public DataTable ObtenerDerechosPorIdSolidcitud(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_derechos_peticion_obtener_por_id_solicitud ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = " + ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
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

        public DataTable ObtenerDerechosPorIdSolidcitudRegistroContrato(Decimal ID_SOLICITUD, Decimal REGISTRO_CONTRATO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_derechos_peticion_obtener_por_id_solicitud_registro_contrato ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
                ejecutar = false;
            }

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO;
                informacion += "REGISTRO_CONTRATO = " + REGISTRO_CONTRATO;
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser 0\n";
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

        public DataTable ObtenerDerechosActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_derechos_peticion_obtener_activos";

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

            _dataTable.Columns.Add("ALERTA");
            DateTime fechaHoy = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime fechaSolicitud;

            DataRow filaInfoDerecho;
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                filaInfoDerecho = _dataTable.Rows[i];
                fechaSolicitud = Convert.ToDateTime(filaInfoDerecho["FECHA_NOTIFICACION"]);

                if (fechaHoy <= fechaSolicitud.AddDays(5))
                {
                    filaInfoDerecho["ALERTA"] = "BAJA";
                }
                else
                {
                    if (fechaHoy <= fechaSolicitud.AddDays(10))
                    {
                        filaInfoDerecho["ALERTA"] = "MEDIA";
                    }
                    else
                    {
                        filaInfoDerecho["ALERTA"] = "ALTA";
                    }
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerDerechosPorIdDerecho(Decimal ID_DERECHO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_derechos_peticion_ObtenerPorIdDerecho ";

            #region validaciones

            if (ID_DERECHO != 0)
            {
                sql += ID_DERECHO;
                informacion += "ID_DERECHO = " + ID_DERECHO;
            }
            else
            {
                MensajeError += "El campo ID_DERECHO no puede ser 0\n";
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

        public Decimal AdicionarDerecho(Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            Decimal ID_SOLICITUD,
            String SOLICITANTE,
            String ID_CIUDAD,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String MOTIVO,
            Decimal ID_ABOGADO,
            String CONSIDERACIONES)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_derechos_peticion_adicionar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(SOLICITANTE) != true)
            {
                sql += "'" + SOLICITANTE + "', ";
                informacion += "SOLICITANTE = '" + SOLICITANTE + "', ";
            }
            else
            {
                MensajeError += "El campo SOLICITANTE no puede ser nulo\n";
                ejecutar = false;
            }
            if (String.IsNullOrEmpty(ID_CIUDAD) != true)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";

            if (FECHA_CONTESTACION != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
                informacion += "FECHA_CONTESTACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NNULL', ";
            }

            if (String.IsNullOrEmpty(MOTIVO) != true)
            {
                sql += "'" + MOTIVO + "', ";
                informacion += "MOTIVO = '" + MOTIVO + "', ";
            }
            else
            {
                MensajeError += "El campo MOTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ABOGADO != 0)
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO = '" + ID_ABOGADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ABOGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONSIDERACIONES) != true)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                MensajeError += "El campo CONSIDERACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_DERECHOS_PETICION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Boolean ActualizarDerecho(Decimal ID_DERECHO,
            String SOLICITANTE,
            String ID_CIUDAD,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String MOTIVO,
            Decimal ID_ABOGADO,
            String CONSIDERACIONES)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_derechos_peticion_actualizar ";

            #region validaciones
            if (ID_DERECHO != 0)
            {
                sql += ID_DERECHO + ", ";
                informacion += "ID_DERECHO = '" + ID_DERECHO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DERECHO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(SOLICITANTE) == false)
            {
                sql += "'" + SOLICITANTE + "', ";
                informacion += "SOLICITANTE = '" + SOLICITANTE + "', ";
            }
            else
            {
                MensajeError += "El campo SOLICITANTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";
            informacion += "FECHA_PLAZO_CONTESTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "', ";

            if (FECHA_CONTESTACION == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CONTESTACION = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
                informacion += "FECHA_CONTESTACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CONTESTACION) + "', ";
            }

            if (String.IsNullOrEmpty(MOTIVO) == false)
            {
                sql += "'" + MOTIVO + "', ";
                informacion += "MOTIVO = '" + MOTIVO + "', ";
            }
            else
            {
                MensajeError += "El campo MOTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ABOGADO != 0)
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO = '" + ID_ABOGADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ABOGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                sql += "'" + CONSIDERACIONES + "', ";
                informacion += "CONSIDERACIONES = '" + CONSIDERACIONES + "', ";
            }
            else
            {
                MensajeError += "El campo CONSIDERACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_DERECHOS_PETICION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean terminarProcesoDerecho(Decimal ID_DERECHO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_derechos_peticion_terminar_proceso ";

            #region validaciones
            if (ID_DERECHO != 0)
            {
                sql += ID_DERECHO + ", ";
                informacion += "ID_DERECHO = '" + ID_DERECHO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DERECHO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_DERECHOS_PETICION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        #endregion DERECHOS PETICION

        #region ADJUNTOS DE ACTOS JURIDICOS
        public DataTable ObtenerAdjuntosActosJuridicosPorIdActo(Decimal ID_TUTELA, Decimal ID_DEMANDA, Decimal ID_DERECHO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_adjuntos_actos_juridicos_obtener_por_id_acto_juridico ";

            #region validaciones

            if (ID_TUTELA != 0)
            {
                sql += ID_TUTELA + ", ";
                informacion += "ID_TUTELA = '" + ID_TUTELA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_TUTELA = 'NULL', ";
            }

            if (ID_DEMANDA != 0)
            {
                sql += ID_DEMANDA + ", ";
                informacion += "ID_DEMANDA = '" + ID_DEMANDA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_DEMANDA = 'NULL', ";
            }

            if (ID_DERECHO != 0)
            {
                sql += ID_DERECHO;
                informacion += "ID_DERECHO = '" + ID_DERECHO + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "ID_DERECHO = 'NULL'";
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

        public DataTable ObtenerInfoAdjuntoPorIdAdjunto(Decimal ID_ADJUNTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_adjuntos_actos_juridicos_obtenerPorIdAdjunto ";

            #region validaciones

            if (ID_ADJUNTO != 0)
            {
                sql += ID_ADJUNTO + "";
                informacion += "ID_ADJUNTO = '" + ID_ADJUNTO + "'";
            }
            else
            {
                MensajeError = "El campo ID_ADJUNTO no puede ser nullo.";
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


        public Decimal AdicionarAdjuntoActoJuridico(Decimal ID_TUTELA,
            Decimal ID_DEMANDA,
            Decimal ID_DERECHO,
            DateTime FECHA_R,
            DateTime FECHA_ADJUNTO,
            String TITULO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            Decimal ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_TYPE)
        {
            String ID = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_adjuntos_actos_juridicos_adicionar ";

            #region validaciones

            if ((ID_TUTELA == 0) && (ID_DEMANDA == 0) && (ID_DERECHO == 0))
            {
                MensajeError = "ID_TUTELA, ID_DEMANDA, ID_DERECHO no pueden ser 0 al tiempo.";
                ejecutar = false;
            }
            else
            {
                if (ID_TUTELA != 0)
                {
                    sql += ID_TUTELA + ", ";
                    informacion += "ID_TUTELA = '" + ID_TUTELA + "', ";
                }
                else
                {
                    sql += "NULL, ";
                    informacion += "ID_TUTELA = 'NULL', ";
                }

                if (ID_DEMANDA != 0)
                {
                    sql += ID_DEMANDA + ", ";
                    informacion += "ID_DEMANDA = '" + ID_DEMANDA + "', ";
                }
                else
                {
                    sql += "NULL, ";
                    informacion += "ID_DEMANDA = 'NULL', ";
                }

                if (ID_DERECHO != 0)
                {
                    sql += ID_DERECHO + ", ";
                    informacion += "ID_DERECHO = '" + ID_DERECHO + "', ";
                }
                else
                {
                    sql += "NULL, ";
                    informacion += "ID_DERECHO = 'NULL', ";
                }
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ADJUNTO) + "', ";
            informacion += "FECHA_ADJUNTO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ADJUNTO) + "', ";

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError = "El campo TITULO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser vacio.";
                ejecutar = false;
            }

            if (ARCHIVO_ADJUNTO != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_ADJUNTO = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_ADJUNTO_EXTENSION + "', ";
                informacion += "ARCHIVO_ADJUNTO_EXTENSION = '" + ARCHIVO_ADJUNTO_EXTENSION + "', ";

                sql += ARCHIVO_ADJUNTO_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_ADJUNTO_TAMANO = '" + ARCHIVO_ADJUNTO_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_ADJUNTO_TYPE + "', ";
                informacion += "ARCHIVO_ADJUNTO_TYPE = '" + ARCHIVO_ADJUNTO_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO_TYPE = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar adjunto
                    ID = conexion.ExecuteEscalarParaAdicionarAdjuntoActo(ID_TUTELA, ID_DEMANDA, ID_DERECHO, FECHA_R, FECHA_ADJUNTO, TITULO, DESCRIPCION, ARCHIVO_ADJUNTO, ARCHIVO_ADJUNTO_EXTENSION, ARCHIVO_ADJUNTO_TAMANO, ARCHIVO_ADJUNTO_TYPE, Usuario);
                    #endregion adicionar adjunto

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ADJUNTOS_ACTOS_JURIDICOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
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
                        MensajeError = "ERROR: intenatar ingresar en la bd el adjunto.";
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
        #endregion ADJUNTOS DE ACTOS JURIDICOS

        #region JUZGADOS
        public DataTable ObtenerJuzgadosActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_juzgados_obtener_todos_activos ";

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
        #endregion JUZGADOS

        #region ABOGADOS
        public DataTable ObtenerAbogadosActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_abogados_obtener_todos_activos";

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

        public DataTable ObtenerAbogadosSertempoActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_abogados";

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

        #endregion ABOGADOS

        #region SABER EMPRESAS EN LAS QUE TRABAJÓ UN TRABAJADOR
        public DataTable ObtenerEmpresasEnLasQueTrabajo(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_empresas_en_las_que_trabajo ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = " + ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
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
        #endregion SABER EMPRESAS EN LAS QUE TRABAJÓ UN TRABAJADOR

        #region CONDICIONES DE NEGOCIACION ACTOS JURIDICOS
        public DataTable ObtenerCondicionesNegociacionPorIdActoJuridico(Decimal ID_ACTOS_JURIDICOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_condiciones_abogados_obtener_por_id_acto_juridico ";

            #region validaciones

            if (ID_ACTOS_JURIDICOS != 0)
            {
                sql += ID_ACTOS_JURIDICOS;
                informacion += "ID_ACTOS_JURIDICOS = " + ID_ACTOS_JURIDICOS;
            }
            else
            {
                MensajeError += "El campo ID_ACTOS_JURIDICOS no puede ser 0\n";
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

        public DataTable ObtenerInfoCondicionNegociacionPorId(Decimal ID_CONDICION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_condiciones_abogados_ObtenerPorIdCondicion ";

            #region validaciones

            if (ID_CONDICION != 0)
            {
                sql += ID_CONDICION + "";
                informacion += "ID_CONDICION = '" + ID_CONDICION + "'";
            }
            else
            {
                MensajeError = "El campo ID_CONDICION no puede ser nullo.";
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


        #endregion

        #region CON_REG_EMBARGOS
        public Decimal AdicionarEmbargo(Decimal ID_SOLICITUD,
            Decimal ID_EMPLEADO,
            String ID_CIUDAD,
            String COD_JUZGADO,
            String NOMBRE_JUZGADO,
            String NUM_CUENTA_JUZGADO,
            String NRO_RADICADO,
            String TIP_DOC_IDENTIDAD_DEMANDANTE,
            String NUM_DOC_IDENTIDAD_DEMANDANTE,
            String NOMBRE_DEMANDANTE,
            String TIPO_VALOR_DESCUENTO,
            Decimal VALOR_DESCUENTO_MENSUAL,
            Decimal TOPE_MAXIMO_SALARIO_MINIMO,
            Decimal VALOR_A_EMBARGAR,
            Boolean APLICA_SALARIO,
            Boolean APLICA_PRESTACIONES,
            Boolean APLICA_VACACIONES,
            Boolean APLICA_PAGOS_NO_SALARIALES,
            Boolean APLICA_INDEMNIZACIONES,
            String TIPO_EMBARGO,
            String CONCEPTO_EMBARGO,
            Boolean DESCONTAR_SALDO_AL_TERMINAR_CONTRATO)
        {
            Decimal ID_EMBARGO = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_embargos_adicionar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(COD_JUZGADO) == false)
            {
                sql += "'" + COD_JUZGADO + "', ";
                informacion += "COD_JUZGADO = '" + COD_JUZGADO + "', ";
            }
            else
            {
                MensajeError = "El campo COD_JUZGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_JUZGADO) == false)
            {
                sql += "'" + NOMBRE_JUZGADO + "', ";
                informacion += "NOMBRE_JUZGADO = '" + NOMBRE_JUZGADO + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE_JUZGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_CUENTA_JUZGADO) == false)
            {
                sql += "'" + NUM_CUENTA_JUZGADO + "', ";
                informacion += "NUM_CUENTA_JUZGADO = '" + NUM_CUENTA_JUZGADO + "', ";
            }
            else
            {
                MensajeError = "El campo NUM_CUENTA_JUZGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NRO_RADICADO) == false)
            {
                sql += "'" + NRO_RADICADO + "', ";
                informacion += "NRO_RADICADO = '" + NRO_RADICADO + "', ";
            }
            else
            {
                MensajeError = "El campo NRO_RADICADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIP_DOC_IDENTIDAD_DEMANDANTE) == false)
            {
                sql += "'" + TIP_DOC_IDENTIDAD_DEMANDANTE + "', ";
                informacion += "TIP_DOC_IDENTIDAD_DEMANDANTE = '" + TIP_DOC_IDENTIDAD_DEMANDANTE + "', ";
            }
            else
            {
                MensajeError = "El campo TIP_DOC_IDENTIDAD_DEMANDANTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD_DEMANDANTE) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD_DEMANDANTE + "', ";
                informacion += "NUM_DOC_IDENTIDAD_DEMANDANTE = '" + NUM_DOC_IDENTIDAD_DEMANDANTE + "', ";
            }
            else
            {
                MensajeError = "El campo NUM_DOC_IDENTIDAD_DEMANDANTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_DEMANDANTE) == false)
            {
                sql += "'" + NOMBRE_DEMANDANTE + "', ";
                informacion += "NOMBRE_DEMANDANTE = '" + NOMBRE_DEMANDANTE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE_DEMANDANTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_VALOR_DESCUENTO) == false)
            {
                sql += "'" + TIPO_VALOR_DESCUENTO + "', ";
                informacion += "TIPO_VALOR_DESCUENTO = '" + TIPO_VALOR_DESCUENTO + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_VALOR_DESCUENTO no puede ser vacio.";
                ejecutar = false;

            }

            if (VALOR_DESCUENTO_MENSUAL != 0)
            {
                sql += VALOR_DESCUENTO_MENSUAL.ToString().Replace(',', '.') + ", ";
                informacion += "VALOR_DESCUENTO_MENSUAL = '" + VALOR_DESCUENTO_MENSUAL.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0.00, ";
                informacion += "VALOR_DESCUENTO_MENSUAL = '0.00', ";

            }

            if (TOPE_MAXIMO_SALARIO_MINIMO != 0)
            {
                sql += TOPE_MAXIMO_SALARIO_MINIMO.ToString().Replace(',', '.') + ", ";
                informacion += "TOPE_MAXIMO_SALARIO_MINIMO = '" + TOPE_MAXIMO_SALARIO_MINIMO.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0.00, ";
                informacion += "TOPE_MAXIMO_SALARIO_MINIMO = '0.00', ";

            }

            if (VALOR_A_EMBARGAR != 0)
            {
                sql += VALOR_A_EMBARGAR.ToString().Replace(',', '.') + ", ";
                informacion += "VALOR_A_EMBARGAR = '" + VALOR_A_EMBARGAR.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0.00, ";
                informacion += "TOPE_MAXIMO_SALARIO_MINIMO = '0.00', ";

            }

            if (APLICA_SALARIO == true)
            {
                sql += "'True', ";
                informacion += "APLICA_SALARIO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_SALARIO = 'False', ";

            }

            if (APLICA_PRESTACIONES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_PRESTACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_PRESTACIONES = 'False', ";
            }

            if (APLICA_VACACIONES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_VACACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_VACACIONES = 'False', ";

            }

            if (APLICA_PAGOS_NO_SALARIALES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_PAGOS_NO_SALARIALES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_PAGOS_NO_SALARIALES = 'False', ";
            }

            if (APLICA_INDEMNIZACIONES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_INDEMNIZACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_INDEMNIZACIONES = 'False', ";
            }

            if (String.IsNullOrEmpty(TIPO_EMBARGO) == false)
            {
                sql += "'" + TIPO_EMBARGO + "', ";
                informacion += "TIPO_EMBARGO = '" + TIPO_EMBARGO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_EMBARGO = 'null', ";
            }

            if (String.IsNullOrEmpty(CONCEPTO_EMBARGO) == false)
            {
                sql += "'" + CONCEPTO_EMBARGO + "', ";
                informacion += "CONCEPTO_EMBARGO = '" + CONCEPTO_EMBARGO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CONCEPTO_EMBARGO = 'null', ";
            }

            if (DESCONTAR_SALDO_AL_TERMINAR_CONTRATO == true)
            {
                sql += "'True', ";
                informacion += "DESCONTAR_SALDO_AL_TERMINAR_CONTRATO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "DESCONTAR_SALDO_AL_TERMINAR_CONTRATO = 'False', ";
            }

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    ID_EMBARGO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_EMBARGOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                    conexion.AceptarTransaccion();
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
                return ID_EMBARGO;
            }
            else
            {
                return 0;
            }
        }

        public DataTable ObtenerInformacionDeUnEmbargo(Decimal ID_EMBARGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_embargos_obtener_por_id_embargo " + ID_EMBARGO.ToString();

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



        public Boolean ActualizarEmbargo(Decimal ID_EMBARGO,
            String ID_CIUDAD,
            String COD_JUZGADO,
            String NOMBRE_JUZGADO,
            String NUM_CUENTA_JUZGADO,
            String NRO_RADICADO,
            String TIP_DOC_IDENTIDAD_DEMANDANTE,
            String NUM_DOC_IDENTIDAD_DEMANDANTE,
            String NOMBRE_DEMANDANTE,
            String TIPO_VALOR_DESCUENTO,
            Decimal VALOR_DESCUENTO_MENSUAL,
            Decimal TOPE_MAXIMO_SALARIO_MINIMO,
            Decimal VALOR_A_EMBARGAR,
            Boolean APLICA_SALARIO,
            Boolean APLICA_PRESTACIONES,
            Boolean APLICA_VACACIONES,
            Boolean APLICA_PAGOS_NO_SALARIALES,
            Boolean APLICA_INDEMNIZACIONES,
            String TIPO_EMBARGO,
            String CONCEPTO_EMBARGO,
            Boolean DESCONTAR_SALDO_AL_TERMINAR_CONTRATO,
            String ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_embargos_actualizar ";

            if (ID_EMBARGO != 0)
            {
                sql += ID_EMBARGO + ", ";
                informacion += "ID_EMBARGO = '" + ID_EMBARGO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMBARGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(COD_JUZGADO) == false)
            {
                sql += "'" + COD_JUZGADO + "', ";
                informacion += "COD_JUZGADO = '" + COD_JUZGADO + "', ";
            }
            else
            {
                MensajeError = "El campo COD_JUZGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_JUZGADO) == false)
            {
                sql += "'" + NOMBRE_JUZGADO + "', ";
                informacion += "NOMBRE_JUZGADO = '" + NOMBRE_JUZGADO + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE_JUZGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_CUENTA_JUZGADO) == false)
            {
                sql += "'" + NUM_CUENTA_JUZGADO + "', ";
                informacion += "NUM_CUENTA_JUZGADO = '" + NUM_CUENTA_JUZGADO + "', ";
            }
            else
            {
                MensajeError = "El campo NUM_CUENTA_JUZGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NRO_RADICADO) == false)
            {
                sql += "'" + NRO_RADICADO + "', ";
                informacion += "NRO_RADICADO = '" + NRO_RADICADO + "', ";
            }
            else
            {
                MensajeError = "El campo NRO_RADICADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIP_DOC_IDENTIDAD_DEMANDANTE) == false)
            {
                sql += "'" + TIP_DOC_IDENTIDAD_DEMANDANTE + "', ";
                informacion += "TIP_DOC_IDENTIDAD_DEMANDANTE = '" + TIP_DOC_IDENTIDAD_DEMANDANTE + "', ";
            }
            else
            {
                MensajeError = "El campo TIP_DOC_IDENTIDAD_DEMANDANTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD_DEMANDANTE) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD_DEMANDANTE + "', ";
                informacion += "NUM_DOC_IDENTIDAD_DEMANDANTE = '" + NUM_DOC_IDENTIDAD_DEMANDANTE + "', ";
            }
            else
            {
                MensajeError = "El campo NUM_DOC_IDENTIDAD_DEMANDANTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_DEMANDANTE) == false)
            {
                sql += "'" + NOMBRE_DEMANDANTE + "', ";
                informacion += "NOMBRE_DEMANDANTE = '" + NOMBRE_DEMANDANTE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE_DEMANDANTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_VALOR_DESCUENTO) == false)
            {
                sql += "'" + TIPO_VALOR_DESCUENTO + "', ";
                informacion += "TIPO_VALOR_DESCUENTO = '" + TIPO_VALOR_DESCUENTO + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_VALOR_DESCUENTO no puede ser vacio.";
                ejecutar = false;

            }

            if (VALOR_DESCUENTO_MENSUAL != 0)
            {
                sql += VALOR_DESCUENTO_MENSUAL.ToString().Replace(',', '.') + ", ";
                informacion += "VALOR_DESCUENTO_MENSUAL = '" + VALOR_DESCUENTO_MENSUAL.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0.00, ";
                informacion += "VALOR_DESCUENTO_MENSUAL = '0.00', ";

            }

            if (TOPE_MAXIMO_SALARIO_MINIMO != 0)
            {
                sql += TOPE_MAXIMO_SALARIO_MINIMO.ToString().Replace(',', '.') + ", ";
                informacion += "TOPE_MAXIMO_SALARIO_MINIMO = '" + TOPE_MAXIMO_SALARIO_MINIMO.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0.00, ";
                informacion += "TOPE_MAXIMO_SALARIO_MINIMO = '0.00', ";

            }

            if (VALOR_A_EMBARGAR != 0)
            {
                sql += VALOR_A_EMBARGAR.ToString().Replace(',', '.') + ", ";
                informacion += "VALOR_A_EMBARGAR = '" + VALOR_A_EMBARGAR.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0.00, ";
                informacion += "TOPE_MAXIMO_SALARIO_MINIMO = '0.00', ";

            }

            if (APLICA_SALARIO == true)
            {
                sql += "'True', ";
                informacion += "APLICA_SALARIO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_SALARIO = 'False', ";

            }

            if (APLICA_PRESTACIONES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_PRESTACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_PRESTACIONES = 'False', ";
            }

            if (APLICA_VACACIONES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_VACACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_VACACIONES = 'False', ";

            }

            if (APLICA_PAGOS_NO_SALARIALES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_PAGOS_NO_SALARIALES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_PAGOS_NO_SALARIALES = 'False', ";
            }

            if (APLICA_INDEMNIZACIONES == true)
            {
                sql += "'True', ";
                informacion += "APLICA_INDEMNIZACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APLICA_INDEMNIZACIONES = 'False', ";
            }

            if (String.IsNullOrEmpty(TIPO_EMBARGO) == false)
            {
                sql += "'" + TIPO_EMBARGO + "', ";
                informacion += "TIPO_EMBARGO = '" + TIPO_EMBARGO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_EMBARGO = 'null', ";
            }

            if (String.IsNullOrEmpty(CONCEPTO_EMBARGO) == false)
            {
                sql += "'" + CONCEPTO_EMBARGO + "', ";
                informacion += "CONCEPTO_EMBARGO = '" + CONCEPTO_EMBARGO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CONCEPTO_EMBARGO = 'null', ";
            }

            if (DESCONTAR_SALDO_AL_TERMINAR_CONTRATO == true)
            {
                sql += "'True', ";
                informacion += "DESCONTAR_SALDO_AL_TERMINAR_CONTRATO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "DESCONTAR_SALDO_AL_TERMINAR_CONTRATO = 'False', ";
            }

            sql += "'" + Usuario.ToString() + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(ACTIVO) == false)
            {
                sql += "'" + ACTIVO + "'";
                informacion += "ACTIVO = '" + ACTIVO + "'";
            }
            else
            {
                MensajeError = "El campo ACTIVO no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_EMBARGOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
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

            return ejecutadoCorrectamente;
        }
        #endregion CON_REG_EMBARGOS
        #endregion metodos
    }
}