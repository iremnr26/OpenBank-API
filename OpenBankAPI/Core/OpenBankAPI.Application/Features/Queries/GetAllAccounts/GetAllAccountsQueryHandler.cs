using System;
using System.Security.Claims;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenBankAPI.Application.Repositories;

namespace OpenBankAPI.Application.Features.Queries.GetAllAccounts
{
	public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQueryRequest, GetAllAccountsQueryResponse>
    {
        readonly IAccountReadRepository _accountReadRepository;

        readonly ILogger<GetAllAccountsQueryHandler> _logger;

        readonly IHttpContextAccessor _httpContextAccessor;


        public GetAllAccountsQueryHandler(IAccountReadRepository accountReadRepository, ILogger<GetAllAccountsQueryHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _accountReadRepository = accountReadRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

       
        public Task<GetAllAccountsQueryResponse> Handle(GetAllAccountsQueryRequest request, CancellationToken cancellationToken)
        {

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
            _logger.LogInformation("🔥 User ID: {UserId}", userId);

            var allClaims = _httpContextAccessor.HttpContext?.User?.Claims;
            foreach (var c in allClaims)
            {
                _logger.LogInformation($"CLAIM: {c.Type} = {c.Value}");
            }
  
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                _logger.LogWarning("❗ Kullanıcı ID’si alınamadı veya parse edilemedi: {UserId}", userId);
                return Task.FromResult<GetAllAccountsQueryResponse>(null);
            }

            _logger.LogInformation("Get accounts of user {UserId}", parsedUserId);

            var query = _accountReadRepository.GetAll(false)
                .Where(a => a.UserGuidId == parsedUserId); // 👈 sadece login olan kullanıcının hesapları

            var totalAccountCount = query.Count();
            int skipValue = Math.Max(0, (request.Page - 1) * request.Size);

            var accounts = query
                .OrderBy(p => p.Id)
                .Skip(skipValue)
                .Take(request.Size)
                .Select(p => new
                {
                    p.Id,
                    p.UserId,
                    p.AccountNumber,
                    p.BankId,
                    p.Balance,
                    p.Currency
                })
                .ToList();

            var response = new GetAllAccountsQueryResponse
            {
                TotalAccountCount = totalAccountCount,
                Accounts = accounts
            };

            return Task.FromResult(response);

        }
       
       /* public Task<GetAllAccountsQueryResponse> Handle(GetAllAccountsQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

            _logger.LogInformation("Get accounts of user {UserId}", userId);

            var totalAccountCount = _accountReadRepository.GetAll(false).Count();
            int skipValue = Math.Max(0, (request.Page - 1) * request.Size);

            var accounts = _accountReadRepository.GetAll(false)
                .OrderBy(p => p.Id) // ✅ Sıralama eklemeden Skip/Take kullanma!
                .Skip(skipValue)
                .Take(request.Size)
                .Select(p => new
                {
                    p.Id,
                    p.UserId,
                    p.AccountNumber,
                    p.BankId,
                    p.Balance,
                    p.Currency
                })
                .ToList();

            var response = new GetAllAccountsQueryResponse
            {
                TotalAccountCount = totalAccountCount,
                Accounts = accounts
            };

            return Task.FromResult(response);
        }*/
        
    }
}

