﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentYourHomeAPI.DataAccess;

namespace RentYourHomeAPI.Migrations
{
    [DbContext(typeof(BookingContext))]
    [Migration("20200115104247_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RentYourHomeAPI.Models.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Description");

                    b.Property<string>("Imagen");

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<int>("Stars");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Homes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Motril",
                            Description = "El mejor hotel en la Costa Tropical",
                            Imagen = "http://localhost:62122/imgs/campana1.jpg",
                            Name = "La Campana",
                            OwnerId = 1,
                            Stars = 5
                        },
                        new
                        {
                            Id = 2,
                            City = "Málaga",
                            Description = "El mejor hotel en la Costa del Sol",
                            Imagen = "http://localhost:62122/imgs/bahia1.jpg",
                            Name = "Bahia Málaga",
                            OwnerId = 1,
                            Stars = 4
                        },
                        new
                        {
                            Id = 3,
                            City = "Málaga",
                            Description = "El mejor hotel de Teatinos",
                            Imagen = "http://localhost:62122/imgs/Teatinos1.jpg",
                            Name = "Teatinos Alto",
                            OwnerId = 2,
                            Stars = 2
                        });
                });

            modelBuilder.Entity("RentYourHomeAPI.Models.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Owners");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Benito",
                            Password = "pepe"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pepe",
                            Password = "lucas"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lucas",
                            Password = "maria"
                        });
                });

            modelBuilder.Entity("RentYourHomeAPI.Models.Home", b =>
                {
                    b.HasOne("RentYourHomeAPI.Models.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
