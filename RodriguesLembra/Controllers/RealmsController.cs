using RodriguesLembra.DAL;
using RodriguesLembra.Models;
using RodriguesLembra.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RodriguesLembra.Controllers
{
    [Authorize]
    public class RealmsController : Controller
    {
        // GET: Realms
        public ActionResult Index()
        {
            var list = new List<Realm>();
            using (var context = new ApplicationDbContext())
            {
                var userId = context.Users.First(m => m.UserName.Equals(User.Identity.Name)).Id;

                list = context.Realms.Where(m => m.UserID.Equals(userId))
                    .OrderBy(m => m.Name)
                    .Include(m => m.Todos)
                    .ToList();
            }

            return View(list);
        }

        // GET: Realms/Details
        public ActionResult Details(Guid id, bool error = false, bool success = false)
        {
            using (var context = new ApplicationDbContext())
            {
                var userId = context.Users.First(m => m.UserName.Equals(User.Identity.Name)).Id;
                var realm = context.Realms
                    .Include(m => m.Todos)
                    .First(m => m.RealmID.Equals(id));

                if (error)
                    ViewBag.ErrorMessage = "Eita, deu ruim! Liga pra mim que a gente resolve ;)";

                if (success)
                    ViewBag.Message = "Tudo certinho, moça! Mas não esquece de mim :3";

                if (realm.UserID == userId)
                {
                    return View(realm);
                }
                else
                {
                    return View("Index", "Home");
                }
            }
        }

        // GET: Realms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Realms/Create
        [HttpPost]
        public ActionResult Create(CreateRealmViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string userId = string.Empty;
                    using (var context = new ApplicationDbContext())
                    {
                        userId = context.Users.First(m => m.UserName.Equals(User.Identity.Name)).Id;

                        Realm realm = new Realm
                        {
                            Name = model.Name,
                            Description = model.Description,
                            UserID = userId
                        };

                        context.Realms.Add(realm);
                        var response = context.SaveChanges();

                        if (response > 0)
                            return RedirectToAction("Index");
                    }
                }

                ViewBag.ErrorMessage = "Eita, deu ruim! Liga pra mim que a gente resolve ;)";
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "Eita, deu ruim! Liga pra mim que a gente resolve ;)";
                return View(model);
            }
        }

        // GET: Realms/Delete/5
        public ActionResult Delete(Guid id)
        {
            using (var context = new ApplicationDbContext())
            {
                var userId = context.Users.First(m => m.Email.Equals(User.Identity.Name)).Id;
                var realm = context.Realms.Find(id);

                if (realm == null)
                    return RedirectToAction("Index");

                if (realm.UserID != userId)
                    return RedirectToAction("Index");

                return View(realm);
            }
        }

        // POST: Realms/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            using (var context = new ApplicationDbContext())
            {
                Realm realm = context.Realms.Find(id);
                try
                {
                    if (ModelState.IsValid)
                    {
                        context.Realms.Remove(realm);
                        var response = context.SaveChanges();

                        if (response > 0)
                            return RedirectToAction("Index");
                    }

                    ViewBag.ErrorMessage = "Eita, deu ruim! Liga pra mim que a gente resolve ;)";
                    return View(realm);
                }
                catch
                {
                    ViewBag.ErrorMessage = "Eita, deu ruim! Liga pra mim que a gente resolve ;)";
                    return View(realm);
                }
            }
        }
    }
}
