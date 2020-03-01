﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomsManager;

namespace RoomsManager.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20191230200605_monPremier3")]
    partial class monPremier3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RoomsManager.Models.MesReserves", b =>
                {
                    b.Property<int>("MesReservesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("MesReservesId");

                    b.ToTable("MesReserves");
                });

            modelBuilder.Entity("RoomsManager.Models.Rooms", b =>
                {
                    b.Property<int>("RoomsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Adulte");

                    b.Property<int?>("Enfants");

                    b.Property<int>("NbrePieces");

                    b.Property<int>("Prix");

                    b.Property<bool?>("Status");

                    b.Property<string>("UserEmail");

                    b.HasKey("RoomsId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("RoomsManager.Models.Users", b =>
                {
                    b.Property<string>("UserEmail")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Token");

                    b.Property<string>("UserRole");

                    b.HasKey("UserEmail");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
