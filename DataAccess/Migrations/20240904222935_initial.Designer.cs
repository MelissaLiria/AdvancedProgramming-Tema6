﻿// <auto-generated />
using System;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240904222935_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.27");

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Structure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Structure", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Variable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("TEXT");

                    b.Property<int>("StructureType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Variables", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.HistoricalData.Sample", b =>
                {
                    b.Property<Guid>("VariableId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("VariableId", "DateTime");

                    b.ToTable("Samples", (string)null);

                    b.HasDiscriminator<string>("DataType").HasValue("Sample");
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Building", b =>
                {
                    b.HasBaseType("Domain.Entities.ConfigurationData.Structure");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.ToTable("Buildings", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Floor", b =>
                {
                    b.HasBaseType("Domain.Entities.ConfigurationData.Structure");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("BuildingId");

                    b.ToTable("Floors", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Room", b =>
                {
                    b.HasBaseType("Domain.Entities.ConfigurationData.Structure");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FloorId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsProduction")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.HasIndex("FloorId");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.HistoricalData.SampleBool", b =>
                {
                    b.HasBaseType("Domain.Entities.HistoricalData.Sample");

                    b.Property<bool>("Value")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Bool");
                });

            modelBuilder.Entity("Domain.Entities.HistoricalData.SampleDouble", b =>
                {
                    b.HasBaseType("Domain.Entities.HistoricalData.Sample");

                    b.Property<double>("Value")
                        .HasColumnType("REAL")
                        .HasColumnName("SampleDouble_Value");

                    b.HasDiscriminator().HasValue("Double");
                });

            modelBuilder.Entity("Domain.Entities.HistoricalData.SampleInt", b =>
                {
                    b.HasBaseType("Domain.Entities.HistoricalData.Sample");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER")
                        .HasColumnName("SampleInt_Value");

                    b.HasDiscriminator().HasValue("Int");
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Variable", b =>
                {
                    b.HasOne("Domain.Entities.ConfigurationData.Structure", "Location")
                        .WithMany("Variables")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.ValueObjects.VariableType", "VariableType", b1 =>
                        {
                            b1.Property<Guid>("VariableId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("MeasurementUnit")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("VariableId");

                            b1.ToTable("Variables");

                            b1.WithOwner()
                                .HasForeignKey("VariableId");
                        });

                    b.Navigation("Location");

                    b.Navigation("VariableType")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Building", b =>
                {
                    b.HasOne("Domain.Entities.ConfigurationData.Structure", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.ConfigurationData.Building", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Floor", b =>
                {
                    b.HasOne("Domain.Entities.ConfigurationData.Building", "Building")
                        .WithMany("Floors")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ConfigurationData.Structure", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.ConfigurationData.Floor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Room", b =>
                {
                    b.HasOne("Domain.Entities.ConfigurationData.Floor", "Floor")
                        .WithMany("Rooms")
                        .HasForeignKey("FloorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ConfigurationData.Structure", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.ConfigurationData.Room", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Floor");
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Structure", b =>
                {
                    b.Navigation("Variables");
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Building", b =>
                {
                    b.Navigation("Floors");
                });

            modelBuilder.Entity("Domain.Entities.ConfigurationData.Floor", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}