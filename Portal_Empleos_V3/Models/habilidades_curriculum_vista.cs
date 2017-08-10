using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class habilidades_curriculum_vista
    {

        [Key]
        [Column(Order = 1)]
        public int id_habilidad_curriculum { get; set; }

        public string nombre_habilidad { get; set; }

        public string nombre_grado_habilidad { get; set; }


    }
}