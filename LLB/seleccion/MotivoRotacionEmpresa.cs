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

namespace Brainsbits.LLB.seleccion
{
    public class MotivoRotacionEmpresa
    {
        #region variables
        private Decimal _id_rotacion_empresa = 0;
        private Decimal _id_detalle_rotacion = 0;
        private Boolean _activo = true;
        private Decimal _id_empresa = 0;

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        #endregion variables

        #region propiedades

        public Decimal ID_ROTACION_EMPRESA
        {
            get { return _id_rotacion_empresa; }
            set { _id_rotacion_empresa = value; }
        }

        public Decimal ID_DETALLE_ROTACION
        {
            get { return _id_detalle_rotacion; }
            set { _id_detalle_rotacion = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public Decimal ID_EMPRESA
        {
            get { return _id_empresa; }
            set { _id_empresa = value; }
        }






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

        #endregion propiedades

        #region constructores
        public MotivoRotacionEmpresa()
        {

        }
        public MotivoRotacionEmpresa(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        #endregion metodos

    }
}
