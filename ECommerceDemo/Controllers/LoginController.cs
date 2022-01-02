using ECommerceDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceDemo.Controllers
{
    public class LoginController : Controller
    {
        private readonly SHOPContext _context;

        public LoginController(SHOPContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void SignIn(string email, string password)
        {

        }

        [HttpPost]
        public IActionResult Register(string email, string password,string confirmPassword)
        {
            var user = _context.Users.FirstOrDefault(i => i.Email == email);
            if(user != null)
            {
                ViewBag.RegisterError = "Email Daha Önce Kayıt edilmiştir.";
                
            }
            user = new User {
                Email = email,
                Password = password         
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            user = _context.Users.FirstOrDefault(i => i.Email == email);
            var customerRole = _context.UserRoles.FirstOrDefault(i => i.SystemName == "Customer");
            _context.UserUserRoleMappings.Add(new UserUserRoleMapping { 
                UserId = user.Id,
                UserRoleId=customerRole.Id
            });
            _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
