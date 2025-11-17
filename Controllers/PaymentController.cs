using _24DH113423_MyStore.Models.ViewModel;
using _24DH113423_MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _24DH113423_MyStore.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        private MyStoreEntities db = new MyStoreEntities();

        private CartService GetCartService()
        {
            return new CartService(Session);
        }

        // GET: /Payment hoặc /Payment/Index
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();

            // Nếu giỏ trống thì quay lại giỏ hàng
            if (cart == null || cart.Items == null || !cart.Items.Any())
            {
                return RedirectToAction("Index2", "Cart");
            }

            return View(cart);  // View: Views/Payment/Index.cshtml
        }
    }
}