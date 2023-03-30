namespace AssignColor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductColor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ColorID = c.Long(nullable: false, identity: true),
                        ColorName = c.String(),
                    })
                .PrimaryKey(t => t.ColorID);
            
            CreateTable(
                "dbo.ProductColors",
                c => new
                    {
                        ProductColorID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        ColorID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ProductColorID)
                .ForeignKey("dbo.Colors", t => t.ColorID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.ColorID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(),
                        MfgName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductColors", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductColors", "ColorID", "dbo.Colors");
            DropIndex("dbo.ProductColors", new[] { "ColorID" });
            DropIndex("dbo.ProductColors", new[] { "ProductID" });
            DropTable("dbo.Products");
            DropTable("dbo.ProductColors");
            DropTable("dbo.Colors");
        }
    }
}
