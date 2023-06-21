using BirdPlatForm.UserRespon;
using BirdPlatForm.ViewModel;
using BirdPlatFormEcommerce.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdPlatFormEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "AD")]
    public class AdminController : ControllerBase
    {
        private readonly SwpContext _context;

        public AdminController(SwpContext swp) {
            _context = swp;
        }
        [HttpGet]
        public  async  Task<IActionResult> getAlluser()
        {
            var user = _context.TbUsers.ToList();
            return Ok(user);
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
