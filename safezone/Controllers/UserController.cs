using Microsoft.AspNetCore.Mvc;
using safezone.application.DTOs.User;
using safezone.application.Interfaces;
using safezone.domain.Entities;

namespace safezone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null) return NotFound($"User with ID {id} not found.");
            return Ok(user);

        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody]UserDTO dto)
        {
            var user = new User
            {
                Email = dto.Email,
                Password = dto.Password, // Ensure password is hashed in the repository
            };


            if (user == null) return BadRequest("User cannot be null.");
            await _userRepository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]UserDTO dto)
        {

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound($"User with ID {id} not found.");

            user.Email = dto.Email;
            user.Password = dto.Password; // Ensure password is hashed in the repository

            await _userRepository.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound($"User with ID {id} not found.");
            await _userRepository.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("validate")]
        public async Task<ActionResult<bool>> ValidateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return BadRequest("Email and password cannot be empty.");
            var isValid = await _userRepository.ValidateUserAsync(email, password);
            return Ok(isValid);


        }
    }
}
