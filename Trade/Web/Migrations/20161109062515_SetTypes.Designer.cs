using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Web.Model;

namespace Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161109062515_SetTypes")]
    partial class SetTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Web.Model.CompleteTrade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<string>("BuyComment");

                    b.Property<DateTime>("BuyTime");

                    b.Property<DateTime>("CompleteAt");

                    b.Property<double>("Price");

                    b.Property<string>("SellComment");

                    b.Property<DateTime>("SellTime");

                    b.HasKey("Id");

                    b.ToTable("CompleteTrades");
                });

            modelBuilder.Entity("Web.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActionType");

                    b.Property<int>("Amount");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateAt");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });
        }
    }
}
