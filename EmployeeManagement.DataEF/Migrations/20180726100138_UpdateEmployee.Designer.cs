﻿// <auto-generated />
using System;
using EmployeeManagement.DataEF.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.DataEF.Migrations
{
    [DbContext(typeof(ManagementContext))]
    [Migration("20180726100138_UpdateEmployee")]
    partial class UpdateEmployee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeeManagement.DataEF.Entities.Department", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("EmployeeManagement.DataEF.Entities.Employee", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("ManagerId");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50);

                    b.Property<int?>("Position");

                    b.Property<int>("Profession");

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("EmployeeManagement.DataEF.Entities.Settings", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("Language");

                    b.Property<int>("Topic");

                    b.HasKey("UserId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("EmployeeManagement.DataEF.Entities.User", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EmployeeManagement.DataEF.Entities.Employee", b =>
                {
                    b.HasOne("EmployeeManagement.DataEF.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmployeeManagement.DataEF.Entities.Settings", b =>
                {
                    b.HasOne("EmployeeManagement.DataEF.Entities.User", "User")
                        .WithOne("Settings")
                        .HasForeignKey("EmployeeManagement.DataEF.Entities.Settings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
