using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CainAbel.Models
{
    public class UserVideos
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Video { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public int Ranking { get; set; }
        public int Votes { get; set; }

       
    }
}
