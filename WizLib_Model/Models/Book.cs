using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WizLib_Model.Models
{
    // Note! Book has one-to-one relationship with BookDetail
    //       Book has one-to-one relationship with Publisher but Publisher has one-to-many relationship with Book
    //       Book and Author has many-to-many relationship. To implement many-to-many, we need Link table called "BookAuthor"
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }
        [Required]
        public double Price { get; set; }

        /* Note! This approach OK but explicitly set
        [ForeignKey("Category")]
        public int CategoryId { get; set; }     // Navigational Props
        */
        //public Category Category { get; set; }  // Navigational Props which automatically set a foreignkey of 'CategoryId'; 1-to-1 Relationship with Category

        [ForeignKey("BookDetail")]
        public int? BookDetailId { get; set;}
        public BookDetail BookDetail { get; set; } // Navigational Props; one-to-one Relationship with Book


        [ForeignKey("Publisher")]
        public int PublisherId { get; set;}
        public Publisher Publisher { get; set; } // Navigational Props; one-to-one Relationship with Publisher. However, Publisher has one-to-many Relationship with Book

        public ICollection<BookAuthor> BookAuthors { get; set;} // Navigational prop; Many-to-many relationship with Author

        [NotMapped]
        public string PriceRange { get; set; }   // Low, Middle, High; Computed prop based on Price which should not be mapped to DB
    }
}