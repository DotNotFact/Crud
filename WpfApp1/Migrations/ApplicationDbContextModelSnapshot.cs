﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WpfApp1.Data;

#nullable disable

namespace WpfApp1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WpfApp1.Models.Product", b =>
                {
                    b.Property<Guid>("Uid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Uid");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Uid = new Guid("11111111-1111-1111-1111-111111111111"),
                            CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Игровой ноутбук",
                            Name = "Ноутбук",
                            Price = 85000m,
                            Quantity = 5
                        },
                        new
                        {
                            Uid = new Guid("22222222-2222-2222-2222-222222222222"),
                            CreatedDate = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Беспроводная мышь",
                            Name = "Мышь",
                            Price = 2500m,
                            Quantity = 15
                        },
                        new
                        {
                            Uid = new Guid("33333333-3333-3333-3333-333333333333"),
                            CreatedDate = new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Механическая клавиатура",
                            Name = "Клавиатура",
                            Price = 7500m,
                            Quantity = 8
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
