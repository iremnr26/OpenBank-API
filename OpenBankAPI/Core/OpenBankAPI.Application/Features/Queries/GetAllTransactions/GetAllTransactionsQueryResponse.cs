using System;
namespace OpenBankAPI.Application.Features.Queries.GetAllTransactions
{
    public class GetAllTransactionsQueryResponse
    {
        public int TotalTransactionCount { get; set; }
        public object Transactions { get; set; }
    }

}

