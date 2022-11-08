using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDBContext _db;
        public OrderDetailRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(OrderDetail obj)
        {
            
            _db.OrderDetail.Update(obj);
        }
    }
}
