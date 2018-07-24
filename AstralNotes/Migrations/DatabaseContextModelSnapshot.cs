﻿// <auto-generated />
using System;
using AstralNotes.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AstralNotes.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AstralNotes.Domain.Entities.Note", b =>
                {
                    b.Property<Guid>("NoteGuid");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Text");

                    b.Property<string>("Theme");

                    b.Property<Guid>("UserGuid");

                    b.HasKey("NoteGuid");

                    b.HasIndex("UserGuid");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("AstralNotes.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserGuid");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("UserGuid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AstralNotes.Domain.Entities.Note", b =>
                {
                    b.HasOne("AstralNotes.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
