using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_agregar_experiencia_laboral_curriculum
    {


        public string id_cargo_experiencia_laboral { get; set; }

        public string id_area_experiencia_laboral { get; set; }

        public string nombre_experiencia_laboral { get; set; }

        public string empresa_experiencia_laboral { get; set; }

        public DateTime ano_inicio_experiencia_laboral { get; set; }

        public DateTime ano_termino_experiencia_laboral { get; set; }

        public string detalle_experiencia_laboral { get; set; }
    }
}