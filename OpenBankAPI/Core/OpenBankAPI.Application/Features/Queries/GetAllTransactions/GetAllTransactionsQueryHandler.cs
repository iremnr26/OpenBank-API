using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenBankAPI.Application.Repositories;

namespace OpenBankAPI.Application.Features.Queries.GetAllTransactions
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQueryRequest, GetAllTransactionsQueryResponse>
    {
        private readonly ITransactionReadRepository _transactionReadRepository;
        private readonly ITransactionWriteRepository _transactionWriteRepository;

        private readonly ILogger<GetAllTransactionsQueryHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly IAccountReadRepository _accountReadRepository;
        public GetAllTransactionsQueryHandler(
            ITransactionReadRepository transactionReadRepository,
            ITransactionWriteRepository transactionWriteRepository,
            ILogger<GetAllTransactionsQueryHandler> logger,
            IHttpContextAccessor httpContextAccessor,
            IAccountReadRepository accountReadRepository)
        {
            _transactionReadRepository = transactionReadRepository;
            _transactionWriteRepository = transactionWriteRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _accountReadRepository = accountReadRepository;
        }

        public Task<GetAllTransactionsQueryResponse> Handle(GetAllTransactionsQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
            _logger.LogInformation("🔐 User ID: {UserId}", userId);

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                _logger.LogWarning("❗ Kullanıcı ID alınamadı veya parse edilemedi.");
                return Task.FromResult<GetAllTransactionsQueryResponse>(null);
            }

            _logger.LogInformation("📤 Fetching transactions for user {UserId}", parsedUserId);

            // 1. Kullanıcının hesap ID’lerini çek
            var userAccountIds = _accountReadRepository.GetAll(false)
                .Where(a => a.UserGuidId == parsedUserId)
                .Select(a => a.Id)
                .ToList();

            // 2. Bu ID'lere ait transaction'ları filtrele
            var query = _transactionReadRepository.GetAll(false)
                .Where(t => userAccountIds.Contains(t.FromAccountId) || userAccountIds.Contains(t.ToAccountId));
            int totalCount = query.Count();
            int skipValue = Math.Max(0, (request.Page - 1) * request.Size);

            var transactions = query
                .OrderByDescending(t => t.TransactionDate)
                .Skip(skipValue)
                .Take(request.Size)
                .Select(t => new
                {
                    t.Id,
                    t.FromAccountId,
                    t.ToAccountId,
                    t.Amount,
                    t.Description,
                    t.TransactionDate
                })
                .ToList();

            var response = new GetAllTransactionsQueryResponse
            {
                TotalTransactionCount = totalCount,
                Transactions = transactions
            };

            return Task.FromResult(response);
        }
    }
}

