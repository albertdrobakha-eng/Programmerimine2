using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
namespace KooliProjekt.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20261127000000_AddUsersAndPortfolioOwners")]
    public partial class AddUsersAndPortfolioOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(name: "OwnerId", table: "Portfolios", type: "int", nullable: true);
            migrationBuilder.CreateIndex(name: "IX_Portfolios_OwnerId", table: "Portfolios", column: "OwnerId");
            migrationBuilder.AddForeignKey(name: "FK_Portfolios_Users_OwnerId", table: "Portfolios", column: "OwnerId", principalTable: "Users", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
        }
        // Keep ownership and user data intact if this migration is ever rolled back manually.
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
