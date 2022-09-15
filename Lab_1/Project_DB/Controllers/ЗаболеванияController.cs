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
using System.Diagnostics;

namespace OsipovCourseWork.Controllers
{
    public class ЗаболеванияController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: Заболевания
        public async Task<ActionResult> Index(string incubLeft, string incubRight, string diseaseName, 
                                              string oldDisease, string newDisease)
        {
            var заболевания = db.Заболевания.Include(з => з.Пациенты);
            try
            {
                if (!string.IsNullOrEmpty(diseaseName))
                {
                    заболевания = заболевания.Where(з => з.Название == diseaseName);
                }

                if (!string.IsNullOrEmpty(incubLeft) && !string.IsNullOrEmpty(incubRight))
                {
                    int left = int.Parse(incubLeft);
                    int right = int.Parse(incubRight);
                    заболевания = заболевания.Where(з => з.ИнкубационныйПериод >= left && з.ИнкубационныйПериод <= right);
                }

                if (!string.IsNullOrEmpty(oldDisease) && !string.IsNullOrEmpty(newDisease))
                {
                    using (CourseworkOsipovEntities db = new CourseworkOsipovEntities())
                    {
                        System.Data.SqlClient.SqlParameter[] parameters = new System.Data.SqlClient.SqlParameter[]
                        {
                            new System.Data.SqlClient.SqlParameter("@oldDisease", oldDisease),
                            new System.Data.SqlClient.SqlParameter("@newDisease", newDisease)
                        };
                        db.Database.ExecuteSqlCommand("ModifyDisease @oldDisease, @newDisease", parameters[0], parameters[1]);
                    }
                }
            }
            catch
            {
                ViewBag.Error = "Ошибка! Неправильно заполнены поля поиска, фильтрации или аргументы процедуры.";
                return View("Error");
            }

            return View(await заболевания.ToListAsync());
        }

        public async Task<ActionResult> IndexU(string incubLeft, string incubRight, string diseaseName)
        {
            var заболевания = db.Заболевания.Include(з => з.Пациенты);
            try
            {
                if (!string.IsNullOrEmpty(diseaseName))
                {
                    заболевания = заболевания.Where(з => з.Название == diseaseName);
                }

                if (!string.IsNullOrEmpty(incubLeft) && !string.IsNullOrEmpty(incubRight))
                {
                    int left = int.Parse(incubLeft);
                    int right = int.Parse(incubRight);
                    заболевания = заболевания.Where(з => з.ИнкубационныйПериод >= left && з.ИнкубационныйПериод <= right);
                }
            }
            catch
            {
                ViewBag.Error = "Ошибка! Неправильно заполнены поля поиска, фильтрации или аргументы процедуры.";
                return View("Error");
            }
            return View(await заболевания.ToListAsync());
        }

        public async Task<ActionResult> IndexG(string incubLeft, string incubRight, string diseaseName)
        {
            var заболевания = db.Заболевания.Include(з => з.Пациенты);
            try
            {
                if (!string.IsNullOrEmpty(diseaseName))
                {
                    заболевания = заболевания.Where(з => з.Название == diseaseName);
                }

                if (!string.IsNullOrEmpty(incubLeft) && !string.IsNullOrEmpty(incubRight))
                {
                    int left = int.Parse(incubLeft);
                    int right = int.Parse(incubRight);
                    заболевания = заболевания.Where(з => з.ИнкубационныйПериод >= left && з.ИнкубационныйПериод <= right);
                }
            }
            catch
            {
                ViewBag.Error = "Ошибка! Неправильно заполнены поля поиска, фильтрации или аргументы процедуры.";
                return View("Error");
            }
            return View(await заболевания.ToListAsync());
        }

        // GET: Заболевания/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заболевания заболевания = await db.Заболевания.FindAsync(id);
            if (заболевания == null)
            {
                return HttpNotFound();
            }
            return View(заболевания);
        }

        // GET: Заболевания/Create
        public ActionResult Create()
        {
            Заболевания заболевания = new Заболевания { ТипЗаболевания = "вирус" };
            return View(заболевания);
        }

        // POST: Заболевания/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Название,ТипЗаболевания,Возбудитель,МКБ_10,Смертность,Воздействие,Симптомы,СпособПередачи,ИнкубационныйПериод")] Заболевания заболевания)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Заболевания.Add(заболевания);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании."; 
                    return View("Error"); 
                }
            }

            return View(заболевания);
        }

        // GET: Заболевания/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заболевания заболевания = await db.Заболевания.FindAsync(id);
            if (заболевания == null)
            {
                return HttpNotFound();
            }
            return View(заболевания);
        }

        // POST: Заболевания/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Название,ТипЗаболевания,Возбудитель,МКБ_10,Смертность,Воздействие,Симптомы,СпособПередачи,ИнкубационныйПериод")] Заболевания заболевания)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(заболевания).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении."; 
                    return View("Error"); 
                }
            }
            return View(заболевания);
        }

        public async Task<ActionResult> EditU(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заболевания заболевания = await db.Заболевания.FindAsync(id);
            if (заболевания == null)
            {
                return HttpNotFound();
            }
            return View(заболевания);
        }

        // POST: Заболевания/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "Название,ТипЗаболевания,Возбудитель,МКБ_10,Смертность,Воздействие,Симптомы,СпособПередачи,ИнкубационныйПериод")] Заболевания заболевания)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(заболевания).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            return View(заболевания);
        }

        // GET: Заболевания/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заболевания заболевания = await db.Заболевания.FindAsync(id);
            if (заболевания == null)
            {
                return HttpNotFound();
            }
            return View(заболевания);
        }

        // POST: Заболевания/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {
                Заболевания заболевания = await db.Заболевания.FindAsync(id);
                db.Заболевания.Remove(заболевания);
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
