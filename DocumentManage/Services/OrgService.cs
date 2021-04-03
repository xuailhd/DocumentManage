using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentManage.EF;
using DocumentManage.Models;
using DocumentManage.Dtos;
using DocumentManage.Common;
using System.Text;
using DocumentManage.Dtos.Request;
using System.IO;

namespace DocumentManage.Services
{
    public class OrgService
    {
        public bool Edit(RequestOrgDTO request, string operUserID, out string reason)
        {
            reason = "";

            List<string> oldFiles = new List<string>();
            Dictionary<string, string> copyfiles = new Dictionary<string, string>();
            var needDelete = new List<VisitFile>();
            var needNew = new List<VisitFile>();

            using (var db = new DBEntities())
            {
                var model = db.Orgnazitions.Where(t => t.OrgID == request.OrgID).FirstOrDefault();

                if (model != null)
                {
                    if(!string.IsNullOrEmpty(model.CreateUserID) && model.CreateUserID!= operUserID)
                    {
                        if(!CommonService.HasOtherDataAuth(operUserID, db))
                        {
                            reason = "非本人创建的数据，不允许修改";
                            return false;
                        }
                    }

                    model.OrgID = request.OrgID;
                    model.OrgInfo = request.OrgInfo;
                    model.OrgHistory = request.OrgHistory;
                    model.OrgName = request.OrgName;
                    model.OrgNameEN = request.OrgNameEN;
                    model.OrgType = request.OrgType;
                    model.Province = request.Province;
                    model.Remark = request.Remark;
                    model.ShortNameCN = request.ShortNameCN;
                    model.ShortNameEN = request.ShortNameEN;
                    model.Tag = request.Tag;
                    model.Tel1 = request.Tel1;
                    model.Tel2 = request.Tel2;
                    model.Tax = request.Tax;
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

                    var bjFiles = db.VisitFiles.Where(t => t.OutID == model.OrgID && t.Type == "20").ToList();
                    CommonService.FindNewAndOld(bjFiles, request.BJFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.OrgID, "20", copyfiles, db);

                    var otherFiles = db.VisitFiles.Where(t => t.OutID == model.OrgID && t.Type == "16").ToList();
                    CommonService.FindNewAndOld(otherFiles, request.OtherFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.OrgID, "16", copyfiles, db);
                }
                else
                {
                    var exits = db.Orgnazitions.Where(t => t.OrgName == request.OrgName || t.OrgNameEN == request.OrgNameEN).FirstOrDefault();

                    if (exits != null)
                    {
                        reason = "机构名称重复";
                        return false;
                    }

                    CommonService.HandleFiles(null, request.BJFiles, oldFiles, request.OrgID, "20", copyfiles, db);
                    CommonService.HandleFiles(null, request.OtherFiles, oldFiles, request.OrgID, "16", copyfiles, db);

                    request.CreateUserID = operUserID;
                    db.Orgnazitions.Add(request);
                }

                var ret = db.SaveChanges() > 0;
                if (ret)
                {
                    foreach (var item in oldFiles)
                    {
                        try
                        {
                            File.Delete(item);
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    foreach (var item in copyfiles)
                    {
                        try
                        {
                            File.Move(item.Key, item.Value);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                return ret;
            }
        }

        public ResponseOrgnazitionDTO GetDetail(RequestOrgQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from org in db.Orgnazitions.Where(t => !t.IsDeleted && t.OrgID == request.OrgID)
                            join uac in db.Users on org.CreateUserID equals uac.ID
                            join uam in db.Users on org.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            select new ResponseOrgnazitionDTO()
                            {
                                Address = org.Address,
                                ContactPerson1 = org.ContactPerson1,
                                ContactPerson2 = org.ContactPerson2,
                                Continent = org.Continent,
                                Country = org.Country,
                                CreateTime = org.CreateTime,
                                CreateUserName = uac.UserName,
                                Email1 = org.Email1,
                                Email2 = org.Email2,
                                Tax = org.Tax,
                                FromType = org.FromType,
                                Level = org.Level,
                                ModifyTime = org.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                OrgBack = org.OrgBack,
                                OrgID = org.OrgID,
                                OrgInfo = org.OrgInfo,
                                OrgHistory = org.OrgHistory,
                                OrgName = org.OrgName,
                                OrgNameEN = org.OrgNameEN,
                                OrgType = org.OrgType,
                                Province = org.Province,
                                Remark = org.Remark,
                                ShortNameCN = org.ShortNameCN,
                                ShortNameEN = org.ShortNameEN,
                                Tag = org.Tag,
                                Tel1 = org.Tel1,
                                Tel2 = org.Tel2,
                                WorkAddress = org.WorkAddress,
                                WorkTime = org.WorkTime
                            };

                if(!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }
                 var ret = query.FirstOrDefault();

                if (ret != null)
                {
                    var fileq = from vifile in db.VisitFiles
                                select vifile;

                    ret.OtherFiles = fileq.Where(t => t.Type == "16" && t.OutID == ret.OrgID).ToList();
                    ret.BJFiles = fileq.Where(t => t.Type == "20" && t.OutID == ret.OrgID).ToList();
                }

                return ret;
            }
        }


        public PagedList<ResponseOrgnazitionDTO> GetList(RequestOrgQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from org in db.Orgnazitions
                            join uac in db.Users on org.CreateUserID equals uac.ID
                            join uam in db.Users on org.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            where !org.IsDeleted
                            select new ResponseOrgnazitionDTO()
                            {
                                Address = org.Address,
                                ContactPerson1 = org.ContactPerson1,
                                ContactPerson2 = org.ContactPerson2,
                                Continent = org.Continent,
                                Country = org.Country,
                                CreateTime = org.CreateTime,
                                CreateUserName = uac.UserName,
                                Email1 = org.Email1,
                                Email2 = org.Email2,
                                FromType = org.FromType,
                                Level = org.Level,
                                ModifyTime = org.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                OrgBack = org.OrgBack,
                                OrgID = org.OrgID,
                                OrgInfo = org.OrgInfo,
                                OrgHistory = org.OrgHistory,
                                OrgName = org.OrgName,
                                OrgNameEN = org.OrgNameEN,
                                OrgType = org.OrgType,
                                Province = org.Province,
                                Remark = org.Remark,
                                ShortNameCN = org.ShortNameCN,
                                ShortNameEN = org.ShortNameEN,
                                Tag = org.Tag,
                                Tel1 = org.Tel1,
                                Tel2 = org.Tel2,
                                WorkAddress = org.WorkAddress,
                                WorkTime = org.WorkTime
                            };

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

        public bool Delete(RequestOrgQDTO request, string operUserID, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var query = db.Orgnazitions.Where(t => t.OrgID == request.OrgID);

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                var model = query.FirstOrDefault();

                if (!string.IsNullOrEmpty(model.CreateUserID) && model.CreateUserID != operUserID)
                {
                    if (!CommonService.HasOtherDataAuth(operUserID, db))
                    {
                        reason = "非本人创建的数据，不允许修改";
                        return false;
                    }
                }

                if (model != null)
                {
                    model.IsDeleted = true;
                    return db.SaveChanges() > 0;
                }

                return false;
            }
        }

        public string Export(RequestOrgQDTO request)
        {
            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1'  >");
            sbHtml.Append("<tr>");

            var lstTitle = new List<string> { "来源","机构编码", "机构名称", "中文简称", "英文名称", "英文简称", "标签"
                , "机构级别" ,"机构地址","洲","国","省","机构性质" ,"机构背景","交往历史","机构简介","办公地址","办公时间","联系人1"
                ,"联系人2","联系电话1","联系电话2","邮箱1","邮箱2","备注","创建者","创建日期","最后修改者","最后修改日期"
                };
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");


            request.PageIndex = 1;
            request.PageSize = int.MaxValue;
            var data = GetList(request);

            foreach (var item in data)
            {
                sbHtml.Append("<tr>");
                sbHtml.AppendFormat("<td >{0}</td>", item.FromType);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgID);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgName);
                sbHtml.AppendFormat("<td >{0}</td>", item.ShortNameCN);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgNameEN);
                sbHtml.AppendFormat("<td >{0}</td>", item.ShortNameEN);
                sbHtml.AppendFormat("<td >{0}</td>", item.Tag);

                sbHtml.AppendFormat("<td >{0}</td>", item.Level);
                sbHtml.AppendFormat("<td >{0}</td>", item.Address);
                sbHtml.AppendFormat("<td >{0}</td>", item.Continent);
                sbHtml.AppendFormat("<td >{0}</td>", item.Country);
                sbHtml.AppendFormat("<td >{0}</td>", item.Province);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgType);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgBack);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgHistory);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgInfo);
                sbHtml.AppendFormat("<td >{0}</td>", item.WorkAddress);
                sbHtml.AppendFormat("<td >{0}</td>", item.WorkTime);
                sbHtml.AppendFormat("<td >{0}</td>", item.ContactPerson1);

                sbHtml.AppendFormat("<td >{0}</td>", item.ContactPerson2);
                sbHtml.AppendFormat("<td >{0}</td>", item.Tel1);
                sbHtml.AppendFormat("<td >{0}</td>", item.Tel2);
                sbHtml.AppendFormat("<td >{0}</td>", item.Email1);
                sbHtml.AppendFormat("<td >{0}</td>", item.Email2);
                sbHtml.AppendFormat("<td >{0}</td>", item.Remark);
                sbHtml.AppendFormat("<td >{0}</td>", item.CreateUserName);
                sbHtml.AppendFormat("<td >{0}</td>", item.CreateTime.ToString("yyyy-MM-dd HH:mm"));
                sbHtml.AppendFormat("<td >{0}</td>", item.ModifyUserName);
                sbHtml.AppendFormat("<td >{0}</td>", item.ModifyTime.HasValue? item.ModifyTime.Value.ToString("yyyy-MM-dd HH:mm"):"" );
                sbHtml.Append("</tr>");
            }

            return sbHtml.ToString();
        }
    }
}