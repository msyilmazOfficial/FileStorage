using FileStorage.Bussiness.Abstract;
using FileStorage.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Create file 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] Entities.File file)
        {
            var File = await _fileService.Create(file);
            return CreatedAtAction(nameof(Create), File);
        }

        /// <summary>
        /// Delete file with id
        /// Checks if file exists. if so deletes.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _fileService.GetById(id) != null)
            {
                await _fileService.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// returns all the files
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var files = await _fileService.GetAll();
            return Ok(files);
        }

        /// <summary>
        /// get file by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var file = await _fileService.GetById(id);
            if (file != null)
            {
                return Ok(file);
            }
            return NotFound();
        }



        /// <summary>
        /// Update file 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Entities.File file)
        {

            var oldFile = await _fileService.GetById(file.Id);
            if (oldFile != null)
            {
                return Ok(await _fileService.Update(file));
            }
            return NotFound();
        }
    }
}
