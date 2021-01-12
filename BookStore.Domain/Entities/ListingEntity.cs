using BookStore.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class ListingEntity : BaseEntity,IAudit
    {
        public NotEmptyString Title { get; set; }
        public int DisplayOrder { get; set; }
        public int PageSize { get; set; }

        

        #region Audit

        public DateTimeOffset? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
        public bool? IsDeleted { get; set; }


        #endregion

       
    }
}
