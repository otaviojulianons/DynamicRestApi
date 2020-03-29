using Common.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Templates
{
    public class CollectionTemplate<T> : List<ItemTemplate<T>>
    {
        public CollectionTemplate(IEnumerable<T> items)
        {
            foreach (var item in items)
                this.Add(new ItemTemplate<T>(item));

            this.FirstOrDefault().IfNotNull().Then(x => x.IsFirst = true);
            this.LastOrDefault().IfNotNull().Then(x => x.IsLast = true);
        }
    }
}
