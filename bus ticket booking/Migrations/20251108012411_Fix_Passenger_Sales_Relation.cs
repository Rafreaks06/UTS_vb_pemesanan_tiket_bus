using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bus_ticket_booking.Migrations
{
    public partial class Fix_Passenger_Sales_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Sales_PassengerId", table: "Sales");
            migrationBuilder.CreateIndex(name: "IX_Sales_PassengerId", table: "Sales", column: "PassengerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Sales_PassengerId", table: "Sales");
            migrationBuilder.CreateIndex(name: "IX_Sales_PassengerId", table: "Sales", column: "PassengerId", unique: true);
        }
    }
}
