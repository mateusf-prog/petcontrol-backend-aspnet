﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetControlSystem.Data.Context;

#nullable disable

namespace PetControlSystem.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20250218010009_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppointmentPetSupport", b =>
                {
                    b.Property<Guid>("AppointmentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PetSupportsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AppointmentsId", "PetSupportsId");

                    b.HasIndex("PetSupportsId");

                    b.ToTable("AppointmentPetSupport");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.Property<Guid>("OrdersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrdersId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Complement")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("varchar(8)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<double?>("Weight")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.PetSupport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("LargeDogPrice")
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("MediumDogPrice")
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal?>("SmallDogPrice")
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("PetSupport");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(400)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("AppointmentPetSupport", b =>
                {
                    b.HasOne("PetControlSystem.Domain.Entities.Appointment", null)
                        .WithMany()
                        .HasForeignKey("AppointmentsId")
                        .IsRequired();

                    b.HasOne("PetControlSystem.Domain.Entities.PetSupport", null)
                        .WithMany()
                        .HasForeignKey("PetSupportsId")
                        .IsRequired();
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.HasOne("PetControlSystem.Domain.Entities.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .IsRequired();

                    b.HasOne("PetControlSystem.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .IsRequired();
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Appointment", b =>
                {
                    b.HasOne("PetControlSystem.Domain.Entities.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Customer", b =>
                {
                    b.HasOne("PetControlSystem.Domain.Entities.Address", "Address")
                        .WithOne("Customer")
                        .HasForeignKey("PetControlSystem.Domain.Entities.Customer", "AddressId")
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Order", b =>
                {
                    b.HasOne("PetControlSystem.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Pet", b =>
                {
                    b.HasOne("PetControlSystem.Domain.Entities.Customer", "Customer")
                        .WithMany("Pets")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.User", b =>
                {
                    b.HasOne("PetControlSystem.Domain.Entities.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("PetControlSystem.Domain.Entities.User", "AddressId")
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Address", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetControlSystem.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Orders");

                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
