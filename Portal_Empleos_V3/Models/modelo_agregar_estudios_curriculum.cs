using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_agregar_estudios_curriculum
    {
        public string id_estudio { get; set; }

        public string id_estado_estudio { get; set; }

        public string id_institucion { get; set; }

        public string  id_tipo_estudio { get; set; }

        public DateTime ano_inicio_estudio_curriculum { get; set; }

        public DateTime ano_termino_estudio_curriculum { get; set; }
    }
}