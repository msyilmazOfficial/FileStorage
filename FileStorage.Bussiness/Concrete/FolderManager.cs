using FileStorage.Bussiness.Abstract;
using FileStorage.DataAccess.Abstract;
using FileStorage.Entities;

namespace FileStorage.Bussiness.Concrete
{
    public class FolderManager : IFolderService
    {
        private readonly IFolderRepository _folderRepository;
        public FolderManager(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public async Task<Folder> Create(Folder folder)
        {
            return await _folderRepository.Create(folder);
        }

        public async Task Delete(int id)
        {
            await _folderRepository.Delete(id);
        }

        public async Task<List<Folder>> GetAll()
        {
            return await _folderRepository.GetAll();
        }

        public async Task<Folder> GetById(int id)
        {
            return await _folderRepository.GetById(id);
        }

        public async Task<Folder> Update(Folder folder)
        {
            return await _folderRepository.Update(folder);
        }
    }
}
