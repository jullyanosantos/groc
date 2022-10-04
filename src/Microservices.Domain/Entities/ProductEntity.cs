﻿namespace Microservices.Domain.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}