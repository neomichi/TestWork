using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpDevTest.Data.Migrations
{
    public partial class FixRecipient2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorysLog_AspNetUsers_RecipientIdId",
                table: "HistorysLog");

            migrationBuilder.RenameColumn(
                name: "RecipientIdId",
                table: "HistorysLog",
                newName: "RecipientId");

            migrationBuilder.RenameIndex(
                name: "IX_HistorysLog_RecipientIdId",
                table: "HistorysLog",
                newName: "IX_HistorysLog_RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorysLog_AspNetUsers_RecipientId",
                table: "HistorysLog",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorysLog_AspNetUsers_RecipientId",
                table: "HistorysLog");

            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "HistorysLog",
                newName: "RecipientIdId");

            migrationBuilder.RenameIndex(
                name: "IX_HistorysLog_RecipientId",
                table: "HistorysLog",
                newName: "IX_HistorysLog_RecipientIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorysLog_AspNetUsers_RecipientIdId",
                table: "HistorysLog",
                column: "RecipientIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
