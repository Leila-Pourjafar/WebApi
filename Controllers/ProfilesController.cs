using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

       

        public ProfilesController(TodoContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetProfile()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            var profile = await _context.Users.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }
            return NotFound();
            // return profile;
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public async Task<ActionResult<Profile>> PutProfile(Profile profile)
        {
            var result = await _signInManager.PasswordSignInAsync(profile.Username, profile.Password, false, false);
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(profile.Username);
                if (user != null)
                {
                    user.Address = profile.Address;
                    user.BirthDate = profile.BirthDate;
                    user.City = profile.City;
                    user.Province = profile.Province;
                    user.ProfileImage = profile.ProfileImage;
                    _context.Entry(user).State = EntityState.Modified;
                }

                try
                {
                    await _context.SaveChangesAsync();
                    return Ok(profile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ProfileExists(id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                    return BadRequest(profile);
                }

            }
            //if (id != profile.Id)
            //{
            //    return BadRequest();
            //}

            
            return NoContent();
        }

        // POST: api/Profiles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
       
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
          //  _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }

        // DELETE: api/Profiles/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Profile>> DeleteProfile(int id)
        //{
        //   //// var profile = await _context.Profile.FindAsync(id);
        //   // if (profile == null)
        //   // {
        //   //     return NotFound();
        //   // }

        //   //// _context.Profile.Remove(profile);
        //   // await _context.SaveChangesAsync();

        //   // return profile;
        //}

        //private bool ProfileExists(int id)
        //{
        //    return _context.Profile.Any(e => e.Id == id);
        //}
    }
}
