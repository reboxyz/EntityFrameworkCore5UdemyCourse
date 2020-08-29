using System.ComponentModel.DataAnnotations;

namespace WizLib_Model.Models
{
    // Note! Book has 1-to-1 relationship with BookDetail. But DbSet<BookDetail> is not set in the DbContext
    public class BookDetail
    {
        public int BookDetailId { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public double Weight { get; set; }
        public Book Book { get; set; }   // one-to-one Relationship with Book
    }
}