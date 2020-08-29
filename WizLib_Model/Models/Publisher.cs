using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WizLib_Model.Models
{
    // Note! Publisher has one-to-many relationship with Book
    public class Publisher
    {
        public int PublisherId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public List<Book> Books { get; set; }  // Navigational prop
    }
}