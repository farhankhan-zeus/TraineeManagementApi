using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LearningTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Descriptiion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    ExpectedTechStack = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTask", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mentors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Expertise = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentors", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    TechStack = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Passwordhash = table.Column<string>(type: "longtext", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskAssignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TraineeId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MentorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LearningTaskId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AssignDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    Remarks = table.Column<string>(type: "longtext", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAssignment_LearningTask_LearningTaskId",
                        column: x => x.LearningTaskId,
                        principalTable: "LearningTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignment_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignment_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TaskAssignmentId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SubmissionUrl = table.Column<string>(type: "longtext", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submission_TaskAssignment_TaskAssignmentId",
                        column: x => x.TaskAssignmentId,
                        principalTable: "TaskAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MentorId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Feedback = table.Column<string>(type: "longtext", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReviewedStatus = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Mentors_MentorId",
                        column: x => x.MentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_Submission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubmissionFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    orignalName = table.Column<string>(type: "longtext", nullable: false),
                    generatedName = table.Column<string>(type: "longtext", nullable: false),
                    ContentType = table.Column<string>(type: "longtext", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    checksum = table.Column<string>(type: "longtext", nullable: false),
                    UploadedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    SubmissionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionFile_Submission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmissionFile_Trainees_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "Trainees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LearningTask_Id",
                table: "LearningTask",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_Id",
                table: "Mentors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_Id",
                table: "Review",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_MentorId",
                table: "Review",
                column: "MentorId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_SubmissionId",
                table: "Review",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_Id",
                table: "Submission",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submission_TaskAssignmentId",
                table: "Submission",
                column: "TaskAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFile_Id",
                table: "SubmissionFile",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFile_SubmissionId",
                table: "SubmissionFile",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFile_UploadedById",
                table: "SubmissionFile",
                column: "UploadedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignment_Id",
                table: "TaskAssignment",
                column: "Id",
                unique: true);

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
                name: "IX_Trainees_Id",
                table: "Trainees",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "SubmissionFile");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropTable(
                name: "TaskAssignment");

            migrationBuilder.DropTable(
                name: "LearningTask");

            migrationBuilder.DropTable(
                name: "Mentors");

            migrationBuilder.DropTable(
                name: "Trainees");
        }
    }
}
