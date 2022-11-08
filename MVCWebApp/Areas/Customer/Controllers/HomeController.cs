using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Security.Claims;

namespace MVCWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitofWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productsList = _unitofWork.Product.GetAll(IncludeProperties: "Category,CoverType");
            return View(productsList);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _unitofWork.Product.GetFirstOrDefault(u => u.Id == productId, IncludeProperties: "Category,CoverType")
            };

            return View(cartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;


            ShoppingCart CartfromDb = _unitofWork.ShoppingCart.GetFirstOrDefault(
                u => u.ApplicationUserId == shoppingCart.ApplicationUserId && u.ProductId == shoppingCart.ProductId
                );

            if (CartfromDb == null)
            {
                _unitofWork.ShoppingCart.Add(shoppingCart);
                _unitofWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitofWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
            else
            {
                _unitofWork.ShoppingCart.IncreamentCount(CartfromDb, shoppingCart.Count);
                _unitofWork.Save();
            }



            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}