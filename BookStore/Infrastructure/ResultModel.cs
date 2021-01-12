using System;

namespace BookStore.Infrastructure
{
    internal sealed class ResultModel<T>
    {
        public T Data { get; private set; }

        public Exception Error { get; private set; }

        private ResultModel()
        {
        }

        public static ResultModel<T> Create(T data)
        {
            return new ResultModel<T>
            {
                Data = data,
                Error = null
            };
        }

        public static ResultModel<T> Create(Exception error)
        {
            return new ResultModel<T>
            {
                Error = error
            };
        }
    }
}
