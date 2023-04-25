using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Book.Areas.Admin.Models
{
    public class Products
    {
        [Key]
        public int Id{set;get;}
        [Required(ErrorMessage = "Nhập tiêu đề !")]
        [Display(Name ="Tiêu đề")]
        public string Title{set;get;}
        [Display(Name ="Nội dung")]
        public string Description{set;get;}

        [Required(ErrorMessage ="vui lòng nhập ISBN !")]
        [Display(Name ="Tạo QR code")]
        public string ISBN { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên tác giả !")]
        [Display(Name ="Tên tác giả")]
        public string Author{set;get;}

        [Required]
        [Display(Name ="Price")]
        [Range(1,1000)]
        public double ListPrice{set;get;}

        [Required]
        [Display(Name ="Price for 1 => 50")]
        [Range(1,1000)]
        public double Price{set;get;}

        [Required]
        [Display(Name ="Price for 50")]
        [Range(1, 1000)]
        public double Price50{set;get;}

        [Required]
        [Display(Name ="Price for 100")]
        [Range(1, 1000)]
        public double Price100{set;get;}
        
        public int CategoryId{set;get;}
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Categorys Category{set;get;}
        [ValidateNever]
        public string ImageUrl{set;get;}
    }
}