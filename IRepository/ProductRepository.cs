using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Areas.Admin.Models;
using Book.Data;
using Book.Models;

namespace Book.IRepository
{
    public class ProductRepository : Repository<Products>, IProductRepository
    {
        private readonly BookDbContext _db;
        public ProductRepository(BookDbContext db) : base (db)
        {
            _db = db;
            
        }

        public void Update(Products obj)
        {
            var objDb = _db.Products.FirstOrDefault(p => p.Id == obj.Id);
            if(objDb != null)
            {
                objDb.Title = obj.Title;
                objDb.Description = obj.Description;
                objDb.ISBN = obj.ISBN;
                objDb.Author = obj.Author;
                objDb.ListPrice = obj.ListPrice;
                objDb.Price = obj.Price;
                objDb.Price50 = obj.Price50;
                objDb.Price100 = obj.Price100;
                
                if(objDb.ImageUrl != null)
                {
                    objDb.ImageUrl = obj.ImageUrl;
                }
            }

        }


        
    }
}