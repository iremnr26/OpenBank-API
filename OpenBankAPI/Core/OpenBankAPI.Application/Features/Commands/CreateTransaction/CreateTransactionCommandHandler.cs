using System;
using System.Security.Claims;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenBankAPI.Application.Features.Queries.GetAllAccounts;
using OpenBankAPI.Application.Repositories;

namespace OpenBankAPI.Application.Features.Commands.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionCommandResponse>
    {
        private readonly IAccountReadRepository _accountReadRepository;
        private readonly IAccountWriteRepository _accountWriteRepository;
        private readonly ITransactionWriteRepository _transactionWriteRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ILogger<CreateTransactionCommandHandler> _logger;



        public CreateTransactionCommandHandler(IAccountReadRepository accRead, IAccountWriteRepository accWrite, ITransactionWriteRepository trxWrite, IHttpContextAccessor httpContextAccessor, ILogger<CreateTransactionCommandHandler> logger)
        {
            _accountReadRepository = accRead;
            _accountWriteRepository = accWrite;
            _transactionWriteRepository = trxWrite;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;


        }

        public async Task<CreateTransactionCommandResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value
          ?? _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

            _logger.LogInformation("🧩 Extracted userId: {UserId}", userId);

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedGuid))
            {
                _logger.LogError("❌ Kullanıcı kimliği null veya geçersiz GUID.");
                return new CreateTransactionCommandResponse
                {
                    Success = false,
                    Message = "Kullanıcı kimliği alınamadı."
                };
            }


            // Kullanıcının tüm hesaplarını çek
            var userAccounts = _accountReadRepository
                .GetWhere(a => a.UserGuidId == parsedGuid)
                .ToList(); // Burada tüm hesapları tek seferde çekiyoruz

            _logger.LogInformation("Kullanıcının hesap sayısı: {Count}", userAccounts.Count);

            // Gönderen hesap bu hesaplardaysa işleme devam et
            var fromAccount = userAccounts.FirstOrDefault(a => a.AccountNumber == request.FromAccountNumber);
            var toAccount = userAccounts.FirstOrDefault(a => a.AccountNumber == request.ToAccountNumber);
            if (fromAccount == null || toAccount == null)
            {
                return new CreateTransactionCommandResponse
                {
                    Success = false,
                    Message = "Hesap(lar) bulunamadı."
                };
            }

            _logger.LogInformation("Token'dan gelen GUID: {Guid}", parsedGuid);
            _logger.LogInformation("fromAccount.UserGuidId: {UserGuid}", fromAccount.UserGuidId);
            _logger.LogInformation("toAccount.UserGuidId: {UserGuid}", toAccount.UserGuidId);


            if (fromAccount == null || toAccount == null)
                return new CreateTransactionCommandResponse { Success = false, Message = "Hesap(lar) bulunamadı." };

            if (fromAccount.UserGuidId != parsedGuid)
                return new CreateTransactionCommandResponse { Success = false, Message = "Bu hesaptan işlem yapma yetkiniz yok." };

            if (fromAccount.Currency != toAccount.Currency)
                return new CreateTransactionCommandResponse { Success = false, Message = "Farklı para birimleri arasında transfer yapılamaz." };

            if (fromAccount.Balance < request.Amount)
                return new CreateTransactionCommandResponse { Success = false, Message = "Yetersiz bakiye." };

            // Bakiye güncelle
            fromAccount.Balance -= request.Amount;
            toAccount.Balance += request.Amount;
            // 1. UpdatedBy doldur
            fromAccount.UpdatedBy = parsedGuid.ToString();
            toAccount.UpdatedBy = parsedGuid.ToString();

            // 2. Değişiklikleri EF'e bildir
            _accountWriteRepository.Update(fromAccount);
            _accountWriteRepository.Update(toAccount);

   
            var transaction = new Transaction
            {
                FromAccountId = (int)fromAccount.Id,
                ToAccountId = (int)toAccount.Id,
                Amount = request.Amount,
                Description = request.Description,
                TransactionDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId,
                UpdatedBy = userId, // 🔥 burası eksikti
                UniqueId = Guid.NewGuid()
            };


            await _transactionWriteRepository.AddAsync(transaction);
            _logger.LogWarning("fromAccount.UpdatedBy: {from}", fromAccount.UpdatedBy);
            _logger.LogWarning("toAccount.UpdatedBy: {to}", toAccount.UpdatedBy);

            await _transactionWriteRepository.SaveAsync();
            // 3. Değişiklikleri kaydet
            await _accountWriteRepository.SaveAsync();
            return new CreateTransactionCommandResponse
            {
                Success = true,
                Message = "Transfer başarılı."
            };
        }


    }
}

