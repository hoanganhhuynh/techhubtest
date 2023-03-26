using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
	public sealed class TodoDbContext : DbContext
	{
		public DbSet<TodoEntity> Todos { get; set; }
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
        public TodoDbContext()
		{
		}

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Validate();
            BeforeOnChange();
            var count = await base.SaveChangesAsync(cancellationToken);
            return count;
        }

        private void Validate()
        {
            var changedEntities = ChangeTracker
                .Entries()
                .Where(_ => _.State == EntityState.Added || _.State == EntityState.Modified);

            var errors = new List<ValidationResult>();
            foreach (var e in changedEntities.ToList())
            {
                var vc = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(
                    e.Entity, vc, errors, validateAllProperties: true);
            }
        }

        private void BeforeOnChange()
        {
            ChangeTracker.DetectChanges();

            ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList().ForEach(entry =>
            {
                entry.Property(nameof(BaseEntity.Id)).CurrentValue = Guid.NewGuid();
                entry.Property(nameof(BaseEntity.CreatedDate)).CurrentValue = DateTime.UtcNow;
                entry.Property(nameof(BaseEntity.UpdateDate)).CurrentValue = DateTime.UtcNow;
            });

            ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList().ForEach(entry =>
            {
                entry.Property(nameof(BaseEntity.UpdateDate)).CurrentValue = DateTime.UtcNow;
            });
        }
    }
}

