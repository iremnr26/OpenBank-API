using System;
using MediatR;
using OpenBankAPI.Application.RequestParameters;

namespace OpenBankAPI.Application.Features.Queries.GetAllAccounts
{
	public class GetAllAccountsQueryRequest : IRequest<GetAllAccountsQueryResponse>
    {
        //public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}

