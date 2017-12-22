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
using System.Text;
using System.Web;
using System.Web.Http;

namespace DocumentManage.Controllers.API
{
    public class PersonController : ApiController
    {
        private readonly PersonService personService;
        public PersonController()
        {
            personService = new PersonService();
        }

        [HttpPost]
        public ApiResult Edit([FromBody]RequestPersonDTO request)
        {
            string reason;
            var ret = personService.Edit(request, SecurityHelper.LoginUser.ID, out reason);

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
        public ApiResult GetDetail([FromBody]RequestPersonQDTO request)
        {
            var ret = personService.GetDetail(request);

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
        public ApiResult GetList([FromBody]RequestPersonQDTO request)
        {
            var ret = personService.GetList(request);
            return ret.ToApiResult();
        }

        [HttpPost]
        public ApiResult Delete([FromBody]RequestPersonQDTO request)
        {
            string reason;
            var ret = personService.Delete(request, SecurityHelper.LoginUser.ID, out reason);

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
        public ApiResult Export(RequestPersonQDTO request)
        {
            PersonService personService = new PersonService();

            var rootpath = ConfigurationManager.AppSettings["rootpath"].ToString();
            var fileid = "personList_" + DateTime.Now.ToString("yyyy-MM-dd");
            var filename = fileid + ".xls";

            var filePath = System.IO.Path.Combine(rootpath, filename);

            File.WriteAllBytes(filePath, Encoding.UTF8.GetBytes(personService.Export(request)));
            return fileid.ToApiResult();
        }
    }
}