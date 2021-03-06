﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(RoomContext))]
    [Migration("20201116143441_addAppUserToRoom")]
    partial class addAppUserToRoom
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Core.Entities.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("Core.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AppUserEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescribeNeighborhood")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("InitialDeposit")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSecurityChecked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Layout")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MoveInDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberBathRooms")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberBedRooms")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberRoommateAllowed")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Rent")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoommateDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("SpaceDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("StayDuration")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Core.Entities.RoomAmenities", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmenitiesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoomId", "AmenitiesId");

                    b.HasIndex("AmenitiesId");

                    b.ToTable("RoomAmenities");
                });

            modelBuilder.Entity("Core.Entities.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("Core.Entities.Amenity", b =>
                {
                    b.HasOne("Core.Entities.Room", null)
                        .WithMany("Amenities")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("Core.Entities.RoomAmenities", b =>
                {
                    b.HasOne("Core.Entities.Amenity", "Amenities")
                        .WithMany()
                        .HasForeignKey("AmenitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amenities");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Core.Entities.Rule", b =>
                {
                    b.HasOne("Core.Entities.Room", null)
                        .WithMany("Rules")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("Core.Entities.Room", b =>
                {
                    b.Navigation("Amenities");

                    b.Navigation("Rules");
                });
#pragma warning restore 612, 618
        }
    }
}
