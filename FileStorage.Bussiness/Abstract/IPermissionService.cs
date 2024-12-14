using FileStorage.Entities;

namespace FileStorage.Bussiness.Abstract
{
    public interface IPermissionService
    {
        Task<List<Permission>> GetAll();
        Task<Permission> GetById(int id);
        Task<Permission> Create(Permission permission);
        Task<Permission> Update(Permission permission);
        Task Delete(int id);
        Task<bool> CheckPermission(int userId, int? folderId, int? fileId);
    }
}
