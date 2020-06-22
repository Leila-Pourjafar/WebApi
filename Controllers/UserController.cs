using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Models.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserController(TodoContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        //public AccountController(IMapper mapper, UserManager<User> userManager)
        //{
        //    _mapper = mapper;
        //    _userManager = userManager;
        //}
        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserRegistrationModel(int page = 1, bool Active = true)
        {
            User user = new User();
            int RecordsPerPage = 10;
            //if (await _userManager.IsInRoleAsync(user, "Administrator"))
            //{
            return await _context.Users
           .OrderByDescending(x => x.Id)
           .Skip((page - 1) * RecordsPerPage)
           .Take(RecordsPerPage)
           .ToListAsync();
            //}


            //return null;

        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserRegistrationModel(int id)
        {
            var userRegistrationModel = await _context.Users.FindAsync(id);

            if (userRegistrationModel == null)
            {
                return NotFound();
            }

            return userRegistrationModel;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRegistrationModel(int id, UserRegistrationModel userRegistrationModel)
        {
            //if (id != userRegistrationModel.Id)
            //{
            //    return BadRequest();
            //}

            _context.Entry(userRegistrationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRegistrationModelExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserRegistrationModel>> PostUserRegistrationModel(UserRegistrationModel model)
        {


            /////////////
            if (!ModelState.IsValid)
            {
                // return View(userModel);
                //return NoContent();
                return BadRequest(ModelState);
            }

            // var user = _mapper.Map<User>(model);
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.UserName
            };
            //var user = new User { UserName = model.UserName, Email = model.UserName };
            //var result = await UserManager.CreateAsync(user, model.Password);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                // return (userModel);
                return BadRequest(ModelState.ToString());
            }

            await _userManager.AddToRoleAsync(user, "User");
            // await _userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
         //   var newRefreshToken = _userManager.GenerateNewAuthenticatorKey();
           // await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", newRefreshToken);

            await _userManager.RemoveAuthenticationTokenAsync(user, "MyApp", "RefreshToken");
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, "MyApp", "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user, "MyApp", "RefreshToken", newRefreshToken);

            model.Token = newRefreshToken;
            ////////////////////////
            //_context.UserRegistrationModel.Add(userRegistrationModel);
            //await _context.SaveChangesAsync();



            return CreatedAtAction("GetUserRegistrationModel", new { id = user.Id }, model);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserRegistrationModel(int id)
        {
            var userRegistrationModel = await _context.Users.FindAsync(id);
            if (userRegistrationModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userRegistrationModel);
            await _context.SaveChangesAsync();

            return userRegistrationModel;
        }

        private bool UserRegistrationModelExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
