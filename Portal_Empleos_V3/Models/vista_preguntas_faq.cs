using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("preguntas_faq")]
    public class vista_preguntas_faq
    {
        [Key]
        [Column(Order = 1)]
        public long id_pregunta { get; set; }

        public long id_categoria { get; set; }

        public string nombre_pregunta { get; set; }

        public string respuesta_pregunta { get; set; }
    }
}