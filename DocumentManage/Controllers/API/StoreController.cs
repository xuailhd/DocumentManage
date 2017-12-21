using DocumentManage.Dtos;
using DocumentManage.EF;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DocumentManage.Controllers.API
{
    public class RenamingMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        private string _FileName { get; set; }

        private string _Directory { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileUrl
        {
            get
            {
                return string.Format("{0}\\{1}", _Directory, _FileName);
            }
        }

        /// <summary>
        /// 指定存储目录｛userPath}/{yy}/{mm}/{dd}
        /// </summary>
        /// <param name="root"></param>
        public RenamingMultipartFormDataStreamProvider(
            string rootPath,
            string Directory) : base(System.IO.Path.Combine(rootPath, Directory))
        {
            this._Directory = Directory;

        }


        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string filePath = headers.ContentDisposition.FileName;

            if (filePath.StartsWith(@"""") && filePath.EndsWith(@""""))
            {
                filePath = filePath.Substring(1, filePath.Length - 2);
            }
            //扩展名
            var extension = Path.GetExtension(filePath);
            //文件类型
            var contentType = headers.ContentType.MediaType;

            //完成文件名称｛userPath}/{yy}/{mm}/{dd}/{GUID}.{extension}
            this._FileName = Guid.NewGuid().ToString("N") + extension;

            this.Remark = filePath;

            return this._FileName;
        }

    }


    public class StoreController : ApiController
    {
        public async Task<ApiResult> Upload()
        {
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return new ApiResult("", EnumApiStatus.BizError, "UnsupportedMediaType");
            }

            try
            {
                //var stream = await Request.Content.ReadAsMultipartAsync();

                //文件存储配置
                var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();

                var directory = "temp";

                var provider = new RenamingMultipartFormDataStreamProvider(rootpath, directory);

                //// 接收数据，并保存文件
                var bodyparts = await Request.Content.ReadAsMultipartAsync(provider);

                return bodyparts.FileUrl.ToApiResult();
            }
            catch (Exception ex)
            {
                return new ApiResult("", EnumApiStatus.BizError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Download(string id)
        {
            return await Task.Run<HttpResponseMessage>(() =>
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                VisitFile visitFile = null;

                using (var db = new DBEntities())
                {
                    visitFile = db.VisitFiles.Where(t => t.FileID == id).FirstOrDefault();
                }

                var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();

                

                if (visitFile != null)
                {
                    var filePath = System.IO.Path.Combine(rootpath, visitFile.FileUrl);
                    if(File.Exists(filePath))
                    {
                        var stream = File.OpenRead(filePath);
                        result.Content = new StreamContent(stream);
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        result.Content.Headers.Add("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(visitFile.FileName) + "\"");
                        return result;
                    }
                    
                }
                else if (visitFile == null)
                {
                    var filePath = System.IO.Path.Combine(rootpath, id + ".xls");
                    if (File.Exists(filePath))
                    {
                        var stream = File.OpenRead(filePath);
                        result.Content = new StreamContent(stream);
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                        result.Content.Headers.Add("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(id) + ".xls\"");
                        return result;
                    }
                }
                throw new HttpException(404, "无效文件");
                
            });
        }

        
    }
}