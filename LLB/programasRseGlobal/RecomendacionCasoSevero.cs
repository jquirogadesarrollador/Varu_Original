using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.programasRseGlobal
{
    public class RecomendacionCasoSevero
    {
        #region variables
        private Decimal _id_recomendacion = 0;
        private Decimal _id_maestra_caso_severo = 0;
        private DateTime _fch_recom_desde = new DateTime();
        private DateTime _fch_recom_hasta = new DateTime();
        private String _tipo_entidad_emite = null;
        private Decimal _id_entidad_emite = 0;
        private String _recomendacion = null;

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

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

        public Decimal ID_RECOMENDACION
        {
            get { return _id_recomendacion; }
            set { _id_recomendacion = value; }
        }

        public Decimal ID_MAESTRA_CASO_SEVERO
        {
            get { return _id_maestra_caso_severo; }
            set { _id_maestra_caso_severo = value; }
        }

        public DateTime FCH_RECOM_DESDE
        {
            get { return _fch_recom_desde; }
            set { _fch_recom_desde = value; }
        }

        public DateTime FCH_RECOM_HASTA
        {
            get { return _fch_recom_hasta; }
            set { _fch_recom_hasta = value; }
        }

        public String TIPO_ENTIDAD_EMITE
        {
            get { return _tipo_entidad_emite; }
            set { _tipo_entidad_emite = value; }
        }

        public Decimal ID_ENTIDAD_EMITE
        {
            get { return _id_entidad_emite; }
            set { _id_entidad_emite = value; }
        }

        public String RECOMENDACION
        {
            get { return _recomendacion; }
            set { _recomendacion = value; }
        }
        #endregion propiedades

        #region constructores
        public RecomendacionCasoSevero()
        {

        }
        public RecomendacionCasoSevero(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos

    }
}
