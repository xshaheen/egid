using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EGID.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitizenDetails",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Create = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 128, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    AccountId = table.Column<string>(maxLength: 128, nullable: true),
                    PrivateKey = table.Column<string>(nullable: false),
                    FatherId = table.Column<string>(maxLength: 128, nullable: true),
                    MotherId = table.Column<string>(maxLength: 128, nullable: true),
                    Address_PostalCode = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false),
                    Religion = table.Column<byte>(nullable: false),
                    SocialStatus = table.Column<byte>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    PhotoUrl = table.Column<string>(maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeathCertificates",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Create = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 128, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    CitizenId = table.Column<string>(maxLength: 128, nullable: false),
                    CauseOfDeath = table.Column<string>(maxLength: 4098, nullable: true),
                    DateOfDeath = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeathCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeathCertificate_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "CitizenDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthInformation",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    BloodType = table.Column<int>(nullable: false),
                    Phone1 = table.Column<string>(maxLength: 24, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 24, nullable: true),
                    Phone3 = table.Column<string>(maxLength: 24, nullable: true),
                    CitizenId = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthInfo_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "CitizenDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExitHospitalRecords",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Create = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 128, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    HealthInfoId = table.Column<string>(maxLength: 128, nullable: false),
                    EnterDate = table.Column<DateTime>(nullable: false),
                    ExitDate = table.Column<DateTime>(nullable: false),
                    Medications = table.Column<string>(maxLength: 4096, nullable: true),
                    Diagnosis = table.Column<string>(maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExitHospitalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExitHospitalRecords_HealthInfo_HealthInfoId",
                        column: x => x.HealthInfoId,
                        principalTable: "HealthInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthRecords",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Create = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(maxLength: 128, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    HealthInfoId = table.Column<string>(maxLength: 128, nullable: false),
                    Medications = table.Column<string>(maxLength: 4096, nullable: true),
                    Diagnosis = table.Column<string>(maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthRecords_HealthInfo_HealthInfoId",
                        column: x => x.HealthInfoId,
                        principalTable: "HealthInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExitHospitalRecordAttachments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    ExitHospitalRecordId = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExitHospitalRecordAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExitHospitalRecordAttachment_ExitHospitalRecord_ExitHospitalRecordId",
                        column: x => x.ExitHospitalRecordId,
                        principalTable: "ExitHospitalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthRecordAttachments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    HealthRecordId = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecordAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthRecordAttachment_HealthRecord_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeathCertificates_CitizenId",
                table: "DeathCertificates",
                column: "CitizenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExitHospitalRecordAttachments_ExitHospitalRecordId",
                table: "ExitHospitalRecordAttachments",
                column: "ExitHospitalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ExitHospitalRecords_HealthInfoId",
                table: "ExitHospitalRecords",
                column: "HealthInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthInformation_CitizenId",
                table: "HealthInformation",
                column: "CitizenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecordAttachments_HealthRecordId",
                table: "HealthRecordAttachments",
                column: "HealthRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_HealthInfoId",
                table: "HealthRecords",
                column: "HealthInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeathCertificates");

            migrationBuilder.DropTable(
                name: "ExitHospitalRecordAttachments");

            migrationBuilder.DropTable(
                name: "HealthRecordAttachments");

            migrationBuilder.DropTable(
                name: "ExitHospitalRecords");

            migrationBuilder.DropTable(
                name: "HealthRecords");

            migrationBuilder.DropTable(
                name: "HealthInformation");

            migrationBuilder.DropTable(
                name: "CitizenDetails");
        }
    }
}
