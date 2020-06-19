using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CainAbel.Models
{
    public class InfoUserModel
    {
        [Key]
        public string Email { get; set; }

        public string? Adress { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? AboutMe { get; set; }
        public string? Photo { get; set; }
       
        public InfoUserModel(string email,string Adress,string fullname,string phone,string aboutme,string photo)
        {
           
            Email = email;
            this.Adress = Adress;
            FullName = fullname;
            Phone = phone;
            AboutMe = aboutme;
            Photo = photo;
        }
        public InfoUserModel() { }
    }
}
