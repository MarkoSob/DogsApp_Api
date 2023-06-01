using DogsApp_Core;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;

namespace DogsApp_DAL.Helpers
{
    public class SortHelper<T> : ISortHelper<T>
    {
        public IQueryable<T> ApplySort(IQueryable<T> entities, string atribute, string order)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if (string.IsNullOrWhiteSpace(atribute))
            {
                return entities;
            }
           
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(atribute, StringComparison.InvariantCultureIgnoreCase));

            string sortingOrder = !string.IsNullOrEmpty(order) && order.Equals(AppConstants.SortDescending) ? "descending" : string.Empty;
            string orderQueryString = $"{objectProperty!.Name} {sortingOrder}";
            var orderByParameter =  entities.OrderBy(orderQueryString);

            return orderByParameter;
        }
    }
}
