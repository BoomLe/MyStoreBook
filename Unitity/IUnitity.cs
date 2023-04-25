using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.IRepository;

namespace Book.Unitity
{
    public interface IUnitityOfWork
    {
        ICategoryRepository Category {get;}
        IProductRepository Product{get;}
        void Save();
    }
}