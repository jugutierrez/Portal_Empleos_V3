using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_cuentas
    {
        private List<personas> lista_cuentas = new List<personas>();
        PersonaDBContext db = new PersonaDBContext();

        public modelo_cuentas()
        {

            //   lista_cuentas.Add(new cuentas { username = "c1", password = "123123", roles = new string[] {"0","1","2","3" } });
            // lista_cuentas.Add(new cuentas { username = "c2", password = "123123", roles = new string[] { "0", "1" } });
            //lista_cuentas.Add(new cuentas { username = "c3", password = "123123", roles = new string[] { "0" } });
            //lista_cuentas.Add(new cuentas { username = "c4", password = "123123", roles = new string[] { "0", "1", "2" } });
            lista_cuentas = db.personas.ToList();

        }
        public personas find(string user)
        {
            return lista_cuentas.Where(acc => acc.correo_electronico_persona.Equals(user)).FirstOrDefault();
        }

        public personas login(string user, string pass)

        {
            return lista_cuentas.Where(acc => acc.correo_electronico_persona.Equals(user) && acc.clave_persona.Equals(pass)).FirstOrDefault();
        }

    }
}