using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FertilizantesV1.Models;

namespace FertilizantesV1.Controllers
{
    [Authorize]
    public class paqueteriasController : Controller
    {
        private contextTienda db = new contextTienda();

        // GET: paqueterias
        public ActionResult Index()
        {
            return View(db.paqueteria.ToList());
        }

        // GET: paqueterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paqueteria paqueteria = db.paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // GET: paqueterias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: paqueterias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_paqueteria,nombre,rfc,tel,web,direccion,contacto,tel_contacto")] paqueteria paqueteria)
        {
            if (ModelState.IsValid)
            {
                db.paqueteria.Add(paqueteria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paqueteria);
        }

        // GET: paqueterias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paqueteria paqueteria = db.paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // POST: paqueterias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_paqueteria,nombre,rfc,tel,web,direccion,contacto,tel_contacto")] paqueteria paqueteria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paqueteria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paqueteria);
        }

        // GET: paqueterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paqueteria paqueteria = db.paqueteria.Find(id);
            if (paqueteria == null)
            {
                return HttpNotFound();
            }
            return View(paqueteria);
        }

        // POST: paqueterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paqueteria paqueteria = db.paqueteria.Find(id);
            db.paqueteria.Remove(paqueteria);
            db.SaveChanges();
            return RedirectToAction("Index");
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
