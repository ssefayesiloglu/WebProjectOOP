using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectOOP.DataAccess;
using WebProjectOOP.Entities;
using WebProjectOOP.Entities.Dtos;

namespace WebProjectOOP.Controllers
{
    public class AuthController : Controller
    {
        private readonly ToDoContext _context;

            public AuthController(ToDoContext context)
        {
            _context = context;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserTask>> Register(UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new UserTask
            {
                UserName = request.UserName,
                PasswordHash = passwordHash,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user == null)

                return BadRequest("Kullanıcı Bulunamadı.");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Hatalı Şifre");
            }
            return Ok("Giriş Başarılı.");


        }


    }
}
