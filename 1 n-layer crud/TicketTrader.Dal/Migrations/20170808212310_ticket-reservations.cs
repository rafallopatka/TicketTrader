using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketTrader.Dal.Migrations
{
    public partial class ticketreservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_TicketProducts_TicketProductId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "TicketProductId",
                table: "Reservations",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_TicketProductId",
                table: "Reservations",
                newName: "IX_Reservations_TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_TicketProducts_TicketId",
                table: "Reservations",
                column: "TicketId",
                principalTable: "TicketProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_TicketProducts_TicketId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Reservations",
                newName: "TicketProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_TicketId",
                table: "Reservations",
                newName: "IX_Reservations_TicketProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_TicketProducts_TicketProductId",
                table: "Reservations",
                column: "TicketProductId",
                principalTable: "TicketProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
