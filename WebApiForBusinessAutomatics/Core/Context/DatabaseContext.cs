using Core.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Core.Context
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DatabaseContext() : base("Database")
        {
            // Указывает EF, что если модель изменилась, нужно воссоздать базу данных с новой структурой
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserConfiguration());
        }

    }

    /// <summary>
    /// Класс конфигурации таблицы Users
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasRequired(u => u.Department).WithRequiredPrincipal(d => d.User);
        }
    }
}
