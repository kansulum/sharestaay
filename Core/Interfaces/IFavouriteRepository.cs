using Core.Entities;

namespace Core.Interfaces
{
    public interface IFavouriteRepository
    {
        void Add(Favourite favourite);

        void Delete(Favourite favourite);

        bool Any(int roomId, string userId);

        Favourite GetFavourite(int roomId, string userId);
    }
}