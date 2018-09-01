using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBClone.DataAccess.Concrete;
using FBClone.WebUI.Models;
using Microsoft.Ajax.Utilities;

namespace FBClone.WebUI.Controllers
{
    public class UserController : Controller
    {
        private FBContext _context = new FBContext();


        // GET: User
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                var ID = Convert.ToInt32(Session["ID"]);
                UserViewModels model = new UserViewModels();
                model.Shareds = _context.Shareds.Where(x => x.UserId == ID).OrderByDescending(x => x.CreatedAt)
                    .ToList();

                model.Friends = _context.Friends.Where(x => x.UserId == ID).DistinctBy(x => x.FriendId).ToList();
                model.Users = _context.Users.ToList();

                var userBilgiler = _context.Users.FirstOrDefault(x => x.Id == ID);
                ViewData["FirstName"] = userBilgiler.FirstName;
                ViewData["LastName"] = userBilgiler.LastName;
                ViewData["DisplayName"] = userBilgiler.DisplayName;
                ViewData["Email"] = userBilgiler.Email;
                ViewData["Password"] = userBilgiler.Password;

                return View(model);
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult profileUpdate(FormCollection form)
        {
            var SessionUser = Convert.ToInt32(Session["ID"]);
            var user = _context.Users.Find(SessionUser);

            _context.Users.AddOrUpdate(new User()
            {
                Id = user.Id,
                DisplayName = form["DisplayName"],
                FirstName = form["FirstName"],
                LastName = form["LastName"],
                Password = form["Password"],
                Email = form["Email"],
            });
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }

            return null;
        }

        [HttpPost]
        public ActionResult Shared(FormCollection form)
        {
            var ID = Convert.ToInt32(Session["ID"]);

            _context.Shareds.Add(new Shared()
            {
                Content = form["Content"],
                UserId = ID,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                isDelete = false,
            });
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }

            return null;
        }

        [Route("/user/profile/Id")]
        public ActionResult Profile(int Id)
        {
            UserViewModels model = new UserViewModels();
            model.Shareds = _context.Shareds.Where(x => x.UserId == Id).OrderByDescending(x => x.CreatedAt).ToList();
            var user = _context.Users.FirstOrDefault(x => x.Id == Id);
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;
            ViewData["DisplayName"] = user.DisplayName;
            ViewData["Email"] = user.Email;
            return View(model);
        }

        [Route("/home/FriendAdd/{ID}")]
        public ActionResult FriendAdd(int ID)
        {
            var sessionUser = Convert.ToInt32(Session["ID"]);
            _context.Friends.Add(new Friend()
            {
                UserId = sessionUser,
                FriendId = ID,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                isDelete = false
            });
            _context.Messages.Add(new Message()
            {
                MessageContent = "Say Hello",
                FriendId = ID,
                UserId = sessionUser,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                isDelete = false

            });
            if (_context.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }

            return null;
        }

        public PartialViewResult Shareds()
        {
            return PartialView("Shareds");
        }

        public PartialViewResult Friends()
        {
            return PartialView("Friends");
        }
    }
}