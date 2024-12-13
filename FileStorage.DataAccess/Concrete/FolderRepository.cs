using FileStorage.DataAccess.Abstract;
using FileStorage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DataAccess.Concrete
{
    public class FolderRepository : IFolderRepository
    {
        public async Task<Folder> Create(Folder folder)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Folders.Add(folder);
                await fileStorageDbContext.SaveChangesAsync();
                return folder;
            }
        }

        public async Task Delete(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                Folder folder = await GetById(id);
                fileStorageDbContext.Folders.Remove(folder);
                await fileStorageDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Folder>> GetAll()
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Folders.ToListAsync();
            }
        }

        public async Task<Folder> GetById(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Folders.FindAsync(id);
            }
        }

        public async Task<Folder> Update(Folder folder)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Folders.Update(folder);
                await fileStorageDbContext.SaveChangesAsync();
                return folder;
            }
        }
    }
}
