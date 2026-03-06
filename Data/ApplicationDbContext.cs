using ManageAccountApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageAccountApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        // DbSet cho các bảng trong database
        public DbSet<Account> Accounts { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình bảng Account
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("ACCOUNTS");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("ID");
                
                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(200)
                    .IsRequired();

                // Cấu hình quan hệ 1-1 với SavingsAccount
                entity.HasOne(a => a.SavingsAccount)
                    .WithOne()
                    .HasForeignKey<SavingsAccount>("AccountId")
                    .OnDelete(DeleteBehavior.Cascade);

                // Cấu hình quan hệ 1-1 với CheckingAccount
                entity.HasOne(a => a.CheckingAccount)
                    .WithOne()
                    .HasForeignKey<CheckingAccount>("AccountId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình bảng SavingsAccount
            modelBuilder.Entity<SavingsAccount>(entity =>
            {
                entity.ToTable("SAVINGS_ACCOUNTS");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("ID");
                
                entity.Property(e => e.AccountId)
                    .HasColumnName("ACCOUNT_ID");
                
                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(100);
                
                entity.Property(e => e.Balance)
                    .HasColumnName("BALANCE")
                    .HasColumnType("NUMBER(18,2)");
                
                entity.Property(e => e.InterestRate)
                    .HasColumnName("INTEREST_RATE")
                    .HasColumnType("NUMBER(5,2)");
            });

            // Cấu hình bảng CheckingAccount
            modelBuilder.Entity<CheckingAccount>(entity =>
            {
                entity.ToTable("CHECKING_ACCOUNTS");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .HasColumnName("ID");
                
                entity.Property(e => e.AccountId)
                    .HasColumnName("ACCOUNT_ID");
                
                entity.Property(e => e.Type)
                    .HasColumnName("TYPE")
                    .HasMaxLength(100);
                
                entity.Property(e => e.Balance)
                    .HasColumnName("BALANCE")
                    .HasColumnType("NUMBER(18,2)");
                
                entity.Property(e => e.InterestRate)
                    .HasColumnName("INTEREST_RATE")
                    .HasColumnType("NUMBER(5,2)");
            });
        }
    }
}
