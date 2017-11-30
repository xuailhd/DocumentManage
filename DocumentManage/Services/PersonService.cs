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
                    model.PersonID = Guid.NewGuid().ToString("N");
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
                var query = db.PersonInfos.Where(t => !t.IsDeleted && t.PersonID == request.PersonID);

                if(!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                var model = query.FirstOrDefault();

                if(model == null)
                {
                    return null;
                }
                else
                {
                    RequestPersonDTO ret = model.Map<PersonInfo, RequestPersonDTO>();
                    var idNumberFiles = db.VisitFiles.Where(t => t.OutID == model.PersonID && t.Type == "3").ToList();
                    var passportFiles = db.VisitFiles.Where(t => t.OutID == model.PersonID && t.Type == "1").ToList();
                    var photoFiles = db.VisitFiles.Where(t => t.OutID == model.PersonID && t.Type == "2").ToList();
                    ret.IDNumberFiles = idNumberFiles;
                    ret.PassportFiles = passportFiles;
                    ret.PhotoFiles = photoFiles;
                    return ret;
                }
            }
        }


        public PagedList<PersonInfo> GetList(RequestPersonQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from org in db.PersonInfos
                            where !org.IsDeleted
                            select org;

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

        public bool Delete(RequestPersonQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = db.PersonInfos.Where(t => t.PersonID == request.PersonID);

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