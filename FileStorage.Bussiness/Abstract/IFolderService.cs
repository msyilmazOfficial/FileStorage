using FileStorage.Entities;

namespace FileStorage.Bussiness.Abstract
{
    public interface IFolderService
    {
        Task<List<Folder>> GetAll();
        Task<Folder> GetById(int id);
        Task<Folder> Create(Folder folder);
        Task<Folder> Update(Folder folder);
        Task Delete(int id);
    }
}
