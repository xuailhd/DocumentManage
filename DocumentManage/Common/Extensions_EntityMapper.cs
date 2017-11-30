using EmitMapper;
using EmitMapper.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentManage.Common
{
    public static class Extensions_EntityMapper
    {
        public static TDestEntity Map<TSourceEntity, TDestEntity>(this TSourceEntity obj, params string[] members)
        {
            ObjectsMapper<TSourceEntity, TDestEntity> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TSourceEntity, TDestEntity>(
                new DefaultMapConfig().IgnoreMembers<TSourceEntity, TDestEntity>(members));
            TDestEntity dst = mapper.Map(obj);
            return dst;
        }

        public static TDestEntity MapFrom<TSourceEntity, TDestEntity>(this TDestEntity obj, TSourceEntity source, params string[] members)
        {
            ObjectsMapper<TSourceEntity, TDestEntity> mapper = ObjectMapperManager.DefaultInstance.GetMapper<TSourceEntity, TDestEntity>(
                new DefaultMapConfig().IgnoreMembers<TSourceEntity, TDestEntity>(members));
            TDestEntity dst = mapper.Map(source, obj);
            return dst;
        }
    }
}