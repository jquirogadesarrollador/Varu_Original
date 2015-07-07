using System;

namespace Brainsbits.LLB.seleccion
{
    public class respuestaReferencia
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_RESPUESTA = 0;
        private Decimal _ID_PREGUNTA = 0;
        private Decimal _ID_REFERENCIA = 0;
        private String _RESPUESTA = null;

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


        public Decimal ID_RESPUESTA
        {
            get { return _ID_RESPUESTA; }
            set { _ID_RESPUESTA = value; }
        }

        public Decimal ID_PREGUNTA
        {
            get { return _ID_PREGUNTA; }
            set { _ID_PREGUNTA = value; }
        }

        public Decimal ID_REFERENCIA
        {
            get { return _ID_REFERENCIA; }
            set { _ID_REFERENCIA = value; }
        }

        public String RESPUESTA
        {
            get { return _RESPUESTA; }
            set { _RESPUESTA = value; }
        }

        #endregion propiedades

        #region constructores
        public respuestaReferencia()
        {
        }
        public respuestaReferencia(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos
    }
}