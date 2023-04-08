﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PropertyRenting.Api.Models.Contexts;

#nullable disable

namespace PropertyRenting.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221023161738_fixpercision")]
    partial class fixpercision
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.AccountEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.AccountSetupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExpenseAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RevenueAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseAccountId");

                    b.HasIndex("RevenueAccountId");

                    b.ToTable("AccountSetup");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.BuildingContributerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContributerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Percentage")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("ContributerId");

                    b.ToTable("BuildingContributer");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.BuildingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ConstructionStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EstablisYear")
                        .HasColumnType("int");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelNo")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longtude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReceiveDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RentableArea")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("SymbolNo")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalArea")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int>("UnitsNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("YearReRentAmount")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<decimal>("YearRentAmount")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.CityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.ContactPersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("IdentityExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("IdentityIssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentityIssuePlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdentityTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Mobile1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RenterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RenterId");

                    b.ToTable("ContactPerson");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.ContributerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Contributer");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.CountryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalityAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalityEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.DistrictEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.EmployeeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.NationalityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Nationality");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.OwnerContractEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ContractAmount")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<DateTime>("ContractDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ContractEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ContractNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ContractState")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("OwnerId");

                    b.ToTable("OwnerContract");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.OwnerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.OwnerFinancialTransactionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<Guid>("OppositeAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("OppositeAccountId");

                    b.ToTable("OwnerFinancialTransaction");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.RenterContractEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ContractAmount")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<DateTime>("ContractDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ContractEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ContractNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ContractState")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Increasing")
                        .HasColumnType("bit");

                    b.Property<decimal?>("IncreasingValue")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<Guid>("RenterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UnitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RenterId");

                    b.HasIndex("UnitId");

                    b.ToTable("RenterContract");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.RenterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderTypeId")
                        .HasColumnType("int");

                    b.Property<string>("GuarantorAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuarantorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuarantorPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IdentityExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IdentityIssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentityIssuePlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdentityTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBlackListed")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NationalityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("NationalityId");

                    b.ToTable("Renter");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.RenterFinancialTransactionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<Guid>("OppositeAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("OppositeAccountId");

                    b.ToTable("RenterFinancialTransaction");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.UnitEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("AnnualRentAmount")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<bool>("HasCentralAC")
                        .HasColumnType("bit");

                    b.Property<bool>("HasInternetService")
                        .HasColumnType("bit");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReceiveDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("RentStatus")
                        .HasColumnType("bit");

                    b.Property<decimal>("RentableArea")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<int>("RoomsNumber")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<decimal>("TotalArea")
                        .HasPrecision(20, 2)
                        .HasColumnType("decimal(20,2)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("DistrictId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.AccountSetupEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.AccountEntity", "ExpenseAccount")
                        .WithMany("ExpenseAccounts")
                        .HasForeignKey("ExpenseAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.AccountEntity", "RevenueAccount")
                        .WithMany("RevenueAccounts")
                        .HasForeignKey("RevenueAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ExpenseAccount");

                    b.Navigation("RevenueAccount");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.BuildingContributerEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.BuildingEntity", "Building")
                        .WithMany("Owners")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.ContributerEntity", "Contributer")
                        .WithMany("Buildings")
                        .HasForeignKey("ContributerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Contributer");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.BuildingEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.DistrictEntity", "District")
                        .WithMany("Buildings")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.EmployeeEntity", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.CityEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.CountryEntity", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.ContactPersonEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.RenterEntity", "Renter")
                        .WithMany("ContactPersons")
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.DistrictEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.CityEntity", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.OwnerContractEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.BuildingEntity", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.OwnerEntity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.OwnerFinancialTransactionEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.AccountEntity", "Account")
                        .WithMany("OwnerRenterAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.AccountEntity", "OppositeAccount")
                        .WithMany("OwnerOppositeAccounts")
                        .HasForeignKey("OppositeAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("OppositeAccount");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.RenterContractEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.RenterEntity", "Renter")
                        .WithMany()
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.UnitEntity", "Unit")
                        .WithMany("RenterContracts")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Renter");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.RenterEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.CityEntity", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.NationalityEntity", "Nationality")
                        .WithMany("Renters")
                        .HasForeignKey("NationalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Nationality");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.RenterFinancialTransactionEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.AccountEntity", "Account")
                        .WithMany("RenterAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.AccountEntity", "OppositeAccount")
                        .WithMany("RenterOppositeAccounts")
                        .HasForeignKey("OppositeAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("OppositeAccount");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.UnitEntity", b =>
                {
                    b.HasOne("PropertyRenting.Api.Models.Entities.BuildingEntity", "Building")
                        .WithMany("Units")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyRenting.Api.Models.Entities.DistrictEntity", "District")
                        .WithMany("Units")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("District");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.AccountEntity", b =>
                {
                    b.Navigation("ExpenseAccounts");

                    b.Navigation("OwnerOppositeAccounts");

                    b.Navigation("OwnerRenterAccounts");

                    b.Navigation("RenterAccounts");

                    b.Navigation("RenterOppositeAccounts");

                    b.Navigation("RevenueAccounts");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.BuildingEntity", b =>
                {
                    b.Navigation("Owners");

                    b.Navigation("Units");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.CityEntity", b =>
                {
                    b.Navigation("Districts");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.ContributerEntity", b =>
                {
                    b.Navigation("Buildings");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.CountryEntity", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.DistrictEntity", b =>
                {
                    b.Navigation("Buildings");

                    b.Navigation("Units");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.NationalityEntity", b =>
                {
                    b.Navigation("Renters");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.RenterEntity", b =>
                {
                    b.Navigation("ContactPersons");
                });

            modelBuilder.Entity("PropertyRenting.Api.Models.Entities.UnitEntity", b =>
                {
                    b.Navigation("RenterContracts");
                });
#pragma warning restore 612, 618
        }
    }
}
