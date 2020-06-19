using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CainAbel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Security.Principal;
using Microsoft.CodeAnalysis.Differencing;
using CainAbel.Util;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CainAbel.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly TablesConection tables_conection;
        private readonly IWebHostEnvironment env;
        private readonly UpdaterTable updater;
        private readonly Uploader uploader;


        // contructor
        public  MyProfileController( TablesConection tablesConection,IWebHostEnvironment environment)
        {
            env = environment;
            tables_conection = tablesConection;
            updater = new UpdaterTable(tablesConection);
             uploader= new Uploader(env, tablesConection);
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserEmail") == null)
                return RedirectToAction("InvalidPage", "Home");
            
            InfoUserModel profile_informations = tables_conection.InfoUser.SingleOrDefault(x => x.Email == HttpContext.Session.GetString("UserEmail"));

            var listVideo = tables_conection.UserVideosTable.Where(x => x.Email == HttpContext.Session.GetString("UserEmail")).ToList();
                ViewBag.listofRecords = listVideo;
           
             return View(profile_informations);
        }
       
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile ifile,InfoUserModel iu) {
            string email = HttpContext.Session.GetString("UserEmail");
            if (ifile != null)
            {
                string extension = Path.GetExtension(ifile.FileName);

                if (extension == ".jpg" || extension == ".png" || extension == ".JPG" || extension == ".PNG")
                {
                    string absolute_path = Path.Combine(env.WebRootPath, "Images", Path.GetFileName(ifile.FileName)); //cale blout care fisier

                    var stream = new FileStream(absolute_path, FileMode.Create); // dechidem un flux  catre acaea cale si creem acolo
                    await ifile.CopyToAsync(stream);

                }
                 
            }
            if(ifile==null)
            {              
                 new UpdaterTable(tables_conection).updateDb(iu, "",email);
            }
            else {
             new UpdaterTable(tables_conection).updateDb(iu, Path.Combine("Images", Path.GetFileName(ifile.FileName)),email);  
            }
            return RedirectToAction("Index","MyProfile");
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 409715200)]
        [RequestSizeLimit(409715200)]
        public IActionResult UpdateVideo(IFormFile file,string title,string type)
        {
           string email= HttpContext.Session.GetString("UserEmail");
            
             uploader.UploadVideo(file);
             updater.updateVideo(title, type, Path.Combine("Videos", Path.GetFileName(file.FileName)), email);
            return RedirectToAction("Index");
        }

       [HttpPost]
        public IActionResult UpdateRating( string rating ) {
            if (rating == null)  return RedirectToAction("Index"); 
            updater.updateRnak(rating);
          
           return RedirectToAction("Index");
        }
        

        public IActionResult DeleteRecord(string id)
        {
            if (id != null)
            uploader.deleteRecord(id);
            return RedirectToAction("Index");

        }

    }
}