using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class vista_respuesta_multiple_pregunta
    {
        [Key]
        [Column(Order = 1)]

        public int id_pregunta  { get; set; }

        public int id_respuesta { get; set; }

        public string nombre_respuesta { get; set; }
    }
}