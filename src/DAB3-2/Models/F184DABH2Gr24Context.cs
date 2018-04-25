using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAB32.Models
{
    public partial class F184DABH2Gr24Context : DbContext
    {
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<AddressNames> AddressNames { get; set; }
        public virtual DbSet<AddressTypes> AddressTypes { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<CountryCodes> CountryCodes { get; set; }
        public virtual DbSet<PersonAddresses> PersonAddresses { get; set; }
        public virtual DbSet<PersonAddressTypes> PersonAddressTypes { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<PrimaryAddresses> PrimaryAddresses { get; set; }
        public virtual DbSet<TelephoneCompanies> TelephoneCompanies { get; set; }

        public F184DABH2Gr24Context(DbContextOptions<F184DABH2Gr24Context> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasIndex(e => e.AddressNameId);

                entity.HasIndex(e => e.CityId);

                entity.HasOne(d => d.AddressName)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.AddressNameId);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<AddressTypes>(entity =>
            {
                entity.HasIndex(e => e.AddressId);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.AddressTypes)
                    .HasForeignKey(d => d.AddressId);
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasIndex(e => e.CountryCodeId)
                    .IsUnique();

                entity.HasOne(d => d.CountryCode)
                    .WithOne(p => p.Cities)
                    .HasForeignKey<Cities>(d => d.CountryCodeId);
            });

            modelBuilder.Entity<PersonAddresses>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.AddressId });

                entity.HasIndex(e => e.AddressId);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PersonAddresses)
                    .HasForeignKey(d => d.AddressId);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAddresses)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<PersonAddressTypes>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.AddressTypeId });

                entity.HasIndex(e => e.AddressTypeId);

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.PersonAddressTypes)
                    .HasForeignKey(d => d.AddressTypeId);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAddressTypes)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.HasIndex(e => e.PrimaryAddressId);

                entity.Property(e => e.Context).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.HasOne(d => d.PrimaryAddress)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.PrimaryAddressId);
            });

            modelBuilder.Entity<PhoneNumbers>(entity =>
            {
                entity.HasIndex(e => e.PersonId);

                entity.HasIndex(e => e.TelephoneCompanyId);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.PersonId);

                entity.HasOne(d => d.TelephoneCompany)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.TelephoneCompanyId);
            });

            modelBuilder.Entity<PrimaryAddresses>(entity =>
            {
                entity.HasIndex(e => e.AddressNameId);

                entity.HasIndex(e => e.CityId);

                entity.HasOne(d => d.AddressName)
                    .WithMany(p => p.PrimaryAddresses)
                    .HasForeignKey(d => d.AddressNameId);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PrimaryAddresses)
                    .HasForeignKey(d => d.CityId);
            });

            modelBuilder.Entity<TelephoneCompanies>(entity =>
            {
                entity.Property(e => e.CompanyName).IsRequired();
            });
        }
    }
}
