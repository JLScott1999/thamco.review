namespace ThAmCo.Review.Services.Review
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ThAmCo.Review.Models;

    public interface IReviewService : IService
    {

        public IEnumerable<ProductReviewModel> GetProductReviews(Guid id);

    }
}
