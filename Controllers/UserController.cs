using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPromo.Data;
using UserPromo.DTOs;
using UserPromo.Models;
using UserPromo.Services;

namespace UserPromo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserController(AppDbContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserDTO>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //Disparar e-mail de boas-vindas via Azure Functions
            await _emailService.SendWelcomeEmailAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, _mapper.Map<UserDTO>(user));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UserDTO userDTO)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null) 
                return NotFound("User not found.");
            _mapper.Map(userDTO, user);
            await _context.SaveChangesAsync();

            //Disparar e-mail de edição via Azure Functions
            await _emailService.SendEditEmailAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound("User not found.");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
