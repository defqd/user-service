﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserWebAPI.Data.Contexts;

#nullable disable

namespace UserWebAPI.Data.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20230421181122_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("UserWebAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Admin")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Admin");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("TEXT")
                        .HasColumnName("BirthDay");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreatedOn");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Gender");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Login");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("ModifiedBy");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("TEXT")
                        .HasColumnName("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Password");

                    b.Property<string>("RevokedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("RevokedBy");

                    b.Property<DateTime?>("RevokedOn")
                        .HasColumnType("TEXT")
                        .HasColumnName("RevokedOn");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("58cec208-48ce-4491-9a0c-beb0deaa9b5d"),
                            Admin = true,
                            BirthDay = new DateTime(2023, 4, 21, 21, 11, 22, 195, DateTimeKind.Local).AddTicks(613),
                            CreatedBy = "GOD",
                            CreatedOn = new DateTime(2023, 4, 21, 21, 11, 22, 195, DateTimeKind.Local).AddTicks(625),
                            Gender = 2,
                            Login = "admin",
                            Name = "Админ",
                            Password = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
