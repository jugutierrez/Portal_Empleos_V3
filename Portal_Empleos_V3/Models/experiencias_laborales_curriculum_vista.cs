using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class experiencias_laborales_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_experiencia_laboral_curriculum { get; set; }

        public string nombre_experiencia_laboral { get; set; }

        public string empresa_experiencia_laboral { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_inicio_experiencia_laboral { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_termino_experiencia_laboral { get; set; }

        public string detalle_experiencia_laboral { get; set; }

        public int id_area_experiencia_laboral { get; set; }
        public string nombre_area_experiencia_laboral { get; set; }

        public int id_cargo_experiencia_laboral { get; set; }
        public string nombre_cargo_experiencia_laboral { get; set; }

        public long id_referencia_laboral { get; set; }
        public referencia_laboral referencia_laboral { get; set; }



    }
}