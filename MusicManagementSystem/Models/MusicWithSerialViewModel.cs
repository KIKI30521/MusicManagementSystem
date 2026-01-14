using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicManagementSystem.Models
{

    public class MusicWithSerialViewModel
    {
        // 动态连续序号
        public int SerialNumber { get; set; }
        public int Id { get; set; }
        public string SongName { get; set; }
        public string ImageUrl { get; set; }
        public string MusicSourceId { get; set; }
        public string MusicUrl { get; set; }
    }
}