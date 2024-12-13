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
    public class FileRepository : IFileRepository
    {
        public async Task<Entities.File> Create(Entities.File file)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Files.Add(file);
                await fileStorageDbContext.SaveChangesAsync();
                return file;
            }
        }

        public async Task Delete(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                Entities.File file = await GetById(id);
                fileStorageDbContext.Files.Remove(file);
                await fileStorageDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Entities.File>> GetAll()
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Files.ToListAsync();
            }
        }

        public async Task<Entities.File> GetById(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Files.FindAsync(id);
            }
        }

        public async Task<Entities.File> Update(Entities.File file)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Files.Update(file);
                await fileStorageDbContext.SaveChangesAsync();
                return file;
            }
        }
    }
}
