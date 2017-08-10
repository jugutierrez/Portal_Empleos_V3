using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class idiomas_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]

        public int id_idioma_curriculum { get; set; }

        public string nombre_idioma { get; set; }

        public string nombre_nivel_idioma { get; set; }
    }
}