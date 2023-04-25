using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Book.IRepository;

namespace Book.Unitity
{
    public class UnitityOfWork : IUnitityOfWork
    {
        private readonly BookDbContext _db;

        public ICategoryRepository Category {get; private set;}
        public IProductRepository Product {get; private set;}
        public UnitityOfWork(BookDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }

        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}