using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;
using DocumentManage.Models;

namespace DocumentManage.EF
{
    //..CodeFirst移除：
    [DbConfigurationType(typeof(DbContextConfiguration))]
    public class DBEntities : DbContext
    {
        public DBEntities() : base("DBEntities")
        {
            //关闭了实体验证
            this.Configuration.ValidateOnSaveEnabled = false;
            //关闭延迟加载
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<DBEntities>(null);

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //..CodeFirst移除：
            modelBuilder.HasDefaultSchema("");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AuthModel> AuthModels { get; set; }

        public DbSet<AuthRoleMap> AuthRoleMaps { get; set; }

        public DbSet<LoginLog> LoginLogs { get; set; }

        public DbSet<Orgnazition> Orgnazitions { get; set; }

        public DbSet<PersonInfo> PersonInfos { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRoleMap> UserRoleMaps { get; set; }

        public DbSet<VisitFile> VisitFiles { get; set; }

        public DbSet<VisitRecord> VisitRecords { get; set; }

        public DbSet<VisitDetail> VisitDetails { get; set; }

        public DbSet<VisitOrg> VisitOrgs { get; set; }

        public DbSet<VisitPerson> VisitPersons { get; set; }
    }
}




