using CainAbel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CainAbel.Util
{
    public class UpdaterTable
    {
         readonly TablesConection tables_conection;
         
        public UpdaterTable(TablesConection tb)

        {
            tables_conection = tb;
        }
        public void updateDb(InfoUserModel iu, string photo,string email)
        {
            
            InfoUserModel info = tables_conection.InfoUser.SingleOrDefault(x => x.Email ==  email);
            if (info != null && iu != null)
            {

                info.Adress = iu.Adress;
                info.Phone = iu.Phone;
                info.FullName = iu.FullName;
                info.AboutMe = iu.AboutMe;
                if (photo != "")
                    info.Photo = photo;

                tables_conection.SaveChanges();

            }

        }
        public void updateRnak(string rating) {

            String[] strlist = rating.Split('-');
            int score = Int32.Parse(strlist[0]);
            int id = Int32.Parse(strlist[1]);
            var record = tables_conection.UserVideosTable.SingleOrDefault(x => x.Id == id);
            record.Ranking = (record.Ranking + score) / 2;
            record.Votes += 1;
            tables_conection.SaveChanges();
            
        }
        public void updateVideo(string title,string type,string video,string email)
        {
            tables_conection.UserVideosTable.Add(new UserVideos { Email = email, Video = video,Title=title,Type=type });
            tables_conection.SaveChanges();
            //Path.Combine("Videos", Path.GetFileName(file.FileName))
        }
       
    }
    }

