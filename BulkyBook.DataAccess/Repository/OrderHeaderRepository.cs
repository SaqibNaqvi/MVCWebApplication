using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Model;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDBContext _db;
        public OrderHeaderRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(OrderHeader obj)
        {

            _db.OrderHeader.Update(obj);
        }

        public void UpdateStatus(int id, string orderstatus, string? paymentstatus = null)
        {
            var orderFromDB = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            if (orderFromDB != null)
            {
                orderFromDB.OrderStatus = orderstatus;
                if (paymentstatus != null)
                {
                    orderFromDB.PaymentStatus = paymentstatus;
                }
            }

        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDB = _db.OrderHeader.FirstOrDefault(u => u.Id == id);
            orderFromDB.PaymentDate = DateTime.Now;
            orderFromDB.SessionId = sessionId;
            orderFromDB.PaymentIntentId = paymentIntentId;
        }
    }
}
