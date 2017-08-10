using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("cargo_experiencias_laborales")]
    public class cargo_experiencias_laborales
    {
        [Key]
        [Column(Order = 1)]
        public int id_cargo_experiencia_laboral { get; set; }

        public string nombre_cargo_experiencia_laboral { get; set; }
    }
}