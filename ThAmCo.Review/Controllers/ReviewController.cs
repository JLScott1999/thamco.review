namespace ThAmCo.Review.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ThAmCo.Review.DTOs;
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

        [HttpPost]
        [Authorize]
        [Route("product/{id}")]
        public IActionResult SubmitProductReview([FromQuery] Guid id, [FromBody] ProductReviewDTO productReview)
        {
            //var fullNameClaim = ((ClaimsIdentity)this.HttpContext.User.Identity).FindFirst("FullName")?.Value;
            //if (fullNameClaim != null)
            //{ 
            if (this.reviewService.SubmitProductReview(
                new ProductReviewModel()
                {
                    Date = DateTime.Now,
                    Description = productReview.Description,
                    ProductId = id,
                    Name = "T7046516"
                })
            )
            {
                return this.Ok();
            }
            else
            {
                return this.BadRequest();
            }
            //}
            //else
            //{
            //    return this.Unauthorized("Unauthenticated user");
            //}
        }

    }
}
