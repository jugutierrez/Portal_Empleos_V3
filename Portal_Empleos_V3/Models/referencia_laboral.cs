using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("referencia_laboral")]
    public class referencia_laboral
    {
        [Key]
        [Column(Order = 1)]
        public long id_referencia_laboral { get; set; }

        public string nombre_referencia_laboral { get; set; }

        public string cargo_referencia_laboral { get; set; }

        public int contacto_referencia_laboral { get; set; }

        public string correo_referencia_laboral { get; set; }
    }
}