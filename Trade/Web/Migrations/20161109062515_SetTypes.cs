using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class SetTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Orders",
                nullable: false);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "CompleteTrades",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "CompleteTrades",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CompleteTrades",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "CompleteTrades",
                nullable: false);
        }
    }
}
