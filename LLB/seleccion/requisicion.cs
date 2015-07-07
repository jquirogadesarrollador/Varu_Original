using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.seleccion
{
    public class requisicion
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
        public requisicion(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #region CON_REQUERIMIENTOS


        public Boolean ActualizarConRequerimeintos(int ID_REQUERIMIENTO,
            String TIPO_REQ,
            DateTime FECHA_REQUERIDA,
            int CANTIDAD,
            Decimal SALARIO,
            String HORARIO,
            String DURACION,
            String OBS_REQUERIMIENTO,
            String CIUDAD_REQ,
            Decimal REGISTRO_PERFIL,
            Decimal ID_SERVICIO_RESPECTIVO,
            Decimal REGISTRO_ENVIO_CANDIDATOS,
            DateTime FECHA_REFERENCIA_SISTEMA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_requerimientos_actualizar ";

            #region validaciones
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(TIPO_REQ)))
            {
                sql += " '" + TIPO_REQ + "' , ";
                informacion += "TIPO_REQ= '" + TIPO_REQ.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_REQ no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_REQUERIDA.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REQUERIDA) + "', ";
                informacion += "FECHA_REQUERIDA = '" + FECHA_REQUERIDA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_REQUERIDA no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD != 0)
            {
                sql += CANTIDAD + ", ";
                informacion += "CANTIDAD = '" + CANTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (SALARIO != 0)
            {
                sql += SALARIO.ToString().Replace(',', '.') + ", ";
                informacion += "SALARIO = '" + SALARIO.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(HORARIO)))
            {
                sql += " '" + HORARIO + "' , ";
                informacion += "HORARIO = '" + HORARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo HORARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DURACION)))
            {
                sql += " '" + DURACION + "' , ";
                informacion += "DURACION = '" + DURACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DURACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_REQUERIMIENTO)))
            {
                sql += " '" + OBS_REQUERIMIENTO + "' , ";
                informacion += "OBS_REQUERIMIENTO = '" + OBS_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIUDAD_REQ)))
            {
                sql += " '" + CIUDAD_REQ + "' , ";
                informacion += "CIUDAD_REQ = '" + CIUDAD_REQ.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD_REQ no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            if (REGISTRO_PERFIL != 0)
            {
                sql += REGISTRO_PERFIL + ", ";
                informacion += "REGISTRO_PERFIL= '" + REGISTRO_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SERVICIO_RESPECTIVO != 0)
            {
                sql += ID_SERVICIO_RESPECTIVO + ", ";
                informacion += "ID_SERVICIO_RESPECTIVO= '" + ID_SERVICIO_RESPECTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO_RESPECTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (REGISTRO_ENVIO_CANDIDATOS != 0)
            {
                sql += REGISTRO_ENVIO_CANDIDATOS + ", ";
                informacion += "REGISTRO_ENVIO_CANDIDATOS= '" + REGISTRO_ENVIO_CANDIDATOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENVIO_CANDIDATOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (FECHA_REFERENCIA_SISTEMA == new DateTime())
            {
                sql += "NULL";
                informacion += "FECHA_REFERENCIA_SISTEMA = 'NULL'";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REFERENCIA_SISTEMA) + "'";
                informacion += "FECHA_REFERENCIA_SISTEMA = '" + FECHA_REFERENCIA_SISTEMA.ToShortDateString() + "'";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarSueldoConRequerimientos(int ID_REQUERIMIENTO, Decimal SALARIO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_requerimientos_actualizar_sueldo ";

            #region validaciones
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (SALARIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO);
                informacion += "SALARIO = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO) + "'";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Decimal AdicionarConRequerimientos(String TIPO_REQ,
            DateTime FECHA_REQUERIDA,
            int ID_EMPRESA,
            int CANTIDAD,
            Decimal SALARIO,
            String HORARIO,
            String CIUDAD_CONTRATO,
            String DURACION,
            String OBS_REQUERIMIENTO,
            String CIUDAD_REQ,
            Decimal REGISTRO_PERFIL,
            Decimal ID_SERVICIO_COMPLEMENTARIO,
            Decimal REGISTRO_ENVIO_CANDIDATOS,
            DateTime FECHA_REFERECNIA_SSISTEMA)
        {
            String sql = null;
            String idRecComFuentes = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_requerimientos_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(TIPO_REQ)))
            {
                sql += " '" + TIPO_REQ + "' , ";
                informacion += "TIPO_REQ= '" + TIPO_REQ.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_REQ no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_REQUERIDA.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REQUERIDA) + "', ";
                informacion += "FECHA_REQUERIDA= '" + FECHA_REQUERIDA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_REQUERIDA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD != 0)
            {
                sql += CANTIDAD + ", ";
                informacion += "CANTIDAD = '" + CANTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (SALARIO != 0)
            {
                sql += SALARIO.ToString().Replace(',', '.') + ", ";
                informacion += "SALARIO = '" + SALARIO.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(HORARIO)))
            {
                sql += " '" + HORARIO + "' , ";
                informacion += "HORARIO = '" + HORARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo HORARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIUDAD_CONTRATO)))
            {
                sql += " '" + CIUDAD_CONTRATO + "' , ";
                informacion += "CIUDAD_CONTRATO = '" + CIUDAD_CONTRATO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DURACION)))
            {
                sql += " '" + DURACION + "' , ";
                informacion += "DURACION = '" + DURACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DURACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_REQUERIMIENTO)))
            {
                sql += " '" + OBS_REQUERIMIENTO + "' , ";
                informacion += "OBS_REQUERIMIENTO = '" + OBS_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIUDAD_REQ)))
            {
                sql += " '" + CIUDAD_REQ + "' , ";
                informacion += "CIUDAD_REQ = '" + CIUDAD_REQ.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD_REQ no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (REGISTRO_PERFIL != 0)
            {
                sql += REGISTRO_PERFIL + ", ";
                informacion += "REGISTRO_PERFIL= '" + REGISTRO_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SERVICIO_COMPLEMENTARIO != 0)
            {
                sql += ID_SERVICIO_COMPLEMENTARIO + ", ";
                informacion += "ID_SERVICIO_COMPLEMENTARIO= '" + ID_SERVICIO_COMPLEMENTARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO_COMPLEMENTARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (REGISTRO_ENVIO_CANDIDATOS != 0)
            {
                sql += REGISTRO_ENVIO_CANDIDATOS + ", ";
                informacion += "REGISTRO_ENVIO_CANDIDATOS= '" + REGISTRO_ENVIO_CANDIDATOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_ENVIO_CANDIDATOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (FECHA_REFERECNIA_SSISTEMA == new DateTime())
            {
                sql += "NULL";
                informacion += "FECHA_REFERENCIA_SISTEMA = 'NULL'";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REFERECNIA_SSISTEMA) + "'";
                informacion += "FECHA_REFERENCIA_SISTEMA = '" + FECHA_REFERECNIA_SSISTEMA.ToShortDateString() + "'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecComFuentes = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecComFuentes))) return Convert.ToDecimal(idRecComFuentes);
            else return 0;
        }

        public DataTable ObtenerComRequerimientoPorIdRequerimiento(Decimal ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_id ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerComRequerimientoPorIdRequerimiento(Decimal ID_REQUERIMIENTO,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_id ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerComRequerimientoPorUsuLog(String USU_LOG)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_usu_log ";

            if (!(String.IsNullOrEmpty(USU_LOG)))
            {
                sql += "'" + USU_LOG + "'";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

        public DataTable ObtenerComRequerimientoPorUsuLogFiltroRegional(String REGIONAL)
        {

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_usu_log_filtro_regional ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REGIONAL)))
            {
                sql += "'" + REGIONAL + "'";
            }
            else
            {
                MensajeError += "El campo REGIONAL no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

        public DataTable ObtenerComRequerimientoPorUsuLogFiltroCiudad(String CIUDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_usu_log_filtro_ciudad ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIUDAD)))
            {
                sql += "'" + CIUDAD + "'";
            }
            else
            {
                MensajeError += "El campo CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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
        public DataTable ObtenerComRequerimientoPorUsuLogFiltroCliente(String CLIENTE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_usu_log_filtro_Cliente ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CLIENTE)))
            {
                sql += "'" + CLIENTE + "'";
            }
            else
            {
                MensajeError += "El campo CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

        public DataTable ObtenerComRequerimientoPorUsuLogFiltroREQ(String REQ)
        {

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "dbo.usp_con_requerimientos_obtener_por_usu_log_filtro_REQ ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REQ)))
            {
                sql += "'" + REQ + "'";
            }
            else
            {
                MensajeError += "El campo CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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
        public Decimal ObtenerComRequerimientoPorIdRequerimientoPorContratar(Decimal ID_REQUERIMIENTO)
        {
            Decimal cantidad = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_contratar ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidad = Convert.ToDecimal(conexion.ExecuteScalar(sql));


                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            return cantidad;
        }

        public Decimal ObtenerComRequerimientoPorIdRequerimientoContratados(Decimal ID_REQUERIMIENTO)
        {
            Decimal cantidad = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_contratados ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidad = Convert.ToDecimal(conexion.ExecuteScalar(sql));
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

            return cantidad;
        }



        public Decimal ObtenerComRequerimientoPorIdRequerimientoPorDisponibles(Decimal ID_REQUERIMIENTO)
        {
            Decimal cantidad = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_disponibles ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidad = Convert.ToDecimal(conexion.ExecuteScalar(sql));


                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            return cantidad;
        }

        public DataTable ObtenerTablaRequerimientosUsuario()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            tools _tool = new tools();

            _dataTable = ObtenerComRequerimientoPorUsuLog(Usuario);

            return _dataTable;
        }

        public DataTable ObtenerRequerimientosPorNumeroDocumento(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtenerPorNumeroDocumento ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRequerimientosPorNombre(String NOMBRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtenerPorNombre ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public Decimal ObtenerAlertas(Decimal ID_REQUERIMIENTO)
        {
            Decimal cantidad = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_alerta ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidad = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            return cantidad;
        }

        public DataTable ObtenerComRequerimientoPorIdEmpresa(Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerComRequerimientoPorRazSocial(String RAZ_SOCIAL, String CANCELADO, String CUMPLIDO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_raz_social ";

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "',";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo RAZ_SOCIAL no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CANCELADO)))
            {
                sql += "'" + CANCELADO + "',";
                informacion += "CANCELADO = '" + CANCELADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo CANCELADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CUMPLIDO)))
            {
                sql += "'" + CUMPLIDO + "', ";
                informacion += "CUMPLIDO = '" + CUMPLIDO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CUMPLIDO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerComRequerimientoPorCodEmpresa(String COD_EMPRESA, String CANCELADO, String CUMPLIDO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_cod_empresa ";

            if (!(String.IsNullOrEmpty(COD_EMPRESA)))
            {
                sql += "'" + COD_EMPRESA + "',";
                informacion += "COD_EMPRESA = '" + COD_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo COD_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CANCELADO)))
            {
                sql += "'" + CANCELADO + "',";
                informacion += "CANCELADO = '" + CANCELADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo CANCELADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CUMPLIDO)))
            {
                sql += "'" + CUMPLIDO + "', ";
                informacion += "CUMPLIDO = '" + CUMPLIDO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CUMPLIDO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_LOG = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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



        public DataTable ObtenerComRequerimientoPorIdEmpresaEstadoReq(Decimal ID_EMPRESA, String CANCELADO, String CUMPLIDO)
        {

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_IdEmpresaEstadosReq ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ",";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CANCELADO)))
            {
                sql += "'" + CANCELADO + "',";
            }
            else
            {
                MensajeError += "El campo CANCELADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CUMPLIDO)))
            {
                sql += "'" + CUMPLIDO + "', ";
            }
            else
            {
                MensajeError += "El campo CUMPLIDO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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


        private Boolean CambiarEstadoRequisicion(Decimal ID_REQUERIMIENTO,
            String CUMPLIDO,
            String CUMPLE_OPORTUNO,
            String CUMPLE_EFECTIVA,
            String MOTIVO_CUMPLIDO,
            String CANCELADO,
            String TIPO_CANCELA,
            String MOTIVO_CANCELA,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_requerimientos_actualizar_banderas ";

            #region validaciones
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CUMPLIDO)))
            {
                sql += " '" + CUMPLIDO + "' , ";
                informacion += "CUMPLIDO = '" + CUMPLIDO + "', ";
            }
            else
            {
                MensajeError += "El campo CUMPLIDO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CUMPLE_OPORTUNO)))
            {
                sql += " '" + CUMPLE_OPORTUNO + "' , ";
                informacion += "CUMPLE_OPORTUNO = '" + CUMPLE_OPORTUNO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CUMPLE_OPORTUNO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CUMPLE_EFECTIVA)))
            {
                sql += " '" + CUMPLE_EFECTIVA + "' , ";
                informacion += "CUMPLE_EFECTIVA = '" + CUMPLE_EFECTIVA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CUMPLE_EFECTIVA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOTIVO_CUMPLIDO)))
            {
                sql += " '" + MOTIVO_CUMPLIDO + "',  ";
                informacion += "MOTIVO_CUMPLIDO = '" + MOTIVO_CUMPLIDO + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "MOTIVO_CUMPLIDO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CANCELADO)))
            {
                sql += " '" + CANCELADO + "', ";
                informacion += "CANCELADO = '" + CANCELADO + "', ";
            }
            else
            {
                MensajeError += "El campo CANCELADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_CANCELA)))
            {
                sql += " '" + TIPO_CANCELA + "' , ";
                informacion += "TIPO_CANCELA = '" + TIPO_CANCELA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_CANCELA = 'null', ";
            }

            if (!(String.IsNullOrEmpty(MOTIVO_CANCELA)))
            {
                sql += " '" + MOTIVO_CANCELA + "' , ";
                informacion += "MOTIVO_CANCELA = '" + MOTIVO_CANCELA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "MOTIVO_CANCELA = 'null', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        private Boolean DesasociarPersoanalInconclusoDeRequisicion(Decimal ID_REQUERIMIENTO,
            Conexion conexion)
        {
            Boolean resultado = true;

            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_desligar_de_requerimiento ";

            #region validaciones
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                    resultado = false;
                }
            }
            else
            {
                resultado = false;
            }

            return resultado;
        }

        public Boolean ActualizarConRequerimeintosBanderas(Decimal ID_REQUERIMIENTO,
            String CUMPLIDO,
            String CUMPLE_OPORTUNO,
            String CUMPLE_EFECTIVA,
            String MOTIVO_CUMPLIDO,
            String CANCELADO,
            String TIPO_CANCELA,
            String MOTIVO_CANCELA)
        {
            Boolean correcto = true;

            Conexion conexion;

            conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (CambiarEstadoRequisicion(ID_REQUERIMIENTO, CUMPLIDO, CUMPLE_OPORTUNO, CUMPLE_EFECTIVA, MOTIVO_CUMPLIDO, CANCELADO, TIPO_CANCELA, MOTIVO_CANCELA, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    if (CUMPLIDO.ToUpper() == "S" || CANCELADO.ToUpper() == "S")
                    {
                        if (DesasociarPersoanalInconclusoDeRequisicion(ID_REQUERIMIENTO, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
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
                correcto = false;
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public DataTable ObtenerPersonasPorContratar()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_grilla_por_contratar ";
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (System.DateTime.Now.Date > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }


        public DataTable ObtenerPersonasPorAuditar(string dato = "*")
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_grilla_por_auditar "; 
            sql += "'" + Usuario + "' ";
            sql += ", '" + dato + "' ";

            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            informacion += ", BUSCAR = '" + dato + "' ";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            DateTime fechaActual = DateTime.Now;
            DateTime fechaCapturada;


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (!DBNull.Value.Equals(fila["FCH_INGRESO"]))
                {
                    fechaCapturada = Convert.ToDateTime(fila["FCH_INGRESO"]);

                    if (fechaCapturada.Date <= fechaActual.AddDays(-2).Date)
                    {
                        fila["ALERTA"] = "ALTA";
                    }
                    else
                    {
                        if (fechaCapturada.Date <= fechaActual.AddDays(-1).Date)
                        {
                            fila["ALERTA"] = "MEDIA";

                        }
                        else
                        {
                            if (fechaCapturada.Date >= fechaActual.Date)
                                fila["ALERTA"] = "BAJA";

                        }
                    }

                    if ((fila["ESTADO_PROCESO"].ToString().Trim() == "CONTRATADO") || (fila["ESTADO_PROCESO"].ToString().Trim() == "ELABORAR CONTRATO"))
                    {
                        fila["ESTADO_PROCESO"] = "SIN AUDITAR";
                        fila["USUARIO_PROCESO"] = "SIN ASIGNAR";
                    }
                }
            }
            return _dataTable;
        }


        public DataTable ObtenerContratosPosiblesDeAuditarPorNumIdentidad(String NUM_DOC_IDENTIDAD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_contratos_activos_auditados_por_num_identidad ";

            #region validaciones
            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NUM_DOC_IDENTIDAD no puede ser vacio.";
            }

            sql += "'" + Usuario + "'";


            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            DateTime fechaActual = DateTime.Now;
            DateTime fechaCapturada;


            foreach (DataRow fila in _dataTable.Rows)
            {
                fechaCapturada = Convert.ToDateTime(fila["FCH_INGRESO"]);

                if (fechaCapturada.Date <= fechaActual.AddDays(-2).Date)
                {
                    fila["ALERTA"] = "ALTA";
                }
                else
                {
                    if (fechaCapturada.Date <= fechaActual.AddDays(-1).Date)
                    {
                        fila["ALERTA"] = "MEDIA";

                    }
                    else
                    {
                        if (fechaCapturada.Date >= fechaActual.Date)
                            fila["ALERTA"] = "BAJA";

                    }
                }

                if ((fila["ESTADO_PROCESO"].ToString().Trim() == "CONTRATADO") || (fila["ESTADO_PROCESO"].ToString().Trim() == "ELABORAR CONTRATO"))
                {
                    fila["ESTADO_PROCESO"] = "SIN AUDITAR";
                    fila["USUARIO_PROCESO"] = "SIN ASIGNAR";
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerContratosPosiblesDeAuditarPorNombresEmpleado(String NOMBRES)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_contratos_activos_auditados_por_nombres_empleado ";

            #region validaciones
            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NOMBRES no puede ser vacio.";
            }

            sql += "'" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            DateTime fechaActual = DateTime.Now;
            DateTime fechaCapturada;


            foreach (DataRow fila in _dataTable.Rows)
            {
                fechaCapturada = Convert.ToDateTime(fila["FCH_INGRESO"]);

                if (fechaCapturada.Date <= fechaActual.AddDays(-2).Date)
                {
                    fila["ALERTA"] = "ALTA";
                }
                else
                {
                    if (fechaCapturada.Date <= fechaActual.AddDays(-1).Date)
                    {
                        fila["ALERTA"] = "MEDIA";

                    }
                    else
                    {
                        if (fechaCapturada.Date >= fechaActual.Date)
                            fila["ALERTA"] = "BAJA";

                    }
                }

                if ((fila["ESTADO_PROCESO"].ToString().Trim() == "CONTRATADO") || (fila["ESTADO_PROCESO"].ToString().Trim() == "ELABORAR CONTRATO"))
                {
                    fila["ESTADO_PROCESO"] = "SIN AUDITAR";
                    fila["USUARIO_PROCESO"] = "SIN ASIGNAR";
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerContratosPosiblesDeAuditarPorApellidosEmpleado(String APELLIDOS)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_contratos_activos_auditados_por_apellidos_empleado ";

            #region validaciones
            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo APELLIDOS no puede ser vacio.";
            }

            sql += "'" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            DateTime fechaActual = DateTime.Now;
            DateTime fechaCapturada;


            foreach (DataRow fila in _dataTable.Rows)
            {
                fechaCapturada = Convert.ToDateTime(fila["FCH_INGRESO"]);

                if (fechaCapturada.Date <= fechaActual.AddDays(-2).Date)
                {
                    fila["ALERTA"] = "ALTA";
                }
                else
                {
                    if (fechaCapturada.Date <= fechaActual.AddDays(-1).Date)
                    {
                        fila["ALERTA"] = "MEDIA";

                    }
                    else
                    {
                        if (fechaCapturada.Date >= fechaActual.Date)
                            fila["ALERTA"] = "BAJA";

                    }
                }

                if ((fila["ESTADO_PROCESO"].ToString().Trim() == "CONTRATADO") || (fila["ESTADO_PROCESO"].ToString().Trim() == "ELABORAR CONTRATO"))
                {
                    fila["ESTADO_PROCESO"] = "SIN AUDITAR";
                    fila["USUARIO_PROCESO"] = "SIN ASIGNAR";
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasPorContratarCiudad(String Ciudad)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_ciudad_contratar ";

            #region validaciones
            if (!(String.IsNullOrEmpty(Ciudad)))
            {
                sql += " '" + Ciudad + "',  ";
                informacion += "Ciudad = '" + Ciudad.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo Ciudad no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasPorContratarRegional(String Regional)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_regional_contratar ";

            #region validaciones
            if (!(String.IsNullOrEmpty(Regional)))
            {
                sql += " '" + Regional + "',  ";
                informacion += "Regional = '" + Regional.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo Regional no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }
        public DataTable ObtenerPersonasPorContratarPorEmpresa(String RAZ_SOCIAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_empresa_contratar ";

            #region validaciones
            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += " '" + RAZ_SOCIAL + "',  ";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RAZ_SOCIAL no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasPorContratarPorNombres(String NOMBRES)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_por_nombres_contratar ";

            #region validaciones
            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += " '" + NOMBRES + "',  ";
                informacion += "NOMBRES = '" + NOMBRES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasPorContratarPorApellidos(String APELLIDOS)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_apellidos_contratar ";

            #region validaciones
            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += " '" + APELLIDOS + "',  ";
                informacion += "APELLIDOS = '" + APELLIDOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasPorContratarPorNumDocIdentificacion(String NUM_DOC_IDENTIFICACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad_contratar ";

            #region validaciones
            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIFICACION)))
            {
                sql += " '" + NUM_DOC_IDENTIFICACION + "',  ";
                informacion += "NUM_DOC_IDENTIFICACION = '" + NUM_DOC_IDENTIFICACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIFICACION no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasPorContratarPorIds(int ID_REQUERIMEINTO, int ID_SOLICITUD, int ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_ids ";

            #region validaciones
            if (ID_REQUERIMEINTO != 0)
            {
                sql += ID_REQUERIMEINTO + ",  ";
                informacion += "ID_REQUERIMEINTO = '" + ID_REQUERIMEINTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMEINTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ",  ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ",  ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerRequisicionesPorFiltro(String RAZ_SOCIAL, String CUMPLIDO, String CANCELADO, String REGIONAL, String CIUDAD, String PSICOLOGO, String TIPO, DateTime FECHA_INICIO, DateTime FECHA_FINAL)
        {

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tool = new tools();


            sql = "usp_con_requerimientos_obtener_por_filtros ";

            #region validaciones
            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "',  ";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "RAZ_SOCIAL = 'null', ";
            }
            if (!(String.IsNullOrEmpty(CUMPLIDO)))
            {
                sql += "'" + CUMPLIDO + "',  ";
                informacion += "CUMPLIDO = '" + CUMPLIDO.ToString() + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "CUMPLIDO = 'null', ";
            }
            if (!(String.IsNullOrEmpty(CANCELADO)))
            {
                sql += "'" + CANCELADO + "',  ";
                informacion += "CANCELADO = '" + CANCELADO.ToString() + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "CANCELADO = 'null', ";
            }
            if (!(String.IsNullOrEmpty(REGIONAL)))
            {
                sql += "'" + REGIONAL + "',  ";
                informacion += "REGIONAL = '" + REGIONAL.ToString() + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "REGIONAL = 'null', ";
            }
            if (!(String.IsNullOrEmpty(CIUDAD)))
            {
                sql += "'" + CIUDAD + "',  ";
                informacion += "CIUDAD = '" + CIUDAD.ToString() + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "CIUDAD = 'null', ";
            }
            if (!(String.IsNullOrEmpty(PSICOLOGO)))
            {
                sql += "'" + PSICOLOGO + "',  ";
                informacion += "PSICOLOGO = '" + PSICOLOGO.ToString() + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "PSICOLOGO = 'null', ";
            }
            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO + "',  ";
                informacion += "TIPO = '" + TIPO.ToString() + "', ";
            }
            else
            {
                sql += "null,  ";
                informacion += "TIPO = 'null', ";
            }
            if (FECHA_INICIO == new DateTime())
            {
                sql += "null,  ";
                informacion += "FECHA_INICIO = 'null', ";

            }
            else
            {
                sql += "'" + _tool.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "',  ";
                informacion += "FECHA_INICIO = '" + FECHA_INICIO.ToString() + "', ";
            }
            if (FECHA_FINAL == new DateTime())
            {
                sql += "null  ";
                informacion += "FECHA_FINAL = 'null' ";
            }
            else
            {
                sql += "'" + _tool.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "'  ";
                informacion += "FECHA_FINAL = '" + FECHA_FINAL.ToString() + "' ";
            }
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerPersonasContratadasPorEmpresa(String RAZ_SOCIAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_empresa_contratado ";

            #region validaciones
            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += " '" + RAZ_SOCIAL + "',  ";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RAZ_SOCIAL no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasContratadasPorNombres(String NOMBRES)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_por_nombres_contratado ";

            #region validaciones
            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += " '" + NOMBRES + "',  ";
                informacion += "NOMBRES = '" + NOMBRES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasContratadasPorApellidos(String APELLIDOS)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_apellidos_contratado ";

            #region validaciones
            if (!(String.IsNullOrEmpty(APELLIDOS)))
            {
                sql += " '" + APELLIDOS + "',  ";
                informacion += "APELLIDOS = '" + APELLIDOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }

        public DataTable ObtenerPersonasContratadasPorNumDocIdentificacion(String NUM_DOC_IDENTIFICACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_solicitudes_ingreso_obtener_Por_num_doc_identidad_contratado ";

            #region validaciones
            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIFICACION)))
            {
                sql += " '" + NUM_DOC_IDENTIFICACION + "',  ";
                informacion += "NUM_DOC_IDENTIFICACION = '" + NUM_DOC_IDENTIFICACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIFICACION no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS + ", " + tabla.REG_SOLICITUDES_INGRESO + ", " + tabla.VEN_EMPRESAS + ", " + tabla.REC_OCUPACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

            _dataTable.Columns.Add("ALERTA", typeof(String));


            foreach (DataRow fila in _dataTable.Rows)
            {
                if (Convert.ToDateTime(System.DateTime.Now.Date) > Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "ALTA";

                }
                else if (Convert.ToDateTime(System.DateTime.Now.Date) == Convert.ToDateTime(fila["fechaPorContrar"].ToString()).Date)
                {
                    fila["ALERTA"] = "MEDIA";

                }
                else
                {
                    fila["ALERTA"] = "BAJA";

                }
            }

            return _dataTable;
        }


        public DataTable ObtenerEstadoGestionReq(Decimal ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_faltantes_terminacion_requisicion ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

        public DataTable ObtenerEnvioEmailPorEmpresaCiudad(Decimal idEmpresa, String idCiudad)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_envio_email_new_requerimiento ";

            if (idEmpresa != 0)
            {
                sql += idEmpresa + ", ";
                informacion += "ID_EMPRESA = '" + idEmpresa.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo.\n";
                ejecutar = false;
            }

            if (string.IsNullOrEmpty(idCiudad) == false)
            {
                sql += "'" + idCiudad + "'";
                informacion += "ID_CIUDAD = '" + idCiudad + "'";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo.\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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



        #endregion CON_REQUERIMIENTOS

        #region CON_ASP_ENVIADOS_CLIENTE
        public Boolean ActualizarConAspEnviadosCliente(int REGISTRO, int ID_SOLICITUD, int ID_EMPRESA, int ID_REQUERIMIENTO, DateTime FECHA_R)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_asp_enviados_cliente_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + ", ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ASP_ENVIADOS_CLIENTE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Decimal AdicionarConAspEnviadosCliente(int ID_SOLICITUD, int ID_EMPRESA, int ID_REQUERIMIENTO, DateTime FECHA_R)
        {
            String sql = null;

            Int32 numregistrosIngresados = 0;

            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_asp_enviados_cliente_adicionar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    numregistrosIngresados = Convert.ToInt32(conexion.ExecuteScalar(sql));

                    if (numregistrosIngresados <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        numregistrosIngresados = 0;
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(Usuario, tabla.CON_ASP_ENVIADOS_CLIENTE, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            numregistrosIngresados = 0;
                        }
                        #endregion auditoria
                    }

                    if (numregistrosIngresados > 0)
                    {
                        conexion.AceptarTransaccion();
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    numregistrosIngresados = 0;
                    conexion.DeshacerTransaccion();
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                numregistrosIngresados = 0;
            }

            return numregistrosIngresados;
        }

        public DataTable ObtenerConAspEnviadosClientePorIdEmpresa(int ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_asp_enviados_cliente_obtener_por_id_Empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ASP_ENVIADOS_CLIENTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerConAspEnviadosClientePorIdRequerimiento(Decimal ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_asp_enviados_cliente_obtener_por_id_requerimiento ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ASP_ENVIADOS_CLIENTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerConAspEnviadosClientePorIdRequerimientoEnCliente(Decimal ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_asp_enviados_cliente_obtener_por_id_requerimiento_en_cliente ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ASP_ENVIADOS_CLIENTE, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        #endregion CON_ASP_ENVIADOS_CLIENTE

        #region CON_REQ_SEG
        public Boolean ActualizarConRegSeg(int REGISTRO, int ID_REQUERIMIENTO, DateTime FECHA_R, String COMENTARIOS)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_req_seg_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + ", ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(COMENTARIOS)))
            {
                sql += "'" + COMENTARIOS + ", ";
                informacion += "COMENTARIOS= '" + COMENTARIOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COMENTARIOS no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQ_SEG, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Decimal AdicionarConRegSeg(int ID_REQUERIMIENTO, DateTime FECHA_R, String ACCION, String COMENTARIOS)
        {
            String sql = null;
            String idRecComFuentes = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_req_seg_adicionar ";
            #region validaciones


            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ACCION)))
            {
                sql += "'" + ACCION + "', ";
                informacion += "ACCION= '" + ACCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COMENTARIOS no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(COMENTARIOS)))
            {
                sql += "'" + COMENTARIOS + "', ";
                informacion += "COMENTARIOS= '" + COMENTARIOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo COMENTARIOS no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecComFuentes = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQ_SEG, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecComFuentes))) return Convert.ToDecimal(idRecComFuentes);
            else return 0;
        }

        public DataTable ObtenerConRegSegPorIdRequerimiento(int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_req_seg_otener_por_id_requerimiento ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REQ_SEG, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        #endregion CON_REQ_SEG


        public DataTable ObtenerTrabajadoresContratadosPorReq(Decimal idRequerimiento)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_contratos_contar_por_id_requerimiento ";

            if (idRequerimiento != 0)
            {
                sql += idRequerimiento;
                informacion += "ID_REQUERIMINETO = '" + idRequerimiento + "'";
            }
            else
            {
                MensajeError += "El campo ID DEL REQUERIMIENTO no puede ser nulo o cero.\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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


        public DataTable ObtenerEstadoGestionReqPorContratarContratados(Decimal ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_por_contratar_y_contratados_de_requisicion ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

        #endregion metodos
    }
}
