using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Security.Cryptography;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seguridad
{
    public class UsuarioEmpresaCiudad
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        String _nombre_usuario = null;

        private Decimal _id_empresa = 0;
        private String _id_ciudad = null;

        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String NombreUsuario
        {
            get { return _nombre_usuario; }
            set { _nombre_usuario = value; }
        }

        public Decimal ID_EMPRESA
        {
            get { return _id_empresa; }
            set { _id_empresa = value; }
        }

        public String ID_CIUDAD
        {
            get { return _id_ciudad; }
            set { _id_ciudad = value; }
        }
        #endregion propiedades

        #region constructores
        public UsuarioEmpresaCiudad(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        public UsuarioEmpresaCiudad()
        {

        }
        #endregion constructores
    }
}
