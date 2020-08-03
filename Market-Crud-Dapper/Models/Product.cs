using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Market_Crud_Dapper.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Ürün adı girilmesi zorunludur.")]
        [Display(Name ="Ürün Adı")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Kategori girilmesi zorunludur.")]
        [Display(Name = "Ürün Kategorisi")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Ürün fiyatı girilmesi zorunludur.")]
        [Display(Name = "Ürün Fiyatı")]
        public decimal ProductPrice { get; set; }
        [Required(ErrorMessage = "Ürün açıklaması girilmesi zorunludur.")]
        [Display(Name = "Ürün Açıklaması")]
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Ürün barkodu girilmesi zorunludur.")]
        [Display(Name = "Ürün Barkodu")]
        public string ProductCode { get; set; }

    }
}