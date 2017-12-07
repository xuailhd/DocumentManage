using System;
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

        public bool ResetPassword(RequestChangePasswordDTO dto)
        {
            using (var db = new DBEntities())
            {
                var password = StringEncrypt.EncryptWithMD5(dto.NewPassword);
                var model = db.Users.Where(t => t.UserID == dto.UserID && !t.IsDeleted).FirstOrDefault();

                if (model != null)
                {
                    model.Password = password;
                    var ret = db.SaveChanges() > 0;

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

        public bool EditRole(Role dto, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var model = db.Roles.Where(t => t.RoleID == dto.RoleID).FirstOrDefault();

                if (model != null)
                {
                    
                }
                else
                {
                    var exits = db.Roles.Where(t => t.RoleName == dto.RoleName).FirstOrDefault();

                    if (exits != null)
                    {
                        reason = "角色名称重复";
                        return false;
                    }

                    db.Roles.Add(dto);
                }
                return db.SaveChanges() > 0;
            }
        }

        public PagedList<ResponseRoleDTO> GetRoleList(RequestRoleQDTO request)
        {
            using (var db = new DBEntities())
            {
                if(!string.IsNullOrEmpty(request.UserID))
                {
                    var query = from role in db.Roles
                                join uarole in db.UserRoleMaps.Where(t=>t.UserID == request.UserID) on role.RoleID equals uarole.RoleID into uaroleleft
                                from uaroleempty in uaroleleft.DefaultIfEmpty()
                                select new ResponseRoleDTO()
                                {
                                    RoleID = role.RoleID,
                                    RoleName = role.RoleName,
                                    Selected = uaroleempty == null ? false : true
                                };

                    query = query.OrderBy(t => t.RoleID);
                    return query.ToPagedList(request.PageIndex, int.MaxValue);
                }
                else
                {
                    var query = from role in db.Roles
                                select new ResponseRoleDTO()
                                {
                                     RoleID = role.RoleID,
                                      RoleName =role.RoleName,
                                };

                    query = query.OrderBy(t => t.RoleID);
                    return query.ToPagedList(request.PageIndex, request.PageSize);
                }
            }
        }

        public bool EditUserRoles(RequestEditUserRoleDTO dto, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var model = db.Users.Where(t => t.UserID == dto.UserID).FirstOrDefault();

                if (model == null)
                {
                    reason = "账号不存在";
                    return false;
                }
                else
                {
                    if (model.IsSystem)
                    {
                        reason = "系统账号不允许修改";
                    }

                    var olds = db.UserRoleMaps.Where(t => t.UserID == dto.UserID).ToList();
                    foreach (var item in olds)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

      
                    var listexit = db.Roles.Where(t => dto.RoleLists.Contains(t.RoleID)).Select(t=>t.RoleID).ToList();

                    foreach (var item in listexit)
                    {
                        UserRoleMap userRoleMap = new UserRoleMap()
                        {
                            MapID = Guid.NewGuid().ToString("N"),
                            RoleID = item,
                            UserID = dto.UserID
                        };
                        db.UserRoleMaps.Add(userRoleMap);
                    }
                }
                return db.SaveChanges() > 0;
            }
        }

        public bool EditRoleAuths(RequestEditRoleAuthDTO dto, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var model = db.Roles.Where(t => t.RoleID == dto.RoleID).FirstOrDefault();

                if (model == null)
                {
                    reason = "角色不存在";
                    return false;
                }
                else
                {
                    if (model.IsSystem)
                    {
                        reason = "系统角色不允许修改";
                    }

                    var olds = db.AuthRoleMaps.Where(t => t.RoleID == dto.RoleID).ToList();
                    foreach (var item in olds)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }


                    var listexit = db.AuthModels.Where(t => dto.AuthLists.Contains(t.AuthID)).Select(t => t.AuthID).ToList();

                    foreach (var item in listexit)
                    {
                        AuthRoleMap authRoleMap = new AuthRoleMap()
                        {
                            MapID = Guid.NewGuid().ToString("N"),
                            RoleID = dto.RoleID,
                            AuthID = item
                        };
                        db.AuthRoleMaps.Add(authRoleMap);
                    }
                }
                return db.SaveChanges() > 0;
            }
        }

        public PagedList<AuthModel> GetAuthList(RequestUserQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from auth in db.AuthModels
                            select auth;

                query = query.OrderBy(t => t.AuthID);

                return query.ToPagedList(request.PageIndex, request.PageSize);
            }
        }

        public PagedList<ResponseUserDTO> GetUserList(RequestUserQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from ua in db.Users
                            join rolemap in db.UserRoleMaps on ua.UserID equals rolemap.UserID
                            join role in db.Roles on rolemap.RoleID equals role.RoleID
                            group new { ua.LastTime , ua.UserID, ua.UserName, role.RoleID, role.RoleName }
                            by new { ua.LastTime , ua.UserID, ua.UserName} into gro
                            select new ResponseUserDTO()
                            {
                                LastTime = gro.Key.LastTime,
                                UserID = gro.Key.UserID,
                                UserName = gro.Key.UserName,
                                Roles = gro.Where(t => t.UserID == gro.Key.UserID).Select(t => new Role { RoleID = t.RoleID, RoleName = t.RoleName }).ToList()
                            };

                if(!string.IsNullOrEmpty(request.UserName))
                {
                    query = query.Where(t => t.UserName.Contains(request.UserName) || t.UserID.Contains(request.UserName));
                }

                query = query.OrderBy(t => t.UserID);

                var ret = query.ToPagedList(request.PageIndex, request.PageSize);

                ret.ForEach(t =>
                {
                    t.LastTimeStr = t.LastTime.HasValue? t.LastTime.Value.ToString("yyyy-MM-dd HH:mm:ss"):"";
                    t.RolesStr = string.Join(",", t.Roles.Select(q=>q.RoleName).ToList());
                });

                return ret;
            }
        }
    }
}