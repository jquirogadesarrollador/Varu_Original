using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.seleccion
{
    public class listaPruebasAplicados
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_SOLICITUD;
        private Decimal _REGISTRO;
        private Decimal _ID_PRUEBA;
        private Decimal _ID_CATEGORIA;
        private String _NOM_PRUEBA;
        private DateTime _FECHA_R;
        private String _RESULTADOS;
        private byte[] _ARCHIVO_PRUEBA = null;
        private Int32 _ARCHIVO_TAMANO = 0;
        private String _ARCHIVO_EXTENSION = null;
        private String _ARCHIVO_TYPE = null;
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

        public Decimal ID_SOLICITUD
        {
            get { return _ID_SOLICITUD; }
            set { _ID_SOLICITUD = value; }
        }

        public Decimal REGISTRO
        {
            get { return _REGISTRO; }
            set { _REGISTRO = value; }
        }

        public Decimal ID_PRUEBA
        {
            get { return _ID_PRUEBA; }
            set { _ID_PRUEBA = value; }
        }

        public Decimal ID_CATEGORIA
        {
            get { return _ID_CATEGORIA; }
            set { _ID_CATEGORIA = value; }
        }

        public String NOM_PRUEBA
        {
            get { return _NOM_PRUEBA; }
            set { _NOM_PRUEBA = value; }
        }

        public DateTime FECHA_R
        {
            get { return _FECHA_R; }
            set { _FECHA_R = value; }
        }

        public String RESULTADOS
        {
            get { return _RESULTADOS; }
            set { _RESULTADOS = value; }
        }

        public byte[] ARCHIVO_PRUEBA
        {
            get { return _ARCHIVO_PRUEBA; }
            set { _ARCHIVO_PRUEBA = value; }
        }

        public Int32 ARCHIVO_TAMANO
        {
            get { return _ARCHIVO_TAMANO; }
            set { _ARCHIVO_TAMANO = value; }
        }

        public String ARCHIVO_EXTENSION
        {
            get { return _ARCHIVO_EXTENSION; }
            set { _ARCHIVO_EXTENSION = value; }
        }

        public String ARCHIVO_TYPE
        {
            get { return _ARCHIVO_TYPE; }
            set { _ARCHIVO_TYPE = value; }
        }
        #endregion propiedades

        #region constructores
        public listaPruebasAplicados()
        {
        }
        public listaPruebasAplicados(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #endregion metodos
    }
}
