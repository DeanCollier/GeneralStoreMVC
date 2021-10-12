using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStore.MVC.Models
{
    public class CreateTransactionViewModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }

    }
}