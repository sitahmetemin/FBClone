using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FBClone.DataAccess.Concrete;
using FBClone.WebUI.Models;
using Microsoft.Ajax.Utilities;

namespace FBClone.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private FBContext _context = new FBContext();


        public ActionResult Index()
        {

            var ID = Convert.ToInt32(Session["ID"]);
            if (Session["ID"] != null)
            {
                UserViewModels model = new UserViewModels();
                model.Friends = _context.Friends.Where(x => x.UserId == ID).ToList();
                List<Shared> koleksiyon = new List<Shared>();
                foreach (var friend in model.Friends)
                {
                    koleksiyon.AddRange(_context.Shareds.Where(x => x.UserId == friend.FriendId).ToList());
                }
                
                ViewBag.liste = koleksiyon.OrderByDescending(x => x.CreatedAt);
                return View();

            }

            return RedirectToAction("Login");

        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fomCollection)
        {
            if (fomCollection["Giris"] != null)
            {
                var email = fomCollection["Email"];
                var pass = fomCollection["Password"];
                var sorgu = _context.Users.FirstOrDefault(user =>
                    user.Email == email && user.Password == pass);

                if (sorgu != null)
                {
                    var name = sorgu.FirstName + " " + sorgu.LastName;
                    Session.Add("ID", sorgu.Id);
                    Session.Add("DisplayName", sorgu.DisplayName);
                    Session.Add("Email", sorgu.Email);
                    Session.Add("Name", name);

                    return RedirectToAction("Index");
                }
            }
            else
            {
                var email = fomCollection["Email"];
                var pass = fomCollection["Password"];
                var firstName = fomCollection["FirstName"];
                var lastName = fomCollection["LastName"];
                var displayName = fomCollection["DisplayName"];
                _context.Users.Add(new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DisplayName = displayName,
                    Email = email,
                    Password = pass,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    isDelete = false
                });
                _context.SaveChanges();
                ViewBag.Basarili = "Kayıt Gerçekleşti Şimdi Giriş Yapabilirsiniz";
                return RedirectToAction("Login");
            }
            

            return RedirectToAction("Login");

        }

        public PartialViewResult Profile()
        {
            return PartialView();
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
            if (_context.SaveChanges()>0)
            {
                return RedirectToAction("Index");
            }

            return null;

        }

        public ActionResult Logout()
        {
            Session.Remove("ID");
            Session.Remove("DisplayName");
            Session.Remove("Email");
            Session.Remove("Name");

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            var searchKey = form["Search"];

            UserViewModels model = new UserViewModels();
            model.Users = _context.Users.Where(x => x.DisplayName.Contains(searchKey) || x.FirstName.Contains(searchKey) || x.LastName.Contains(searchKey)).ToList();
            model.Shareds = _context.Shareds.Where(x => x.Content.Contains(searchKey)).ToList();

            ViewBag.SearchKey = searchKey;
            return View(model);
        }

        [Route("/Home/like/{ID}")]
        public ActionResult Like(int ID)
        {
            var sessionID = Convert.ToInt32(Session["ID"]);
            _context.Likes.Add(new Like()
            {
                SharedId = ID,
                UserId = sessionID,
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


        public ActionResult ShareCommentAdd(string mesaj, int paylasimID)
        {
            var ID = Convert.ToInt32(Session["ID"]);
            _context.Comments.Add(new Comment()
            {
                SharedId = paylasimID,
                UserId = ID,
                SharedComment = mesaj,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                isDelete = false
            });
            if (_context.SaveChanges() > 0)
            {
                return Json("Başarılı", JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        public JsonResult ShareGetComment(int sharedID)
        {
            var degisken = _context.Comments.Where(x => x.SharedId == sharedID).ToList();
            return Json(degisken, JsonRequestBehavior.AllowGet);
        }

    }
}