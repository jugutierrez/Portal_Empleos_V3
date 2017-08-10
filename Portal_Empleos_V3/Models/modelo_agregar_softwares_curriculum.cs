using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_agregar_softwares_curriculum
    {
        [Required]
        public string id_software { get; set; }

        public string id_grado_conocimiento_software { get; set; }
    }
}