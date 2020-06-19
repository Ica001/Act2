using CainAbel.Models;
using CainAbel.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CainAbel.Controllers
{
    public class RegisterController : Controller
    {
        private readonly TablesConection _cs;
       
        public RegisterController(TablesConection cs)
        {
            _cs = cs;
           
        }
        public IActionResult Index()
        {
            return View("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User u)
        {        
            Uniqe check = new Uniqe(_cs);

            if (check.isUnique(u) == 3)
            {
                _cs.UserTable.Add(new User { Email = u.Email, Password = Encoder.Md5_encrypt(u.Password), Username = u.Username });           
                _cs.SaveChanges();
                _cs.InfoUser.Add(new InfoUserModel(u.Email, "", "", "", "", ""));
                _cs.SaveChanges();

            }
            else if (check.isUnique(u) == 1)
                ViewBag.Message = "Already exist this username";
            else if (check.isUnique(u) == 2)
                ViewBag.Message = "Already exist an user with this email";
            
                return View();
            
        }

       

    }
}