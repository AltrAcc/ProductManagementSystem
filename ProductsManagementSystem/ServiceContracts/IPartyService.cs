﻿using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Enums;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IPartyService
    {
        //PartyRequest?
        /// <summary>
        /// Add Party object to parties
        /// </summary>
        /// <param name="request">Party Request</param>
        /// <returns>Return party object after adding</returns>
        //Task<PartyResponse> AddParty(PartyRequest? request);
        PartyResponse AddParty(PartyRequest? request);
        List<PartyResponse> GetAllParties();

        PartyResponse GetPartyById(int? PartyID);

        public PartyResponse? GetPartyByName(string PartyName);

        PartyResponse UpdateParty(PartyUpdateRequest? partyUpdateRequest);

        bool DeleteParty(int? partyID);

        List<PartyResponse> GetFilteredParties(string searchBy, string? searchString);
        List<PartyResponse> GetSortedParties(List<PartyResponse> parties, string sortBy, SortOrderOptions sortOrder);
    }
}
