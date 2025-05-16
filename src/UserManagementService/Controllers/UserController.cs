using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Core.Data;
using UserManagementService.Core.UserDTO;
using UserManagementService.Core.userServiceContract;
using UserManagementService.Infra;

namespace UserManagementService.app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserContract _userRepository;

        public UserController(IuserContract userrepository)
        {
            _userRepository = userrepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            var response = await _userRepository.Login(user);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet]
        [Route("getuserTotalCashback/{userId}")]

        public async Task<IActionResult> GetTotalUserCashback([FromRoute]int userId)
        {
            var response = await _userRepository.getTotalCashback(userId);
            return Ok(response);
        }

        

        [HttpGet]
        [Route("getUserClickHistory/{userId}")]
        public async Task<IActionResult> GetUserClickHistory([FromRoute] int userId)
        {
            var response = await _userRepository.getClickHistory(userId);
            return Ok(response);
        }

        [HttpGet]
        [Route("getPurchaseHistory/{userId}")]
        public async Task<IActionResult> GetPurchaseHistory([FromRoute] int userId)
        {
            var response = await _userRepository.getcpurchasehistory(userId);
            return Ok(response);
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> AddUser([FromBody] userAddRequest user)
        {
            var response = await _userRepository.AddUser(user);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet]
        [Route("ListOfUsers")]
         public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("getUser/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound(new response { Success = false, Message = "User not found." });
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] updateUserDTO user)
        {
           
             var response = await _userRepository.UpdateUser(id ,user);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var response = await _userRepository.DeleteUser(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
