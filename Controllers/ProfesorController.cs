using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prueba_Colegios.Models;

namespace Prueba_Colegios.Controllers
{
    public class ProfesorController : Controller
    {
        private Prueba_ColegiosEntities db = new Prueba_ColegiosEntities();


        // GET: Profesor/Details/5
        public ActionResult Details()
        {

            string Usuario = (string)TempData["Usuario"];
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            var IdP = db.Profesor.SingleOrDefault(p => p.Usuario == Usuario);
            if (IdP == null)
            {
                return HttpNotFound("No se encontró un estudiante con el usuario proporcionado.");
            }

            Profesor profesor = db.Profesor.Find(IdP.Id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);

        }

        // GET: Profesor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesor.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        // POST: Profesor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Usuario,Contraseña,Telefono")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View(profesor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
