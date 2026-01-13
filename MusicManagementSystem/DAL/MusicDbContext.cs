using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MusicManagementSystem.Models;

namespace MusicManagementSystem.DAL
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext() : base("name=MusicDbContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MusicDbContext>());
        }

        public DbSet<Music> Musics { get; set; }
    }
}