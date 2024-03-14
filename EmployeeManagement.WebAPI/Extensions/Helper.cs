namespace EmployeeManagement.WebAPI.Extensions
{
    public static class Helper
    {
        public static bool HasValue(this IEnumerable<object> data)
        {
            return data != null && data.Count() > 0;
        }
    }
}
