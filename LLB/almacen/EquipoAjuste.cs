using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class EquipoAjuste
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        Decimal _id_equipo = 0;
        Decimal _id_producto = 0;
        Decimal _id_documento = 0;
        String _marca = null;
        String _modelo = null;
        String _serie = null;
        String _imei = null;
        Decimal _nummero_celular = 0;
        Decimal _id_lote = 0;

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

        public Decimal ID_EQUIPO
        {
            get { return _id_equipo; }
            set { _id_equipo = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }

        public Decimal ID_DOCUMENTO
        {
            get { return _id_documento; }
            set { _id_documento = value; }
        }

        public String MARCA
        {
            get { return _marca; }
            set { _marca = value; }
        }
        
        public string MODELO
        {
            get { return _modelo; }
            set { _modelo = value; }
        }
        
        public string SERIE
        {
            get { return _serie; }
            set { _serie = value; }
        }
        public string IMEI
        {
            get { return _imei; }
            set { _imei = value; }
        }
        public Decimal NUMERO_CELULAR
        {
            get { return _nummero_celular; }
            set { _nummero_celular = value; }
        }

        public Decimal ID_LOTE
        {
            get { return _id_lote; }
            set { _id_lote = value; }
        }
        #endregion propiedades

        #region constructores
        public EquipoAjuste(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public EquipoAjuste()
        {
        }
        #endregion constructores

        #region metodos
        #endregion metodos
    }
}
