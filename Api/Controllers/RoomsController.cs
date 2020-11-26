using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Extentions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly RoomContext _context;
        private readonly IGenericRepository<Room> _roomRepository;
        private readonly IMapper _mapper;
        public RoomsController(RoomContext context, IGenericRepository<Room> roomRepository, IMapper mapper)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IReadOnlyList<Room>> Rooms([FromQuery] RoomSpecParams roomParams)
        {
            return await _roomRepository.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Room(int id)
        {
            return await _roomRepository.GetByIDAsync(id);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(RoomCreateDto dto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            dto.AppUserEmail = email;
            
            var room = _mapper.Map<RoomCreateDto,Room>(dto);
            _roomRepository.Add(room);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoom(int id,RoomCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var room = await _roomRepository.GetByIDAsync(id);

            if (room == null)
                return NotFound();    

            var roomUpdate = _mapper.Map<RoomCreateDto,Room>(dto,room);           
             _roomRepository.Update(roomUpdate);
             await _context.SaveChangesAsync();

            return Ok(roomUpdate);
        }


    }
}
