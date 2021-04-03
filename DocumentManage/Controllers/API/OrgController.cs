using DocumentManage.Dtos;
using DocumentManage.Dtos.Request;
using DocumentManage.Filters;
using DocumentManage.Models;
using DocumentManage.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace DocumentManage.Controllers.API
{
    public class OrgController : ApiController
    {
        [HttpPost]
        public ApiResult Edit([FromBody]RequestOrgDTO request)
        {
            OrgService orgService = new OrgService();
            string reason;
            var ret = orgService.Edit(request, SecurityHelper.LoginUser.ID, out reason);

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
        public ApiResult GetDetail([FromBody]RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();
            var ret = orgService.GetDetail(request);

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
        public ApiResult GetList([FromBody]RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();
            var ret = orgService.GetList(request);
            return ret.ToApiResult();
        }

        [HttpPost]
        public ApiResult Delete([FromBody]RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();
            string reason;
            var ret = orgService.Delete(request, SecurityHelper.LoginUser.ID, out reason);

            if (!ret)
            {
                return new ApiResult() { Status = EnumApiStatus.BizError, Msg = reason };
            }
            else
            {
                return ret.ToApiResult();
            }
        }

        //[HttpPost]
        //public HttpResponseMessage Export(RequestOrgQDTO request)
        //{
        //    OrgService orgService = new OrgService();
        //    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //    var stream = new MemoryStream(Encoding.UTF8.GetBytes(orgService.Export(request)));
        //    result.Content = new StreamContent(stream);
        //    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    result.Content.Headers.Add("Content-Disposition", "attachment;filename=\"机构列表_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls\"");
        //    return result;
        //}

        [HttpPost]
        public ApiResult Export(RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();

            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            var fileid = "orgList_" + DateTime.Now.ToString("yyyy-MM-dd");
            var filename = fileid + ".xls";

            var filePath = System.IO.Path.Combine(rootpath, filename);

            File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(orgService.Export(request)));
            return fileid.ToApiResult();
        }

        public ApiResult ExportOne(RequestOrgQDTO request)
        {
            OrgService orgService = new OrgService();
            List<string> files = new List<string>();

            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            var fileid = "org_" + request.OrgID;
            var filename = fileid + ".xls";
            var filePath = System.IO.Path.Combine(rootpath, filename);
            File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(orgService.Export(request)));
            files.Add(filePath);

            var ret = orgService.GetDetail(request);
            if (ret != null)
            {
                foreach (var file in ret.BJFiles)
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