using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IPartyService
    {
        //PartyRequest?
        /// <summary>
        /// Add Party object to parties
        /// </summary>
        /// <param name="request">Party Request</param>
        /// <returns>Return party object after adding it</returns>
        //Task<PartyResponse> AddParty(PartyRequest? request);
        PartyResponse AddParty(PartyRequest? request);
        List<PartyResponse> GetAllParties();
    }
}
