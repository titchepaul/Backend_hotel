using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomsManager;
using RoomsManager.Models;

namespace RoomsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private readonly DefaultContext _context;

        public RoomsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public IEnumerable<Rooms> GetRooms()
        {
            return _context.Rooms;
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRooms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rooms = await _context.Rooms.FindAsync(id);

            if (rooms == null)
            {
                return NotFound();
            }

            return Ok(rooms);
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRooms([FromRoute] int id, [FromBody] Rooms rooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rooms.RoomsId)
            {
                return BadRequest();
            }

            _context.Entry(rooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomsExists(id))
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

        // POST: api/Rooms
        [HttpPost]
        public async Task<IActionResult> PostRooms([FromBody] Rooms rooms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Rooms.Add(rooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRooms", new { id = rooms.RoomsId }, rooms);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRooms([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();

            return Ok(rooms);
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomsId == id);
        }
        /*[HttpGet("{roomsStatus}")]
        public async Task<ActionResult<IEnumerable<Rooms>>> roomsRes(int Prix)
        {
            /*var user = _userService.login(userParam.UserEmail, userParam.Password); ;

            if (user == null)
                return BadRequest(new { message = "Nom d'utilisateur ou mot de passe incorrect" });
            Users userSend = user;
            userSend.Password = null;
            //retrun awaitreturn _context.Rooms;
            //return Ok(userSend);
            //var str = await _context.Rooms.FindAsync(Prix);
            //return str;
            return await _context.Rooms.Where(x => x.Prix.Equals(Prix)).Select(x => x).ToListAsync();
        } */
        [HttpGet("stat/{prix}")]
        public async Task<IActionResult> GetRoomsPrix([FromRoute] int prix)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rooms = await _context.Rooms.FindAsync(prix);

            if (rooms == null)
            {
                return NotFound();
            }

            return Ok(rooms);

        }

        [HttpPut("update")]
        public async Task<IActionResult> PutRooms([FromBody] Rooms chambre)
        {

            try
            {            
                var searchRooms = await _context.Rooms.FindAsync(chambre.RoomsId);

                searchRooms.UserEmail = chambre.UserEmail;
                searchRooms.Status = chambre.Status;
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
        [HttpPut("paye")]
        public async Task<IActionResult> PayeRooms([FromBody] Rooms chambre)
        {

            try
            {
                var searchRooms = await _context.Rooms.FindAsync(chambre.UserEmail);

                // searchRooms.UserEmail = chambre.UserEmail;
                searchRooms.Status = chambre.Status;
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