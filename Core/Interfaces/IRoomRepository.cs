using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<IReadOnlyList<Room>> GetRoomsAsync();
        Task<Room> GetRoomAsync(int id);

        List<Room> GetFavourites(string userId);
    }
}