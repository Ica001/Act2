
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CainAbel.Models
{
    public class TablesConection : DbContext
    {
        public DbSet<User> UserTable { get; set; }
        public DbSet<InfoUserModel>InfoUser {get;set;}
        public DbSet<UserVideos> UserVideosTable { get; set; }
        public TablesConection(DbContextOptions<TablesConection> options) : base(options)
        {

        }
    }
}
