using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _db;


        public CompanyController(IUnitOfWork db)
        {
            _db = db;

        }
        public IActionResult Index()
        {

            return View();
        }


        //GeT
        public IActionResult UpSert(int? id)
        {
            Company company = new()
            {

            };

            if (id == null || id == 0)
            {
                //Create Product

                return View(company);
            }
            else
            {
                Company companyfromDB = _db.Company.GetFirstOrDefault(u => u.Id == id);
                return View(companyfromDB);

            }

            //return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(Company obj)
        {

            if (ModelState.IsValid)
            {


                if (obj.Id == 0)
                {
                    _db.Company.Add(obj);
                    TempData["Success"] = "Company Created Successfully!";
                }
                else
                {
                    _db.Company.Update(obj);
                    TempData["Success"] = "Company Updated Successfully!";
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
            var companies = _db.Company.GetAll();
            return Json(new { data = companies });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyfromDB = _db.Company.GetFirstOrDefault(u => u.Id == id);
            if (companyfromDB == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }


            _db.Company.Remove(companyfromDB);
            _db.Save();
            return Json(new { success = true, message = "Deleted" });



        }

        #endregion





    }
}
