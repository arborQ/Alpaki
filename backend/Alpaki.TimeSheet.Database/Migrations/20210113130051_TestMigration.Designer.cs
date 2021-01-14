﻿// <auto-generated />
using System;
using Alpaki.TimeSheet.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alpaki.TimeSheet.Database.Migrations
{
    [DbContext(typeof(TimeSheetDatabaseContext))]
    [Migration("20210113130051_TestMigration")]
    partial class TestMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alpaki.TimeSheet.Database.Models.Project", b =>
                {
                    b.Property<long>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("Project","TimeSheet");
                });

            modelBuilder.Entity("Alpaki.TimeSheet.Database.Models.TimeEntry", b =>
                {
                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<decimal>("Hours")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.HasKey("Year", "Month", "UserId", "Day");

                    b.HasIndex("ProjectId");

                    b.ToTable("TimeEntry","TimeSheet");
                });

            modelBuilder.Entity("Alpaki.TimeSheet.Database.Models.TimeSheetPeriod", b =>
                {
                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LockedFrom")
                        .HasColumnType("datetime2");

                    b.HasKey("Year", "Month", "UserId");

                    b.ToTable("TimeSheetPeriod","TimeSheet");
                });

            modelBuilder.Entity("Alpaki.TimeSheet.Database.Models.TimeEntry", b =>
                {
                    b.HasOne("Alpaki.TimeSheet.Database.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("Alpaki.TimeSheet.Database.Models.TimeSheetPeriod", "TimeSheet")
                        .WithMany("TimeEntries")
                        .HasForeignKey("Year", "Month", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
