using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Extentions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class RoommatesController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RoommatesController(UserManager<AppUser> userManger, IMapper mapper, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManger = userManger;

        }

        [HttpGet]
        public async Task<IReadOnlyList<RoommateDto>> Roommates()
        {
            var roommateDto =
             _mapper.Map<IReadOnlyList<AppUser>, IReadOnlyList<RoommateDto>>(await _userManger.Users.ToListAsync());

            return roommateDto;
        }

        [HttpGet("onlineFriends")]
        [Authorize]
        public async Task<IReadOnlyList<Core.Entities.Profile>> OnlineFriends()
        {
            var userEmail = HttpContext.User.RetrieveEmailFromPrincipal();
            var userId = _userRepository.GetUserId(userEmail);
            var onlineFriends = _userRepository.GetOnlineFriends(userId);

            var ojbModel = onlineFriends.Select(m => new Core.Entities.Profile()
            {
                UserEmail = m.UserId

            }).ToList();

            return ojbModel;
        }
    }
}