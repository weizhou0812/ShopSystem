﻿using Microsoft.EntityFrameworkCore;
using ShopSystem.Models;

namespace ShopSystem.Data
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext (DbContextOptions<ShopDbContext> options) : base(options) {}
        public DbSet<Users> users { get; set; }
        public DbSet<Employees> employees { get; set; }
        public DbSet<Products> products { get; set; }
    }
}
