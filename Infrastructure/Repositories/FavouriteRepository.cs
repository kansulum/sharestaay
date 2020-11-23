using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class FavouriteRepository : IFavouriteRepository
    {
        private readonly RoomContext _context;
        public FavouriteRepository(RoomContext context)
        {
            _context = context;
        }
        public void Add(Favourite favourite)
        {
            _context.Favourites.Add(favourite);
            _context.SaveChanges();
        }

        public void Delete(Favourite favourite)
        {
            _context.Favourites.Remove(favourite);
        }

        public bool Any(int roomId, string userId)
        {
            return _context.Favourites.Any(a => a.AppUserId == userId && a.RoomId == roomId);
        }

        public Favourite GetFavourite(int roomId, string userId)
        {
            return _context.Favourites.SingleOrDefault(f => f.AppUserId == userId && f.RoomId == roomId);
        }
    }
}