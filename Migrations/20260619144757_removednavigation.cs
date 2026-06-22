using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class removednavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionFile_Trainees_UploadedById",
                table: "SubmissionFile");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionFile_UploadedById",
                table: "SubmissionFile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFile_UploadedById",
                table: "SubmissionFile",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionFile_Trainees_UploadedById",
                table: "SubmissionFile",
                column: "UploadedById",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
