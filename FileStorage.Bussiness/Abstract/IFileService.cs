namespace FileStorage.Bussiness.Abstract
{
    public interface IFileService
    {
        Task<List<Entities.File>> GetAll();
        Task<Entities.File> GetById(int id);
        Task<Entities.File> Create(Entities.File file);
        Task<Entities.File> Update(Entities.File file);
        Task Delete(int id);
    }
}
