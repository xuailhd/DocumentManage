using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManage.EF
{
    /// <summary>
    /// 解决默认情况下，EntityFramework.Extended不支持MySQL的.Where().Update()，以及.Where().Delete()写法
    /// </summary>
    public class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
            EntityFramework.Locator.Current.Register<EntityFramework.Batch.IBatchRunner>(() => new EntityFramework.Batch.MySqlBatchRunner());
        }
    }
}
