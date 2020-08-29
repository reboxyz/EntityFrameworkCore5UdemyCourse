using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class PublisherController: Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public PublisherController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Publisher> publishers = await _dbContext.Publishers.ToListAsync();
            return View(publishers);
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
            Publisher publisher = new Publisher();
            if (id == null)
            {
                return View(publisher);
            }

            // Edit Category
            publisher = await _dbContext.Publishers.FindAsync(id);

            if (publisher == null) return NotFound();

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.PublisherId == 0) 
                {
                    // Create
                    _dbContext.Publishers.Add(publisher);

                } else {
                    // Update
                    _dbContext.Publishers.Update(publisher);
                }

                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Publisher publisher = await _dbContext.Publishers.FindAsync(id);
            if (publisher == null) return NotFound();

            _dbContext.Publishers.Remove(publisher);

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}