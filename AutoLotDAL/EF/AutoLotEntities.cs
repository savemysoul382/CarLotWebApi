using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using AutoLotDAL.Interception;
using AutoLotDAL.Models;

namespace AutoLotDAL.EF
{
    public partial class AutoLotEntities : DbContext
    {
        static readonly DatabaseLogger DatabaseLogger = new DatabaseLogger("sqllog.txt", true);

        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            //DbInterception.Add(new ConsoleWriterInterceptor());
            DatabaseLogger.StartLogging();
            DbInterception.Add(DatabaseLogger);

            // Код перехватчика.
            //var context = (this as IObjectContextAdapter).ObjectContext;
            //context.ObjectMaterialized += ContextOnObjectMaterialized;
            //context.SavingChanges += ContextOnSavingChanges;
        }

        public virtual DbSet<CreditRisk> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<inventory> Cars { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.Custld);

            modelBuilder.Entity<inventory>()
                .Property(e => e.Make)
                .IsFixedLength();

            modelBuilder.Entity<inventory>()
                .Property(e => e.Color)
                .IsFixedLength();

            modelBuilder.Entity<inventory>()
                .Property(e => e.PetName)
                .IsFixedLength();

            modelBuilder.Entity<inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .HasForeignKey(e => e.Carld)
                .WillCascadeOnDelete(false);
        }

        private void ContextOnObjectMaterialized(Object sender, ObjectMaterializedEventArgs e)
        {
        }

        private void ContextOnSavingChanges(Object sender, EventArgs e)
        {
            // Параметр sender имеет тип ObjectContext.
            // Можно получать текущие и исходные значения,
            //а также отменять/модифицировать операцию
            // сохранения любым желаемым образом,
            var context = sender as ObjectContext;
            if (context == null) return;
            foreach (ObjectStateEntry item in
                context.ObjectStateManager.GetObjectStateEntries(
                    EntityState.Modified | EntityState.Added))
            {
                // Делать здесь что-то важное.
                if ((item.Entity as inventory) != null)
                {
                    var entity = (inventory)item.Entity;
                    if (entity.Color == "Red")
                    {
                        item.RejectPropertyChanges(nameof(entity.Color));
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            DbInterception.Remove(DatabaseLogger);
            DatabaseLogger.StopLogging();
            base.Dispose(disposing);
        }
    }
}