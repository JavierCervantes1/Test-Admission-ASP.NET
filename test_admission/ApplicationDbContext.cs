using Microsoft.EntityFrameworkCore;
using test_admission.Models;
namespace test_admission
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuider)
        {
            optionsBuider
                .UseMySql(
                    "server=127.0.0.1;port=3306;user=root;password=;database=db_testadmission", 
                    new MySqlServerVersion(new Version())
                 )
                .UseLoggerFactory(LoggerFactory.Create(b => b.AddConsole()
                    .AddFilter(level => level >= LogLevel.Information)
                ))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
