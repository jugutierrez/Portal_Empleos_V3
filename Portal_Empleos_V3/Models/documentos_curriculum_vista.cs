using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class documentos_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]
        public long id_documento_curriculum { get; set; }

        public string nombre_documento_curriculum { get; set; }

        public string enlace_documento_curriculum { get; set; }

        public string nombre_documento { get; set; }
    }
}