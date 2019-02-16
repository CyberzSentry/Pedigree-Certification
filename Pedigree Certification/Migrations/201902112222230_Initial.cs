namespace Pedigree_Certification.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Breeders",
                c => new
                    {
                        BreederId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.BreederId);
            
            CreateTable(
                "dbo.Dogs",
                c => new
                    {
                        DogId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ChipNumber = c.String(),
                        Nickname = c.String(),
                        PedegreeCertificateNumber = c.String(),
                        Rase = c.String(),
                        Tatoo = c.String(),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Coat = c.String(),
                        Sex = c.String(),
                        Dysplasia = c.String(),
                        FatherId = c.Int(),
                        MotherId = c.Int(),
                        DNA = c.String(),
                        ExhibitionTitles = c.String(),
                        Training = c.String(),
                        CertificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CertificationNo = c.Int(),
                        ParentId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Breeder_BreederId = c.Int(),
                        Owner_OwnerId = c.Int(),
                        Parent_DogId = c.Int(),
                    })
                .PrimaryKey(t => t.DogId)
                .ForeignKey("dbo.Breeders", t => t.Breeder_BreederId)
                .ForeignKey("dbo.Owners", t => t.Owner_OwnerId)
                .ForeignKey("dbo.Dogs", t => t.Parent_DogId)
                .Index(t => t.Breeder_BreederId)
                .Index(t => t.Owner_OwnerId)
                .Index(t => t.Parent_DogId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Adderss = c.String(),
                    })
                .PrimaryKey(t => t.OwnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dogs", "Parent_DogId", "dbo.Dogs");
            DropForeignKey("dbo.Dogs", "Owner_OwnerId", "dbo.Owners");
            DropForeignKey("dbo.Dogs", "Breeder_BreederId", "dbo.Breeders");
            DropIndex("dbo.Dogs", new[] { "Parent_DogId" });
            DropIndex("dbo.Dogs", new[] { "Owner_OwnerId" });
            DropIndex("dbo.Dogs", new[] { "Breeder_BreederId" });
            DropTable("dbo.Owners");
            DropTable("dbo.Dogs");
            DropTable("dbo.Breeders");
        }
    }
}
