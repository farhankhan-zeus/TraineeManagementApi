using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Trainees_Id",
                table: "Trainees",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_Id",
                table: "Mentors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearningTask_Id",
                table: "LearningTask",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trainees_Id",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Mentors_Id",
                table: "Mentors");

            migrationBuilder.DropIndex(
                name: "IX_LearningTask_Id",
                table: "LearningTask");
        }
    }
}
