namespace AutoLotDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Custld", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Carld", "dbo.inventory");
            DropPrimaryKey("dbo.CreditRisks");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.inventory");
            DropColumn("dbo.CreditRisks", "CustID");
            DropColumn("dbo.Customers", "CustId");
            DropColumn("dbo.Orders", "Orderld");
            DropColumn("dbo.inventory", "CarId");
            AddColumn("dbo.CreditRisks", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CreditRisks", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.inventory", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.inventory", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddPrimaryKey("dbo.CreditRisks", "Id");
            AddPrimaryKey("dbo.Customers", "Id");
            AddPrimaryKey("dbo.Orders", "Id");
            AddPrimaryKey("dbo.inventory", "Id");
            CreateIndex("dbo.CreditRisks", new[] { "LastName", "FirstName" }, unique: true, name: "IDX_CreditRisk_Name");
            AddForeignKey("dbo.Orders", "Custld", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Carld", "dbo.inventory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Carld", "dbo.inventory");
            DropForeignKey("dbo.Orders", "Custld", "dbo.Customers");
            DropPrimaryKey("dbo.inventory");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.CreditRisks");
            DropColumn("dbo.inventory", "Id");
            DropColumn("dbo.Orders", "Id");
            DropColumn("dbo.Customers", "Id");
            DropColumn("dbo.CreditRisks", "Id");
            AddColumn("dbo.inventory", "CarId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "Orderld", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "CustId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CreditRisks", "CustID", c => c.Int(nullable: false, identity: true));
            
            DropIndex("dbo.CreditRisks", "IDX_CreditRisk_Name");
            DropColumn("dbo.inventory", "Timestamp");
            DropColumn("dbo.Orders", "Timestamp");
            DropColumn("dbo.Customers", "Timestamp");
            DropColumn("dbo.CreditRisks", "Timestamp");
            AddPrimaryKey("dbo.inventory", "CarId");
            AddPrimaryKey("dbo.Orders", "Orderld");
            AddPrimaryKey("dbo.Customers", "CustId");
            AddPrimaryKey("dbo.CreditRisks", "CustID");
            AddForeignKey("dbo.Orders", "Carld", "dbo.inventory", "CarId");
            AddForeignKey("dbo.Orders", "Custld", "dbo.Customers", "CustId", cascadeDelete: true);
        }
    }
}
