using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_datos_soporte
    {
        public int id_tipo_correo { get; set; }

        public string identificacion_persona { get; set; }
    }
}