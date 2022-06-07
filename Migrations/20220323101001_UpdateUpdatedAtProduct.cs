using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Migrations
{
    public partial class UpdateUpdatedAtProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Products",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 379, DateTimeKind.Utc).AddTicks(6892), "Laborum quam et tenetur vel provident cupiditate sunt. Voluptas at beatae repudiandae. Quasi ut quod dolor molestiae.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 404, DateTimeKind.Utc).AddTicks(9037), "Voluptatem maiores sit et voluptatem. Et nulla assumenda aperiam illum ratione odio exercitationem. Mollitia sed omnis sed dolorum est.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 405, DateTimeKind.Utc).AddTicks(5490), "Accusantium qui similique animi beatae. Omnis quo nisi omnis voluptatibus dolorem. Doloribus veniam aperiam iure id ut impedit qui quia quia.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 406, DateTimeKind.Utc).AddTicks(2126), "Qui voluptatem natus minima quod quos. Dolor ut alias distinctio voluptatem. Omnis aut dolorem mollitia numquam sunt iure beatae ducimus earum.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 406, DateTimeKind.Utc).AddTicks(8655), "Voluptas vitae officia voluptatem inventore pariatur. Nulla dolor labore magnam. Recusandae rerum doloribus veniam.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 407, DateTimeKind.Utc).AddTicks(4151), "Enim aperiam repellendus error ratione odio error voluptas vel. Ea ab aut voluptas occaecati. Omnis perferendis minus atque enim ea maxime minima et voluptatem.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 416, DateTimeKind.Utc).AddTicks(2900), "Eius animi aliquid quisquam non necessitatibus aut molestiae. Voluptatem vel eum veritatis aut id fuga. Quasi ipsum dolore optio autem.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 416, DateTimeKind.Utc).AddTicks(9870), "Autem consequuntur beatae vitae est optio. Praesentium et vitae repellat. Dolor aliquam hic voluptate itaque voluptates qui voluptate qui.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 417, DateTimeKind.Utc).AddTicks(7580), "Et est possimus velit maxime perferendis perspiciatis esse accusamus rerum. Qui ea quis tenetur voluptas molestiae rerum delectus iure. Est rerum sint quo saepe voluptatibus ducimus reiciendis voluptates.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 418, DateTimeKind.Utc).AddTicks(6529), "Provident excepturi adipisci et enim quam. Numquam excepturi itaque sunt tempora assumenda itaque. Molestias et id sint sed.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 419, DateTimeKind.Utc).AddTicks(2761), "Non repellat pariatur mollitia libero. Consequatur voluptatem ut non est sit voluptates et. Consequatur non voluptas eos nihil.", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 23, 10, 10, 0, 419, DateTimeKind.Utc).AddTicks(8471), "Praesentium ipsum labore eum deleniti itaque odit vitae et voluptatibus. Tempore perspiciatis veniam et eos. Nesciunt quae autem quae quam autem.", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 168, DateTimeKind.Local).AddTicks(6887), "Molestiae animi corporis in beatae. Ut ab est ullam harum consequatur molestias. Omnis rerum est necessitatibus quam eum.", new DateTime(2022, 3, 21, 20, 54, 38, 171, DateTimeKind.Local).AddTicks(2158) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(250), "Dicta dolorum ipsa consequatur in. Sapiente voluptatibus et aut reprehenderit magnam accusantium sed officia voluptas. Praesentium ratione dolor quidem voluptas sit quam atque voluptas nostrum.", new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(295) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(8275), "Qui nam placeat expedita est adipisci et. Sint nobis est error corrupti. Facere sed eum sit.", new DateTime(2022, 3, 21, 20, 54, 38, 204, DateTimeKind.Local).AddTicks(8289) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 205, DateTimeKind.Local).AddTicks(3600), "Est expedita inventore est ea. Reprehenderit ipsa et inventore hic magnam eveniet consequatur occaecati et. Porro est ullam qui non deleniti voluptas a voluptas.", new DateTime(2022, 3, 21, 20, 54, 38, 205, DateTimeKind.Local).AddTicks(3612) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(1107), "Enim eaque eius quisquam quas nesciunt. Ut ut mollitia voluptatum harum ipsam quam iure. Voluptatibus non aut ut ipsum quo.", new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(1119) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(7104), "Repellendus dolorum vero incidunt. Officia autem nemo aspernatur delectus autem. Voluptatem voluptatum repellat quae.", new DateTime(2022, 3, 21, 20, 54, 38, 206, DateTimeKind.Local).AddTicks(7113) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(1956), "Omnis laboriosam non est veniam expedita ab quam et. Est facere perspiciatis nostrum aut corrupti ut est doloribus tenetur. Et atque commodi minima sint.", new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(1968) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(9403), "Sapiente temporibus aliquam harum quidem aliquid. Omnis animi autem molestiae aliquid. Non culpa et expedita et labore perspiciatis.", new DateTime(2022, 3, 21, 20, 54, 38, 207, DateTimeKind.Local).AddTicks(9413) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 208, DateTimeKind.Local).AddTicks(5218), "Atque et neque laudantium laboriosam voluptatem. Qui recusandae quo incidunt recusandae voluptatem voluptatum iure eveniet corrupti. Natus maiores tempore dolores non.", new DateTime(2022, 3, 21, 20, 54, 38, 208, DateTimeKind.Local).AddTicks(5229) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 215, DateTimeKind.Local).AddTicks(5002), "Odit aspernatur molestiae sint dignissimos voluptate et. Aut molestias sequi autem quia quasi. Optio ea quisquam officiis et dolorum placeat nihil vero quo.", new DateTime(2022, 3, 21, 20, 54, 38, 215, DateTimeKind.Local).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 216, DateTimeKind.Local).AddTicks(4149), "Voluptatem vel illo quae ut iusto assumenda perferendis repellat fugiat. Magnam harum voluptatum ipsa saepe atque nesciunt non. Dolor facilis et aut et unde aut commodi.", new DateTime(2022, 3, 21, 20, 54, 38, 216, DateTimeKind.Local).AddTicks(4183) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "Description", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 3, 21, 20, 54, 38, 217, DateTimeKind.Local).AddTicks(2955), "Et est aperiam provident eos. Expedita expedita aut unde neque dolorum id. Quasi consectetur necessitatibus ex laboriosam.", new DateTime(2022, 3, 21, 20, 54, 38, 217, DateTimeKind.Local).AddTicks(2985) });
        }
    }
}
