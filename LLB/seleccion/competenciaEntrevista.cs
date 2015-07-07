using System;

namespace Brainsbits.LLB.seleccion
{
    public class competenciaEntrevista
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_COMPETENCIA = 0;
        private String _COMPETENCIA = null;
        private String _DEFINICION = null;
        private String _AREA = null;
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


        public Decimal ID_COMPETENCIA
        {
            get { return _ID_COMPETENCIA; }
            set { _ID_COMPETENCIA = value; }
        }

        public String COMPETENCIA
        {
            get { return _COMPETENCIA; }
            set { _COMPETENCIA = value; }
        }

        public String DEFINICION
        {
            get { return _DEFINICION; }
            set { _DEFINICION = value; }
        }

        public String AREA
        {
            get { return _AREA; }
            set { _AREA = value; }
        }
        #endregion propiedades

        #region constructores
        public competenciaEntrevista()
        {
        }
        public competenciaEntrevista(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos
    }
}