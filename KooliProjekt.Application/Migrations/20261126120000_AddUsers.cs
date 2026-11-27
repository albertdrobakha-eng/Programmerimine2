using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace KooliProjekt.Application.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20261126120000_AddUsers")]
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[Users]', N'U') IS NULL
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY(1,1),
        [UserName] nvarchar(25) NOT NULL,
        [PasswordHash] nvarchar(255) NOT NULL,
        [IsAdmin] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Users_UserName' AND object_id = OBJECT_ID(N'[Users]'))
    CREATE UNIQUE INDEX [IX_Users_UserName] ON [Users] ([UserName]);
");
        }

        // User records must not be dropped automatically when rolling back this migration.
        protected override void Down(MigrationBuilder migrationBuilder) { }
    }
}
