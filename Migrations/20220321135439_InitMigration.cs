using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Văn Học" },
                    { 2L, "Giáo Khoa" },
                    { 3L, "Thiếu Nhi" },
                    { 4L, "Tâm Lý - Kỹ Năng Sống" },
                    { 5L, "Manga - Comic" },
                    { 6L, "Sách Học Ngoại Ngữ" },
                    { 7L, "Kinh Tế" },
                    { 8L, "Khoa Học Kỹ Thuật" },
                    { 9L, "Lịch Sử - Địa Lý - Tôn Giáo" },
                    { 10L, "Nuôi Dạy Con" },
                    { 11L, "Chính Trị - Pháp Lý - Triết Học" },
                    { 12L, "Nữ Công Gia Chánh" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedBy", "Description", "Name", "Price", "Quantity", "Sku", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2022, 3, 21, 20, 54, 38, 168, DateTimeKind.Local).AddTicks(6887), null, "Molestiae animi corporis in beatae. Ut ab est ullam harum consequatur molestias. Omnis rerum est necessitatibus quam eum.", "Khuôn Mặt Người Khác", 103000.0, 20, "S1", new DateTime(2022, 3, 21, 20, 54, 38, 171, DateTimeKind.Local).AddTicks(2158), null },
                    { 2L, 2L, new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(250), null, "Dicta dolorum ipsa consequatur in. Sapiente voluptatibus et aut reprehenderit magnam accusantium sed officia voluptas. Praesentium ratione dolor quidem voluptas sit quam atque voluptas nostrum.", "Hội Vệ Nhân", 152000.0, 20, "S2", new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(295), null },
                    { 8L, 2L, new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(9403), null, "Sapiente temporibus aliquam harum quidem aliquid. Omnis animi autem molestiae aliquid. Non culpa et expedita et labore perspiciatis.", "Khải Vi Về Cõi Vô Hình", 130000.0, 20, "S8", new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(9413), null },
                    { 3L, 3L, new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(8275), null, "Qui nam placeat expedita est adipisci et. Sint nobis est error corrupti. Facere sed eum sit.", "Khuôn Mặt Người Khác", 103.53, 20, "S3", new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(8289), null },
                    { 9L, 3L, new DateTime(2022, 3, 21, 20, 54, 38, 208, DateTimeKind.Local).AddTicks(5218), null, "Atque et neque laudantium laboriosam voluptatem. Qui recusandae quo incidunt recusandae voluptatem voluptatum iure eveniet corrupti. Natus maiores tempore dolores non.", "Thần Thoại Hy Lạp", 191000.0, 20, "S9", new DateTime(2022, 3, 21, 20, 54, 38, 208, DateTimeKind.Local).AddTicks(5229), null },
                    { 4L, 4L, new DateTime(2022, 3, 21, 20, 54, 38, 205, DateTimeKind.Local).AddTicks(3600), null, "Est expedita inventore est ea. Reprehenderit ipsa et inventore hic magnam eveniet consequatur occaecati et. Porro est ullam qui non deleniti voluptas a voluptas.", "Giọt rừng", 86000.0, 20, "S4", new DateTime(2022, 3, 21, 20, 54, 38, 205, DateTimeKind.Local).AddTicks(3612), null },
                    { 10L, 4L, new DateTime(2022, 3, 21, 20, 54, 38, 215, DateTimeKind.Local).AddTicks(5002), null, "Odit aspernatur molestiae sint dignissimos voluptate et. Aut molestias sequi autem quia quasi. Optio ea quisquam officiis et dolorum placeat nihil vero quo.", "Lì quá để nói quài", 67000.0, 20, "S10", new DateTime(2022, 3, 21, 20, 54, 38, 215, DateTimeKind.Local).AddTicks(5050), null },
                    { 5L, 5L, new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(1107), null, "Enim eaque eius quisquam quas nesciunt. Ut ut mollitia voluptatum harum ipsam quam iure. Voluptatibus non aut ut ipsum quo.", "Người rủ ngủ", 120000.0, 20, "S5", new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(1119), null },
                    { 11L, 5L, new DateTime(2022, 3, 21, 20, 54, 38, 216, DateTimeKind.Local).AddTicks(4149), null, "Voluptatem vel illo quae ut iusto assumenda perferendis repellat fugiat. Magnam harum voluptatum ipsa saepe atque nesciunt non. Dolor facilis et aut et unde aut commodi.", "Cỏ Khô Lầm Lỡ Muốt Mùa", 180000.0, 20, "S11", new DateTime(2022, 3, 21, 20, 54, 38, 216, DateTimeKind.Local).AddTicks(4183), null },
                    { 6L, 6L, new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(7104), null, "Repellendus dolorum vero incidunt. Officia autem nemo aspernatur delectus autem. Voluptatem voluptatum repellat quae.", "Mong Mẹ Hãy Yêu Lấy Chính Mình", 86000.0, 20, "S6", new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(7113), null },
                    { 7L, 7L, new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(1956), null, "Omnis laboriosam non est veniam expedita ab quam et. Est facere perspiciatis nostrum aut corrupti ut est doloribus tenetur. Et atque commodi minima sint.", "The Friend - Bạn Đồng Hành", 95000.0, 20, "S7", new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(1968), null },
                    { 12L, 7L, new DateTime(2022, 3, 21, 20, 54, 38, 217, DateTimeKind.Local).AddTicks(2955), null, "Et est aperiam provident eos. Expedita expedita aut unde neque dolorum id. Quasi consectetur necessitatibus ex laboriosam.", "Hiểm Họa Ở Nhà Kết", 91000.0, 20, "S12", new DateTime(2022, 3, 21, 20, 54, 38, 217, DateTimeKind.Local).AddTicks(2985), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Sku",
                table: "Products",
                column: "Sku",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
