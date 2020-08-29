using System.Collections.Generic;

namespace WizLib_Model.Models
{
    public class Fluent_Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        
        public double Price { get; set; }

        /* Note! This approach OK but explicitly set
        [ForeignKey("Category")]
        public int CategoryId { get; set; }     // Navigational Props
        */
        //public Category Category { get; set; }  // Navigational Props which automatically set a foreignkey of 'CategoryId'; 1-to-1 Relationship with Category
        
        // One-to-One relationship between Fluent_Book and Fluent_BookDetail
        public int BookDetailId { get; set;}
        public Fluent_BookDetail Fluent_BookDetail { get; set; } // Navigational Props; one-to-one Relationship with Book

        // One-to-Many relationship between Fluent_Book and Fluent_Publisher
        public int PublisherId { get; set;}
        public Fluent_Publisher Fluent_Publisher { get; set; } // Navigational Props; one-to-one Relationship with Publisher. However, Publisher has one-to-many Relationship with Book

        public string PriceRange { get; set; }   // Low, Middle, High; Computed prop based on Price which should not be mapped to DB

        // Many-to-Many relationship between Fluent_Book and Fluent_Author using Fluent_BookAuthor link table
        public ICollection<Fluent_BookAuthor> Fluent_BookAuthors { get; set; }
    }
}