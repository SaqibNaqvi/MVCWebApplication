using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Model
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
       
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for 50+")]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for 100+")]
        public double Price100 { get; set; }

        [ValidateNever]
        [Display(Name = "URL For Image")]
        public string ImageURL { get; set; }
        [Required]
        [ValidateNever]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        [ValidateNever]
        [Display(Name = "Cover Type")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
