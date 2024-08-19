using Api.Models.Todo;

namespace MinimalApi.Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().ToTable("TodoItems", t => t.IsTemporal());
        modelBuilder.Entity<User>().ToTable("Users", u => u.IsTemporal());
        for (var i = 1; i <= 20; i++)
        {
            var salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                $"secret-{i}",
                salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                256 / 8));

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = i,
                Username = $"user{i}",
                Password = hashed,
                Email = $"user{i}@example.com",
                CreatedOn = DateTime.UtcNow,
                Salt = Convert.ToBase64String(salt),
                PermitLimit = 60,
                RateLimitWindowInMinutes = 5
            });
        }

        for (var i = 1; i <= 20; i++)
            modelBuilder.Entity<TodoItem>().HasData(new TodoItem
            {
                Id = i,
                Title = $"Todo Item {i}",
                IsCompleted = false,
                CreatedOn = DateTime.UtcNow,
                UserId = 1
            });
    }
}