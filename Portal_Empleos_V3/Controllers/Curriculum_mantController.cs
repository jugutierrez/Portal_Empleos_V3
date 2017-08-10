using Portal_Empleos_V3.Models;
using Portal_Empleos_V3.seguridad;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Portal_Empleos_V3.Controllers
{
    public class Curriculum_mantController : Controller
    {
        // GET: Curriculum_mant
        private PersonaDBContext db = new PersonaDBContext();
        [CustomAuthorize]
        public ActionResult Index()
        {
            try
            {
          
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("LogOff", "login");
            }


        }
        [CustomAuthorize]
        public ActionResult postulaciones_inicio()
        {
            try
            {

                vista_info_postulaciones v_i = new vista_info_postulaciones();
                v_i.cantidad_postulaciones = db.Database.SqlQuery<Int32>("select count(id_postulacion) from postulaciones where id_persona = {0}", Convert.ToInt32(Session["persona_id"])).Single();
                v_i.evaluacion_postulaciones = db.Database.SqlQuery<Int32>("select count(id_postulacion) from postulaciones inner join estado_postulaciones on postulaciones.id_estado_postulacion = estado_postulaciones.id_estado_postulacion where id_persona = {0} and postulaciones.id_estado_postulacion > {1} ", Convert.ToInt32(Session["persona_id"]), 0).Single();
                v_i.finalista_postulaciones = db.Database.SqlQuery<Int32>("select count(id_postulacion) from postulaciones inner join estado_postulaciones on postulaciones.id_estado_postulacion = estado_postulaciones.id_estado_postulacion where id_persona = {0} and postulaciones.id_estado_postulacion = {1} ", Convert.ToInt32(Session["persona_id"]), 1).Single();

                if (v_i != null)
                {
                    return Json(new { success = true, postulaciones_i = v_i }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, postulaciones_i = v_i }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult postulaciones_inicio_ofertas()
        {


            try
            {

                List<vista_ofertas_recomendadas> v_o = db.Database.SqlQuery<vista_ofertas_recomendadas>("exec sp_vista_ofertas_recomendadas @id_persona = {0}", Convert.ToInt32(Session["persona_id"])).ToList();
                if (v_o.Count > 0)
                {
                    return Json(new { success = true, postulaciones_o = v_o }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(new { success = false, postulaciones_o = v_o }, JsonRequestBehavior.AllowGet); }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        [CustomAuthorize]
        public ActionResult vista_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                return View("curriculum_persona_mant");
            }
            catch (Exception ex)
            {
                return View("Error");
            }


        }
        [CustomAuthorize]
        public ActionResult vista_postulaciones()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                return View("curriculum_postulaciones_persona");
            }
            catch (Exception ex)
            {
                return View("Error");
            }


        }
        [CustomAuthorize]
        public ActionResult llamar_foto_curriculum()
        {
            string s;
            try
            {
                curriculums curriculum = db.curriculums.Find(Convert.ToInt32(Session["curriculum_id"]));



                if (curriculum.foto_curriculum != null)
                {
                    s = Convert.ToBase64String(curriculum.foto_curriculum);
                }
                else
                { s = null; }
                return Json(new { success = true, foto_v = new { id_curriculum = curriculum.id_curriculum, foto_curriculum = s } }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }

        }


        [CustomAuthorize]
        public ActionResult editar_vista_foto_curriculum(int? id)
        {
            try
            {
                if (Request.IsAuthenticated)
                {
                    return PartialView("_vista_termino_sesion");
                }

                ViewBag.id = id;
                return PartialView("c_foto/_editar_foto_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult actualiza_foto_curriculum(modelo_actualiza_foto_curriculum up_foto)
        {
            try
            {
                byte[] data = new byte[up_foto.file.ContentLength];
                up_foto.file.InputStream.Read(data, 0, up_foto.file.ContentLength);

                up_foto.foto_curriculum = data;
                db.Database.ExecuteSqlCommand("exec sp_actualiza_curriculum_foto @id_curriculum = {0} , @foto_curriculum = {1}", up_foto.id_foto, up_foto.foto_curriculum);
                return Json(new { success = true, foto_up = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult listar_tipo_identificacion_persona()
        {
            try
            {

                List<tipo_identificacion_personas> i_personas = db.tipo_identificacion_personas.ToList();



                return Json(new { success = true, tipo_i = i_personas }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult datos_persona_curriculum()
        {
            try
            {
                persona_curriculum_vista datos_persona = db.Database.SqlQuery<persona_curriculum_vista>("Exec sp_obtener_datos_curriculums  @id_persona = {0} ", Convert.ToInt32(Session["persona_id"])).Single();



                if (datos_persona != null)
                {
                    return Json(new { success = true, datos_p = datos_persona }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, datos_p = datos_persona }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult editar_vista_datos_persona_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }



                return PartialView("c_datos_persona/_editar_datos_persona_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult actualiza_datos_persona_curriculum(persona_curriculum_vista datos_per)
        {
            try
            {
                db.Database.ExecuteSqlCommand("exec sp_actualiza_persona_curriculum @id_persona = {0} , @nombre_persona = {1} , @apellido_materno_persona = {2} , @apellido_paterno_persona = {3} , " +
                    " @identificacion_persona = {4} , @correo_electronico_persona = {5} , @fecha_nacimiento_persona = {6} , @id_comuna ={7} , @id_tipo_identificacion_persona = {8} ,@id_tipo_persona = {9} , " +
                    "@direccion_curriculum = {10} , @telefono_curriculum_1 = {11} , @telefono_curriculum_2 = {12} ,@sueldo_esperado = {13} , @id_curriculum = {14}", Convert.ToInt32(Session["persona_id"]), datos_per.nombre_persona, datos_per.apellido_materno_persona,
                    datos_per.apellido_paterno_persona, datos_per.identificacion_persona, datos_per.correo_electronico_persona, datos_per.fecha_nacimiento_persona, datos_per.id_comuna, datos_per.id_tipo_identificacion_persona,
                    datos_per.id_tipo_persona, datos_per.direccion_curriculum, datos_per.telefono_curriculum_1, datos_per.telefono_curriculum_2, datos_per.sueldo_esperado, Convert.ToInt32(Session["curriculum_id"]));

                return Json(new { success = true, datos_p = datos_per }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult vista_descripcion_curriculum(int? id)
        {
            try
            {


                var c = db.curriculums.Find(Convert.ToInt32(Session["curriculum_id"]));

                if (String.IsNullOrEmpty(c.descripcion_curriculum))
                {

                    return Json(new { success = true, desc_c = "" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, desc_c = c.descripcion_curriculum }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult editar_vista_descripcion_curriculum()
        {
            try
            {
                /*
                if (!Request.IsAuthenticated)
                {
                    return PartialView("_vista_termino_sesion");
                }
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }*/

                return PartialView("c_descripcion/_editar_descripcion_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult vista_profesion_curriculum()
        {
            try
            {

                List<profesiones_curriculum_vista> profesiones_c = db.Database.SqlQuery<profesiones_curriculum_vista>("Exec sp_obtener_preferencia_curriculum @id_curriculum ={0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();

                if (profesiones_c.Count() > 0)
                {
                    return Json(new { success = true, profesion_c = profesiones_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, profesion_c = profesiones_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult agregar_profesiones_curriculum(modelo_agregar_profesion_curriculum nueva_profesion)
        {



            try
            {
                db.Database.ExecuteSqlCommand("insert into curriculums_profesiones (id_curriculum ,id_profesion) values({0},{1})", Convert.ToInt32(Session["curriculum_id"]), nueva_profesion.id_profesion);
                return Json(new { success = true, respuesta = nueva_profesion }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_profesiones_curriculum(int id)
        {



            try
            {
                db.Database.ExecuteSqlCommand("delete from curriculums_profesiones  where id_curriculum_profesion = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_profesiones_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                return PartialView("c_profesiones/_agregar_profesiones_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_profesiones_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                ViewBag.id_profesion = id;
                return PartialView("c_profesiones/_eliminar_profesiones_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }




        [CustomAuthorize]
        public ActionResult vista_experiencias_laborales_curriculum()
        {
            try
            {

                List<experiencias_laborales_curriculum_vista> expe_c = db.Database.SqlQuery<experiencias_laborales_curriculum_vista>("Exec sp_obtener_experiencia_laboral_curriculums @id_curriculum ={0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();

                foreach (var k in expe_c)
                {
                    k.referencia_laboral = db.Database.SqlQuery<referencia_laboral>("select * from referencia_laboral where id_referencia_laboral = {0}", k.id_referencia_laboral).Single();
                }

                if (expe_c.Count() > 0)
                {
                    return Json(new { success = true, exp_c = expe_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, exp_c = expe_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult detalle_experiencias_laborales_curriculum(int id)
        {
            try
            {
                experiencias_laborales_curriculum_vista expe_c = db.Database.SqlQuery<experiencias_laborales_curriculum_vista>("Exec sp_obtener_experiencia_laboral_curriculums_id @id_curriculum ={0} , @id_exp_curriculum = {1} ", Convert.ToInt32(Session["curriculum_id"]), id).Single();



                if (expe_c != null)
                {
                    return Json(new { success = true, exp_c = expe_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, exp_c = expe_c }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult edita_experiencia_laboral(int id, experiencias_laborales_curriculum_vista edita_experiencia_laboral)
        {
            try
            {

                db.Database.ExecuteSqlCommand("exec sp_actualiza_experiencias_laborales_curriculum @id_experiencia_laboral_curriculum = {0} , " +
                    "@id_cargo_experiencia_laboral = {1} ,  @id_area_experiencia_laboral = {2} , @nombre_experiencia_laboral = {3} ," +
                    "@empresa_experiencia_laboral = {4} , @ano_inicio_experiencia_laboral = {5} , @ano_termino_experiencia_laboral = {6}, @detalle_experiencia_laboral = {7}",
                    id, edita_experiencia_laboral.id_cargo_experiencia_laboral, edita_experiencia_laboral.id_area_experiencia_laboral,
                    edita_experiencia_laboral.nombre_experiencia_laboral, edita_experiencia_laboral.empresa_experiencia_laboral, edita_experiencia_laboral.ano_inicio_experiencia_laboral,
                    edita_experiencia_laboral.ano_termino_experiencia_laboral, edita_experiencia_laboral.detalle_experiencia_laboral);

                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_experiencias_laborales_curriculum(modelo_agregar_experiencia_laboral_curriculum nueva_experiencia_laboral)
        {



            try
            {
                db.Database.ExecuteSqlCommand(" exec sp_inserta_experiencias_laborales_curriculum @id_curriculum = {0} , @id_cargo_experiencia_laboral = {1} , @id_area_experiencia_laboral = {2} , " +
                    " @nombre_experiencia_laboral = {3}, @empresa_experiencia_laboral ={4}, @ano_inicio_experiencia_laboral = {5}, @ano_termino_experiencia_laboral = {6}, @detalle_experiencia_laboral = {7} ",
                    Convert.ToInt32(Session["curriculum_id"]), nueva_experiencia_laboral.id_cargo_experiencia_laboral, nueva_experiencia_laboral.id_area_experiencia_laboral, nueva_experiencia_laboral.nombre_experiencia_laboral,
                    nueva_experiencia_laboral.empresa_experiencia_laboral, nueva_experiencia_laboral.ano_inicio_experiencia_laboral, nueva_experiencia_laboral.ano_termino_experiencia_laboral, nueva_experiencia_laboral.detalle_experiencia_laboral);

                return Json(new { success = true, respuesta = nueva_experiencia_laboral }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_referencias_laborales_curriculum(modelo_agregar_referencia_laboral_curriculum nueva_referencia_laboral)
        {



            try
            {
                db.Database.ExecuteSqlCommand("exec sp_inserta_referencia_laboral @id_experiencia_laboral_curriculum = {0} ,@nombre_referencia_laboral = {1} , " +
                    "@cargo_referencia_laboral = {2} ,@contacto_referencia_laboral = {3} , @correo_referencia_laboral = {4}"
                    , nueva_referencia_laboral.id_experiencia_laboral_curriculum, nueva_referencia_laboral.nombre_referencia_laboral, nueva_referencia_laboral.cargo_referencia_laboral, nueva_referencia_laboral.contacto_referencia_laboral, nueva_referencia_laboral.correo_referencia_laboral);

                return Json(new { success = true, respuesta = nueva_referencia_laboral }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_experiencias_laborales_curriculum(int id)
        {



            try
            {
                var k = db.Database.SqlQuery<long>("select id_referencia_laboral from experiencias_laborales_curriculums where id_experiencia_laboral_curriculum = {0}", id).Single();
                if (k != 0)
                {
                    db.Database.ExecuteSqlCommand("exec sp_eliminar_referencia_laboral @a = {0} , @id_experiencia_laboral_curriculum = {1}", null, id);
                }
                db.Database.ExecuteSqlCommand("delete from experiencias_laborales_curriculums  where id_experiencia_laboral_curriculum = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_referencias_laborales_curriculum(int id)
        {



            try
            {
                var k = db.Database.SqlQuery<long>("select id_referencia_laboral from experiencias_laborales_curriculums where id_experiencia_laboral_curriculum = {0}", id).Single();
                if (k != 0)
                {
                    db.Database.ExecuteSqlCommand("exec sp_eliminar_referencia_laboral @a = {0} , @id_experiencia_laboral_curriculum = {1}", null, id);
                }
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_experiencias_laborales_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                return PartialView("c_experiencia_laboral/_agregar_experiencia_laboral_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult editar_vista_experiencias_laborales_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_experiencia_laboral = id;
                return PartialView("c_experiencia_laboral/_editar_experiencia_laboral_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_experiencias_laborales_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_experiencia_laboral = id;
                return PartialView("c_experiencia_laboral/_eliminar_experiencia_laboral_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_referencias_laborales_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_experiencia_laboral = id;
                return PartialView("c_experiencia_laboral/referencia_laboral/_eliminar_referencia_laboral_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult agregar_vista_referencias_laborales_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_experiencia_laboral_curriculum = id;
                return PartialView("c_experiencia_laboral/referencia_laboral/_agregar_referencia_laboral_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }






        [CustomAuthorize]
        public ActionResult vista_estudios_curriculum()
        {
            try
            {
                List<estudios_curriculum_vista> estudios_c = db.Database.SqlQuery<estudios_curriculum_vista>("Exec sp_obtener_estudios_curriculums @id_curriculum ={0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();
          
                if (estudios_c.Count() > 0)
                {
                    return Json(new { success = true, estudios_c = estudios_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, estudios_c = estudios_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult detalle_estudios_curriculum(int id)
        {
            try
            {
                estudios_curriculum_vista estudios_c = db.Database.SqlQuery<estudios_curriculum_vista>("Exec sp_obtener_estudios_curriculums_id @id_curriculum ={0} ,@id_estudio_cur = {1} ", Convert.ToInt32(Session["curriculum_id"]), id).Single();
            
                if (estudios_c != null)
                {
                    return Json(new { success = true, estudios_c = estudios_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, estudios_c = estudios_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult agregar_estudios_curriculum(modelo_agregar_estudios_curriculum nuevo_estudio)
        {



            try
            {
                db.Database.ExecuteSqlCommand(" exec sp_agregar_estudios_curriculum  @id_estudio = {0} , @id_curriculum = {1} , @id_estado_estudio = {2} , @id_institucion = {3} , @id_tipo_estudio = {4}, @ano_inicio_estudio_curriculum = {5} , @ano_termino_estudio_curriculum = {6}",
                   nuevo_estudio.id_estudio, Convert.ToInt32(Session["curriculum_id"]), nuevo_estudio.id_estado_estudio, nuevo_estudio.id_institucion, nuevo_estudio.id_tipo_estudio, nuevo_estudio.ano_inicio_estudio_curriculum, nuevo_estudio.ano_termino_estudio_curriculum);

                return Json(new { success = true, respuesta = nuevo_estudio }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_estudios_curriculum(int id)
        {



            try
            {
                db.Database.ExecuteSqlCommand("delete from estudios_curriculums  where id_estudio_curriculum = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult edita_estudios(int id, estudios_curriculum_vista edita_estudios)
        {
            try
            {

                db.Database.ExecuteSqlCommand("exec sp_actualiza_estudios_curriculums @id_estudio_curriculum = {0} , @id_estudio = {1} , @id_estado_estudio = {2} , " +
                    "@id_institucion = {3} ,@id_tipo_estudio = {4} , @ano_inicio_estudio_curriculum  = {5} , @ano_termino_estudio_curriculum = {6}",
                   id, edita_estudios.id_estudio, edita_estudios.id_estado_estudio, edita_estudios.id_institucion, edita_estudios.id_tipo_estudio,
                    edita_estudios.ano_inicio_estudio_curriculum, edita_estudios.ano_termino_estudio_curriculum);

                return Json(new { success = true, exp_c = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_estudios_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                return PartialView("c_estudios/_agregar_estudios_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult editar_vista_estudios_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_estudio = id;
                return PartialView("c_estudios/_editar_estudios_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }

        [CustomAuthorize]
        public ActionResult eliminar_vista_estudios_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_estudio = id;
                return PartialView("c_estudios/_eliminar_estudios_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult vista_capacitaciones_curriculum()
        {
            try
            {
                List<capacitaciones_curriculum_vista> capacitaciones_c = db.Database.SqlQuery<capacitaciones_curriculum_vista>("Exec sp_obtener_capacitaciones_curriculums @id_curriculum ={0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();


                if (capacitaciones_c.Count() > 0)
                {
                    return Json(new { success = true, capacitaciones_c = capacitaciones_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, capacitaciones_c = capacitaciones_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult agregar_capacitaciones_curriculum(modelo_agregar_capacitaciones_curriculum nueva_capacitacion)
        {



            try
            {
                db.Database.ExecuteSqlCommand("insert into capacitaciones_curriculums (id_capacitacion,id_curriculum , id_estado_capacitacion, id_institucion , id_tipo_capacitacion, ano_inicio_capacitacion_curriculum ,ano_termino_capacitacion_curriculum , horas_capacitacion , descripcion_capacitacion) values({0},{1},{2},{3},{4}, {5},{6},{7}, {8})",
                   nueva_capacitacion.id_capacitacion, Convert.ToInt32(Session["curriculum_id"]), nueva_capacitacion.id_estado_capacitacion, nueva_capacitacion.id_institucion, 0, nueva_capacitacion.ano_inicio_capacitacion_curriculum, nueva_capacitacion.ano_termino_capacitacion_curriculum, nueva_capacitacion.horas_capacitacion, nueva_capacitacion.descripcion_capacitacion);

                return Json(new { success = true, respuesta = nueva_capacitacion }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult detalle_capacitaciones_curriculum(int id)
        {
            try
            {
                capacitaciones_curriculum_vista ca_c = db.Database.SqlQuery<capacitaciones_curriculum_vista>("Exec sp_obtener_capacitaciones_curriculums_id @id_curriculum ={0} ,@id_cap_curriculum = {1} ", Convert.ToInt32(Session["curriculum_id"]), id).Single();
                //List<experiencias_laborales_curriculum_vista> estudios_c = db.Database.SqlQuery<experiencias_laborales_curriculum_vista>("Exec sp_obtener_experiencia_laboral_curriculums @id_curriculum ={0} ", 0).ToList();

                if (ca_c != null)
                {
                    return Json(new { success = true, ca_c = ca_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, ca_c = ca_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult edita_capacitaciones(int id, capacitaciones_curriculum_vista edita_capacitacion)
        {
            try
            {

                db.Database.ExecuteSqlCommand("exec sp_actualiza_capacitaciones_curriculums @id_capacitacion_curriculum = {0} , @id_capacitacion = {1} , @id_tipo_capacitacion = {2} , @id_estado_capacitacion = {3} ," +
                    "@id_institucion = {4} , @horas_capacitacion = {5} , @descripcion_capacitacion = {6} , @ano_inicio_capacitacion_curriculum = {7} , @ano_termino_capacitacion_curriculum = {8}",
                    id, edita_capacitacion.id_capacitacion, edita_capacitacion.id_tipo_capacitacion, edita_capacitacion.id_estado_capacitacion, edita_capacitacion.id_institucion, edita_capacitacion.horas_capacitacion,
                    edita_capacitacion.descripcion_capacitacion, edita_capacitacion.ano_inicio_capacitacion_curriculum, edita_capacitacion.ano_termino_capacitacion_curriculum);

                return Json(new { success = true, exp_c = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_capacitaciones_curriculum(int id)
        {



            try
            {
                db.Database.ExecuteSqlCommand("delete from capacitaciones_curriculums  where id_capacitacion_curriculum = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_capacitaciones_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                return PartialView("c_capacitaciones/_agregar_capacitaciones_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult editar_vista_capacitaciones_curriculum(int? id)
        {

            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_capacitacion = id;
                return PartialView("c_capacitaciones/_editar_capacitaciones_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }
        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_capacitaciones_curriculum(int? id)
        {

            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_capacitacion = id;
                return PartialView("c_capacitaciones/_eliminar_capacitaciones_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }
        }

        public ActionResult vista_softwares_curriculum()
        {
            try
            {
                List<softwares_curriculum_vista> softwares_c = db.Database.SqlQuery<softwares_curriculum_vista>("Exec sp_obtener_softwares_curriculums @id_curriculum ={0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();


                if (softwares_c.Count() > 0)
                {
                    return Json(new { success = true, softwares_c = softwares_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, softwares_c = softwares_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult agregar_softwares_curriculum(modelo_agregar_softwares_curriculum nuevo_software)
        {



            try
            {
                db.Database.ExecuteSqlCommand("insert into softwares_curriculums (id_curriculum , id_software , id_grado_conocimiento_software) values({0},{1},{2})", Convert.ToInt32(Session["curriculum_id"]), nuevo_software.id_software, nuevo_software.id_grado_conocimiento_software);

                return Json(new { success = true, respuesta = nuevo_software }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_softwares_curriculum(int id)
        {



            try
            {
                db.Database.ExecuteSqlCommand("delete from softwares_curriculums  where id_software_curriculum = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_softwares_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                return PartialView("c_softwares/_agregar_softwares_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_softwares_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_software = id;
                return PartialView("c_softwares/_eliminar_softwares_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult vista_habilidades_curriculum()
        {
            try
            {
                List<habilidades_curriculum_vista> habilidades_c = db.Database.SqlQuery<habilidades_curriculum_vista>("Exec sp_obtener_habilidades_curriculums @id_curriculum ={0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();


                if (habilidades_c.Count() > 0)
                {
                    return Json(new { success = true, habilidades_c = habilidades_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, habilidades_c = habilidades_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult agregar_habilidades_curriculum(modelo_agregar_habilidades_curriculum nueva_habilidad)
        {



            try
            {
                db.Database.ExecuteSqlCommand("insert into habilidades_curriculums (id_habilidad ,id_curriculum ,id_grado_habilidad) values({0},{1},{2})", nueva_habilidad.id_habilidad, Convert.ToInt32(Session["curriculum_id"]), nueva_habilidad.id_grado_habilidad);
                return Json(new { success = true, respuesta = nueva_habilidad }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_habilidades_curriculum(int id)
        {



            try
            {
                db.Database.ExecuteSqlCommand("delete from habilidades_curriculums  where id_habilidad_curriculum = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_habilidades_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                return PartialView("c_habilidades/_agregar_habilidades_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_habilidades_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_habilidad = id;

                string k = db.Database.SqlQuery<string>("select nombre_habilidad from habilidades inner join habilidades_curriculums on habilidades_curriculums.id_habilidad = habilidades.id_habilidad where habilidades_curriculums.id_habilidad_curriculum = {0}", id).Single();
                ViewBag.id_nombre_habilidad = k;
                return PartialView("c_habilidades/_eliminar_habilidades_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult vista_idiomas_curriculum()
        {
            try
            {
                List<idiomas_curriculum_vista> idiomas_c = db.Database.SqlQuery<idiomas_curriculum_vista>("Exec sp_obtener_idiomas_curriculums @id_curriculum ={0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();


                if (idiomas_c.Count() > 0)
                {
                    return Json(new { success = true, idiomas_c = idiomas_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, idiomas_c = idiomas_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult agregar_idiomas_curriculum(modelo_agregar_idiomas_curriculum nuevo_idioma)
        {



            try
            {
                db.Database.ExecuteSqlCommand("insert into idiomas_curriculums (id_idioma , id_curriculum,id_nivel_idioma) values({0},{1},{2})", nuevo_idioma.id_idioma, Convert.ToInt32(Session["curriculum_id"]), nuevo_idioma.id_nivel_idioma);
                return Json(new { success = true, respuesta = nuevo_idioma }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_idiomas_curriculum(int id)
        {



            try
            {
                db.Database.ExecuteSqlCommand("delete from idiomas_curriculums  where id_idioma_curriculum = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_idiomas_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                return PartialView("c_idiomas/_agregar_idiomas_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_idiomas_curriculum(int? id)
        {

            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_idioma = id;
                string k = db.Database.SqlQuery<string>("select nombre_idioma from idiomas inner join idiomas_curriculums on idiomas_curriculums.id_idioma = idiomas.id_idioma where idiomas_curriculums.id_idioma_curriculum = {0}", id).Single();
                ViewBag.id_nombre_idioma = k;
                return PartialView("c_idiomas/_eliminar_idiomas_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }

        [CustomAuthorize]
        public ActionResult vista_documento_curriculum()
        {
            try
            {
                List<documentos_curriculum_vista> documentos_c = db.Database.SqlQuery<documentos_curriculum_vista>("Exec sp_obtener_documentos_curriculums @id_curriculum = {0} ", Convert.ToInt32(Session["curriculum_id"])).ToList();


                if (documentos_c.Count() > 0)
                {
                    return Json(new { success = true, doc_c = documentos_c }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, doc_c = documentos_c }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult agregar_documentos_curriculum(modelo_agregar_documentos_curriculum nuevo_documento)
        {



            try
            {
                db.Database.ExecuteSqlCommand("insert into documentos_curriculums (id_documento ,id_curriculum , nombre_documento_curriculum, enlace_documento_curriculum , fecha_creacion_ducumento_curriculum) values(1,{0},{1},{2},getdate())", Convert.ToInt32(Session["curriculum_id"]),
                    nuevo_documento.nombre_documento_curriculum, nuevo_documento.enlace_documento_curriculum);

                return Json(new { success = true, respuesta = nuevo_documento }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult eliminar_documentos_curriculum(int id)
        {



            try
            {
                db.Database.ExecuteSqlCommand("delete from documentos_curriculums  where id_documento_curriculum = {0}", id);
                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }


        }
        [CustomAuthorize]
        public ActionResult agregar_vista_documentos_curriculum()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                return PartialView("c_documentos/_agregar_documentos_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult detalle_vista_documentos_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                vista_documentos_curriculum v_o = db.Database.SqlQuery<vista_documentos_curriculum>("select id_documento_curriculum , nombre_documento_curriculum, enlace_documento_curriculum from documentos_curriculums where id_documento_curriculum =  {0}", id).Single();

                ViewBag.nombre_documento = v_o.nombre_documento_curriculum;
                ViewBag.enlace_documento = v_o.enlace_documento_curriculum;
                return PartialView("c_documentos/_detalle_documentos_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }
        [CustomAuthorize]
        public ActionResult eliminar_vista_documentos_curriculum(int? id)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }

                ViewBag.id_documento = id;
                return PartialView("c_documentos/_eliminar_documentos_curriculum");
            }
            catch (Exception ex)
            {
                return PartialView("Error");
            }

        }



        public ActionResult rellenar_habilidad()
        {
            try
            {

                List<habilidades> hab = db.habilidades.ToList();



                return Json(new { success = true, rellenar_ha = hab }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_grado_habilidad()
        {
            try
            {

                List<grado_habilidades> hab = db.grado_habilidades.ToList();



                return Json(new { success = true, rellenar_grado_ha = hab }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        public ActionResult rellenar_idiomas()
        {
            try
            {

                List<idiomas> r_idioma = db.idiomas.ToList();



                return Json(new { success = true, rellenar_idioma = r_idioma }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        public ActionResult rellenar_nivel_idiomas()
        {
            try
            {

                List<nivel_idiomas> r_n_idioma = db.nivel_idiomas.ToList();



                return Json(new { success = true, rellenar_n_idioma = r_n_idioma }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        public ActionResult rellenar_profesiones()
        {
            try
            {

                List<profesiones> r_profesion = db.profesiones.ToList();



                return Json(new { success = true, rellenar_p = r_profesion }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_software()
        {
            try
            {

                List<softwares> r_softwares = db.softwares.ToList();



                return Json(new { success = true, rellenar_s = r_softwares }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_grado_software()
        {
            try
            {

                List<grado_conocimiento_softwares> r_grado_softwares = db.grado_conocimiento_softwares.ToList();



                return Json(new { success = true, rellenar_g_s = r_grado_softwares }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        public ActionResult rellenar_institucion()
        {
            try
            {

                List<instituciones> r_i = db.instituciones.ToList();



                return Json(new { success = true, rellenar_ints = r_i }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_capacitacion()
        {
            try
            {

                List<capacitaciones> r_c = db.capacitaciones.ToList();



                return Json(new { success = true, rellenar_cap = r_c }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_estado_capacitacion()
        {
            try
            {

                List<estado_capacitaciones> r_e_c = db.estado_capacitaciones.ToList();



                return Json(new { success = true, rellenar_e_cap = r_e_c }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_estudios()
        {
            try
            {

                List<estudios> r_es = db.estudios.ToList();



                return Json(new { success = true, rellenar_est = r_es }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_tipo_estudios()
        {
            try
            {

                List<tipo_estudios> r_t_es = db.tipo_estudios.ToList();



                return Json(new { success = true, rellenar_t_est = r_t_es }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_estado_estudios()
        {
            try
            {

                List<estado_estudios> r_e_es = db.estado_estudios.ToList();



                return Json(new { success = true, rellenar_e_est = r_e_es }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        public ActionResult rellenar_cargo_exp()
        {
            try
            {

                List<cargo_experiencias_laborales> r_c_exp = db.cargo_experiencias_laborales.ToList();



                return Json(new { success = true, rellenar_c_exp = r_c_exp }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_area_exp()
        {
            try
            {

                List<area_experiencias_laborales> r_a_exp = db.area_experiencias_laborales.ToList();



                return Json(new { success = true, rellenar_a_exp = r_a_exp }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        public ActionResult rellenar_estado_postulaciones()
        {
            try
            {

                List<estado_postulaciones> r_e_pos = db.estado_postulaciones.ToList();



                return Json(new { success = true, rellenar_e_pos = r_e_pos }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }

        [CustomAuthorize]
        public ActionResult listar_postulaciones_persona()
        {
            try
            {

                List<vista_listado_postulaciones> v_l_p = db.Database.SqlQuery<vista_listado_postulaciones>("select postulaciones.id_oferta , ofertas.nombre_oferta , postulaciones.fecha_postulacion , postulaciones.id_estado_postulacion from postulaciones inner join ofertas " +
" on postulaciones.id_oferta = ofertas.id_oferta where id_persona = {0} ", Convert.ToInt32(Session["persona_id"])).ToList();

                foreach (var item in v_l_p)

                {


                    item.cantidad_postulantes = db.Database.SqlQuery<int>("select count(id_postulacion) from postulaciones where id_oferta = {0}", item.id_oferta).Single();

                }


                return Json(new { success = true, listado_post = v_l_p }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_region()
        {
            try
            {

                List<regiones> r_region = db.regiones.ToList();



                return Json(new { success = true, rellenar_r = r_region }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_discapacidad()
        {
            try
            {

                List<discapacidad_personas> r_d = db.discapacidad_personas.ToList();



                return Json(new { success = true, rellenar_d = r_d }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        public ActionResult rellenar_genero()
        {
            try
            {

                List<tipo_personas> r_g = db.tipo_personas.ToList();



                return Json(new { success = true, rellenar_g = r_g }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            }
        }
        [CustomAuthorize]
        public ActionResult actualiza_descripcion(modelo_actualiza_descripcion actualizar_descripcion)
        {
            try
            {
                if (!Request.IsAuthenticated)
                {
                    return RedirectToAction("LogOff", "login");
                }
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                db.Database.ExecuteSqlCommand("UPDATE curriculums SET descripcion_curriculum = {0}  WHERE id_curriculum = {1}", actualizar_descripcion.descripcion_curriculum, Convert.ToInt32(Session["curriculum_id"]));

                return Json(new { success = true, respuesta = actualizar_descripcion }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }



        [CustomAuthorize]
        public async Task<ActionResult> descarga_curriculum1()
        {
            try
            {

                int id = Convert.ToInt32(Session["persona_id"]);
                super_modelo_curriculum s_m_c2 = new super_modelo_curriculum();
                s_m_c2.persona_curriculum_vista = db.Database.SqlQuery<persona_curriculum_vista>("Exec sp_obtener_datos_curriculums @id_persona = {0} ", id).Single();
                s_m_c2.capacitaciones_curiculums_vista = db.Database.SqlQuery<capacitaciones_curriculum_vista>("Exec sp_obtener_capacitaciones_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.habilidades_curriculums_vista = db.Database.SqlQuery<habilidades_curriculum_vista>("Exec sp_obtener_habilidades_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.curriculums = db.curriculums.Find(s_m_c2.persona_curriculum_vista.id_curriculum);

                List<experiencias_laborales_curriculum_vista> e_l = db.Database.SqlQuery<experiencias_laborales_curriculum_vista>("Exec sp_obtener_experiencia_laboral_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                foreach (var k in e_l)
                {
                    k.referencia_laboral = db.Database.SqlQuery<referencia_laboral>("select * from referencia_laboral where id_referencia_laboral = {0}", k.id_referencia_laboral).Single();
                }
                s_m_c2.experiencias_laborales_curriculums_vista = e_l;

                s_m_c2.estudios_curriculums_vista = db.Database.SqlQuery<estudios_curriculum_vista>("Exec sp_obtener_estudios_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.idiomas_curriculums_vista = db.Database.SqlQuery<idiomas_curriculum_vista>("Exec sp_obtener_idiomas_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.softwares_curriculums_vista = db.Database.SqlQuery<softwares_curriculum_vista>("Exec sp_obtener_softwares_curriculums @id_curriculum ={0} ", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                s_m_c2.documentos_curriculums = db.Database.SqlQuery<vista_documentos_curriculum>("exec sp_obtener_documentos_curriculums @id_curriculum = {0}", s_m_c2.persona_curriculum_vista.id_curriculum).ToList();
                return new PartialViewAsPdf("c_descargas/_vista_curriculum_descarga", s_m_c2);
            }
            catch (Exception ex)
            {
                return new PartialViewAsPdf("Error");

            }
        }

    }


}