using _24DH113423_MyStore.Models;
using _24DH113423_MyStore.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;
using Microsoft.AspNetCore.Identity;

namespace _24DH113423_MyStore.Controllers
{
    public class AccountController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        private const string ROLE_CUSTOMER = "C";

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                // kiểm tra trùng username
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }

                // nếu chưa tồn tại thì tạo bản ghi thông tin tài khoản trong bảng User
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password, 
                    UserRole = ROLE_CUSTOMER     // "C"
                };
                db.Users.Add(user);

                // và tạo bản ghi thông tin khách hàng trong bảng Customer
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username
                };
                db.Customers.Add(customer);

                try
                {
                    // lưu thông tin tài khoản và thông tin khách hàng vào CSDL
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    // Ghi rõ field nào sai
                    var errors = ex.EntityValidationErrors
                                    .SelectMany(e => e.ValidationErrors)
                                    .Select(e => e.PropertyName + ": " + e.ErrorMessage)
                                    .ToList();

                    string errorMessage = string.Join("; ", errors);
                    ModelState.AddModelError("", "Validation failed: " + errorMessage);

                    return View(model);
                }
            }

            return View(model);
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                // tìm user với Username, Password khớp và role là Customer
                var user = db.Users.SingleOrDefault(u =>
                        u.Username == model.Username &&
                        u.Password == model.Password &&
                        u.UserRole == ROLE_CUSTOMER);

                if (user != null)
                {
                    // lưu trạng thái đăng nhập vào session
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;

                    // không dùng FormsAuthentication, chỉ dùng Session
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            return View(model);
        }

        // Logout đơn giản
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
