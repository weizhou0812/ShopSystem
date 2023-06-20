﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopSystem.Data;

#nullable disable

namespace ShopSystem.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20230620013932_initdb")]
    partial class initdb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShopSystem.Models.Employees", b =>
                {
                    b.Property<int>("ENumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EAge")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EBirth")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EIdentity")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasComment("身份");

                    b.Property<string>("EName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ESex")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsOnJob")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ENumber");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("ShopSystem.Models.Products", b =>
                {
                    b.Property<int>("PNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Inventory")
                        .HasColumnType("int")
                        .HasComment("库存");

                    b.Property<bool>("IsExpire")
                        .HasColumnType("tinyint(1)")
                        .HasComment("是否过期");

                    b.Property<string>("PId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<DateTime>("ProduceDate")
                        .HasColumnType("datetime(6)")
                        .HasComment("生产日期");

                    b.Property<int>("SellbyDate")
                        .HasColumnType("int")
                        .HasComment("保质期");

                    b.Property<DateTime>("WarehousingDate")
                        .HasColumnType("datetime(6)")
                        .HasComment("入库时间");

                    b.HasKey("PNumber");

                    b.ToTable("products");
                });

            modelBuilder.Entity("ShopSystem.Models.Users", b =>
                {
                    b.Property<int>("UNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsOnJob")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UIdentity")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasComment("身份");

                    b.Property<string>("UName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UNumber");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
