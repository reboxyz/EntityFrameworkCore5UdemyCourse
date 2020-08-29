using System.Collections.Generic;

namespace WizLib_Model.Models
{
    public class Fluent_Publisher
    {
        public int PublisherId { get; set; }
        
        public string Name { get; set; }
        
        public string Location { get; set; }
        
        public List<Fluent_Book> Fluent_Books { get; set; }  // Navigational prop
    }
}