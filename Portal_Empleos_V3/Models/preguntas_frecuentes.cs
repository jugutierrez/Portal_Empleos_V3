using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class preguntas_frecuentes
    {

        public int id_pregunta_frecuente { get; set; }

        public string  nombre_pregunta { get; set; }

        public string respuesta { get; set; }

        public int id_categoria_pregunta_frecuente { get; set; }
    }
}