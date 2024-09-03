﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsManagementSystem.Data;

#nullable disable

namespace ProductsManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductManagementSystem.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PartyID")
                        .HasColumnType("int");

                    b.Property<Guid>("PartyID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceID");

                    b.HasIndex("PartyID1");

                    b.ToTable("Invoices", (string)null);
                });

            modelBuilder.Entity("ProductManagementSystem.Models.InvoiceDetail", b =>
                {
                    b.Property<int>("InvoiceDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceDetailID"));

                    b.Property<int>("InvoiceID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceDetailID");

                    b.HasIndex("InvoiceID");

                    b.HasIndex("ProductID1");

                    b.ToTable("InvoiceDetail");
                });

            modelBuilder.Entity("ProductManagementSystem.Models.Party", b =>
                {
                    b.Property<Guid>("PartyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PartyCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartyID");

                    b.ToTable("Parties", (string)null);
                });

            modelBuilder.Entity("ProductManagementSystem.Models.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PartyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.HasIndex("PartyID");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("ProductManagementSystem.Models.ProductRate", b =>
                {
                    b.Property<int>("ProductRateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductRateID"));

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductID1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductRateID");

                    b.HasIndex("ProductID1");

                    b.ToTable("ProductRates", (string)null);
                });

            modelBuilder.Entity("ProductManagementSystem.Models.Invoice", b =>
                {
                    b.HasOne("ProductManagementSystem.Models.Party", "Party")
                        .WithMany("invoices")
                        .HasForeignKey("PartyID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Party");
                });

            modelBuilder.Entity("ProductManagementSystem.Models.InvoiceDetail", b =>
                {
                    b.HasOne("ProductManagementSystem.Models.Invoice", "Invoice")
                        .WithMany("InvoiceDeatilts")
                        .HasForeignKey("InvoiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProductManagementSystem.Models.Product", "Product")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("ProductID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductManagementSystem.Models.Product", b =>
                {
                    b.HasOne("ProductManagementSystem.Models.Party", null)
                        .WithMany("products")
                        .HasForeignKey("PartyID");
                });

            modelBuilder.Entity("ProductManagementSystem.Models.ProductRate", b =>
                {
                    b.HasOne("ProductManagementSystem.Models.Product", "Product")
                        .WithMany("ProductRates")
                        .HasForeignKey("ProductID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProductManagementSystem.Models.Invoice", b =>
                {
                    b.Navigation("InvoiceDeatilts");
                });

            modelBuilder.Entity("ProductManagementSystem.Models.Party", b =>
                {
                    b.Navigation("invoices");

                    b.Navigation("products");
                });

            modelBuilder.Entity("ProductManagementSystem.Models.Product", b =>
                {
                    b.Navigation("InvoiceDetails");

                    b.Navigation("ProductRates");
                });
#pragma warning restore 612, 618
        }
    }
}
