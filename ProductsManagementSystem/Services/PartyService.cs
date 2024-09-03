using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.RepositoryContracts;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Services
{
    public class PartyService : IPartyService
    {
        private readonly ApplicationDbContext _db;

        public PartyService(ApplicationDbContext db)
        {
            _db = db;
        }


        //private readonly IPartiesRepository _partiesRepositories;

        //public PartyService(IPartiesRepository partiesRepositories)
        //{
        //    _partiesRepositories = partiesRepositories;
        //}

        //public async Task<PartyResponse> AddParty(PartyRequest partyRequest)
        //{
        //    if(partyRequest == null) throw new ArgumentNullException(nameof(partyRequest));

        //    Party party = partyRequest.ToParty();

        //    party.PartyID = Guid.NewGuid();

        //    await _partiesRepositories.AddParty(party);

        //    return party.ToPartyResponse();

        //    //throw new NotImplementedException();
        //}

        public PartyResponse AddParty(PartyRequest? partyRequest)
        {
            if (partyRequest == null) throw new ArgumentNullException(nameof(partyRequest));

            if (partyRequest.PartyName == null) throw new ArgumentException(nameof(partyRequest.PartyName));


            //Convert object from partyRequest to Party type
            Party party = partyRequest.ToParty();

            //Generate PartyID
            party.PartyID = Guid.NewGuid();

            //Add party object to table
            _db.Add(party);
            _db.SaveChanges();

            return party.ToPartyResponse();
        }
    }
}
