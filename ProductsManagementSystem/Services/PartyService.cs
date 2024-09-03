using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.RepositoryContracts;
using ProductsManagementSystem.ServiceContracts;
using System;
using System.IO;

namespace ProductsManagementSystem.Services
{
    public class PartyService : IPartyService
    {
        private readonly ApplicationDbContext _db;

        public PartyService(ApplicationDbContext db)
        {
            _db = db;
        }

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

        public List<PartyResponse> GetAllParties()
        {
            return _db.Parties.ToList()
                .Select(temp => ConvertPartyToPartyResponse(temp)).ToList();
        }

        private PartyResponse ConvertPartyToPartyResponse(Party party)
        {
            PartyResponse partyResponse = party.ToPartyResponse();
            return partyResponse;
        }
    }
}
