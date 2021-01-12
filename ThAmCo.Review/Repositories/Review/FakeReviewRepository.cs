namespace ThAmCo.Review.Repositories.Review
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ThAmCo.Review.Models;

    public class FakeReviewRepository : IReviewRepository
    {
        public IEnumerable<ProductReviewModel> Get() => throw new NotImplementedException();

        public ProductReviewModel Get(Guid id) => throw new NotImplementedException();

        public IEnumerable<ProductReviewModel> GetProductReviews(Guid productId) => throw new NotImplementedException();

        public void Insert(ProductReviewModel model) => throw new NotImplementedException();

    }
}
