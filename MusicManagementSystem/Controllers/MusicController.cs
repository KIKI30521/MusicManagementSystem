using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicManagementSystem.DAL;
using MusicManagementSystem.Models;

namespace MusicManagementSystem.Controllers
{
    [Authorize] // 登录拦截特性：未登录用户自动跳转登录页
    public class MusicController : Controller
    {
        // 初始化数据库上下文（与数据库交互核心）
        private MusicDbContext db = new MusicDbContext();

        //1. 登录状态验证（重写方法，拦截未登录请求）
        // 登录状态验证（双重拦截）
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["IsLogin"] == null || !(bool)Session["IsLogin"])
            {
                // 记录当前请求地址（登录成功后返回原页面）
                string returnUrl = filterContext.HttpContext.Request.Url?.PathAndQuery ?? "~/Music/Index";
                // 跳转至登录页，携带返回地址参数
                string returnUrl = filterContext.HttpContext.Request.Url?.AbsolutePath ?? "~/Music/Index"; // 只取路径，不要查询参数
                                                                                                           // 限制长度
                if (returnUrl.Length > 100)
                {
                    returnUrl = "~/Music/Index";
                }
                filterContext.Result = new RedirectResult($"~/Account/Login?returnUrl={Server.UrlEncode(returnUrl)}");
            }
        }

        //2. 首页音乐列表（适配文档「首页显示歌曲列表」需求）
        // GET: Music/Index
        // 歌曲浏览页（首页）
        public ActionResult Index()
        {
            // 从数据库查询所有歌曲，按ID倒序排列（最新添加的歌曲在前面）
            var musicList = db.Musics.OrderByDescending(m => m.Id).ToList();
            // 传递歌曲列表数据到Index视图（匹配文档中ID、名称、音源ID等显示字段）
            return View(musicList);
            var musicList = db.Musics.OrderBy(m => m.Id).ToList();

            var musicWithSerial = musicList.Select((music, index) => new MusicWithSerialViewModel
            {
                SerialNumber = index + 1, // 动态序号
                Id = music.Id,            // 数据库ID
                SongName = music.SongName,
                ImageUrl = music.ImageUrl,
                MusicSourceId = music.MusicSourceId,
                MusicUrl = music.MusicUrl
            }).ToList();

            return View(musicWithSerial);
        }

        //3. 添加歌曲（适配文档「添加歌曲页面」需求）
        // GET: Music/Create（显示添加歌曲表单）
        // 添加歌曲（GET）
        public ActionResult Create()
        {
            // 传递空的Music模型到Create视图（用于表单绑定）
            return View(new Music());
            return View();
        }

        // POST: Music/Create（处理添加歌曲表单提交）
        // 添加歌曲（POST）
        [HttpPost]
        [ValidateAntiForgeryToken] // 防CSRF攻击（表单提交必加）
        [ValidateAntiForgeryToken]
        public ActionResult Create(Music music)
        {
            // 验证表单输入合法性（匹配文档中「歌曲名称必填」等规则）
            if (ModelState.IsValid)
            {
                // 将表单提交的歌曲数据添加到数据库
                db.Musics.Add(music);
                // 保存修改到数据库
                db.SaveChanges();
                // 添加成功后跳转回首页列表（刷新显示新歌曲）
                return RedirectToAction("Index");
            }
            // 若表单验证失败（如必填项未填），返回添加表单页并显示错误提示
            return View(music);
        }

        //4. 编辑歌曲（适配文档「首页编辑按钮」需求）
        // GET: Music/Edit（显示编辑歌曲表单）
        // 编辑歌曲（GET）
        public ActionResult Edit(int? id)
        {
            // 检查歌曲ID是否为空（防止非法访问）
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            // 根据ID从数据库查询待编辑的歌曲
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Music music = db.Musics.Find(id);
            // 若歌曲不存在（如ID错误），返回404页面
            if (music == null)
            {
                return HttpNotFound();
            }
            // 传递待编辑的歌曲数据到Edit视图
            if (music == null) return HttpNotFound();
            return View(music);
        }

        // POST: Music/Edit（处理编辑歌曲表单提交）
        // 编辑歌曲（POST）
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Music music)
        {
            // 验证表单输入合法性
            if (ModelState.IsValid)
            {
                // 标记歌曲数据为「已修改」，通知EF更新数据库
                db.Entry(music).State = System.Data.Entity.EntityState.Modified;
                // 保存修改到数据库
                db.SaveChanges();
                // 编辑成功后跳转回首页列表
                return RedirectToAction("Index");
            }
            // 若验证失败，返回编辑表单页并显示错误提示
            return View(music);
        }

        //5. 删除歌曲（适配文档「首页删除按钮」需求）
        // GET: Music/Delete（显示删除确认页面）
        // 删除确认页（GET）
        public ActionResult Delete(int? id)
        {
            // 检查歌曲ID是否为空
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            // 根据ID查询待删除的歌曲
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Music music = db.Musics.Find(id);
            // 若歌曲不存在，返回404页面
            if (music == null)
            {
                return HttpNotFound();
            }
            // 传递待删除的歌曲数据到Delete视图（显示确认信息）
            if (music == null) return HttpNotFound();
            return View(music);
        }

        // POST: Music/Delete（处理删除确认，执行删除操作）
        [HttpPost, ActionName("Delete")] // ActionName与GET方法一致，统一访问路径
        // 执行删除（POST）
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 根据ID查询待删除的歌曲
            Music music = db.Musics.Find(id);
            // 从数据库中删除该歌曲
            db.Musics.Remove(music);
            // 保存修改到数据库
            db.SaveChanges();
            // 删除成功后跳转回首页列表
            return RedirectToAction("Index");
        }

        //6. 资源释放（防止内存泄漏）
        // 释放数据库资源
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 释放数据库上下文资源
                db.Dispose();
            }
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        //7. 退出登录（适配文档「点头像退出」需求，供首页调用）
        public ActionResult Logout()
        {
            // 清除Session中的登录状态（与AccountController.Logout逻辑一致）
            Session.Clear();
            // 退出后跳转回登录页
            return RedirectToAction("Login", "Account");
        }
    }
}