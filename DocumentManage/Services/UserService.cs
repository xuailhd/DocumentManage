﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentManage.EF;
using DocumentManage.Models;
using DocumentManage.Dtos;
using DocumentManage.Common;

namespace DocumentManage.Services
{
    public class UserService
    {
        public ResponseLoginDTO Login(RequestLoginDTO dto)
        {
            using (var db = new DBEntities())
            {
                var password = StringEncrypt.EncryptWithMD5(dto.Password);
                var model = db.Users.Where(t => t.Password == password 
                    && t.UserID == dto.UserID && !t.IsDeleted).FirstOrDefault();

                if(model != null)
                {
                    model.UserToken = Guid.NewGuid().ToString("N");
                    model.LastTime = DateTime.Now;
                    db.SaveChanges();

                    return new ResponseLoginDTO() { UserToken = model.UserToken };
                }
                return null;
            }
        }

        public bool LoginOut(string userid)
        {
            using (var db = new DBEntities())
            {
                var model = db.Users.Where(t => t.UserID == userid).FirstOrDefault();

                if (model != null)
                {
                    model.UserToken = "";
                    return db.SaveChanges()>0;
                }
                return false;
            }
        }

        public bool UpdatePassword(RequestChangePasswordDTO dto)
        {
            using (var db = new DBEntities())
            {
                var oldpassword = StringEncrypt.EncryptWithMD5(dto.OldPassword);
                var password = StringEncrypt.EncryptWithMD5(dto.NewPassword);
                var model = db.Users.Where(t => t.Password == oldpassword
                    && t.UserID == dto.UserID && !t.IsDeleted).FirstOrDefault();

                if (model != null)
                {
                    model.Password = password;
                    var ret = db.SaveChanges()>0;

                    return ret;
                }
                return false;
            }
        }

        public RequestUserInfoDTO GetUserInfo(string userid)
        {
            using (var db = new DBEntities())
            {
                var query = from ua in db.Users
                            where ua.UserID == userid
                            select new RequestUserInfoDTO()
                            {
                                UserName = ua.UserName
                            };

                return query.FirstOrDefault();
            }
        }

        public bool UpdateUserInfo(RequestUserInfoDTO dto, string userid)
        {
            using (var db = new DBEntities())
            {
                var query = from ua in db.Users
                            where ua.UserID == userid
                            select ua;

                var model = query.FirstOrDefault();

                model.UserName = dto.UserName;

                return db.SaveChanges() > 0;
            }
        }

        public List<AuthModel> GetUserAuths(RequestAuthDTO dto)
        {
            using (var db = new DBEntities())
            {
                var query = from rolemap in db.UserRoleMaps
                            join authmap in db.AuthRoleMaps on rolemap.RoleID equals authmap.RoleID
                            join auths in db.AuthModels on authmap.AuthID equals auths.AuthID
                            where rolemap.UserID  == dto.UserID && auths.Type == dto.Type
                            select auths;

                var data = query.Distinct().ToList();

                if(data == null)
                {
                    return new List<AuthModel>();
                }

                data = data.OrderBy(t => t.OrderNo).ToList();

                return data;
            }
        }
    }
}