using RodriguesLembra.DAL;
using RodriguesLembra.Models;
using RodriguesLembra.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RodriguesLembra.Controllers
{
    [Authorize]
    public class TodosController : Controller
    {
        // GET: Todos/Create
        public ActionResult Create(Guid realmId)
        {
            CreateTodoViewModel model = new CreateTodoViewModel()
            {
                RealmID = realmId
            };

            return View(model);
        }

        // POST: Todos/Create
        [HttpPost]
        public ActionResult Create(CreateTodoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        Todo todo = new Todo
                        {
                            Title = model.Title,
                            Description = model.Description,
                            DueDate = model.DueDate,
                            IsDone = false,
                            RealmID = model.RealmID
                        };

                        context.Todos.Add(todo);
                        var response = context.SaveChanges();

                        if (response > 0)
                            return RedirectToAction("Details", "Realms", new { id = model.RealmID, success = true });
                    }
                }

                ViewBag.ErrorMessage = "Erro na criação do contexto de tarefas";
                return View(model);
            }
            catch
            {
                ViewBag.ErrorMessage = "Erro na criação do contexto de tarefas";
                return View(model);
            }
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Todos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Todos/Delete/5
        public ActionResult Delete(Guid id)
        {
            using (var context = new ApplicationDbContext())
            {
                var userId = context.Users.First(m => m.UserName.Equals(User.Identity.Name)).Id;

                var todo = context.Todos.Find(id);

                if (todo == null)
                    return RedirectToAction("Index", "Home");

                if (todo.Realm.UserID != userId)
                    return RedirectToAction("Index", "Home");

                context.Todos.Remove(todo);
                var response = context.SaveChanges();

                if (response > 0)
                    return RedirectToAction("Details", "Realms", new { id = todo.RealmID, success = true });
                else
                    return RedirectToAction("Details", "Realms", new { id = todo.RealmID, error = true });
            }
        }

        public ActionResult ToggleStatus(Guid id, bool fromHomePage = false)
        {
            using (var context = new ApplicationDbContext())
            {
                var todo = context.Todos.Find(id);
                var user = context.Users.First(m => m.UserName.Equals(User.Identity.Name));

                if (todo == null)
                    return RedirectToAction("Index", "Home");
                if (user == null)
                    return RedirectToAction("Index", "Home");

                todo.IsDone = !todo.IsDone;

                if (todo.IsDone)
                    user.Score += 10;
                else
                    user.Score -= 10;

                context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                var response = context.SaveChanges();

                if (fromHomePage)
                    return RedirectToAction("Index", "Home");

                if (response > 0)
                    return RedirectToAction("Details", "Realms", new { id = todo.RealmID, success = true });
                else
                    return RedirectToAction("Details", "Realms", new { id = todo.RealmID, error = true });
            }
        }
    }
}
