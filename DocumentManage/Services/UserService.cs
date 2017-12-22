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
                    && t.ID == dto.UserID && !t.IsDeleted).FirstOrDefault();

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
                var model = db.Users.Where(t => t.ID == userid).FirstOrDefault();

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
                    && t.ID == dto.ID && !t.IsDeleted).FirstOrDefault();

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
                var model = db.Users.Where(t => t.ID == dto.ID && !t.IsDeleted).FirstOrDefault();

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
                            where ua.ID == userid
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
                            where ua.ID == userid
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
                            join role in db.Roles.Where(t=>t.State == 0) on rolemap.RoleID equals role.ID
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

        public bool EditRole(RequestRoleDTO dto, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                if (dto.IsNew)
                {
                    var exits = db.Roles.Where(t => (t.RoleName == dto.RoleName || t.RoleID == dto.RoleID) && t.State == 0).FirstOrDefault();

                    if (exits != null)
                    {
                        reason = "角色编码或者名称重复";
                        return false;
                    }

                    db.Roles.Add(new Role() {  ID = Guid.NewGuid().ToString("N"), RoleID = dto.RoleID, RoleName = dto.RoleName});
                }
                else
                {
                    var model = db.Roles.Where(t => t.ID == dto.ID).FirstOrDefault();

                    if (model.IsSystem)
                    {
                        reason = "系统角色不允许修改";
                        return false;
                    }

                    if(model == null)
                    {
                        return true;
                    }
                    else
                    {
                        model.RoleName = dto.RoleName;
                    }

                }
                return db.SaveChanges() > 0;
            }
        }

        public bool DeleteRole(RequestRoleDTO dto, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var model = db.Roles.Where(t => t.ID == dto.ID).FirstOrDefault();

                if (model.IsSystem)
                {
                    reason = "系统角色不允许修改";
                    return false;
                }

                if (model == null)
                {
                    return true;
                }
                else
                {
                    model.State = 1;
                }
                return db.SaveChanges() > 0;
            }
        }

        public PagedList<ResponseRoleDTO> GetRoleList(RequestRoleQDTO request)
        {
            using (var db = new DBEntities())
            {
                if(!string.IsNullOrEmpty(request.ID))
                {
                    var query = from role in db.Roles.Where(t=>t.State == 0)
                                join uarole in db.UserRoleMaps.Where(t=>t.UserID == request.ID) on role.ID equals uarole.RoleID into uaroleleft
                                from uaroleempty in uaroleleft.DefaultIfEmpty()
                                select new ResponseRoleDTO()
                                {
                                    ID = role.ID,
                                    RoleID = role.RoleID,
                                    RoleName = role.RoleName,
                                    Selected = uaroleempty != null
                                };

                    query = query.OrderBy(t => t.RoleID);
                    return query.ToPagedList(request.PageIndex, int.MaxValue);
                }
                else
                {
                    var query = from role in db.Roles.Where(t=>t.State == 0)
                                select new ResponseRoleDTO()
                                {
                                    ID = role.ID,
                                    RoleID = role.RoleID,
                                    RoleName = role.RoleName,
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
                var model = db.Users.Where(t => t.ID == dto.ID && !t.IsDeleted).FirstOrDefault();

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
                        return false;
                    }

                    var olds = db.UserRoleMaps.Where(t => t.UserID == dto.ID).ToList();
                    foreach (var item in olds)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var rolelist = dto.RoleLists.Where(t => t.Selected).Select(t => t.ID).ToList();

                    var listexit = db.Roles.Where(t => rolelist.Contains(t.ID) && t.State == 0).Select(t=>t.ID).ToList();

                    foreach (var item in listexit)
                    {
                        UserRoleMap userRoleMap = new UserRoleMap()
                        {
                            MapID = Guid.NewGuid().ToString("N"),
                            RoleID = item,
                            UserID = dto.ID
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
                var model = db.Roles.Where(t => t.ID == dto.ID).FirstOrDefault();

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
                        return false;
                    }

                    var olds = (from auths in db.AuthModels.Where(t => t.Type == dto.Type)
                                join rolemap in db.AuthRoleMaps on auths.AuthID equals rolemap.AuthID
                                where rolemap.RoleID == dto.ID
                                select rolemap).ToList();
                    foreach (var item in olds)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }

                    var authlist = dto.AuthLists.Where(t => t.Selected).Select(t => t.AuthID).ToList();

                    var listexit = db.AuthModels.Where(t => authlist.Contains(t.AuthID)).Select(t => t.AuthID).ToList();

                    foreach (var item in listexit)
                    {
                        AuthRoleMap authRoleMap = new AuthRoleMap()
                        {
                            MapID = Guid.NewGuid().ToString("N"),
                            RoleID = dto.ID,
                            AuthID = item
                        };
                        db.AuthRoleMaps.Add(authRoleMap);
                    }
                }
                return db.SaveChanges() > 0;
            }
        }

        public PagedList<ResponseAuthModelDTO> GetAuthList(RequestAuthModelQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from auth in db.AuthModels.Where(t=>t.Type == request.Type)
                            join automap in db.AuthRoleMaps.Where(t=>t.RoleID == request.ID) on auth.AuthID equals automap.AuthID into automapLeft
                            from automapEmpty in automapLeft.DefaultIfEmpty()
                            select new ResponseAuthModelDTO()
                            {
                                AuthID = auth.AuthID,
                                AuthName = auth.AuthName,
                                Selected = automapEmpty != null
                            };

                query = query.OrderBy(t => t.AuthID);

                return query.ToPagedList(request.PageIndex, request.PageSize);
            }
        }


        public PagedList<ResponseUserDTO> GetUserList(RequestUserQDTO request)
        {
            using (var db = new DBEntities())
            {
                var query = from ua in db.Users.Where(t=>!t.IsDeleted)
                            join rolemap in db.UserRoleMaps on ua.ID equals rolemap.UserID into rolemapLeft
                            from rolemapEmpty in rolemapLeft.DefaultIfEmpty()
                            join role in db.Roles.Where(t=>t.State == 0) on rolemapEmpty.RoleID equals role.ID into roleLeft
                            from roleEmpty in roleLeft.DefaultIfEmpty()
                            group new { ua.LastTime , ua.ID, ua.UserID, ua.UserName, roleEmpty.RoleName }
                            by new { ua.LastTime , ua.ID, ua.UserID, ua.UserName} into gro
                            select new ResponseUserDTO()
                            {
                                LastTime = gro.Key.LastTime,
                                ID = gro.Key.ID,
                                UserID = gro.Key.UserID,
                                UserName = gro.Key.UserName,
                                Roles = gro.Where(t => t.ID == gro.Key.ID).Select(t => new ResponseRoleDTO { RoleName = t.RoleName }).ToList()
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

        public bool AddAccount(RequestUserInfoDTO dto, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                User ua = dto.Map<RequestUserInfoDTO, User>();
                ua.ID = Guid.NewGuid().ToString("N");
                ua.Password = StringEncrypt.EncryptWithMD5(dto.Password);
                db.Users.Add(ua);

                return db.SaveChanges() > 0;
            }
        }

        public bool DeleteAccount(RequestUserInfoDTO dto, out string reason)
        {
            reason = "";
            using (var db = new DBEntities())
            {
                var model = db.Users.Where(t => t.ID == dto.ID && !t.IsDeleted).FirstOrDefault();

                if (model != null)
                {
                    if (model.IsSystem)
                    {
                        reason = "系统用户不允许删除";
                        return false;
                    }

                    model.IsDeleted = true;
                    return db.SaveChanges() > 0;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}