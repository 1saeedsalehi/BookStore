using BookStore.Core;
using BookStore.Core.Exceptions;

namespace BookStore.Domain.ValueObjects
{
    public class NotEmptyString : ValueOf<string,NotEmptyString>
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Value))
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, Messages.NotEmptystirngError);
            }
            base.Validate();
        }
    }
}
