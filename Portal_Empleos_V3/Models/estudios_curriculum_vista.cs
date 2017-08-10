using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class estudios_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_estudio_curriculum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_inicio_estudio_curriculum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ano_termino_estudio_curriculum { get; set; }

        public int id_estudio { get; set; }
        public string nombre_estudio { get; set; }

        public int id_tipo_estudio { get; set; }
        public string nombre_tipo_estudio { get; set; }

        public int id_estado_estudio { get; set; }
        public string nombre_estado_estudio { get; set; }

        public int id_institucion { get; set; }
        public string nombre_institucion { get; set; }
    }
}