using System.Security.Claims;
using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class FaviouritesController:BaseApiController
    {
        
        private readonly IMapper mapper;
        private readonly IFavouriteRepository repository;
        private readonly IRoomRepository roomRepository;
        private readonly IUserRepository userRepository;
        private readonly RoomContext _context;
        private readonly UserManager<AppUser> userManager;
        public FaviouritesController(IMapper mapper,IFavouriteRepository repository,
        IRoomRepository roomRepository,
        IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.roomRepository = roomRepository;
            this.userRepository = userRepository;

          
        }
       
        [HttpPost]
        public IActionResult Post([FromBody]DtoFavourite saveFavourite)
        {           
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            var userId = userRepository.GetUserId(userEmail);
            if (repository.Any(saveFavourite.RoomId,userId))
                return BadRequest("The favourite already exists.");

            var favourite = new Favourite
            {
                AppUserId = userId,
                RoomId = saveFavourite.RoomId
            };
            repository.Add(favourite);      
            return Ok(favourite);

        }


        [HttpDelete]
        public IActionResult Delete(DtoFavourite saveFavourite)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = userRepository.GetUserId(userEmail);

            if (!repository.Any(saveFavourite.RoomId, userId))
                return BadRequest("The favourite does not exist");

            repository.Delete(repository.GetFavourite(saveFavourite.RoomId,userId));
            return Ok();
         }

        [HttpGet]
        public IActionResult Get()
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = userRepository.GetUserId(userEmail);
            var favoutites = roomRepository.GetFavourites(userId);
            return Ok(favoutites);
        }
    }
}