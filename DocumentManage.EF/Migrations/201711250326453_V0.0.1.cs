namespace DocumentManage.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AuthModels",
                c => new
                    {
                        AuthID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        AuthName = c.String(unicode: false),
                        ParentID = c.String(unicode: false),
                        AuthUrl = c.String(unicode: false),
                        CSSClass = c.String(unicode: false),
                        Target = c.String(unicode: false),
                        OrderNo = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AuthID);
            
            CreateTable(
                "AuthRoleMaps",
                c => new
                    {
                        MapID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        AuthID = c.String(unicode: false),
                        RoleID = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.MapID);
            
            CreateTable(
                "LoginLogs",
                c => new
                    {
                        LogID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        LoginAccount = c.String(unicode: false),
                        LoginName = c.String(unicode: false),
                        LoginTime = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.LogID);
            
            CreateTable(
                "Orgnazitions",
                c => new
                    {
                        OrgID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        OrgName = c.String(unicode: false),
                        FromType = c.String(unicode: false),
                        ShortNameCN = c.String(unicode: false),
                        OrgNameEN = c.String(unicode: false),
                        ShortNameEN = c.String(unicode: false),
                        Tag = c.String(unicode: false),
                        Level = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Continent = c.String(unicode: false),
                        Country = c.String(unicode: false),
                        Province = c.String(unicode: false),
                        OrgType = c.String(unicode: false),
                        OrgBack = c.String(unicode: false),
                        OrgInfo = c.String(unicode: false),
                        WorkAddress = c.String(unicode: false),
                        WorkTime = c.String(unicode: false),
                        ContactPerson1 = c.String(unicode: false),
                        ContactPerson2 = c.String(unicode: false),
                        Tel1 = c.String(unicode: false),
                        Tel2 = c.String(unicode: false),
                        Email1 = c.String(unicode: false),
                        Email2 = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        CreateUserID = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        ModifyUserID = c.String(unicode: false),
                        ModifyTime = c.DateTime(precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrgID);
            
            CreateTable(
                "PersonInfoes",
                c => new
                    {
                        PersonID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        FromType = c.String(unicode: false),
                        OrgID = c.String(unicode: false),
                        OrgName = c.String(unicode: false),
                        NameCN = c.String(unicode: false),
                        NameEN = c.String(unicode: false),
                        Tag = c.String(unicode: false),
                        Department = c.String(unicode: false),
                        PassportCode = c.String(unicode: false),
                        PassportUrlName = c.String(unicode: false),
                        PassportUrl = c.String(unicode: false),
                        PassportDate = c.DateTime(precision: 0),
                        PassportSignDate = c.DateTime(precision: 0),
                        PassportSignAdress = c.String(unicode: false),
                        PassportType = c.String(unicode: false),
                        PhotoUrl = c.String(unicode: false),
                        PhotoUrlName = c.String(unicode: false),
                        Title = c.String(unicode: false),
                        IDNumber = c.String(unicode: false),
                        IDNumberUrl = c.String(unicode: false),
                        IDNumberUrlName = c.String(unicode: false),
                        Duty = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Tel1 = c.String(unicode: false),
                        Tel2 = c.String(unicode: false),
                        Mobile1 = c.String(unicode: false),
                        Mobile2 = c.String(unicode: false),
                        ContactAddress = c.String(unicode: false),
                        Sex = c.String(unicode: false),
                        Birth = c.DateTime(precision: 0),
                        Nationality = c.String(unicode: false),
                        Fancy = c.String(unicode: false),
                        Taboo = c.String(unicode: false),
                        RecLevel = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        CreateUserID = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        ModifyUserID = c.String(unicode: false),
                        ModifyTime = c.DateTime(precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID);
            
            CreateTable(
                "Roles",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "UserRoleMaps",
                c => new
                    {
                        MapID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserID = c.String(unicode: false),
                        RoleID = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.MapID);
            
            CreateTable(
                "Users",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Password = c.String(unicode: false),
                        UserName = c.String(unicode: false),
                        UserToken = c.String(unicode: false),
                        LastTime = c.DateTime(precision: 0),
                        CreateUserID = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        ModifyUserID = c.String(unicode: false),
                        ModifyTime = c.DateTime(precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "VisitDetails",
                c => new
                    {
                        DetailID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        VisitID = c.String(unicode: false),
                        No = c.String(unicode: false),
                        FromDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        Adress = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.DetailID);
            
            CreateTable(
                "VisitFiles",
                c => new
                    {
                        FileID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        FileName = c.String(unicode: false),
                        FileUrl = c.String(unicode: false),
                        Type = c.String(unicode: false),
                        OutID = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.FileID);
            
            CreateTable(
                "VisitOrgs",
                c => new
                    {
                        VisitOrgID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        OrgID = c.String(unicode: false),
                        VisitID = c.String(unicode: false),
                        OwenType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisitOrgID);
            
            CreateTable(
                "VisitPersons",
                c => new
                    {
                        VisitPersonID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        PersonID = c.String(unicode: false),
                        VisitID = c.String(unicode: false),
                        OwenType = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VisitPersonID);
            
            CreateTable(
                "VisitRecords",
                c => new
                    {
                        VisitID = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        VisitType = c.String(unicode: false),
                        VisitName = c.String(unicode: false),
                        VisitFor = c.String(unicode: false),
                        MianDepartment = c.String(unicode: false),
                        MianPersonName = c.String(unicode: false),
                        FromDate = c.DateTime(nullable: false, precision: 0),
                        EndDate = c.DateTime(nullable: false, precision: 0),
                        VisitTag = c.String(unicode: false),
                        VisType = c.String(unicode: false),
                        FeeType = c.String(unicode: false),
                        PayType = c.String(unicode: false),
                        TakeLevel = c.String(unicode: false),
                        IsLine = c.String(unicode: false),
                        AnsLevel = c.String(unicode: false),
                        Remark = c.String(unicode: false),
                        CreateUserID = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                        ModifyUserID = c.String(unicode: false),
                        ModifyTime = c.DateTime(precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VisitID);
            
        }
        
        public override void Down()
        {
            DropTable("VisitRecords");
            DropTable("VisitPersons");
            DropTable("VisitOrgs");
            DropTable("VisitFiles");
            DropTable("VisitDetails");
            DropTable("Users");
            DropTable("UserRoleMaps");
            DropTable("Roles");
            DropTable("PersonInfoes");
            DropTable("Orgnazitions");
            DropTable("LoginLogs");
            DropTable("AuthRoleMaps");
            DropTable("AuthModels");
        }
    }
}
