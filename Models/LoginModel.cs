using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace CainAbel.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Enter email !")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
