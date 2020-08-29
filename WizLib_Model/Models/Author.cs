using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WizLib_Model.Models
{
    // Note! Author and Book has many-to-many relationship. To implement many-to-many, we need Link table called "BookAuthor"
    public class Author
    {
        [Key]                                                   // Note! Explicitly defined which is redundant
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // Note! Explicitly defined which is redundant
        public int AuthorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        protected ICollection<BookAuthor> BookAuthors { get; set;} // Navigational prop; Many-to-many relationship with Book

        public string Location { get; set; }
        [NotMapped]
        public string FullName { 
            get {
                return $"{FirstName} {LastName}";
            }
        }
    }
}