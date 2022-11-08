using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _db;

        public CoverTypeController(IUnitOfWork db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeFromDb = _db.CoverType.GetAll();
            return View(coverTypeFromDb);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.CoverType.Add(obj);
                _db.Save();
                TempData["Success"] = "Cover Type is Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverTypeFromDB = _db.CoverType.GetFirstOrDefault(p => p.Id == id);
            if (coverTypeFromDB == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDB);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.CoverType.Update(obj);
                _db.Save();
                TempData["Success"] = "Cover Type is Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverTypeFromDB = _db.CoverType.GetFirstOrDefault(p => p.Id == id);
            if (coverTypeFromDB == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDB);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var coverTypeFromDB = _db.CoverType.GetFirstOrDefault(p => p.Id == id);
            if (coverTypeFromDB == null)
            {
                return NotFound();
            }

            _db.CoverType.Remove(coverTypeFromDB);
            _db.Save();
            TempData["Success"] = "Cover Type is Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
