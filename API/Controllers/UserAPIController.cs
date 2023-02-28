using AutoMapper;
using API.Models.Dto;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;

namespace API.Controllers
{

    [Route("/userAuth")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {


        private readonly B2bapiContext _db;
        protected APIResponse _response;
        private readonly UserService _userService;
        private User loginedUser;

        //for service
        private string secretKey;
        private readonly IMapper _mapper; //for call userDTO 

       // private readonly UserManager<User> _userManager;


        public UserAPIController(B2bapiContext db, UserService userService, IConfiguration configuration, IMapper mapper)
        {
            _db = db;
            this._response = new();
            _userService = userService;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _mapper = mapper;

           // _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {


            var loginResponse = await _userService.Login(model,_db, secretKey, _mapper);
            User user = _db.Users.FirstOrDefault(u => u.LoginId == model.username);


            if (loginResponse.ErrorMassage == "Inactive")
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Your account is not activated. Please contact our customer service team.");

                return BadRequest(new { message = "Your account is not activated. Please contact customer service team." });
            }

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token) || loginResponse.ErrorMassage == "Invalid")
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");

                return BadRequest(new { message = "Username or password is incorrect" });
            }

            loginedUser = user;

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;  
            return Ok(_response);
        }

        [Authorize]
        [HttpGet]
        [Route("Users/current")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> getLoggedInUserId()
        {
            string id = Convert.ToString(HttpContext.User.FindFirstValue("username"));
 

            
            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                         {
                    new Claim(ClaimTypes.Name, id),
                    new Claim("username", id),
                         }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            //Actually generate token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            

            return Ok(new { LoginId = id, Token = tokenHandler.WriteToken(token) });

         }



        /*
        [Authorize]
        [HttpGet("currentUser")]
        public ICollection<User> GetUserInfo()
        {
         
                //var usertest = await _userManager.FindByNameAsync(User.Identity.Name);
                return _db.Users.FromSqlInterpolated($"select * from b2bapi.dbo.Users").OrderBy(a => a.LoginId).ToList();

            /*
            if (loginedUser == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("User is not available");
                return BadRequest(new { message = "Username or password is incorrect" });

            }
            else //generate token
            {
                //if user was found generate JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                       {
                    new Claim(ClaimTypes.Name, loginedUser.LoginId),
                       }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };


                //Actually generate token
                var token = tokenHandler.CreateToken(tokenDescriptor);
                LoginResponseDTO loginResponse = new LoginResponseDTO()
                {
                    Token = tokenHandler.WriteToken(token),
                    User = _mapper.Map<UserDTO>(loginedUser),

                };
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = loginResponse;
                return Ok(_response);
            }

           
        }  */



    }
}
