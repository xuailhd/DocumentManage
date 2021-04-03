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
using System.Text;

namespace DocumentManage.Services
{
    public class RecordService
    {
        public bool Edit(RequestVisitRecordDTO request, string operUserID, out string reason)
        {
            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            reason = "";
            List<string> oldFiles = new List<string>();
            Dictionary<string, string> copyfiles = new Dictionary<string, string>();
            var needDelete = new List<VisitFile>();
            var needNew = new List<VisitFile>();

            using (var db = new DBEntities())
            {
                var model = db.VisitRecords.Where(t => t.VisitID == request.VisitID).FirstOrDefault();

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

                    request.VisitID = model.VisitID;

                    var sjwlFiles = db.VisitFiles.Where(t => t.OutID == model.VisitID && t.Type == "11").ToList();
                    CommonService.FindNewAndOld(sjwlFiles, request.SJWLFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.VisitID, "11", copyfiles, db);

                    var lbwlFiles = db.VisitFiles.Where(t => t.OutID == model.VisitID && t.Type == "12").ToList();
                    CommonService.FindNewAndOld(lbwlFiles, request.LBWLFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.VisitID, "12", copyfiles, db);

                    var nbglFiles = db.VisitFiles.Where(t => t.OutID == model.VisitID && t.Type == "13").ToList();
                    CommonService.FindNewAndOld(nbglFiles, request.NBGLFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.VisitID, "13", copyfiles, db);

                    var hyxgFiles = db.VisitFiles.Where(t => t.OutID == model.VisitID && t.Type == "14").ToList();
                    CommonService.FindNewAndOld(hyxgFiles, request.HYXGFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.VisitID, "14", copyfiles, db);

                    var newsFiles = db.VisitFiles.Where(t => t.OutID == model.VisitID && t.Type == "15").ToList();
                    CommonService.FindNewAndOld(newsFiles, request.NewsFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.VisitID, "15", copyfiles, db);

                    var otherFiles = db.VisitFiles.Where(t => t.OutID == model.VisitID && t.Type == "16").ToList();
                    CommonService.FindNewAndOld(otherFiles, request.OtherFiles, needDelete, needNew);
                    CommonService.HandleFiles(needDelete, needNew, oldFiles, model.VisitID, "16", copyfiles, db);

                    #region 删除旧数据
                    var oldDetails = db.VisitDetails.Where(t => t.VisitID == model.VisitID).ToList();
                    foreach (var item in oldDetails)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var oldMianPersons = db.VisitPersons.Where(t => t.VisitID == model.VisitID && t.OwenType == EnumPersonOwenType.MainHanle).ToList();
                    foreach (var item in oldMianPersons)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var oldOurOrgs = db.VisitOrgs.Where(t => t.VisitID == model.VisitID && t.OwenType ==  EnumOrgOwenType.Our).ToList();
                    foreach (var item in oldOurOrgs)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var oldOurPersons = db.VisitPersons.Where(t => t.VisitID == model.VisitID && t.OwenType == EnumPersonOwenType.Our && t.Level == 0).ToList();
                    foreach (var item in oldOurPersons)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var oldOurOtherPersons = db.VisitPersons.Where(t => t.VisitID == model.VisitID && t.OwenType == EnumPersonOwenType.Our && t.Level == 1).ToList();
                    foreach (var item in oldOurOtherPersons)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var oldTheyOrgs = db.VisitOrgs.Where(t => t.VisitID == model.VisitID && t.OwenType == EnumOrgOwenType.They).ToList();
                    foreach (var item in oldTheyOrgs)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var oldTheyMainPersons = db.VisitPersons.Where(t => t.VisitID == model.VisitID && t.OwenType == EnumPersonOwenType.They && t.Level == 0).ToList();
                    foreach (var item in oldTheyMainPersons)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    //var oldTheyOtherPersons = db.VisitPersons.Where(t => t.VisitID == model.VisitID && t.OwenType == EnumPersonOwenType.They && t.Level == 1).ToList();
                    //foreach (var item in oldTheyOtherPersons)
                    //{
                    //    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    //}

                    var oldBeViOrgs = db.VisitOrgs.Where(t => t.VisitID == model.VisitID && t.OwenType == EnumOrgOwenType.BeVi).ToList();
                    foreach (var item in oldBeViOrgs)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var oldtags = db.VisitTags.Where(t => t.VisitID == model.VisitID).ToList();
                    foreach (var item in oldtags)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    #endregion

                    AddNewDetails(request, db);

                    model.VisitFor = request.VisitFor;
                    model.VisitName = request.VisitName;
                    model.VisitType = request.VisitType;
                    model.AnsLevel = request.AnsLevel;
                    model.VisType = request.VisType;
                    model.EndDate = request.EndDate;
                    model.FeeType = request.FeeType;
                    model.IsLine = request.IsLine;
                    model.MainDepartment = request.MainDepartment;
                    model.PayType = request.PayType;
                    model.Remark = request.Remark;
                    model.TakeLevel = request.TakeLevel;
                    model.FromDate = request.FromDate;
                    model.EndDate = request.EndDate;
                    model.ModifyTime = DateTime.Now;
                    model.ModifyUserID = operUserID;
                    model.TheyOtherPersonStr = request.TheyOtherPersonStr;
                    model.OurOtherPersonStr = request.OurOtherPersonStr;

                }
                else
                {
                    CommonService.HandleFiles(null, request.SJWLFiles, oldFiles, request.VisitID, "11", copyfiles, db);
                    CommonService.HandleFiles(null, request.LBWLFiles, oldFiles, request.VisitID, "12", copyfiles, db);
                    CommonService.HandleFiles(null, request.NBGLFiles, oldFiles, request.VisitID, "13", copyfiles, db);
                    CommonService.HandleFiles(null, request.HYXGFiles, oldFiles, request.VisitID, "14", copyfiles, db);
                    CommonService.HandleFiles(null, request.NewsFiles, oldFiles, request.VisitID, "15", copyfiles, db);
                    CommonService.HandleFiles(null, request.OtherFiles, oldFiles, request.VisitID, "16", copyfiles, db);

                    AddNewDetails(request, db);

                    model = request.Map<RequestVisitRecordDTO, VisitRecord>();
                    model.CreateUserID = operUserID;

                    db.VisitRecords.Add(model);
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

        public void AddNewDetails(RequestVisitRecordDTO request, DBEntities db)
        {
            #region 新增新数据
            if (request.VisitDetails != null)
            {
                request.VisitDetails = request.VisitDetails.Where(t => t.FromDate.HasValue && t.EndDate.HasValue).OrderBy(t => t.FromDate).ToList();

                if (request.VisitDetails != null && request.VisitDetails.Count > 0)
                {
                    for (var i = 0; i < request.VisitDetails.Count; i++)
                    {
                        var item = request.VisitDetails[i];
                        item.VisitID = request.VisitID;
                        item.DetailID = Guid.NewGuid().ToString("N");
                        item.No = (i+1).ToString();
                        db.VisitDetails.Add(item);

                        if (!request.FromDate.HasValue || request.FromDate.Value.CompareTo(item.FromDate) > 0)
                        {
                            request.FromDate = item.FromDate;
                        }

                        if (!request.EndDate.HasValue || request.EndDate.Value.CompareTo(item.EndDate) < 0)
                        {
                            request.EndDate = item.EndDate;
                        }
                    }
                }
                else
                {
                    request.EndDate = null;
                    request.FromDate = null;
                }
            }
            else
            {
                request.EndDate = null;
                request.FromDate = null;
            }

            if (request.MianPersons != null)
            {
                foreach (var item in request.MianPersons)
                {
                    item.OwenType = EnumPersonOwenType.MainHanle;
                    item.VisitID = request.VisitID;
                    item.VisitPersonID = Guid.NewGuid().ToString("N");
                    db.VisitPersons.Add(item);
                }
            }

            if (request.OurOrgs != null)
            {
                foreach (var item in request.OurOrgs)
                {
                    item.OwenType = EnumOrgOwenType.Our;
                    item.VisitID = request.VisitID;
                    item.VisitOrgID = Guid.NewGuid().ToString("N");
                    db.VisitOrgs.Add(item);
                }
            }

            if (request.OurPersons != null)
            {
                foreach (var item in request.OurPersons)
                {
                    item.OwenType = EnumPersonOwenType.Our;
                    item.VisitID = request.VisitID;
                    item.VisitPersonID = Guid.NewGuid().ToString("N");
                    item.Level = 0;
                    db.VisitPersons.Add(item);
                }
            }

            //if (request.OurOtherPersons != null)
            //{
            //    foreach (var item in request.OurOtherPersons)
            //    {
            //        item.OwenType = EnumPersonOwenType.Our;
            //        item.VisitID = request.VisitID;
            //        item.VisitPersonID = Guid.NewGuid().ToString("N");
            //        item.Level = 1;
            //        db.VisitPersons.Add(item);
            //    }
            //}

            if (request.TheyOrgs != null)
            {
                foreach (var item in request.TheyOrgs)
                {
                    item.OwenType = EnumOrgOwenType.They;
                    item.VisitID = request.VisitID;
                    item.VisitOrgID = Guid.NewGuid().ToString("N");
                    db.VisitOrgs.Add(item);
                }
            }

            if (request.TheyPersons != null)
            {
                foreach (var item in request.TheyPersons)
                {
                    item.OwenType = EnumPersonOwenType.They;
                    item.VisitID = request.VisitID;
                    item.VisitPersonID = Guid.NewGuid().ToString("N");
                    item.Level = 0;
                    db.VisitPersons.Add(item);
                }
            }

            //if (request.TheyOtherPersons != null)
            //{
            //    foreach (var item in request.TheyOtherPersons)
            //    {
            //        item.OwenType = EnumPersonOwenType.They;
            //        item.VisitID = request.VisitID;
            //        item.VisitPersonID = Guid.NewGuid().ToString("N");
            //        item.Level = 1;
            //        db.VisitPersons.Add(item);
            //    }
            //}

            if (request.BeViOrgs != null)
            {
                foreach (var item in request.BeViOrgs)
                {
                    item.OwenType = EnumOrgOwenType.BeVi;
                    item.VisitID = request.VisitID;
                    item.VisitOrgID = Guid.NewGuid().ToString("N");
                    db.VisitOrgs.Add(item);
                }
            }

            if (request.VisitTags != null)
            {
                foreach (var item in request.VisitTags)
                {
                    VisitTag visitTag = new VisitTag();
                    visitTag.Name = item;
                    visitTag.VisitID = request.VisitID;
                    visitTag.VisitTagID = Guid.NewGuid().ToString("N");
                    db.VisitTags.Add(visitTag);
                }
            }
            #endregion
        }

        public ResponseVisitRecordDTO GetDetail(RequestVisitRecordQDTO request)
        {
            ResponseVisitRecordDTO ret = null;

            using (var db = new DBEntities())
            {
                var query = from vir in db.VisitRecords.Where(t => !t.IsDeleted && t.VisitID == request.VisitID)
                            join uac in db.Users on vir.CreateUserID equals uac.ID
                            join uam in db.Users on vir.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            select new ResponseVisitRecordDTO()
                            {
                                AnsLevel = vir.AnsLevel,
                                CreateTime = vir.CreateTime,
                                CreateUserID = vir.CreateUserID,
                                CreateUserName = uac.UserName,
                                EndDate = vir.EndDate,
                                FeeType = vir.FeeType,
                                FromDate = vir.FromDate,
                                IsLine = vir.IsLine,
                                MainDepartment = vir.MainDepartment,
                                ModifyTime = vir.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                PayType = vir.PayType,
                                Remark = vir.Remark,
                                TakeLevel = vir.TakeLevel,
                                VisitFor = vir.VisitFor,
                                VisitID = vir.VisitID,
                                VisitName = vir.VisitName,
                                VisitType = vir.VisitType,
                                VisType = vir.VisType,
                                OurOtherPersonStr = vir.OurOtherPersonStr,
                                TheyOtherPersonStr = vir.TheyOtherPersonStr,
                            };

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }


                ret = query.FirstOrDefault();

                if (ret != null)
                {
                    ret.VisitDetails = db.VisitDetails.Where(t => t.VisitID == ret.VisitID).OrderBy(t=>t.No).ToList();

                    var personq = from mperson in db.VisitPersons
                                  join mpersoninfo in db.PersonInfos on mperson.PersonID equals mpersoninfo.PersonID
                                  select new ResponseVisitPerson()
                                  {
                                      Level = mperson.Level,
                                      NameCN = mpersoninfo.NameCN,
                                      NameEN = mpersoninfo.NameEN,
                                      OwenType = mperson.OwenType,
                                      PersonID = mperson.PersonID,
                                      VisitID = mperson.VisitID,
                                      VisitPersonID = mperson.VisitPersonID
                                  };
                    ret.MianPersons = personq.Where(t => t.OwenType == EnumPersonOwenType.MainHanle && t.VisitID == ret.VisitID).ToList();
                    ret.OurPersons = personq.Where(t => t.OwenType == EnumPersonOwenType.Our && t.Level == 0 && t.VisitID == ret.VisitID).ToList();
                    //ret.OurOtherPersons = personq.Where(t => t.OwenType == EnumPersonOwenType.Our && t.Level == 1 && t.VisitID == ret.VisitID).ToList();
                    ret.TheyPersons = personq.Where(t => t.OwenType == EnumPersonOwenType.They && t.Level == 0 && t.VisitID == ret.VisitID).ToList();
                    //ret.TheyOtherPersons = personq.Where(t => t.OwenType == EnumPersonOwenType.They && t.Level == 1 && t.VisitID == ret.VisitID).ToList();

                    var orgq = from beviorg in db.VisitOrgs
                               join beviorginfo in db.Orgnazitions on beviorg.OrgID equals beviorginfo.OrgID
                               select new ResponseVisitOrg()
                               {
                                   OrgID = beviorg.OrgID,
                                   OrgName = beviorginfo.OrgName,
                                   OrgNameEN = beviorginfo.OrgNameEN,
                                   OwenType = beviorg.OwenType,
                                   ShortNameCN = beviorginfo.ShortNameCN,
                                   ShortNameEN = beviorginfo.ShortNameEN,
                                   VisitID = beviorg.VisitID,
                                   VisitOrgID = beviorg.VisitOrgID
                               };

                    ret.OurOrgs = orgq.Where(t => t.OwenType == EnumOrgOwenType.Our && t.VisitID == ret.VisitID).ToList();
                    ret.TheyOrgs = orgq.Where(t => t.OwenType == EnumOrgOwenType.They && t.VisitID == ret.VisitID).ToList();
                    ret.BeViOrgs = orgq.Where(t => t.OwenType == EnumOrgOwenType.BeVi && t.VisitID == ret.VisitID).ToList();

                    var fileq = from vifile in db.VisitFiles
                                select vifile;

                    ret.SJWLFiles = fileq.Where(t => t.Type == "11" && t.OutID == ret.VisitID).ToList();
                    ret.LBWLFiles = fileq.Where(t => t.Type == "12" && t.OutID == ret.VisitID).ToList();
                    ret.NBGLFiles = fileq.Where(t => t.Type == "13" && t.OutID == ret.VisitID).ToList();
                    ret.HYXGFiles = fileq.Where(t => t.Type == "14" && t.OutID == ret.VisitID).ToList();
                    ret.NewsFiles = fileq.Where(t => t.Type == "15" && t.OutID == ret.VisitID).ToList();
                    ret.OtherFiles = fileq.Where(t => t.Type == "16" && t.OutID == ret.VisitID).ToList();

                    ret.VisitTags = db.VisitTags.Where(t=> t.VisitID == ret.VisitID).Select(t=>t.Name).ToList();
                }

                return ret;
            }
        }


        public PagedList<ResponseVisitListDTO> GetList(RequestVisitRecordQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from record in db.VisitRecords
                            join ua in db.Users on record.CreateUserID equals ua.ID
                            join uam in db.Users on record.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            join mperson in db.VisitPersons.Where(t=>t.OwenType == EnumPersonOwenType.MainHanle) on record.VisitID equals mperson.VisitID into mpersonleft
                            from mpersonempty in mpersonleft.DefaultIfEmpty()
                            join mpersoninfo in db.PersonInfos on mpersonempty.PersonID equals mpersoninfo.PersonID into mpersoninfoleft
                            from mpersoninfoempty in mpersoninfoleft.DefaultIfEmpty()
                            join ourperson in db.VisitPersons.Where(t=>t.OwenType == EnumPersonOwenType.Our && t.Level == 0) on record.VisitID equals ourperson.VisitID into ourpersonleft
                            from ourpersonempty in ourpersonleft.DefaultIfEmpty()
                            join ourpersoninfo in db.PersonInfos on ourpersonempty.PersonID equals ourpersoninfo.PersonID into ourpersoninfoleft
                            from ourpersoninfoempty in ourpersoninfoleft.DefaultIfEmpty()
                            join theymperson in db.VisitPersons.Where(t=>t.OwenType == EnumPersonOwenType.They && t.Level == 0) on record.VisitID equals theymperson.VisitID into theympersonleft
                            from theympersonempty in theympersonleft.DefaultIfEmpty()
                            join theympersoninfo in db.PersonInfos on theympersonempty.PersonID equals theympersoninfo.PersonID into theympersoninfoleft
                            from theympersoninfoempty in theympersoninfoleft.DefaultIfEmpty()
                            join beviorg in db.VisitOrgs.Where(t=>t.OwenType == EnumOrgOwenType.BeVi) on record.VisitID equals beviorg.VisitID into beviorgleft
                            from beviorgempty in beviorgleft.DefaultIfEmpty()
                            join beviorginfo in db.Orgnazitions on beviorgempty.OrgID equals beviorginfo.OrgID into beviorginfoleft
                            from beviorginfoempty in beviorginfoleft.DefaultIfEmpty()
                            join visittag in db.VisitTags on record.VisitID equals visittag.VisitID into visittagleft
                            from visittagempty in visittagleft.DefaultIfEmpty()
                            where !record.IsDeleted
                            select new 
                            {
                                VisitID = record.VisitID,
                                AnsLevel = record.AnsLevel,
                                VisType = record.VisType,
                                EndDate = record.EndDate,
                                FeeType = record.FeeType,
                                FromDate = record.FromDate,
                                IsLine = record.IsLine,
                                MainDepartment = record.MainDepartment,
                                PayType = record.PayType,
                                Remark = record.Remark,
                                TakeLevel = record.TakeLevel,
                                VisitFor = record.VisitFor,
                                VisitName = record.VisitName,
                                VisitType = record.VisitType,
                                CreateTime = record.CreateTime,
                                CreateUserID = record.CreateUserID,
                                CreateUserName = ua.UserName,
                                ModifyTime = record.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                VisitTag = visittagempty.Name,
                                MainPersonNameCN = mpersoninfoempty.NameCN,
                                MainPersonNameEN = mpersoninfoempty.NameEN,
                                TheyPersonNameCN = theympersoninfoempty.NameCN,
                                TheyPersonNameEN = theympersoninfoempty.NameEN,
                                OurPersonNameCN = ourpersoninfoempty.NameCN,
                                OurPersonNameEN = ourpersoninfoempty.NameEN,
                                BeviOrgName = beviorginfoempty.OrgName,
                                BeviOrgNameEN = beviorginfoempty.OrgNameEN,
                                BeviShortNameCN = beviorginfoempty.ShortNameCN,
                                BeviShortNameEN = beviorginfoempty.ShortNameEN,
                            };

                #region filter
                if (!string.IsNullOrEmpty(request.VisitID))
                {
                    query = query.Where(t => t.VisitID.Contains(request.VisitID));
                }

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                if (!string.IsNullOrEmpty(request.VisitFor))
                {
                    query = query.Where(t => t.VisitFor == request.VisitFor);
                }

                if (!string.IsNullOrEmpty(request.VisitName))
                {
                    query = query.Where(t => t.VisitName.Contains(request.VisitName));
                }

                if (!string.IsNullOrEmpty(request.VisitTag))
                {
                    query = query.Where(t => t.VisitTag == request.VisitTag);
                }

                if (!string.IsNullOrEmpty(request.VisitType))
                {
                    query = query.Where(t => t.VisitType == request.VisitType);
                }

                if (!string.IsNullOrEmpty(request.VisType))
                {
                    query = query.Where(t => t.VisType == request.VisType);
                }

                if (!string.IsNullOrEmpty(request.FeeType))
                {
                    query = query.Where(t => t.FeeType == request.FeeType);
                }

                if (!string.IsNullOrEmpty(request.BeViOrg))
                {
                    query = query.Where(t => t.BeviOrgName.Contains(request.BeViOrg) || t.BeviOrgNameEN.Contains(request.BeViOrg)
                        || t.BeviShortNameCN.Contains(request.BeViOrg) || t.BeviShortNameEN.Contains(request.BeViOrg));
                }

                if (request.EndDate.HasValue)
                {
                    var endDate = request.EndDate.Value;
                    query = query.Where(t => t.FromDate.Value.CompareTo(endDate) <= 0);
                }

                if (request.FromDate.HasValue)
                {
                    var fromDate = request.FromDate.Value;
                    query = query.Where(t => t.EndDate.Value.CompareTo(fromDate) >= 0);
                }

                if (!string.IsNullOrEmpty(request.MainDepartment))
                {
                    query = query.Where(t => t.MainDepartment.Contains(request.MainDepartment));
                }

                if (!string.IsNullOrEmpty(request.MianPerson))
                {
                    query = query.Where(t => t.MainPersonNameCN.Contains(request.MianPerson) || t.MainPersonNameEN.Contains(request.MianPerson));
                }

                if (!string.IsNullOrEmpty(request.OurPerson))
                {
                    query = query.Where(t => t.OurPersonNameCN.Contains(request.OurPerson) || t.OurPersonNameEN.Contains(request.OurPerson));
                }

                if (!string.IsNullOrEmpty(request.TheyPerson))
                {
                    query = query.Where(t => t.TheyPersonNameCN.Contains(request.TheyPerson) || t.TheyPersonNameEN.Contains(request.TheyPerson));
                }
                #endregion


                var retquery = from da in query
                               group da by new { da.VisitID, da.AnsLevel, da.VisType,da.EndDate,da.FeeType,da.FromDate,da.IsLine,
                               da.MainDepartment,da.PayType,da.Remark,da.TakeLevel,da.VisitFor,da.VisitName,da.VisitType,
                               da.CreateTime,da.CreateUserID,da.CreateUserName,da.ModifyTime,da.ModifyUserName} into gro
                               select new ResponseVisitListDTO()
                               {
                                   VisitID = gro.Key.VisitID,
                                   AnsLevel = gro.Key.AnsLevel,
                                   VisType = gro.Key.VisType,
                                   EndDate = gro.Key.EndDate,
                                   FeeType = gro.Key.FeeType,
                                   FromDate = gro.Key.FromDate,
                                   IsLine = gro.Key.IsLine,
                                   MainDepartment = gro.Key.MainDepartment,
                                   PayType = gro.Key.PayType,
                                   Remark = gro.Key.Remark,
                                   TakeLevel = gro.Key.TakeLevel,
                                   VisitFor = gro.Key.VisitFor,
                                   VisitName = gro.Key.VisitName,
                                   VisitType = gro.Key.VisitType,
                                   CreateTime = gro.Key.CreateTime,
                                   CreateUserID = gro.Key.CreateUserID,
                                   CreateUserName = gro.Key.CreateUserName,
                                   ModifyTime = gro.Key.ModifyTime,
                                   ModifyUserName = gro.Key.ModifyUserName
                                    
                               };

                retquery = retquery.OrderByDescending(t => t.CreateTime);

                var ret = retquery.ToPagedList(request.PageIndex, request.PageSize);

                if(ret!=null && ret.Count > 0)
                {
                    var visitIDs = ret.Select(t => t.VisitID).ToList();

                    var allpersons = (from rperson in db.VisitPersons
                                      join person in db.PersonInfos on rperson.PersonID equals person.PersonID
                                      where visitIDs.Contains(rperson.VisitID)
                                      select new
                                      {
                                          person.PersonID,
                                          person.NameCN,
                                          rperson.OwenType,
                                          rperson.VisitID,
                                          rperson.Level
                                      }).ToList();

                    ret.ForEach(t =>
                    {
                        t.MianPersonStr = string.Join(",", allpersons.Where(q => q.VisitID == t.VisitID && q.OwenType == EnumPersonOwenType.MainHanle)
                            .Select(q => q.NameCN).ToList());

                        t.TheyPersonStr = string.Join(",", allpersons.Where(q => q.VisitID == t.VisitID && q.OwenType == EnumPersonOwenType.They && q.Level == 0)
                            .Select(q => q.NameCN).ToList());
                    });

                }

                return ret;
            }
        }

