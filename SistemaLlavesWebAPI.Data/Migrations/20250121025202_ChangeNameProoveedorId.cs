using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLlavesWebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameProoveedorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_ProovedorId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "ProovedorId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_ProovedorId",
                table: "Compras",
                column: "ProovedorId",
                principalTable: "Proveedores",
                principalColumn: "ProovedorId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_ProovedorId",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "ProovedorId",
                table: "Compras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_ProovedorId",
                table: "Compras",
                column: "ProovedorId",
                principalTable: "Proveedores",
                principalColumn: "ProovedorId");
        }
    }
}
