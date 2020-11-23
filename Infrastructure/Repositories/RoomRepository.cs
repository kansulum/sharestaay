using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RoomContext _context;
        public RoomRepository(RoomContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomAsync(int id)
        {
          return await _context.Rooms.FindAsync(id);
        }

        public async Task<IReadOnlyList<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public List<Room> GetFavourites(string userId)
        {

            return _context.Favourites
               .Where(f => f.AppUserId == userId)
               .Select(f => f.Room)
               .ToList();

        }   
    }
}