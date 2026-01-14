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
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            // 排除Login动作，防止循环跳转
            if (filterContext.ActionDescriptor.ActionName == "Login")
            {
                return;
            }
            // 验证登录状态
            if (Session["IsLogin"] == null || !(bool)Session["IsLogin"])
            {
                string returnUrl = filterContext.HttpContext.Request.Url?.PathAndQuery ?? "~/Music/Index";
                // 限制returnUrl长度（只保留路径，不保留查询参数）
                if (returnUrl.Length > 100)
                {
                    returnUrl = "~/Music/Index";
                }
                filterContext.Result = new RedirectResult($"~/Account/Login?returnUrl={Server.UrlEncode(returnUrl)}");
            }
        }
        // 模拟管理员数据
        private List<AdminViewModel> GetAdminList()
        {
            return new List<AdminViewModel>
            {
                new AdminViewModel { Id = 1, Username = "admin", Password = "123456" },
                new AdminViewModel { Id = 2, Username = "manager", Password = "654321" }
            };
        }

        // 登录页（GET）
        public ActionResult Login(string returnUrl)
        {
            if (Session["IsLogin"] != null && (bool)Session["IsLogin"])
            {
                return RedirectToAction("Index", "Music");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        // 登录提交（POST）
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            // 第一步：验证表单输入合法性（前端验证不通过时，返回登录页）
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 验证账号密码（admin/123456）
            var admin = GetAdminList().FirstOrDefault(a => a.Username == model.Username && a.Password == model.Password);
            if (admin != null)
            {
                // 登录成功：记录Session
                Session["IsLogin"] = true;
                Session["Username"] = model.Username;
                // 跳转回原请求页或首页
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Music");
            }
            else
            {
                ModelState.AddModelError("", "账号或密码错误（默认：admin/123456）");
                return View(model);
            }
            }

        // 个人信息页
        public ActionResult Info()
        {
            var adminList = GetAdminList();
            return View(adminList);
        }

        // 退出登录
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}