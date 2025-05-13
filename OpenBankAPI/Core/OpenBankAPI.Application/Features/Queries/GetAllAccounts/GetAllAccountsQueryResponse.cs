using System;
namespace OpenBankAPI.Application.Features.Queries.GetAllAccounts
{
	public class GetAllAccountsQueryResponse
	{
        public int TotalAccountCount { get; set; }
        public object Accounts { get; set; }
    }
}

