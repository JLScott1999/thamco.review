namespace ThAmCo.Review.Repositories.Review
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ThAmCo.Review.Data;
    using ThAmCo.Review.Models;

    public class ReviewRepository : IReviewRepository
    {

        private readonly DataContext context;

        public ReviewRepository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<ProductReviewModel> GetProductReviews(Guid id) =>
            this.context.ReviewData
                .Where(x => x.ProductId.Equals(id))
                .Select(x =>
                    DataToModel(x)
                );

        public void Insert(ProductReviewModel model)
        {
            this.context.Add(ModelToData(model));
            this.context.SaveChanges();
        }

        private static ProductReviewModel DataToModel(ReviewData reviewData)
        {
            return new ProductReviewModel()
            {
                Id = reviewData.Id,
                ProductId = reviewData.ProductId,
                Date = reviewData.Date,
                Name = reviewData.Name,
                Description = reviewData.Description
            };
        }

        private static ReviewData ModelToData(ProductReviewModel reviewModel)
        {
            return new ReviewData()
            {
                ProductId = reviewModel.ProductId,
                Date = reviewModel.Date,
                Name = reviewModel.Name,
                Description = reviewModel.Description
            };
        }

    }
}
