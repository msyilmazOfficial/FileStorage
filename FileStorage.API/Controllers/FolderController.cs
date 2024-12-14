using FileStorage.Bussiness.Abstract;
using FileStorage.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;
        private readonly IPermissionService _permissionService;
        public FolderController(IFolderService folderService, IPermissionService permissionService)
        {
            _folderService = folderService;
            _permissionService = permissionService;
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
            var userId = User?.FindFirst(ClaimTypes.Sid)?.Value;
            Permission permission = await _permissionService.CheckPermission(Convert.ToInt32(userId), id, 0);
            if (permission == null || permission.AccessLevel != AccessLevel.Delete)
                return Unauthorized(new { message = "You do not have permission" });
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
            var userRole = User?.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "SuperAdmin" && userRole != "Admin")
                return Unauthorized(new { message = "You do not have permission" });
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
            var userId = User?.FindFirst(ClaimTypes.Sid)?.Value;
            Permission permission = await _permissionService.CheckPermission(Convert.ToInt32(userId), id, 0);
            if (permission == null || (permission.AccessLevel != AccessLevel.Read && permission.AccessLevel != AccessLevel.Write))
                return Unauthorized(new { message = "You do not have permission" });
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
            var userId = User?.FindFirst(ClaimTypes.Sid)?.Value;
            Permission permission = await _permissionService.CheckPermission(Convert.ToInt32(userId), folder.Id, 0);
            if (permission == null || permission.AccessLevel != AccessLevel.Write)
                return Unauthorized(new { message = "You do not have permission" });
            var oldFolder = await _folderService.GetById(folder.Id);
            if (oldFolder != null)
            {
                return Ok(await _folderService.Update(folder));
            }
            return NotFound();
        }
    }
}
