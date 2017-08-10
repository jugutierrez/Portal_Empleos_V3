using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class capacitaciones_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_capacitacion_curriculum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_inicio_capacitacion_curriculum { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_termino_capacitacion_curriculum { get; set; }

        public int horas_capacitacion { get; set; }

        public string  descripcion_capacitacion { get; set; }

        public int id_capacitacion { get; set; }
        public string nombre_capacitacion { get; set; }

        public int id_estado_capacitacion { get; set; }
        public string nombre_estado_capacitacion { get; set; }

        public int id_tipo_capacitacion { get; set; }
        public string nombre_tipo_capacitacion { get; set; }

        public int id_institucion { get; set; }
        public string nombre_institucion { get; set; }
    }
}