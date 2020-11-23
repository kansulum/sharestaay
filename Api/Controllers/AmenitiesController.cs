using System.Linq;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AmenitiesController : BaseApiController
    {
        private readonly RoomContext _context;
        public AmenitiesController(RoomContext context)
        {
            _context = context;

        }

        [HttpGet]
        [Authorize]
        public ActionResult Amenities()
        {
            return Ok(_context.Amenities.ToList());
        }
    }
}