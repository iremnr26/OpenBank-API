using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml;
using OpenBankAPI.Domain.Entities.Common;
namespace Domain.Entities
{
    public class Transaction : BaseEntity

    {
        public int FromAccountId { get; set; } // Gönderen hesabın ID'si

        public int ToAccountId { get; set; } // Alıcı hesabın ID'si

        public decimal Amount { get; set; } // Transfer edilen miktar

        public string Description { get; set; } // İşlem açıklaması 

        public DateTime TransactionDate { get; set; } // Transfer tarihi

    }

}

