using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceControl.API.DataAccess.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administrator",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    admin_name = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrator", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cycle",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    year = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cycle", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_data",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dni = table.Column<string>(type: "varchar(9)", nullable: false),
                    firstname = table.Column<string>(type: "varchar(255)", nullable: false),
                    lastname1 = table.Column<string>(type: "varchar(255)", nullable: false),
                    lastname2 = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    total_absences = table.Column<int>(type: "int", nullable: false),
                    total_delays = table.Column<int>(type: "int", nullable: false),
                    person_data_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_person_data_person_data_id",
                        column: x => x.person_data_id,
                        principalTable: "person_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teacher",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(32)", nullable: false),
                    password = table.Column<string>(type: "varchar(32)", nullable: false),
                    person_data_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher", x => x.id);
                    table.ForeignKey(
                        name: "FK_teacher_person_data_person_data_id",
                        column: x => x.person_data_id,
                        principalTable: "person_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    teacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.id);
                    table.ForeignKey(
                        name: "FK_subject_teacher_teacherId",
                        column: x => x.teacherId,
                        principalTable: "teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cycle_has_subjects",
                columns: table => new
                {
                    cycleId = table.Column<int>(nullable: false),
                    subjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cycle_has_subjects", x => new { x.cycleId, x.subjectId });
                    table.ForeignKey(
                        name: "FK_cycle_has_subjects_cycle_cycleId",
                        column: x => x.cycleId,
                        principalTable: "cycle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cycle_has_subjects_subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedule",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    day = table.Column<int>(type: "int", nullable: false),
                    start = table.Column<DateTime>(type: "timestamp", nullable: false),
                    end = table.Column<DateTime>(type: "timestamp", nullable: false),
                    subjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_schedule_subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_has_subjects",
                columns: table => new
                {
                    studentId = table.Column<int>(nullable: false),
                    subjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_has_subjects", x => new { x.studentId, x.subjectId });
                    table.ForeignKey(
                        name: "FK_student_has_subjects_student_studentId",
                        column: x => x.studentId,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_student_has_subjects_subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "subject",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "absence",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<int>(type: "int", nullable: false),
                    scheduleId = table.Column<int>(type: "int", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_absence", x => x.id);
                    table.ForeignKey(
                        name: "FK_absence_schedule_scheduleId",
                        column: x => x.scheduleId,
                        principalTable: "schedule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_absence_student_studentId",
                        column: x => x.studentId,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_absence_scheduleId",
                table: "absence",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_absence_studentId",
                table: "absence",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_cycle_has_subjects_subjectId",
                table: "cycle_has_subjects",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_subjectId",
                table: "schedule",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_student_person_data_id",
                table: "student",
                column: "person_data_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_student_has_subjects_subjectId",
                table: "student_has_subjects",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_subject_teacherId",
                table: "subject",
                column: "teacherId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_person_data_id",
                table: "teacher",
                column: "person_data_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "absence");

            migrationBuilder.DropTable(
                name: "administrator");

            migrationBuilder.DropTable(
                name: "cycle_has_subjects");

            migrationBuilder.DropTable(
                name: "student_has_subjects");

            migrationBuilder.DropTable(
                name: "schedule");

            migrationBuilder.DropTable(
                name: "cycle");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "subject");

            migrationBuilder.DropTable(
                name: "teacher");

            migrationBuilder.DropTable(
                name: "person_data");
        }
    }
}
