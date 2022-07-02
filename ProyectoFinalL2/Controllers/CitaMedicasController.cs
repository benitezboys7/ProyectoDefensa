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
    public class CitaMedicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CitaMedicas
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var citaMedicas = db.CitaMedicas.Include(c => c.Mascotas).Include(c => c.Veterinarios);
            return View(await citaMedicas.ToListAsync());
        }

        // GET: CitaMedicas/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitaMedica citaMedica = await db.CitaMedicas.FindAsync(id);
            if (citaMedica == null)
            {
                return HttpNotFound();
            }
            return View(citaMedica);
        }

        // GET: CitaMedicas/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre");
            ViewBag.VeterinarioID = new SelectList(db.Veterinarios, "VeterinarioID", "Nombre");
            return View();
        }

        // POST: CitaMedicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "CitaMedicaID,VeterinarioID,MascotasID,FechaReserva,Hora")] CitaMedica citaMedica)
        {
            if (ModelState.IsValid)
            {
                db.CitaMedicas.Add(citaMedica);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", citaMedica.MascotasID);
            ViewBag.VeterinarioID = new SelectList(db.Veterinarios, "VeterinarioID", "Nombre", citaMedica.VeterinarioID);
            return View(citaMedica);
        }

        // GET: CitaMedicas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitaMedica citaMedica = await db.CitaMedicas.FindAsync(id);
            if (citaMedica == null)
            {
                return HttpNotFound();
            }
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", citaMedica.MascotasID);
            ViewBag.VeterinarioID = new SelectList(db.Veterinarios, "VeterinarioID", "Nombre", citaMedica.VeterinarioID);
            return View(citaMedica);
        }

        // POST: CitaMedicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "CitaMedicaID,VeterinarioID,MascotasID,FechaReserva,Hora")] CitaMedica citaMedica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citaMedica).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", citaMedica.MascotasID);
            ViewBag.VeterinarioID = new SelectList(db.Veterinarios, "VeterinarioID", "Nombre", citaMedica.VeterinarioID);
            return View(citaMedica);
        }

        // GET: CitaMedicas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CitaMedica citaMedica = await db.CitaMedicas.FindAsync(id);
            if (citaMedica == null)
            {
                return HttpNotFound();
            }
            return View(citaMedica);
        }

        // POST: CitaMedicas/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CitaMedica citaMedica = await db.CitaMedicas.FindAsync(id);
            db.CitaMedicas.Remove(citaMedica);
            await db.SaveChangesAsync();
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
