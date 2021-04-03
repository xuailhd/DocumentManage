using DocumentManage.Common;
using DocumentManage.EF;
using DocumentManage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;

namespace DocumentManage.Services
{
    public class CommonService
    {
        public static void FindNewAndOld(List<VisitFile> olds, List<VisitFile> news, List<VisitFile> needdels, List<VisitFile> neednews)
        {
            needdels.Clear();
            foreach (var olditem in olds)
            {
                if (news == null || !news.Where(t => t.FileUrl == olditem.FileUrl).Any())
                {
                    needdels.Add(olditem);
                }
            }

            neednews.Clear();
            foreach (var newitem in news)
            {
                if (olds == null || !olds.Where(t => t.FileUrl == newitem.FileUrl).Any())
                {
                    neednews.Add(newitem);
                }
            }
        }

        public static void HandleFiles(List<VisitFile> needdels, List<VisitFile> neednews, List<string> oldFiles,
            string visitID, string type, Dictionary<string, string> copyfiles, DBEntities db)
        {
            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            if (needdels != null)
            {
                foreach (var item in needdels)
                {
                    if (File.Exists(System.IO.Path.Combine(rootpath, item.FileUrl)) && !oldFiles.Contains(item.FileUrl))
                    {
                        oldFiles.Add(System.IO.Path.Combine(rootpath, item.FileUrl));
                    }

                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                }
            }

            if (neednews != null)
            {
                foreach (var item in neednews)
                {
                    if (!string.IsNullOrEmpty(item.FileUrl))
                    {
                        try
                        {
                            if (File.Exists(System.IO.Path.Combine(rootpath, item.FileUrl)))
                            {
                                var curMonth = DateTime.Now.ToString("yyyyMM");
                                if (!Directory.Exists(System.IO.Path.Combine(rootpath, curMonth)))
                                {
                                    Directory.CreateDirectory(System.IO.Path.Combine(rootpath, curMonth));
                                }

                                var newpath = item.FileUrl.Replace("temp\\", curMonth + "\\");

                                if (!copyfiles.ContainsKey(System.IO.Path.Combine(rootpath, item.FileUrl)))
                                {
                                    copyfiles.Add(System.IO.Path.Combine(rootpath, item.FileUrl),
                                        System.IO.Path.Combine(rootpath, newpath));
                                }

                                item.FileUrl = newpath;
                                VisitFile visitFile = new VisitFile();
                                visitFile.FileID = Guid.NewGuid().ToString("N");
                                visitFile.FileUrl = item.FileUrl;
                                visitFile.FileName = item.FileName;
                                visitFile.OutID = visitID;
                                visitFile.Type = type;
                                db.VisitFiles.Add(visitFile);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteError(ex);
                        }
                    }
                }
            }
        }


        public static bool HasOtherDataAuth(string userID,DBEntities db)
        {
            var query = from rolemap in db.UserRoleMaps.Where(t => t.UserID == userID)
                        join roleauth in db.AuthRoleMaps.Where(t => t.AuthID == "LE001") on rolemap.RoleID equals roleauth.RoleID
                        select roleauth;

            return query.Any();
        }

        public static void CompressFiles(List<string> files, string zipfilename)
        {
            using (ZipOutputStream s = new ZipOutputStream(File.Create(zipfilename)))
            {

                s.SetLevel(9); // 0 - store only to 9 - means best compression
                byte[] buffer = new byte[4096];
                foreach (string file in files)
                {
                    if (!File.Exists(file))
                    {
                        continue;
                    }
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(file))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                s.Finish();
                s.Close();
            }
        }
    }
}