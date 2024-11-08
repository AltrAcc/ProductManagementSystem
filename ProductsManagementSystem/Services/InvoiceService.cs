﻿using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _db;
        private readonly IPartyService _partyService;
        public InvoiceService(ApplicationDbContext db, IPartyService partyService)
        {
            _db = db;
            _partyService = partyService;
        }

        public InvoiceResponse AddInvoice(IEnumerable<InvoiceRequest> invoiceRequest, int partyId)
        {
            if (invoiceRequest == null)
            {
                throw new ArgumentNullException(nameof(invoiceRequest));
            }

            decimal TotalPrice = invoiceRequest.Select(x => x.Price * x.Quantity).Sum();

            Console.WriteLine(TotalPrice);

            Invoice invoice = new Invoice()
            {
                PartyId = partyId,
                InvoiceDate = DateTime.Now,
            };

            _db.Invoices.Add(invoice);
            _db.SaveChanges();

            foreach (var item in invoiceRequest)
            {
                InvoiceDetails invoiceDetails = new InvoiceDetails()
                {
                    InvoiceId = invoice.InvoiceId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = (int)item.Price,
                };
                _db.InvoicesDetails.Add(invoiceDetails);
            }
            _db.SaveChanges();

            return new InvoiceResponse()
            {
                PartyId = partyId,
                InvoiceId = invoice.InvoiceId,
                ProductCount = invoiceRequest.Count(),
                Total = TotalPrice,
            };
        }

        public bool DeleteInvoice(int invoiceId)
        {
            var invoice = _db.Invoices.Find(invoiceId);
            if (invoice == null)
            {
                throw new InvalidOperationException("Invoice Not Found");
            }

            // Remove Invoice as well from InvoiceDetail
            var invoiceDetails = _db.InvoicesDetails.Where(inv => inv.InvoiceId == invoiceId).ToList();
            _db.InvoicesDetails.RemoveRange(invoiceDetails);

            _db.Invoices.Remove(invoice);

            _db.SaveChanges();

            return true;
        }

        public IEnumerable<InvoiceResponse> GetAllInvoice()
        {
            var invoiceSummaries = _db.Invoices
                .Select(invoice => new InvoiceResponse
                {
                    InvoiceId = invoice.InvoiceId,
                    PartyId = invoice.PartyId,
                    PartyName = _db.Parties.Where(p => p.PartyID == invoice.PartyId).FirstOrDefault().PartyName,
                    ProductCount = invoice.InvoiceDetails.Count(),
                    Total = invoice.InvoiceDetails.Sum(detail => detail.Quantity * detail.Price)
                })
                .ToList();

            return invoiceSummaries;
        }

        public InvoiceResponse GetInvoiceByInvoiceId(int invoiceId)
        {
            var invoice = _db.Invoices.Find(invoiceId);
            if (invoice == null)
            {
                throw new InvalidOperationException("Invoice Not Found");
            }

            InvoiceDetails? invoiceDetails = _db.InvoicesDetails.Where(p => p.InvoiceId == invoiceId).FirstOrDefault();

            return invoice.ToInvoiceResponse(invoiceDetails);
        }

        public IEnumerable<InvoiceResponse> GetInvoiceByPartyId(int partyId)
        {
            var partyInvoice = _db.Invoices.Where(invoice => invoice.PartyId == partyId)
                .Select(invoice => new InvoiceResponse
                {
                    InvoiceId = invoice.InvoiceId,
                    PartyId = invoice.PartyId,
                    PartyName = _db.Parties.Where(p => p.PartyID == invoice.PartyId).FirstOrDefault().PartyName,
                    ProductCount = invoice.InvoiceDetails.Count(),
                    Total = invoice.InvoiceDetails.Sum(detail => detail.Quantity * detail.Price),
                    InvoiceDate = invoice.InvoiceDate,

                }).ToList();

            return partyInvoice;
        }

        public InvoiceViewModel GetInvoiceDetailsByInvoiceId(int invoiceId)
        {
            try
            {
                var Invoice = _db.Invoices.Find(invoiceId);
            }
            catch (Exception error)
            {
                throw error;
            }

            InvoiceViewModel InvoiceDetail = _db.Invoices.Where(invoice => invoice.InvoiceId == invoiceId).Select(invoice => new InvoiceViewModel
            {
                InvoiceId = invoice.InvoiceId,
                PartyId = invoice.PartyId,
                PartyName = _db.Parties.Where(party => party.PartyID == invoice.PartyId).FirstOrDefault().PartyName,
                InvoiceDate = invoice.InvoiceDate,
                invoiceItems = invoice.InvoiceDetails.Select(item => new InvoiceItems
                {
                    ProductId = item.ProductId,
                    ProductName = _db.Products.Where(invoice => invoice.ProductID == item.ProductId).FirstOrDefault().ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                })
            }).First();

            return InvoiceDetail;

        }
    }
}
