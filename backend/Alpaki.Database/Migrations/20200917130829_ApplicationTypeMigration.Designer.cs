﻿// <auto-generated />
using System;
using Alpaki.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alpaki.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200917130829_ApplicationTypeMigration")]
    partial class ApplicationTypeMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
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

            modelBuilder.Entity("Alpaki.Database.Models.AssignedSponsor", b =>
                {
                    b.Property<long>("DreamId")
                        .HasColumnType("bigint");

                    b.Property<long>("SponsorId")
                        .HasColumnType("bigint");

                    b.HasKey("DreamId", "SponsorId");

                    b.HasIndex("SponsorId");

                    b.ToTable("AssignedSponsors");
                });

            modelBuilder.Entity("Alpaki.Database.Models.Dream", b =>
                {
                    b.Property<long>("DreamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<long>("DreamCategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DreamComeTrueDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("DreamImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DreamState")
                        .HasColumnType("int");

                    b.Property<string>("DreamUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("DreamId");

                    b.HasIndex("DreamCategoryId");

                    b.HasIndex("DreamImageId");

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

            modelBuilder.Entity("Alpaki.Database.Models.DreamCategoryDefaultStep", b =>
                {
                    b.Property<long>("DreamCategoryDefaultStepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("DreamCategoryId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsSponsorRelated")
                        .HasColumnType("bit");

                    b.Property<string>("StepDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DreamCategoryDefaultStepId");

                    b.HasIndex("DreamCategoryId");

                    b.ToTable("DreamCategoryDefaultSteps");
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

            modelBuilder.Entity("Alpaki.Database.Models.Image", b =>
                {
                    b.Property<Guid>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Alpaki.Database.Models.Invitations.Invitation", b =>
                {
                    b.Property<int>("InvitationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Attempts")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("InvitationId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("Alpaki.Database.Models.Sponsor", b =>
                {
                    b.Property<long>("SponsorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("ContactPersonName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("CooperationType")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("SponsorId");

                    b.ToTable("Sponsors");
                });

            modelBuilder.Entity("Alpaki.Database.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationType")
                        .HasColumnType("int");

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

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            ApplicationType = 0,
                            Email = "admin@admin.pl",
                            FirstName = "admin",
                            LastName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEJVioQ8QWVZEpT7F3elW92J94EBDnlJw9oPZJR2zWXjV5siZliAhydafLJ20NXP/CQ==",
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

            modelBuilder.Entity("Alpaki.Database.Models.AssignedSponsor", b =>
                {
                    b.HasOne("Alpaki.Database.Models.Dream", "Dream")
                        .WithMany("Sponsors")
                        .HasForeignKey("DreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Alpaki.Database.Models.Sponsor", "Sponsor")
                        .WithMany("Dreams")
                        .HasForeignKey("SponsorId")
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

                    b.HasOne("Alpaki.Database.Models.Image", "DreamImage")
                        .WithMany()
                        .HasForeignKey("DreamImageId");
                });

            modelBuilder.Entity("Alpaki.Database.Models.DreamCategoryDefaultStep", b =>
                {
                    b.HasOne("Alpaki.Database.Models.DreamCategory", "DreamCategory")
                        .WithMany("DefaultSteps")
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

            modelBuilder.Entity("Alpaki.Database.Models.User", b =>
                {
                    b.HasOne("Alpaki.Database.Models.Image", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("ProfileImageId");
                });
#pragma warning restore 612, 618
        }
    }
}
