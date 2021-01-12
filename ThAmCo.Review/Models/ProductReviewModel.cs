namespace ThAmCo.Review.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductReviewModel
    {

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

    }
}
