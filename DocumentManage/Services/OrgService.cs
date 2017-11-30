using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentManage.EF;
using DocumentManage.Models;
using DocumentManage.Dtos;
using DocumentManage.Common;

namespace DocumentManage.Services
{
    public class OrgService
    {
        public bool Edit(Orgnazition request, string operUserID, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var model = db.Orgnazitions.Where(t => t.OrgID == request.OrgID).FirstOrDefault();

                if (model != null)
                {
                    model.OrgID = request.OrgID;
                    model.OrgInfo = request.OrgInfo;
                    model.OrgName = request.OrgName;
                    model.OrgNameEN = request.OrgNameEN;
                    model.OrgType = request.OrgType;
                    model.Province = request.Province;
                    model.Remark = request.Remark;
                    model.ShortNameCN = request.ShortNameCN;
                    model.ShortNameEN = request.ShortNameEN;
                    model.Tag = request.Tag;
                    model.Tel1 = request.Tel2;
                    model.WorkAddress = request.WorkAddress;
                    model.WorkTime = request.WorkTime;
                    model.Address = request.Address;
                    model.ContactPerson1 = request.ContactPerson1;
                    model.ContactPerson2 = request.ContactPerson2;
                    model.Continent = request.Continent;
                    model.Country = request.Country;
                    model.Email1 = request.Email1;
                    model.Email2 = request.Email2;
                    model.FromType = request.FromType;
                    model.Level = request.Level;
                    model.OrgBack = request.OrgBack;
                    model.ModifyUserID = operUserID;
                    model.ModifyTime = DateTime.Now;
                }
                else
                {
                    var exits = db.Orgnazitions.Where(t => t.OrgName == request.OrgName || t.OrgNameEN == request.OrgNameEN).FirstOrDefault();

                    if (exits != null)
                    {
                        reason = "机构名称重复";
                        return false;
                    }

                    request.CreateUserID = operUserID;
                    db.Orgnazitions.Add(request);
                }
                return db.SaveChanges() > 0;
            }
        }

        public Orgnazition GetDetail(RequestOrgQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = db.Orgnazitions.Where(t => !t.IsDeleted && t.OrgID == request.OrgID);

                if(!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                return query.FirstOrDefault();
            }
        }


        public PagedList<Orgnazition> GetList(RequestOrgQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from org in db.Orgnazitions
                            where !org.IsDeleted
                            select org;

                if (!string.IsNullOrEmpty(request.OrgID))
                {
                    query = query.Where(t => t.OrgID.Contains(request.OrgID));
                }

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                if (!string.IsNullOrEmpty(request.OrgName))
                {
                    query = query.Where(t => t.OrgName.Contains(request.OrgName) || t.OrgNameEN.Contains(request.OrgName)
                    || t.ShortNameCN.Contains(request.OrgName) || t.ShortNameEN.Contains(request.OrgName));
                }

                if (!string.IsNullOrEmpty(request.Tag))
                {
                    query = query.Where(t => t.Tag == request.Tag);
                }

                if (!string.IsNullOrEmpty(request.Level))
                {
                    query = query.Where(t => t.Level == request.Level);
                }

                if (!string.IsNullOrEmpty(request.Address))
                {
                    query = query.Where(t => t.Address.Contains(request.Address));
                }

                if (!string.IsNullOrEmpty(request.Continent))
                {
                    query = query.Where(t => t.Continent == request.Continent);
                }

                if (!string.IsNullOrEmpty(request.Country))
                {
                    query = query.Where(t => t.Country.Contains(request.Country));
                }

                query = query.OrderByDescending(t => t.CreateTime);

                return query.ToPagedList(request.PageIndex, request.PageSize);
            }
        }

        public bool Delete(RequestOrgQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = db.Orgnazitions.Where(t => t.OrgID == request.OrgID);

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                var model = query.FirstOrDefault();

                if (model != null)
                {
                    model.IsDeleted = true;
                    return db.SaveChanges() > 0;
                }

                return false;
            }
        }
    }
}