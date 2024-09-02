using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.RepositoryContracts;

namespace ProductsManagementSystem.Repositories
{
    public class PartiesRepository : IPartiesRepository
    {
        private readonly ApplicationDbContext _db;

        public PartiesRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Party> AddParty(Party party)
        {
            _db.Parties.Add(party);
            await _db.SaveChangesAsync();

            return party;
            //throw new NotImplementedException();
        }
    }
}
