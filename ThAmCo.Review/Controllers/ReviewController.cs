namespace ThAmCo.Review.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using ThAmCo.Review.Models;
    using ThAmCo.Review.Services.Review;

    [ApiController]
    [FormatFilter]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        [Route("product/{id}")]
        public IEnumerable<ProductReviewModel> GetProductReview(Guid id)
        {
            return this.reviewService.GetProductReviews(id);
        }

    }
}
