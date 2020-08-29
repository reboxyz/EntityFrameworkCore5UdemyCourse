using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class AuthorController: Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _dbContext.Authors.ToListAsync();
            return View(authors);
        }

        public async Task<IActionResult> Upsert(int? id) 
        {
            Author author = new Author();
            if (id == null)
            {
                return View(author);
            }

            // Edit 
            author = await _dbContext.Authors.FindAsync(id);

            if (author == null) return NotFound();

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.AuthorId == 0) 
                {
                    // Create
                    _dbContext.Authors.Add(author);

                } else {
                    // Update
                    _dbContext.Authors.Update(author);
                }

                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Author author = await _dbContext.Authors.FindAsync(id);
            if (author == null) return NotFound();

            _dbContext.Authors.Remove(author);

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}