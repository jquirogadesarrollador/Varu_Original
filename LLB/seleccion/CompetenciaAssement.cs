using System;
using System.Collections.Generic;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class CompetenciaAssesment
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        private Decimal _id_competencia_assesment = 0;
        private Decimal _id_assesment = 0;
        private Decimal _id_competencia = 0;
        private Boolean _activo = true;

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

        public Decimal ID_COMPETENCIA_ASSESMENT
        {
            get { return _id_competencia_assesment; }
            set { _id_competencia_assesment = value; }
        }

        public Decimal ID_ASSESMENT
        {
            get { return _id_assesment; }
            set { _id_assesment = value; }
        }

        public Decimal ID_COMPETENCIA
        {
            get { return _id_competencia; }
            set { _id_competencia = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }
        #endregion propiedades

        #region constructores
        public CompetenciaAssesment()
        {

        }

        public CompetenciaAssesment(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region METODOS
        #endregion METODOS
    }
}
