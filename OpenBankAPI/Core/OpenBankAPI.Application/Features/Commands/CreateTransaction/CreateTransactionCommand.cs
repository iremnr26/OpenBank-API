using System;
using MediatR;

namespace OpenBankAPI.Application.Features.Commands.CreateTransaction
{
	public class CreateTransactionCommand : IRequest<CreateTransactionCommandResponse>
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

    }
}

