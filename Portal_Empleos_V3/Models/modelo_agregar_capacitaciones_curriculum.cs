using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_agregar_capacitaciones_curriculum
    {
        public string id_capacitacion { get; set; }

        public  string id_estado_capacitacion  { get; set; }

        public string id_institucion { get; set; }

        public string id_tipo_capacitacion { get; set; }

        public string ano_inicio_capacitacion_curriculum { get; set; }

        public string ano_termino_capacitacion_curriculum { get; set; }

        public string horas_capacitacion { get; set; }

        public string descripcion_capacitacion { get; set; }

    }
}