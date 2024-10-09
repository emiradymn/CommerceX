﻿// <auto-generated />
using System;
using CommerceX.Data.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommerceX.Web.Migrations
{
    [DbContext(typeof(ComDbContext))]
    [Migration("20241004125545_AddOrdersTables")]
    partial class AddOrdersTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CommerceX.Data.Concrete.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Vitamin",
                            Url = "vitamin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Protein Tozu",
                            Url = "protein-tozu"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Gainer",
                            Url = "gainer"
                        });
                });

            modelBuilder.Entity("CommerceX.Data.Concrete.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdressLine")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("OrderData")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CommerceX.Data.Concrete.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("CommerceX.Data.Concrete.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Önemli bir vitamin",
                            Name = "Product 1",
                            Price = 1200m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Önemli bir vitamin",
                            Name = "Product 2",
                            Price = 200m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Önemli bir vitamin",
                            Name = "Product 3",
                            Price = 2000m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Önemli bir vitamin",
                            Name = "Product 4",
                            Price = 300m
                        },
                        new
                        {
                            Id = 5,
                            Description = "Önemli bir vitamin",
                            Name = "Product 5",
                            Price = 6000m
                        },
                        new
                        {
                            Id = 6,
                            Description = "Önemli bir vitamin",
                            Name = "Product 6",
                            Price = 400m
                        });
                });

            modelBuilder.Entity("CommerceX.Data.Concrete.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 4
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 5
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 6
                        });
                });

            modelBuilder.Entity("CommerceX.Data.Concrete.OrderItem", b =>
                {
                    b.HasOne("CommerceX.Data.Concrete.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommerceX.Data.Concrete.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CommerceX.Data.Concrete.ProductCategory", b =>
                {
                    b.HasOne("CommerceX.Data.Concrete.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommerceX.Data.Concrete.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommerceX.Data.Concrete.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}