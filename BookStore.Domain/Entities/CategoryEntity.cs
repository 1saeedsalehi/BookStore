using BookStore.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class CategoryEntity : BaseEntity, IAudit
    {

        public NotEmptyString Name { get; set; }
        public string Description { get; set; }

        public CategoryEntity Parent { get; set; }
        public int? ParentId { get; set; }
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
