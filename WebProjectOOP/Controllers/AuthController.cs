using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProjectOOP.DataAccess;
using WebProjectOOP.Entities;
using WebProjectOOP.Entities.Dtos;

namespace WebProjectOOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ToDoContext _context;

            public AuthController(ToDoContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            //  Mühür Kontrolü: Bu kullanıcı adı veritabanında zaten var mı?
            var userExists = await _context.Users.AnyAsync(u => u.UserName == request.UserName);

            if (userExists)
            {
                // Eğer varsa, SQL'e hiç gitmeden hata döndür 
                return BadRequest("Bu kullanıcı adı zaten mühürlenmiş, başka bir tane dene.");
            }

            //  Kullanıcı yoksa normal kayıt işlemlerine devam et
            var user = new UserTask
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Kayıt başarılı kanka!");
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user == null) return BadRequest("Kullanıcı Bulunamadı.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Hatalı Şifre");
            }

            // React'in beklediği 'token' objesini gönderiyoruz
            // AuthController.cs - Login Metodu Sonu
            return Ok(new
            {
                Token = "gecici_token_basarili", // Buraya  gerçek tokenString gelecek
                UserId = user.Id,               // React bu ID'yi bekliyor
                UserName = user.UserName
            });
        }


    }
}
