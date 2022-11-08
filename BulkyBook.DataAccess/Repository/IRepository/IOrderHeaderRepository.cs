using BulkyBook.Model;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);

        void UpdateStatus(int id, string orderstatus, string? paymentstatus = null);
        void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);

    }
}
