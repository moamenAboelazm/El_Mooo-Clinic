using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Mooo_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class EditAppointmentsSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotDurationInMinutes",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "SlotDurationInMinutes",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
