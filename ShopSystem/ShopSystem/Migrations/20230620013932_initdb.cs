using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopSystem.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    ENumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EId = table.Column<string>(type: "longtext", nullable: false),
                    EName = table.Column<string>(type: "longtext", nullable: false),
                    ESex = table.Column<string>(type: "longtext", nullable: false),
                    EAge = table.Column<string>(type: "longtext", nullable: false),
                    EBirth = table.Column<string>(type: "longtext", nullable: false),
                    EEmail = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    EIdentity = table.Column<string>(type: "longtext", nullable: false, comment: "身份"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsOnJob = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.ENumber);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    PNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PId = table.Column<string>(type: "longtext", nullable: false),
                    PName = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    SellbyDate = table.Column<int>(type: "int", nullable: false, comment: "保质期"),
                    ProduceDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "生产日期"),
                    WarehousingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "入库时间"),
                    Inventory = table.Column<int>(type: "int", nullable: false, comment: "库存"),
                    IsExpire = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否过期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.PNumber);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UId = table.Column<string>(type: "longtext", nullable: false),
                    UName = table.Column<string>(type: "longtext", nullable: false),
                    UPassword = table.Column<string>(type: "longtext", nullable: false),
                    UIdentity = table.Column<string>(type: "longtext", nullable: false, comment: "身份"),
                    IsOnJob = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UNumber);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
