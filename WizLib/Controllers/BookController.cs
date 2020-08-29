using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WizLib_Model.ViewModels;
using System.Linq;

namespace WizLib.Controllers
{
    public class BookController: Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            // Explicit loading approach (Note! Not so efficient)
            /*
            List<Book> books = await _dbContext.Books.ToListAsync();
            foreach(var book in books)
            {
                _dbContext.Entry(book).Reference(b => b.Publisher).Load();          // Single Object
                _dbContext.Entry(book).Collection(b => b.BookAuthors).Load();       // Collection of Objects
                // We have to explicitly load each Author inside the BookAuthors colleciton
                foreach(var bookAuth in book.BookAuthors)
                {
                    _dbContext.Entry(bookAuth).Reference(ba => ba.Author).Load();
                }
            }
            */    
            
            List<Book> books = await _dbContext
                .Books
                .Include(b => b.Publisher)    // Eager loading
                .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                .ToListAsync();
            
            return View(books);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            BookViewModel bookVM = new BookViewModel();
            bookVM.Book = new Book();

            // Note! Use EF Projection to fill the Selection for Publisher (Dropdown List)
            bookVM.PublisherList = _dbContext.Publishers.Select(p => new SelectListItem{
                Text = p.Name,
                Value = p.PublisherId.ToString()
            });

            if (id == null)
            {
                return View(bookVM);
            }

            // Edit
            bookVM.Book = await _dbContext.Books.FindAsync(id);

