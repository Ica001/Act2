using CainAbel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CainAbel.Util
{
    public class Uploader
    {
        public readonly TablesConection tables_conection;
        public readonly IWebHostEnvironment env;
        public Uploader(IWebHostEnvironment env,TablesConection tables_conection)
        {
           this.env = env;
           this.tables_conection = tables_conection;
        }
        
        public async Task UploadVideo(IFormFile file)
        {
            if (file.Length > 40971520) file = null;
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);

                if (extension == ".mov" || extension == ".mp4")
                {
                    string absolute_path = Path.Combine(env.WebRootPath, "Videos", Path.GetFileName(file.FileName)); //cale blout care fisier

                    using (FileStream fs = System.IO.File.Create(absolute_path))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                   
                }

            }



        }
        public async Task deleteRecord(string id) {
            int ident = Int32.Parse(id);
            var record = tables_conection.UserVideosTable.SingleOrDefault(x => x.Id == ident);
            string filename = record.Video;
            tables_conection.UserVideosTable.Remove(record);
            tables_conection.SaveChanges();
           
            string absolute_path = Path.Combine(env.WebRootPath,  filename);

              
                System.IO.File.Delete(absolute_path);
            
          

            

        }
    }
}
