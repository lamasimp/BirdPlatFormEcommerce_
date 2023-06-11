using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdPlatFormEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileLengthType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_ProductCategory",
                columns: table => new
                {
                    CateID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ProductCategory", x => x.CateID);
                });

            migrationBuilder.CreateTable(
                name: "tb_Role",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tb_User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Avatar = table.Column<byte[]>(type: "image", nullable: true),
                    Phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ShopID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_tb_User_tb_Role",
                        column: x => x.RoleID,
                        principalTable: "tb_Role",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Payment", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_tb_Payment_tb_User",
                        column: x => x.UserID,
                        principalTable: "tb_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Shop",
                columns: table => new
                {
                    ShopID = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    ShopName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Is_verified = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Shop", x => x.ShopID);
                    table.ForeignKey(
                        name: "FK_tb_Shop_tb_User",
                        column: x => x.ShopID,
                        principalTable: "tb_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Token",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "ntext", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Token", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_Token_tb_User",
                        column: x => x.UserID,
                        principalTable: "tb_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_tb_Order_tb_Payment",
                        column: x => x.PaymentID,
                        principalTable: "tb_Payment",
                        principalColumn: "PaymentID");
                    table.ForeignKey(
                        name: "FK_tb_Order_tb_User",
                        column: x => x.UserID,
                        principalTable: "tb_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Detail = table.Column<string>(type: "ntext", nullable: true),
                    QuantitySold = table.Column<int>(type: "int", nullable: true),
                    CateID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ShopID = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    Thumbnail = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    SoldPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_tb_Product_tb_ProductCategory",
                        column: x => x.CateID,
                        principalTable: "tb_ProductCategory",
                        principalColumn: "CateID");
                    table.ForeignKey(
                        name: "FK_tb_Product_tb_Shop",
                        column: x => x.ShopID,
                        principalTable: "tb_Shop",
                        principalColumn: "ShopID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Profit",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    Orderdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Sales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_Profit_tb_Order",
                        column: x => x.OrderID,
                        principalTable: "tb_Order",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_tb_Sales_tb_Shop",
                        column: x => x.ShopID,
                        principalTable: "tb_Shop",
                        principalColumn: "ShopID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Cart",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ShopName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Cart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_Cart_tb_Product",
                        column: x => x.ProductID,
                        principalTable: "tb_Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_tb_Cart_tb_User",
                        column: x => x.UserID,
                        principalTable: "tb_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Feedback",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: true),
                    Detail = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Feedback", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_Feedback_tb_Product",
                        column: x => x.ProductID,
                        principalTable: "tb_Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_tb_Feedback_tb_User",
                        column: x => x.UserID,
                        principalTable: "tb_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Image",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    VideoPath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Image", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_Image_tb_Product",
                        column: x => x.ProductID,
                        principalTable: "tb_Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "tb_OrderDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_OrderDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_OrderDetail_tb_Order",
                        column: x => x.OrderID,
                        principalTable: "tb_Order",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_tb_OrderDetail_tb_Product",
                        column: x => x.ProductID,
                        principalTable: "tb_Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "tb_Post",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Post", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_Post_tb_Product",
                        column: x => x.ProductID,
                        principalTable: "tb_Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_tb_Post_tb_Shop",
                        column: x => x.ShopID,
                        principalTable: "tb_Shop",
                        principalColumn: "ShopID");
                });

            migrationBuilder.CreateTable(
                name: "tb_WishList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_WishList", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_WishList_tb_Product",
                        column: x => x.ProductID,
                        principalTable: "tb_Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_tb_WishList_tb_User",
                        column: x => x.UserID,
                        principalTable: "tb_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Cart_ProductID",
                table: "tb_Cart",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Cart_UserID",
                table: "tb_Cart",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Feedback_ProductID",
                table: "tb_Feedback",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Feedback_UserID",
                table: "tb_Feedback",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Image_ProductID",
                table: "tb_Image",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Order_PaymentID",
                table: "tb_Order",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Order_UserID",
                table: "tb_Order",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_OrderDetail_OrderID",
                table: "tb_OrderDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_OrderDetail_ProductID",
                table: "tb_OrderDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Payment_UserID",
                table: "tb_Payment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Post_ProductID",
                table: "tb_Post",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Post_ShopID",
                table: "tb_Post",
                column: "ShopID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Product_CateID",
                table: "tb_Product",
                column: "CateID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Product_ShopID",
                table: "tb_Product",
                column: "ShopID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Profit_OrderID",
                table: "tb_Profit",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Profit_ShopID",
                table: "tb_Profit",
                column: "ShopID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Token_UserID",
                table: "tb_Token",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_User_RoleID",
                table: "tb_User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_WishList_ProductID",
                table: "tb_WishList",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_WishList_UserID",
                table: "tb_WishList",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Cart");

            migrationBuilder.DropTable(
                name: "tb_Feedback");

            migrationBuilder.DropTable(
                name: "tb_Image");

            migrationBuilder.DropTable(
                name: "tb_OrderDetail");

            migrationBuilder.DropTable(
                name: "tb_Post");

            migrationBuilder.DropTable(
                name: "tb_Profit");

            migrationBuilder.DropTable(
                name: "tb_Token");

            migrationBuilder.DropTable(
                name: "tb_WishList");

            migrationBuilder.DropTable(
                name: "tb_Order");

            migrationBuilder.DropTable(
                name: "tb_Product");

            migrationBuilder.DropTable(
                name: "tb_Payment");

            migrationBuilder.DropTable(
                name: "tb_ProductCategory");

            migrationBuilder.DropTable(
                name: "tb_Shop");

            migrationBuilder.DropTable(
                name: "tb_User");

            migrationBuilder.DropTable(
                name: "tb_Role");
        }
    }
}
