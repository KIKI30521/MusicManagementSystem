namespace MusicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Musics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongName = c.String(nullable: false, maxLength: 100),
                        Singer = c.String(nullable: false, maxLength: 50),
                        Album = c.String(maxLength: 100),
                        ReleaseDate = c.DateTime(nullable: false),
                        NeteaseUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Musics");
        }
    }
}
