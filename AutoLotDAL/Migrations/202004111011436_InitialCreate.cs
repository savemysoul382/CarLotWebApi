namespace AutoLotDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditRisks",
                c => new
                    {
                        CustID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CustId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Orderld = c.Int(nullable: false, identity: true),
                        Custld = c.Int(nullable: false),
                        Carld = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Orderld)
                .ForeignKey("dbo.inventory", t => t.Carld)
                .ForeignKey("dbo.Customers", t => t.Custld, cascadeDelete: true)
                .Index(t => t.Custld)
                .Index(t => t.Carld);
            
            CreateTable(
                "dbo.inventory",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Make = c.String(maxLength: 50, fixedLength: true),
                        Color = c.String(maxLength: 50, fixedLength: true),
                        PetName = c.String(maxLength: 50, fixedLength: true),
                    })
                .PrimaryKey(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Custld", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Carld", "dbo.inventory");
            DropIndex("dbo.Orders", new[] { "Carld" });
            DropIndex("dbo.Orders", new[] { "Custld" });
            DropTable("dbo.inventory");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.CreditRisks");
        }
    }
}
