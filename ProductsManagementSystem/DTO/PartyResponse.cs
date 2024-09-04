using ProductManagementSystem.Models;
using System;

namespace ProductsManagementSystem.DTO
{
    public class PartyResponse
    {
        public Guid PartyID { get; set; }

        public string PartyName { get; set; }

        public string PartyCategory { get; set; }


        public override string ToString()
        {
            return $"Party ID: {PartyID}, Party Name: {PartyName}, Party Category: {PartyCategory}";
        }

        public PartyUpdateRequest ToPartyUpdateRequest()
        {
            return new PartyUpdateRequest()
            {
                PartyID = PartyID,
                PartyName = PartyName,
                PartyCategory = PartyCategory
            };
        }

    }

    public static class PartyExtensions
    {
        public static PartyResponse ToPartyResponse(this Party party)
        {
            //party => convert => PersonResponse
            return new PartyResponse()
            {
                PartyID = party.PartyID,
                PartyName = party.PartyName,
               PartyCategory = party.PartyCategory
            };
        }
    }
}
