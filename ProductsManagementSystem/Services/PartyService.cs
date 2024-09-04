using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Enums;
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

        public PartyResponse? GetPartyById(Guid? PartyID)
        {
            if (PartyID == null)
                return null;

            Party? party = _db.Parties
                .FirstOrDefault(temp => temp.PartyID == PartyID);
            if (party == null)
                return null;

            return ConvertPartyToPartyResponse(party);
        }

        public PartyResponse UpdateParty(PartyUpdateRequest? partyUpdateRequest)
        {
            Party? party = _db.Parties.Find(partyUpdateRequest?.PartyID);

            if (party == null)
            {
                throw new KeyNotFoundException("Party not found");
            }

            //update all details
            party.PartyName = partyUpdateRequest.PartyName;
            party.PartyCategory = partyUpdateRequest.PartyCategory;

            _db.SaveChanges();

            return party.ToPartyResponse();
        }

        public bool DeleteParty(Guid? partyID)
        {
            if (partyID == null)
            {
                throw new ArgumentNullException(nameof(partyID));
            }

            Party? person = _db.Parties.FirstOrDefault(temp => temp.PartyID == partyID);
            
            if (person == null)
                return false;

            _db.Parties
                .Remove(_db.Parties.First(temp => temp.PartyID == partyID));
            _db.SaveChanges(); 

            return true;
        }

        private PartyResponse ConvertPartyToPartyResponse(Party party)
        {
            PartyResponse partyResponse = party.ToPartyResponse();
            return partyResponse;
        }

        public List<PartyResponse> GetFilteredParties(string searchBy, string? searchString)
        {
            List<PartyResponse> parties = GetAllParties();
            List<PartyResponse> matchingParties = parties;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
                return matchingParties;


            switch (searchBy)
            {
                case nameof(PartyResponse.PartyName):
                    matchingParties = parties.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PartyName) ?
                    temp.PartyName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(PartyResponse.PartyCategory):
                    matchingParties = parties.Where(temp =>
                    (!string.IsNullOrEmpty(temp.PartyCategory) ?
                    temp.PartyCategory.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: matchingParties = parties; break;
            }
            return matchingParties;
        }

        public List<PartyResponse> GetSortedParties(List<PartyResponse> parties, string sortBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return parties;

            List<PartyResponse> sortedParties = (sortBy, sortOrder) switch
            {
                (nameof(PartyResponse.PartyName), SortOrderOptions.ASC) => parties.OrderBy(temp => temp.PartyName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PartyResponse.PartyName), SortOrderOptions.DESC) => parties.OrderByDescending(temp => temp.PartyName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PartyResponse.PartyCategory), SortOrderOptions.ASC) => parties.OrderBy(temp => temp.PartyCategory, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PartyResponse.PartyCategory), SortOrderOptions.DESC) => parties.OrderByDescending(temp => temp.PartyCategory, StringComparer.OrdinalIgnoreCase).ToList(),


                _ => parties
            };

            return sortedParties;
        }
    }
 }

