using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_proekt.Models;

namespace Web_rekuperator.Models
{
    public class UserContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Model> models { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            /*models.Add(new Model());
            SaveChanges();*/
        }
    
    }
}
