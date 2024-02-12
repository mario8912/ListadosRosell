using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Entidades.Modelos.Tablas
{
    public class Ruta : Tabla
    {
        private const string TABLA = "Ruta";

        private readonly string IdRuta = "idRuta";

        public string GetMinIdRuta()
        {
            return GetMinField(IdRuta, TABLA).RecordsAffected.ToString();
        }

        public string GetMaxIdRuta() 
        {
            return GetMinField(IdRuta, TABLA).RecordsAffected.ToString();
        }
    }
}
