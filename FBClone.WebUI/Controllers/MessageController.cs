using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBClone.DataAccess.Concrete;
using FBClone.WebUI.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace FBClone.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private FBContext _context = new FBContext();

        // GET: Message
        public ActionResult Index()
        {
            //var ID = Convert.ToInt32(Session["ID"]);
            //UserViewModels model = new UserViewModels();
            //model.Friends = _context.Friends.Where(x => x.UserId == ID).ToList();
            //return View(model);

            var ID = Convert.ToInt32(Session["ID"]);
            if (Session["ID"] != null)
            {
                UserViewModels model = new UserViewModels();
                model.Friends = _context.Friends.Where(x => x.UserId == ID).DistinctBy(x => x.FriendId).ToList();
                model.Users = _context.Users.ToList();

                List<Message> collection = new List<Message>();
                foreach (var friend in model.Friends)
                {
                    collection.AddRange(_context.Messages.Where(x => x.UserId == ID && x.FriendId == friend.FriendId).ToList());
                }

                ViewBag.liste = collection;
                return View(model);
            }

            return RedirectToAction("Login", "Home");
        }

        public JsonResult GetMessageAjax(int friendID)
        {
            var SessionUser = Convert.ToInt32(Session["ID"]);
            var messages = _context.Messages.Where(x => x.UserId == SessionUser && x.FriendId == friendID).OrderByDescending(x => x.CreatedAt).ToList();

            return Json(messages, JsonRequestBehavior.AllowGet);
        }
    }
}