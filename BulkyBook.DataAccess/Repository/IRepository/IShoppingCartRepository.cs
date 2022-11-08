using BulkyBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int IncreamentCount(ShoppingCart shoppingCart, int count);
        int DecreamentCount(ShoppingCart shoppingCart, int count);

    }
}
