using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTier.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicant",
                columns: table => new
                {
                    applicant_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    applicant_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    applicant_surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    applicant_birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contact_email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    contact_number = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    applicant_creationdate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 5, 6, 7, 6, 14, 331, DateTimeKind.Utc).AddTicks(4022)),
                    applicant_modifieddate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicant", x => x.applicant_id);
                });

            migrationBuilder.CreateTable(
                name: "application_status",
                columns: table => new
                {
                    application_status_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    application_status_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    application_status_creationdate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 5, 6, 7, 6, 14, 331, DateTimeKind.Utc).AddTicks(7973)),
                    application_status_modifieddate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_status", x => x.application_status_id);
                });

            migrationBuilder.CreateTable(
                name: "grade",
                columns: table => new
                {
                    grade_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grade_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    grade_number = table.Column<int>(type: "int", nullable: false),
                    grade_capacity = table.Column<int>(type: "int", nullable: false),
                    grade_creationdate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 5, 6, 7, 6, 14, 332, DateTimeKind.Utc).AddTicks(2249)),
                    grade_modifieddate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade", x => x.grade_id);
                });

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    application_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    applicant_id = table.Column<long>(type: "bigint", nullable: false),
                    grade_id = table.Column<long>(type: "bigint", nullable: false),
                    application_status_id = table.Column<long>(type: "bigint", nullable: false),
                    application_creationdate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 5, 6, 7, 6, 14, 332, DateTimeKind.Utc).AddTicks(4467)),
                    application_modifieddate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    application_schoolyear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_application_applicant_applicant_id",
                        column: x => x.applicant_id,
                        principalTable: "applicant",
                        principalColumn: "applicant_id");
                    table.ForeignKey(
                        name: "FK_application_application_status_application_status_id",
                        column: x => x.application_status_id,
                        principalTable: "application_status",
                        principalColumn: "application_status_id");
                    table.ForeignKey(
                        name: "FK_application_grade_grade_id",
                        column: x => x.grade_id,
                        principalTable: "grade",
                        principalColumn: "grade_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_application_applicant_id",
                table: "application",
                column: "applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_application_status_id",
                table: "application",
                column: "application_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_grade_id",
                table: "application",
                column: "grade_id");

            migrationBuilder.CreateIndex(
                name: "IX_application_status_application_status_name",
                table: "application_status",
                column: "application_status_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grade_grade_name",
                table: "grade",
                column: "grade_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_grade_grade_number",
                table: "grade",
                column: "grade_number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "applicant");

            migrationBuilder.DropTable(
                name: "application_status");

            migrationBuilder.DropTable(
                name: "grade");
        }
    }
}
