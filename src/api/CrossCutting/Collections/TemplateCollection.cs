using Infrastructure.CrossCutting.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.CrossCutting.Collections
{
    public class TemplateCollection<T> : List<TemplateCollectionItem<T>>
    {
        public TemplateCollection(IEnumerable<T> items)
        {
            foreach (var item in items)
                this.Add(new TemplateCollectionItem<T>(item));

            this.FirstOrDefault().IfNotNull().Then(x => x.IsFirst = true);
            this.LastOrDefault().IfNotNull().Then(x => x.IsLast = true);
        }
    }

    public class TemplateCollectionItem<T>
    {
        public TemplateCollectionItem(T item)
        {
            Item = item;
        }

        public T Item { get; set; }

        public bool IsFirst { get; set; }

        public bool IsLast { get; set; }

        public bool HasMore { get => !IsLast; }

        public static implicit operator TemplateCollectionItem<T>(T item) => new TemplateCollectionItem<T>(item);

        public static implicit operator T(TemplateCollectionItem<T> item) => item.Item;
    }

    public static class TemplateCollectionExtensions
    {
        public static TemplateCollection<T> ToNavigableList<T>(this IEnumerable<T> items) => new TemplateCollection<T>(items);
    }
}
