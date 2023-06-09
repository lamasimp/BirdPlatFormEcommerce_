
using BirdPlatForm.UserRespon;
using BirdPlatForm.ViewModel;
using BirdPlatFormEcommerce.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BirdPlatForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SwpContext _context;
        private readonly IConfiguration _config;

        public UserController(SwpContext bird, IConfiguration config)
        {
            _context = bird;
            _config = config;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserView model)
        
        {
            var user =  _context.TbUsers.Include(x => x.Role).SingleOrDefault(o => o.Email == model.Email && model.Password == o.Password);
            if (user == null) return Ok(new ErrorRespon
            {
                Error = false,
                Message = "Email Or Password Incorrect :("
            });
            var token = await GeneratToken(user);
            return Ok(new ErrorRespon
            {
                Error = true,
                Message = "Login Success",
                Data = token
            });
        }

        private async Task<TokenRespon> GeneratToken(TbUser user)
        {

            var jwttokenHandler = new JwtSecurityTokenHandler();
            var secretkey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);

            var tokendescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Email", user.Email),
                    new Claim(ClaimTypes.Role, user.RoleId)


                }),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],

                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha512)


            };
            var token = jwttokenHandler.CreateToken(tokendescription);
            var accessToken = jwttokenHandler.WriteToken(token);
            return new TokenRespon
            {
                AccessToken = accessToken,
            } ;
            ////var refreshToken = GenerateRefreshToken();

            ////Lưu database  
            //var refreshTokenEntity = new TbToken
            //{

            //    Id = user.UserId,
            //    UserId = user.UserId,
            //    Token = refreshToken,
            //    ExpiryDate = DateTime.UtcNow.AddSeconds(10),
            //};

            //await _context.AddAsync(refreshTokenEntity);
            //await _context.SaveChangesAsync();

            //return new TokenRespon
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken
            //};
        }

        private string GenerateRefreshToken()
        {

            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }
        [HttpPost("Register")]
        public async Task<ActionResult<TbUser>> Register(RegisterModel register)
        {
            var regis =  _context.TbUsers.FirstOrDefault(o => o.Email == register.Email);
            if(regis != null)
            {
                return Ok(new ErrorRespon
                {
                    Error = false,
                    Message ="Fail"
                });
            }
            var user = new TbUser
            {
                Dob = register.Dob,
                Gender = register.Gender,
                Email = register.Email,
                Name = register.Name,
                Password = register.Password,
                RoleId = register.RoleId,
                UpdateDate = register.UpdateDate,
                CreateDate = register.CreateDate,
                Avatar = register.Avatar,
                Phone = register.Phone,
                Address = register.Address,
                ShopId = register.ShopId,

            };
            await _context.TbUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new ErrorRespon
            {
                Error = true,
                Message = "Success"
            });
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdate user)

        {
            var update = await _context.TbUsers.FindAsync(id);
            if (update != null)
            {
                update.Dob = user.Dob;
                update.Gender = user.Gender;
                update.Name = user.Name;
                update.CreateDate = user.CreateDate;
                update.UpdateDate = user.UpdateDate;
                update.Avatar = user.Avatar;
                update.Phone = user.Phone;
                update.Address = user.Address;
                await _context.SaveChangesAsync();
                return Ok(update);
            }
            return BadRequest("Faill ");
        }

        [HttpGet("AllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var user = _context.TbUsers.ToList();
            return Ok(user);
        }
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserByid(int id)
        {
            var user = await _context.TbUsers.FindAsync(id);
            if (user == null)
            {
                return Ok(new ErrorRespon
                {
                    Error = false,
                    Message = "No User :("
                });
            }
            return Ok(user);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Deleteacount(int Id)
        {
            var tokens = _context.TbTokens.Where(t => t.Id == Id).ToList();
            if (tokens == null)
            {
                return null;
            }
            // Xóa các bản ghi trong bảng tb_Token
            _context.TbTokens.RemoveRange(tokens);
            var user = await _context.TbUsers.FindAsync(Id);

            // Tiếp tục xóa bản ghi trong bảng tb_User
            if (user != null)
            {
                _context.TbUsers.Remove(user);
            }
            // Lưu thay đổi vào cơ sở dữ liệu
            _context.SaveChanges();

            return Ok();
        }
    }
}
