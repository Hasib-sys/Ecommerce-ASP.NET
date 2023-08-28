using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceWebsite.Models;

namespace EcommerceWebsite.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult RoleList()
        {
            var roleList = context.Roles.ToList();
            return View(roleList);
        }

        // GET: Role
        public ActionResult CreateRole()
        {
            var role = new IdentityRole();
            return View(role);
        }

        [HttpPost]
        public ActionResult CreateRole(IdentityRole identity)
        {
            context.Roles.Add(identity);
            context.SaveChanges();
            return View();
        }
    }
}