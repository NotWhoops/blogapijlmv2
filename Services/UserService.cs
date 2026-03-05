using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using blogapibvh2.Models.DTO;
using blogapibvh2.Services.Context;
using blogapijlmv2.Models;
using blogapijlmv2.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace blogapijlmv2.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        //We need a helper method to check if the user exists in our database
        public bool DoesUserExist(string username)
        {
            //Check our tables to see if the user name exist
            return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
        }

        public bool AddUser(CreateAccountDTO userToAdd)
        {
            bool result = false;

            if (userToAdd.Username != null && !DoesUserExist(userToAdd.Username))
            {
                UserModel newUser = new UserModel();

                var HashedPassword = HashPassword(userToAdd.Password);

                newUser.Id = userToAdd.Id;
                newUser.Username = userToAdd.Username;

                newUser.Salt = HashedPassword.Salt;
                newUser.Hash = HashedPassword.Hash;

                _context.Add(newUser);

                result = _context.SaveChanges() != 0;

            }
            return result;
        }

        //we are going to need a hash helper function help us has our password
        //We need to set our newUser.Id = UserToAdd.Id

        //Username
        //Salt
        //Hash

        //then we add it to our DataContext
        //Save our changes

        //return a bool to return true of false


        //Function that will help hash our password
        public PasswordDTO HashPassword(string? password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();

            byte[] SaltBytes = new byte[64];

            var provider = RandomNumberGenerator.Create();
            provider.GetNonZeroBytes(SaltBytes);

            var Salt = Convert.ToBase64String(SaltBytes);

            var rfc2898DerviceBytes = new Rfc2898DeriveBytes(password ?? "", SaltBytes, 10000,
            HashAlgorithmName.SHA256);

            var Hash = Convert.ToBase64String(rfc2898DerviceBytes.GetBytes(256));

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;
        }

        //Helper function to verify Password
        public bool verifyUserPassword(string? Password, string? StoredHash, string? StoredSalt)
        {
            if (StoredSalt == null)
            {
                return false;
            }

            var SaltBytes = Convert.FromBase64String(StoredSalt);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password ?? "", SaltBytes, 10000,
            HashAlgorithmName.SHA256);

            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == StoredHash;

        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return _context.UserInfo;
        }

        public IActionResult Login(LoginDTO user)
        {
            IActionResult result = Unauthorized();
            //If the user exists
            if (DoesUserExist(user.Username))
            {
                //Create a secret key used to sing the JTW token
                //This should be store securely (not hard coded in produciton)
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersupersupersuperdupersecurekey@34456789"));
                //Create signing credentials using the secret key and HMACSHA256 alogrithm
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); //This ensures the token cant be tampered with

                //Build the JWT token with metadata

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials
                );
                
                //Conver the token object into string that can be sent to the client
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                //Return the token as JSON to the client
                result = Ok(new {Token = tokenString});

            }
            //Return either the token (if user exists) or Unauthorizd (if user does not exist)
            return result;
        }

        internal UserIdDTO GetUserIdDTOByUserName(string username)
        {
            throw new NotImplementedException();
        }
    }
}