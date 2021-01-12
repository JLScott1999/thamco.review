namespace ThAmCo.Review.Services.Review
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using ThAmCo.Review.Models;
    using ThAmCo.Review.Repositories.Review;

    public class ReviewService : IReviewService
    {

        private readonly IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public IEnumerable<ProductReviewModel> GetProductReviews(Guid id)
        {
            return this.reviewRepository.GetProductReviews(id);
        }

        public bool SubmitProductReview(ProductReviewModel productReview) => throw new NotImplementedException();

    }
}
