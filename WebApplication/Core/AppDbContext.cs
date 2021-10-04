using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Core
{
	public class AppDbContext : DbContext
	{
		public DbSet<Call> Calls { get; set; }
		public DbSet<Cargo> Cargos { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Payer> Payers { get; set; }
		public DbSet<Receiver> Receivers { get; set; }
		public DbSet<Request> Requests { get; set; }
		public DbSet<Sender> Senders { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Call>(entity => { Call.SetModelBuilder(entity); });
			modelBuilder.Entity<Request>(entity => { Request.SetModelBuilder(entity); });

			modelBuilder.Entity<Cargo>(entity => { Cargo.SetModelBuilder(entity); });
			modelBuilder.Entity<Sender>(entity => { Sender.SetModelBuilder(entity); });
			modelBuilder.Entity<Receiver>(entity => { Receiver.SetModelBuilder(entity); });
			modelBuilder.Entity<Payer>(entity => { Payer.SetModelBuilder(entity); });
			modelBuilder.Entity<Order>(entity => { Order.SetModelBuilder(entity); });
		}
	}
}