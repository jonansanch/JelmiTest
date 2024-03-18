using Domain.DTO;
using Domain.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JelmiTest.Controllers
{
    /// <summary>
    /// Manejo de Usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        #region fields
        private readonly IUserService _userService;        
        #endregion

        #region Constructor
        /// <summary>
        /// Controlador de Cuentas
        /// </summary>
        /// <param name="accountService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtiene la lista de todos los usuarios
        /// </summary>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var lstAreaDeTrabajo = await _userService.GetAllAsync();
            return Ok(lstAreaDeTrabajo);
        }

        /// <summary>
        /// Obtiene usuario por id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserId/{id}")]
        public async Task<IActionResult> GetUserId(Guid id)
        {
            var lst = await _userService.GetUserID(id);
            return Ok(lst);
        }

        /// <summary>
        /// Crea o edita un usuario
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> Create(UserDto data)
        {
            var result = await _userService.AddOrUpdateAsync(data);
            return Ok(result);
        }
        
        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<object> Delete(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return new
            {
                data = result,
                status = "success",
                msg = "success_save"
            };            
        }
        #endregion
    }
}
