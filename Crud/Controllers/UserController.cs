using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserCrud.Models;
using UserCrud.Services;

namespace UserCrud.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> getUsers()
        {
            return Ok(_userService.GetAll());
            
        }


        [HttpGet("{id}")]
        public ActionResult<User> GetUser(long id)
        {
            User user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("{id}")]
        public IActionResult PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if(!_userService.UserExists(id))
            {
                return NotFound("User was not found");
            }

            _userService.Put(user, id);
            

            return NoContent();
        }


        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            User savedUser = _userService.Save(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, savedUser);
        }

 
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(long id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound("User was not found");
            }
            _userService.Delete(user);
            return Ok(user);
        }
    }
}
