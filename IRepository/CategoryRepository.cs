using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Book.Models;

namespace Book.IRepository
{
    public class CategoryRepository : Repository<Categorys>, ICategoryRepository
    {
        private readonly BookDbContext _db;
        public CategoryRepository(BookDbContext db) : base (db)
        {
            _db = db;
            
        }

        public void Update(Categorys obj)
        {
            _db.Categorys.Update(obj);

        }


        
    }
}