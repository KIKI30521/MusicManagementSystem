using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.Models
{
    public class Music
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "歌曲名称不能为空")]
        [Display(Name = "歌曲名称")]
        [StringLength(100, ErrorMessage = "名称长度不能超过100个字符")]
        public string SongName { get; set; }

        [Display(Name = "图片链接")]
        [Url(ErrorMessage = "请输入有效的图片链接")]
        public string ImageUrl { get; set; }

        [Display(Name = "音乐源地址")]
        [Url(ErrorMessage = "请输入有效的音乐链接")]
        public string MusicUrl { get; set; }

        [Display(Name = "音乐源ID")]
        public string MusicSourceId { get; set; }
    }
}