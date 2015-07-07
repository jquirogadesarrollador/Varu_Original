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
    public class FactorCausaAccidente
    {
        #region variables

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _id_relacion = 0;
        private Decimal _id_accidente = 0;
        private Decimal _id_causa = 0;
        private Boolean _activo = true;
        private String _valor_item = null;
        private String _factor = null;
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

        public Decimal ID_RELACION
        {
            get { return _id_relacion; }
            set { _id_relacion = value; }
        }

        public Decimal ID_ACCIDENTE
        {
            get { return _id_accidente; }
            set { _id_accidente = value; }
        }

        public Decimal ID_CAUSA
        {
            get { return _id_causa; }
            set { _id_causa = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public String VALOR_ITEM
        {
            get { return _valor_item; }
            set { _valor_item = value; }
        }

        public String FACTOR
        {
            get { return _factor; }
            set { _factor = value; }
        }

        #endregion propiedades

        #region constructores
        public FactorCausaAccidente()
        {

        }
        public FactorCausaAccidente(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        #endregion metodos

    }
}
