using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLlavesWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoPropiedadCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Categorias");
        }
    }
}
