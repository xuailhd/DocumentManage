﻿using DocumentManage.Dtos;
using DocumentManage.Dtos.Request;
using DocumentManage.Filters;
using DocumentManage.Models;
using DocumentManage.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace DocumentManage.Controllers.API
{
    public class RecordController : ApiController
    {
        private readonly RecordService recordService;
        public RecordController()
        {
            recordService = new RecordService();
        }

        [HttpPost]
        public ApiResult Edit([FromBody]RequestVisitRecordDTO request)
        {
            string reason;
            var ret = recordService.Edit(request, SecurityHelper.LoginUser.ID, out reason);

            if (!ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
            else
            {
                return ret.ToApiResult();
            }

        }

        [HttpPost]
        public ApiResult GetDetail([FromBody]RequestVisitRecordQDTO request)
        {
            var ret = recordService.GetDetail(request);

            if (ret == null)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = "数据不存在，或没有权限" };
            }
            else
            {
                return ret.ToApiResult();
            }
        }

        [HttpPost]
        public ApiResult GetList([FromBody]RequestVisitRecordQDTO request)
        {
            var ret = recordService.GetList(request);
            return ret.ToApiResult();
        }

        [HttpPost]
        public ApiResult GetQueryList([FromBody]RequestVisitRecordQDTO request)
        {
            var ret = recordService.GetQueryList(request);
            ApiResult apiResult = new ApiResult() { Data = ret, Total = ret.VisitRecords == null ? 0 : ret.VisitRecords.TotalCount };
            return apiResult;
        }

        [HttpPost]
        public ApiResult Delete([FromBody]RequestVisitRecordQDTO request)
        {
            string reason;
            var ret = recordService.Delete(request, SecurityHelper.LoginUser.ID, out reason);

            if (!ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
            else
            {
                return ret.ToApiResult();
            }
        }

        [HttpPost]
        public ApiResult Export(RequestVisitRecordQDTO request)
        {
            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            var fileid = "recordList_" + DateTime.Now.ToString("yyyy-MM-dd");
            var filename = fileid + ".xls";

            var filePath = System.IO.Path.Combine(rootpath, filename);

            File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(recordService.ExportQuery(request)));
            return fileid.ToApiResult();
        }

        [HttpPost]
        public ApiResult ExportOne(RequestVisitRecordQDTO request)
        {
            List<string> files = new List<string>();

            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            var fileid = "record_" + request.VisitID;
            var filename = fileid + ".xls";
            var filePath = System.IO.Path.Combine(rootpath, filename);
            File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(recordService.ExportQuery(request)));
            files.Add(filePath);

            var ret = recordService.GetDetail(request);
            if (ret != null)
            {
                foreach (var file in ret.SJWLFiles)
                {
                    if (File.Exists(System.IO.Path.Combine(rootpath, file.FileUrl)))
                    {
                        File.Copy(System.IO.Path.Combine(rootpath, file.FileUrl), System.IO.Path.Combine(rootpath, file.FileName), true);
                        files.Add(System.IO.Path.Combine(rootpath, file.FileName));
                    }
                }

                foreach (var file in ret.LBWLFiles)
                {
                    if (File.Exists(System.IO.Path.Combine(rootpath, file.FileUrl)))
                    {
                        File.Copy(System.IO.Path.Combine(rootpath, file.FileUrl), System.IO.Path.Combine(rootpath, file.FileName), true);
                        files.Add(System.IO.Path.Combine(rootpath, file.FileName));
                    }
                }

                foreach (var file in ret.NBGLFiles)
                {
                    if (File.Exists(System.IO.Path.Combine(rootpath, file.FileUrl)))
                    {
                        File.Copy(System.IO.Path.Combine(rootpath, file.FileUrl), System.IO.Path.Combine(rootpath, file.FileName), true);
                        files.Add(System.IO.Path.Combine(rootpath, file.FileName));
                    }
                }

                foreach (var file in ret.HYXGFiles)
                {
                    if (File.Exists(System.IO.Path.Combine(rootpath, file.FileUrl)))
                    {
                        File.Copy(System.IO.Path.Combine(rootpath, file.FileUrl), System.IO.Path.Combine(rootpath, file.FileName), true);
                        files.Add(System.IO.Path.Combine(rootpath, file.FileName));
                    }
                }

                foreach (var file in ret.NewsFiles)
                {
                    if (File.Exists(System.IO.Path.Combine(rootpath, file.FileUrl)))
                    {
                        File.Copy(System.IO.Path.Combine(rootpath, file.FileUrl), System.IO.Path.Combine(rootpath, file.FileName), true);
                        files.Add(System.IO.Path.Combine(rootpath, file.FileName));
                    }
                }

                foreach (var file in ret.OtherFiles)
                {
                    if (File.Exists(System.IO.Path.Combine(rootpath, file.FileUrl)))
                    {
                        File.Copy(System.IO.Path.Combine(rootpath, file.FileUrl), System.IO.Path.Combine(rootpath, file.FileName), true);
                        files.Add(System.IO.Path.Combine(rootpath, file.FileName));
                    }
                }

            }
            CommonService.CompressFiles(files, System.IO.Path.Combine(rootpath, fileid + ".zip"));

            return fileid.ToApiResult();
        }
    }
}