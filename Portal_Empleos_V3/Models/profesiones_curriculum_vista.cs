using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class profesiones_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]

        public int id_curriculum_profesion { get; set; }

        public int id_profesion { get; set; }

        public string nombre_profesion { get; set; }
    }
}