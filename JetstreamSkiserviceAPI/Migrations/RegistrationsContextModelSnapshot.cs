﻿// <auto-generated />
using System;
using JetstreamSkiserviceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JetstreamSkiserviceAPI.Migrations
{
    [DbContext(typeof(RegistrationsContext))]
    partial class RegistrationsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int?>("Attempts")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            Attempts = 0,
                            Password = "password",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Priority", b =>
                {
                    b.Property<int>("PriorityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriorityId"));

                    b.Property<string>("PriorityName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("PriorityId");

                    b.ToTable("Priority");

                    b.HasData(
                        new
                        {
                            PriorityId = 1,
                            PriorityName = "Tief"
                        },
                        new
                        {
                            PriorityId = 2,
                            PriorityName = "Standard"
                        },
                        new
                        {
                            PriorityId = 3,
                            PriorityName = "Express"
                        });
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegistrationId"));

                    b.Property<string>("Comment")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Create_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("Pickup_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("RegistrationId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StatusId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            ServiceId = 1,
                            ServiceName = "Kleiner Service"
                        },
                        new
                        {
                            ServiceId = 2,
                            ServiceName = "Grosser Service"
                        },
                        new
                        {
                            ServiceId = 3,
                            ServiceName = "Rennski Service"
                        },
                        new
                        {
                            ServiceId = 4,
                            ServiceName = "Bindungen montieren und einstellen"
                        },
                        new
                        {
                            ServiceId = 5,
                            ServiceName = "Fell zuschneiden"
                        },
                        new
                        {
                            ServiceId = 6,
                            ServiceName = "Heisswachsen"
                        });
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            StatusName = "Offen"
                        },
                        new
                        {
                            StatusId = 2,
                            StatusName = "InArbeit"
                        },
                        new
                        {
                            StatusId = 3,
                            StatusName = "abgeschlossen"
                        });
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Registration", b =>
                {
                    b.HasOne("JetstreamSkiserviceAPI.Models.Priority", "Priority")
                        .WithMany("Registrations")
                        .HasForeignKey("PriorityId");

                    b.HasOne("JetstreamSkiserviceAPI.Models.Service", "Service")
                        .WithMany("Registrations")
                        .HasForeignKey("ServiceId");

                    b.HasOne("JetstreamSkiserviceAPI.Models.Status", "Status")
                        .WithMany("Registrations")
                        .HasForeignKey("StatusId");

                    b.Navigation("Priority");

                    b.Navigation("Service");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Priority", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Service", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("JetstreamSkiserviceAPI.Models.Status", b =>
                {
                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
