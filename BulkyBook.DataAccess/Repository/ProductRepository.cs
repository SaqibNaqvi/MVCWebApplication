using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _db;
        public ProductRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product product)
        {
            var productFromDB = _db.Products.FirstOrDefault(u => u.Id == product.Id);
            if(productFromDB != null)
            {
                productFromDB.Title = product.Title;
                productFromDB.ISBN = product.ISBN;
                productFromDB.ListPrice = product.ListPrice;
                productFromDB.Price = product.Price;
                productFromDB.Price50 = product.Price50;
                productFromDB.Price100 = product.Price100;
                productFromDB.Author = product.Author;  
                productFromDB.Description = product.Description;
                productFromDB.CategoryId = product.CategoryId;
                productFromDB.CoverTypeId = product.CoverTypeId;
                if(product.ImageURL != null)
                {
                    productFromDB.ImageURL = product.ImageURL;
                }
            }
        }
    }
}
