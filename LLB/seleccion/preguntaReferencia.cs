using System;

namespace Brainsbits.LLB.seleccion
{
    public class preguntaReferencia
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_PREGUNTA = 0;
        private Decimal _ID_CATEGORIA = 0;
        private String _CONTENIDO = null;
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

        public Decimal ID_PREGUNTA
        {
            get { return _ID_PREGUNTA; }
            set { _ID_PREGUNTA = value; }
        }

        public Decimal ID_CATEGORIA
        {
            get { return _ID_CATEGORIA; }
            set { _ID_CATEGORIA = value; }
        }

        public String CONTENIDO
        {
            get { return _CONTENIDO; }
            set { _CONTENIDO = value; }
        }
        #endregion propiedades

        #region constructores
        public preguntaReferencia()
        {
        }
        public preguntaReferencia(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos
    }
}