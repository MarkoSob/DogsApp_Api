﻿// <auto-generated />
using System;
using DogsApp_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DogsApp_DAL.Migrations
{
    [DbContext(typeof(EfDbContext))]
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DogsApp_DAL.Entities.Dog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("TailLength")
                        .HasColumnType("float");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Dogs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9d25f40b-68de-4e7f-b76b-74f87f26f654"),
                            Color = "red & amber",
                            Name = "Neo",
                            TailLength = 22.0,
                            Weight = 32.0
                        },
                        new
                        {
                            Id = new Guid("44f8f00a-17e6-46d9-9e0c-be3326514c02"),
                            Color = "black & white",
                            Name = "Jessy",
                            TailLength = 7.0,
                            Weight = 14.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
