using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageAccountApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCOUNTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCOUNTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CHECKING_ACCOUNTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ACCOUNT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TYPE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    BALANCE = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false),
                    INTEREST_RATE = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHECKING_ACCOUNTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CHECKING_ACCOUNTS_ACCOUNTS_ACCOUNT_ID",
                        column: x => x.ACCOUNT_ID,
                        principalTable: "ACCOUNTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SAVINGS_ACCOUNTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ACCOUNT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TYPE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    BALANCE = table.Column<decimal>(type: "NUMBER(18,2)", nullable: false),
                    INTEREST_RATE = table.Column<decimal>(type: "NUMBER(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAVINGS_ACCOUNTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SAVINGS_ACCOUNTS_ACCOUNTS_ACCOUNT_ID",
                        column: x => x.ACCOUNT_ID,
                        principalTable: "ACCOUNTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHECKING_ACCOUNTS_ACCOUNT_ID",
                table: "CHECKING_ACCOUNTS",
                column: "ACCOUNT_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SAVINGS_ACCOUNTS_ACCOUNT_ID",
                table: "SAVINGS_ACCOUNTS",
                column: "ACCOUNT_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHECKING_ACCOUNTS");

            migrationBuilder.DropTable(
                name: "SAVINGS_ACCOUNTS");

            migrationBuilder.DropTable(
                name: "ACCOUNTS");
        }
    }
}
