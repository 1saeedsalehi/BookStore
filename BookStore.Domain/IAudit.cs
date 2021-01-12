using System;

namespace BookStore.Domain
{
    public interface IAudit
    {
        DateTimeOffset? CreatedOn { get; set; }
        int? CreatedBy { get; set; }

        DateTimeOffset? ModifiedOn { get; set; }
        int? ModifiedBy { get; set; }

        DateTimeOffset? DeletedOn { get; set; }
        int? DeletedBy { get; set; }

        bool? IsDeleted { get; set; }
    }
}
