using FileStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.DataAccess.Abstract
{
    public interface IFolderRepository
    {
        Task<List<Folder>> GetAll();
        Task<Folder> GetById(int id);
        Task<Folder> Create(Folder folder);
        Task<Folder> Update(Folder folder);
        Task Delete(int id);
    }
}
