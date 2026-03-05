using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapibvh2.Models.DTO;
using blogapijlmv2.Models;
using blogapijlmv2.Models.DTO;
using blogapijlmv2.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogapijlmv2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;
        public UserController(UserService dataFromService)
        {
            _data = dataFromService;
        }

        //Function to add our user type of CreateAccountDTO call UserToadd this will return bool once our user is added
        //Add user
        [HttpPost("AddUser")]
        public bool AddUser(CreateAccountDTO UserToAdd)
        {
            return _data.AddUser(UserToAdd);
        }

        //GetAllUsers
        [HttpGet("GetAllUsers")]

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _data.GetAllUsers();
        }


        //GetUserByUsername

        [HttpGet("GetUserByUserName")]

        public UserIdDTO GetUserDTOUserName(string username)
        {
            return _data.GetUserIdDTOByUserName(username);
        }


        //Login Endpoint 

        [HttpPost("Login")]

        public IActionResult Login ( [FromBody] LoginDTO user)
        {
            return _data.Login(user);
        }
    }
}