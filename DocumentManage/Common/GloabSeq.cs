using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Common
{
    public static class GloabSeq
    {
        public static string GetWaterNo(int type)
        {
            switch (type)
            {
                case 1:
                    return "ZJ" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99).ToString().PadLeft(2, '0');
                case 2:
                    return "WJ" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99).ToString().PadLeft(2, '0');
                case 3:
                    return "ZR" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99).ToString().PadLeft(2, '0');
                case 4:
                    return "WR" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99).ToString().PadLeft(2, '0');
                case 5:
                    return "C" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99).ToString().PadLeft(2, '0');
                case 6:
                    return "L" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99).ToString().PadLeft(2, '0');
            }
            return DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 99).ToString().PadLeft(2, '0');
        }
    }
}