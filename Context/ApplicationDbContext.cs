using BudgetApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BudgetApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public  DbSet<ManualMonthlyExpense> ManualMonthlyExpenses { get; set; }
        public  DbSet<MasExpense> MasExpenses { get; set; }
        public  DbSet<MasMonthlyExpense> MasMonthlyExpenses { get; set; }
        public  DbSet<MonthlyExpense> MonthlyExpenses { get; set; }
        public  DbSet<ManualMonthlyCreditExpense> ManualMonthlyCreditExpenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DevConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ManualMonthlyCreditExpense>(entity =>
            {
                entity.HasKey(e => e.ManualMonthlyCreditExpensesId)
                    .HasName("PK_88");

                entity.ToTable("manual_monthly_credit_expenses");

                entity.HasIndex(e => e.MasMonthlyExpensesId, "FK_91");

                entity.Property(e => e.ManualMonthlyCreditExpensesId).HasColumnName("manual_monthly_credit_expenses_id");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("description");

                entity.Property(e => e.MasMonthlyExpensesId).HasColumnName("mas_monthly_expenses_id");

                entity.Property(e => e.Payment)
                    .HasColumnType("money")
                    .HasColumnName("payment");

                //entity.HasOne(d => d.MasMonthlyExpenses)
                //    .WithMany(p => p.ManualMonthlyCreditExpenses)
                //    .HasForeignKey(d => d.MasMonthlyExpensesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_89");
            });

            modelBuilder.Entity<ManualMonthlyExpense>(entity =>
            {
                entity.HasKey(e => e.ManualMonthlyExpensesId)
                    .HasName("PK_65");

                entity.ToTable("manual_monthly_expenses");

                entity.HasIndex(e => e.MasMonthlyExpensesId, "FK_83");

                entity.Property(e => e.ManualMonthlyExpensesId).HasColumnName("manual_monthly_expenses_id");

                entity.Property(e => e.Budget)
                    .HasColumnType("money")
                    .HasColumnName("budget");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.MasMonthlyExpensesId).HasColumnName("mas_monthly_expenses_id");

                entity.Property(e => e.Payment)
                    .HasColumnType("money")
                    .HasColumnName("payment");

                //entity.HasOne(d => d.MasMonthlyExpenses)
                //    .WithMany(p => p.ManualMonthlyExpenses)
                //    .HasForeignKey(d => d.MasMonthlyExpensesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_81");
            });

            modelBuilder.Entity<MasExpense>(entity =>
            {
                entity.HasKey(e => e.MasExpensesId)
                    .HasName("PK_6");

                entity.ToTable("mas_expenses");

                entity.Property(e => e.MasExpensesId).HasColumnName("mas_expenses_id");

                entity.Property(e => e.BiweeklyNumber).HasColumnName("biweekly_number");

                entity.Property(e => e.Budget)
                    .HasColumnType("money")
                    .HasColumnName("budget");

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<MasMonthlyExpense>(entity =>
            {
                entity.HasKey(e => e.MasMonthlyExpensesId)
                    .HasName("PK_10");

                entity.ToTable("mas_monthly_expenses");

                entity.Property(e => e.MasMonthlyExpensesId).HasColumnName("mas_monthly_expenses_id");

                entity.Property(e => e.BiweeklyNumber).HasColumnName("biweekly_number");

                entity.Property(e => e.Income)
                    .HasColumnType("money")
                    .HasColumnName("income");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<MonthlyExpense>(entity =>
            {
                entity.HasKey(e => e.MontlyExpensesId)
                    .HasName("PK_50");

                entity.ToTable("monthly_expenses");

                entity.HasIndex(e => e.MasMonthlyExpensesId, "FK_53");

                entity.HasIndex(e => e.MasExpensesId, "FK_62");

                entity.Property(e => e.MontlyExpensesId).HasColumnName("montly_expenses_id");

                entity.Property(e => e.MasExpensesId).HasColumnName("mas_expenses_id");

                entity.Property(e => e.MasMonthlyExpensesId).HasColumnName("mas_monthly_expenses_id");

                entity.Property(e => e.Payment)
                    .HasColumnType("money")
                    .HasColumnName("payment");

                //entity.HasOne(d => d.MasExpenses)
                //    .WithMany(p => p.MonthlyExpenses)
                //    .HasForeignKey(d => d.MasExpensesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_60");

                //entity.HasOne(d => d.MasMonthlyExpenses)
                //    .WithMany(p => p.MonthlyExpenses)
                //    .HasForeignKey(d => d.MasMonthlyExpensesId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_51");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
