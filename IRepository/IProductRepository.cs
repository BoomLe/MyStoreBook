using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Areas.Admin.Models;
using Book.Models;

namespace Book.IRepository
{
    public interface IProductRepository : IRepository<Products>
    {
        void Update(Products obj);

    
    }


}