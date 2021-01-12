using BookStore.Core;
using BookStore.Core.Exceptions;
using System.Net;

namespace BookStore.Domain.ValueObjects
{
    public class Money : ValueOf<decimal,Money>
    {
        protected override void Validate()
        {
            if (Val < 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, Messages.NegativePriceError);
            }
            base.Validate();
        }
    }
}
