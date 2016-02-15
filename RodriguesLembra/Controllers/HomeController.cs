using RodriguesLembra.DAL;
using RodriguesLembra.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RodriguesLembra.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Todo> tasks = new List<Todo>();
            using (var context = new ApplicationDbContext())
            {
                var userId = context.Users.First(m => m.UserName.Equals(User.Identity.Name)).Id;

                tasks = context.Todos.Where(m => m.Realm.UserID.Equals(userId))
                    .Include(m => m.Realm)
                    .OrderBy(m => m.DueDate)
                    .ThenBy(m => m.Title)
                    .Take(20)
                    .ToList();
            }

            return View(tasks);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
