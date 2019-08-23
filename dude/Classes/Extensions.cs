using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace dude
{
    public static class Extensions
    {
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> enumerable)
        {
            return new BindingList<T>(enumerable.ToList());
        }
    }
}