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
    public class PartialsController : Controller
    {
        public PartialViewResult Realms()
        {
            List<Realm> realms = new List<Realm>();
            if (User.Identity.IsAuthenticated)
            {
                using (var context = new ApplicationDbContext())
                {
                    realms = context.Realms.Where(m => m.User.UserName.Equals(User.Identity.Name))
                        .Include(m => m.Todos)
                        .OrderBy(m => m.Name)
                        .ToList();
                }
            }

            return PartialView("_Realms", realms);
        }

        public PartialViewResult Score()
        {
            using (var context = new ApplicationDbContext())
            {
                var score = "0";
                if (User.Identity.IsAuthenticated)
                {
                    score = context.Users.First(m => m.UserName.Equals(User.Identity.Name))
                        .Score.ToString();
                }

                return PartialView("_Score", score);
            }
        }

        public PartialViewResult RealmsDropDown()
        {
            List<Realm> realms = new List<Realm>();
            if (User.Identity.IsAuthenticated)
            {
                using (var context = new ApplicationDbContext())
                {
                    realms = context.Realms.Where(m => m.User.UserName.Equals(User.Identity.Name))
                        .Include(m => m.Todos)
                        .OrderBy(m => m.Name)
                        .ToList();
                }
            }

            return PartialView("_RealmsDropDown", realms);
        }

        public PartialViewResult AccountInfo()
        {
            string name = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                using (var context = new ApplicationDbContext())
                {
                    name = context.Users.First(m => m.UserName.Equals(User.Identity.Name)).Name;
                }
            }

            return PartialView("_AccountInfo", name);
        }
    }
}