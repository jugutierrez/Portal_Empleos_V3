using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class vista_documentos_ofertas
    {
        [Key]
        [Column(Order = 1)]
        public int id_oferta_documento { get; set; }

        public string nombre_documento_oferta { get; set; }

        public string nombre_relevancia_documento { get; set; }

        public string enlace_documento_oferta { get; set; }
    }
}