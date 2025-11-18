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
                try
                {
                    // Kiểm tra xem tên đăng nhập đã tồn tại chưa
                    var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                        return View(model);
                    }

                    // Kiểm tra email đã tồn tại chưa
                    var existingEmail = db.Customers.SingleOrDefault(c => c.CustomerEmail == model.CustomerEmail);
                    if (existingEmail != null)
                    {
                        ModelState.AddModelError("CustomerEmail", "Email này đã được sử dụng.");
                        return View(model);
                    }

                    // Mã hóa mật khẩu
                    var passwordHash = HashPassword(model.Password);

                    // Tạo bản ghi thông tin tài khoản trong bảng User
                    var user = new User
                    {
                        Username = model.Username,
                        Password = passwordHash, // Mã hóa mật khẩu trước khi lưu
                        UserRole = ROLE_CUSTOMER
                    };
                    db.Users.Add(user);

                    // Tạo bản ghi thông tin khách hàng trong bảng Customer
                    var customer = new Customer
                    {
                        CustomerName = model.CustomerName,
                        CustomerEmail = model.CustomerEmail,
                        CustomerPhone = model.CustomerPhone,
                        CustomerAddress = model.CustomerAddress,
                        Username = model.Username,
                    };
                    db.Customers.Add(customer);

                    // Lưu thông tin tài khoản và thông tin khách hàng vào CSDL
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (DbEntityValidationException ex)
                {
                    var errors = ex.EntityValidationErrors
                                    .SelectMany(e => e.ValidationErrors)
                                    .Select(e => e.PropertyName + ": " + e.ErrorMessage)
                                    .ToList();
                    var errorMessage = string.Join("; ", errors);

                    // Thêm thông báo lỗi vào ModelState để hiển thị trong view
                    ModelState.AddModelError("", "Validation failed: " + errorMessage);
                    return View(model); // Trả lại View và hiển thị thông báo lỗi
                }
            }
            return View(model);
        }

        // Hàm mã hóa mật khẩu
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
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
                // Kiểm tra đăng nhập với tên người dùng và mật khẩu đã mã hóa
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (user != null)
                {
                    // Kiểm tra mật khẩu
                    var passwordHash = HashPassword(model.Password);
                    if (passwordHash == user.Password)
                    {
                        // Lưu trạng thái đăng nhập vào session
                        Session["Username"] = user.Username;
                        Session["UserRole"] = user.UserRole;

                        // Lưu thông tin xác thực người dùng vào cookie
                        FormsAuthentication.SetAuthCookie(user.Username, false);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }
    }
}
