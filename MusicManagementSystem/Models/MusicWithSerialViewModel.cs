using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicManagementSystem.Models
{
    /// <summary>
    /// 带动态连续序号的歌曲视图模型
    /// </summary>
    public class MusicWithSerialViewModel
    {
        // 动态连续序号（页面展示用）
        public int SerialNumber { get; set; }
        public int Id { get; set; }
        public string SongName { get; set; }
        public string ImageUrl { get; set; }
        public string MusicSourceId { get; set; }
        public string MusicUrl { get; set; }
    }
}