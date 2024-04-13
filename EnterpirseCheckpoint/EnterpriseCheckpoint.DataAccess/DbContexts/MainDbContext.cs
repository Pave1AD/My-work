﻿using EnterpriseCheckpoint.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EnterpriseCheckpoint.DataAccess.DbContexts
{
    public class MainDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MainDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDayAssignment> EmployeeDayAssignments { get; set; }
        public DbSet<SalaryRate> EmployeeSalaryRates { get; set; }
        public DbSet<EmployeeHoursWorked> EmployeeHoursWorked { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SQLProviderConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
        }
    }
}
