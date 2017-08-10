using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("curriculums")]
    public class curriculums
    {
        [Key]
        [Column(Order = 1)]
        public int id_curriculum { get; set; }

        public string direccion_curriculum { get; set; }

        public string telefono_curriculum_1 { get; set; }

        public string telefono_curriculum_2 { get; set; }

        public string descripcion_curriculum { get; set; }


        public byte[] foto_curriculum { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase file { get; set; }


        public int id_estado_curriculum { get; set; }
       // public virtual estado_curriculums estado_curriculums { get; set; }
    }
}