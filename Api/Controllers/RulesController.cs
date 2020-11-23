using System.Linq;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class RulesController : BaseApiController
    {
        private readonly RoomContext _context;
        public RulesController(RoomContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult Rules(){
            return Ok(_context.Rules.ToList());
        }
    }
}