using System;
using System.Collections.Generic;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class FormacionAcademica
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        private Decimal id_info_academica = 0;
        private Decimal _registro_entrevista = 0;
        private String _tipo_educacion = null;
        private String _nivel_academico = null;
        private String _institucion = null;
        private Int32 _anno = 0;
        private String _observaciones = null;
        private String _curso = null;
        private Decimal _duracion = 0;
        private String _unidad_duracion = null;
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


        public Decimal ID_INFO_ACADEMICA
        {
            get { return id_info_academica; }
            set { id_info_academica = value; }
        }

        public Decimal REGISTRO_ENTREVISTA
        {
            get { return _registro_entrevista; }
            set { _registro_entrevista = value; }
        }

        public String TIPO_EDUCACION
        {
            get { return _tipo_educacion; }
            set { _tipo_educacion = value; }
        }

        public String NIVEL_ACADEMICO
        {
            get { return _nivel_academico; }
            set { _nivel_academico = value; }
        }

        public String INSTITUCION
        {
            get { return _institucion; }
            set { _institucion = value; }
        }

        public Int32 ANNO
        {
            get { return _anno; }
            set { _anno = value; }
        }

        public String OBSERVACIONES
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        public String CURSO
        {
            get { return _curso; }
            set { _curso = value; }
        }

        public Decimal DURACION
        {
            get { return _duracion; }
            set { _duracion = value; }
        }

        public String UNIDAD_DURACION
        {
            get { return _unidad_duracion; }
            set { _unidad_duracion = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }
        #endregion propiedades

        #region constructores
        public FormacionAcademica()
        {

        }

        public FormacionAcademica(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region METODOS
        #endregion METODOS
    }
}
