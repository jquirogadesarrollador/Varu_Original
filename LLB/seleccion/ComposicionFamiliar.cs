using System;
using System.Collections.Generic;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class ComposicionFamiliar
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        private Decimal _id_composicion = 0;
        private Decimal _registro_entrevista = 0;
        private String _id_tipo_familiar = null;
        private String _nombres = null;
        private String _apellidos = null;
        private DateTime _fecha_nacimiento = new DateTime();
        private String _profesion = null;
        private String _id_ciudad = null;
        private Boolean _vive_con_el = false;
        private Boolean _activo = false;
        #endregion varialbes

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }


        public Decimal ID_COMPOSICION
        {
            get { return _id_composicion; }
            set { _id_composicion = value; }
        }

        public Decimal REGISTRO_ENTREVISTA
        {
            get { return _registro_entrevista; }
            set { _registro_entrevista = value; }
        }

        public String ID_TIPO_FAMILIAR
        {
            get { return _id_tipo_familiar; }
            set { _id_tipo_familiar = value; }
        }

        public String NOMBRES
        {
            get { return _nombres; }
            set { _nombres = value; }
        }

        public String APELLIDOS
        {
            get { return _apellidos; }
            set { _apellidos = value; }
        }

        public DateTime FECHA_NACIMIENTO
        {
            get { return _fecha_nacimiento; }
            set { _fecha_nacimiento = value; }
        }

        public String PROFESION
        {
            get { return _profesion; }
            set { _profesion = value; }
        }

        public String ID_CIUDAD
        {
            get { return _id_ciudad; }
            set { _id_ciudad = value; }
        }

        public Boolean VIVE_CON_EL
        {
            get { return _vive_con_el; }
            set { _vive_con_el = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }
        #endregion propiedades

        #region constructores
        public ComposicionFamiliar()
        {

        }

        public ComposicionFamiliar(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region METODOS
        #endregion METODOS
    }
}
