using System.ComponentModel.DataAnnotations.Schema;

namespace WizLib_Model.Models
{
    // Note! Link Table between Book and Author many-to-many relationship
    public class BookAuthor
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Book Book { get; set; }          // Navigation prop
        public Author Author { get; set; }      // Navigation prop
        
    }
}