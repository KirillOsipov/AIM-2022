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
    public class ПалатыController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: Палаты
        public async Task<ActionResult> Index()
        {
            var палаты = db.Палаты.Include(п => п.МедицинскийПерсонал);
            return View(await палаты.ToListAsync());
        }

        public async Task<ActionResult> IndexU()
        {
            var палаты = db.Палаты.Include(п => п.МедицинскийПерсонал);
            return View(await палаты.ToListAsync());
        }

        public async Task<ActionResult> IndexG()
        {
            var палаты = db.Палаты.Include(п => п.МедицинскийПерсонал);
            return View(await палаты.ToListAsync());
        }

        // GET: Палаты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Палаты палаты = await db.Палаты.FindAsync(id);
            if (палаты == null)
            {
                return HttpNotFound();
            }
            return View(палаты);
        }

        // GET: Палаты/Create
        public ActionResult Create()
        {
            ViewBag.ИД_Персонала = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность");
            return View();
        }

        // POST: Палаты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "НомерПалаты,КоличествоПациентов,КоличествоМест,Этаж,ИД_Персонала")] Палаты палаты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Палаты.Add(палаты);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании.";
                    return View("Error");
                }
            }

            ViewBag.ИД_Персонала = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", палаты.ИД_Персонала);
            return View(палаты);
        }

        // GET: Палаты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Палаты палаты = await db.Палаты.FindAsync(id);
            if (палаты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_Персонала = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", палаты.ИД_Персонала);
            return View(палаты);
        }

        // POST: Палаты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "НомерПалаты,КоличествоПациентов,КоличествоМест,Этаж,ИД_Персонала")] Палаты палаты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(палаты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД_Персонала = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", палаты.ИД_Персонала);
            return View(палаты);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Палаты палаты = await db.Палаты.FindAsync(id);
            if (палаты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_Персонала = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", палаты.ИД_Персонала);
            return View(палаты);
        }

        // POST: Палаты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "НомерПалаты,КоличествоПациентов,КоличествоМест,Этаж,ИД_Персонала")] Палаты палаты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(палаты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД_Персонала = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", палаты.ИД_Персонала);
            return View(палаты);
        }

        // GET: Палаты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Палаты палаты = await db.Палаты.FindAsync(id);
            if (палаты == null)
            {
                return HttpNotFound();
            }
            return View(палаты);
        }

        // POST: Палаты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Палаты палаты = await db.Палаты.FindAsync(id);
                db.Палаты.Remove(палаты);
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
