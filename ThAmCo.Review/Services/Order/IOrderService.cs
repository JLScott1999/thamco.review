namespace ThAmCo.Review.Services.Order
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ThAmCo.Review.Models;

    public interface IOrderService : IService
    {

        public Task<IEnumerable<ProductOrderModel>> HasOrderedAsync(Guid id);

    }
}
