﻿using CrudWithEntity.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudWithEntity.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
}
