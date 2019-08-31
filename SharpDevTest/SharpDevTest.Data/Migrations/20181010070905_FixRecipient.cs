using Microsoft.EntityFrameworkCore.Migrations;

namespace SharpDevTest.Data.Migrations
{
    public partial class FixRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecipientId",
                table: "HistorysLog",
                newName: "RecipientIdId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorysLog_RecipientIdId",
                table: "HistorysLog",
                column: "RecipientIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistorysLog_AspNetUsers_RecipientIdId",
                table: "HistorysLog",
                column: "RecipientIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistorysLog_AspNetUsers_RecipientIdId",
                table: "HistorysLog");

            migrationBuilder.DropIndex(
                name: "IX_HistorysLog_RecipientIdId",
                table: "HistorysLog");

            migrationBuilder.RenameColumn(
                name: "RecipientIdId",
                table: "HistorysLog",
                newName: "RecipientId");
        }
    }
}
