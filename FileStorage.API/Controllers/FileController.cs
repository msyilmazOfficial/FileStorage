using FileStorage.Bussiness.Abstract;
using FileStorage.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IPermissionService _permissionService;
        private readonly IConfiguration _config;
        public FileController(IFileService fileService, IPermissionService permissionService, IConfiguration config)
        {
            _fileService = fileService;
            _permissionService = permissionService;
            _config = config;
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
            IFormFile fileToUpload = file.FileToUpload;

            if (fileToUpload == null || fileToUpload.Length == 0)
                return BadRequest("No file uploaded.");
            var filePath = await UploadFileAsync(fileToUpload.OpenReadStream(), fileToUpload.FileName, "files");

            file.Url = filePath;
            var File = await _fileService.Create(file);
            return CreatedAtAction(nameof(Create), File);
        }


        private async Task<string> UploadFileAsync(Stream fileStream, string fileName, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(_config["AzureBlobStorage:ConnectionString"]);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobClient.Uri.ToString();
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
            var userId = User?.FindFirst(ClaimTypes.Sid)?.Value;
            Permission permission = await _permissionService.CheckPermission(Convert.ToInt32(userId), 0, id);
            if (permission == null || permission.AccessLevel != AccessLevel.Delete)
                return Unauthorized(new { message = "You do not have permission" });
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
            var userRole = User?.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "SuperAdmin" && userRole != "Admin")
                return Unauthorized(new { message = "You do not have permission" });
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
            var userId = User?.FindFirst(ClaimTypes.Sid)?.Value;
            Permission permission = await _permissionService.CheckPermission(Convert.ToInt32(userId), 0, id);
            if (permission == null || (permission.AccessLevel != AccessLevel.Read && permission.AccessLevel != AccessLevel.Write))
                return Unauthorized(new { message = "You do not have permission" });
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

            var userId = User?.FindFirst(ClaimTypes.Sid)?.Value;
            Permission permission = await _permissionService.CheckPermission(Convert.ToInt32(userId), 0, file.Id);
            if (permission == null || permission.AccessLevel != AccessLevel.Write)
                return Unauthorized(new { message = "You do not have permission" });
            var oldFile = await _fileService.GetById(file.Id);
            if (oldFile != null)
            {
                return Ok(await _fileService.Update(file));
            }
            return NotFound();
        }
    }
}
