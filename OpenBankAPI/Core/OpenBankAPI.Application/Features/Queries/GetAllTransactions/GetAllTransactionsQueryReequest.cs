using System;
using MediatR;

namespace OpenBankAPI.Application.Features.Queries.GetAllTransactions
{
    public class GetAllTransactionsQueryRequest : IRequest<GetAllTransactionsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}

