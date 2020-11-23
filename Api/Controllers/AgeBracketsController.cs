using System.Linq;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AgeBracketsController : BaseApiController
    {
        private readonly RoomContext _context;
        public AgeBracketsController(RoomContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult AgeBrackets(){
            return Ok(_context.AgeBrackets.ToList());
        }
    }
}