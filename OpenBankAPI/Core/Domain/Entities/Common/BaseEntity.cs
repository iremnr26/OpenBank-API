using System;
namespace OpenBankAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        public long Id { get; set; } // Benzersiz ID

        public Guid UniqueId { get; set; } // Benzersiz kimlik

        public DateTime CreatedDate { get; set; } // Oluşturulma tarihi

        public DateTime? UpdatedDate { get; set; } // Son güncellenme tarihi 

        public string UpdatedBy { get; set; } // Güncelleyen kullanıcı

        public string CreatedBy { get; set; } // Güncelleyen kullanıcı

        public bool IsDeleted { get; set; } // Soft delete özelliği
    }
}