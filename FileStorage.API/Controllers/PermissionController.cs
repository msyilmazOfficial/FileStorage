using FileStorage.Bussiness.Abstract;
using FileStorage.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        /// <summary>
        /// checks permissions returns boolean
        /// also checks parent folders
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="folderId"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{userId}/{folderId}/{fileId}")]
        public async Task<IActionResult> CheckPermission(int userId, int? folderId, int? fileId)
        {
            if ((folderId == null && fileId == null) || (folderId < 1 && fileId < 1))
            {
                return BadRequest(new { message = "Both folderId and fileId are empty." });
            }
            bool HasPermission = await _permissionService.CheckPermission(userId, folderId, fileId);
            return Ok(HasPermission);
        }


        /// <summary>
        /// only admins can Create permissions
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] Permission permission)
        {
            var Permission = await _permissionService.Create(permission);
            return CreatedAtAction(nameof(Create), Permission);
        }

        /// <summary>
        /// Delete permission with id
        /// Checks if permission exists. if so deletes.
        /// Only admins can delete permissions
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _permissionService.GetById(id) != null)
            {
                await _permissionService.Delete(id);
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// returns all the permissions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionService.GetAll();
            return Ok(permissions);
        }

        /// <summary>
        /// get permission by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _permissionService.GetById(id);
            if (permission != null)
            {
                return Ok(permission);
            }
            return NotFound();
        }



        /// <summary>
        /// Update permission 
        /// only admins can Update permissions
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] Permission permission)
        {

            var oldPermission = await _permissionService.GetById(permission.Id);
            if (oldPermission != null)
            {
                return Ok(await _permissionService.Update(permission));
            }
            return NotFound();
        }
    }
}
