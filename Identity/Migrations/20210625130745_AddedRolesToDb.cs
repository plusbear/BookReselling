using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7051f15b-6df0-446a-9894-a5e89d86ef53", "a8898b7e-7349-4ae1-9646-10a9eaf11c33", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbca3c70-bcc2-4213-87c9-20e5922548d9", "d08bcc80-7b51-4ece-adac-e1fd2d9a2875", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7051f15b-6df0-446a-9894-a5e89d86ef53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbca3c70-bcc2-4213-87c9-20e5922548d9");
        }
    }
}
