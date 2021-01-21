﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Natsecure.SocialGuard.Api.Data;

namespace Natsecure.SocialGuard.Api.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20210120224703_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Natsecure.SocialGuard.Api.Data.Models.TrustlistUser", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTime>("EntryAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("EscalationLevel")
                        .HasColumnType("tinyint");

                    b.Property<string>("EscalationNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastEscalated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TrustlistUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
