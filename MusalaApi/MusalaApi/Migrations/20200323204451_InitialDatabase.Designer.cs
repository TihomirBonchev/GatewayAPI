﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusalaApi.Model;

namespace MusalaApi.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20200323204451_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusalaApi.Model.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(20);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.Property<string>("Vendor")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("DeviceId");

                    b.HasIndex("SerialNumber");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("MusalaApi.Model.Gateway", b =>
                {
                    b.Property<string>("SerialNumber")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<string>("IPAddress")
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("SerialNumber");

                    b.ToTable("Gateway");
                });

            modelBuilder.Entity("MusalaApi.Model.Device", b =>
                {
                    b.HasOne("MusalaApi.Model.Gateway", "Gateway")
                        .WithMany("Devices")
                        .HasForeignKey("SerialNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
