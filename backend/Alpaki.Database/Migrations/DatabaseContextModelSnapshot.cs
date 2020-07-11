﻿// <auto-generated />
using System;
using Alpaki.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alpaki.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alpaki.Database.Models.AssignedDreams", b =>
                {
                    b.Property<long>("DreamId")
                        .HasColumnType("bigint");

                    b.Property<long>("VolunteerId")
                        .HasColumnType("bigint");

                    b.HasKey("DreamId", "VolunteerId");

                    b.HasIndex("VolunteerId");

                    b.ToTable("AssignedDreams");
                });

            modelBuilder.Entity("Alpaki.Database.Models.Dream", b =>
                {
                    b.Property<long>("DreamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<long>("DreamCategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DreamComeTrueDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("DreamState")
                        .HasColumnType("int");

                    b.Property<string>("DreamUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DreamId");

                    b.HasIndex("DreamCategoryId");

                    b.ToTable("Dreams");
                });

            modelBuilder.Entity("Alpaki.Database.Models.DreamCategory", b =>
                {
                    b.Property<long>("DreamCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("DreamCategoryId");

                    b.ToTable("DreamCategories");

                    b.HasData(
                        new
                        {
                            DreamCategoryId = 1L,
                            CategoryName = "Chcę dostać"
                        },
                        new
                        {
                            DreamCategoryId = 2L,
                            CategoryName = "Chcę poznać"
                        },
                        new
                        {
                            DreamCategoryId = 3L,
                            CategoryName = "Chcę pojechać"
                        },
                        new
                        {
                            DreamCategoryId = 4L,
                            CategoryName = "Chcę kimś się stać"
                        },
                        new
                        {
                            DreamCategoryId = 5L,
                            CategoryName = "Chcę komuś coś dać"
                        });
                });

            modelBuilder.Entity("Alpaki.Database.Models.DreamStep", b =>
                {
                    b.Property<long>("DreamStepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DreamId")
                        .HasColumnType("bigint");

                    b.Property<string>("StepDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StepState")
                        .HasColumnType("int");

                    b.HasKey("DreamStepId");

                    b.HasIndex("DreamId");

                    b.ToTable("DreamSteps");
                });

            modelBuilder.Entity("Alpaki.Database.Models.Invitations.Invitation", b =>
                {
                    b.Property<int>("InvitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("InvitationId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("Alpaki.Database.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            Email = "admin@admin.pl",
                            FirstName = "admin",
                            LastName = "admin",
                            Role = 7
                        });
                });

            modelBuilder.Entity("Alpaki.Database.Models.AssignedDreams", b =>
                {
                    b.HasOne("Alpaki.Database.Models.Dream", "Dream")
                        .WithMany("Volunteers")
                        .HasForeignKey("DreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alpaki.Database.Models.User", "Volunteer")
                        .WithMany("AssignedDreams")
                        .HasForeignKey("VolunteerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Alpaki.Database.Models.Dream", b =>
                {
                    b.HasOne("Alpaki.Database.Models.DreamCategory", "DreamCategory")
                        .WithMany("Dreams")
                        .HasForeignKey("DreamCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Alpaki.Database.Models.DreamStep", b =>
                {
                    b.HasOne("Alpaki.Database.Models.Dream", "Dream")
                        .WithMany("RequiredSteps")
                        .HasForeignKey("DreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
