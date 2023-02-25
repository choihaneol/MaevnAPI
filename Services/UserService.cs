using API.Models;
using API.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static API.Services.UserService;
using API.Services;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using Azure;


namespace API.Services
{
    public class UserService
    {
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO, B2bapiContext _db,
            string secretKey, IMapper _mapper)
        {
            //check Username 
            string requestUsername = loginRequestDTO.username;
            String userRole;
            bool isValid;
            User user = _db.Users.FirstOrDefault(u => u.LoginId == loginRequestDTO.username);


            //check id 
            if (user == null || string.IsNullOrEmpty(user.LoginId) )
            {
                isValid = false;
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null,
                    ErrorMassage = "Invalid",
                };
            }
            else
            {
                isValid = true;
                //role
                if (!string.IsNullOrEmpty(user.LoginId) && user.IsAdmin)
                {
                    userRole = "admin";
                }
                else
                {
                    userRole = "subAccount";
                }
            }

          
            if (user.IsActive == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null,
                    ErrorMassage = "Inactive",
                };
            }
     


            //check password     
            if (user.Password == loginRequestDTO.Password)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            //Logger, error, isactive, username, passwopr, token

            //if not valid
            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null,
                    ErrorMassage = "Invalid",
                };
            }


            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                         {
                    new Claim(ClaimTypes.Name, user.LoginId),
                    new Claim("username", user.LoginId.ToString()),
                     new Claim(ClaimTypes.Role, userRole)
                         }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

           

            //Actually generate token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDTO>(user),

            };


            return loginResponseDTO;
        }

    }
}