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
using PagedList;
using PagedList.Mvc;



namespace ProyectoFinalL2.Controllers
{
    
    public class ClientesController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var cliente = from s in db.Clientes
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                cliente = cliente.Where(s => s.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    cliente = cliente.OrderByDescending(s => s.Nombre);

                    break;
                default:
                    cliente = cliente.OrderBy(s => s.Nombre);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(cliente.ToPagedList(pageNumber, pageSize));
        }

        //GET: Clientes
       //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Clientes.ToListAsync());
        //}

        // GET: Clientes/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }
        [Authorize(Roles = "Admin")]

        // GET: Clientes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "ClienteID,Nombre,Edad,Cedula,Telefono,Direccion,Tamano,Area,EdadMascota")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ClienteID,Nombre,Edad,Cedula,Telefono,Direccion,Tamano,Area,EdadMascota")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            db.Clientes.Remove(cliente);
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

        public async Task<ActionResult> Sugerir(int id)
        {
            var preferenciasCliente = await db.Clientes.Where(x => x.ClienteID == id).FirstOrDefaultAsync();
            var mascotasSugeridas = await db.Mascotas.Where(x => x.Nombre == "").ToListAsync();
            var mascotas = await db.Mascotas.ToListAsync();



            mascotasSugeridas.AddRange(await db.Mascotas.SqlQuery($"SELECT * FROM Mascotas WHERE Area like '%{preferenciasCliente.Area}%' and Tamano like '%{preferenciasCliente.Tamano}%'").ToListAsync());


            return View(mascotasSugeridas);


            

        }


        public async Task<ActionResult> Rango(int id)
        {
            
            var preferenciasCliente = await db.Clientes.Where(x => x.ClienteID == id).FirstOrDefaultAsync();
            var mascotasSugeridas = await db.Mascotas.Where(x => x.Nombre == "").ToListAsync();
            var mascotas = await db.Mascotas.ToListAsync();
            mascotasSugeridas.AddRange(await db.Mascotas.SqlQuery($"SELECT * FROM Mascotas WHERE  Area  like'%{150}%' or Area like'%{300}%'").ToListAsync());


            

            return View(mascotasSugeridas);
        }
    }
}
