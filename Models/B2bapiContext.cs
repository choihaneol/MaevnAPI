using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class B2bapiContext : DbContext
{
    public B2bapiContext()
    {
    }

    public B2bapiContext(DbContextOptions<B2bapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<CardInformation> CardInformations { get; set; }

    public virtual DbSet<Carrier> Carriers { get; set; }

    public virtual DbSet<CountryState> CountryStates { get; set; }

    public virtual DbSet<Locator> Locators { get; set; }

    public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

    public virtual DbSet<OrderHeaderLine> OrderHeaderLines { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=12litt\\mssqlserver12lit;Initial Catalog=B2BAPI;Integrated Security=True;User Id=b2bapiuser;password=W@ndi23;TrustServerCertificate=True;Trusted_Connection=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Address__3213E83FA8655247");

            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1).IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ErpAddressId).HasColumnName("erpAddressId");
            entity.Property(e => e.ErpCustomerId).HasColumnName("erpCustomerId");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Address__UserId__245D67DE");
        });

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Basket__B40CC6CD3859EA65");

            entity.ToTable("Basket");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
        });

        modelBuilder.Entity<CardInformation>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__CardInfo__55FECDAE2C469805");

            entity.ToTable("CardInformation");

            entity.Property(e => e.CardId).ValueGeneratedNever();
            entity.Property(e => e.CardHolderName).HasMaxLength(50);
            entity.Property(e => e.ErpCustomerId).HasColumnName("erpCustomerId");
            entity.Property(e => e.ExpirationDate).HasColumnType("date");
            entity.Property(e => e.LoginId).HasMaxLength(460);

            entity.HasOne(d => d.User).WithMany(p => p.CardInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CardInfor__UserI__25518C17");
        });

        modelBuilder.Entity<Carrier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carrier__3213E83F6FAF1233");

            entity.ToTable("Carrier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActiveFlag).HasColumnName("activeFlag");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Rno).HasColumnName("rno");
        });

        modelBuilder.Entity<CountryState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CountryS__3213E83F203A3DEC");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("abbreviation");
            entity.Property(e => e.ActiveFlag).HasColumnName("activeFlag");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Rno).HasColumnName("rno");
        });

        modelBuilder.Entity<Locator>(entity =>
        {
            entity.HasKey(e => e.LocatorId).HasName("PK__Locator__8E1EB90FC9D73796");

            entity.ToTable("Locator");

            entity.Property(e => e.LocatorId).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LoginId).HasMaxLength(450);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.ZipCode).HasMaxLength(30);
        });

        modelBuilder.Entity<OrderHeader>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderHea__C3905BCFC8DB4DAE");

            entity.ToTable("OrderHeader");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.ErpCustomerId).HasColumnName("erpCustomerId");
            entity.Property(e => e.ErpCustomerPo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("erpCustomerPO");
            entity.Property(e => e.Note).IsUnicode(false);
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RequestedShipDate).HasColumnType("date");
            entity.Property(e => e.ShipToAddress1).IsUnicode(false);
            entity.Property(e => e.ShipToAddress2).IsUnicode(false);
            entity.Property(e => e.ShipToCity).IsUnicode(false);
            entity.Property(e => e.ShipToCountryId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShipToName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShipToState).IsUnicode(false);
            entity.Property(e => e.ShipToZipcode).IsUnicode(false);
            entity.Property(e => e.UseDropShip)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderHeaderLine>(entity =>
        {
            entity.HasKey(e => e.OrderLineId).HasName("PK__OrderHea__29068A105AAD4F70");

            entity.ToTable("OrderHeaderLine");

            entity.Property(e => e.OrderLineId).ValueGeneratedNever();
            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Size)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StyleNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("money");
            entity.Property(e => e.TotalScannedPrice).HasColumnType("money");
            entity.Property(e => e.WholeSalePrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHeaderLines)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderHead__Order__2645B050");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A383597E8E7");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).ValueGeneratedNever();
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BalanceToUse).HasColumnType("money");
            entity.Property(e => e.CardHolderName).HasMaxLength(50);
            entity.Property(e => e.ErpCustomerId).HasColumnName("erpCustomerId");
            entity.Property(e => e.ErpCustomerPo)
                .HasMaxLength(100)
                .HasColumnName("erpCustomerPO");
            entity.Property(e => e.PaymentAmount).HasColumnType("money");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__Product__C5705938D73B1E14");

            entity.ToTable("Product");

            entity.Property(e => e.B2bActiveFlag).HasColumnName("b2bActiveFlag");
            entity.Property(e => e.ColorCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ErpActiveFlag).HasColumnName("erpActiveFlag");
            entity.Property(e => e.ErpProductId).HasColumnName("erpProductId");
            entity.Property(e => e.ErpProgramId).HasColumnName("erpProgramId");
            entity.Property(e => e.FabricContent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fit)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.GarmentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InseamLength)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemWeight)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LongDescription)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.NextAvailDate)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("nextAvailDate");
            entity.Property(e => e.ProductLine)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductUrlId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.QtyAvailable).HasColumnName("qtyAvailable");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sizes)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StyleNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitPrice).HasColumnType("money");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductC__3213E83FD7CEFD5C");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.B2bActiveFlag).HasColumnName("b2bActiveFlag");
            entity.Property(e => e.Colors)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ErpProgramId).HasColumnName("erpProgramId");
            entity.Property(e => e.FabricContent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fits)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GarmentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InseamLengths)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemWeight)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LongDescription)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.PriceMax).HasColumnType("money");
            entity.Property(e => e.PriceMin).HasColumnType("money");
            entity.Property(e => e.ProductLine)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sizes)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.StyleNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3213E83F7B8FD987");

            entity.ToTable("ProductImage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ColorCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ColorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductCategoryId).HasColumnName("productCategoryId");
            entity.Property(e => e.ProductUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StyleNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Role__1788CC4C28D2A2ED");

            entity.ToTable("Role");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.RoleCancelation).HasColumnName("Role_Cancelation");
            entity.Property(e => e.RoleInvoice).HasColumnName("Role_Invoice");
            entity.Property(e => e.RoleMapPrice).HasColumnName("Role_MapPrice");
            entity.Property(e => e.RolePayment).HasColumnName("Role_Payment");
            entity.Property(e => e.RolePlaceOrder).HasColumnName("Role_PlaceOrder");
            entity.Property(e => e.RoleProductMaster).HasColumnName("Role_ProductMaster");
            entity.Property(e => e.RoleStatement).HasColumnName("Role_Statement");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CA122B7A1");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Credit).HasColumnType("money");
            entity.Property(e => e.ErpCustomerId).HasColumnName("erpCustomerId");
            entity.Property(e => e.LoginId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(450)
                .IsUnicode(false);
            entity.Property(e => e.PaymentTerm)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalCreditBalance).HasColumnType("money");
            entity.Property(e => e.TotalOpenBalance).HasColumnType("money");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishItemId).HasName("PK__Wishlist__0351A7FEF2EE519B");

            entity.ToTable("Wishlist");

            entity.Property(e => e.WishItemId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
