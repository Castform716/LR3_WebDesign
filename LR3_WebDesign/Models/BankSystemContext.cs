using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LR3_WebDesign.Models;

public partial class BankSystemContext : DbContext
{
    public BankSystemContext()
    {
    }

    public BankSystemContext(DbContextOptions<BankSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<InsuranceGiver> InsuranceGivers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-AQAD48J\\SQLEXPRESS;Database=BankSystem;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountNumber).HasName("PK__Account__BE2ACD6E4CFA75C3");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Itn, "IX_Account_ITN");

            entity.HasIndex(e => e.Usreou, "IX_Account_USREOU");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(17)
                .IsFixedLength();
            entity.Property(e => e.Balance).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.Currency)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.Itn)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("ITN");
            entity.Property(e => e.Usreou)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("USREOU");

            entity.HasOne(d => d.ItnNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Itn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__ITN__22AA2996");

            entity.HasOne(d => d.UsreouNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Usreou)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__USREOU__21B6055D");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Usreou).HasName("PK__Bank__E16F21C3F654D63D");

            entity.ToTable("Bank");

            entity.Property(e => e.Usreou)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("USREOU");
            entity.Property(e => e.Departments)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.OriginCountry)
                .HasMaxLength(15)
                .IsFixedLength();
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardNumber).HasName("PK__Card__A4E9FFE814BA8F9F");

            entity.ToTable("Card");

            entity.HasIndex(e => e.Itn, "IX_Card_ITN");

            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Cvv).HasColumnName("CVV");
            entity.Property(e => e.DateOfExpire).HasColumnType("date");
            entity.Property(e => e.Itn)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("ITN");
            entity.Property(e => e.Percentage).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.TypeOfCard)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.ItnNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.Itn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Card__ITN__164452B1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Itn).HasName("PK__Customer__C4978204CC9EB27C");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Customer__85FB4E381EC5770E").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053491D83182").IsUnique();

            entity.Property(e => e.Itn)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("ITN");
            entity.Property(e => e.Adress)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.LegalEntityIndividual)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("LegalEntity/Individual");
            entity.Property(e => e.Mfo).HasColumnName("MFO");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(16)
                .IsFixedLength();
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.HasIndex(e => e.FromBank, "IX_Department_FromBank");

            entity.Property(e => e.DepartmentId).ValueGeneratedNever();
            entity.Property(e => e.FromBank)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Specialization)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.WorkDays)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.FromBankNavigation).WithMany(p => p.DepartmentsNavigation)
                .HasForeignKey(d => d.FromBank)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Departmen__FromB__2B3F6F97");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Itn).HasName("PK__Employee__C4978204A7CB1A92");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Department, "IX_Employee_Department");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__85FB4E386F059E34").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534EC02EEF6").IsUnique();

            entity.Property(e => e.Itn)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("ITN");
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.Job)
                .HasMaxLength(18)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(18)
                .IsFixedLength();

            entity.HasOne(d => d.DepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Department)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__Depart__300424B4");
        });

        modelBuilder.Entity<InsuranceGiver>(entity =>
        {
            entity.HasKey(e => e.InsuranceUsreou).HasName("PK__Insuranc__863B01ED95F48418");

            entity.ToTable("InsuranceGiver");

            entity.HasIndex(e => e.InsuranceObject, "IX_InsuranceGiver_InsuranceObject");

            entity.Property(e => e.InsuranceUsreou)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("InsuranceUSREOU");
            entity.Property(e => e.BankCountry)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Bank/Country");
            entity.Property(e => e.InsuranceObject)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.InsuranceObjectNavigation).WithMany(p => p.InsuranceGivers)
                .HasForeignKey(d => d.InsuranceObject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Insurance__Insur__286302EC");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasIndex(e => e.CardNumber, "IX_Transactions_CardNumber");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("TransactionID");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .IsFixedLength();

            entity.HasOne(d => d.CardNumberNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CardNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__CardN__1A14E395");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
