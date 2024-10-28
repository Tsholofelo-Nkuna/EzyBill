using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EzyBill.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPaidFromInvoiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Invoices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
