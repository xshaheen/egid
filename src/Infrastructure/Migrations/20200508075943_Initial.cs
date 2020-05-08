using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EGID.Infrastructure.Migrations
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
                    CreateBy = table.Column<string>(maxLength: 128, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedBy = table.Column<string>(maxLength: 128, nullable: true),
                    PrivateKey = table.Column<string>(nullable: false),
                    PublicKey = table.Column<string>(nullable: false),
                    FatherId = table.Column<string>(maxLength: 128, nullable: true),
                    MotherId = table.Column<string>(maxLength: 128, nullable: true),
                    FullName_FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    FullName_SecondName = table.Column<string>(maxLength: 50, nullable: true),
                    FullName_ThirdName = table.Column<string>(maxLength: 50, nullable: true),
                    FullName_LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Address_Street = table.Column<string>(maxLength: 50, nullable: true),
                    Address_City = table.Column<string>(maxLength: 50, nullable: true),
                    Address_State = table.Column<string>(maxLength: 50, nullable: true),
                    Address_PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    Address_Country = table.Column<string>(maxLength: 50, nullable: true),
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
                name: "EGRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGRoles", x => x.Id);
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
                name: "EGCards",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 24, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CitizenId = table.Column<string>(maxLength: 128, nullable: false),
                    Pin1Hash = table.Column<string>(nullable: false),
                    Pin1Salt = table.Column<string>(nullable: false),
                    Pin2Hash = table.Column<string>(nullable: false),
                    Pin2Salt = table.Column<string>(nullable: false),
                    CardIssuer = table.Column<string>(maxLength: 128, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    TerminationDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EGCards_CitizenDetails_CitizenId",
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
                name: "EGRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EGRoleClaim_EGRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EGRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EGCardClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGCardClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EGCardClaims_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EGCardLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGCardLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_EGCardLogins_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EGCardRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGCardRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_EGCardRoles_EGRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EGRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EGCardRoles_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EGCardToken",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGCardToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_EGCardToken_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
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
                name: "HealthRecordAttachments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 135, nullable: false),
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
                name: "IX_EGCardClaims_UserId",
                table: "EGCardClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EGCardLogins_UserId",
                table: "EGCardLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EGCardRoles_RoleId",
                table: "EGCardRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EGCards_CitizenId",
                table: "EGCards",
                column: "CitizenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "EGCards",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "EGCards",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EGRoleClaim_RoleId",
                table: "EGRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "EGRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "EGCardClaims");

            migrationBuilder.DropTable(
                name: "EGCardLogins");

            migrationBuilder.DropTable(
                name: "EGCardRoles");

            migrationBuilder.DropTable(
                name: "EGCardToken");

            migrationBuilder.DropTable(
                name: "EGRoleClaim");

            migrationBuilder.DropTable(
                name: "HealthRecordAttachments");

            migrationBuilder.DropTable(
                name: "EGCards");

            migrationBuilder.DropTable(
                name: "EGRoles");

            migrationBuilder.DropTable(
                name: "HealthRecords");

            migrationBuilder.DropTable(
                name: "HealthInformation");

            migrationBuilder.DropTable(
                name: "CitizenDetails");
        }
    }
}
