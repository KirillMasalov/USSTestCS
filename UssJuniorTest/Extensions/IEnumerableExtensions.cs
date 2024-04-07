using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UssJuniorTest.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> queryable, int page, int pageSize)
        {
            return queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
