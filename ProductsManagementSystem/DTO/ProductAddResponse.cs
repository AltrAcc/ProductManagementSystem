using ProductManagementSystem.Models;
using System;

namespace ProductsManagementSystem.DTO
{
    public class ProductAddResponse
    {
        public int ProductID { get; set; }

        public string? ProductName { get; set; }

        public Decimal? ProductPrice { get; set; }

        public string? ProductDescription { get; set; }


        public override string ToString()
        {
            return $"Party ID: {ProductID}, Party Name: {ProductName}, Party Category: {ProductDescription}";
        }

    }

    public static class ProductExtensions
    {
        public static ProductAddResponse ToProductResponse(this Product product, ProductRate? productRate)
        {
            //product => convert => ProductAddResponse
            return new ProductAddResponse()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = productRate?.Rate
            };
        }
    }
}
