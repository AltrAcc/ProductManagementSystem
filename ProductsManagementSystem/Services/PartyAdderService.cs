using ProductManagementSystem.Models;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.RepositoryContracts;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Services
{
    public class PartyAdderService : IPartyAdderService
    {
        private readonly IPartiesRepository _partiesRepositories;

        public PartyAdderService(IPartiesRepository partiesRepositories)
        {
            _partiesRepositories = partiesRepositories;
        }

        public async Task<PartyResponse> AddParty(PartyRequest partyRequest)
        {
            if(partyRequest == null) throw new ArgumentNullException(nameof(partyRequest));

            Party party = partyRequest.ToParty();

            party.PartyID = Guid.NewGuid();

            await _partiesRepositories.AddParty(party);

            return party.ToPartyResponse();

            //throw new NotImplementedException();
        }
    }
}
