using FileStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DataAccess.Abstract
{
    public interface IFileRepository
    {
        Task<List<Entities.File>> GetAll();
        Task<Entities.File> GetById(int id);
        Task<Entities.File> Create(Entities.File file);
        Task<Entities.File> Update(Entities.File file);
        Task Delete(int id);
    }
}
