using Domain.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
namespace Domain.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("WebData")
        {
            // Указывает EF, что если модель изменилась, нужно воссоздать базу данных с новой структурой
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<LegalRequisites> LegalRequisites { get; set; }
        public DbSet<LegalAddress> LegalAddresses { get; set; }
        public DbSet<LegalContactAddress> LegalContactAddresses { get; set; }
        public DbSet<IndividualRequisites> IndividualRequisites { get; set; }
        public DbSet<IndividualAddress> IndividualAddresses { get; set; }
        public DbSet<PhysicalRequisites> PhysicalRequisites { get; set; }
        public DbSet<PhysicalAddress> PhysicalAddresses { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceState> ServiceStates { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CallLog> CallLogs { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new LegalRequisitesConfiguration());
            modelBuilder.Configurations.Add(new IndividualRequisitesConfiguration());
            modelBuilder.Configurations.Add(new PhysicalRequisitesConfiguration());
            modelBuilder.Configurations.Add(new SeviceConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfigurarion());
        }
    }

    /// <summary>
    /// Конфигурация таблицы Users
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {

            HasRequired(ur => ur.LegalRequisites).WithRequiredPrincipal(lr => lr.User);
            HasRequired(ur => ur.PhysicalRequisites).WithRequiredPrincipal(pr => pr.User);
            HasRequired(ur => ur.IndividualRequisites).WithRequiredPrincipal(ir => ir.User);
            HasMany(u => u.Services).WithRequired(s => s.User);
        }
    }

    /// <summary>
    /// Конфигурация таблицы LegalRequisites
    /// </summary>
    public class LegalRequisitesConfiguration : EntityTypeConfiguration<LegalRequisites>
    {
        public LegalRequisitesConfiguration()
        {
            HasRequired(lr => lr.LegalAddress).WithRequiredPrincipal(la => la.LegalRequisites);
            HasRequired(lr => lr.LegalContactAddress).WithRequiredPrincipal(lca => lca.LegalRequisites);
        }
    }

    /// <summary>
    /// Конфигурация таблицы IndividualRequisites
    /// </summary>
    public class IndividualRequisitesConfiguration : EntityTypeConfiguration<IndividualRequisites>
    {
        public IndividualRequisitesConfiguration()
        {
            HasRequired(ir => ir.IndividualAddress).WithRequiredPrincipal(ia => ia.IndividualRequisites);
        }
    }

    /// <summary>
    /// Конфигурация таблицы IndividualRequisites
    /// </summary>
    public class PhysicalRequisitesConfiguration : EntityTypeConfiguration<PhysicalRequisites>
    {
        public PhysicalRequisitesConfiguration()
        {
            HasRequired(pr => pr.PhysicalAddress).WithRequiredPrincipal(pa => pa.PhysicalRequisites);
        }
    }

    /// <summary>
    /// Конфигурация таблицы Sevices
    /// </summary>
    public class SeviceConfiguration : EntityTypeConfiguration<Service>
    {
        public SeviceConfiguration()
        {
            HasRequired(s => s.ServiceState).WithRequiredPrincipal(ss => ss.Service);
            HasRequired(s => s.CallLog).WithRequiredPrincipal(c => c.Service);
            HasRequired(s => s.TaskLog).WithRequiredPrincipal(t => t.Service);
        }
    }

    /// <summary>
    /// Конфигурация таблицы Employees
    /// </summary>
    public class EmployeeConfigurarion : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfigurarion()
        {
            HasMany(e => e.CallLogs).WithOptional(c => c.Employee);
        }
    }
}
