namespace WizLib_Model.Models
{
    // Link table for the Many-to-Many relation ship between Fluent_Book and Fluent_Author
    // Note! Many-to-many is just 2 sets of one-to-many relationship from the perspective of the Link Table
    public class Fluent_BookAuthor
    {
        public int BookId { get; set; }
        
        public int AuthorId { get; set; }

        public Fluent_Book Fluent_Book { get; set; }          // Navigation prop
        public Fluent_Author Fluent_Author { get; set; }      // Navigation prop
    }
}