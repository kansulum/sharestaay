using System.Linq;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class GenderController : BaseApiController
    {
        private readonly RoomContext _context;
        public GenderController(RoomContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Genders(){
         return Ok(_context.Genders.ToList());
        }
    }
}