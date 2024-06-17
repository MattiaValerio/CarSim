﻿// <auto-generated />
using CarSim.BackEnd.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarSim.BackEnd.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240617092015_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarSim.Shared.Models.Car", b =>
                {
                    b.Property<string>("Plate")
                        .HasColumnType("text");

                    b.Property<bool>("Accelerator")
                        .HasColumnType("boolean");

                    b.Property<int>("Body")
                        .HasColumnType("integer");

                    b.Property<bool>("BrakePedal")
                        .HasColumnType("boolean");

                    b.Property<int>("Engine")
                        .HasColumnType("integer");

                    b.Property<int>("FuelType")
                        .HasColumnType("integer");

                    b.Property<int>("SteeringWheel")
                        .HasColumnType("integer");

                    b.Property<int>("Tank")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Plate");

                    b.ToTable("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
