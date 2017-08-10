using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class vista_cuestionario_ofertas
    {

        [Key]
        [Column(Order = 1)]

        public int id_cuestionario_pregunta { get; set; }

        public int id_cuestionario { get; set; }

        public int id_pregunta { get; set; }

        public string nombre_pregunta { get; set; }

        public int id_tipo_pregunta { get; set; }
      
                 public List<vista_respuesta_multiple_pregunta> vista_respuesta_multiple_pregunta { get; set; }


    }
}