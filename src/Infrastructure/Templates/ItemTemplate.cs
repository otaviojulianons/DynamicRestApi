namespace Infrastructure.Templates
{
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
}
