using AutoMapper;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MCSAndroidAPI.Repositories
{
    public class AuthenticateRepository : RepositoryBase<MUser>, IAuthenticateRepository
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger _logger;

        #region Encrypt Key
        //Create a variable to control the key, with start key 'm' is the static variable
        protected const string m_EncryptKey = "@Nidec";
        #endregion
        public AuthenticateRepository(NidecMCSContext nidecMCSContext,IMapper mapper , IConfiguration configuration, ILogger logger) : base(nidecMCSContext, mapper)
        {
            _configuration = configuration;
            _logger = logger;
        }

        #region Login Method
        public async Task<ResponseModel<JWTTokenResponse>> AuthenticateAsync(LoginModel model)
        {
            var response = new ResponseModel<JWTTokenResponse>();

            JWTTokenResponse tokenResponse = new JWTTokenResponse();

            MUser? user = await CheckUserAsync(model.Username, model.Password);

            if (user != null)
            {
                try
                {
                    if (await ValidateUserAsync(model.Username, model.Password))
                    {
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Role, user.RoleId.ToString()??"0"),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.UtcNow.AddDays(3),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256));

                        tokenResponse.Fullname = user.FullName;
                        tokenResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);

                        Generation.GenerateResponse(ref response, tokenResponse);
                    }
                    else
                    {
                        _logger.LogWarning($"[Authenticate] {SystemConstants.Message.USER_INACTIVE}");
                        Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.USER_INACTIVE);
                    }
                } catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    Debug.WriteLine(ex);
                    Generation.GenerateResponse(ref response, null, false);
                }
            }
            else 
            {
                _logger.LogWarning($"[Authenticate] {SystemConstants.Message.LOGIN_ERROR}");
                Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.LOGIN_ERROR);
            }

            return response;
        }
        #endregion

        #region Encrypt Method
        /// <summary>
        /// Function Encrypt the string to MD5 string
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, bool useHashing)
        {

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(m_EncryptKey));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(m_EncryptKey);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        #endregion
        #region Check User Login
        /// <summary>
        /// Checking User Name and Password are exists in db
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<MUser?> CheckUserAsync(string userName, string password)
        {
            try
            {
                string encryptedPassword = Encrypt(password + userName.ToLower(), true);
                return await NidecMCSContext.MUsers.FirstOrDefaultAsync(i => i.UserName.ToLower() == userName.ToLower() && i.PassWord == encryptedPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Checking user status
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ValidateUserAsync(string userName, string password)
        {
            string encryptedPassword = Encrypt(password + userName.ToLower(), true);
            var o = await NidecMCSContext.MUsers.FirstOrDefaultAsync(i => i.UserName.ToLower() == userName.ToLower() && i.PassWord == encryptedPassword && i.IsActive == true);
            return (o != null);
        }
        #endregion

    }
}
