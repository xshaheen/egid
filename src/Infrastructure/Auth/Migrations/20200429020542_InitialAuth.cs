using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EGID.Infrastructure.Auth.Migrations
{
    public partial class InitialAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    PrivateKeyXml = table.Column<string>(nullable: false),
                    PublicKeyXml = table.Column<string>(nullable: false),
                    Pin1 = table.Column<string>(maxLength: 128, nullable: false),
                    Pin2 = table.Column<string>(maxLength: 128, nullable: false),
                    Puk = table.Column<string>(maxLength: 128, nullable: false),
                    CardIssuer = table.Column<string>(maxLength: 128, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    TerminationDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGCards", x => x.Id);
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
                name: "EGUserClaims",
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
                    table.PrimaryKey("PK_EGUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EGUserClaims_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EGUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_EGUserLogins_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EGUserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_EGUserToken_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
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
                name: "EGUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EGUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_EGUserRoles_EGRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "EGRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EGUserRoles_EGCards_UserId",
                        column: x => x.UserId,
                        principalTable: "EGCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_EGUserClaims_UserId",
                table: "EGUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EGUserLogins_UserId",
                table: "EGUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EGUserRoles_RoleId",
                table: "EGUserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EGRoleClaim");

            migrationBuilder.DropTable(
                name: "EGUserClaims");

            migrationBuilder.DropTable(
                name: "EGUserLogins");

            migrationBuilder.DropTable(
                name: "EGUserRoles");

            migrationBuilder.DropTable(
                name: "EGUserToken");

            migrationBuilder.DropTable(
                name: "EGRoles");

            migrationBuilder.DropTable(
                name: "EGCards");
        }
    }
}
