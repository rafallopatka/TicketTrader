using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketTrader.Dal.Migrations
{
    public partial class orderdelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Deliveries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Deliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Deliveries",
                nullable: true);
        }
    }
}
