using FileStorage.Bussiness.Abstract;
using FileStorage.DataAccess.Abstract;

namespace FileStorage.Bussiness.Concrete
{
    public class FileManager : IFileService
    {
        private readonly IFileRepository _fileRepository;
        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public async Task<Entities.File> Create(Entities.File file)
        {
            return await _fileRepository.Create(file);
        }

        public async Task Delete(int id)
        {
            await _fileRepository.Delete(id);
        }

        public async Task<List<Entities.File>> GetAll()
        {
            return await _fileRepository.GetAll();
        }

        public async Task<Entities.File> GetById(int id)
        {
            return await _fileRepository.GetById(id);
        }

        public async Task<Entities.File> Update(Entities.File file)
        {
            return await _fileRepository.Update(file);
        }
    }
}
