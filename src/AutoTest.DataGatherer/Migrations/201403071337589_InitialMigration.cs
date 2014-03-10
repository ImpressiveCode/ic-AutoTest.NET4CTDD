namespace AutoTest.Net.DataGatherer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogDumps",
                c => new
                    {
                        LogDumpId = c.Int(nullable: false, identity: true),
                        StationName = c.String(),
                        NumberOfBuildsFailed = c.Int(nullable: false),
                        NumberOfBuildsSucceeded = c.Int(nullable: false),
                        NumberOfProjectsBuilt = c.Int(nullable: false),
                        NumberOfTestsFailed = c.Int(nullable: false),
                        NumberOfTestsIgnored = c.Int(nullable: false),
                        NumberOfTestsPassed = c.Int(nullable: false),
                        NumberOfTestsRan = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.LogDumpId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
            DropTable("dbo.LogDumps");
        }
    }
}
