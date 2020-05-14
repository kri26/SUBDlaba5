using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lab5
{
    class DatabaseContext: DbContext
    {
        private string server;
        private string port;
        private string username;
        private string database;

        public DatabaseContext(string server, string port, string username, string database)
        {
            this.server = server;
            this.port = port;
            this.username = username;
            this.database = database;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String connectionString = "Server=" + server + ";" +
                                      "Port=" + port + ";" +
                                      "Username=" + username + ";" +
                                      "Database=" + database + ";";

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("seq_worker").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_money").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_number").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_customer").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_position").IncrementsBy(1).StartsAt(1);
            modelBuilder.HasSequence<int>("seq_transport").IncrementsBy(1).StartsAt(1);

            modelBuilder.Entity<Worker>(entity => { entity.Property(e => e.Id).UseHiLo("seq_worker"); });
            modelBuilder.Entity<Money>(entity => { entity.Property(e => e.Id).UseHiLo("seq_money"); });
            modelBuilder.Entity<Number>(entity => { entity.Property(e => e.Id).UseHiLo("seq_number"); });
            modelBuilder.Entity<Customer>(entity => { entity.Property(e => e.Id).UseHiLo("seq_customer"); });
            modelBuilder.Entity<Orders>(entity => { entity.Property(e => e.Id).UseHiLo("seq_orders"); });
            modelBuilder.Entity<Transport>(entity => { entity.Property(e => e.Id).UseHiLo("seq_transport"); });
        }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Money> Moneys { get; set; }
        public DbSet<Number> Numbers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Transport> Transports { get; set; }
    }
}
