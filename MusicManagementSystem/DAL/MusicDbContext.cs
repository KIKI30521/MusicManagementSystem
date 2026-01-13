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
        // 关联Web.config中的连接字符串名称
        public MusicDbContext() : base("name=MusicDbContext")
        {
            // 初始化数据库（不存在则创建）
            Database.SetInitializer(new CreateDatabaseIfNotExists<MusicDbContext>());
        }

        // 定义数据集（对应数据库中的Musics表）
        public DbSet<Music> Musics { get; set; }
    }
}