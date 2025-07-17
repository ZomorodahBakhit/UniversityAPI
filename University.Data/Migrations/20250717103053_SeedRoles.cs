using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT INTO Roles (Name, NormalizedName, ConcurrencyStamp)
        VALUES ('Student', 'STUDENT', NEWID());

        INSERT INTO Roles (Name, NormalizedName, ConcurrencyStamp)
        VALUES ('Teacher', 'TEACHER', NEWID());
    ");
        }
        //Part 1 – Create Roles Using Migration
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("DELETE FROM Roles WHERE Name IN ('Student', 'Teacher');");
        }
    }
}
