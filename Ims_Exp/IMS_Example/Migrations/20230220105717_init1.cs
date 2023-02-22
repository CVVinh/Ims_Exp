using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IMS_Example.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    IdDevice = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    Info = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    UserCreated = table.Column<int>(type: "integer", nullable: false),
                    UserUpdated = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    DateUpdated = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Note = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.IdDevice);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameGroup = table.Column<string>(type: "text", nullable: false),
                    Discription = table.Column<string>(type: "text", nullable: true),
                    userCreated = table.Column<int>(type: "integer", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "date", nullable: true),
                    userModified = table.Column<int>(type: "integer", nullable: true),
                    dateModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "varchar", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idModule = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "Text", nullable: false),
                    icon = table.Column<string>(type: "Text", nullable: false),
                    view = table.Column<string>(type: "Text", nullable: false),
                    controller = table.Column<string>(type: "Text", nullable: false),
                    action = table.Column<string>(type: "Text", nullable: false),
                    parent = table.Column<int>(type: "integer", nullable: false),
                    isDeleted = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameModule = table.Column<string>(type: "text", nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<int>(type: "integer", nullable: false),
                    idSort = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permission_Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdGroup = table.Column<int>(type: "integer", nullable: false),
                    IdModule = table.Column<int>(type: "integer", nullable: false),
                    Access = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission_Use_Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idModule = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    IdMenu = table.Column<int>(type: "integer", nullable: false),
                    Add = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Update = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Delete = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Export = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    userCreated = table.Column<int>(type: "integer", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    userModified = table.Column<int>(type: "integer", nullable: true),
                    dateModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission_Use_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Leader = table.Column<int>(type: "integer", nullable: false),
                    UserCreated = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "date", nullable: false),
                    UserUpdate = table.Column<int>(type: "integer", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "date", nullable: false),
                    IsOnGitlab = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    idRole = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameRole = table.Column<string>(type: "varchar", nullable: false),
                    description = table.Column<string>(type: "varchar", nullable: false),
                    disabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.idRole);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userCode = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    userPassword = table.Column<string>(type: "varchar", maxLength: 1000, nullable: false),
                    userCreated = table.Column<int>(type: "integer", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    userModified = table.Column<int>(type: "integer", nullable: true),
                    dateModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    firstName = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    dOB = table.Column<DateTime>(type: "date", nullable: true),
                    gender = table.Column<byte>(type: "smallint", nullable: true),
                    address = table.Column<string>(type: "varchar", maxLength: 200, nullable: true),
                    university = table.Column<string>(type: "varchar", maxLength: 100, nullable: true),
                    yearGraduated = table.Column<int>(type: "integer", nullable: true),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    emailPassword = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    skype = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    skypePassword = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    workStatus = table.Column<byte>(type: "smallint", nullable: false),
                    dateStartWork = table.Column<DateTime>(type: "date", nullable: false, defaultValue: new DateTime(2023, 2, 20, 17, 57, 17, 888, DateTimeKind.Local).AddTicks(2955)),
                    dateLeave = table.Column<DateTime>(type: "date", nullable: true),
                    maritalStatus = table.Column<byte>(type: "smallint", nullable: true),
                    reasonResignation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    identitycard = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<byte>(type: "smallint", nullable: false),
                    refreshToken = table.Column<string>(type: "text", nullable: true),
                    refreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IdGroup = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_Key",
                table: "Group",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectCode",
                table: "Projects",
                column: "ProjectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_userCode",
                table: "Users",
                column: "userCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Permission_Group");

            migrationBuilder.DropTable(
                name: "Permission_Use_Menu");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
