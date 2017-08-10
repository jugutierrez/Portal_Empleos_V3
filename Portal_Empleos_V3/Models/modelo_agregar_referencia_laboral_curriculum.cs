using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_agregar_referencia_laboral_curriculum
    {
        [Key]
        [Column(Order = 1)]
        public int id_experiencia_laboral_curriculum { get; set; }

        public string nombre_referencia_laboral { get; set; }

        public string cargo_referencia_laboral { get; set; }

        public int contacto_referencia_laboral { get; set; }

        public string correo_referencia_laboral { get; set; }
    }
}