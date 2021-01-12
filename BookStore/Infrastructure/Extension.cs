namespace BookStore.Infrastructure
{
    internal static class Extension
    {
        public static ResultModel<T> ToResultModel<T>(this T obj)
        {
            return ResultModel<T>.Create(obj);
        }
    }
}
