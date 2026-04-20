using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CelSyte.Migrations
{
    /// <inheritdoc />
    public partial class AddCompositionElement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanvasImage");

            migrationBuilder.CreateTable(
                name: "CompositionElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    CanvasId = table.Column<int>(type: "int", nullable: false),
                    XCoord = table.Column<double>(type: "float", nullable: false),
                    YCoord = table.Column<double>(type: "float", nullable: false),
                    Opacity = table.Column<int>(type: "int", nullable: false),
                    Scale = table.Column<double>(type: "float", nullable: false),
                    RotationAngle = table.Column<double>(type: "float", nullable: false),
                    OrderPlacement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositionElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompositionElement_Canvas_CanvasId",
                        column: x => x.CanvasId,
                        principalTable: "Canvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CompositionElement_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompositionElement_CanvasId",
                table: "CompositionElement",
                column: "CanvasId");

            migrationBuilder.CreateIndex(
                name: "IX_CompositionElement_ImageId",
                table: "CompositionElement",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompositionElement");

            migrationBuilder.CreateTable(
                name: "CanvasImage",
                columns: table => new
                {
                    CanvasesId = table.Column<int>(type: "int", nullable: false),
                    ImagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanvasImage", x => new { x.CanvasesId, x.ImagesId });
                    table.ForeignKey(
                        name: "FK_CanvasImage_Canvas_CanvasesId",
                        column: x => x.CanvasesId,
                        principalTable: "Canvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanvasImage_Image_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanvasImage_ImagesId",
                table: "CanvasImage",
                column: "ImagesId");
        }
    }
}
