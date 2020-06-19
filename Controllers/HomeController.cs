using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CainAbel.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CainAbel.Util;
using Microsoft.AspNetCore.Http;

namespace CainAbel.Controllers
{
    public class HomeController : Controller
    {
       
       private readonly TablesConection table_conections;
        private readonly ILogger<HomeController> _logger;
        private readonly UpdaterTable updater;

        public HomeController(ILogger<HomeController> logger,TablesConection tablesConection)
        {
            _logger = logger;
            table_conections = tablesConection;
            updater = new UpdaterTable(tablesConection);
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("username", "");
            HttpContext.Session.SetInt32("id", 0);

            return View();
        }
        [HttpPost]
        public JsonResult showSugestions(string Prefix)
        {
         
            
            var UserList = (from N in table_conections.UserTable
                            where N.Username.StartsWith(Prefix)
                            select new { N.Username });
            return Json(UserList);
            
        }
       
        public IActionResult SearchInput( string searched)
        {
           
            if (searched != null)   HttpContext.Session.SetString("username", searched);
            if (table_conections.UserTable.Where(x => x.Username == HttpContext.Session.GetString("username")).Count() > 0)
            {


                string email = table_conections.UserTable.SingleOrDefault(x => x.Username == HttpContext.Session.GetString("username")).Email;

                InfoUserModel info = table_conections.InfoUser.SingleOrDefault(x => x.Email == email);
                ViewBag.listofRecords = table_conections.UserVideosTable.Where(x => x.Email == email).ToList();
                return View(info);

            }
            else
            {
                TempData["msg"] = "<script>alert('There is no users with that username.');</script>";
               return RedirectToAction("Index");
            }
          
            
        }
        public IActionResult SeeProfile(string id)
        {
            if (id != null)
            {
                int Id = Int32.Parse(id);
                HttpContext.Session.SetInt32("id", Id);
            }
            
            if (HttpContext.Session.GetInt32("id") == null) { return RedirectToAction("Index"); }

            string email = table_conections.UserVideosTable.SingleOrDefault(x => x.Id == HttpContext.Session.GetInt32("id")).Email;
            InfoUserModel info = table_conections.InfoUser.SingleOrDefault(x => x.Email == email);
            ViewBag.listofRecords = table_conections.UserVideosTable.Where(x => x.Email == email).ToList();
            return View("SearchInput", info);


        }

        [HttpPost]
        public IActionResult UpdateStrangerRating(string rating)
        {
            if (rating == null) return RedirectToAction("Index");
            
            updater.updateRnak(rating);
            if (HttpContext.Session.GetString("username") !="")
                return RedirectToAction("SearchInput");
            else if (HttpContext.Session.GetInt32("id") != 0)
                return RedirectToAction("SeeProfile");
            else return RedirectToAction("showClip");
        }


        public IActionResult showClip(string type)
        {
            if (type != null) HttpContext.Session.SetString("type", type);
            var tip = HttpContext.Session.GetString("type");
            var list_of_comedy_clip = table_conections.UserVideosTable.Where(x => x.Type == tip).ToList();
            ViewBag.list_of_comedy_clip = list_of_comedy_clip;
           
            return View("Index");

            

        }

        public IActionResult InvalidPage() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
