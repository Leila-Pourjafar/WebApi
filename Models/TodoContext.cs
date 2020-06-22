using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Api.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Api.Configuration;
namespace Api.Models
{
    public class TodoContext : IdentityDbContext<User, ApplicationRole,
    int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.Entity<User>()
          .HasMany<Post>(g => g.Posts)
          .WithOne(x => x.CurrentUser)
          .HasForeignKey(x => x.CreatorId);
        }
        // public DbSet<Api.Models.ViewModel.UserRegistrationModel> UserRegistrationModel { get; set; }
    }
    //public class ApplicationContext : IdentityDbContext<User>
    //{
    //    public ApplicationContext(DbContextOptions options)
    //    : base(options)
    //    {
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //       // modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    //    }

    //    public DbSet<TodoItem> TodoItems { get; set; }
    //}
}
