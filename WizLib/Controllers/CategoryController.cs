using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            //List<Category> categoryList = await _dbContext.Categories.ToListAsync();
            List<Category> categoryList = await _dbContext.Categories.AsNoTracking().ToListAsync();  // Note! 'No Tracking' is ideal for read-only scenario and it is 'faster'. Retreiving by default will make EF track the Object
            return View(categoryList);
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
            Category category = new Category();
            if (id == null)
            {
                return View(category);
            }

            // Edit Category
            category = await _dbContext.Categories.FindAsync(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0) 
                {
                    // Create
                    _dbContext.Categories.Add(category);

                } else {
                    // Update
                    _dbContext.Categories.Update(category);
                }

                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _dbContext.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _dbContext.Categories.Remove(category);

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiple2()
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < 2; i++)
            {
                //_dbContext.Categories.Add(new Category{ Name = "Category " + DateTime.Now.ToString("HHmm")});
                categories.Add(new Category{ Name = "Category " + DateTime.Now.ToString()});
            }

            await _dbContext.Categories.AddRangeAsync(categories);

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateMultiple5()
        {
            List<Category> categories = new List<Category>();
            for (int i = 0; i < 5; i++)
            {
                //_dbContext.Categories.Add(new Category{ Name = "Category " + DateTime.Now.ToString("HHmm")});
                categories.Add(new Category{ Name = "Category " + DateTime.Now.ToString()});
            }

            await _dbContext.Categories.AddRangeAsync(categories);

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveMultiple2()
        {
            IEnumerable<Category> categories = await _dbContext.Categories.OrderByDescending(c => c.CategoryId).Take(2).ToListAsync();
            _dbContext.Categories.RemoveRange(categories);

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveMultiple5()
        {
            IEnumerable<Category> categories = await _dbContext.Categories.OrderByDescending(c => c.CategoryId).Take(5).ToListAsync();
            _dbContext.Categories.RemoveRange(categories);

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}