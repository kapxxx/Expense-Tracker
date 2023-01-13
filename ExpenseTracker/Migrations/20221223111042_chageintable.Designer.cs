﻿// <auto-generated />
using System;
using ExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseTracker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221223111042_chageintable")]
    partial class chageintable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExpenseTracker.Models.Category", b =>
                {
                    b.Property<int>("C_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("C_Id"));

                    b.Property<int>("C_Expense_Limit")
                        .HasColumnType("int");

                    b.Property<string>("C_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("C_Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Expense", b =>
                {
                    b.Property<int>("E_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("E_Id"));

                    b.Property<int>("C_id")
                        .HasColumnType("int");

                    b.Property<int>("CategoryC_Id")
                        .HasColumnType("int");

                    b.Property<int>("E_Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("E_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("E_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("E_Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("E_Id");

                    b.HasIndex("CategoryC_Id");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Expense", b =>
                {
                    b.HasOne("ExpenseTracker.Models.Category", "Category")
                        .WithMany("expenses")
                        .HasForeignKey("CategoryC_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Category", b =>
                {
                    b.Navigation("expenses");
                });
#pragma warning restore 612, 618
        }
    }
}