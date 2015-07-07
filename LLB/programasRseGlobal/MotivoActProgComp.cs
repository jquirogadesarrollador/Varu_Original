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
    public class MotivoProgComp
    {
        #region variables

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private Decimal _id_motivo = 0;
        private String _motivo = null;
        private Boolean _activo = true;
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

        public Decimal ID_MOTIVO
        {
            get { return _id_motivo; }
            set { _id_motivo = value; }
        }

        public String MOTIVO
        {
            get { return _motivo; }
            set { _motivo = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }

        #endregion propiedades

        #region constructores
        public MotivoProgComp()
        {

        }
        public MotivoProgComp(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerMotivosActProgCompPorAreaYTipo(Programa.Areas area, String tipo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_motivos_cancelacion_reprogramacion_obtenerPorAreaYTipo ";

            sql += "'" + area.ToString() + "', '" + tipo.ToString() + "'";

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

        public Boolean ActualizarMotivo(Decimal id_motivo,
            String motivo,
            Boolean activo,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_prog_motivos_cancelacion_reprogrmacion_actualizar ";
            informacion = sql;

            #region validaciones
            if (id_motivo != 0)
            {
                sql += id_motivo + ", ";
                informacion += "ID_MOTIVO = '" + id_motivo + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MOTIVO no puede ser nulo\n";
                ejecutar = false;
            }

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

            if (activo == false)
            {
                sql += "'False', ";
                informacion += "ACTIVO = 'False', ";
            }
            else
            {
                sql += "'True', ";
                informacion += "ACTIVO = 'True', ";
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
                    _auditoria.Adicionar(Usuario, tabla.PROG_MOTIVOS_CANCELACION_REPROGRAMACION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarMotivos(Programa.Areas area,
            String tipo,
            List<MotivoProgComp> listaMotivos)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                foreach (MotivoProgComp motivo in listaMotivos)
                {
                    if (motivo.ID_MOTIVO == 0)
                    {
                        if (AdicionarMotivo(area, motivo.MOTIVO, tipo, motivo.ACTIVO, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                    else
                    {
                        if (ActualizarMotivo(motivo.ID_MOTIVO, motivo.MOTIVO, motivo.ACTIVO, conexion) == false)
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
