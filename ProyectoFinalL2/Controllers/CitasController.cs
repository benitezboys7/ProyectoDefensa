using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinalL2.Models;

namespace ProyectoFinalL2.Controllers
{
    public class CitasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Citas

       
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Index()
        {
            var citas = db.Citas.Include(c => c.Clientes).Include(c => c.Mascotas);
            return View(await citas.ToListAsync());
        }

        // GET: Citas/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = await db.Citas.FindAsync(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // GET: Citas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre");
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CitasID,ClienteID,MascotasID,FechaReserva")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                db.Citas.Add(citas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", citas.ClienteID);
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", citas.MascotasID);
            return View(citas);
        }

        // GET: Citas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = await db.Citas.FindAsync(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", citas.ClienteID);
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", citas.MascotasID);
            return View(citas);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "CitasID,ClienteID,MascotasID,FechaReserva")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nombre", citas.ClienteID);
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", citas.MascotasID);
            return View(citas);
        }

        // GET: Citas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = await db.Citas.FindAsync(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Citas citas = await db.Citas.FindAsync(id);
            db.Citas.Remove(citas);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
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
