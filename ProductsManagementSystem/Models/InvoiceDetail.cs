﻿namespace ProductManagementSystem.Models
{
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Invoice Invoice { get; set; }

        public Product Product { get; set; }

    }

}

