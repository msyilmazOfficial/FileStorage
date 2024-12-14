using FileStorage.DataAccess.Abstract;
using FileStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.DataAccess.Concrete
{
    public class PermissionRepository : IPermissionRepository
    {

        public async Task<bool> CheckPermission(int userId, int? folderId, int? fileId)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                if (folderId > 0)
                {
                    if (fileStorageDbContext.Permissions.ToList().Exists(permission => permission.FolderId == folderId && permission.UserId == userId))
                        return await Task.FromResult(true);

                    Folder folder = fileStorageDbContext.Folders.Find(folderId);
                    if (folder == null || folder.ParentFolderId == null)
                        return await Task.FromResult(false);


                    return await CheckPermission(userId, folder.ParentFolderId, fileId);
                }
                else if (fileId > 0)
                    return await Task.FromResult(fileStorageDbContext.Permissions.ToList().Exists(permission => permission.FileId == fileId && permission.UserId == userId));
                else
                    return await Task.FromResult(false);
            }
        }

        public async Task<Permission> Create(Permission permission)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Permissions.Add(permission);
                await fileStorageDbContext.SaveChangesAsync();
                return permission;
            }
        }

        public async Task Delete(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                Permission permission = await GetById(id);
                fileStorageDbContext.Permissions.Remove(permission);
                await fileStorageDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Permission>> GetAll()
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Permissions.ToListAsync();
            }
        }

        public async Task<Permission> GetById(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Permissions.FindAsync(id);
            }
        }

        public async Task<Permission> Update(Permission permission)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Permissions.Update(permission);
                await fileStorageDbContext.SaveChangesAsync();
                return permission;
            }
        }
    }
}
