using FileStorage.Bussiness.Abstract;
using FileStorage.DataAccess.Abstract;
using FileStorage.Entities;
using System.Security;

namespace FileStorage.Bussiness.Concrete
{
    public class PermissionManager : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionManager(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Permission> CheckPermission(int userId, int? folderId, int? fileId)
        {
            if ((folderId == null && fileId == null) || (folderId < 1 && fileId < 1))
            {
                return await Task.FromResult(new Permission());
            }
            return await _permissionRepository.CheckPermission(userId, folderId, fileId);
        }

        public async Task<Permission> Create(Permission permission)
        {
            return await _permissionRepository.Create(permission);
        }

        public async Task Delete(int id)
        {
            await _permissionRepository.Delete(id);
        }

        public async Task<List<Permission>> GetAll()
        {
            return await _permissionRepository.GetAll();
        }

        public async Task<Permission> GetById(int id)
        {
            return await _permissionRepository.GetById(id);
        }

        public async Task<Permission> Update(Permission permission)
        {
            return await _permissionRepository.Update(permission);
        }
    }
}
