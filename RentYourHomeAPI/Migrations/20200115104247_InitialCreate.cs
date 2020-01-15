using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentYourHomeAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Stars = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Imagen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homes_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 1, "Benito", "pepe" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 2, "Pepe", "lucas" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 3, "Lucas", "maria" });

            migrationBuilder.InsertData(
                table: "Homes",
                columns: new[] { "Id", "City", "Description", "Imagen", "Name", "OwnerId", "Stars" },
                values: new object[] { 1, "Motril", "El mejor hotel en la Costa Tropical", "http://localhost:62122/imgs/campana1.jpg", "La Campana", 1, 5 });

            migrationBuilder.InsertData(
                table: "Homes",
                columns: new[] { "Id", "City", "Description", "Imagen", "Name", "OwnerId", "Stars" },
                values: new object[] { 2, "Málaga", "El mejor hotel en la Costa del Sol", "http://localhost:62122/imgs/bahia1.jpg", "Bahia Málaga", 1, 4 });

            migrationBuilder.InsertData(
                table: "Homes",
                columns: new[] { "Id", "City", "Description", "Imagen", "Name", "OwnerId", "Stars" },
                values: new object[] { 3, "Málaga", "El mejor hotel de Teatinos", "http://localhost:62122/imgs/Teatinos1.jpg", "Teatinos Alto", 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Homes_OwnerId",
                table: "Homes",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
