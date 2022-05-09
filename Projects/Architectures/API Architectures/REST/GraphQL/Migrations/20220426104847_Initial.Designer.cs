﻿// <auto-generated />
using GraphQL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GraphQL.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20220426104847_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("GraphQL.Models.ItemData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ListId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("GraphQL.Models.ItemList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("GraphQL.Models.ItemData", b =>
                {
                    b.HasOne("GraphQL.Models.ItemList", "ItemList")
                        .WithMany("ItemDatas")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_ItemData_ItemList");

                    b.Navigation("ItemList");
                });

            modelBuilder.Entity("GraphQL.Models.ItemList", b =>
                {
                    b.Navigation("ItemDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
