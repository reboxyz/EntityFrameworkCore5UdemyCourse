using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WizLib_Model.Models;

namespace WizLib_Model.ViewModels
{
    public class BookAuthorViewModel
    {
        public BookAuthor BookAuthor { get; set; }  // Link Table between Book and Author many-to-many relationship
        public Book Book { get; set; }              // Current selected Book to add an Author
        public IEnumerable<BookAuthor>  BookAuthorList { get; set; }  // For all Authors associated to the current Book
        public IEnumerable<SelectListItem> AuthorList { get; set; }   // For display of all available Authors to be associated
    }
}