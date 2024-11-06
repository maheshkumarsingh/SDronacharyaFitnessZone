﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Infrastructure.DBContext;

#nullable disable

namespace WebApp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241102081629_InitialDBSetup")]
    partial class InitialDBSetup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<WebApp.Core.Domain.Entities.Enums.Gender>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("gender", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Male"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Female"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<WebApp.Core.Domain.Entities.Enums.MembershipType>", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("membership_type", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Monthly"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Quaterly"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Half_Yearly"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Yearly"
                        });
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Gym", b =>
                {
                    b.Property<int>("GymId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GymId"));

                    b.Property<string>("GymAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GymContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GymDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GymName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GymOffers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GymId");

                    b.ToTable("Gyms");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Maintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Charge")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("GymId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Member", b =>
                {
                    b.Property<string>("MemberLoginName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AlternatePhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BloodGroup")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("GymId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("JoiningDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MembershipType")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberLoginName");

                    b.HasIndex("Gender");

                    b.HasIndex("GymId");

                    b.HasIndex("MemberLoginName")
                        .IsUnique();

                    b.HasIndex("MembershipType");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("DueAmount")
                        .HasColumnType("float");

                    b.Property<bool>("IsMembershipActive")
                        .HasColumnType("bit");

                    b.Property<string>("MemberLoginName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("MembershipAmount")
                        .HasColumnType("float");

                    b.Property<DateOnly>("MembershipEndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("MembershipStartDate")
                        .HasColumnType("date");

                    b.Property<int>("MembershipType")
                        .HasColumnType("int");

                    b.Property<double>("PaidAmount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MemberLoginName");

                    b.HasIndex("MembershipType");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Supplement", b =>
                {
                    b.Property<int>("SupplementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplementId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GymId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("SupplementId");

                    b.HasIndex("GymId");

                    b.ToTable("Supplements");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.SupplementOrder", b =>
                {
                    b.Property<int>("SupplementOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplementOrderId"));

                    b.Property<string>("MemberLoginName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SupplementId")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("SupplementOrderId");

                    b.HasIndex("MemberLoginName");

                    b.HasIndex("SupplementId");

                    b.ToTable("SupplementOrders");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Maintenance", b =>
                {
                    b.HasOne("WebApp.Core.Domain.Entities.Gym", "Gym")
                        .WithMany("Maintenances")
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gym");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Member", b =>
                {
                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<WebApp.Core.Domain.Entities.Enums.Gender>", null)
                        .WithMany()
                        .HasForeignKey("Gender")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Core.Domain.Entities.Gym", null)
                        .WithMany("Members")
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<WebApp.Core.Domain.Entities.Enums.MembershipType>", null)
                        .WithMany()
                        .HasForeignKey("MembershipType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Membership", b =>
                {
                    b.HasOne("WebApp.Core.Domain.Entities.Member", "Member")
                        .WithMany("Memberships")
                        .HasForeignKey("MemberLoginName");

                    b.HasOne("SpatialFocus.EntityFrameworkCore.Extensions.EnumWithNumberLookup<WebApp.Core.Domain.Entities.Enums.MembershipType>", null)
                        .WithMany()
                        .HasForeignKey("MembershipType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Supplement", b =>
                {
                    b.HasOne("WebApp.Core.Domain.Entities.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gym");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.SupplementOrder", b =>
                {
                    b.HasOne("WebApp.Core.Domain.Entities.Member", "Member")
                        .WithMany("SupplementOrders")
                        .HasForeignKey("MemberLoginName");

                    b.HasOne("WebApp.Core.Domain.Entities.Supplement", "Supplement")
                        .WithMany()
                        .HasForeignKey("SupplementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Supplement");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Gym", b =>
                {
                    b.Navigation("Maintenances");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("WebApp.Core.Domain.Entities.Member", b =>
                {
                    b.Navigation("Memberships");

                    b.Navigation("SupplementOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
