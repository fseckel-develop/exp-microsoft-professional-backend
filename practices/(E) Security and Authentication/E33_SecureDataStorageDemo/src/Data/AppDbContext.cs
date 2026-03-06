using Microsoft.EntityFrameworkCore;
using SecureDataStorageDemo.Models;

namespace SecureDataStorageDemo.Data;

public class AppDbContext : DbContext
{
    public DbSet<Message> Messages => Set<Message>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}