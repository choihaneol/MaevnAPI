using AutoMapper;
using API.Models.Dto;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Controllers
{

    [Route("/userAuth")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {


        private readonly B2bapiContext _db;
        protected APIResponse _response;
        private readonly UserService _userService;


        //for service
        private string secretKey;
        private readonly IMapper _mapper; //for call userDTO 


        public UserAPIController(B2bapiContext db, UserService userService, IConfiguration configuration, IMapper mapper)
        {
            _db = db;
            this._response = new();
            _userService = userService;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {


            var loginResponse = await _userService.Login(model,_db, secretKey, _mapper);
            User user = _db.Users.FirstOrDefault(u => u.LoginId == model.UserName);


            if (loginResponse.ErrorMassage == "Inactive")
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Your account is not activated. Please contact our customer service team.");

                return BadRequest(new { message = "Your account is not activated. Please contact customer service team." });
            }

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token) )
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");

                return BadRequest(new { message = "Username or password is incorrect" });
            }
           

        
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;  
            return Ok(_response);
        }
 

    }
}
