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
    public class MaestraCompromiso
    {
        #region variables
        private Decimal _id_maestra_compromiso = 0;
        private Decimal _id_actividad_genera = 0;
        private String _nombre_actividad_genera = null;
        private String _tipo_genera = null;
        private String _compromiso = null;
        private String _usu_log_responsable = null;
        private DateTime _fecha_p;
        private DateTime _fecha_ejecucion;
        private String _observacicones = null;
        private String _estado = null;

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        public enum EstadosCompromiso
        {
            ABIERTO = 0,
            CERRADO
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

        public Decimal ID_MAESTRA_COMPROMISO
        {
            get { return _id_maestra_compromiso; }
            set { _id_maestra_compromiso = value; }
        }

        public Decimal ID_ACTIVIDAD_GENERA
        {
            get { return _id_actividad_genera; }
            set { _id_actividad_genera = value; }
        }

        public String NOMBRE_ACTIVIDAD_GENERA
        {
            get { return _nombre_actividad_genera; }
            set { _nombre_actividad_genera = value; }
        }

        public String TIPO_GENERA
        {
            get { return _tipo_genera; }
            set { _tipo_genera = value; }
        }

        public String COMPROMISO
        {
            get { return _compromiso; }
            set { _compromiso = value; }
        }

        public String USU_LOG_RESPONSABLE
        {
            get { return _usu_log_responsable; }
            set { _usu_log_responsable = value; }
        }

        public DateTime FECHA_P
        {
            get { return _fecha_p; }
            set { _fecha_p = value; }
        }

        public DateTime FECHA_EJECUCION
        {
            get { return _fecha_ejecucion; }
            set { _fecha_ejecucion = value; }
        }

        public String OBSERVACIONES
        {
            get { return _observacicones; }
            set { _observacicones = value; }
        }

        public String ESTADO
        {
            get { return _estado; }
            set { _estado = value; }
        }
        #endregion propiedades

        #region constructores
        public MaestraCompromiso()
        {

        }
        public MaestraCompromiso(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos

    }
}
