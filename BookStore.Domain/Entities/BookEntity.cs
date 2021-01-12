using BookStore.Domain.ValueObjects;
using System;

namespace BookStore.Domain.Entities
{
    public class BookEntity : BaseEntity, IAudit
    {
        public NotEmptyString Name { get; set; }
        public NotEmptyString Title { get; set; }
        public string AuthorName { get; set; }
        public string Translator { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public DateTimeOffset PublishedOn { get; set; }

        public string CoverImage { get; set; }
        public string Descripion { get; set; }
        public Money Price { get; set; }

        #region Audit

        public string Keywords { get; set; } //may be a  comma separated value object 🤔
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
