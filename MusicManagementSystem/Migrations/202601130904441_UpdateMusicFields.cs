namespace MusicManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMusicFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "ImageUrl", c => c.String());
            AddColumn("dbo.Musics", "MusicUrl", c => c.String());
            AddColumn("dbo.Musics", "MusicSourceId", c => c.String());
            AlterColumn("dbo.Musics", "SongName", c => c.String(nullable: false));
            DropColumn("dbo.Musics", "Singer");
            DropColumn("dbo.Musics", "Album");
            DropColumn("dbo.Musics", "ReleaseDate");
            DropColumn("dbo.Musics", "NeteaseUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Musics", "NeteaseUrl", c => c.String());
            AddColumn("dbo.Musics", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Musics", "Album", c => c.String(maxLength: 100));
            AddColumn("dbo.Musics", "Singer", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Musics", "SongName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Musics", "MusicSourceId");
            DropColumn("dbo.Musics", "MusicUrl");
            DropColumn("dbo.Musics", "ImageUrl");
        }
    }
}
