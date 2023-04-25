using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Areas.Admin.Models
{
    public class CartItem
    {
        public int quantity {set; get;}
        public Products product {set; get;}
    }
}