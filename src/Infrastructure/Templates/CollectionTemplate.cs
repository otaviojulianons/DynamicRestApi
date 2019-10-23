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

    public class ItemTemplate<T>
    {
        public ItemTemplate(T item)
        {
            Item = item;
        }

        public T Item { get; set; }

        public bool IsFirst { get; set; }

        public bool IsLast { get; set; }

        public bool HasMore { get => !IsLast; }

        public static implicit operator ItemTemplate<T>(T item) => new ItemTemplate<T>(item);

        public static implicit operator T(ItemTemplate<T> item) => item.Item;
    }

    public static class CollectionTemplateExtensions
    {
        public static CollectionTemplate<T> ToNavigableList<T>(this IEnumerable<T> items) => new CollectionTemplate<T>(items);
    }
}
