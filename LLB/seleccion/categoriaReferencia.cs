using System;

namespace Brainsbits.LLB.seleccion
{
    public class CategoriaReferencia
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_CATEGORIA = 0;
        private String _NOMBRE_CAT = null;
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


        public Decimal ID_CATEGORIA
        {
            get { return _ID_CATEGORIA; }
            set { _ID_CATEGORIA = value; }
        }
        public String NOMBRE_CAT
        {
            get { return _NOMBRE_CAT; }
            set { _NOMBRE_CAT = value; }
        }
        #endregion propiedades

        #region constructores
        public CategoriaReferencia()
        {
        }
        public CategoriaReferencia(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos
    }
}