namespace WizLib_Model.Models
{
    public class Fluent_BookDetail
    {
        public int BookDetailId { get; set; }
        
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public double Weight { get; set; }
        public Fluent_Book Fluent_Book { get; set; }   // one-to-one Relationship with Book
    }
}