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
    public class UserRepository : IUserRepository
    {
        public async Task<User> Create(User user)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Users.Add(user);
                await fileStorageDbContext.SaveChangesAsync();
                return user;
            }
        }

        public async Task Delete(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                User user = await GetById(id);
                fileStorageDbContext.Users.Remove(user);
                await fileStorageDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAll()
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Users.ToListAsync();
            }
        }

        public async Task<User> GetById(int id)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                return await fileStorageDbContext.Users.FindAsync(id);
            }
        }

        public async Task<User> Update(User user)
        {
            using (var fileStorageDbContext = new FileStorageDbContext())
            {
                fileStorageDbContext.Users.Update(user);
                await fileStorageDbContext.SaveChangesAsync();
                return user;
            }
        }
    }
}
