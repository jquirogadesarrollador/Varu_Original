using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.programasRseGlobal
{
    public class SeguimientoAccidentes
    {
        #region variables

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        public enum EstadosAccidente
        {
            ABIERTO = 0,
            CERRADO,
            SEGUIMIENTO
        }

        public enum TiposArchivo
        {
            ENCUESTA = 1,
            ASISTENCIA,
            ADJUNTO
        }
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
        public SeguimientoAccidentes()
        {

        }
        public SeguimientoAccidentes(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerSeguimientoAccidentePorEstado(EstadosAccidente estado)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_seguimiento_accidentes_obtenerPorEstado ";

            sql += "'" + estado.ToString() + "'";

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

        public DataTable ObtenerSeguimientoAccidentePorIdSolicitud(Decimal idSolicitud)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_seguimiento_accidentes_obtenerPorIdSolicitud ";

            sql += idSolicitud.ToString();

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

        public DataTable ObtenerContratoActivoPorIdSolicitud(Decimal idSolicitud)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerContratoActivoPorIdSolicitud ";

            sql += idSolicitud.ToString();

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

        public DataTable ObtenerContratoPorIdEmpleado(Decimal idEmpleado)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerContratoPorIdEmpleado_so ";

            sql += idEmpleado.ToString();

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

        public DataTable ObtenerIncapacidadPorRegistro(Decimal registro)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_reg_incapacidades_obtener_por_registro_so ";

            sql += registro.ToString();

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


        public Decimal AdicionarRegistroProgSeguimientoAccidentes(Decimal ID_SOLICITUD,
            Decimal ID_EMPLEADO,
            DateTime FCH_REGISTRO,
            DateTime FCH_ACCIDENTE,
            String HORA_ACCIDENTE,
            DateTime FCH_TOPE_INVESTIGACION,
            String NRO_SINIESTRO,
            String DIA_SEMANA,
            String JORNADA,
            String DSC_ACCIDENTE,
            EstadosAccidente ESTADO)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_ACCIDENTE = 0;

            sql = "usp_prog_seguimiento_accidentes_adicionar ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (FCH_REGISTRO != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_REGISTRO) + "', ";
                informacion += "FCH_REGISTRO = '" + FCH_REGISTRO.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_REGISTRO = 'null', ";
            }

            if (FCH_ACCIDENTE != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_ACCIDENTE) + "', ";
                informacion += "FCH_ACCIDENTE = '" + FCH_ACCIDENTE.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_ACCIDENTE = 'null', ";
            }

            if (String.IsNullOrEmpty(HORA_ACCIDENTE) == false)
            {
                sql += "'" + HORA_ACCIDENTE + "', ";
                informacion += "HORA_ACCIDENTE = '" + HORA_ACCIDENTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "HORA_ACCIDENTE = 'null', ";
            }

            if (FCH_TOPE_INVESTIGACION != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_TOPE_INVESTIGACION) + "', ";
                informacion += "FCH_TOPE_INVESTIGACION = '" + FCH_TOPE_INVESTIGACION.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_TOPE_INVESTIGACION = 'null', ";
            }

            if (String.IsNullOrEmpty(NRO_SINIESTRO) == false)
            {
                sql += "'" + NRO_SINIESTRO + "', ";
                informacion += "NRO_SINIESTRO = '" + NRO_SINIESTRO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NRO_SINIESTRO = 'null', ";
            }

            if (String.IsNullOrEmpty(DIA_SEMANA) == false)
            {
                sql += "'" + DIA_SEMANA + "', ";
                informacion += "DIA_SEMANA = '" + DIA_SEMANA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DIA_SEMANA = 'null', ";
            }

            if (String.IsNullOrEmpty(JORNADA) == false)
            {
                sql += "'" + JORNADA + "', ";
                informacion += "JORNADA = '" + JORNADA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "JORNADA = 'null', ";
            }

            if (String.IsNullOrEmpty(DSC_ACCIDENTE) == false)
            {
                sql += "'" + DSC_ACCIDENTE + "', ";
                informacion += "DSC_ACCIDENTE = '" + DSC_ACCIDENTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DSC_ACCIDENTE = 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            sql += "'" + ESTADO.ToString() + "'";
            informacion += "ESTADO = '" + ESTADO.ToString() + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {

                    ID_ACCIDENTE = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_SEGUIMIENTO_ACCIDENTES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_ACCIDENTE = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return ID_ACCIDENTE;
        }

        public DataTable ObtenerSeguimientoAccidentePorIdAccidente(Decimal ID_ACCIDENTE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_seguimiento_accidentes_obtenerPorIdAccidente ";

            sql += ID_ACCIDENTE.ToString();

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

        public Boolean ActualizarRegistroProgSeguimientoAccidentes(Decimal ID_ACCIDENTE,
            String RIESGO_GENERADOR,
            String TIPO_ACCIDENTE,
            String TIPO_LESION,
            String PARTE_CUERPO,
            String AGENTE_LESION,
            String MECANISMO_LESION,
            String SITIO,
            String COND_INSEGURA,
            String ACTO_INSEGURO,
            String FACTORES_TRABAJO,
            String FACTORES_PERSONALES,
            Decimal ID_INCAPACIDAD,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_seguimiento_accidentes_actualizar ";

            #region validaciones
            if (ID_ACCIDENTE != 0)
            {
                sql += ID_ACCIDENTE + ", ";
                informacion += "ID_ACCIDENTE = '" + ID_ACCIDENTE + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ACCIDENTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RIESGO_GENERADOR) == false)
            {
                sql += "'" + RIESGO_GENERADOR + "', ";
                informacion += "RIESGO_GENERADOR = '" + RIESGO_GENERADOR + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "RIESGO_GENERADOR = 'null', ";
            }

            if (String.IsNullOrEmpty(TIPO_ACCIDENTE) == false)
            {
                sql += "'" + TIPO_ACCIDENTE + "', ";
                informacion += "TIPO_ACCIDENTE = '" + TIPO_ACCIDENTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_ACCIDENTE = 'null', ";
            }
            
            if (String.IsNullOrEmpty(TIPO_LESION) == false)
            {
                sql += "'" + TIPO_LESION + "', ";
                informacion += "TIPO_LESION = '" + TIPO_LESION + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_LESION = 'null', ";
            }

            if (String.IsNullOrEmpty(PARTE_CUERPO) == false)
            {
                sql += "'" + PARTE_CUERPO + "', ";
                informacion += "PARTE_CUERPO = '" + PARTE_CUERPO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "PARTE_CUERPO = 'null', ";
            }

            if (String.IsNullOrEmpty(AGENTE_LESION) == false)
            {
                sql += "'" + AGENTE_LESION + "', ";
                informacion += "AGENTE_LESION = '" + AGENTE_LESION + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "AGENTE_LESION = 'null', ";
            }

            if (String.IsNullOrEmpty(MECANISMO_LESION) == false)
            {
                sql += "'" + MECANISMO_LESION + "', ";
                informacion += "MECANISMO_LESION = '" + MECANISMO_LESION + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "MECANISMO_LESION = 'null', ";
            }

            if (String.IsNullOrEmpty(SITIO) == false)
            {
                sql += "'" + SITIO + "', ";
                informacion += "SITIO = '" + SITIO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "SITIO = 'null', ";
            }

            if (String.IsNullOrEmpty(COND_INSEGURA) == false)
            {
                sql += "'" + COND_INSEGURA + "', ";
                informacion += "COND_INSEGURA = '" + COND_INSEGURA + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "COND_INSEGURA = 'null', ";
            }

            if (String.IsNullOrEmpty(ACTO_INSEGURO) == false)
            {
                sql += "'" + ACTO_INSEGURO + "', ";
                informacion += "ACTO_INSEGURO = '" + ACTO_INSEGURO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ACTO_INSEGURO = 'null', ";
            }

            if (String.IsNullOrEmpty(FACTORES_TRABAJO) == false)
            {
                sql += "'" + FACTORES_TRABAJO + "', ";
                informacion += "FACTORES_TRABAJO = '" + FACTORES_TRABAJO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FACTORES_TRABAJO = 'null', ";
            }

            if (String.IsNullOrEmpty(FACTORES_PERSONALES) == false)
            {
                sql += "'" + FACTORES_PERSONALES + "', ";
                informacion += "FACTORES_PERSONALES = '" + FACTORES_PERSONALES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FACTORES_PERSONALES = 'null', ";
            }

            if (ID_INCAPACIDAD != 0)
            {
                sql += ID_INCAPACIDAD + ", ";
                informacion += "ID_INCAPACIDAD = '" + ID_INCAPACIDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_INCAPACIDAD = 'null', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_SEGUIMIENTO_ACCIDENTES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Decimal AdicionarRegistroProgDetSegAccidente(Decimal ID_ACCIDENTE,
            DateTime FECHA_SEGUIMIENTO,
            String SEGUIMIENTO,
            Boolean GENERA_COMPROMISO,
            Decimal ID_COMPROMISO_GENERADO,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_DETALLE_SEGUIMIENTO = 0;

            sql = "usp_prog_det_seg_accidentes_adicionar ";

            if (ID_ACCIDENTE != 0)
            {
                sql += ID_ACCIDENTE + ", ";
                informacion += "ID_ACCIDENTE = '" + ID_ACCIDENTE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ACCIDENTE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_SEGUIMIENTO) + "', ";
            informacion += "FECHA_SEGUIMIENTO = '" + FECHA_SEGUIMIENTO.ToShortDateString() + "', ";

            if (String.IsNullOrEmpty(SEGUIMIENTO) == false)
            {
                sql += "'" + SEGUIMIENTO + "', ";
                informacion += "SEGUIMIENTO = '" + SEGUIMIENTO + "', ";
            }
            else
            {
                MensajeError += "El campo SEGUIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (GENERA_COMPROMISO == true)
            {
                sql += "'True', ";
                informacion += "GENERA_COMPROMISO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "GENERA_COMPROMISO = 'False', ";
            }

            if (ID_COMPROMISO_GENERADO != 0)
            {
                sql += ID_COMPROMISO_GENERADO + ", ";
                informacion += "ID_COMPROMISO_GENERADO = '" + ID_COMPROMISO_GENERADO + "', ";
            }
            else
            {
                sql += ID_COMPROMISO_GENERADO + ", ";
                informacion += "ID_COMPROMISO_GENERADO = '" + ID_COMPROMISO_GENERADO + "', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_DETALLE_SEGUIMIENTO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DET_SEG_CASOS_SEVEROSS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_DETALLE_SEGUIMIENTO = 0;
                }
            }

            return ID_DETALLE_SEGUIMIENTO;
        }

        public Decimal AdicionarFactorAUnAccidente(Decimal ID_ACCIDENTE,
            Decimal ID_CAUSA,
            String VALOR_ITEM,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_RELACION = 0;

            sql = "usp_so_causas_basicas_perdias_por_accidente_adicionar ";

            if (ID_ACCIDENTE != 0)
            {
                sql += ID_ACCIDENTE + ", ";
                informacion += "ID_ACCIDENTE = '" + ID_ACCIDENTE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ACCIDENTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CAUSA != 0)
            {
                sql += ID_CAUSA + ", ";
                informacion += "ID_CAUSA = '" + ID_CAUSA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CAUSA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(VALOR_ITEM) == false)
            {
                sql += "'" + VALOR_ITEM + "', ";
                informacion += "VALOR_ITEM = '" + VALOR_ITEM + "', ";
            }
            else
            {
                MensajeError += "El campo VALOR_ITEM no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_RELACION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SO_CAUSAS_BASICAS_PERDIDAS_POR_ACCIDENTE, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_RELACION = 0;
                }
            }

            return ID_RELACION;
        }


        public Boolean DesactivarFactoresRelacionadosConAccidente(Decimal ID_ACCIDENTE,
            String IDS_CAUSAS,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_so_causas_basicas_perdidas_por_accidente_desactivar ";

            #region validaciones
            if (ID_ACCIDENTE != 0)
            {
                sql += ID_ACCIDENTE + ", ";
                informacion += "ID_ACCIDENTE = '" + ID_ACCIDENTE + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ACCIDENTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(IDS_CAUSAS) == false)
            {
                sql += "'" + IDS_CAUSAS + "', ";
                informacion += "IDS_CAUSAS = '" + IDS_CAUSAS + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo IDS_CAUSAS no puede ser vacio.";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SO_CAUSAS_BASICAS_PERDIDAS_POR_ACCIDENTE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarSeguimientoCasoSevero(Decimal ID_ACCIDENTE,
            String RIESGO_GENERADOR,
            String TIPO_ACCIDENTE,
            String TIPO_LESION,
            String PARTE_CUERPO,
            String AGENTE_LESION,
            String MECANISMO_LESION,
            String SITIO,
            String COND_INSEGURA,
            String ACTO_INSEGURO,
            String FACTORES_TRABAJO,
            String FACTORES_PERSONALES,
            Decimal ID_INCAPACIDAD,
            List<DetalleSeguimientoAccidente> listaDetallesSeguimiento,
            String NOMBRE_ACCIDENTE,
            Programa.Areas ID_AREA,
            List<FactorCausaAccidente> listaFactores)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarRegistroProgSeguimientoAccidentes(ID_ACCIDENTE, RIESGO_GENERADOR, TIPO_ACCIDENTE, TIPO_LESION, PARTE_CUERPO, AGENTE_LESION, MECANISMO_LESION, SITIO, COND_INSEGURA, ACTO_INSEGURO, FACTORES_TRABAJO, FACTORES_PERSONALES, ID_INCAPACIDAD, conexion) == false)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    Programa _prog = new Programa(Empresa, Usuario);
                    foreach (DetalleSeguimientoAccidente detalleAccidente in listaDetallesSeguimiento)
                    {
                        if (detalleAccidente.GENERA_COMPROMISO == true)
                        {
                            detalleAccidente.ID_COMPROMISO_GENERADO = _prog.AdicionarRegistroCompromisoEnActividad(ID_ACCIDENTE, NOMBRE_ACCIDENTE, Programa.TiposGeneraCompromiso.ACCIDENTE.ToString(), detalleAccidente.SEGUIMIENTO, detalleAccidente.ENCARGADO_COMPROMISO, detalleAccidente.FECHA_COMPROMISO, detalleAccidente.OBSERVACIONES, MaestraCompromiso.EstadosCompromiso.ABIERTO.ToString(), ID_AREA.ToString(), conexion);

                            if (detalleAccidente.ID_COMPROMISO_GENERADO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                MensajeError = _prog.MensajeError;
                                break;
                            }
                        }
                        else
                        {
                            detalleAccidente.ID_COMPROMISO_GENERADO = 0;
                        }

                        if (correcto == true)
                        {
                            Decimal ID_DETALLE_SEGUIMIENTO = AdicionarRegistroProgDetSegAccidente(ID_ACCIDENTE, detalleAccidente.FECHA_SEGUIMIENTO, detalleAccidente.SEGUIMIENTO, detalleAccidente.GENERA_COMPROMISO, detalleAccidente.ID_COMPROMISO_GENERADO, conexion);

                            if (ID_DETALLE_SEGUIMIENTO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_ACCIDENTE = 0;
                                break;
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        String IDS_CAUSAS = "";
                        foreach (FactorCausaAccidente factor in listaFactores)
                        {
                            Decimal ID_RELACION = AdicionarFactorAUnAccidente(ID_ACCIDENTE, factor.ID_CAUSA, factor.VALOR_ITEM, conexion);

                            if (String.IsNullOrEmpty(IDS_CAUSAS) == true)
                            {
                                IDS_CAUSAS = factor.ID_CAUSA.ToString();
                            }
                            else
                            {
                                IDS_CAUSAS += "," + factor.ID_CAUSA.ToString();
                            }

                            if (ID_RELACION <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_ACCIDENTE = 0;
                                break;
                            }
                        }

                        if (correcto == true)
                        {
                            if (DesactivarFactoresRelacionadosConAccidente(ID_ACCIDENTE, IDS_CAUSAS, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_ACCIDENTE = 0;
                            }
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
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public DataTable ObtenerDetalleSeguimientoAccidentePorIdAccidente(Decimal ID_ACCIDENTE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_det_seg_accidentes_obtener_por_id_accidente ";

            sql += ID_ACCIDENTE;

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

        public Boolean FinalizarRegistroProgSeguimientoAccidentes(Decimal ID_ACCIDENTE,
            String OBSERVACIONES_FINALIZACION,
            EstadosAccidente estado)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_seguimiento_accidentes_finalizar ";

            #region validaciones
            if (ID_ACCIDENTE != 0)
            {
                sql += ID_ACCIDENTE + ", ";
                informacion += "ID_ACCIDENTE = '" + ID_ACCIDENTE + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ACCIDENTE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES_FINALIZACION) == false)
            {
                sql += "'" + OBSERVACIONES_FINALIZACION + "', ";
                informacion += "OBSERVACIONES_FINALIZACION = '" + OBSERVACIONES_FINALIZACION + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBSERVACIONES_FINALIZACION = 'null', ";
            }

            sql += "'" + estado.ToString() + "', ";
            informacion += "ESTADO = '" + estado.ToString() + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_SEGUIMIENTO_ACCIDENTES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable ObtenerArchivoAdjuntoInvestigacionPorIdAccidente(Decimal ID_ACCIDENTE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_archivos_adjuntos_investigaciones_obtenerPorIdAccidente ";

            sql += ID_ACCIDENTE;

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


        public Decimal AdicionarAdjuntoAccidente(Decimal ID_ACCIDENTE,
            String TITULO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            Int32 ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            String ARCHIVO_ADJUNTO_TYPE)
        {
            Decimal ID_ADJUNTO = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_ADJUNTO = conexion.ExecuteEscalarParaAdicionarDocsAdjuntosAccidentes(ID_ACCIDENTE, TiposArchivo.ADJUNTO.ToString(), ARCHIVO_ADJUNTO, ARCHIVO_ADJUNTO_EXTENSION, ARCHIVO_ADJUNTO_TAMANO, ARCHIVO_ADJUNTO_TYPE, Usuario, TITULO, DESCRIPCION);

                if (ID_ADJUNTO <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_ADJUNTO = 0;
                }
                else
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                ID_ADJUNTO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_ADJUNTO;
        }



        public DataTable ObtenerArchivoAdjuntoAccidente(Decimal ID_ADJUNTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_archivos_adjuntos_investigaciones_obtenerPorId ";

            if (ID_ADJUNTO != 0)
            {
                sql += ID_ADJUNTO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_ADJUNTO no puede ser 0.";
            }

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


        #endregion metodos

    }
}
