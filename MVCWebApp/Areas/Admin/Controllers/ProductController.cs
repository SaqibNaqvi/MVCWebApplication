using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly IWebHostEnvironment _env;

        public ProductController(IUnitOfWork db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {

            return View();
        }


        //GeT
        public IActionResult UpSert(int? id)
        {
            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _db.Category.GetAll().Select
                (
                    p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    }
                 ),
                CoverTypeList = _db.CoverType.GetAll().Select(
                    p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    }
                    )
            };

            if (id == null || id == 0)
            {
                //Create Product

                return View(productVM);
            }
            else
            {
                productVM.product = _db.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);

            }

            //return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ProductVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwrootpath = _env.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwrootpath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.product.ImageURL != null)
                    {
                        var oldImagePath = Path.Combine(wwwrootpath, obj.product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var filestreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }
                    obj.product.ImageURL = @"\images\products\" + filename + extension;
                }
                if (obj.product.Id == 0)
                {
                    _db.Product.Add(obj.product);
                    TempData["Success"] = "Product Created Successfully!";
                }
                else
                {
                    _db.Product.Update(obj.product);
                    TempData["Success"] = "Product Updated Successfully!";
                }

                _db.Save();

                return RedirectToAction("Index");
            }
            return View(obj);

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _db.Product.GetAll(IncludeProperties: "Category,CoverType");
            return Json(new { data = products });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var categoryfromDB = _db.Product.GetFirstOrDefault(u => u.Id == id);
            if (categoryfromDB == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            var oldImagePath = Path.Combine(_env.WebRootPath, categoryfromDB.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _db.Product.Remove(categoryfromDB);
            _db.Save();
            return Json(new { success = true, message = "Deleted" });



        }

        #endregion





    }
}
