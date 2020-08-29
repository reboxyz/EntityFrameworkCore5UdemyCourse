using System.Collections.Generic;
using WizLib_Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WizLib_Model.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }

    }
}