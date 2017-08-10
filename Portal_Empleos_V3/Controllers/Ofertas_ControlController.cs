using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Portal_Empleos_V3.Models;
using System.Net;
using System.Data.SqlClient;
using Portal_Empleos_V3.seguridad;

namespace Portal_Empleos_V3.Controllers
{
    public class Ofertas_ControlController : Controller
    {
        public int itemsPerPage = 5;
        private PersonaDBContext db = new PersonaDBContext();
        //mail m = new mail();

        public ActionResult Index()
        {

            return RedirectToAction("filtrar_basico");
        }
        public ViewResult filtrar_basico(int? page, string palabra_clave)
        {
            List<vista_buscador_oferta> Articles;

            cargarcombos();
            if (palabra_clave == null)
            {
                palabra_clave = "";
            }
            //carga_estados();
            Articles = db.Database.SqlQuery<vista_buscador_oferta>("exec sp_buscador_ofertas_basico_portal @palabra_clave = {0}", palabra_clave).ToList();

            foreach (var items in Articles)
            {
                items.cantidad_postulados_oferta = db.Database.SqlQuery<int>("select count(id_postulacion) from postulaciones where id_oferta = {0}", items.id_oferta).Single();
                items.categorias_oferta = db.Database.SqlQuery<vista_categorias_ofertas>("exec sp_obtener_categorias_ofertas @id_oferta = {0}", items.id_oferta).ToList(); ;

            }
            //cargarcombos();
            int pageNumber = (page ?? 1);
            ViewBag.palabra_clave = palabra_clave;
            ViewBag.control = "filtrar_basico";
            return View("index", Articles.ToPagedList(pageNumber, itemsPerPage));

        }
        public ViewResult filtrar_full(int? page, string id_categoria, string id_tipo_oferta, string id_contrato_oferta,
            string id_jornada_oferta, string monto_maximo, string ultimo_update, FormCollection form)
        {
            List<vista_buscador_oferta> Articles;

            string[] rad = new string[form.AllKeys.Count()];
            if (form.AllKeys.Length != 0)
            {
                for (int i = 0; i < form.AllKeys.Count(); i++)
                {
                    if (Request[form.Keys[i]] != "")
                    {
                        rad[i] = Request[form.Keys[i]];
                    }
                    else
                    {
                        rad[i] = null;
                    }

                }

            }
            else
            {
                rad = new string[6];
                rad[0] = id_categoria;
                rad[1] = id_tipo_oferta;
                rad[2] = id_contrato_oferta;
                rad[3] = id_jornada_oferta;
                rad[4] = monto_maximo;
                rad[5] = ultimo_update;


            }



            ViewBag.filtros1 = rad[0];
            ViewBag.filtros2 = rad[1];
            ViewBag.filtros3 = rad[2];

            ViewBag.filtros4 = rad[3];
            ViewBag.filtros5 = rad[4];
            ViewBag.filtros6 = rad[5];







            Articles = db.Database.SqlQuery<vista_buscador_oferta>("exec sp_buscador_ofertas_avanzado_portal @id_categoria = {0}  , @id_tipo_oferta = {1} , "
           + "@id_contrato_oferta = {2}, @id_jornada_oferta = {3}, "
           + " @monto_maximo = {4}, @ultimo_update = {5}",
          rad[0], rad[1], rad[2], rad[3], rad[4],
           rad[5]).ToList();
            foreach (var items in Articles)
            {
                items.cantidad_postulados_oferta = db.Database.SqlQuery<int>("select count(id_postulacion) from postulaciones where id_oferta = {0}", items.id_oferta).Single();
                items.categorias_oferta = db.Database.SqlQuery<vista_categorias_ofertas>("exec sp_obtener_categorias_ofertas @id_oferta = {0}", items.id_oferta).ToList(); ;

            }

            cargarcombos();
            int pageNumber = (page ?? 1);

            ViewBag.control = "filtrar_full";
            return View("index", Articles.ToPagedList(pageNumber, itemsPerPage));
        }
        public void cargarcombos()
        {
            ViewBag.id_categoria = new SelectList(db.categorias, "id_categoria", "nombre_categoria");

            ViewBag.id_jornada_oferta = new SelectList(db.jornada_ofertas, "id_jornada_oferta", "nombre_jornada_oferta");
            ViewBag.id_contrato_oferta = new SelectList(db.contrato_ofertas, "id_contrato_oferta", "nombre_contrato_oferta");
            ViewBag.id_tipo_oferta = new SelectList(db.tipo_ofertas, "id_tipo_oferta", "nombre_tipo_oferta");

            List<listado_fecha_actualizacion> f_a = new List<listado_fecha_actualizacion>();
            //  f_a.Add(new listado_fecha_actualizacion() { valor_fecha_actualizacion = "", nombre_fecha_actualizacion = "Seleccione..." });
            f_a.Add(new listado_fecha_actualizacion() { valor_fecha_actualizacion = "15", nombre_fecha_actualizacion = "Menor a 15 días" });
            f_a.Add(new listado_fecha_actualizacion() { valor_fecha_actualizacion = "30", nombre_fecha_actualizacion = "Menor a 1 mes" });
            f_a.Add(new listado_fecha_actualizacion() { valor_fecha_actualizacion = "90", nombre_fecha_actualizacion = " Menor a 3 meses" });
            f_a.Add(new listado_fecha_actualizacion() { valor_fecha_actualizacion = "180", nombre_fecha_actualizacion = "Menor a 6 meses" });
            f_a.Add(new listado_fecha_actualizacion() { valor_fecha_actualizacion = "999999", nombre_fecha_actualizacion = "Mas de 6 meses" });

            ViewBag.ultimo_update = new SelectList(f_a, "valor_fecha_actualizacion", "nombre_fecha_actualizacion");
        }

