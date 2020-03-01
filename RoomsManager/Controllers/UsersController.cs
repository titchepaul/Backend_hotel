using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RoomsManager.JWT;
using RoomsManager.Models;
using RoomsManager.MonServives;

namespace RoomsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class UsersController : ControllerBase
    {
        private readonly DefaultContext _context;
        private readonly IOptions<JWTParam> _appSettings;
        private readonly UsersService _userService;

        public UsersController(DefaultContext context, IOptions<JWTParam> appSettings)
        {
            _context = context;
            this._appSettings = appSettings;
            this._userService = new UsersServiceForToken(context, _appSettings);
        }

        // GET: api/Users
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] string id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.UserEmail)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers( Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.UserEmail }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.UserEmail == id);
        }
        /// <summary>
        /// https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api#users-controller-cs
        /// </summary>
        /// <param name="userParam"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Authenticate(Users userParam)
        {
            var user = _userService.login(userParam.UserEmail, userParam.Password); ;

            if (user == null)
                return BadRequest(new { message = "Nom d'utilisateur ou mot de passe incorrect" });
            Users userSend = user;
            userSend.Password = null;
            return Ok(userSend);
        }
        [HttpPut("update")]
        public async Task<IActionResult> PutUsers([FromBody] Users users)
        {

            try
            {
                var str = await _context.Users.FindAsync(users.UserEmail);

                str.UserEmail = users.UserEmail;
                str.Password = users.Password;
                str.UserRole = users.UserRole;
                //searchRooms.Enfants = chambre.Enfants;
                //searchRooms.Adulte = chambre.Adulte;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}