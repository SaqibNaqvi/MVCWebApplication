using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _db;

        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Category.GetAll();
            return View(objCategoryList);
        }

        //GeT
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Name and Display Order are both the same.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.Save();
                TempData["Success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GeT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryfromDB = _db.Categories.Find(id);
            var categoryfromDB = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryfromDB == null)
            {
                return NotFound();
            }
            return View(categoryfromDB);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The Name and Display Order are both the same.");
            }
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.Save();
                TempData["Success"] = "Category Edited Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GeT
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromDB = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryfromDB == null)
            {
                return NotFound();
            }
            return View(categoryfromDB);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryfromDB = _db.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryfromDB == null)
            {
                return NotFound();
            }


            _db.Category.Remove(categoryfromDB);
            _db.Save();
            TempData["Success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");



        }







    }
}