            if (bookVM.Book == null)
            {
                return NotFound();
            }

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookViewModel bookVM)
        {
            if (ModelState.IsValid) 
            {
                if (bookVM.Book.BookId == 0)
                {
                    // Create
                    _dbContext.Books.Add(bookVM.Book);
                } else {
                    // Update
                    _dbContext.Books.Update(bookVM.Book);
                }

                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(bookVM);
        }

        public async Task<IActionResult> Delete(int id) 
        {
            var bookToDelete = await _dbContext.Books.FindAsync(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(bookToDelete);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Note! This is for Book Details
        public async Task<IActionResult> Details(int? id)
        {
            BookViewModel bookVM = new BookViewModel();
            bookVM.Book = new Book();

            if (id == null)
            {
                return View(bookVM);
            }

            // Edit
            // Note! Replace with Eager loading
            /*
            //bookVM.Book = await _dbContext.Books.FindAsync(id);
            bookVM.Book = await _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);

            //bookVM.Book.BookDetail = await _dbContext.BookDetails.FindAsync(bookVM.Book.BookDetailId);
            bookVM.Book.BookDetail = await _dbContext.BookDetails.FirstOrDefaultAsync(bd => bd.BookDetailId == bookVM.Book.BookDetailId);
            */

            // Eager Loading
            bookVM.Book = await _dbContext.Books.Include(b => b.BookDetail).SingleOrDefaultAsync(b => b.BookId == id);

            if (bookVM.Book == null)
            {
                return NotFound();
            }

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookViewModel bookVM)
        {
            //if (ModelState.IsValid) 
            //{
                //if (bookVM.Book.BookDetailId == null || bookVM.Book.BookDetailId == 0)
                if (bookVM.Book.BookDetail.BookDetailId == 0)
                {
                    // Create Book Detail Record
                    _dbContext.BookDetails.Add(bookVM.Book.BookDetail);
                    _dbContext.SaveChanges();

                    // Update Book after updating the BookDetail coz the BookDetailId is generated after inserting new record
                    Book bookFromDb = _dbContext.Books.Find(bookVM.Book.BookId);
                    bookFromDb.BookDetailId = bookVM.Book.BookDetail.BookDetailId;

                    _dbContext.SaveChanges();
                } else {
                    // Update
                    _dbContext.BookDetails.Update(bookVM.Book.BookDetail);
                    _dbContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            //}
            //return View(bookVM);
        }

        public async Task<IActionResult> ManageAuthors(int id)
        {
            BookAuthorViewModel obj = new BookAuthorViewModel
            {
                BookAuthorList = await _dbContext.BookAuthors
                    .Include(ba => ba.Book)
                    .Include(ba => ba.Author)
                    .Where(ba => ba.BookId == id)
                    .ToListAsync(),
                BookAuthor = new BookAuthor{
                    BookId = id
                    // Note! Author props to be set in the screen
                },
                Book = _dbContext.Books.FirstOrDefault(b => b.BookId == id),
            };

            // For AuthorList, we only need to extract Authors not yet assigned to the target Book
            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(ba => ba.AuthorId).ToList();

            // NOT IN Clause in LINQ
            // Get all authors whose ID is not in the tempListOfAssignedAuthors
            var tempList = _dbContext.Authors.Where(a => !tempListOfAssignedAuthors
                                            .Contains(a.AuthorId)).ToList();

            // Create EF Projection
            obj.AuthorList = tempList.Select(a => new SelectListItem {
                Text = a.FullName,
                Value = a.AuthorId.ToString()
            });

            return View(obj);
        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorViewModel bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.BookId != 0 && bookAuthorVM.BookAuthor.AuthorId != 0)
            {
                _dbContext.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new {@id = bookAuthorVM.BookAuthor.BookId});
        }

        
        [HttpPost]
        // Note! authorId is from the Query string and bookAuthorVM is from the Body
        public IActionResult RemoveAuthors(int authorId, BookAuthorViewModel bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.BookId;
            BookAuthor bookAuthor = _dbContext.BookAuthors.FirstOrDefault(ba => ba.AuthorId == authorId 
                                    && ba.BookId == bookId);
                            
            _dbContext.BookAuthors.Remove(bookAuthor);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(ManageAuthors), new {@id = bookId});
        }

        public IActionResult PlayGround()
        {
            // Note! This has no View
            /*
            var bookTemp = _dbContext.Books.FirstOrDefault();
            bookTemp.Price = 100;

            var bookCollection = _dbContext.Books;
            double totalPrice = 0;

            foreach(var book in bookCollection)
            {
                totalPrice += book.Price;
            }

            var bookList = _dbContext.Books.ToList();
            foreach (var book in bookList)
            {
                totalPrice += book.Price;
            }

            var bookCollection2 = _dbContext.Books;
            var bookCount1 = bookCollection2.Count();

            var bookCount2 = _dbContext.Books.Count();

            // IEnumerable VS IQueryable
            IEnumerable<Book> bookList1 = _dbContext.Books;
            var filteredBooks1 = bookList1.Where(b => b.Price > 10).ToList();

            IQueryable<Book> bookList2 = _dbContext.Books;
            var filteredBooks2 = bookList2.Where(b => b.Price > 10).ToList();

            // Update VS Attach
            // Note! EF core will track all retrieve Objects and when Update is called it will perform Updating to all Objects
            var bookTemp1 = _dbContext.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.BookId == 8);
            bookTemp1.BookDetail.NumberOfChapters = 22;
            bookTemp1.Price = 345;
            _dbContext.Books.Update(bookTemp1);  // Note! 2 separate Update calls in the DB will executed
            _dbContext.SaveChanges();

            // Note! EF core will track all retrieve Objects and when Update is called it will perform Updating to all Objects
            var bookTemp2 = _dbContext.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.BookId == 8);
            bookTemp2.BookDetail.NumberOfChapters = 33;
            bookTemp2.Price = 346;
            _dbContext.Books.Attach(bookTemp2);  // Note! 'Attach' only those 'modified' state  object will be updated unlike 'Update'
            _dbContext.SaveChanges();


            // Manually setting to 'Modified' state
            var category = _dbContext.Categories.FirstOrDefault();
            // Note! This will force to call Update despite no props were updated coz we explicitly set to 'Modified'
            _dbContext.Entry(category).State = EntityState.Modified;  
                      _dbContext.SaveChanges();  
            */

            // Views
            var viewList = _dbContext.BookDetailsFromView.ToList();
            var viewList1 = _dbContext.BookDetailsFromView.FirstOrDefault();
            var viewList2 = _dbContext.BookDetailsFromView.Where(v => v.Price > 10).ToList();

            // Raw SQL
            // Note! Selected fields must match with the Entity used
            var bookRaw = _dbContext.Books.FromSqlRaw("SELECT * FROM dbo.Books").ToList();

            int bookId = 8;
            var bookTemp1 = _dbContext.Books.FromSqlInterpolated($"SELECT * FROM dbo.Books WHERE BookId = {bookId} ").ToList();

            // Stored Procedure 
            var bookSproc = _dbContext.Books.FromSqlInterpolated($"EXEC dbo.getAllBookDetails {bookId}").ToList();

            // .NET 5 Features only
            var bookFilter = _dbContext.Books
                                .Include(b => b.BookAuthors.Where(ba => ba.AuthorId == 1))  // Note! LEFT OUTER JOIN is used behind the scene
                                .ToList();

            var bookFilter2 = _dbContext.Books
                                .Include(b => b.BookAuthors.OrderByDescending(ba => ba.AuthorId ).Take(2))  // Note! OUTER APPLY is used behind the scene
                                .ToList();


            return RedirectToAction(nameof(Index));  
        }

    }
}