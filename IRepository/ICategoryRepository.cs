using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;

namespace Book.IRepository
{
    public interface ICategoryRepository : IRepository<Categorys>
    {
        void Update(Categorys obj);

    
    }


}