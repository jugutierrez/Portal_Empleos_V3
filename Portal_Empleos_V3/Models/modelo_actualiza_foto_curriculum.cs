using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_actualiza_foto_curriculum
    {
        public string id_foto { get; set; }

        public byte[] foto_curriculum { get; set; }
        [NotMapped]
        //[Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase file { get; set; }
    }
}