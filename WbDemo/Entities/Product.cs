﻿namespace WbDemo.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}