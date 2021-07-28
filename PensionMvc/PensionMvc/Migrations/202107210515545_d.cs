namespace PensionMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PensionDetails",
                c => new
                    {
                        PAN = c.String(nullable: false, maxLength: 128),
                        NamePensioner = c.String(),
                        DOB = c.DateTime(nullable: false),
                        PensionSelected = c.String(),
                        PensionAmt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PAN);
            
            CreateTable(
                "dbo.PensionerInputs",
                c => new
                    {
                        PAN = c.String(nullable: false, maxLength: 128),
                        NamePensioner = c.String(),
                        DOB = c.DateTime(nullable: false),
                        AadhaarNo = c.String(),
                        PensionSelected = c.String(),
                    })
                .PrimaryKey(t => t.PAN);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PensionerInputs");
            DropTable("dbo.PensionDetails");
        }
    }
}
