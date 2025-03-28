﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.DTO
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string LegalName { get; set; }
        public string TradeName { get; set; }
        public string TaxIndentNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string Website { get; set; }
        public string PhysicalAddress { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public float AnnualRevenue { get; set; }
        public string LastEdited { get; set; }
        public bool SupplierActive { get; set; }
    }
}
