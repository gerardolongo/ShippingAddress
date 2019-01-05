using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace GestioneIndirizzi.Models
{
    public partial class GestioneIndirizziContext : DbContext
    {
        public virtual DbSet<BillingAddressAudit> BillingAddressAudit { get; set; }
        public virtual DbSet<BillingAddress> BillingAddress { get; set; }
        public virtual DbSet<ShippingAddress> ShippingAddress { get; set; }
        public virtual DbSet<ShippingAddressAudit> ShippingAddressAudit { get; set; }
        public virtual DbSet<LogInfo> Loginfo { get; set; }
        public virtual DbSet<Tenant> Tenant { get; set; }

        public IConfiguration Configuration { get; }

        //public GestioneIndirizziContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public GestioneIndirizziContext(DbContextOptions<GestioneIndirizziContext> options, IConfiguration configuration) 
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.ToTable("Shipping_Address");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.city)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.company)
                    .IsRequired()
                    .HasColumnName("company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.first_name)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.label)
                    .IsRequired()
                    .HasColumnName("label")
                    .IsUnicode(false);

                entity.Property(e => e.last_name)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.postal_code)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.tenant_id).HasColumnName("tenant_id");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.ShippingAddress)
                    .HasForeignKey(d => d.tenant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_1_Tenant");
            });

            modelBuilder.Entity<BillingAddress>(entity =>
            {
                entity.ToTable("Billing_Address");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.city)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.company)
                    .IsRequired()
                    .HasColumnName("company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.first_name)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.fiscal_code)
                    .IsRequired()
                    .HasColumnName("fiscal_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.label)
                    .IsRequired()
                    .HasColumnName("label")
                    .IsUnicode(false);

                entity.Property(e => e.last_name)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.postal_code)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(2)
                    .IsUnicode(false);


                entity.Property(e => e.email)
                  .IsRequired()
                  .HasColumnName("email")
                  .HasMaxLength(50)
                  .IsUnicode(false);


                entity.Property(e => e.vat_code)
                    .IsRequired()
                    .HasColumnName("vat_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.tenant_id).HasColumnName("tenant_id");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.BillingAddress)
                    .HasForeignKey(d => d.tenant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_2_Tenant");
            });

            modelBuilder.Entity<BillingAddressAudit>(entity =>
            {
                entity.ToTable("Billing_Address_Audit");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.id_billing)
                   .IsRequired()
                   .HasColumnName("id_billing")
                   .HasMaxLength(60)
                   .IsUnicode(false);

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.city)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.company)
                    .IsRequired()
                    .HasColumnName("company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.first_name)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.fiscal_code)
                    .IsRequired()
                    .HasColumnName("fiscal_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.Property(e => e.last_name)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.postal_code)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(2)
                    .IsUnicode(false);


                entity.Property(e => e.email)
                  .IsRequired()
                  .HasColumnName("email")
                  .HasMaxLength(50)
                  .IsUnicode(false);


                entity.Property(e => e.vat_code)
                    .IsRequired()
                    .HasColumnName("vat_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.date_upd)
                   .IsRequired()
                   .HasColumnName("date_upd")
                   .HasMaxLength(50)
                   .IsUnicode(false);

            });

            modelBuilder.Entity<ShippingAddressAudit>(entity =>
            {
                entity.ToTable("Shipping_Address_Audit");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.id_shipping)
                   .IsRequired()
                   .HasColumnName("id_shipping")
                   .HasMaxLength(60)
                   .IsUnicode(false);

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.city)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.company)
                    .IsRequired()
                    .HasColumnName("company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.first_name)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);


                entity.Property(e => e.last_name)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.postal_code)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(2)
                    .IsUnicode(false);


                entity.Property(e => e.date_upd)
                   .IsRequired()
                   .HasColumnName("date_upd")
                   .HasMaxLength(50)
                   .IsUnicode(false);

            });

            modelBuilder.Entity<LogInfo>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.msg)
                    .IsRequired()
                    .HasColumnName("msg")
                    .IsUnicode(false);

                entity.Property(e => e.log_type)
                    .IsRequired()
                    .HasColumnName("log_type")
                    .IsUnicode(false);

                entity.Property(e => e.date)
                    .IsRequired()
                    .HasColumnName("date")
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.tenant_id)
                    .IsRequired()
                    .HasColumnName("tenant_id")
                    .IsUnicode(false);
            });

        }
    }
}
