using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.Models
{
    public class Music
    {
        [Key] // 主键，标记该字段为数据库表的主键
        public int Id { get; set; }

        [Required(ErrorMessage = "歌曲名称不能为空")]
        [Display(Name = "歌曲名称")]
        [StringLength(100, ErrorMessage = "名称长度不能超过100个字符")]
        public string SongName { get; set; }

        [Required(ErrorMessage = "歌手不能为空")]
        [Display(Name = "歌手")]
        [StringLength(50)]
        public string Singer { get; set; }

        [Display(Name = "专辑")]
        [StringLength(100)]
        public string Album { get; set; }

        [Display(Name = "发布时间")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "网易云音乐链接")]
        [Url(ErrorMessage = "请输入有效的URL")]
        public string NeteaseUrl { get; set; }
    }
}