        public ResponseVisitQueryListDTO GetQueryList(RequestVisitRecordQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from record in db.VisitRecords
                            join ua in db.Users on record.CreateUserID equals ua.ID
                            join uam in db.Users on record.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            join mperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.MainHanle) on record.VisitID equals mperson.VisitID into mpersonleft
                            from mpersonempty in mpersonleft.DefaultIfEmpty()
                            join mpersoninfo in db.PersonInfos on mpersonempty.PersonID equals mpersoninfo.PersonID into mpersoninfoleft
                            from mpersoninfoempty in mpersoninfoleft.DefaultIfEmpty()
                            join ourperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.Our && t.Level == 0) on record.VisitID equals ourperson.VisitID into ourpersonleft
                            from ourpersonempty in ourpersonleft.DefaultIfEmpty()
                            join ourpersoninfo in db.PersonInfos on ourpersonempty.PersonID equals ourpersoninfo.PersonID into ourpersoninfoleft
                            from ourpersoninfoempty in ourpersoninfoleft.DefaultIfEmpty()
                            //join ouroperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.Our && t.Level == 1) on record.VisitID equals ouroperson.VisitID into ouropersonleft
                            //from ouropersonempty in ouropersonleft.DefaultIfEmpty()
                            //join ouropersoninfo in db.PersonInfos on ouropersonempty.PersonID equals ouropersoninfo.PersonID into ouropersoninfoleft
                            //from ouropersoninfoempty in ouropersoninfoleft.DefaultIfEmpty()
                            join theymperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.They && t.Level == 0) on record.VisitID equals theymperson.VisitID into theympersonleft
                            from theympersonempty in theympersonleft.DefaultIfEmpty()
                            join theympersoninfo in db.PersonInfos on theympersonempty.PersonID equals theympersoninfo.PersonID into theympersoninfoleft
                            from theympersoninfoempty in theympersoninfoleft.DefaultIfEmpty()
                            //join theyoperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.They && t.Level == 1) on record.VisitID equals theyoperson.VisitID into theyopersonleft
                            //from theyopersonempty in theyopersonleft.DefaultIfEmpty()
                            //join theyopersoninfo in db.PersonInfos on theyopersonempty.PersonID equals theyopersoninfo.PersonID into theyopersoninfoleft
                            //from theyopersoninfoempty in theyopersoninfoleft.DefaultIfEmpty()
                            join ourorg in db.VisitOrgs.Where(t => t.OwenType == EnumOrgOwenType.Our) on record.VisitID equals ourorg.VisitID into ourorgleft
                            from ourorgempty in ourorgleft.DefaultIfEmpty()
                            join ourorginfo in db.Orgnazitions on ourorgempty.OrgID equals ourorginfo.OrgID into ourorginfoleft
                            from ourorginfoempty in ourorginfoleft.DefaultIfEmpty()
                            join theyorg in db.VisitOrgs.Where(t => t.OwenType == EnumOrgOwenType.They) on record.VisitID equals theyorg.VisitID into theyorgleft
                            from theyorgempty in theyorgleft.DefaultIfEmpty()
                            join theyorginfo in db.Orgnazitions on theyorgempty.OrgID equals theyorginfo.OrgID into theyorginfoleft
                            from theyorginfoempty in theyorginfoleft.DefaultIfEmpty()
                            join beviorg in db.VisitOrgs.Where(t => t.OwenType == EnumOrgOwenType.BeVi) on record.VisitID equals beviorg.VisitID into beviorgleft
                            from beviorgempty in beviorgleft.DefaultIfEmpty()
                            join beviorginfo in db.Orgnazitions on beviorgempty.OrgID equals beviorginfo.OrgID into beviorginfoleft
                            from beviorginfoempty in beviorginfoleft.DefaultIfEmpty()
                            join visittag in db.VisitTags on record.VisitID equals visittag.VisitID into visittagleft
                            from visittagempty in visittagleft.DefaultIfEmpty()
                            where !record.IsDeleted
                            select new
                            {
                                VisitID = record.VisitID,
                                AnsLevel = record.AnsLevel,
                                VisType = record.VisType,
                                EndDate = record.EndDate,
                                FeeType = record.FeeType,
                                FromDate = record.FromDate,
                                IsLine = record.IsLine,
                                MainDepartment = record.MainDepartment,
                                PayType = record.PayType,
                                Remark = record.Remark,
                                TakeLevel = record.TakeLevel,
                                VisitFor = record.VisitFor,
                                VisitName = record.VisitName,
                                VisitTag = visittagempty.Name,
                                VisitType = record.VisitType,
                                CreateTime = record.CreateTime,
                                CreateUserID = record.CreateUserID,
                                CreateUserName = ua.UserName,
                                ModifyTime = record.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                MainPersonNameCN = mpersoninfoempty.NameCN,
                                MainPersonNameEN = mpersoninfoempty.NameEN,
                                TheyPersonNameCN = theympersoninfoempty.NameCN,
                                TheyPersonNameEN = theympersoninfoempty.NameEN,
                                TheyPersonTitle = theympersoninfoempty.Title,
                                record.TheyOtherPersonStr,
                                //TheyOPersonNameCN = theyopersoninfoempty.NameCN,
                                //TheyOPersonNameEN = theyopersoninfoempty.NameEN,
                                //TheyOPersonTitle = theyopersoninfoempty.Title,
                                TheyOrgName = theyorginfoempty.OrgName,
                                TheyOrgNameEN = theyorginfoempty.OrgNameEN,
                                TheyShortNameCN = theyorginfoempty.ShortNameCN,
                                TheyShortNameEN = theyorginfoempty.ShortNameEN,
                                TheyOrgLevel = theyorginfoempty.Level,
                                TheyOrgContinent = theyorginfoempty.Continent,
                                TheyOrgCountry = theyorginfoempty.Country,
                                OurOrgName = ourorginfoempty.OrgName,
                                OurOrgNameEN = ourorginfoempty.OrgNameEN,
                                OurShortNameCN = ourorginfoempty.ShortNameCN,
                                OurShortNameEN = ourorginfoempty.ShortNameEN,
                                OurOrgLevel = ourorginfoempty.Level,
                                OurOrgCountry = ourorginfoempty.Country,
                                OurOrgProvince = ourorginfoempty.Province,
                                OurPersonNameCN = ourpersoninfoempty.NameCN,
                                OurPersonNameEN = ourpersoninfoempty.NameEN,
                                OurPersonTitle = ourpersoninfoempty.Title,
                                record.OurOtherPersonStr,
                                //OurOPersonNameCN = ouropersoninfoempty.NameCN,
                                //OurOPersonNameEN = ouropersoninfoempty.NameEN,
                                //OurOPersonTitle = ouropersoninfoempty.Title,
                                BeviOrgName = beviorginfoempty.OrgName,
                                BeviOrgNameEN = beviorginfoempty.OrgNameEN,
                                BeviShortNameCN = beviorginfoempty.ShortNameCN,
                                BeviShortNameEN = beviorginfoempty.ShortNameEN,
                            };

                #region filter
                if (!string.IsNullOrEmpty(request.VisitID))
                {
                    query = query.Where(t => t.VisitID.Contains(request.VisitID));
                }

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                if (!string.IsNullOrEmpty(request.VisitFor))
                {
                    query = query.Where(t => t.VisitFor == request.VisitFor);
                }

                if (!string.IsNullOrEmpty(request.VisitName))
                {
                    query = query.Where(t => t.VisitName.Contains(request.VisitName));
                }

                if (!string.IsNullOrEmpty(request.VisitTag))
                {
                    query = query.Where(t => t.VisitTag == request.VisitTag);
                }

                if (!string.IsNullOrEmpty(request.VisitType))
                {
                    query = query.Where(t => t.VisitType == request.VisitType);
                }

                if (!string.IsNullOrEmpty(request.VisType))
                {
                    query = query.Where(t => t.VisType == request.VisType);
                }

                if (!string.IsNullOrEmpty(request.FeeType))
                {
                    query = query.Where(t => t.FeeType == request.FeeType);
                }

                //if (!string.IsNullOrEmpty(request.BeViOrg))
                //{
                //    query = query.Where(t => t.BeviOrgName.Contains(request.BeViOrg) || t.BeviOrgNameEN.Contains(request.BeViOrg)
                //        || t.BeviShortNameCN.Contains(request.BeViOrg) || t.BeviShortNameEN.Contains(request.BeViOrg));
                //}

                if (request.EndDate.HasValue)
                {
                    var endDate = request.EndDate.Value;
                    query = query.Where(t => t.FromDate.Value.CompareTo(endDate) <= 0);
                }

                if (request.FromDate.HasValue)
                {
                    var fromDate = request.FromDate.Value;
                    query = query.Where(t => t.EndDate.Value.CompareTo(fromDate) >= 0);
                }

                if (!string.IsNullOrEmpty(request.MainDepartment))
                {
                    query = query.Where(t => t.MainDepartment.Contains(request.MainDepartment));
                }

                //if (!string.IsNullOrEmpty(request.MianPerson))
                //{
                //    query = query.Where(t => t.MainPersonNameCN.Contains(request.MianPerson) || t.MainPersonNameEN.Contains(request.MianPerson));
                //}

                if (!string.IsNullOrEmpty(request.OurPerson))
                {

                    query = query.Where(t => t.TheyPersonNameCN.Contains(request.OurPerson) || t.TheyPersonNameEN.Contains(request.OurPerson)
                        || t.TheyOtherPersonStr.Contains(request.OurPerson) || t.VisitName.Contains(request.OurPerson) || t.MainPersonNameCN.Contains(request.OurPerson)
                        || t.MainPersonNameEN.Contains(request.OurPerson) || t.OurPersonNameCN.Contains(request.OurPerson) || t.OurPersonNameEN.Contains(request.OurPerson)
                        || t.OurOtherPersonStr.Contains(request.OurPerson));
                }

                if (!string.IsNullOrEmpty(request.OurPersonTitle))
                {
                    //query = query.Where(t => t.OurPersonTitle.Contains(request.OurPersonTitle) || t.OurOPersonTitle.Contains(request.OurPersonTitle));
                    query = query.Where(t => t.OurPersonTitle.Contains(request.OurPersonTitle));
                }

                if (!string.IsNullOrEmpty(request.TheyPerson))
                {
                    query = query.Where(t => t.TheyPersonNameCN.Contains(request.TheyPerson) || t.TheyPersonNameEN.Contains(request.TheyPerson)
                        || t.TheyOtherPersonStr.Contains(request.TheyPerson) || t.VisitName.Contains(request.TheyPerson) || t.MainPersonNameCN.Contains(request.TheyPerson)
                        || t.MainPersonNameEN.Contains(request.TheyPerson) || t.OurPersonNameCN.Contains(request.TheyPerson) || t.OurPersonNameEN.Contains(request.TheyPerson)
                        || t.OurOtherPersonStr.Contains(request.TheyPerson));
                }

                if (!string.IsNullOrEmpty(request.TheyPersonTitle))
                {
                    //query = query.Where(t => t.TheyPersonTitle.Contains(request.TheyPersonTitle) || t.TheyOPersonTitle.Contains(request.TheyPersonTitle));
                    query = query.Where(t => t.TheyPersonTitle.Contains(request.TheyPersonTitle));
                }

                if (!string.IsNullOrEmpty(request.OurOrg))
                {
                    query = query.Where(t => t.OurOrgName.Contains(request.OurOrg) || t.OurOrgNameEN.Contains(request.OurOrg)
                        ||t.OurShortNameCN.Contains(request.OurOrg) || t.OurShortNameEN.Contains(request.OurOrg));
                }

                if (!string.IsNullOrEmpty(request.OurOrgLevel))
                {
                    query = query.Where(t => t.OurOrgLevel == request.OurOrgLevel);
                }

                if (!string.IsNullOrEmpty(request.OurOrgCountry))
                {
                    query = query.Where(t => t.OurOrgCountry.Contains(request.OurOrgCountry));
                }

                if (!string.IsNullOrEmpty(request.OurOrgProvince))
                {
                    query = query.Where(t => t.OurOrgProvince.Contains(request.OurOrgProvince));
                }

                if (!string.IsNullOrEmpty(request.TheyOrg))
                {
                    query = query.Where(t => t.TheyOrgName.Contains(request.TheyOrg) || t.TheyOrgNameEN.Contains(request.TheyOrg)
                        || t.TheyShortNameCN.Contains(request.TheyOrg) || t.TheyShortNameEN.Contains(request.TheyOrg));
                }

                if (!string.IsNullOrEmpty(request.TheyOrgLevel))
                {
                    query = query.Where(t => t.TheyOrgLevel == request.TheyOrgLevel);
                }

                if (!string.IsNullOrEmpty(request.TheyOrgContinent))
                {
                    query = query.Where(t => t.TheyOrgContinent == request.TheyOrgContinent);
                }

                if (!string.IsNullOrEmpty(request.TheyOrgCountry))
                {
                    query = query.Where(t => t.TheyOrgCountry.Contains(request.TheyOrgCountry));
                }

                #endregion

                var retquery = from da in query
                               group da by new
                               {
                                   da.VisitID,
                                   da.AnsLevel,
                                   da.VisType,
                                   da.EndDate,
                                   da.FeeType,
                                   da.FromDate,
                                   da.IsLine,
                                   da.MainDepartment,
                                   da.PayType,
                                   da.Remark,
                                   da.TakeLevel,
                                   da.VisitFor,
                                   da.VisitName,
                                   da.VisitType,
                                   da.CreateTime,
                                   da.CreateUserID,
                                   da.CreateUserName,
                                   da.ModifyTime,
                                   da.ModifyUserName
                               } into gro
                               select new ResponseVisitListDTO()
                               {
                                   VisitID = gro.Key.VisitID,
                                   AnsLevel = gro.Key.AnsLevel,
                                   VisType = gro.Key.VisType,
                                   EndDate = gro.Key.EndDate,
                                   FeeType = gro.Key.FeeType,
                                   FromDate = gro.Key.FromDate,
                                   IsLine = gro.Key.IsLine,
                                   MainDepartment = gro.Key.MainDepartment,
                                   PayType = gro.Key.PayType,
                                   Remark = gro.Key.Remark,
                                   TakeLevel = gro.Key.TakeLevel,
                                   VisitFor = gro.Key.VisitFor,
                                   VisitName = gro.Key.VisitName,
                                   VisitType = gro.Key.VisitType,
                                   CreateTime = gro.Key.CreateTime,
                                   CreateUserID = gro.Key.CreateUserID,
                                   CreateUserName = gro.Key.CreateUserName,
                                   ModifyTime = gro.Key.ModifyTime,
                                   ModifyUserName = gro.Key.ModifyUserName
                               };

                retquery = retquery.OrderByDescending(t => t.CreateTime);

                ResponseVisitQueryListDTO ret = new ResponseVisitQueryListDTO();

                var q2 = (from q in query
                         group q by new { q.VisitID, q.VisitTag } into gro
                         select new 
                         {
                             VisitTag = gro.Key.VisitTag,
                             VisitID = gro.Key.VisitID
                         }).ToList();

                ret.QGLHCount = q2.Where(t => t.VisitTag == "全国两会").Count();
                ret.GZCount = q2.Where(t => t.VisitTag == "国宗").GroupBy(t => t.VisitID).Count();
                ret.TZBCount = q2.Where(t => t.VisitTag == "统战部").GroupBy(t => t.VisitID).Count();
                ret.ZFJGCount = q2.Where(t => t.VisitTag == "政府机构").GroupBy(t => t.VisitID).Count();
                ret.FWYXCount = q2.Where(t => t.VisitTag == "访问院校").GroupBy(t => t.VisitID).Count();
                ret.QTCount = q2.Where(t => t.VisitTag == "其他").GroupBy(t => t.VisitID).Count();
                ret.CountryCount = query.Where(t => !string.IsNullOrEmpty(t.TheyOrgCountry)).GroupBy(t => t.TheyOrgCountry).Count();
                ret.VisitRecords = retquery.ToPagedList(request.PageIndex, request.PageSize);
                return ret;
            }
        }

        public bool Delete(RequestVisitRecordQDTO request, string operUserID,out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var query = db.VisitRecords.Where(t => t.VisitID == request.VisitID);

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
                else
                {
                    reason = "数据不存在,或者没有权限修改";
                }

                return false;
            }
        }

        public string ExportQuery(RequestVisitRecordQDTO request)
        {
            var sbHtml = new StringBuilder();
            sbHtml.Append("<table border='1'  >");
            sbHtml.Append("<tr>");

            var lstTitle = new List<string> { "来源","档案编号", "档案名称", "访问目的", "主要经办部门", "主要经办人", "接待机构"
                , "主要接待人员" ,"其他接待人员","来访机构","领队","其他人员","邀请机构" ,"访问性质","费用承担","支付方式","接待标准"
                ,"是否成行","外方评分","访问标注","备注","总行程","创建者","创建日期","最后修改者","最后修改日期"};
            foreach (var item in lstTitle)
            {
                sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: #DCE0E2; font-weight:bold;' height='25'>{0}</td>", item);
            }
            sbHtml.Append("</tr>");


            using (var db = new DBEntities())
            {
                #region 查询
                var query = from record in db.VisitRecords
                            join ua in db.Users on record.CreateUserID equals ua.ID
                            join uam in db.Users on record.ModifyUserID equals uam.ID into uamleft
                            from uamEmpty in uamleft.DefaultIfEmpty()
                            join mperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.MainHanle) on record.VisitID equals mperson.VisitID into mpersonleft
                            from mpersonempty in mpersonleft.DefaultIfEmpty()
                            join mpersoninfo in db.PersonInfos on mpersonempty.PersonID equals mpersoninfo.PersonID into mpersoninfoleft
                            from mpersoninfoempty in mpersoninfoleft.DefaultIfEmpty()
                            join ourperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.Our && t.Level == 0) on record.VisitID equals ourperson.VisitID into ourpersonleft
                            from ourpersonempty in ourpersonleft.DefaultIfEmpty()
                            join ourpersoninfo in db.PersonInfos on ourpersonempty.PersonID equals ourpersoninfo.PersonID into ourpersoninfoleft
                            from ourpersoninfoempty in ourpersoninfoleft.DefaultIfEmpty()
                            //join ouroperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.Our && t.Level == 1) on record.VisitID equals ouroperson.VisitID into ouropersonleft
                            //from ouropersonempty in ouropersonleft.DefaultIfEmpty()
                            //join ouropersoninfo in db.PersonInfos on ouropersonempty.PersonID equals ouropersoninfo.PersonID into ouropersoninfoleft
                            //from ouropersoninfoempty in ouropersoninfoleft.DefaultIfEmpty()
                            join theymperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.They && t.Level == 0) on record.VisitID equals theymperson.VisitID into theympersonleft
                            from theympersonempty in theympersonleft.DefaultIfEmpty()
                            join theympersoninfo in db.PersonInfos on theympersonempty.PersonID equals theympersoninfo.PersonID into theympersoninfoleft
                            from theympersoninfoempty in theympersoninfoleft.DefaultIfEmpty()
                            //join theyoperson in db.VisitPersons.Where(t => t.OwenType == EnumPersonOwenType.They && t.Level == 1) on record.VisitID equals theyoperson.VisitID into theyopersonleft
                            //from theyopersonempty in theyopersonleft.DefaultIfEmpty()
                            //join theyopersoninfo in db.PersonInfos on theyopersonempty.PersonID equals theyopersoninfo.PersonID into theyopersoninfoleft
                            //from theyopersoninfoempty in theyopersoninfoleft.DefaultIfEmpty()
                            join ourorg in db.VisitOrgs.Where(t => t.OwenType == EnumOrgOwenType.Our) on record.VisitID equals ourorg.VisitID into ourorgleft
                            from ourorgempty in ourorgleft.DefaultIfEmpty()
                            join ourorginfo in db.Orgnazitions on ourorgempty.OrgID equals ourorginfo.OrgID into ourorginfoleft
                            from ourorginfoempty in ourorginfoleft.DefaultIfEmpty()
                            join theyorg in db.VisitOrgs.Where(t => t.OwenType == EnumOrgOwenType.They) on record.VisitID equals theyorg.VisitID into theyorgleft
                            from theyorgempty in theyorgleft.DefaultIfEmpty()
                            join theyorginfo in db.Orgnazitions on theyorgempty.OrgID equals theyorginfo.OrgID into theyorginfoleft
                            from theyorginfoempty in theyorginfoleft.DefaultIfEmpty()
                            join beviorg in db.VisitOrgs.Where(t => t.OwenType == EnumOrgOwenType.BeVi) on record.VisitID equals beviorg.VisitID into beviorgleft
                            from beviorgempty in beviorgleft.DefaultIfEmpty()
                            join beviorginfo in db.Orgnazitions on beviorgempty.OrgID equals beviorginfo.OrgID into beviorginfoleft
                            from beviorginfoempty in beviorginfoleft.DefaultIfEmpty()
                            join visittag in db.VisitTags on record.VisitID equals visittag.VisitID into visittagleft
                            from visittagempty in visittagleft.DefaultIfEmpty()
                            where !record.IsDeleted
                            select new
                            {
                                VisitID = record.VisitID,
                                AnsLevel = record.AnsLevel,
                                VisType = record.VisType,
                                EndDate = record.EndDate,
                                FeeType = record.FeeType,
                                FromDate = record.FromDate,
                                IsLine = record.IsLine,
                                MainDepartment = record.MainDepartment,
                                PayType = record.PayType,
                                Remark = record.Remark,
                                TakeLevel = record.TakeLevel,
                                VisitFor = record.VisitFor,
                                VisitName = record.VisitName,
                                VisitTag = visittagempty.Name,
                                VisitType = record.VisitType,
                                CreateTime = record.CreateTime,
                                CreateUserID = record.CreateUserID,
                                CreateUserName = ua.UserName,
                                ModifyTime = record.ModifyTime,
                                ModifyUserName = uamEmpty.UserName,
                                MainPersonNameCN = mpersoninfoempty.NameCN,
                                MainPersonNameEN = mpersoninfoempty.NameEN,
                                TheyPersonNameCN = theympersoninfoempty.NameCN,
                                TheyPersonNameEN = theympersoninfoempty.NameEN,
                                TheyPersonTitle = theympersoninfoempty.Title,
                                record.TheyOtherPersonStr,
                                //TheyOPersonNameCN = theyopersoninfoempty.NameCN,
                                //TheyOPersonNameEN = theyopersoninfoempty.NameEN,
                                //TheyOPersonTitle = theyopersoninfoempty.Title,
                                TheyOrgName = theyorginfoempty.OrgName,
                                TheyOrgNameEN = theyorginfoempty.OrgNameEN,
                                TheyShortNameCN = theyorginfoempty.ShortNameCN,
                                TheyShortNameEN = theyorginfoempty.ShortNameEN,
                                TheyOrgLevel = theyorginfoempty.Level,
                                TheyOrgContinent = theyorginfoempty.Continent,
                                TheyOrgCountry = theyorginfoempty.Country,
                                OurOrgName = ourorginfoempty.OrgName,
                                OurOrgNameEN = ourorginfoempty.OrgNameEN,
                                OurShortNameCN = ourorginfoempty.ShortNameCN,
                                OurShortNameEN = ourorginfoempty.ShortNameEN,
                                OurOrgLevel = ourorginfoempty.Level,
                                OurOrgCountry = ourorginfoempty.Country,
                                OurOrgProvince = ourorginfoempty.Province,
                                OurPersonNameCN = ourpersoninfoempty.NameCN,
                                OurPersonNameEN = ourpersoninfoempty.NameEN,
                                OurPersonTitle = ourpersoninfoempty.Title,
                                record.OurOtherPersonStr,
                                //OurOPersonNameCN = ouropersoninfoempty.NameCN,
                                //OurOPersonNameEN = ouropersoninfoempty.NameEN,
                                //OurOPersonTitle = ouropersoninfoempty.Title,
                                BeviOrgName = beviorginfoempty.OrgName,
                                BeviOrgNameEN = beviorginfoempty.OrgNameEN,
                                BeviShortNameCN = beviorginfoempty.ShortNameCN,
                                BeviShortNameEN = beviorginfoempty.ShortNameEN,
                            };

                #region filter
                if (!string.IsNullOrEmpty(request.VisitID))
                {
                    query = query.Where(t => t.VisitID.Contains(request.VisitID));
                }

                if (!string.IsNullOrEmpty(request.UserID))
                {
                    query = query.Where(t => t.CreateUserID == request.UserID);
                }

                if (!string.IsNullOrEmpty(request.VisitFor))
                {
                    query = query.Where(t => t.VisitFor == request.VisitFor);
                }

                if (!string.IsNullOrEmpty(request.VisitName))
                {
                    query = query.Where(t => t.VisitName.Contains(request.VisitName));
                }

                if (!string.IsNullOrEmpty(request.VisitTag))
                {
                    query = query.Where(t => t.VisitTag == request.VisitTag);
                }

                if (!string.IsNullOrEmpty(request.VisitType))
                {
                    query = query.Where(t => t.VisitType == request.VisitType);
                }

                if (!string.IsNullOrEmpty(request.VisType))
                {
                    query = query.Where(t => t.VisType == request.VisType);
                }

                if (!string.IsNullOrEmpty(request.FeeType))
                {
                    query = query.Where(t => t.FeeType == request.FeeType);
                }

                //if (!string.IsNullOrEmpty(request.BeViOrg))
                //{
                //    query = query.Where(t => t.BeviOrgName.Contains(request.BeViOrg) || t.BeviOrgNameEN.Contains(request.BeViOrg)
                //        || t.BeviShortNameCN.Contains(request.BeViOrg) || t.BeviShortNameEN.Contains(request.BeViOrg));
                //}

                //if (request.EndDate.HasValue)
                //{
                //    var endDate = request.EndDate.Value;
                //    query = query.Where(t => t.FromDate.Value.CompareTo(endDate) <= 0);
                //}

                //if (request.FromDate.HasValue)
                //{
                //    var fromDate = request.FromDate.Value;
                //    query = query.Where(t => t.EndDate.Value.CompareTo(fromDate) >= 0);
                //}

                if (!string.IsNullOrEmpty(request.MainDepartment))
                {
                    query = query.Where(t => t.MainDepartment.Contains(request.MainDepartment));
                }

                //if (!string.IsNullOrEmpty(request.MianPerson))
                //{
                //    query = query.Where(t => t.MainPersonNameCN.Contains(request.MianPerson) || t.MainPersonNameEN.Contains(request.MianPerson));
                //}

                if (!string.IsNullOrEmpty(request.OurPerson))
                {
                    //query = query.Where(t => t.OurPersonNameCN.Contains(request.OurPerson) || t.OurPersonNameEN.Contains(request.OurPerson)
                    //    || t.OurOPersonNameCN.Contains(request.OurPerson) || t.OurOPersonNameEN.Contains(request.OurPerson));
                    query = query.Where(t => t.OurPersonNameCN.Contains(request.OurPerson) || t.OurPersonNameEN.Contains(request.OurPerson)
                        || t.OurOtherPersonStr.Contains(request.OurPerson));
                }

                if (!string.IsNullOrEmpty(request.OurPersonTitle))
                {
                    //query = query.Where(t => t.OurPersonTitle.Contains(request.OurPersonTitle) || t.OurOPersonTitle.Contains(request.OurPersonTitle));
                    query = query.Where(t => t.OurPersonTitle.Contains(request.OurPersonTitle));
                }

                if (!string.IsNullOrEmpty(request.TheyPerson))
                {
                    //query = query.Where(t => t.TheyPersonNameCN.Contains(request.TheyPerson) || t.TheyPersonNameEN.Contains(request.TheyPerson)
                    //    || t.TheyOPersonNameCN.Contains(request.OurPerson) || t.TheyOPersonNameEN.Contains(request.OurPerson));
                    query = query.Where(t => t.TheyPersonNameCN.Contains(request.TheyPerson) || t.TheyPersonNameEN.Contains(request.TheyPerson)
                        || t.TheyOtherPersonStr.Contains(request.OurPerson));
                }

                if (!string.IsNullOrEmpty(request.TheyPersonTitle))
                {
                    //query = query.Where(t => t.TheyPersonTitle.Contains(request.TheyPersonTitle) || t.TheyOPersonTitle.Contains(request.TheyPersonTitle));
                    query = query.Where(t => t.TheyPersonTitle.Contains(request.TheyPersonTitle));
                }

                if (!string.IsNullOrEmpty(request.OurOrg))
                {
                    query = query.Where(t => t.OurOrgName.Contains(request.OurOrg) || t.OurOrgNameEN.Contains(request.OurOrg)
                        || t.OurShortNameCN.Contains(request.OurOrg) || t.OurShortNameEN.Contains(request.OurOrg));
                }

                if (!string.IsNullOrEmpty(request.OurOrgLevel))
                {
                    query = query.Where(t => t.OurOrgLevel == request.OurOrgLevel);
                }

                if (!string.IsNullOrEmpty(request.OurOrgCountry))
                {
                    query = query.Where(t => t.OurOrgCountry.Contains(request.OurOrgCountry));
                }

                if (!string.IsNullOrEmpty(request.OurOrgProvince))
                {
                    query = query.Where(t => t.OurOrgProvince.Contains(request.OurOrgProvince));
                }

                if (!string.IsNullOrEmpty(request.TheyOrg))
                {
                    query = query.Where(t => t.TheyOrgName.Contains(request.TheyOrg) || t.TheyOrgNameEN.Contains(request.TheyOrg)
                        || t.TheyShortNameCN.Contains(request.TheyOrg) || t.TheyShortNameEN.Contains(request.TheyOrg));
                }

                if (!string.IsNullOrEmpty(request.TheyOrgLevel))
                {
                    query = query.Where(t => t.TheyOrgLevel == request.TheyOrgLevel);
                }

                if (!string.IsNullOrEmpty(request.TheyOrgContinent))
                {
                    query = query.Where(t => t.TheyOrgContinent == request.TheyOrgContinent);
                }

                if (!string.IsNullOrEmpty(request.TheyOrgCountry))
                {
                    query = query.Where(t => t.TheyOrgCountry.Contains(request.TheyOrgCountry));
                }

                #endregion

                var retquery = from da in query
                               group da by new
                               {
                                   da.VisitID,
                                   da.AnsLevel,
                                   da.VisType,
                                   da.EndDate,
                                   da.FeeType,
                                   da.FromDate,
                                   da.IsLine,
                                   da.MainDepartment,
                                   da.PayType,
                                   da.Remark,
                                   da.TakeLevel,
                                   da.VisitFor,
                                   da.VisitName,
                                   da.VisitType,
                                   da.CreateTime,
                                   da.CreateUserID,
                                   da.CreateUserName,
                                   da.ModifyTime,
                                   da.ModifyUserName,
                                   da.TheyOtherPersonStr,
                                   da.OurOtherPersonStr,
                               } into gro
                               select new ResponseVisitListDTO()
                               {
                                   VisitID = gro.Key.VisitID,
                                   AnsLevel = gro.Key.AnsLevel,
                                   VisType = gro.Key.VisType,
                                   EndDate = gro.Key.EndDate,
                                   FeeType = gro.Key.FeeType,
                                   FromDate = gro.Key.FromDate,
                                   IsLine = gro.Key.IsLine,
                                   MainDepartment = gro.Key.MainDepartment,
                                   PayType = gro.Key.PayType,
                                   Remark = gro.Key.Remark,
                                   TakeLevel = gro.Key.TakeLevel,
                                   VisitFor = gro.Key.VisitFor,
                                   VisitName = gro.Key.VisitName,
                                   VisitType = gro.Key.VisitType,
                                   CreateTime = gro.Key.CreateTime,
                                   CreateUserID = gro.Key.CreateUserID,
                                   CreateUserName = gro.Key.CreateUserName,
                                   ModifyTime = gro.Key.ModifyTime,
                                   ModifyUserName = gro.Key.ModifyUserName,
                                   TheyOtherPersonStr = gro.Key.TheyOtherPersonStr,
                                   OurOtherPersonStr = gro.Key.OurOtherPersonStr,
                               };

                retquery = retquery.OrderByDescending(t => t.CreateTime);

                var data = retquery.ToList();

                var visitIDs = data.Select(t => t.VisitID).ToList();

                var allorgs = (from rorg in db.VisitOrgs
                              join org in db.Orgnazitions on rorg.OrgID equals org.OrgID
                              where visitIDs.Contains(rorg.VisitID)
                              select new {
                                  org.OrgID,
                                  org.OrgName,
                                  rorg.OwenType,
                                  rorg.VisitID,
                              }).ToList();

                var allpersons = (from rperson in db.VisitPersons
                                 join person in db.PersonInfos on rperson.PersonID equals person.PersonID
                                 where visitIDs.Contains(rperson.VisitID)
                                 select new
                                 {
                                     person.PersonID,
                                     person.NameCN,
                                     rperson.OwenType,
                                     rperson.VisitID,
                                     rperson.Level
                                 }).ToList();

                var allTags = (from tag in db.VisitTags
                                  where visitIDs.Contains(tag.VisitID)
                                  select new
                                  {
                                      tag.VisitID,
                                      tag.Name
                                  }).ToList();


                #endregion
                if (data != null && data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        item.MainPersons = allpersons.Where(t => t.VisitID == item.VisitID && t.OwenType == EnumPersonOwenType.MainHanle)
                            .GroupBy(t => t.NameCN).Select(t=>t.Key) .ToList();

                        item.OurPersons = allpersons.Where(t => t.VisitID == item.VisitID && t.OwenType == EnumPersonOwenType.Our && t.Level == 0)
                            .GroupBy(t => t.NameCN).Select(t => t.Key).ToList();

                        //item.OurOPersons = allpersons.Where(t => t.VisitID == item.VisitID && t.OwenType == EnumPersonOwenType.Our && t.Level == 1)
                        //    .GroupBy(t => t.NameCN).Select(t => t.Key).ToList();

                        item.TheyPersons = allpersons.Where(t => t.VisitID == item.VisitID && t.OwenType == EnumPersonOwenType.They && t.Level == 0)
                            .GroupBy(t => t.NameCN).Select(t => t.Key).ToList();

                        //item.TheyOPersons = allpersons.Where(t => t.VisitID == item.VisitID && t.OwenType == EnumPersonOwenType.They && t.Level == 1)
                        //    .GroupBy(t => t.NameCN).Select(t => t.Key).ToList();


                        item.OurOrgs = allorgs.Where(t => t.VisitID == item.VisitID && t.OwenType ==  EnumOrgOwenType.Our)
                            .GroupBy(t => t.OrgName).Select(t => t.Key).ToList();

                        item.TheyOrgs = allorgs.Where(t => t.VisitID == item.VisitID && t.OwenType == EnumOrgOwenType.They)
                            .GroupBy(t => t.OrgName).Select(t => t.Key).ToList();

                        item.BeviOrgs = allorgs.Where(t => t.VisitID == item.VisitID && t.OwenType == EnumOrgOwenType.BeVi)
                            .GroupBy(t => t.OrgName).Select(t => t.Key).ToList();

                        item.VisitTags = allTags.Where(t=>t.VisitID == item.VisitID).GroupBy(t => t.Name).Select(t => t.Key).ToList();


                        sbHtml.Append("<tr>");
                        sbHtml.AppendFormat("<td >{0}</td>", item.VisitType);
                        sbHtml.AppendFormat("<td >{0}</td>", item.VisitID);
                        sbHtml.AppendFormat("<td >{0}</td>", item.VisitName);
                        sbHtml.AppendFormat("<td >{0}</td>", item.VisitFor);
                        sbHtml.AppendFormat("<td >{0}</td>", item.MainDepartment);

                        sbHtml.AppendFormat("<td >{0}</td>", item.MainPersons == null? "" : string.Join(",", item.MainPersons));
                        sbHtml.AppendFormat("<td >{0}</td>", item.OurOrgs == null ? "" : string.Join(",", item.OurOrgs));

                        sbHtml.AppendFormat("<td >{0}</td>", item.OurPersons == null ? "" : string.Join(",", item.OurPersons));
                        sbHtml.AppendFormat("<td >{0}</td>", item.OurOtherPersonStr);
                        sbHtml.AppendFormat("<td >{0}</td>", item.TheyOrgs == null ? "" : string.Join(",", item.TheyOrgs));
                        sbHtml.AppendFormat("<td >{0}</td>", item.TheyPersons == null ? "" : string.Join(",", item.TheyPersons));
                        sbHtml.AppendFormat("<td >{0}</td>", item.TheyOtherPersonStr);
                        sbHtml.AppendFormat("<td >{0}</td>", item.BeviOrgs == null ? "" : string.Join(",", item.BeviOrgs));
                        sbHtml.AppendFormat("<td >{0}</td>", item.VisType);
                        sbHtml.AppendFormat("<td >{0}</td>", item.FeeType);
                        sbHtml.AppendFormat("<td >{0}</td>", item.PayType);
                        sbHtml.AppendFormat("<td >{0}</td>", item.TakeLevel);

                        sbHtml.AppendFormat("<td >{0}</td>", item.IsLine);
                        sbHtml.AppendFormat("<td >{0}</td>", item.AnsLevel);
                        sbHtml.AppendFormat("<td >{0}</td>", item.VisitTags == null ? "" : string.Join(",", item.VisitTags));
                        sbHtml.AppendFormat("<td >{0}</td>", item.Remark);
                        var dt = item.FromDate.HasValue ? item.FromDate.Value.ToString("yyyy-MM-dd HH:mm") : "";
                        dt = dt + "--" + (item.EndDate.HasValue ? item.EndDate.Value.ToString("yyyy-MM-dd HH:mm") : "");
                        sbHtml.AppendFormat("<td >{0}</td>", dt);
                        sbHtml.AppendFormat("<td >{0}</td>", item.CreateUserName);
                        sbHtml.AppendFormat("<td >{0}</td>", item.CreateTime.ToString("yyyy-MM-dd HH:mm"));
                        sbHtml.AppendFormat("<td >{0}</td>", item.ModifyUserName);
                        sbHtml.AppendFormat("<td >{0}</td>", item.ModifyTime.HasValue ? item.ModifyTime.Value.ToString("yyyy-MM-dd HH:mm") : "");
                        sbHtml.Append("</tr>");
                    }
                    return sbHtml.ToString();
                }
            }
            return "";
        }
    }
}