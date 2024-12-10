using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class profiledatatable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VedioURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    University = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BachelorDegree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaceBook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileData");
        }
    }
}
