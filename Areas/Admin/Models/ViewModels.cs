using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book.Areas.Admin.Models
{
    public class ViewModels
    {
        public Products Product{set;get;}
        [ValidateNever]
        public IEnumerable<SelectListItem> listCategories{set;get;}
    }
}