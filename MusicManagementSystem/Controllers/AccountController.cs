using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicManagementSystem.Models;

namespace MusicManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // 1. 显示登录页面（GET请求）
        public ActionResult Login()
        {
            // 若已登录，直接跳转首页
            if (Session["IsLogin"] != null && (bool)Session["IsLogin"])
            {
                return RedirectToAction("Index", "Music");
            }
            return View(new LoginViewModel()); // 传递空模型到视图
        }

        // 2. 处理登录提交（POST请求）
        [HttpPost]
        [ValidateAntiForgeryToken] // 防CSRF攻击
        public ActionResult Login(LoginViewModel model)
        {
            // 第一步：验证表单输入合法性（前端验证不通过时，返回登录页）
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 第二步：模拟账号密码验证（实际项目需替换为数据库查询）
            // 文档中管理员账号示例：admin/admin（匹配个人信息页面的账号密码）
            bool isLoginSuccess = (model.Username == "admin" && model.Password == "admin");

            // 第三步：登录结果处理（通过Session存储登录状态，通过JS实现弹窗）
            if (isLoginSuccess)
            {
                // 登录成功：记录Session，弹窗提示后跳转首页
                Session["IsLogin"] = true;
                Session["Username"] = model.Username;
                ViewBag.Message = "登录成功！即将跳转首页...";
                ViewBag.IsSuccess = true;
            }
            else
            {
                // 登录失败：弹窗提示错误，停留在登录页
                ViewBag.Message = "用户名或密码错误！"; // 匹配文档中的错误提示
                ViewBag.IsSuccess = false;
            }

            return View(model);
        }

        // 3. 退出登录（后续首页头像退出功能调用）
        public ActionResult Logout()
        {
            Session.Clear(); // 清除登录状态
            return RedirectToAction("Login", "Account"); // 跳转回登录页
        }
    }
}