        public ViewResult vista_detalle_oferta(int id)
        {
            ViewBag.id_oferta = id;
            var x = db.Database.SqlQuery<int>("select id_especificacion_oferta from ofertas where id_oferta  = {0}", id).Single();
            ViewBag.id_especificacion_oferta = x;
            if (Session.Keys.Count > 0)
            {
                int bol = db.Database.SqlQuery<int>("select id_postulacion from postulaciones where id_persona = {0} and id_oferta = {1}", Convert.ToInt32(Session["persona_id"]), id).Count();
                ViewBag.id_postulado = bol;
            }
            return View("detalle_ofertas_control");
        }

        public ActionResult vista_datos_detalle_oferta(int id)
        {
            try
            {
                vista_datos_detalle_ofertas datos_detalle_oferta = db.Database.SqlQuery<vista_datos_detalle_ofertas>("exec sp_obtener_datos_oferta @id_oferta = {0}", id).Single();

                if (datos_detalle_oferta.ToString().Length > 0)
                {
                    return Json(new { success = true, det_of_c = datos_detalle_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, det_of_c = datos_detalle_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult vista_habilidades_oferta(int id)
        {
            try
            {
                List<vista_habilidades_ofertas> habilidades_oferta = db.Database.SqlQuery<vista_habilidades_ofertas>("exec sp_obtener_especificaciones_ofertas_habilidades @id_especificacion_oferta = {0}", id).ToList();

                if (habilidades_oferta.Count > 0)
                {
                    return Json(new { success = true, habilidad_o = habilidades_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, habilidad_o = habilidades_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult vista_idiomas_oferta(int id)
        {
            try
            {
                List<vista_idiomas_ofertas> idiomas_oferta = db.Database.SqlQuery<vista_idiomas_ofertas>("exec sp_obtener_especificaciones_ofertas_idiomas @id_especificacion_oferta = {0}", id).ToList();

                if (idiomas_oferta.Count > 0)
                {
                    return Json(new { success = true, idiomas_o = idiomas_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, idiomas_o = idiomas_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult vista_categorias_oferta(int id)
        {
            try
            {
                List<vista_categorias_ofertas> categorias_oferta = db.Database.SqlQuery<vista_categorias_ofertas>("exec sp_obtener_categorias_ofertas @id_oferta = {0}", id).ToList();

                if (categorias_oferta.Count > 0)
                {
                    return Json(new { success = true, cat_o = categorias_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, cat_o = categorias_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult vista_documentos_oferta(int id)
        {
            try
            {
                List<vista_documentos_ofertas> documentos_oferta = db.Database.SqlQuery<vista_documentos_ofertas>("exec sp_obtener_documentos_ofertas @id_oferta = {0}", id).ToList();

                if (documentos_oferta.Count > 0)
                {
                    return Json(new { success = true, doc_o = documentos_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, doc_o = documentos_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public PartialViewResult detalle_vista_documentos_curriculum(int? id)
        {
            vista_documentos_ofertas v_b = db.Database.SqlQuery<vista_documentos_ofertas>("exec sp_obtener_documentos_ofertas @id_oferta = {0}", id).Single();
            ViewBag.id_documento = v_b.id_oferta_documento;
            ViewBag.nombre_documento = v_b.nombre_documento_oferta;
            ViewBag.enlace_documento = v_b.enlace_documento_oferta;
            return PartialView("oferta_documento/_vista_detalle_documentos_curriculum");
        }
        public ActionResult vista_profesiones_oferta(int id)
        {
            try
            {
                List<vista_profesiones_ofertas> profesiones_oferta = db.Database.SqlQuery<vista_profesiones_ofertas>("exec sp_obtener_especificaciones_ofertas_profesiones @id_especificacion_oferta = {0}", id).ToList();

                if (profesiones_oferta.Count > 0)
                {
                    return Json(new { success = true, prof_o = profesiones_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, prof_o = profesiones_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult vista_softwares_oferta(int id)
        {
            try
            {
                List<vista_softwares_ofertas> softwares_oferta = db.Database.SqlQuery<vista_softwares_ofertas>("exec sp_obtener_especificaciones_ofertas_softwares @id_especificacion_oferta = {0}", id).ToList();

                if (softwares_oferta.Count > 0)
                {
                    return Json(new { success = true, soft_o = softwares_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, soft_o = softwares_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult vista_cuestionario_oferta(int id)
        {
            try
            {
                List<vista_cuestionario_ofertas> cuestionario_oferta = db.Database.SqlQuery<vista_cuestionario_ofertas>("exec sp_obtener_preguntas_cuestionarios @id_cuestionario = {0}", id).ToList();

                foreach (var item in cuestionario_oferta)

                {
                    List<vista_respuesta_multiple_pregunta> respuesta_pregunta = db.Database.SqlQuery<vista_respuesta_multiple_pregunta>("exec sp_obtener_preguntas_respuestas_cuestionario @id_pregunta = {0}", item.id_pregunta).ToList();

                    item.vista_respuesta_multiple_pregunta = respuesta_pregunta;

                }






                if (cuestionario_oferta.Count > 0)
                {

                    return Json(new { success = true, cuestionario_o = cuestionario_oferta }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, cuestionario_o = cuestionario_oferta }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public PartialViewResult vista_detalle_documentos_curriculum(int? id)
        {

            return PartialView("oferta_documento/vista_detalle_documentos_curriculum");
        }
        ////////////////////


        string a1;
        string a2;
        string b1;
        string b2;
        [CustomAuthorize]
        public ActionResult create_postulacion(List<respuesta_cuestionario_multiple> respuesta_cuestionario_multiple, datos_oferta datos_oferta)
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                int id_persona = Convert.ToInt32(Session["persona_id"]);

                if (datos_oferta.id_cuestionario != 0)
                {
                    foreach (var item in respuesta_cuestionario_multiple)

                    {
                        if (item.id_tipo_pregunta == 1)
                        {
                            if (item.id_respuesta == null)
                            {
                                return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (a1 != null)
                                { a1 = String.Concat(a1, " (zxc1) ", item.id_pregunta); }
                                else
                                {
                                    a1 = item.id_pregunta.ToString();
                                }
                                if (a2 != null)
                                { a2 = String.Concat(a2, " (zxc1) ", item.id_respuesta); }
                                else
                                {
                                    a2 = item.id_respuesta;
                                }

                            }
                        }
                        if (item.id_tipo_pregunta == 0)
                        {
                            if (item.respuesta_pregunta == null)
                            {
                                return Json(new { success = false, responseText = "error plox" }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (b1 != null)
                                { b1 = String.Concat(b1, " (zxc1) ", item.id_pregunta); }
                                else
                                {
                                    b1 = item.id_pregunta.ToString();
                                }
                                if (b2 != null)
                                { b2 = String.Concat(b2, " (zxc1) ", item.respuesta_pregunta); }
                                else
                                {
                                    b2 = item.respuesta_pregunta;
                                }
                            }

                        }
                    }

                }

                int bol = db.Database.SqlQuery<int>("select id_postulacion from postulaciones where id_persona = {0} and id_oferta = {1}", id_persona, datos_oferta.id_oferta).Count();
                if (bol == 0)
                {
                    db.Database.ExecuteSqlCommand("Exec sp_inserta_postulacion_cuestionario @id_persona = {0}, @id_oferta  = {1}, @id_cuestionario ={2} , @ids_pregunta_1 = {3} , @ids_respuestas_1 = {4} , @ids_pregunta_2 = {5} , @texto_respuesta_2 = {6} ", id_persona, datos_oferta.id_oferta, datos_oferta.id_cuestionario, a1, a2, b1, b2);

                    var k = db.personas.Find(id_persona);
                  //  m.enviar_correo(null, k.correo_electronico_persona, 2);
                    return Json(new { success = true, responseText = "Te postulaste exitosamente" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "ya estabas postulado para esta oferta" }, JsonRequestBehavior.AllowGet);
                }




            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }



    }
}