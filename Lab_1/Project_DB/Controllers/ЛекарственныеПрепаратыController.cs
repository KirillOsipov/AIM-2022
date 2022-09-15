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
    public class ЛекарственныеПрепаратыController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: ЛекарственныеПрепараты
        public async Task<ActionResult> Index()
        {
            return View(await db.ЛекарственныеПрепараты.ToListAsync());
        }

        public async Task<ActionResult> IndexU()
        {
            return View(await db.ЛекарственныеПрепараты.ToListAsync());
        }

        public async Task<ActionResult> IndexG()
        {
            return View(await db.ЛекарственныеПрепараты.ToListAsync());
        }

        // GET: ЛекарственныеПрепараты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ЛекарственныеПрепараты лекарственныеПрепараты = await db.ЛекарственныеПрепараты.FindAsync(id);
            if (лекарственныеПрепараты == null)
            {
                return HttpNotFound();
            }
            return View(лекарственныеПрепараты);
        }

        // GET: ЛекарственныеПрепараты/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ЛекарственныеПрепараты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ИД_Препарата,НазваниеПрепарата,ПриЗаболевании,КурсПриема,Применение,Количество,Стоимость")] ЛекарственныеПрепараты лекарственныеПрепараты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ЛекарственныеПрепараты.Add(лекарственныеПрепараты);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании.";
                    return View("Error");
                }
            }

            return View(лекарственныеПрепараты);
        }

        // GET: ЛекарственныеПрепараты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ЛекарственныеПрепараты лекарственныеПрепараты = await db.ЛекарственныеПрепараты.FindAsync(id);
            if (лекарственныеПрепараты == null)
            {
                return HttpNotFound();
            }
            return View(лекарственныеПрепараты);
        }

        // POST: ЛекарственныеПрепараты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ИД_Препарата,НазваниеПрепарата,ПриЗаболевании,КурсПриема,Применение,Количество,Стоимость")] ЛекарственныеПрепараты лекарственныеПрепараты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(лекарственныеПрепараты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении."; 
                    return View("Error"); 
                }
            }
            return View(лекарственныеПрепараты);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ЛекарственныеПрепараты лекарственныеПрепараты = await db.ЛекарственныеПрепараты.FindAsync(id);
            if (лекарственныеПрепараты == null)
            {
                return HttpNotFound();
            }
            return View(лекарственныеПрепараты);
        }

        // POST: ЛекарственныеПрепараты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "ИД_Препарата,НазваниеПрепарата,ПриЗаболевании,КурсПриема,Применение,Количество,Стоимость")] ЛекарственныеПрепараты лекарственныеПрепараты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(лекарственныеПрепараты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            return View(лекарственныеПрепараты);
        }

        // GET: ЛекарственныеПрепараты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ЛекарственныеПрепараты лекарственныеПрепараты = await db.ЛекарственныеПрепараты.FindAsync(id);
            if (лекарственныеПрепараты == null)
            {
                return HttpNotFound();
            }
            return View(лекарственныеПрепараты);
        }

        // POST: ЛекарственныеПрепараты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                ЛекарственныеПрепараты лекарственныеПрепараты = await db.ЛекарственныеПрепараты.FindAsync(id);
                db.ЛекарственныеПрепараты.Remove(лекарственныеПрепараты);
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
