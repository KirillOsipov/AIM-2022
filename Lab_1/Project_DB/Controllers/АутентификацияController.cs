using OsipovCourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace OsipovCourseWork.Controllers
{
    public enum UserType { Admin, User, Guest }

    public class АутентификацияController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        [HttpGet]
        public ActionResult Login()
        {
            Session["Access"] = UserType.Guest;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Аутентификация user = null;
                string patternAdminAccount = @"\w+_admin";
                try
                {
                    user = db.Аутентификация.First(u => u.Login == model.Login && u.Password == model.Password); // поиск первого пользователя из базы, чей логин и пароль совпадают с введенным в форму
                }
                catch
                {
                    ModelState.AddModelError("", "Пользователь с таким логином не зарегистрирован или введен неверный пароль.");
                }

                if (user != null && user.Password == model.Password && Regex.IsMatch(user.Login, patternAdminAccount))
                {
                    Session["Access"] = UserType.Admin;
                    return RedirectToAction("Index", "Home");
                }
                else if (user != null && user.Password == model.Password && user.Login == model.Login)
                {
                    Session["Access"] = UserType.User;
                    return RedirectToAction("IndexU", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Аутентификация user = null;
                string patternAdminAccount = @"\w*_admin";
                if (!Regex.IsMatch(model.Login, patternAdminAccount)) // если логин не содержит в себе сочетания "_admin"
                {
                    user = db.Аутентификация.FirstOrDefault(u => u.Login == model.Login); // поиск первого пользователя из базы, чей логин совпадает с введенным в форму

                    if (user == null) // если такого пользователя не нашлось (т.е. введенный при регистрации логин уникален)
                    {
                        db.Аутентификация.Add(new Аутентификация { Login = model.Login, Password = model.Password }); // добавление нового пользователя в базу данных
                        db.SaveChanges(); // сохранение БД
                        user = db.Аутентификация.Where(u => u.Login == model.Login && u.Password == model.Password).FirstOrDefault(); // присвоение переменной user первого пользователя из базы, у которого логин и пароль совпадают с введенными при регистрации

                        if (user != null) // если переменная user не пустая
                        {
                            return RedirectToAction("Login", "Аутентификация"); // перенаправление на страницу входа
                        }
                    }
                    else // иначе вывод ошибки
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже зарегистрирован.");
                    }
                }
                else // иначе вывод ошибки
                {
                    ModelState.AddModelError("", "Нельзя зарегистрировать администраторский аккаунт.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Аутентификация"); // перенаправление на страницу входа
        }
    }
}