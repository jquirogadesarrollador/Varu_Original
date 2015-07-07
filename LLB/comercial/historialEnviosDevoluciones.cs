using System;
using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.comercial
{
    public class HistorialEnvioDevolucion
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_ACCION = 0;
        private Decimal _ID_CONTRATO = 0;
        private DateTime _FECHA_ACCION = new DateTime();
        private String _TIPO_ACCION = null;
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

        public Decimal ID_ACCION
        {
            get { return _ID_ACCION; }
            set { _ID_ACCION = value; }
        }

        public Decimal ID_CONTRATO
        {
            get { return _ID_CONTRATO; }
            set { _ID_CONTRATO = value; }
        }

        public DateTime FECHA_ACCION
        {
            get { return _FECHA_ACCION; }
            set { _FECHA_ACCION = value; }
        }

        public String TIPO_ACCION
        {
            get { return _TIPO_ACCION; }
            set { _TIPO_ACCION = value; }
        }

        public String OBSERVACIONES
        {
            get { return _OBSERVACIONES; }
            set { _OBSERVACIONES = value; }
        }

        #endregion propiedades

        #region constructores
        public HistorialEnvioDevolucion()
        {
        }
        public HistorialEnvioDevolucion(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos
    }
}