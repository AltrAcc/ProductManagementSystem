using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IPartyAdderService
    {
        //PartyRequest?
        Task<PartyResponse> AddParty(PartyRequest request);
    }
}
