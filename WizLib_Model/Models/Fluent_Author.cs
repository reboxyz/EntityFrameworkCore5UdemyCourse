using System;
using System.Collections.Generic;

namespace WizLib_Model.Models
{
    public class Fluent_Author
    {
        public int AuthorId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        
        public string Location { get; set; }

        public string FullName { 
            get {
                return $"{FirstName} {LastName}";
            }
        }

        // Many-to-Many relationship between Fluent_Book and Fluent_Author using Fluent_BookAuthor link table
        public ICollection<Fluent_BookAuthor> Fluent_BookAuthors { get; set; }
    }
}