

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Book.Models{
    public class Categorys{
        [Key]
        public int Id{set;get;}

        [Required(ErrorMessage ="Vui lòng không để rỗng tên !")]
        [MaxLength(20)]
        [Display(Name = "Category Name")]
        public string Name {set;get;}

        
        
        [Range(1,100, ErrorMessage ="Số thứ tự hiển thị được phép từ 1 đến 100")]
        [DisplayName("Display Order")]
        [Required(ErrorMessage ="Số thứ tự hiển thị không được rỗng !")]
        public int DisplayOrder{set;get;}
    }
}