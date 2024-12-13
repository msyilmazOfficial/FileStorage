using FileStorage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DataAccess
{
    public class FileStorageDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //TODO add connection string
            optionsBuilder.UseSqlServer("Server=DESKTOP-JBRKIJL\\SQLEXPRESS; Database=FileStorage; Integrated Security=True; TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Entities.File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }
}
