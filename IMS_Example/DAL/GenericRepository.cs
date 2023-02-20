using IMS_Example.Data.Contexts;
using IMS_Example.Helpers;
using IMS_Example.Response;
using Token = IMS_Example.Helpers.TokenHelper.Token;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace IMS_Example.DAL
{
    public class GenericRepository<T> where T : class
    {
        internal AppDbContext _context;
        internal DbSet<T> _entities;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual async Task<ApiResponse<IEnumerable<T>>> GetListorGetListWithFilter(Token token, int? idModule, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var permission = GetPermission.GetPermissionForUser(token, idModule);
            if (permission.Get)
            {
                try
                {
                    IQueryable<T> query = _entities;
                    if (query == null)
                    {
                        return new ApiResponse<IEnumerable<T>>
                        {
                            Status = HttpStatusCode.NoContent,
                        };
                    }
                    if (filter != null)
                    {
                        query = query.Where(filter).AsNoTracking();
                    }
                    if (orderBy != null)
                    {
                        return new ApiResponse<IEnumerable<T>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = await orderBy(query).ToListAsync(),
                        };
                    }
                    else
                    {
                        return new ApiResponse<IEnumerable<T>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = await (query).ToListAsync(),
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new ApiResponse<IEnumerable<T>>
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = ex.Message,
                    };
                }
            }
            else
            {
                return new ApiResponse<IEnumerable<T>>
                {
                    Status = HttpStatusCode.Forbidden,
                };
            }
        }

        public virtual ApiResponse<T> GetById(int id, Token token, int? idModule)
        {
            var permission = GetPermission.GetPermissionForUser(token, idModule);
            if (permission.Get)
            {
                try
                {
                    var item = _entities.Find(id);
                    if (item == null)
                    {
                        return new ApiResponse<T>
                        {
                            Status = HttpStatusCode.NotFound,
                        };
                    }
                    else
                    {
                        return new ApiResponse<T>
                        {
                            Status = HttpStatusCode.OK,
                            Data = item,
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = ex.Message,
                    };
                }
            }
            else
            {
                return new ApiResponse<T>
                {
                    Status = HttpStatusCode.Forbidden,
                };
            }
        }

        public virtual ApiResponse<T> GetFirstOrdefaultWithFilter(Token token, int? idModule, Expression<Func<T, bool>> filter = null)
        {
            var permission = GetPermission.GetPermissionForUser(token, idModule);
            if (permission.Get)
            {
                try
                {
                    var item = _entities.AsNoTracking().FirstOrDefault();
                    if (item == null)
                    {
                        return new ApiResponse<T>
                        {
                            Status = HttpStatusCode.NotFound,
                        };
                    }
                    else
                    {
                        return new ApiResponse<T>
                        {
                            Status = HttpStatusCode.OK,
                            Data = item,
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = ex.Message,
                    };
                }
            }
            else
            {
                return new ApiResponse<T>
                {
                    Status = HttpStatusCode.Forbidden,
                };
            }
        }

        public virtual ApiResponse<T> Insert(T entity, Token token, int? idModule)
        {
            var permission = GetPermission.GetPermissionForUser(token, idModule);
            if (permission.Get)
            {
                try
                {
                    _entities.Add(entity);
                    _context.SaveChanges();
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.Created,
                    };
                }
                catch (Exception ex)
                {
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = ex.Message,
                    };
                }
            }
            else
            {
                return new ApiResponse<T>
                {
                    Status = HttpStatusCode.Forbidden,
                };
            }
        }

        public virtual ApiResponse<T> Update(T entity, Token token, int? idModule)
        {
            var permission = GetPermission.GetPermissionForUser(token, idModule);
            if (permission.Get)
            {
                try
                {
                    _entities.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.OK,
                    };
                }
                catch (Exception ex)
                {
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = ex.Message,
                    };
                }
            }
            else
            {
                return new ApiResponse<T>
                {
                    Status = HttpStatusCode.Forbidden,
                };
            }
        }

        public virtual ApiResponse<T> Delete(object id, Token token, int? idModule)
        {
            var permission = GetPermission.GetPermissionForUser(token, idModule);
            if (permission.Get)
            {
                try
                {
                    var entityToDelete = _entities.Find(id);
                    _entities.Attach(entityToDelete);
                    _context.Entry(entityToDelete).State = EntityState.Modified;
                    _context.SaveChanges();
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.OK,
                    };
                }
                catch (Exception ex)
                {
                    return new ApiResponse<T>
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = ex.Message,
                    };
                }
            }
            else
            {
                return new ApiResponse<T>
                {
                    Status = HttpStatusCode.Forbidden,
                };
            }
        }

    }
}
