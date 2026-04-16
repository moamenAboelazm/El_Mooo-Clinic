using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Mooo_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class fixErrors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorId",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "DoctorSchedules",
                newName: "DoctorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DoctorSchedules",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorID",
                table: "DoctorSchedules",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorID",
                table: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "DoctorSchedules",
                newName: "DoctorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "DoctorSchedules",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSchedules_DoctorID",
                table: "DoctorSchedules",
                newName: "IX_DoctorSchedules_DoctorId");

            migrationBuilder.AddColumn<double>(
                name: "Age",
                table: "Patients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Doctors_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
