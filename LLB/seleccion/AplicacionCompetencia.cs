using System;
using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.seleccion
{
    public class AplicacionCompetencia
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_APLICACION_COMPETENCIA = 0;
        private Decimal _ID_SOLICITUD = 0;
        private Decimal _ID_COMPETENCIA_ASSESMENT = 0;
        private Int32 _NIVEL_CALIFICACION = 0;
        private String _CALIFICACION = null;
        private String _OBSERVACIONES = null;
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




        public Decimal ID_APLICACION_COMPETENCIA
        {
            get { return _ID_APLICACION_COMPETENCIA; }
            set { _ID_APLICACION_COMPETENCIA = value; }
        }

        public Decimal ID_SOLICITUD
        {
            get { return _ID_SOLICITUD; }
            set { _ID_SOLICITUD = value; }
        }

        public Decimal ID_COMPETENCIA_ASSESMENT
        {
            get { return _ID_COMPETENCIA_ASSESMENT; }
            set { _ID_COMPETENCIA_ASSESMENT = value; }
        }

        public Int32 NIVEL_CALIFICACION
        {
            get { return _NIVEL_CALIFICACION; }
            set { _NIVEL_CALIFICACION = value; }
        }

        public String CALIFICACION
        {
            get { return _CALIFICACION; }
            set { _CALIFICACION = value; }
        }

        public String OBSERVACIONES
        {
            get { return _OBSERVACIONES; }
            set { _OBSERVACIONES = value; }
        }

        #endregion propiedades

        #region constructores
        public AplicacionCompetencia()
        {
        }
        public AplicacionCompetencia(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos
    }
}