using FileStorage.Bussiness.Abstract;
using FileStorage.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;
        public FolderController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        /// <summary>
        /// Create folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] Folder folder)
        {
            var Folder = await _folderService.Create(folder);
            return CreatedAtAction(nameof(Create), Folder);
        }

        /// <summary>
        /// Delete folder with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _folderService.GetById(id) != null)
            {
                await _folderService.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// returns all the folders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var folders = await _folderService.GetAll();
            return Ok(folders);
        }

        /// <summary>
        /// get folder by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var folder = await _folderService.GetById(id);
            if (folder != null)
            {
                return Ok(folder);
            }
            return NotFound();
        }



        /// <summary>
        /// Update folder 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Folder folder)
        {

            var oldFolder = await _folderService.GetById(folder.Id);
            if (oldFolder != null)
            {
                return Ok(await _folderService.Update(folder));
            }
            return NotFound();
        }
    }
}
