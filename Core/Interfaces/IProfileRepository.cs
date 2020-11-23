using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProfileRepository
    {
    Task AddAsync(Profile profile);
    }
}