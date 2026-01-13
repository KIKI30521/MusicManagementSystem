using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicManagementSystem.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入账号")] // 账号必填
        [Display(Name = "Username")] // 匹配文档中的"Username"标签
        [StringLength(20, MinimumLength = 3, ErrorMessage = "账号长度需3-20个字符")]
        public string Username { get; set; }

        [Required(ErrorMessage = "请输入密码")] // 密码必填
        [Display(Name = "Password")] // 匹配文档中的"Password"标签
        [DataType(DataType.Password)] // 密码类型（输入时隐藏）
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度需6-20个字符")]
        public string Password { get; set; }
    }
}