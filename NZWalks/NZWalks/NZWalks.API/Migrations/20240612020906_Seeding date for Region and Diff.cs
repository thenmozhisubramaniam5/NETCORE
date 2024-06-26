using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdateforRegionandDiff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("867102b8-6f1f-4d66-8362-0831c7476b8d"), "Hard" },
                    { new Guid("94354fc7-2798-4985-803d-325e4ab03797"), "Medium" },
                    { new Guid("b7aadb18-67bb-4948-bffe-0e43452b1fe5"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("311adc75-a217-43d3-a762-73e367185a6c"), "GRL", "GreenLand", "Green123.jpg" },
                    { new Guid("4ac91331-a755-45bc-b53b-27454f5ab968"), "AKL", "Auckland Region", "Test123.jpg" },
                    { new Guid("8f9d51b4-58b7-4b67-90bb-7f402ef2beb7"), "SAR", "South Africa Region", "SARegion.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("867102b8-6f1f-4d66-8362-0831c7476b8d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("94354fc7-2798-4985-803d-325e4ab03797"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b7aadb18-67bb-4948-bffe-0e43452b1fe5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("311adc75-a217-43d3-a762-73e367185a6c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4ac91331-a755-45bc-b53b-27454f5ab968"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8f9d51b4-58b7-4b67-90bb-7f402ef2beb7"));
        }
    }
}
