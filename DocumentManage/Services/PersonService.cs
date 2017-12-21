using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentManage.EF;
using DocumentManage.Models;
using DocumentManage.Dtos;
using DocumentManage.Common;
using System.IO;
using System.Configuration;
using DocumentManage.Dtos.Request;
using System.Text;

namespace DocumentManage.Services
{
    public class PersonService
    {
        public bool Edit(RequestPersonDTO request, string operUserID, out string reason)
        {
            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            reason = "";
            List<string> oldFiles = new List<string>();
            Dictionary<string, string> copyfiles = new Dictionary<string, string>();
            var needDelete = new List<VisitFile>();
            var needNew = new List<VisitFile>();

            using (var db = new DBEntities())
            {
                var model = db.PersonInfos.Where(t => t.PersonID == request.PersonID).FirstOrDefault();

                if (model != null)
                {
                    if (!string.IsNullOrEmpty(model.CreateUserID) && model.CreateUserID != operUserID)
                    {
                        if (!CommonService.HasOtherDataAuth(operUserID, db))
                        {
                            reason = "非本人创建的数据，不允许修改";
                            return false;
                        }
                    }
                    model.Birth = request.Birth;
                    model.ContactAddress = request.ContactAddress;
                    model.Department = request.Department;
                    model.Duty = request.Duty;
                    model.Email = request.Email;
                    model.Fancy = request.Fancy;
                    model.FromType = request.FromType;
                    model.IDNumber = request.IDNumber;
                    model.Mobile1 = request.Mobile1;
                    model.Mobile2 = request.Mobile2;
                    model.ModifyTime = DateTime.Now;
                    model.ModifyUserID = operUserID;
                    model.NameCN = request.NameCN;
                    model.NameEN = request.NameEN;
                    model.Nationality = request.Nationality;
                    model.OrgID = request.OrgID;
                    model.OrgName = request.OrgName;
                    model.PassportCode = request.PassportCode;
                    model.PassportDate = request.PassportDate;
                    model.PassportSignAdress = request.PassportSignAdress;
                    model.PassportSignDate = request.PassportSignDate;
                    model.PassportType = request.PassportType;
                    model.Sex = request.Sex;
                    model.Taboo = request.Taboo;
                    model.Tag = request.Tag;
                    model.Tel1 = request.Tel1;
                    model.Tel2 = request.Tel2;
                    model.Title = request.Title;

                    var passportFiles = db.VisitFiles.Where(t => t.OutID == model.PersonID && t.Type == "1").ToList();
                    CommonService.FindNewAndOld(passportFiles, request.PassportFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.PersonID, "1", copyfiles, db);

                    var photoFiles = db.VisitFiles.Where(t => t.OutID == model.PersonID && t.Type == "2").ToList();
                    CommonService.FindNewAndOld(photoFiles, request.PhotoFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.PersonID, "2", copyfiles, db);

                    var idNumberFiles = db.VisitFiles.Where(t => t.OutID == model.PersonID && t.Type == "3").ToList();
                    CommonService.FindNewAndOld(idNumberFiles, request.IDNumberFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.PersonID, "3", copyfiles, db);

                }
                else
                {
                    model = request.Map<RequestPersonDTO, PersonInfo>();
                    model.CreateUserID = operUserID;

                    CommonService.HandleFiles(null, request.PassportFiles, oldFiles, model.PersonID, "1", copyfiles, db);
                    CommonService.HandleFiles(null, request.IDNumberFiles, oldFiles, model.PersonID, "2", copyfiles, db);
                    CommonService.HandleFiles(null, request.PhotoFiles, oldFiles, model.PersonID, "3", copyfiles, db);

                    db.PersonInfos.Add(model);
                }

                var ret = db.SaveChanges() > 0;
                if (ret)
                {
                    foreach(var item in oldFiles)
                    {
                        try
                        {
                            File.Delete(item);
                        }
                        catch(Exception ex)
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

        public RequestPersonDTO GetDetail(RequestPersonQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from per in db.PersonInfos.Where(t => !t.IsDeleted && t.PersonID == request.PersonID)
                            join uac in db.Users on per.CreateUserID equals uac.ID
                            join uam in db.Users on per.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            select new RequestPersonDTO()
                            {
                                Birth = per.Birth,
                                ContactAddress = per.ContactAddress,
                                CreateTime = per.CreateTime,
                                CreateUserID = per.CreateUserID,
                                CreateUserName = uac.UserName,
                                Department = per.Department,
                                Duty = per.Duty,
                                Email = per.Email,
                                Fancy = per.Fancy,
                                FromType = per.FromType,
                                IDNumber = per.IDNumber,
                                Mobile1 = per.Mobile1,
                                Mobile2 = per.Mobile2,
                                ModifyTime = per.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                NameCN = per.NameCN,
                                NameEN = per.NameEN,
                                Nationality = per.Nationality,
                                OrgID = per.OrgID,
                                OrgName = per.OrgName,
                                PassportCode = per.PassportCode,
                                PassportDate = per.PassportDate,
                                PassportSignAdress = per.PassportSignAdress,
                                PassportSignDate = per.PassportSignDate,
                                PassportType = per.PassportType,
                                PersonID = per.PersonID,
                                RecLevel = per.RecLevel,
                                Remark = per.Remark,
                                Sex = per.Sex,
                                Taboo = per.Taboo,
                                Tag = per.Tag,
                                Tel1 = per.Tel1,
                                Tel2 = per.Tel2,
                                Title = per.Title
                            };

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                var ret = query.FirstOrDefault();

                if(ret == null)
                {
                    return null;
                }
                else
                {
                    var idNumberFiles = db.VisitFiles.Where(t => t.OutID == ret.PersonID && t.Type == "3").ToList();
                    var passportFiles = db.VisitFiles.Where(t => t.OutID == ret.PersonID && t.Type == "1").ToList();
                    var photoFiles = db.VisitFiles.Where(t => t.OutID == ret.PersonID && t.Type == "2").ToList();
                    ret.IDNumberFiles = idNumberFiles;
                    ret.PassportFiles = passportFiles;
                    ret.PhotoFiles = photoFiles;
                    return ret;
                }
            }
        }


        public PagedList<RequestPersonDTO> GetList(RequestPersonQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from per in db.PersonInfos.Where(t => !t.IsDeleted && t.PersonID == request.PersonID)
                            join uac in db.Users on per.CreateUserID equals uac.ID
                            join uam in db.Users on per.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            select new RequestPersonDTO()
                            {
                                Birth = per.Birth,
                                ContactAddress = per.ContactAddress,
                                CreateTime = per.CreateTime,
                                CreateUserID = per.CreateUserID,
                                CreateUserName = uac.UserName,
                                Department = per.Department,
                                Duty = per.Duty,
                                Email = per.Email,
                                Fancy = per.Fancy,
                                FromType = per.FromType,
                                IDNumber = per.IDNumber,
                                Mobile1 = per.Mobile1,
                                Mobile2 = per.Mobile2,
                                ModifyTime = per.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                NameCN = per.NameCN,
                                NameEN = per.NameEN,
                                Nationality = per.Nationality,
                                OrgID = per.OrgID,
                                OrgName = per.OrgName,
                                PassportCode = per.PassportCode,
                                PassportDate = per.PassportDate,
                                PassportSignAdress = per.PassportSignAdress,
                                PassportSignDate = per.PassportSignDate,
                                PassportType = per.PassportType,
                                PersonID = per.PersonID,
                                RecLevel = per.RecLevel,
                                Remark = per.Remark,
                                Sex = per.Sex,
                                Taboo = per.Taboo,
                                Tag = per.Tag,
                                Tel1 = per.Tel1,
                                Tel2 = per.Tel2,
                                Title = per.Title
                            };

                if (!string.IsNullOrEmpty(request.PersonID))
                {
                    query = query.Where(t => t.OrgID.Contains(request.PersonID));
                }

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                if (!string.IsNullOrEmpty(request.OrgName))
                {
                    query = query.Where(t => t.OrgName.Contains(request.OrgName));
                }

                if (!string.IsNullOrEmpty(request.Tag))
                {
                    query = query.Where(t => t.Tag == request.Tag);
                }

                if (!string.IsNullOrEmpty(request.ContactAddress))
                {
                    query = query.Where(t => t.ContactAddress.Contains(request.ContactAddress));
                }

                if (!string.IsNullOrEmpty(request.Email))
                {
                    query = query.Where(t => t.Email.Contains(request.Email));
                }

                if (!string.IsNullOrEmpty(request.Mobile))
                {
                    query = query.Where(t => t.Mobile1.Contains(request.Mobile) || t.Mobile2.Contains(request.Mobile));
                }

                if (!string.IsNullOrEmpty(request.Tel))
                {
                    query = query.Where(t => t.Tel1.Contains(request.Tel) || t.Tel2.Contains(request.Tel));
                }

                if (!string.IsNullOrEmpty(request.UserName))
                {
                    query = query.Where(t => t.NameCN.Contains(request.UserName) || t.NameEN.Contains(request.UserName));
                }

                query = query.OrderByDescending(t => t.CreateTime);

                return query.ToPagedList(request.PageIndex, request.PageSize);
            }
        }

        public bool Delete(RequestPersonQDTO request, string operUserID, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var query = db.PersonInfos.Where(t => t.PersonID == request.PersonID);

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

        public string Export(RequestPersonQDTO request)
        {
            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1'  >");
            sbHtml.Append("<tr>");

            var lstTitle = new List<string> { "来源","人员编号", "机构名称", "中文名", "英文名", "标签", "部门"
                , "护照号码" ,"护照有效期","签发日期","签发地","护照类别","头衔" ,"主要职务","身份证号","邮箱","电话1","电话2"
                ,"手机1","手机2","联系地址","出生年月","性别","国籍","喜好","忌讳","接待规格","其他说明","创建者","创建日期"
                ,"最后修改者","最后修改日期"
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
                sbHtml.AppendFormat("<td >{0}</td>", item.PersonID);
                sbHtml.AppendFormat("<td >{0}</td>", item.OrgName);
                sbHtml.AppendFormat("<td >{0}</td>", item.NameCN);
                sbHtml.AppendFormat("<td >{0}</td>", item.NameEN);
                sbHtml.AppendFormat("<td >{0}</td>", item.Tag);
                sbHtml.AppendFormat("<td >{0}</td>", item.Department);

                sbHtml.AppendFormat("<td >{0}</td>", item.PassportCode);
                sbHtml.AppendFormat("<td >{0}</td>", item.PassportDate);
                sbHtml.AppendFormat("<td >{0}</td>", item.PassportSignDate);
                sbHtml.AppendFormat("<td >{0}</td>", item.PassportSignAdress);
                sbHtml.AppendFormat("<td >{0}</td>", item.PassportType);
                sbHtml.AppendFormat("<td >{0}</td>", item.Title);
                sbHtml.AppendFormat("<td >{0}</td>", item.Duty);
                sbHtml.AppendFormat("<td >{0}</td>", item.IDNumber);
                sbHtml.AppendFormat("<td >{0}</td>", item.Email);
                sbHtml.AppendFormat("<td >{0}</td>", item.Tel1);
                sbHtml.AppendFormat("<td >{0}</td>", item.Tel2);

                sbHtml.AppendFormat("<td >{0}</td>", item.Mobile1);
                sbHtml.AppendFormat("<td >{0}</td>", item.Mobile2);
                sbHtml.AppendFormat("<td >{0}</td>", item.ContactAddress);
                sbHtml.AppendFormat("<td >{0}</td>", item.Birth.HasValue ? item.Birth.Value.ToString("yyyy-MM-dd HH:mm") : "");
                sbHtml.AppendFormat("<td >{0}</td>", item.Sex);
                sbHtml.AppendFormat("<td >{0}</td>", item.Nationality);
                sbHtml.AppendFormat("<td >{0}</td>", item.Fancy);
                sbHtml.AppendFormat("<td >{0}</td>", item.Taboo);
                sbHtml.AppendFormat("<td >{0}</td>", item.RecLevel);
                sbHtml.AppendFormat("<td >{0}</td>", item.Remark);
                sbHtml.AppendFormat("<td >{0}</td>", item.CreateUserName);
                sbHtml.AppendFormat("<td >{0}</td>", item.CreateTime.ToString("yyyy-MM-dd HH:mm"));

                sbHtml.AppendFormat("<td >{0}</td>", item.ModifyUserName);
                sbHtml.AppendFormat("<td >{0}</td>", item.ModifyTime.HasValue ? item.ModifyTime.Value.ToString("yyyy-MM-dd HH:mm") : "");
                sbHtml.Append("</tr>");
            }

            return sbHtml.ToString();
        }
    }
}