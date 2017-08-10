using Portal_Empleos_V3.Models;
using Postal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Portal_Empleos_v3.Controllers
{
    public class soporteController : Controller
    {
        private PersonaDBContext db = new PersonaDBContext();
        // GET: soporte
        public ActionResult Index()
        {


            return View();
        }

        public ActionResult enviar_soporte(modelo_datos_soporte nuevo_soporte)
        {
            //mail m = new mail();

            //    byte[] data = new byte[nuevo_soporte.fotos.ContentLength];
            //  nuevo_soporte.fotos.InputStream.Read(data, 0, nuevo_soporte.fotos.ContentLength);

            //nuevo_soporte.foto_soporte = data;

            //m.enviar_correo(nuevo_soporte.foto_soporte, nuevo_soporte.correo_remitente, 0);
            //m.Correo_Soporte(nuevo_soporte.correo_remitente, nuevo_soporte.nombre_remitente, nuevo_soporte.rut_remitente, nuevo_soporte.asunto_mensaje, nuevo_soporte.descripcion_mensaje, nuevo_soporte.foto_soporte);


            return View("index");
        }

        public async Task<ActionResult> gestor_correos(int? id, modelo_datos_soporte recordar_cuenta_personas)

        {
          //  mail m = new mail();
            string formato = null;
            string titulo = null;
            string email_para = null;
            string nombre = null;
            string body = null;
            string rut = null;
            personas p = new personas();
            if (recordar_cuenta_personas.identificacion_persona != null)
            {
                p = db.Database.SqlQuery<personas>("select * from personas where identificacion_persona = {0}", recordar_cuenta_personas.identificacion_persona).Single();
                email_para = p.correo_electronico_persona;
                nombre = p.nombre_persona + " " + p.apellido_paterno_persona + "" + p.apellido_materno_persona;

            }

            if (Session.Contents.Count > 0)
            {
                p = db.personas.Find(Convert.ToInt32(Session["persona_id"]));
                email_para = p.correo_electronico_persona;
                nombre = p.nombre_persona + " " + p.apellido_paterno_persona + "" + p.apellido_materno_persona;
                rut = p.identificacion_persona;
            }



            Boolean adjunto = false;

            switch (id)
            {
                case 0:
                    formato = "recupera_contraseña_correo";
                    titulo = "Recuperacion de Credenciales de Acceso";
                    body = "hola hablo puras weas";
                    break;

                default:
                    formato = "";
                    titulo = "";
                    break;
            }

            dynamic email = new Email(formato);
            email.To = email_para;
            email.Subject = titulo;
            email.nombre = nombre;
            email.body = body;
            email.rut = rut;
            if (adjunto == true)
            {


                // Attachment datax = new Attachment(new MemoryStream(data), "Documento_adjunto.pdf", "application/pdf");
                //email.Attachments.Add(datax);
            }
            //email.IsBodyHtml = true;
            email.Send();




            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}