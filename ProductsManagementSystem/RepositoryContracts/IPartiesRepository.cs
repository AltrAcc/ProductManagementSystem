using ProductManagementSystem.Models;

namespace ProductsManagementSystem.RepositoryContracts
{
    public interface IPartiesRepository
    {
        Task<Party> AddParty(Party party);
    }
}
