using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入账号")]
        [Display(Name = "账号")]
        public string Username { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}