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
    public class TipoActividad
    {
        #region variables

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private Decimal _id_tipo_actividad = 0;
        private String _nombre = null;
        private Boolean _activa = true;
        private String _secciones_habilitadas = null;
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

        public Decimal ID_TIPO_ACTIVIDAD
        {
            get { return _id_tipo_actividad; }
            set { _id_tipo_actividad = value; }
        }

        public String NOMBRE
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public Boolean ACTIVA
        {
            get { return _activa; }
            set { _activa = value; }
        }

        public String SECCIONES_HABILITADAS
        {
            get { return _secciones_habilitadas; }
            set { _secciones_habilitadas = value; }
        }

        #endregion propiedades

        #region constructores
        public TipoActividad()
        {

        }
        public TipoActividad(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerTiposActividadPorArea(Programa.Areas area)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_tipos_actividad_obtenerPorArea ";

            sql += "'" + area.ToString() + "'";

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

        public DataTable ObtenerTiposActividadPorAreayEstado(Programa.Areas area,
            Boolean activa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_tipos_actividad_obtenerPorAreaYActiva ";

            sql += "'" + area.ToString() + "', ";

            if (activa == true)
            {
                sql += "'True'";
            }
            else
            {
                sql += "'False'";
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



        public Decimal AdicionarMotivo(Programa.Areas idArea,
            String motivo,
            String tipo,
            Boolean activo,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_prog_motivos_cancelacion_reprogramacion_adicionar ";
            #region validaciones

            sql += "'" + idArea.ToString() + "', ";
            informacion += "ID_AREA = '" + idArea.ToString() + "', ";

            if (String.IsNullOrEmpty(motivo) == false)
            {
                sql += "'" + motivo + "', ";
                informacion += "MOTIVO = '" + motivo + "', ";
            }
            else
            {
                MensajeError += "El campo MOTIVO no puede ser nulo\n";
                ejecutar = false;
            }
            
            sql += "'" + tipo.ToString() + "', ";
            informacion += "TIPO = '" + tipo.ToString() + "', ";

            if (activo == true)
            {
                sql += "'True', ";
                informacion += "ACTIVO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ACTIVO = 'False', ";
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
                    _auditoria.Adicionar(Usuario, tabla.PROG_MOTIVOS_CANCELACION_REPROGRAMACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Decimal AdicionarTipoActividad(Programa.Areas idArea,
            String nombre,
            Boolean activa,
            String secicones_habilitadas,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_prog_tipos_actividad_adicionar ";
            #region validaciones
            sql += "'" + idArea.ToString() + "', ";
            informacion += "ID_AREA = '" + idArea.ToString() + "', ";

            if (String.IsNullOrEmpty(nombre) == false)
            {
                sql += "'" + nombre + "', ";
                informacion += "NOMBRE = '" + nombre + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (activa == true)
            {
                sql += "'True', ";
                informacion += "ACTIVA = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ACTIVA = 'False', ";
            }

            if (String.IsNullOrEmpty(secicones_habilitadas) == false)
            {
                sql += "'" + secicones_habilitadas + "', ";
                informacion += "SECCIONES_HABILITADAS = '" + secicones_habilitadas + "', ";
            }
            else
            {
                MensajeError += "El campo SECCIONES_HABILITADAS no puede ser nulo\n";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.PROG_TIPOS_ACTIVIDAD, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Boolean ActualizarTipoActividad(Decimal id_tipo_actividad,
           String nombre,
           Boolean activa,
           String secciones_habilitadas,
           Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_prog_tipos_actividad_actualizar ";
            informacion = sql;

            #region validaciones
            if (id_tipo_actividad != 0)
            {
                sql += id_tipo_actividad + ", ";
                informacion += "ID_TIPO_ACTIVIDAD = '" + id_tipo_actividad + "', ";
            }
            else
            {
                MensajeError += "El campo ID_TIPO_ACTIVIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(nombre) == false)
            {
                sql += "'" + nombre + "', ";
                informacion += "NOMBRE = '" + nombre + "', ";
            }
            else
            {
                MensajeError += "El campo nombre no puede ser nulo\n";
                ejecutar = false;
            }

            if (activa == false)
            {
                sql += "'False', ";
                informacion += "ACTIVA = 'False', ";
            }
            else
            {
                sql += "'True', ";
                informacion += "ACTIVA = 'True', ";
            }

            if (String.IsNullOrEmpty(secciones_habilitadas) == false)
            {
                sql += "'" + secciones_habilitadas + "', ";
                informacion += "SECCIONES_HABILITADAS = '" + secciones_habilitadas + "', ";
            }
            else
            {
                MensajeError += "El campo nombre no puede ser nulo\n";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.PROG_TIPOS_ACTIVIDAD, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarTipos(Programa.Areas area,
            List<TipoActividad> listaTipos)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                foreach (TipoActividad tipo in listaTipos)
                {
                    if (tipo.ID_TIPO_ACTIVIDAD == 0)
                    {
                        if (AdicionarTipoActividad(area, tipo.NOMBRE, tipo.ACTIVA, tipo.SECCIONES_HABILITADAS, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                    else
                    {
                        if (ActualizarTipoActividad(tipo.ID_TIPO_ACTIVIDAD, tipo.NOMBRE, tipo.ACTIVA, tipo.SECCIONES_HABILITADAS, conexion) == false)
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


        #endregion metodos

    }
}
