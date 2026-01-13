using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicManagementSystem.DAL;
using MusicManagementSystem.Models;

namespace MusicManagementSystem.Controllers
{
    public class MusicController : Controller
    {
        private MusicDbContext db = new MusicDbContext();

        // 登录状态验证（双重拦截）
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["IsLogin"] == null || !(bool)Session["IsLogin"])
            {
                string returnUrl = filterContext.HttpContext.Request.Url?.AbsolutePath ?? "~/Music/Index"; // 只取路径，不要查询参数
                                                                                                           // 限制长度
                if (returnUrl.Length > 100)
                {
                    returnUrl = "~/Music/Index";
                }
                filterContext.Result = new RedirectResult($"~/Account/Login?returnUrl={Server.UrlEncode(returnUrl)}");
            }
        }

        // 歌曲浏览页（首页）
        public ActionResult Index()
        {
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

        // 添加歌曲（GET）
        public ActionResult Create()
        {
            return View();
        }

        // 添加歌曲（POST）
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Music music)
        {
            if (ModelState.IsValid)
            {
                db.Musics.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(music);
        }

        // 编辑歌曲（GET）
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Music music = db.Musics.Find(id);
            if (music == null) return HttpNotFound();
            return View(music);
        }

        // 编辑歌曲（POST）
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Music music)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(music);
        }

        // 删除确认页（GET）
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            Music music = db.Musics.Find(id);
            if (music == null) return HttpNotFound();
            return View(music);
        }

        // 执行删除（POST）
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Musics.Find(id);
            db.Musics.Remove(music);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // 释放数据库资源
        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}