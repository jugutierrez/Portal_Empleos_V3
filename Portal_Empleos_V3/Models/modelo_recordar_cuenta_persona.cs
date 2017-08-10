using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_recordar_cuenta_persona
    {
        public string correo_electronico_persona { get; set; }

        public int identificacion_persona { get; set; }

        public string digito_identificacion_persona { get; set; }
    }
}