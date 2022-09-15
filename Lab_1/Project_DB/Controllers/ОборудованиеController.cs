using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OsipovCourseWork.Models;

namespace OsipovCourseWork.Controllers
{
    public class ОборудованиеController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: Оборудование
        public async Task<ActionResult> Index()
        {
            var оборудование = db.Оборудование.Include(о => о.Палаты);
            return View(await оборудование.ToListAsync());
        }

        public async Task<ActionResult> IndexU()
        {
            var оборудование = db.Оборудование.Include(о => о.Палаты);
            return View(await оборудование.ToListAsync());
        }

        // GET: Оборудование/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Оборудование оборудование = await db.Оборудование.FindAsync(id);
            if (оборудование == null)
            {
                return HttpNotFound();
            }
            return View(оборудование);
        }

        // GET: Оборудование/Create
        public ActionResult Create()
        {
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты");
            return View();
        }

        // POST: Оборудование/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ИД_Оборудования,Наименование,Количество,НомерПалаты,Применение,Производитель")] Оборудование оборудование)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Оборудование.Add(оборудование);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании."; 
                    return View("Error"); 
                }
            }

            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", оборудование.НомерПалаты);
            return View(оборудование);
        }

        // GET: Оборудование/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Оборудование оборудование = await db.Оборудование.FindAsync(id);
            if (оборудование == null)
            {
                return HttpNotFound();
            }
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", оборудование.НомерПалаты);
            return View(оборудование);
        }

        // POST: Оборудование/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ИД_Оборудования,Наименование,Количество,НомерПалаты,Применение,Производитель")] Оборудование оборудование)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(оборудование).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении."; 
                    return View("Error"); 
                }
            }
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", оборудование.НомерПалаты);
            return View(оборудование);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Оборудование оборудование = await db.Оборудование.FindAsync(id);
            if (оборудование == null)
            {
                return HttpNotFound();
            }
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", оборудование.НомерПалаты);
            return View(оборудование);
        }

        // POST: Оборудование/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "ИД_Оборудования,Наименование,Количество,НомерПалаты,Применение,Производитель")] Оборудование оборудование)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(оборудование).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", оборудование.НомерПалаты);
            return View(оборудование);
        }

        // GET: Оборудование/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Оборудование оборудование = await db.Оборудование.FindAsync(id);
            if (оборудование == null)
            {
                return HttpNotFound();
            }
            return View(оборудование);
        }

        // POST: Оборудование/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Оборудование оборудование = await db.Оборудование.FindAsync(id);
                db.Оборудование.Remove(оборудование);
                await db.SaveChangesAsync();
            }
            catch 
            { 
                ViewBag.Error = "Ошибка! Нарушение ссылочной целостности при удалении."; 
                return View("Error"); 
            }
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
