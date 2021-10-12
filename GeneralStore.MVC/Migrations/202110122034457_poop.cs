namespace GeneralStore.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transactions", "ItemCount");
            DropColumn("dbo.Transactions", "TransactionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "TransactionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transactions", "ItemCount", c => c.Int(nullable: false));
        }
    }
}
