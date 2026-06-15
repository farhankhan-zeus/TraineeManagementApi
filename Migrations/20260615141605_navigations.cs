using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class navigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignment_LearningTaskId",
                table: "TaskAssignment",
                column: "LearningTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignment_MentorId",
                table: "TaskAssignment",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignment_TraineeId",
                table: "TaskAssignment",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_TaskAssignmentId",
                table: "Submission",
                column: "TaskAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_MentorId",
                table: "Review",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_SubmissionId",
                table: "Review",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Mentors_MentorId",
                table: "Review",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Submission_SubmissionId",
                table: "Review",
                column: "SubmissionId",
                principalTable: "Submission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_TaskAssignment_TaskAssignmentId",
                table: "Submission",
                column: "TaskAssignmentId",
                principalTable: "TaskAssignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignment_LearningTask_LearningTaskId",
                table: "TaskAssignment",
                column: "LearningTaskId",
                principalTable: "LearningTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignment_Mentors_MentorId",
                table: "TaskAssignment",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignment_Trainees_TraineeId",
                table: "TaskAssignment",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Mentors_MentorId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Submission_SubmissionId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_TaskAssignment_TaskAssignmentId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignment_LearningTask_LearningTaskId",
                table: "TaskAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignment_Mentors_MentorId",
                table: "TaskAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignment_Trainees_TraineeId",
                table: "TaskAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TaskAssignment_LearningTaskId",
                table: "TaskAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TaskAssignment_MentorId",
                table: "TaskAssignment");

            migrationBuilder.DropIndex(
                name: "IX_TaskAssignment_TraineeId",
                table: "TaskAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Submission_TaskAssignmentId",
                table: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_Review_MentorId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_SubmissionId",
                table: "Review");
        }
    }
}
