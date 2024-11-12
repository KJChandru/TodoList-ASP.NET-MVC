using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList=_db.Categories.ToList();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            var dataindb = _db.Categories.Find(id);
            if (dataindb == null)
            {
                return NotFound();
            }
            return View(dataindb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _db.Categories.Find(obj.Id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                existingCategory.Name = obj.Name;
                existingCategory.DisplayOrder = obj.DisplayOrder;

                _db.Categories.Update(existingCategory);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            var dataindb = _db.Categories.Find(id);
            if (dataindb == null)
            {
                return NotFound();
            }
            return View(dataindb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
                var existingCategory = _db.Categories.Find(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                _db.Categories.Remove(existingCategory);
                _db.SaveChanges();

                return RedirectToAction("Index");
              
        }
    }
}

