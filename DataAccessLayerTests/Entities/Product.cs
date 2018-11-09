using DataAccessLayer;
using Models;
using System;

namespace DataAccessLayerTests
{
    /// <summary>
    /// Business Entity for Product
    /// </summary>
    public class Product : Entity
    {
        public Product()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
