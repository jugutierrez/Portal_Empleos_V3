using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class respuesta_cuestionario_multiple
    {
        [Key]
        public int id_pregunta { get; set; }

        public int id_tipo_pregunta { get; set; }

        public string id_respuesta { get; set; }

        public string respuesta_pregunta { get; set; }

    }
}