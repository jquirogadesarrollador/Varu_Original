using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class Reclutamiento
    {
        #region Variables
        String _Accion = null;
        String _Id_solicitud = null;
        String _Id_Requerimiento = null;
        String _Id_usuario_Asignado = null;
        String _Cantidad = null;
        String _Fecha_Requerida = null;
        String _Fecha_Ingreso_Solicitud = null;
        String _Usuario_Crea_Solicitud = null;
        String _Descripcion = null;
        String _empresa = null;
        String _usuario = null;
        String _Cargo = null;
        String _Tipo_Requerimietno = null;
        String _MensajeError = null;
        String _Regional = null;
        String _Ciudad = null;
        String _Id_Empresa = null;
        String sql = null;
        String _Id_Usuario_Reclutador = null;





        #endregion

        #region Propiedades


        public String Ciudad
        {
            get { return _Ciudad; }
            set { _Ciudad = value; }
        }

        public String Regional
        {
            get { return _Regional; }
            set { _Regional = value; }
        }
        public String Id_solicitud
        {
            get { return _Id_solicitud; }
            set { _Id_solicitud = value; }
        }

        public String Accion
        {
            get { return _Accion; }
            set { _Accion = value; }
        }
        public String Id_Empresa
        {
            get { return _Id_Empresa; }
            set { _Id_Empresa = value; }
        }
        public String Id_Requerimiento
        {
            get { return _Id_Requerimiento; }
            set { _Id_Requerimiento = value; }
        }
        public String Id_usuario_Asignado
        {
            get { return _Id_usuario_Asignado; }
            set { _Id_usuario_Asignado = value; }
        }

        public String Cargo
        {
            get { return _Cargo; }
            set { _Cargo = value; }
        }

        public String Tipo_Requerimietno
        {
            get { return _Tipo_Requerimietno; }
            set { _Tipo_Requerimietno = value; }
        }

        public String Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }
        public String Fecha_Requerida
        {
            get { return _Fecha_Requerida; }
            set { _Fecha_Requerida = value; }
        }
        public String Fecha_Ingreso_Solicitud
        {
            get { return _Fecha_Ingreso_Solicitud; }
            set { _Fecha_Ingreso_Solicitud = value; }
        }
        public String Usuario_Crea_Solicitud
        {
            get { return _Usuario_Crea_Solicitud; }
            set { _Usuario_Crea_Solicitud = value; }
        }
        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
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
            get { return _MensajeError; }
            set { _MensajeError = value; }
        }
        public String Id_Usuario_Reclutador
        {
            get { return _Id_Usuario_Reclutador; }
            set { _Id_Usuario_Reclutador = value; }
        }

        #endregion

        #region Constructor
        public Reclutamiento(String Empresa, String Usuario)
        {
            Empresa = _empresa;
            Usuario = _usuario;
        }
        #endregion

        #region Metodos

        public Decimal SolicitudAccion()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_RECLUTAMIENTO_ACCION ";
            sql += " '" + Accion + "', " + " '" + Id_solicitud + "' " + " , " + " '" + Id_Requerimiento + "', '" + Id_usuario_Asignado + "', '" + Cantidad + "', '" + Fecha_Requerida + "', " + " '" + Fecha_Ingreso_Solicitud + "', '" + Usuario_Crea_Solicitud + "', '" + Descripcion + "', '" + Id_Empresa + "'" + ", '" + Regional + "', " + " '" + Ciudad + "', " + " '" + Cargo + "', " + " '" + Tipo_Requerimietno + "', " + " '" + Id_usuario_Asignado + "' ";
            informacion += "ID_EMPRESA = " + Empresa.ToString() + ", ";

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }
            return REGISTRO_CONTRATO;
        }
        public Decimal SolicitudAccionListarContactos(String @Empresa, String @ACCION, String @Id_Solicitud_requerimiento,
        String @Apellido,
        String @Nombre,
        String @Documento,
        String @Telefono,
        String @Cargo,
        String @Fecha_De_Contacto
            )
        {
            Conexion conexion = new Conexion(@Empresa);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_RECLUTAMIENTO_ACCION_LISTAR_CONTACTOS ";
            sql += " '" + @ACCION + "', " + " '" + @Id_Solicitud_requerimiento + "' " + " , " + " '" + @Apellido + "', '" + @Nombre + "', '" + @Documento + "', '" + @Telefono + "', " + " '" + @Cargo + "', '" + @Fecha_De_Contacto + "' ";
            informacion += "ID_EMPRESA = " + Empresa.ToString() + ", ";

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }
            return REGISTRO_CONTRATO;
        }
        public DataTable CargarGridReclutador(string _empresa_Filtro, String Secion_Usuario)
        {
            {
                Conexion conexion = new Conexion(_empresa_Filtro);
                DataSet _dataSet = new DataSet();
                DataView _dataView = new DataView();
                Boolean ejecutar = true;
                DataTable _Campos = new DataTable();
                tools _tools = new tools();

                sql = "Reclutamiento_Campos_Por_Reclutador";
                sql += " '" + Secion_Usuario + "' ";

                if (ejecutar)
                {
                    try
                    {
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _Campos = _dataView.Table;

                    }
                    catch (Exception e)
                    {

                    }
                }
                return _Campos;
            }
        }
        public DataTable ValidarHV(string _empresa_Filtro, String @Cedula)
        {
            {
                Conexion conexion = new Conexion(_empresa_Filtro);
                DataSet _dataSet = new DataSet();
                DataView _dataView = new DataView();
                Boolean ejecutar = true;
                DataTable _Campos = new DataTable();
                tools _tools = new tools();

                sql = "Confirmacion_Hoja_De_Vida";
                sql += " '" + @Cedula + "' ";

                if (ejecutar)
                {
                    try
                    {
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _Campos = _dataView.Table;

                    }
                    catch (Exception e)
                    {

                    }
                }
                return _Campos;
            }
        }
        public DataTable CargarGridSeguimientoRecepciom(string _empresa_Filtro, String @Documento)
        {
            {
                Conexion conexion = new Conexion(_empresa_Filtro);
                DataSet _dataSet = new DataSet();
                DataView _dataView = new DataView();
                Boolean ejecutar = true;
                DataTable _Campos = new DataTable();
                tools _tools = new tools();

                sql = "Reclutamiento_Seguimiento_Recepcion";
                sql += " '" + @Documento + "' ";

                if (ejecutar)
                {
                    try
                    {
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _Campos = _dataView.Table;

                    }
                    catch (Exception e)
                    {

                    }
                }
                return _Campos;
            }
        }
        public DataTable CargarGrid_Ista_Contactos(string _empresa_Filtro, String @Id_Solicitud_requerimiento)
        {
            {
                Conexion conexion = new Conexion(_empresa_Filtro);
                DataSet _dataSet = new DataSet();
                DataView _dataView = new DataView();
                Boolean ejecutar = true;
                DataTable _Campos = new DataTable();
                tools _tools = new tools();

                sql = "USP_RECLUTAMIENTO_ACCION_LISTAR_CONTACTOS_GrdView";
                sql += " '" + @Id_Solicitud_requerimiento + "' ";

                if (ejecutar)
                {
                    try
                    {
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _Campos = _dataView.Table;

                    }
                    catch (Exception e)
                    {

                    }
                }
                return _Campos;
            }
        }
        public DataTable GridAgendarContactos(string _empresa_Filtro, String @Psicologo, String @FechaAgenda)
        {
            {
                Conexion conexion = new Conexion(_empresa_Filtro);
                DataSet _dataSet = new DataSet();
                DataView _dataView = new DataView();
                Boolean ejecutar = true;
                DataTable _Campos = new DataTable();
                tools _tools = new tools();

                sql = "Reclutamiento_Agenda_Por_psicologo";
                sql += " '" + @Psicologo + "' , '" + @FechaAgenda + "' ";

                if (ejecutar)
                {
                    try
                    {
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _Campos = _dataView.Table;

                    }
                    catch (Exception e)
                    {

                    }
                }
                return _Campos;
            }
        }
        public DataTable FiltroPorRequerimiento(String _empresa_Filtro, String Id_RequerimientoFiltro)
        {
            Conexion conexion = new Conexion(_empresa_Filtro);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            Boolean ejecutar = true;
            DataTable _Campos = new DataTable();
            tools _tools = new tools();

            sql = "Reclutamiento_Campos_Por_Requerimiento";
            sql += " '" + Id_RequerimientoFiltro + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _Campos = _dataView.Table;

                }
                catch (Exception e)
                {

                }
            }
            return _Campos;
        }
        public DataTable Trazabilidad(String _empresa_Filtro, String @ID_REQUERIMIENTO)
        {
            Conexion conexion = new Conexion(_empresa_Filtro);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            Boolean ejecutar = true;
            DataTable _Campos = new DataTable();
            tools _tools = new tools();

            sql = "REQUERIMIENTO_TRAZABILIDAD";
            sql += " '" + @ID_REQUERIMIENTO + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _Campos = _dataView.Table;

                }
                catch (Exception e)
                {

                }
            }
            return _Campos;
        }
        public DataTable FiltroPorRequerimientoResumen(String _empresa_Filtro, String Id_Solicitud_Requerimiento)
        {
            Conexion conexion = new Conexion(_empresa_Filtro);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            Boolean ejecutar = true;
            DataTable _Campos = new DataTable();
            tools _tools = new tools();

            sql = "Reclutamiento_Campos_Resumen";
            sql += " '" + Id_Solicitud_Requerimiento + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _Campos = _dataView.Table;

                }
                catch (Exception e)
                {

                }
            }
            return _Campos;
        }
        public Decimal AgendarContactoPorFecha(
            String @Empresa,
            String @Id_Registro,
            String @Fecha_De_Cita,
            String @Psicologo,
            String @Usuario_Crea_Registro,
            String @Hora,
            String @AceptaOferta
            )
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "AGENDAR_CONTACTO_RECLUTAMIENTO ";
            sql += " '" + @Id_Registro + "', " + " '" + @Fecha_De_Cita + "' " + " , " + " '" + @Psicologo + "', '" + @Usuario_Crea_Registro + "', '" + @Hora + "', '" + @AceptaOferta + "' ";
            informacion += "ID_EMPRESA = " + Empresa.ToString() + ", ";

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }
            return REGISTRO_CONTRATO;
        }
        public Decimal SeguimientoContacto(String @ID_EMPRESA, String @Id_Registro)
        {
            Conexion conexion = new Conexion(@ID_EMPRESA);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;

            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "Reclutamiento_Seguimiento_Recepcion_ASISTENCIA ";
            sql += " '" + @Id_Registro + "' ";

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }
            return REGISTRO_CONTRATO;
        }
        public Decimal SolicitudAccionSeguimientoContactos(String @Empresa, String @ACCION, String @Asiste_Cita,
       String @Usuario,
       String @Acepta_Oferta
           )
        {
            Conexion conexion = new Conexion(@Empresa);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "Reclutamiento_Seguimiento_Asiste ";
            sql += " '" + @ACCION + "', " + " '" + @Asiste_Cita + "' " + " , " + " '" + @Usuario + "', '" + @Acepta_Oferta + "' ";
            informacion += "ID_EMPRESA = " + Empresa.ToString() + ", ";

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }
            return REGISTRO_CONTRATO;
        }
        public DataTable ObtenerComRequerimientoPorUsuLog(String @Empresa, String USU_LOG)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_usu_log_Provisionamiento";

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
                Conexion conexion = new Conexion(@Empresa);
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
        public DataTable ObtenerComRequerimientoPorRequerimiento(String @Empresa, String USU_LOG)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_requerimientos_obtener_por_usu_log_Reclutamiento";

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
                Conexion conexion = new Conexion(@Empresa);
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
        public DataTable ConsultaCandidatoFiltro(String _empresa_Filtro, String @DOCUMENTO)
        {
            Conexion conexion = new Conexion(_empresa_Filtro);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            Boolean ejecutar = true;
            DataTable _Campos = new DataTable();
            tools _tools = new tools();

            sql = "USP_RECLUTAMIENTO_CONSULTA_RECLUTAMIENTO_POR_dOCUMENTO";
            sql += " '" + @DOCUMENTO + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _Campos = _dataView.Table;

                }
                catch (Exception e)
                {

                }
            }
            return _Campos;
        }
        public DataTable CargarAgendaDelPsicologo(String _empresa_Filtro)
        {
            {
                Conexion conexion = new Conexion(_empresa_Filtro);
                DataSet _dataSet = new DataSet();
                DataView _dataView = new DataView();
                Boolean ejecutar = true;
                DataTable _Campos = new DataTable();
                tools _tools = new tools();

                sql = "ESTRUCTURA_AGENDA_PSICOLOGO";

                if (ejecutar)
                {
                    try
                    {
                        _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                        _dataView = _dataSet.Tables[0].DefaultView;
                        _Campos = _dataView.Table;

                    }
                    catch (Exception e)
                    {

                    }
                }
                return _Campos;
            }
        }
        public DataTable AgendaDeContactosEspecial(String _empresa_Filtro, String @Hora, String @Psicologo, String @Fecha)
        {
            Conexion conexion = new Conexion(_empresa_Filtro);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            Boolean ejecutar = true;
            DataTable _Campos = new DataTable();
            tools _tools = new tools();

            sql = "AGENDAR_CONTACTO_RECLUTAMIENTO_AgendaPsicologo ";
            sql += " '" + @Hora + "'" + "," + " '" + @Psicologo + "'" + "," + " '" + @Fecha + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _Campos = _dataView.Table;

                }
                catch (Exception e)
                {

                }
            }
            return _Campos;
        }
        #endregion
    }

}
