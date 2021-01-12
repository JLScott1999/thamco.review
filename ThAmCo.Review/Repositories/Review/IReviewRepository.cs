namespace ThAmCo.Review.Repositories.Review
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ThAmCo.Review.Models;

    public interface IReviewRepository : IRepositoryInsert<ProductReviewModel>
    {

        public IEnumerable<ProductReviewModel> GetProductReviews(Guid id);

    }
}
