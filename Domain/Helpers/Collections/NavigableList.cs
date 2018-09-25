﻿using Domain.Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Helpers.Collections
{
    public class NavigableList<T> : List<NavigableItem<T>>
    {
        public NavigableList(List<T> itens)
        {
            itens.ForEach( item => this.Add(new NavigableItem<T>(item)));
            this.FirstOrDefault()?.Set(x => x.IsFirst = true);
            this.LastOrDefault()?.Set(x => x.IsLast = true);
        }
    }

    public class NavigableItem<T>
    {
        public NavigableItem(T item)
        {
            Item = item;
        }

        public T Item { get; set; }

        public bool IsFirst { get; set; }

        public bool IsLast { get; set; }

        public bool HasMore { get => !IsLast; }

        public static implicit operator NavigableItem<T>(T item) => new NavigableItem<T>(item);

        public static implicit operator T(NavigableItem<T> item) => item.Item;
    }

    public static class NavigableListExtensions
    {
        public static NavigableList<T> ToNavigableList<T>(this List<T> itens) => new NavigableList<T>(itens);
    }
}
