﻿
namespace PetControlSystem.Domain.Entities
{
    public class Product : Entity
    {
        public string? Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string? Description { get; private set; }

        /* EF Relations */
        public List<OrderProduct> OrderProducts { get; private set; } = [];

        public Product() { }

        public Product(string? name, decimal price, int stock, string? description)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
        }

        public void Update(string? name, decimal price, int stock, string? description)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
        }
    }
}
