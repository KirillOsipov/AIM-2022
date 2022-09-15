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
    public class МедицинскийПерсоналController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: МедицинскийПерсонал
        public async Task<ActionResult> Index()
        {
            var медицинскийПерсонал = db.МедицинскийПерсонал.Include(м => м.Гражданин).Include(м => м.Кабинеты);
            return View(await медицинскийПерсонал.ToListAsync());
        }

        public async Task<ActionResult> IndexU()
        {
            var медицинскийПерсонал = db.МедицинскийПерсонал.Include(м => м.Гражданин).Include(м => м.Кабинеты);
            return View(await медицинскийПерсонал.ToListAsync());
        }

        // GET: МедицинскийПерсонал/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            МедицинскийПерсонал медицинскийПерсонал = await db.МедицинскийПерсонал.FindAsync(id);
            if (медицинскийПерсонал == null)
            {
                return HttpNotFound();
            }
            return View(медицинскийПерсонал);
        }

        // GET: МедицинскийПерсонал/Create
        public ActionResult Create()
        {
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта");
            ViewBag.НомерКабинета = new SelectList(db.Кабинеты, "НомерКабинета", "НомерКабинета");
            return View();
        }

        // POST: МедицинскийПерсонал/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ИД,ДатаПринятияНаРаботу,Должность,Квалификация,Зарплата,НомерКабинета")] МедицинскийПерсонал медицинскийПерсонал)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.МедицинскийПерсонал.Add(медицинскийПерсонал);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании.";
                    return View("Error");
                }
            }

            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", медицинскийПерсонал.ИД);
            ViewBag.НомерКабинета = new SelectList(db.Кабинеты, "НомерКабинета", "НомерКабинета", медицинскийПерсонал.НомерКабинета);
            return View(медицинскийПерсонал);
        }

        // GET: МедицинскийПерсонал/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            МедицинскийПерсонал медицинскийПерсонал = await db.МедицинскийПерсонал.FindAsync(id);
            if (медицинскийПерсонал == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", медицинскийПерсонал.ИД);
            ViewBag.НомерКабинета = new SelectList(db.Кабинеты, "НомерКабинета", "НомерКабинета", медицинскийПерсонал.НомерКабинета);
            return View(медицинскийПерсонал);
        }

        // POST: МедицинскийПерсонал/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ИД,ДатаПринятияНаРаботу,Должность,Квалификация,Зарплата,НомерКабинета")] МедицинскийПерсонал медицинскийПерсонал)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(медицинскийПерсонал).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", медицинскийПерсонал.ИД);
            ViewBag.НомерКабинета = new SelectList(db.Кабинеты, "НомерКабинета", "НомерКабинета", медицинскийПерсонал.НомерКабинета);
            return View(медицинскийПерсонал);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            МедицинскийПерсонал медицинскийПерсонал = await db.МедицинскийПерсонал.FindAsync(id);
            if (медицинскийПерсонал == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", медицинскийПерсонал.ИД);
            ViewBag.НомерКабинета = new SelectList(db.Кабинеты, "НомерКабинета", "НомерКабинета", медицинскийПерсонал.НомерКабинета);
            return View(медицинскийПерсонал);
        }

        // POST: МедицинскийПерсонал/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "ИД,ДатаПринятияНаРаботу,Должность,Квалификация,Зарплата,НомерКабинета")] МедицинскийПерсонал медицинскийПерсонал)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(медицинскийПерсонал).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", медицинскийПерсонал.ИД);
            ViewBag.НомерКабинета = new SelectList(db.Кабинеты, "НомерКабинета", "НомерКабинета", медицинскийПерсонал.НомерКабинета);
            return View(медицинскийПерсонал);
        }

        // GET: МедицинскийПерсонал/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            МедицинскийПерсонал медицинскийПерсонал = await db.МедицинскийПерсонал.FindAsync(id);
            if (медицинскийПерсонал == null)
            {
                return HttpNotFound();
            }
            return View(медицинскийПерсонал);
        }

        // POST: МедицинскийПерсонал/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                МедицинскийПерсонал медицинскийПерсонал = await db.МедицинскийПерсонал.FindAsync(id);
                db.МедицинскийПерсонал.Remove(медицинскийПерсонал);
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
