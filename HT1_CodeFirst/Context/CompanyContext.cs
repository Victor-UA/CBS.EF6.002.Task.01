namespace HT1_CodeFirst.Context
{
    using HT1_CodeFirst.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CompanyContext : DbContext
    {
        static CompanyContext()
        {
            Database.SetInitializer(new CompanyContextInitializer());
        }
        // Your context has been configured to use a 'CompanyContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'HT1_CodeFirst.Context.CompanyContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CompanyContext' 
        // connection string in the application configuration file.
        public CompanyContext()
            : base("name=CompanyContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Contragent> Contragents { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Audience> Audiences { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}